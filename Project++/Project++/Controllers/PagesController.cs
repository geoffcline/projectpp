using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project__.Models;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

using Google.Apis.Sample.MVC.Models;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Download;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Sample.MVC.Controllers;

namespace Project__.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult Login()
        {
            var model = new UsersVM();
            string firstname = "Hey";

            model.FirstName = firstname;

            return View("Login",model);
        }
        public ActionResult ManageGroup()
        {
            return View();
        }
        public ActionResult CreateGroup()
        {
            return View();
        }
        public ActionResult Task()
        {
            return View();
        }
    }
}