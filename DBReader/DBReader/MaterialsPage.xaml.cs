using DBReader.CompanyDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для MaterialsPage.xaml
    /// </summary>
    public partial class MaterialsPage : Page
    {
        public MaterialsTableAdapter Adapter { get; } = new MaterialsTableAdapter();

        public MaterialsPage()
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
                Adapter.InsertMaterial("Record" + (Adapter.GetData().Rows.Count + 1), 0);

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
                    Adapter.DeleteMaterialByID((int)drv.Row[0]);
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
                AmountTextBox.Text = dataRow.Row["Amount"].ToString();
            }
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                if (TableDataGrid.SelectedItem != null)
                {
                    Adapter.UpdateMaterial(
                        NameTextBox.Text,
                        int.Parse(AmountTextBox.Text),
                        (int)(TableDataGrid.SelectedItem as DataRowView).Row[0]
                    );

                    int lastSelectedID = TableDataGrid.SelectedIndex;

                    TableDataGrid.ItemsSource = Adapter.GetData();

                    TableDataGrid.SelectedIndex = lastSelectedID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void AmountTextBox_Validate(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.numbersRegex.IsMatch(e.Text) || !Utils.numbersRegex.IsMatch(AmountTextBox.Text + e.Text);
        }
    }
}
