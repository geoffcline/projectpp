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
            var model = new UsersVM();
            int? UserId = (int?)Session["LoginId"];

            if (UserId == null)
            {
            model.Users = db.Users.FirstOrDefault(u => u.UserID == 1);
            }else
            {
                model.Users = db.Users.FirstOrDefault(u => u.UserID == UserId);
            }
            
            return View("Login", model);
        }
        public ActionResult Dashboard()
        {
            var model = new Projects();
            model = db.Projects.FirstOrDefault(p => p.ProjectID == 1);

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
            var model = new UsersVM();
            

            int userid = (int)Session["LoginId"];
            model.Users = db.Users.FirstOrDefault(u => u.UserID == userid);
            model.GroupMemberList = db.GroupMembers.ToList();
            model.GroupList = db.Projects.ToList();

            return View(model);
        }
        public ActionResult Calendar()
        {
            return View();
        }

        public void RegisterUser()
        {
            var firstname = (string.IsNullOrEmpty(Request.Form["firstname"]) ? null : Request.Form["firstname"]);
            var lastname = (string.IsNullOrEmpty(Request.Form["lastname"]) ? null : Request.Form["lastname"]);
            var email = (string.IsNullOrEmpty(Request.Form["email"]) ? null : Request.Form["email"]);
            var username = (string.IsNullOrEmpty(Request.Form["username"]) ? null : Request.Form["username"]);
            var password = (string.IsNullOrEmpty(Request.Form["password"]) ? null : Request.Form["password"]);
            var avatarid = Request.Form["avatarid"];

            var user = new User();
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Email = email;
            user.Username = username;
            user.Password = password;
            user.DefaultGroupID = null;
            user.Avatar_Num = avatarid;

            db.Users.Add(user);
            db.SaveChanges();

            return;
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

                var groupMembers = new GroupMember();
                groupMembers.GroupName = name;
                groupMembers.UserID = (int)Session["LoginId"];

                db.Projects.Add(group);
                db.GroupMembers.Add(groupMembers);
                db.SaveChanges();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public void SignOut()
        {
            try
            {
                Session["LoginId"] = null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return;
        }
        public void UpdateSettings()
        {
            var userid = (int)Session["LoginId"];
            var firstname = (string.IsNullOrEmpty(Request.Form["firstname"]) ? null : Request.Form["firstname"]);
            var lastname = (string.IsNullOrEmpty(Request.Form["lastname"]) ? null : Request.Form["lastname"]);
            var email = (string.IsNullOrEmpty(Request.Form["email"]) ? null : Request.Form["email"]);
            var username = (string.IsNullOrEmpty(Request.Form["username"]) ? null : Request.Form["username"]);
            var oldpassword = (string.IsNullOrEmpty(Request.Form["oldpassword"]) ? null : Request.Form["oldpassword"]);
            var newpassword = (string.IsNullOrEmpty(Request.Form["newpassword"]) ? null : Request.Form["newpassword"]);
            var confirmpassword = (string.IsNullOrEmpty(Request.Form["confirmpassword"]) ? null : Request.Form["confirmpassword"]);
            var defaultgroup = int.Parse(Request.Form["defaultgroup"]);


            var user = db.Users.FirstOrDefault(u => u.UserID == userid);
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Email = email;
            user.Username = username;
            user.DefaultGroupID = defaultgroup;

            if(oldpassword != null && newpassword != null && confirmpassword != null)
            {
                if(user.Password == oldpassword && newpassword == confirmpassword)
                {
                    user.Password = newpassword;
                }
            }

            db.SaveChanges();
        }
    }
}