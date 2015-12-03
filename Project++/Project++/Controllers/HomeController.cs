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
            model.Group = db.Projects.FirstOrDefault(p => p.ProjectID == 2);
            model.GroupMemberList = db.GroupMembers.ToList();
            model.Users = db.Users.FirstOrDefault(u => u.UserID == userId);
            model.Chat = db.Chats.ToList();

            return View("Index", model);
        }
        public ActionResult MessageBox()
        {
            var model = new UsersVM();
            //model.Chat = db.Chat.ToList();

            return PartialView("MessageBox", model);
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
    }

    
}