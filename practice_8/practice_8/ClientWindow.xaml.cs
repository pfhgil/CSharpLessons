using SerializationLib;
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
using System.Windows.Shapes;
using TCPLib;

namespace practice_8
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private TCPClient _tcpClient = new TCPClient();

        public ClientWindow()
        {
            InitializeComponent();

            _tcpClient.Connect("127.0.0.1", 8880);

            _tcpClient.OnNewMessage += (msg) =>
            {
                // лень делать по другому как то
                MessagesListBox.Items.Clear();

                for (int i = 0; i < _tcpClient.Messages.Count; i++)
                {
                    MessagesListBox.Items.Add(_tcpClient.Messages[i]);
                }
            };
        }
        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            string resultText = dateTime.ToString() + " | Anonymous: " + MessageTextBox.Text;
            MessagesListBox.Items.Add(resultText);
            MessageTextBox.Text = "";
            _tcpClient.SendMessage(resultText);
        }

        private void SaveMessagesButton_Click(object sender, RoutedEventArgs e)
        {
            Serialization.SaveObjectToFile<List<string>>(_tcpClient.Messages, "ClientLog.txt");
        }
    }
}
