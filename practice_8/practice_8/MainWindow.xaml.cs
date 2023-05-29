using System;
using System.Collections.Generic;
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

namespace practice_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ServerWindow _serverWindow;
        private ClientWindow _clientWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ServerButton_Click(object sender, RoutedEventArgs e)
        {
            _serverWindow = new ServerWindow();

            this.Close();
            _serverWindow.Show();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            _clientWindow = new ClientWindow();

            this.Close();
            _clientWindow.Show();
        }
    }
}
