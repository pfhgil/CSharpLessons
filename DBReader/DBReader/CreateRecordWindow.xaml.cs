using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для CreateRecordWindow.xaml
    /// </summary>
    public partial class CreateRecordWindow : Window
    {    
        private DataGrid newRecordGrid;

        private TextBlock logTextBlock;

        private DataTable editingTable;

        // reference to main window
        public PagesWindow mainWindow;

        private object currentTableAdapter;

        public CreateRecordWindow()
        {
            InitializeComponent();

            newRecordGrid = (DataGrid)FindName("NewRecordGrid");

            logTextBlock = (TextBlock)FindName("LogTextBlock");
        }

        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                if (currentTableAdapter is PostTableAdapter postTableAdapter)
                {
                    postTableAdapter.InsertPost((int)editingTable.Rows[0][0], (string)editingTable.Rows[0][1]);
                }
                else if (currentTableAdapter is StaffTableAdapter staffTableAdapter)
                {
                    staffTableAdapter.InsertEmployee((string)editingTable.Rows[0][0], (int) editingTable.Rows[0][1]);
                }

                logTextBlock.Text = "No errors";
            }
            catch (Exception ex)
            {
                logTextBlock.Text = ex.Message;
            }

            mainWindow.UpdateDataGrid();
            */
        }

        public void CreateEditor(object tableAdapter)
        {
            /*
            DataTable dataTable = mainWindow.GetDataTable(tableAdapter);
            if (dataTable == null) return;

            currentTableAdapter = tableAdapter;

            logTextBlock.Text = "No errors";

            newRecordGrid.Columns.Clear();  

            editingTable = new DataTable(dataTable.TableName);
            newRecordGrid.CanUserAddRows = false;

            foreach (DataColumn dc in dataTable.Columns)
            {
                if (dc.ColumnName == dataTable.PrimaryKey[0].ColumnName && dataTable.PrimaryKey[0].AutoIncrement) continue;

                editingTable.Columns.Add(dc.ColumnName, dc.DataType);
            }

            editingTable.Rows.Add();

            newRecordGrid.ItemsSource = editingTable.DefaultView;
            */
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
                