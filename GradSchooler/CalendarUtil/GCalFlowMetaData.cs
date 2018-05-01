using System;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
namespace GradSchooler.CalendarUtil
{
    public class GCalFlowMetaData : FlowMetadata
    {

        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "759575157613-vm000p0ipk88nr6f8bf3d3mdfeho6c7r.apps.googleusercontent.com",
                    ClientSecret = "PUT_CLIENT_SECRET_HERE"
                },
                Scopes = new[] { CalendarService.Scope.Calendar },
                DataStore = new FileDataStore("Calendar.Api.Auth.Store")
            });

        public override string GetUserId(Controller controller)
        {
            // TODO: Use Auth Cookie? Identity?
            // In this sample we use the session to store the user identifiers.
            // That's not the best practice, because you should have a logic to identify
            // a user. You might want to use "OpenID Connect".
            // You can read more about the protocol in the following link:
            // https://developers.google.com/accounts/docs/OAuth2Login.
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }
            return user.ToString();

        }


        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }//class
}//namespace
