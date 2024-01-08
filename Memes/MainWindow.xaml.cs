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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Catalog catalog;
        private SaveWindow saveWindow;
        public MainWindow()
        {
            InitializeComponent();
            catalog = new Catalog();
            catalog.LoadMem();
            catalog.UpdateMemList(lb_mem);
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            bool Pc = false;
            saveWindow = new SaveWindow(Pc);
            saveWindow.Show();
        }

        private void Button_SavePC_Click(object sender, RoutedEventArgs e)
        {
            bool Pc = true;
            saveWindow = new SaveWindow(Pc);
            saveWindow.Show();
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            catalog.SearchMem(lb_mem, tbSearch);
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            catalog.DeleteMem(lb_mem);
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            catalog.LoadMem();
            catalog.UpdateMemList(lb_mem);
        }
        private void ListBoxMem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            catalog.ChangeSelectedMem(lb_mem, ImageMem);
        }

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            catalog.SortMem(lb_mem, ComboBoxCategory);
        }

        private void Button_Tag_Click(object sender, RoutedEventArgs e)
        {
            catalog.SearchTag(lb_mem, tbSearch);
        }
    }
}
