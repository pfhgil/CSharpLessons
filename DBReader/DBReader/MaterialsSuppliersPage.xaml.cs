using DBReader.CompanyDataSetTableAdapters;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
using System.IO;

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для MaterialsSuppliersPage.xaml
    /// </summary>
    public partial class MaterialsSuppliersPage : Page
    {
        public MaterialsSuppliersTableAdapter Adapter { get; } = new MaterialsSuppliersTableAdapter();

        public MaterialsSuppliersPage()
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
                Adapter.InsertMaterialsSupplier("Record" + (Adapter.GetData().Rows.Count + 1));

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
                    Adapter.DeleteMaterialsSupplierByID((int)drv.Row[0]);
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
                Adapter.UpdateMaterialsSupplier(
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

        // МНЕ ПОФИГ НА ПОВТОРЯЮЩИЙСЯ КОД, ЛЕНЬ ЭТО ВСЕ СОКРАЩАТЬ. ПОЙДУ ЛУЧШЕ ПОТРОГАЮ ТРАВУ ***** ИЛИ ЗАЙМУСЬ ДРУГИМИ ДЕЛАМИ ****
        private void ExportTableButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true };
            var showRes = dialog.ShowDialog();

            if (showRes == CommonFileDialogResult.Ok)
            {
                File.WriteAllText(dialog.FileName + System.IO.Path.DirectorySeparatorChar + Adapter.GetData().TableName + ".json", JsonConvert.SerializeObject(Adapter.GetData(), Formatting.Indented));
            }
        }

        private void ImportTableButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();

            var showRes = dialog.ShowDialog();

            if (showRes == CommonFileDialogResult.Ok)
            {
                MessageBox.Show(File.ReadAllText(dialog.FileName));

                List<JObject> deserializedList = JsonConvert.DeserializeObject<List<JObject>>(File.ReadAllText(dialog.FileName));

                foreach (JObject obj in deserializedList)
                {
                    if (Adapter.GetMaterialsSupplierByName((string)obj["Name"]).Rows.Count == 0)
                    {
                        Adapter.InsertMaterialsSupplier((string)obj["Name"]);
                    }
                }

                TableDataGrid.ItemsSource = Adapter.GetData();
            }
        }
    }
}
