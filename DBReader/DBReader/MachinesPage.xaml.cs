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
    /// Логика взаимодействия для MachinesPage.xaml
    /// </summary>
    public partial class MachinesPage : Page
    {
        public MachinesTableAdapter Adapter { get; } = new MachinesTableAdapter();

        public MachinesPage()
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
                if (AuthorizationWindow.PagesWindow.MaterialsPage.Adapter.GetData().Rows.Count != 0)
                {
                    Adapter.InsertMachine("Record" + (Adapter.GetData().Rows.Count + 1), 1);

                    TableDataGrid.ItemsSource = Adapter.GetData();
                }
                else
                {
                    MessageBox.Show("Невозможно создать новый станок. В таблице с материалами нет записей.");
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
                    Adapter.DeleteMachineByID((int)drv.Row[0]);
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

                Utils.FillComboBoxFromDataGrid(MaterialComboBox, AuthorizationWindow.PagesWindow.MaterialsPage.TableDataGrid, "Name");

                MaterialComboBox.SelectedIndex = Utils.FindIndexInComboBox(MaterialComboBox, dataRow,
                        (string)AuthorizationWindow.PagesWindow.MaterialsPage.Adapter.GetMaterialByID((int)dataRow.Row["MaterialID"]).Rows[0]["Name"]);
            }
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableDataGrid.SelectedItem != null)
            {
                Adapter.UpdateMachine(
                    NameTextBox.Text,
                    (int)AuthorizationWindow.PagesWindow.MaterialsPage.Adapter.GetMaterialByName(MaterialComboBox.SelectedItem.ToString()).Rows[0][0],
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
