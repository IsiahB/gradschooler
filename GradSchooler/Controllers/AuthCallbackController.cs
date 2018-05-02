using System.Web.Mvc;
using GradSchooler.CalendarUtil;

namespace GradSchooler.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override Google.Apis.Auth.OAuth2.Mvc.FlowMetadata FlowData
        {
            get { return new GCalFlowMetaData(); }
        }
    }
}
