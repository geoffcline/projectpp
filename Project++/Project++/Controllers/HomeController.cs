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
            int? userId = (int?)Session["LoginId"];
            model.GroupId = (int?)Session["GroupId"];
            model.UserId = userId;
            model.GroupMemberList = db.GroupMembers.ToList();
            model.Users = db.Users.FirstOrDefault(u => u.UserID == userId);
            model.GroupList = db.Projects.ToList();
            model.TaskList = db.Tasks.OrderBy(tl => tl.DueDate).ToList();


            return View("Index", model);
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
            catch(Exception ex)
            {
                throw ex;
            }
            
            
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
    }

    
}