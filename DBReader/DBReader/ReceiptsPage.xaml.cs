using DBReader.CompanyDataSetTableAdapters;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using static DBReader.CompanyDataSet;

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для ReceiptsPage.xaml
    /// </summary>
    public partial class ReceiptsPage : Page
    {
        public ReceiptsTableAdapter Adapter { get; } = new ReceiptsTableAdapter();

        private DataTable editingTable;

        public ReceiptsPage()
        {
            InitializeComponent();

            TableDataGrid.CanUserAddRows = false;
            Utils.DisableDataGridEditing(TableDataGrid);
            TableDataGrid.ItemsSource = Adapter.GetData();     
        }

        public void Init()
        {
            AvailableFurnitureDataGrid.ItemsSource = AuthorizationWindow.PagesWindow.FurniturePage.Adapter.GetData();

            editingTable = new DataTable("ReceiptDataTable");
            FurnitureInNewReceiptDataGrid.CanUserAddRows = false;
            AvailableFurnitureDataGrid.CanUserAddRows = false;

            Utils.DisableDataGridEditing(AvailableFurnitureDataGrid);
            Utils.DisableDataGridEditing(FurnitureInNewReceiptDataGrid);

            FurnitureDataTable furnitures = AuthorizationWindow.PagesWindow.FurniturePage.Adapter.GetData();

            foreach (DataColumn dc in furnitures.Columns)
            {
                editingTable.Columns.Add(dc.ColumnName, dc.DataType);
            }
        }

        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (DataRowView drv in TableDataGrid.SelectedItems)
                {
                    Adapter.DeleteReceiptByID((int)drv.Row[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            TableDataGrid.ItemsSource = Adapter.GetData();
        }

        private void TableDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableDataGrid.SelectedItem != null)
            {
                DataRowView dataRow = TableDataGrid.SelectedItem as DataRowView;

                PurchaseAmountTextBox.Text = dataRow.Row["PurchaseAmount"].ToString();
                BuyerMoneyEditTextBox.Text = dataRow.Row["BuyerMoney"].ToString();

                Utils.FillComboBoxFromDataGrid(EmployeeComboBox, AuthorizationWindow.PagesWindow.StaffPage.TableDataGrid, "Login");
                Utils.FillComboBoxFromDataGrid(FurnitureComboBox, AuthorizationWindow.PagesWindow.FurniturePage.TableDataGrid, "Name");

                EmployeeComboBox.SelectedIndex = Utils.FindIndexInComboBox(EmployeeComboBox, dataRow,
                        (string)AuthorizationWindow.PagesWindow.StaffPage.Adapter.GetEmployeeByID((int)dataRow.Row["EmployeeID"]).Rows[0]["Login"]);

                FurnitureComboBox.SelectedIndex = Utils.FindIndexInComboBox(FurnitureComboBox, dataRow,
                        (string)AuthorizationWindow.PagesWindow.FurniturePage.Adapter.GetFurnitureByID((int)dataRow.Row["FurnitureID"]).Rows[0]["Name"]);
            }
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TableDataGrid.SelectedItem != null)
                {
                    Adapter.UpdateReceipt(
                        (int)AuthorizationWindow.PagesWindow.StaffPage.Adapter.GetEmployeeByLogin(EmployeeComboBox.SelectedItem.ToString()).Rows[0][0],
                        (int)AuthorizationWindow.PagesWindow.FurniturePage.Adapter.GetFurnitureByName(FurnitureComboBox.SelectedItem.ToString()).Rows[0][0],
                        int.Parse(PurchaseAmountTextBox.Text),
                        int.Parse(BuyerMoneyEditTextBox.Text),
                        (int)(TableDataGrid.SelectedItem as DataRowView).Row[0]
                    );

                    int lastSelectedID = TableDataGrid.SelectedIndex;

                    TableDataGrid.ItemsSource = Adapter.GetData();

                    TableDataGrid.SelectedIndex = lastSelectedID;
                }
            }
            catch(Exception ex)
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
            e.Handled = !Utils.numbersRegex.IsMatch(e.Text) || !Utils.numbersRegex.IsMatch(PurchaseAmountTextBox.Text + e.Text);
        }

        private void AddFurnitureToReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView drv in AvailableFurnitureDataGrid.SelectedItems)
            {
                editingTable.Rows.Add(drv.Row[0], drv.Row[1], drv.Row[2], drv.Row[3], drv.Row[4]);
            }

            FurnitureInNewReceiptDataGrid.ItemsSource = editingTable.DefaultView;
        }

        private void RemoveFurnitureFromReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            while(FurnitureInNewReceiptDataGrid.SelectedItems.Count > 0)
            {
                DataRowView drv = FurnitureInNewReceiptDataGrid.SelectedItems[FurnitureInNewReceiptDataGrid.SelectedItems.Count - 1] as DataRowView;
                editingTable.Rows.Remove(drv.Row);
            }

            FurnitureInNewReceiptDataGrid.ItemsSource = editingTable.DefaultView;
        }

        private void AddReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            if (BuyerMoneyTextBox.Text == "") return;

            SaveReceipt((int)AuthorizationWindow.FoundEmployee.Rows[0]["EmployeeID"], editingTable, false);
        }

        private void SaveReceipt(int sellerID, DataTable dataTable, bool isUnload)
        {
            try
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true };
                var showRes = dialog.ShowDialog();

                if (showRes == CommonFileDialogResult.Ok)
                {
                    string receiptString = "";

                    int allPurchaseCost = 0;

                    foreach (DataRow dr in dataTable.Rows)
                    {         
                        if(isUnload)
                        {
                            sellerID = (int)dr["EmployeeID"];
                        }

                        string sellerName = (string)AuthorizationWindow.PagesWindow.StaffPage.Adapter.GetEmployeeByID(sellerID).Rows[0]["Name"];

                        int boughtFurnitureID = (int)dr["FurnitureID"];
                        string boughtFurnitureName = (string)AuthorizationWindow.PagesWindow.FurniturePage.Adapter.GetFurnitureByID(boughtFurnitureID).Rows[0]["Name"];
                        int boughtFurnitureCost = isUnload ?
                            (int)dr["PurchaseAmount"] : (int)AuthorizationWindow.PagesWindow.FurniturePage.Adapter.GetFurnitureByID(boughtFurnitureID).Rows[0]["PricePerPiece"];

                        if (!isUnload)
                        {
                            Adapter.InsertReceipt((int)AuthorizationWindow.FoundEmployee.Rows[0]["EmployeeID"], boughtFurnitureID, boughtFurnitureCost, int.Parse(BuyerMoneyTextBox.Text));
                        }                  

                        receiptString += "Продал: " + sellerName + ", его ID: " + sellerID + "\n" +
                            "Куплено: " + boughtFurnitureName + ", ID: " + boughtFurnitureID + "\n" +
                            "Стоимость товара: " + boughtFurnitureCost + "\n";

                        if(isUnload)
                        {
                            receiptString += "Покупатель дал денег: " + dr["BuyerMoney"] + ". Его сдача: " + Math.Max((int)dr["BuyerMoney"] - boughtFurnitureCost, 0) + "\n\n";
                        }
                        else
                        {
                            allPurchaseCost += boughtFurnitureCost;
                            receiptString += "\n";
                        }
                    }

                    if (!isUnload)
                    {
                        receiptString += "\nПокупатель дал денег: " + BuyerMoneyTextBox.Text + ". Его сдача: " + Math.Max(int.Parse(BuyerMoneyTextBox.Text) - allPurchaseCost, 0) + "\n\n";
                    }

                    string resPath = dialog.FileName + System.IO.Path.DirectorySeparatorChar + "Заказ №" + Adapter.GetData().Rows.Count + ".txt";

                    //MessageBox.Show("заказ: " + receiptString + ", путь до файл: " + resPath);

                    File.WriteAllText(resPath, receiptString);

                    TableDataGrid.ItemsSource = Adapter.GetData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void BuyerMoneyTextBox_Validate(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.numbersRegex.IsMatch(e.Text) || !Utils.numbersRegex.IsMatch(BuyerMoneyTextBox.Text + e.Text);
        }

        private void BuyerMoneyEditTextBox_Validate(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.numbersRegex.IsMatch(e.Text) || !Utils.numbersRegex.IsMatch(BuyerMoneyEditTextBox.Text + e.Text);
        }

        private void ExportReceipt_Click(object sender, RoutedEventArgs e)
        {
            SaveReceipt(0, Adapter.GetData(), true);
        }
    }
}
