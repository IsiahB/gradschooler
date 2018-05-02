using System;
using GradSchooler.Models;
using System.Security;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using System.Diagnostics;

namespace GradSchooler.CalendarUtil
{
    public class GCalFlowMetaData : FlowMetadata
    {

        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "26796331557-3fefv8fe0jjkjfh2n83cl8ro8ms7n2qm.apps.googleusercontent.com",
                    ClientSecret = "Y4RZr3gkfkLGRjzsX9lPHwQR"
                },
                Scopes = new[] { CalendarService.Scope.Calendar },
                DataStore = new FileDataStore("Calendar.Api.Auth.Store")
            });

        public override string GetUserId(Controller controller)
        {
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            if (user == null)
            {
                Debug.WriteLine("User is null");

            }
            return user;

        }


        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }//class
}//namespace
