using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project__.Models;

namespace Project__.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ManageGroup()
        {
            return View();
        }
        public ActionResult CreateGroup()
        {
            return View();
        }
        public ActionResult Compiler()
        {
            var model = new Models.Strings();

            

            return View(model);
        }
        public int TranslateSql(int test)
        {
            int variable = test;
            return variable;
        }
    }
}