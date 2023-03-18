using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

using DBReader.DataSetTableAdapters;

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PostTableAdapter postTableAdapter = new PostTableAdapter();
        private StaffTableAdapter staffTableAdapter = new StaffTableAdapter();

        public DataGrid dbDataGrid { get; }

        public SqlConnection sqlConnection { get; } = new SqlConnection("Initial Catalog=master;Integrated Security=True;");

        private List<object> adapters = new List<object>();

        private int currentTableIdx = 0;

        // other windows
        private CreateRecordWindow createRecordWindow = new CreateRecordWindow();

        public MainWindow()
        {
            InitializeComponent();
            sqlConnection.Open();

            dbDataGrid = (DataGrid)FindName("DBDataGrid");

            DropTable("Staff");
            DropTable("Post");  

            postTableAdapter.InsertPost(1, "Junior developer");
            postTableAdapter.InsertPost(2, "Middle developer");
            postTableAdapter.InsertPost(3, "Senior developer");

            postTableAdapter.InsertPost(4, "Teamlead");

            staffTableAdapter.Insert("Дмитрий", 2);
            staffTableAdapter.Insert("Иван", 1);
            staffTableAdapter.Insert("Сергей", 3);
            staffTableAdapter.Insert("Иван", 2);
            staffTableAdapter.Insert("Дмитрий", 4);

            adapters.Add(postTableAdapter);
            adapters.Add(staffTableAdapter);

            dbDataGrid.ItemsSource = postTableAdapter.GetData();

            createRecordWindow.mainWindow = this;
        }

        public DataTable GetDataTable(object tableAdapter)
        {
            DataTable dataTable = null;
            if (tableAdapter is PostTableAdapter postTableAdapter)
            {
                dataTable = postTableAdapter.GetData();
            }
            else if (tableAdapter is StaffTableAdapter staffTableAdapter)
            {
                dataTable = staffTableAdapter.GetData();
            }

            return dataTable; 
        }

        public void UpdateDataGrid()
        {
            DataTable dataTable = GetDataTable(adapters[currentTableIdx]);
            if (dataTable == null) return;

            dbDataGrid.ItemsSource = (IEnumerable)dataTable;
        }

        private void DropTable(string name)
        {
            string deleteCom = "DELETE FROM " + name;
            SqlCommand cmd = new SqlCommand(deleteCom, sqlConnection);
            cmd.ExecuteNonQuery();
        }

        private void NextTableButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementCurrentTable(1);
        }

        private void PreviousTableButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementCurrentTable(-1);           
        }

        private void IncrementCurrentTable(int inc)
        {
            currentTableIdx += inc;

            currentTableIdx = Math.Max(0, Math.Min(currentTableIdx, adapters.Count - 1));

            UpdateDataGrid();
        }

        private void CreateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            createRecordWindow.CreateEditor(adapters[currentTableIdx]);

            createRecordWindow.Show();
        }
    }
}
