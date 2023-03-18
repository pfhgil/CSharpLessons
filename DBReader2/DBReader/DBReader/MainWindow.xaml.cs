using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

        private DataTable[] dataSets = new DataTable[2];

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

            postTableAdapter.Insert(1, "Junior developer");
            postTableAdapter.Insert(2, "Middle developer");
            postTableAdapter.Insert(3, "Senior developer");

            postTableAdapter.Insert(4, "Teamlead");

            staffTableAdapter.Insert("Дмитрий", 2);
            staffTableAdapter.Insert("Иван", 1);
            staffTableAdapter.Insert("Сергей", 3);
            staffTableAdapter.Insert("Иван", 2);
            staffTableAdapter.Insert("Дмитрий", 4);

            dataSets[0] = postTableAdapter.GetData();
            dataSets[1] = staffTableAdapter.GetData();

            dbDataGrid.ItemsSource = postTableAdapter.GetData();

            createRecordWindow.mainWindow = this;
        }


        public void UpdateDataGrid()
        {
            dbDataGrid.ItemsSource = (IEnumerable)dataSets[currentTableIdx];
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

            currentTableIdx = Math.Max(0, Math.Min(currentTableIdx, dataSets.Length - 1));

            UpdateDataGrid();
        }

        private void CreateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            createRecordWindow.CreateEditor(dataSets[currentTableIdx]);

            createRecordWindow.Show();
        }
    }
}
