using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Top20Video.Framework;
using System.Web.Mvc.Ajax;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;

namespace Top20Video.Framework
{
    public class ApiAuthAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (!ValidateToken(request))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            base.OnActionExecuting(actionContext);
        }

        private bool ValidateToken(HttpRequestMessage request)
        {
            if (request.Headers.Count() > 1)
            {
                if (request.Headers.Contains("user") && request.Headers.Contains("token")) //&& request.Headers.Contains("date").isDate()
                {
                    string user = request.Headers.GetValues("user").FirstOrDefault();
                    string token = request.Headers.GetValues("token").FirstOrDefault();

                    return (user == "Top20Video" && token == "pwSe12ojKfaA2w54ipFeP3SWwe9sd0N8m");
                }
            }
            return false;
        }
    }
}
