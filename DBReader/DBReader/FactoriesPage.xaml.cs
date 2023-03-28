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
using static DBReader.CompanyDataSet;

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для FactoriesTablePage.xaml
    /// </summary>
    public partial class FactoriesPage : Page
    {
        public FactoriesTableAdapter Adapter { get; } = new FactoriesTableAdapter();

        public FactoriesPage()
        {
            InitializeComponent();

            TableDataGrid.CanUserAddRows = false;
            Utils.DisableDataGridEditing(TableDataGrid);
            TableDataGrid.ItemsSource = Adapter.GetData();
        }

        private void NextTableButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow.PagesWindow.IncrementCurrentTable(1);
        }

        private void PreviousTableButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow.PagesWindow.IncrementCurrentTable(-1);
        }

        private void CreateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AuthorizationWindow.PagesWindow.MaterialsSuppliersPage.Adapter.GetData().Rows.Count != 0 &&
                    AuthorizationWindow.PagesWindow.FurnitureCarriersPage.Adapter.GetData().Rows.Count != 0)
                {
                    Adapter.InsertFactory("Record" + (Adapter.GetData().Rows.Count + 1), 1, 1);

                    TableDataGrid.ItemsSource = Adapter.GetData();
                }
                else
                {
                    MessageBox.Show("Невозможно создать новый станок. В таблице с поставщиками материалов/перевозчиками мебели нет записей.");
                }
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
                    Adapter.DeleteFactoryByID((int)drv.Row[0]);
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
            if(TableDataGrid.SelectedItem != null)
            {
                DataRowView dataRow = TableDataGrid.SelectedItem as DataRowView;

                AddressTextBox.Text = dataRow.Row["Address"].ToString();

                Utils.FillComboBoxFromDataGrid(MatSupplierComboBox, AuthorizationWindow.PagesWindow.MaterialsSuppliersPage.TableDataGrid, "Name");
                Utils.FillComboBoxFromDataGrid(FurnitureCarriersComboBox, AuthorizationWindow.PagesWindow.FurnitureCarriersPage.TableDataGrid, "Name");

                MatSupplierComboBox.SelectedIndex = Utils.FindIndexInComboBox(MatSupplierComboBox, dataRow,
                        (string)AuthorizationWindow.PagesWindow.MaterialsSuppliersPage.Adapter.GetMaterialsSupplierByID((int)dataRow.Row["MatSupplierID"]).Rows[0]["Name"]);

                FurnitureCarriersComboBox.SelectedIndex = Utils.FindIndexInComboBox(FurnitureCarriersComboBox, dataRow,
                        (string)AuthorizationWindow.PagesWindow.FurnitureCarriersPage.Adapter.GetFurnitureCarrierByID((int)dataRow.Row["FurnitureCarrierID"]).Rows[0]["Name"]);
            }
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if(TableDataGrid.SelectedItem != null)
            {
                Adapter.UpdateFactory(
                    AddressTextBox.Text,
                    (int) AuthorizationWindow.PagesWindow.MaterialsSuppliersPage.Adapter.GetMaterialsSupplierByName(MatSupplierComboBox.SelectedItem.ToString()).Rows[0][0],
                    (int) AuthorizationWindow.PagesWindow.FurnitureCarriersPage.Adapter.GetFurnitureCarrierByName(FurnitureCarriersComboBox.SelectedItem.ToString()).Rows[0][0],
                    (int) (TableDataGrid.SelectedItem as DataRowView).Row[0]
                );

                int lastSelectedID = TableDataGrid.SelectedIndex;

                TableDataGrid.ItemsSource = Adapter.GetData();

                TableDataGrid.SelectedIndex = lastSelectedID;
            }
        }
    }
}
