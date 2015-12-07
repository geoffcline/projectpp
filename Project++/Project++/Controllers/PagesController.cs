﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Project__.Models;
using Google.Apis.Auth.OAuth2.Web;
using Drive.api.auth;
using Google.Apis.Drive.v2.Data;
using System;

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
            }
            else
            {
                model.Users = db.Users.FirstOrDefault(u => u.UserID == UserId);
            }
            
            return View("Login", model);
        }
        public ActionResult GroupDashboard()
        {
            var model = new UsersVM();
            model.GroupId = (int?)Session["GroupId"];
            model.UserId = (int?)Session["LoginId"];
            model.Users = db.Users.FirstOrDefault(u => u.UserID == model.UserId);
            model.Group = db.Projects.FirstOrDefault(g => g.ProjectID == 22);
            
                return View(model);
        }
        public ActionResult ManageGroup()
        {
            var model = new UsersVM();
            model.Group = db.Projects.FirstOrDefault(p => p.ProjectID == 22);
            model.GroupMemberList = db.GroupMembers.Where(g => g.GroupID >= 9).ToList();
            return View(model);
        }
        public ActionResult CreateGroup()
        {
            
            return View();
        }
        public ActionResult Task()
        {
            var model = new UsersVM();
            int userid = (int)Session["LoginId"];
            model.UserId = userid;
            model.Users = db.Users.FirstOrDefault(u => u.UserID == userid);
            model.GroupMemberList = db.GroupMembers.ToList();
            model.GroupList = db.Projects.ToList();
            model.TaskList = db.Tasks.ToList();

            return View(model);
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
            var model = new UsersVM();
            model.EventList = db.Events.ToList();
             List<string> events = new List<string>();

            foreach(var e in model.EventList)
            {
                string eventstring = "title : '" + e.EventName + "',start : '" + e.StartTime + "'";
                events.Add(eventstring);
            }
            ViewData["events"] = events;

            return View(model);
        }
        public ActionResult DriveFiles(CancellationToken cancellationToken)
        {
            var result = new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).AuthorizeAsync(cancellationToken).Result;

            if (result.Credential != null)
            {
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "Project++"
                });

                var list = service.Files.List();
                list.Q = "title = 'Software Engineering Project' and mimeType = 'application/vnd.google-apps.folder'";
                var files = list.Execute();
                FileList newFiles = new FileList();
                foreach (File thing in files.Items)
                {
                    list.Q = "'" + thing.Id + "' in parents";
                    newFiles = list.Execute();
                }

                return View(newFiles);
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }

        public void RegisterUser()
        {
            var firstname = (string.IsNullOrEmpty(Request.Form["firstname"]) ? null : Request.Form["firstname"]);
            var lastname = (string.IsNullOrEmpty(Request.Form["lastname"]) ? null : Request.Form["lastname"]);
            var email = (string.IsNullOrEmpty(Request.Form["email"]) ? null : Request.Form["email"]);
            var username = (string.IsNullOrEmpty(Request.Form["username"]) ? null : Request.Form["username"]);
            var password = (string.IsNullOrEmpty(Request.Form["password"]) ? null : Request.Form["password"]);
            var avatarid = Request.Form["avatarid"];

            var user = new Models.User();
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
            var User = new Models.User();
            User = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (User != null)
            {
                Session["LoginId"] = User.UserID;
                if (User.DefaultGroupID != null)
                {
                    var model = new UsersVM();
                    model.GroupId = User.DefaultGroupID;
                }
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
               
            }
            else
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
            catch (Exception ex)
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
            catch (Exception ex)
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

            if (oldpassword != null && newpassword != null && confirmpassword != null)
            {
                if (user.Password == oldpassword && newpassword == confirmpassword)
                {
                    user.Password = newpassword;
                }
            }

            db.SaveChanges();
        }
        public void CreateNewTask()
        {
            
                int userid = (int)Session["LoginId"];
                var taskname = (string.IsNullOrEmpty(Request.Form["taskname"]) ? null : Request.Form["taskname"]);
                int groupid = int.Parse(Request.Form["groupid"]);
                var description = (string.IsNullOrEmpty(Request.Form["description"]) ? null : Request.Form["description"]);
                var duedate = DateTime.Parse(Request.Form["duedate"]);



                var tasks = new Tasks();
                tasks.GroupID = groupid;
                tasks.TaskName = taskname;
                tasks.AssignedUserID = userid;
                tasks.CreatedDate = DateTime.Now;
                tasks.DueDate = duedate;
                tasks.Description = description;

                db.Tasks.Add(tasks);
                db.SaveChanges();
            

        }
        public void NewMessage()
        {
            try
            {
                string newmessage = Request.Form["newmessage"];

                var chat = new Chat();

                chat.GroupID = 34;
                chat.TimeStamp = DateTime.Now;
                chat.UserId = (int)Session["LoginId"];
                chat.Message = newmessage;

                db.Chats.Add(chat);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GroupDash()
        {
            int? groupid = int.Parse(Request.Form["groupid"]);

            var model = new UsersVM();
            model.GroupId = groupid;
            model.UserId = (int?)Session["LoginId"];
            model.Users = db.Users.FirstOrDefault(u => u.UserID == model.UserId);

            if (groupid == null)
            {
                if (model.Users.DefaultGroupID != null)
                {
                    model.Group = db.Projects.FirstOrDefault(p => p.ProjectID == model.Users.DefaultGroupID);
                    Session["GroupId"] = model.Group.ProjectID;
                }
                else
                {
                    Session["GroupId"] = null;
                }
            }
            else
            {
                model.Group = db.Projects.FirstOrDefault(p => p.ProjectID == groupid);
                Session["GroupId"] = model.Group.ProjectID;
            }
        }
        public void GroupName()
        {
            string groupname = Request.Form["groupname"];
            var group = new Projects();
            group = db.Projects.FirstOrDefault(p => p.ProjectID == 22);
            group.Name = groupname;
            db.SaveChanges();
        }
        public void ImageUrl()
        {
            string imageurl = Request.Form["imageurl"];
            var group = new Projects();
            group = db.Projects.FirstOrDefault(p => p.ProjectID == 22);
            group.ImageUrl = imageurl;
            db.SaveChanges();
        }
        public void NewUser()
        {
            var groupmember = new GroupMember();
            groupmember.UserID = 1;
            groupmember.UserTitle = "djt3md@mst.edu";
            groupmember.GroupName = "Project++";
            groupmember.UserName = "David Tutt";
            groupmember.RegisterDate = DateTime.Now;
            db.GroupMembers.Add(groupmember);
            db.SaveChanges();
        }
        public void RemoveUser()
        {
            string username = Request.Form["removeuser"];
            var groupmember = new GroupMember();
            groupmember = db.GroupMembers.FirstOrDefault(gm => gm.UserName == username);

            
        }
    }
}



public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
{
    protected override FlowMetadata FlowData
    {
        get { return new AppFlowMetadata(); }
    }
}