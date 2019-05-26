using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web;

namespace Ex3.Models
{
    class InfoServer
    {
        private TcpListener listener;
        TcpClient client;
        private string ip;
        int port;
        public InfoServer() {}
        #region
        private static InfoServer m_Instance = null;
        public static InfoServer Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new InfoServer();
                }
                return m_Instance;
            }
        }
        #endregion
        public void Start(string ip, int port)
        {
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            client = listener.AcceptTcpClient();
            Console.WriteLine("added");
        }
        public string[] readFromSimulator()
        {
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            string message = "";
            string[] splitted;
            char c;
            while ((c = reader.ReadChar()) != '\n')
            {
                message += c;
            }
            splitted = message.Split(',');
            message = "";
            return splitted;
        }
        public void Stop()
        {
            client.Close();
            listener.Stop();
        }
    }
}