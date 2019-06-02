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
        [HttpGet]
        public ActionResult pathOnMap(string ip, int port, int rate)
        {
            InfoServer server = InfoServer.Instance;
            server.Start(ip, port);
            Session["rate"] = rate;
            return View();
        }

        public ActionResult save(string ip, int port, int rate, int duration, string name)
        {
            InfoServer server = InfoServer.Instance;
            server.Start(ip, port);
            Session["rate"] = rate;
            Session["duration"] = duration;
            return View();
        }

        public ActionResult load(string name, int rate)
        {
            Session["rate"] = rate;
            return View();
        }

        public List<Position> GetPositionsList()
        {
            return CacheManager.Instance.ReadPositions();
        }
        public Position GetPosition()
        {            
            InfoServer server = InfoServer.Instance;
            string lat = server.Get("position/latitude-deg");
            string lon = server.Get("position/longitude-deg");
            string throttle = server.Get("controls/engines/current-engine/throttle");
            string rudder = server.Get("controls/flight/rudder");
            Position pos =  new Position(Double.Parse(lon), Double.Parse(lat),
                Double.Parse(throttle), Double.Parse(rudder));
            // Saving the position.
            CacheManager.Instance.SavePoint(pos);
            return pos;
        }
        public string ToXml()
        {
            Position pos = GetPosition();
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Positions");
            pos.ToXml(writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }


    }
}