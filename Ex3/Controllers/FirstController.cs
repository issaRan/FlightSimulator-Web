using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ex3.Models;
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
        public ActionResult pathMap(string ip, int port, int time)
        {
            InfoServer server = InfoServer.Instance;
            server.Start(ip, port);
            Lat = Double.Parse(server.Get("position/latitude-deg"));
            Lon = Double.Parse(server.Get("position/longitude-deg"));
            @Session["time"] = time;
            return View();
        }
    }
}