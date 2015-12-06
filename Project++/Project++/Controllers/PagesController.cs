using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Project__.Models;
using Google.Apis.Auth.OAuth2.Web;
using Drive.api.auth;
using Google.Apis.Drive.v2.Data;

namespace Project__.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult Login()
        {
            var model = new UsersVM();
            string firstname = "Hey";

            model.FirstName = firstname;

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
                foreach(File thing in files.Items)
                {
                    list.Q = "'" + thing.Id + "' in parents";
                    newFiles = list.Execute();   
                }

                ViewBag.Message = "FILE COUNT IS: " + files.Items.Count();
                return View(newFiles);
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
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
}