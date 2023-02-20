using Accessibility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ADsServer.Classes
{
    internal class Client : BaseViewModel
    {
        private bool _isConnected;
        private string _status;
        /// <summary>
        /// Client's collection of Users
        /// </summary>
        public ObservableCollection<User> Users { get; set; }
        /// <summary>
        /// Client's Id by IpAddres:Port
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Client's IP Address
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Client's Port
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// Is Client connected
        /// </summary>
        public bool IsConnected
        {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }
        /// <summary>
        /// Status & Process of relationship between client and server
        /// </summary>
        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }
        public Client(string ipPort)
        {
            Users= new();
            IpAddress = ipPort.Split(':')[0];
            Port = ipPort.Split(':')[1];
            IsConnected = true;
            Id = ipPort;
            Status = "";
        }
        /// <summary>
        /// Set status to View
        /// </summary>
        public void Send()
        {
            Status = "O";
        }
        /// <summary>
        /// Set status to View
        /// </summary>
        public void Receive()
        {
            Status = "I";
        }
        /// <summary>
        /// Set status to View
        /// </summary>
        public void Done()
        {
            Status = "";
        }
        /// <summary>
        /// Add User to client's list
        /// </summary>
        /// <param name="userName">Username of user</param>
        public void AddUser(string userName)
        {
            Users.Add(new User(userName));
        }
        /// <summary>
        /// Update client's status
        /// </summary>
        /// <param name="ipPort">Id of clients</param>
        public void Update(string ipPort)
        {
            Id = ipPort;
            IsConnected = true;
            Port = ipPort.Split(":")[1];
        }
    }
}
