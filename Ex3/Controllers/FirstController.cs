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
        public ActionResult display(string ip, int port)
        {
            InfoServer server = InfoServer.Instance;
            server.Start(ip, port);
            return View();
        }
    }
}