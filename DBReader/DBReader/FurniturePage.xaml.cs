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
    /// Логика взаимодействия для FactoriesTablePage.xaml
    /// </summary>
    public partial class FurniturePage : Page
    {
        public FurnitureTableAdapter Adapter { get; } = new FurnitureTableAdapter();

        public FurniturePage()
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
                if (AuthorizationWindow.PagesWindow.FurnitureTypesPage.Adapter.GetData().Rows.Count != 0)
                {
                    Adapter.InsertFurniture("Record" + (Adapter.GetData().Rows.Count + 1), 0, 1000, 1);

                    TableDataGrid.ItemsSource = Adapter.GetData();
                }
                else
                {
                    MessageBox.Show("Невозможно создать новую мебель. В таблице с типами мебели нет записей.");
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
                    Adapter.DeleteFurnitureByID((int)drv.Row[0]);
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
                PricePerPieceTextBox.Text = dataRow.Row["PricePerPiece"].ToString();

                Utils.FillComboBoxFromDataGrid(FurnitureTypeComboBox, AuthorizationWindow.PagesWindow.FurnitureTypesPage.TableDataGrid, "Name");

                FurnitureTypeComboBox.SelectedIndex = Utils.FindIndexInComboBox(FurnitureTypeComboBox, dataRow,
                        (string)AuthorizationWindow.PagesWindow.FurnitureTypesPage.Adapter.GetFurnitureTypeByID((int)dataRow.Row["FurnitureTypeID"]).Rows[0]["Name"]);
            }
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (TableDataGrid.SelectedItem != null)
                {
                    Adapter.UpdateFurniture(
                        NameTextBox.Text,
                        int.Parse(AmountTextBox.Text),
                        int.Parse(PricePerPieceTextBox.Text),
                        (int)AuthorizationWindow.PagesWindow.FurnitureTypesPage.Adapter.GetFurnitureTypeByName(FurnitureTypeComboBox.SelectedItem.ToString()).Rows[0][0],
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

        private void AmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.numbersRegex.IsMatch(e.Text) || !Utils.numbersRegex.IsMatch(AmountTextBox.Text + e.Text);
        }

        private void PricePerPieceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.numbersRegex.IsMatch(e.Text) || !Utils.numbersRegex.IsMatch(AmountTextBox.Text + e.Text);
        }
    }
}
