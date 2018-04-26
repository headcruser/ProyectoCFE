using hola_mundo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hola_mundo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Usuario user = new Usuario();
            user.MostrarDatos();
            return View("Saludar");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Saludar()
        {
            return View();
        }
    }
}