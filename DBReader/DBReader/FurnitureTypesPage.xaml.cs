using DBReader.CompanyDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для FurnitureTypesPage.xaml
    /// </summary>
    public partial class FurnitureTypesPage : Page
    {
        public FurnitureTypesTableAdapter Adapter { get; } = new FurnitureTypesTableAdapter();

        public FurnitureTypesPage()
        {
            InitializeComponent();

            TableDataGrid.CanUserAddRows = false;
            Utils.DisableDataGridEditing(TableDataGrid);
            TableDataGrid.ItemsSource = Adapter.GetData();
        }

        private void CreateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Adapter.InsertFurnitureType("Record" + (Adapter.GetData().Rows.Count + 1));

                TableDataGrid.ItemsSource = Adapter.GetData();
            } 
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (DataRowView drv in TableDataGrid.SelectedItems)
                {
                    Adapter.DeleteFurnitureTypeByID((int)drv.Row[0]);
                }

                TableDataGrid.ItemsSource = Adapter.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TableDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableDataGrid.SelectedItem != null)
            {
                DataRowView dataRow = TableDataGrid.SelectedItem as DataRowView;

                NameTextBox.Text = dataRow.Row["Name"].ToString();
            }
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableDataGrid.SelectedItem != null)
            {
                Adapter.UpdateFurnitureType(
                    NameTextBox.Text,
                    (int)(TableDataGrid.SelectedItem as DataRowView).Row[0]
                );

                int lastSelectedID = TableDataGrid.SelectedIndex;

                TableDataGrid.ItemsSource = Adapter.GetData();

                TableDataGrid.SelectedIndex = lastSelectedID;
            }
        }

        private void PreviousTableButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow.PagesWindow.IncrementCurrentTable(-1);
        }

        private void NextTableButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow.PagesWindow.IncrementCurrentTable(1);
        }
    }
}
