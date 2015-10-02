using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project__.Models;

namespace Project__.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new Models.Index();
            model.i = 5;
            return View(model);
        }

        public ActionResult About()
        {
            var model = new Models.About();
            ViewBag.Message = "Your application description page.";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Colin()
        {
            return View();
        }
    }
}