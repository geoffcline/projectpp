
using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Util.Store;

namespace Google.Apis.Sample.MVC.Controllers
{
    public class AppAuthFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "667062594689-ssc2sj9qna2683u9qckscu81m7lnhn56.apps.googleusercontent.com",
                    ClientSecret = "4YQzn0Bsepy_M5G1vgxW_Chx"
                },
                Scopes = new[] { DriveService.Scope.Drive },
                DataStore = new FileDataStore("Google.Apis.Sample.MVC")
            });

        public override string GetUserId(Controller controller)
        {
            return controller.User.Identity.GetUserName();
        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }

}
