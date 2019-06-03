using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ex3.Models;
using System.Xml;
using System.Text;

namespace Ex3.Controllers
{
    public class FirstController : Controller
    {
        private double Lon;
        private double Lat;
        // GET: First
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ViewMap()
        {
            return View();
        }
        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            InfoServer server = InfoServer.Instance;
            if (server.booleanState())
            {
                server.Stop();
            }
            server.Start(ip, port);
            Lat = Double.Parse(server.Get("position/latitude-deg"));
            Lon = Double.Parse(server.Get("position/longitude-deg"));
            return View();
        }
        [HttpGet]
        public ActionResult pathOnMap(string ip, int port, int rate)
        {
            InfoServer server = InfoServer.Instance;
            if (server.booleanState())
            {
                server.Stop();
            }
            server.Start(ip, port);
            Session["rate"] = rate;
            return View();
        }
        public KeyValuePair<string,string> GetPosition()
        {            
            InfoServer server = InfoServer.Instance;
            string lat = server.Get("position/latitude-deg");
            string lon = server.Get("position/longitude-deg");
            return new KeyValuePair<string, string>(lat, lon);
        }
        public string ToXml()
        {
            KeyValuePair<string, string> salim = GetPosition();
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Position");
            writer.WriteElementString("lat", salim.Key);
            writer.WriteElementString("lon", salim.Value);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }


    }
}