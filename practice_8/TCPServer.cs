using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pratice_6_messenger
{
    static class TCPServer
    {
        private static Socket _socket;
        private static List<Socket> _clients = new List<Socket>();

        private static List<string> _log = new List<string>();
        public static List<string> Log
        {
            get { return _log; }
        }

        private static List<string> _messages = new List<string>();
        public static List<string> Messages
        {
            get { return _messages; }
        }

        #region Params
        public static string Name = "Администратор (сервер)";
        #endregion

        public delegate void NewMessageCallback(string message);
        public delegate void NewClientCallback(Socket client);
        public delegate void ClientDisconnectCallback(Socket client);

        public static event NewMessageCallback OnNewMessage;
        public static event NewClientCallback OnNewClient;
        public static event ClientDisconnectCallback OnClientDiconnect;

        public static void Start(in int port)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(ipEndPoint);
            _socket.Listen(10);

            ListenToClients();
        }

        private static void Stop() { }

        private static async Task ListenToClients()
        {
            while(true)
            {
                var newClient = await _socket.AcceptAsync();
                _clients.Add(newClient);

                OnNewClient(newClient);

                ReceiveMessages(newClient);
                 
                DateTime date = DateTime.Now;
                _log.Add(date + " | " + newClient.RemoteEndPoint + " присоединился к чату");
            }
        }

        private static async Task ReceiveMessages(Socket clientReceived)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];

                await clientReceived?.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

                string message = Encoding.UTF8.GetString(buffer);

                if (message == "/disconnect")
                {
                    continue;
                }

                DateTime date = DateTime.Now;
                _log.Add(date + " | " + clientReceived.RemoteEndPoint + " отправил сообщение: " + message);

                SendMessageToAll(message);
            }
        }

        private static async Task SendMessage(Socket clientToSend, string message)
        {
            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            await clientToSend?.SendAsync(new ArraySegment<byte>(messageBuffer), SocketFlags.None);                
        }

        public static async Task SendMessageToAll(string message)
        {
            int clientsNum = _clients.Count;

            for (int k = 0; k < clientsNum; k++)
            {
                var clientToSend = _clients[k];

                SendMessage(clientToSend, message);
            }

            _messages.Add(message);

            OnNewMessage(message);
        }
    }
}
