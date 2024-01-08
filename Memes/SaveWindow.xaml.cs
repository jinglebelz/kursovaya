using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Memes
{
    /// <summary>
    /// Логика взаимодействия для SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        private Catalog catalog;
        public SaveWindow(bool Pc)
        {
            InitializeComponent();
            catalog = new Catalog();
            if (Pc)
            {
                labelURL.Visibility = Visibility.Hidden;
                tbURL.Visibility = Visibility.Hidden;
            }
            else
            {
                pcPhoto = false;
            }
        }
        bool pcPhoto = true;
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if (pcPhoto) 
            {
                catalog.SaveMemPC(tbName, cb_category, tbTag.Text.Split().ToList());
            }
            else 
            {
                catalog.SaveMemURL(tbName, tbURL, cb_category, tbTag.Text.Split().ToList());
            }
        }
    }
}
