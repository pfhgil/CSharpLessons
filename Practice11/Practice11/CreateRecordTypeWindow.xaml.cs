using Practice11.BudgetDataSetTableAdapters;
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
using System.Windows.Shapes;

namespace Practice11
{
    /// <summary>
    /// Логика взаимодействия для CreateRecordTypeWindow.xaml
    /// </summary>
    public partial class CreateRecordTypeWindow : Window
    {
        public MainWindow MainWindow;

        public CreateRecordTypeWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.RecordsTypesTableAdapter.InsertType(NewRecordTypeTextBox.Text);

                MainWindow.RecordTypeComboBox.Items.Clear();

                foreach (DataRow row in MainWindow.RecordsTypesTableAdapter.GetData().Rows)
                {
                    MainWindow.RecordTypeComboBox.Items.Add(row["Name"]);
                }

                this.Visibility = Visibility.Hidden;
            }
            catch { }
        }
    }
}
