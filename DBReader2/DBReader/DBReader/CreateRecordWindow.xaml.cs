using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для CreateRecordWindow.xaml
    /// </summary>
    public partial class CreateRecordWindow : Window
    {    
        private DataGrid newRecordGrid;

        private TextBlock logTextBlock;

        private DataTable originalDataTable; 
        private DataTable editingTable;

        // reference to main window
        public MainWindow mainWindow;

        public CreateRecordWindow()
        {
            InitializeComponent();

            newRecordGrid = (DataGrid)FindName("NewRecordGrid");

            logTextBlock = (TextBlock)FindName("LogTextBlock");
        }

        // топчик
        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "IF(OBJECTPROPERTY(OBJECT_ID('dbo." + 
                editingTable.TableName +
                "'), 'TableHasIdentity') = 1) SET IDENTITY_INSERT dbo." + editingTable.TableName + " ON\n" +
                "INSERT INTO dbo." + editingTable.TableName + " (";

            for(int i = 0; i < editingTable.Columns.Count; i++)
            {
                var item = editingTable.Columns[i];

                if (i < editingTable.Columns.Count - 1)
                {
                    sqlQuery += FormatItem(item) + ", ";
                }
                else
                {
                    sqlQuery += FormatItem(item) + ") VALUES (";
                }
            }

            for (int i = 0; i < editingTable.Rows[0].ItemArray.Length; i++)
            {
                var item = editingTable.Rows[0].ItemArray[i];

                if(i < editingTable.Rows[0].ItemArray.Length - 1)
                {
                    sqlQuery += FormatItem(item) + ", ";
                }
                else
                {
                    sqlQuery += FormatItem(item) + ")";
                }
            }

            MessageBox.Show(sqlQuery, "Query text");

            try
            {
                SqlCommand cmd = new SqlCommand(sqlQuery, mainWindow.sqlConnection);
                cmd.ExecuteNonQuery();

                originalDataTable.ImportRow(editingTable.Rows[0]);

                logTextBlock.Text = "No errors";
            } 
            catch (Exception ex)
            {
                logTextBlock.Text = ex.Message;
            }  

            mainWindow.UpdateDataGrid();
        }

        private string FormatItem(object item)
        {
            return item is string ? "'" + item + "'" : "" + item;
        }

        public void CreateEditor(DataTable dataTable)
        {
            logTextBlock.Text = "No errors";

            originalDataTable = dataTable;

            newRecordGrid.Columns.Clear();  

            editingTable = new DataTable(dataTable.TableName);
            newRecordGrid.CanUserAddRows = false;

            foreach (DataColumn dc in dataTable.Columns)
            {
                editingTable.Columns.Add(dc.ColumnName, dc.DataType);
            }

            editingTable.Rows.Add();
             
            newRecordGrid.ItemsSource = editingTable.DefaultView;
        }

        private void CreateRecordWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
                