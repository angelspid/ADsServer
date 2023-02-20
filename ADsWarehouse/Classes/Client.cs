using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ADsWarehouse.Classes
{
    public class Client
    {        
        BytesConvert bytesConvert;
        SimpleTcpClient _Client;
        System.Windows.Threading.DispatcherTimer tCollectBytes;
        /// <summary>
        /// Collecting of bytes array for one session staus.
        /// </summary>
        private bool Done { get; set; }
        /// <summary>
        /// Bytes collection of bytes in one session.
        /// </summary>
        private List<byte> data { get; set; }
        public Client()
        {
            data = new List<byte>();
            bytesConvert = new();
            InitClient();
            tCollectBytes = new();
            tCollectBytes.Interval = new TimeSpan(0, 0, 0, 5);
            tCollectBytes.Tick +=tCollectBytes_Tick;
        }
        /// <summary>
        /// Client Initialization
        /// </summary>
        private void InitClient()
        {
            _Client = new SimpleTcpClient("127.0.0.1", 9000, true, "simpletcp.pfx", "simpletcp");
            _Client.Events.DataReceived += DataReceived;
            _Client.Keepalive.EnableTcpKeepAlives = true;
            _Client.Settings.MutuallyAuthenticate = false;
            _Client.Settings.AcceptInvalidCertificates = true;
            _Client.Settings.ConnectTimeoutMs = 5000;
            _Client.Settings.StreamBufferSize = 65536;
            _Client.Settings.NoDelay= false;
            _Client.Settings.UseAsyncDataReceivedEvents= false;
            _Client.ConnectWithRetries(5000);
        }
        /// <summary>
        /// Timer for end of collecting byte for session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tCollectBytes_Tick(object? sender, EventArgs e)
        {
            Done = true;
            tCollectBytes.Stop();
            Send(bytesConvert.ToByteArray(new object[] { "dn", "" }));
        }
        /// <summary>
        /// Data received event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
           data.AddRange(e.Data.Array.ToList());
           tCollectBytes.Stop();
           tCollectBytes.Start();
           Send(bytesConvert.ToByteArray(new object[] { "rcv", "" }));
        }
        /// <summary>
        /// Send to server.
        /// </summary>
        /// <param name="bytes">Bytes array</param>
        public void Send(byte[] bytes)
        {
            if (bytes != null && bytes.Length != 0) _Client.Send(bytes);
        }
        /// <summary>
        /// Async method for waiting of collecting data for one session.
        /// </summary>
        /// <param name="bytes">Bytes array</param>
        /// <returns>Bytes list</returns>
        public async Task<List<byte>> SendReturn(byte[] bytes)
        {

           Send(bytes);
           while (!Done) await Task.Delay(50);
           Done = false;
           return data;
        }
        /// <summary>
        ///  Task to return object after request from server.
        /// </summary>
        /// <typeparam name="T">Type of expected object</typeparam>
        /// <param name="obj">Send query to server</param>
        /// <returns>Return object</returns>
        public Task<T> ReturnObject<T>(object obj)
        {
            return Task.Run(() =>
            {
                Task<List<byte>> task = SendReturn(bytesConvert.ToByteArray(obj));
                task.Wait();
                var list = task.Result;
                task.Dispose();
                var retunrObj = bytesConvert.FromByteArray<T>(bytesConvert.Decompress(list.ToArray()));
                data.Clear();
                return retunrObj;
            });
     
        }
    }
}
