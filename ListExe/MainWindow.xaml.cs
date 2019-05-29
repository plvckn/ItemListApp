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
using ItemListLibrary;

namespace ListExe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Item InitialItem;
        public List<string> ItemNames = new List<string>();

        
        public MainWindow()
        {
            InitializeComponent();

            btnCreateList.IsEnabled = false;
            btnPrintItems.IsEnabled = false;
            btnSortAscending.IsEnabled = false;
            btnSortDescending.IsEnabled = false;
            btnSumItem.IsEnabled = false;
        }

        private void BtnCreateStartingList_Click(object sender, RoutedEventArgs e)
        {
            InitialItem = new Item("main");
            MessageBox.Show("Initial object '" + InitialItem.Name + "' created");
            Update_ComboBox(this, new EventArgs());

            btnCreateStartingList.IsEnabled = false;
            btnCreateList.IsEnabled = true;
            btnPrintItems.IsEnabled = true;
            btnSortAscending.IsEnabled = true;
            btnSortDescending.IsEnabled = true;
            btnSumItem.IsEnabled = true;
        }
        private void ComboBoxItemNames_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void btnCreateList_Click(object sender, RoutedEventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("How many items do add", "Add to list", "0", 250, 200);
            int count = 0;
            int.TryParse(input, out count);
            var name = ComboBoxItemNames.SelectedItem.ToString();
            Item.CreateList(Item.LocateItem(InitialItem, name), count);
            Update_ComboBox(this, new EventArgs());
            MessageBox.Show(count.ToString() + " Items created in item: " + name);
        }

        private void BtnSumItem_Click(object sender, RoutedEventArgs e)
        {
            var name = ComboBoxItemNames.SelectedItem.ToString();
            var sum = Item.SumItem(InitialItem, name);
            MessageBox.Show("The sum of selected item:" + sum.ToString());
        }

        private void BtnSortAscending_Click(object sender, RoutedEventArgs e)
        {
            var name = ComboBoxItemNames.SelectedItem.ToString();
            Item.SortItem(InitialItem, name);
            MessageBox.Show("Item " + name + " sorted in ascending order");
        }

        private void BtnSortDescending_Click(object sender, RoutedEventArgs e)
        {
            var name = ComboBoxItemNames.SelectedItem.ToString();
            Item.SortItem(InitialItem, name, ascending: false);
            MessageBox.Show("Item " + name + " sorted in descending order");
        }

        private void BtnPrintItems_Click(object sender, RoutedEventArgs e)
        {
            var itemList = Item.PrintList(InitialItem, new StringBuilder());
            MessageBox.Show(itemList);
        }

        private void BtnExitWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private static List<string> GetItemNames(Item item, List<string> names)
        {
            names.Add(item.Name);
            foreach (var subitem in item.SubItem)
            {
                GetItemNames(subitem, names);
            }
            return names;
        }

        private void Update_ComboBox(object sender, EventArgs e)
        {
            ItemNames.Clear();
            ComboBoxItemNames.Items.Clear();
            var itemList = GetItemNames(InitialItem, ItemNames);
            foreach (var item in itemList)
            {
                ComboBoxItemNames.Items.Add(item);
            }
            ComboBoxItemNames.SelectedIndex = 0;
        }
    }
}
