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
        private PlusPlusContext db = new PlusPlusContext();

        public ActionResult Login()
        {
            var model = new User();
            model = db.Users.FirstOrDefault(u => u.UserID == 1);


            return View("Login", model);
        }
        public ActionResult ManageGroup()
        {
            return View();
        }
        public ActionResult CreateGroup()
        {
            return View();
        }

        public void RegisterUser()
        {
            var name = (string.IsNullOrEmpty(Request.Form["name"]) ? null : Request.Form["name"]);
            var email = (string.IsNullOrEmpty(Request.Form["email"]) ? null : Request.Form["email"]);
            var username = (string.IsNullOrEmpty(Request.Form["username"]) ? null : Request.Form["username"]);
            var password = (string.IsNullOrEmpty(Request.Form["password"]) ? null : Request.Form["password"]);

            var user = new User();
            user.FirstName = name;
            user.Email = email;
            user.Username = username;
            user.Password = password;
            user.LastName = "testing";
            user.DefaultGroupID = 3;

            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}