using DBReader.CompanyDataSetTableAdapters;
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

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для FurnitureCarriersTablePage.xaml
    /// </summary>
    public partial class FurnitureCarriersPage : Page
    {
        public PagesWindow pagesWindow;

        private FurnitureCarriersTableAdapter adapter = new FurnitureCarriersTableAdapter();

        public FurnitureCarriersPage()
        {
            InitializeComponent();

            TableDataGrid.ItemsSource = adapter.GetData();
        }

        private void NextTableButton_Click(object sender, RoutedEventArgs e)
        {
            pagesWindow.IncrementCurrentTable(1);
        }

        private void PreviousTableButton_Click(object sender, RoutedEventArgs e)
        {
            pagesWindow.IncrementCurrentTable(-1);
        }

        private void CreateRecordButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditRecordButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
