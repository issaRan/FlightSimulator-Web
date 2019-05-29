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
            server.Start(ip, port);
            Lat = Double.Parse(server.Get("position/latitude-deg"));
            Lon = Double.Parse(server.Get("position/longitude-deg"));
            return View();
        }
        public ActionResult pathOnMap(string ip, int port, int time)
        {
            InfoServer server = InfoServer.Instance;
            server.Start(ip, port);
            Lat = Double.Parse(server.Get("position/latitude-deg"));
            Lon = Double.Parse(server.Get("position/longitude-deg"));
            @Session["time"] = time;
            return View();
        }
        public KeyValuePair<string,string> getPosition()
        {            
            InfoServer server = InfoServer.Instance;
            string Lat = server.Get("position/latitude-deg");
            string Lon = server.Get("position/longitude-deg");
            var pair = new KeyValuePair<string, string>(Lat, Lon);
            return pair;
        }
        private string ToXml()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Position");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }


    }
}