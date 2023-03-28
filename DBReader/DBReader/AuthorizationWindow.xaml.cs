using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DBReader.CompanyDataSet;

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public static PagesWindow PagesWindow { get; } = new PagesWindow();

        private static StaffDataTable foundEmployee = null;
        public static StaffDataTable FoundEmployee
        {
            get { return foundEmployee; }
        }

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Utils.loginRegex.IsMatch(LoginTextBox.Text) || LoginTextBox.Text.Length < 4)
            {
                MessageBox.Show("Неверный формат логина!" +
                    "\n-Длина логина должна быть больше 4-ёх символов" +
                    "\n-Логин может иметь только латинские буквы, цифры, а также нижнее подчеркивание" +
                    "\n-Логин должен начинаться с буквы латинского алфавита");
                return;
            }

            foundEmployee = PagesWindow.StaffPage.Adapter.GetEmployeeByLogin(LoginTextBox.Text);
            
            if(foundEmployee.Rows.Count == 0) 
            {
                MessageBox.Show("Пользователь не был найден!");
                return;
            }

            if (foundEmployee.Rows[0]["Password"] as string != PasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            PostDataTable foundPost = PagesWindow.PostPage.Adapter.GetPostByID((int)foundEmployee.Rows[0]["PostID"]);

            switch (foundPost.Rows[0]["Name"])
            {
                case "Администратор":
                    PagesWindow.userRole = UserRole.ADMIN;
                    break;

                case "Продавец":
                    PagesWindow.userRole = UserRole.SELLER;
                    break;
            }

            PagesWindow.LoadPages();

            Close();
            PagesWindow.Show();
        }
    }
}
