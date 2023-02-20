using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ADsServer.Controllers;

namespace ADsServer.Classes
{
    internal class User : BaseViewModel
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }


        public User(string userName)
        {
            Username = userName;
        }

    }
}
