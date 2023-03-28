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
    /// Логика взаимодействия для StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        public StaffTableAdapter Adapter { get; } = new StaffTableAdapter();

        public StaffPage()
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
                if (AuthorizationWindow.PagesWindow.PostPage.Adapter.GetData().Rows.Count != 0 &&
                    AuthorizationWindow.PagesWindow.FactoriesPage.Adapter.GetData().Rows.Count != 0 &&
                    AuthorizationWindow.PagesWindow.MachinesPage.Adapter.GetData().Rows.Count != 0)
                {
                    Adapter.InsertStaff("", "", "", "Record" + (Adapter.GetData().Rows.Count + 1), "", 1, 1, 1);

                    TableDataGrid.ItemsSource = Adapter.GetData();
                }
                else
                {
                    MessageBox.Show("Невозможно создать новый станок. В таблице с должностями/заводами/станками нет записей.");
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
                    Adapter.DeleteEmployeeByID((int)drv.Row[0]);
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
            try
            {
                if (TableDataGrid.SelectedItem != null)
                {
                    DataRowView dataRow = TableDataGrid.SelectedItem as DataRowView;

                    SurnameTextBox.Text = dataRow.Row["Surname"].ToString();
                    NameTextBox.Text = dataRow.Row["Name"].ToString();
                    PatronymicTextBox.Text = dataRow.Row["Patronymic"].ToString();
                    LoginTextBox.Text = dataRow.Row["Login"].ToString();
                    PasswordTextBox.Text = dataRow.Row["Password"].ToString();

                    Utils.FillComboBoxFromDataGrid(PostComboBox, AuthorizationWindow.PagesWindow.PostPage.TableDataGrid, "Name");
                    Utils.FillComboBoxFromDataGrid(FactoryComboBox, AuthorizationWindow.PagesWindow.FactoriesPage.TableDataGrid, "Address");
                    Utils.FillComboBoxFromDataGrid(MachineComboBox, AuthorizationWindow.PagesWindow.MachinesPage.TableDataGrid, "Name");

                    PostComboBox.SelectedIndex = Utils.FindIndexInComboBox(PostComboBox, dataRow, 
                        (string)AuthorizationWindow.PagesWindow.PostPage.Adapter.GetPostByID((int)dataRow.Row["PostID"]).Rows[0]["Name"]);

                    FactoryComboBox.SelectedIndex = Utils.FindIndexInComboBox(FactoryComboBox, dataRow,
                        (string)AuthorizationWindow.PagesWindow.FactoriesPage.Adapter.GetFactoryByID((int)dataRow.Row["FactoryID"]).Rows[0]["Address"]);

                    MachineComboBox.SelectedIndex = Utils.FindIndexInComboBox(MachineComboBox, dataRow,
                        (string)AuthorizationWindow.PagesWindow.MachinesPage.Adapter.GetMachineByID((int)dataRow.Row["MachineID"]).Rows[0]["Name"]);
                }
            }
            catch
            {

            }
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableDataGrid.SelectedItem != null)
            {
                DataTable foundMachine = MachineComboBox.SelectedItem == null ? null: AuthorizationWindow.PagesWindow.MachinesPage.Adapter.GetMachineByName(MachineComboBox.SelectedItem.ToString());

                Adapter.UpdateEmployee(
                    SurnameTextBox.Text,
                    NameTextBox.Text,
                    PatronymicTextBox.Text,
                    LoginTextBox.Text,
                    PasswordTextBox.Text,
                    (int)AuthorizationWindow.PagesWindow.PostPage.Adapter.GetPostByName(PostComboBox.SelectedItem.ToString()).Rows[0][0],
                    (int)AuthorizationWindow.PagesWindow.FactoriesPage.Adapter.GetFactoryByName(FactoryComboBox.SelectedItem.ToString()).Rows[0][0],
                    foundMachine == null ? (int?) null : (int)foundMachine.Rows[0][0],
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

        private void SurnameTextBox_Validate(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.namesAllowedRegex.IsMatch(e.Text) || !Utils.namesAllowedRegex.IsMatch(SurnameTextBox.Text + e.Text);
        }

        private void NameTextBox_Validate(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.namesAllowedRegex.IsMatch(e.Text) || !Utils.namesAllowedRegex.IsMatch(NameTextBox.Text + e.Text);
        }

        private void PatronymicTextBox_Validate(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.namesAllowedRegex.IsMatch(e.Text) || !Utils.namesAllowedRegex.IsMatch(PatronymicTextBox.Text + e.Text);
        }

        private void LoginTextBox_Validate(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.loginRegex.IsMatch(e.Text) || !Utils.loginRegex.IsMatch(LoginTextBox.Text + e.Text);
        }
    }
}
