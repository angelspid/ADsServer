using ADsServer.Classes;
using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZstdNet;

namespace ADsServer.Controllers
{
    internal class ServerController : BaseViewModel
    {
        private BytesConvert bytesConvert;
        private Classes.MySql MySql;
        private SimpleTcpServer _Server;
        private string _isRunning;
        private string _isConnected;
        private string _ipAddress;
        private int _port;
        /// <summary>
        /// Server's list with Clients 
        /// </summary>
        public ObservableCollection<Client> Clients { get; set; }
        /// <summary>
        /// Status of Server
        /// </summary>
        public string IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }
        /// <summary>
        /// Status of MySQL
        /// </summary>
        public string IsConnected
        {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }
        /// <summary>
        /// Server's IP Adrress
        /// </summary>
        public string IpAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }
        /// <summary>
        /// Server's Port
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { SetProperty(ref _port, value); }
        }
        public ServerController()
        {
            IsRunning = "Stoped";
            IsConnected = "Connected";
            MySql = new();
            bytesConvert = new();
            Clients = new();
            InitServer();
        }
        /// <summary>
        ///Server Initialization
        /// </summary>
        private void InitServer()
        {
            IpAddress = "127.0.0.1";
            Port = 9000;
            _Server = new SimpleTcpServer(IpAddress, Port, true, "simpletcp.pfx", "simpletcp");
            _Server.Events.ClientConnected += ClientConnected;
            _Server.Events.ClientDisconnected += ClientDisconnected;
            _Server.Events.DataReceived += DataReceived;
            _Server.Events.DataSent += DataSent;
            _Server.Keepalive.EnableTcpKeepAlives = true;
            _Server.Settings.IdleClientTimeoutMs = 0;
            _Server.Settings.MutuallyAuthenticate = false;
            _Server.Settings.AcceptInvalidCertificates = true;
            _Server.Settings.StreamBufferSize = 65536;
            _Server.Start();
            IsRunning = "Running";
        }
        /// <summary>
        /// Client conected to server. Update client or add new one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientConnected(object sender, ConnectionEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Client? client = GetClientById(e.IpPort);
                if (client == null)
                    Clients.Add(new Client(e.IpPort));
                else
                    client.Update(e.IpPort);
            });

        }

        private void ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            Client? client = GetClientById(e.IpPort);
            if (client != null)
                client.IsConnected = false;
        }
        /// <summary>
        /// Data Received from client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Client? client = GetClientById(e.IpPort);
            if (client != null)
            {
                client.Send();
                object[] obj = bytesConvert.FromByteArray<object[]>(e.Data.Array);
                if (obj != null)
                    if (obj[0].GetType() == typeof(string) && obj.Length > 0)
                        CheckQuery(client, obj);
            }

        }
        /// <summary>
        /// Data Send Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataSent(object sender, DataSentEventArgs e)
        {
            //Client client = Clients.Where<Client>(x => x.id == e.IpPort).FirstOrDefault();    
        }
        /// <summary>
        /// Process query sended by client.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="query"></param>
        private void CheckQuery(Client client, object[] query)
        {
            switch (query[0])
            {
                case "get":
                    IsConnected = "Downloading information...";
                    DataTable table = MySql.GetTable("Select * From spare_parts");
                    Send(client.Id, ParseToBytes(table));
                    IsConnected = "Connected";
                    break;
                case "post":
                    break;
                case "update":
                    break;
                case "chat":
                    break;
                case "usr":
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        client.Users.Add(new User("angelspid"));
                    });
                    Send(client.Id, ParseToBytes("OK"));
                    break;
                case "stop":
                    break;
                case "rcv":
                    client.Receive();
                    break;
                case "dn":
                    client.Done();
                    break;
            }
        }
        /// <summary>
        /// Get client by Id
        /// </summary>
        /// <param name="id">IP Address:Port</param>
        /// <returns></returns>
        private Client? GetClientById(string id)
        {
            return Clients.Where(x => x.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// Convert & Compress bytes array
        /// </summary>
        /// <param name="obj">Object to convert</param>
        /// <returns></returns>
        private byte[] ParseToBytes(object obj)
        {
            return bytesConvert.Compress(bytesConvert.ToByteArray(obj));
        }
        /// <summary>
        /// Send bytes to client by IpAddress:Port
        /// </summary>
        /// <param name="ipPort">IpAddress:Port</param>
        /// <param name="bytes">Array bytes to send</param>
        private void Send(string ipPort, byte[] bytes)
        {

            if (bytes != null && bytes.Length != 0) _Server.Send(ipPort, bytes);
        }
        /// <summary>
        /// Send async
        /// </summary>
        /// <param name="ipPort">IpAddress:Port</param>
        /// <param name="bytes">Array bytes to send</param>
        private void SendAsync(string ipAddress, byte[] bytes)
        {
            if (bytes != null && bytes.Length != 0) _Server.SendAsync(ipAddress, bytes).Wait();
        }
        /// <summary>
        /// Remove client
        /// </summary>
        private void RemoveClient()
        {
            //string ipPort = _LastClientIpPort;
            //if (!String.IsNullOrEmpty(ipPort))
            //{
            //    _Server.DisconnectClient(ipPort);
            //}
        }
    }
}

