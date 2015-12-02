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

            var model = new UsersVM();
            model.Group = db.Project.FirstOrDefault(p => p.ProjectID == 2);

            return View("Index", model);
        }
    }

    
}