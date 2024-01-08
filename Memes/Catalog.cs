using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Memes
{
    public class Catalog
    {
        List<Mem> mem = new List<Mem>();


        public void SaveMem()
        {
            var serializer = new XmlSerializer(typeof(List<Mem>));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, mem);
                File.WriteAllText("mem.xml", writer.ToString());
            }
        }
        public void LoadMem()
        {
            var xml = File.ReadAllText("mem.xml");
            var serializer = new XmlSerializer(typeof(List<Mem>));
            using (var reader = new StringReader(xml))
            {
                mem = (List<Mem>)serializer.Deserialize(reader);
            }
        }
        public void UpdateMemList(ListBox lb_mem)
        {
            lb_mem.Items.Clear();
            foreach (var memas in mem)
            {
                lb_mem.Items.Add(memas.Name);
            }
        }

        public void SaveMemURL(TextBox tbName, TextBox tbURL, ComboBox cb_category, List<string> tags)
        {
            LoadMem();
            var memas = new Mem
            {
                Name = tbName.Text,
                Type = cb_category.Text,
                Tags = tags
            };
            memas.ImageLocation = tbURL.Text;
            mem.Add(memas);
            SaveMem();
        }
        public void SaveMemPC(TextBox tbName, ComboBox cb_category, List<string> tags)
        {
            LoadMem();
            var memas = new Mem
            {
                Name = tbName.Text,
                Type = cb_category.Text,
                Tags = tags
            };
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                memas.ImageLocation = openFileDialog.FileName;
            }
            mem.Add(memas);
            SaveMem();
        }

        public void DeleteMem(ListBox lb_mem)
        {
            var selectedMeme = mem.FirstOrDefault(mem => mem.Name == (string)lb_mem.SelectedItem);
            if (selectedMeme != null)
            {
                mem.Remove(selectedMeme);
                UpdateMemList(lb_mem);
            }
            SaveMem();
        }

        public void ChangeSelectedMem(ListBox lb_mem, Image ImageMem)
        {
            var selectedMeme = mem.FirstOrDefault(mem => mem.Name == (string)lb_mem.SelectedItem);
            if (selectedMeme != null)
            {
                ImageMem.Source = new BitmapImage(new Uri(selectedMeme.ImageLocation));
            }
        }

        public void SortMem(ListBox lb_mem, ComboBox ComboBoxCategory)
        {
            if (ComboBoxCategory.SelectedIndex == 0)
            {
                LoadMem();
                UpdateMemList(lb_mem);
            }
            else
            {
                var category = (ComboBoxCategory.SelectedItem as ComboBoxItem).Content.ToString();
                var filteredMemes = mem.Where(mem => mem.Type == category).ToList();
                lb_mem.Items.Clear();
                foreach (var mem in filteredMemes)
                {
                    lb_mem.Items.Add(mem.Name);
                }
            }
        }
        public void SearchMem(ListBox lb_mem, TextBox tbSearch)
        {
            var searchText = tbSearch.Text.ToLower();
            var filteredMemes = mem.Where(mem => mem.Name.ToLower().Contains(searchText)).ToList();
            lb_mem.Items.Clear();
            foreach (var mem in filteredMemes)
            {
                lb_mem.Items.Add(mem.Name);
            }
        }
        public void SearchTag(ListBox lb_mem, TextBox tbSearch)
        {
            var searchText = tbSearch.Text.ToLower();
            lb_mem.Items.Clear();
            foreach (var memas in mem)
            {
                if (memas.Tags.Contains(searchText))
                {
                    lb_mem.Items.Add(memas.Name);
                }
            }
        }
    }
}
