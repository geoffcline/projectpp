﻿using System;
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
            int? UserId = (int?)Session["LoginId"];

            if (UserId == null)
            {
            model = db.Users.FirstOrDefault(u => u.UserID == 1);
            }else
            {
                model = db.Users.FirstOrDefault(u => u.UserID == UserId);
            }
            
            return View("Login", model);
        }
        public ActionResult Dashboard()
        {
            var model = new Projects();
            model = db.Project.FirstOrDefault(p => p.ProjectID == 1);

            return View("Index", model);
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

        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult Calendar()
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
        public JsonResult ValidateUser(string username, string password)
        {
            var User = new User();
            User = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if(User != null)
            {
                Session["LoginId"] = User.UserID;
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
               
            }else
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public void CreateAGroup()
        {
            try
            {
                string name = (string.IsNullOrEmpty(Request.Form["groupname"]) ? null : Request.Form["groupname"]);
                string desc = Request.Form["groupdescription"];

                var group = new Projects();
                group.Name = name;
                group.Description = desc;
                group.TeamLeaderID = (int)Session["LoginId"];

                db.Project.Add(group);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        
    }
}