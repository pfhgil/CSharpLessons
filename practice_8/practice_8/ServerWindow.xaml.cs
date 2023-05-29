using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SerializationLib;
using TCPLib;

namespace practice_8
{
    /// <summary>
    /// Логика взаимодействия для ServerWindow.xaml
    /// </summary>
    public partial class ServerWindow : Window
    {
        public ServerWindow()
        {
            InitializeComponent();
            TCPServer.Start(8880);

            TCPServer.OnNewMessage += (msg) =>
            {
                // лень делать по другому как то
                MessagesListBox.Items.Clear();

                for (int i = 0; i < TCPServer.Messages.Count; i++)
                {
                    MessagesListBox.Items.Add(TCPServer.Messages[i]);
                }
            };
            TCPServer.OnNewClient += (clientSocket) => 
            {
                DateTime dateTime = DateTime.Now;
                MessagesListBox.Items.Add(dateTime.ToString() + " | New client: " + (clientSocket.LocalEndPoint as IPEndPoint).Address);
            };
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            string resultText = dateTime.ToString() + " | Anonymous: " + MessageTextBox.Text;
            MessagesListBox.Items.Add(resultText);
            MessageTextBox.Text = "";
            TCPServer.SendMessageToAll(resultText);
        }

        private void SaveLogButton_Click(object sender, RoutedEventArgs e)
        {
            Serialization.SaveObjectToFile<List<string>>(TCPServer.Log, "ServerLog.txt");
        }
    }
}
