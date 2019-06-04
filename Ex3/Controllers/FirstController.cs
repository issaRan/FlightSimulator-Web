using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ex3.Models;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;

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
            IPAddress temp;
            if (IPAddress.TryParse(ip, out temp) == false)
            {
                load(ip, port);
                return View("load");
            }
                 
            InfoServer server = InfoServer.Instance;
            if (server.booleanState())
            {
                server.Stop();
            }
            server.Start(ip, port);
            Position pos = GetPosition();
            Lat = pos.Lat;
            Lon = pos.Lon;
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

        [HttpGet]
        public ActionResult save(string ip, int port, int rate, int duration, string name)
        {
            CacheManager.Instance.Path = name;
            System.IO.File.WriteAllBytes(CacheManager.Instance.Path, new byte[0]);
            InfoServer server = InfoServer.Instance;
            server.Start(ip, port);
            Session["rate"] = rate;
            Session["duration"] = duration;
            return View();
        }

        [HttpGet]
        public void load(string name, int rate)
        {
            string[] files = System.IO.File.ReadAllLines(CacheManager.Instance.Path);
            Session["rate"] = rate;
            Array.Resize(ref files, files.Length + 1);
            files[files.Length - 1] = "End,End,End,End";
            Session["arr"] = files;
        }

        [HttpPost]
        public string readPosition()
        {
            string[] files = (string[])Session["arr"];
            string line = files[0];
            string[] words = line.Split(',');
            if ((words[0] != "End") && (words[1] != "End"))
            {
                Position pos = CacheManager.Instance.ToPosition(line);
                files = files.Skip(1).ToArray();
                Session["arr"] = files;
                return ToXml(pos);
            }
            int x = -1000;
            return ToXml(new Position(x, x, x, x));
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
            Position pos =  new Position(Double.Parse(lon), Double.Parse(lat),Double.Parse(throttle), Double.Parse(rudder));
            return pos;
        }
        public string SaveAndGetXmlPosition()
        {
            Position pos = GetPosition();
            CacheManager.Instance.SavePoint(pos);
            return ToXml(pos);
        }
        public string ToXml(Position pos)
        {
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
        public string GetXmlPosition()
        {
            Position pos = GetPosition();
            return ToXml(pos);
        }

    }
}