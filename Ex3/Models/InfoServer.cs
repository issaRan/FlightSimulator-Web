using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web;
using System.Text;

namespace Ex3.Models
{
    class InfoServer
    {
        private TcpListener listener;
        TcpClient client;
        private string ip;
        int port;
        private int Lon;
        private int Lat;
        private Boolean connected;
        Stream stream;
        StreamReader reader;
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
            client = new TcpClient();
            while (!client.Connected)
            {
                try
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                    client.Connect(ep);
                }
                catch (Exception) { };
            }
            stream = client.GetStream();
            reader = new StreamReader(stream);
            this.connected = true;
        }   
        public string Get(string path)
        {
            string message = "get" + " " + path + "\r\n";
            byte[] toSend = ASCIIEncoding.ASCII.GetBytes(message);
            stream.Write(toSend, 0, toSend.Length);
            string param = reader.ReadLine().Split('\'')[1];
            return param;
        }
        public void Stop()
        {
            client.Close();
            listener.Stop();
        }
        public Boolean booleanState()
        {
            return this.connected;
        }
    }
}
/*
<html>
<head>
    <link href="../../Image/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <canvas id="canvas"> </canvas>
    <script>
        var can = document.getElementById("canvas");
        var context = can.getContext("2d");
        context.beginPath();
        context.lineWidth = 1;
        var lon = (@ViewBag.Lon + 180 * (window.innerWidth / 360));
        var lat = (@ViewBag.Lat + 90 * (window.innerWidth / 180));
        context.arc(@ViewBag.Lon + 180, @ViewBag.Lat, 6, 0, 2 * Math.PI);
        context.fillStyle = 'red';
        context.fill();
        context.stroke();
    </script>
</body>
</html>
*/
