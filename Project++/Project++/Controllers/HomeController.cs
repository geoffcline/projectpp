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
        private PlusPlusContext db = new PlusPlusContext();

        public ActionResult Index()
        {

            var model = new User();
            var assignment = new Assignment();
            
            int? UserId = (int)Session["LoginId"];

            if (UserId == null)
            {
                model = db.Users.FirstOrDefault(u => u.UserID == 1);
            }
            else
            {
                model = db.Users.FirstOrDefault(u => u.UserID == UserId);
            }

            return View("Index", model);
        }
    }

    
}