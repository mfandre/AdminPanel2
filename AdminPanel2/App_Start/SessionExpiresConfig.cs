using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdminPanel2.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SessionExpiresConfig : ActionFilterAttribute
    {
        public delegate bool CheckSessionDelegate(HttpSessionStateBase session);

        public static CheckSessionDelegate CheckSessionAlive;


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // If the browser session or authentication session has expired...
            if (ctx.Session["usuario"] == null || !filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // For AJAX requests, we're overriding the returned JSON result with a simple string,
                    // indicating to the calling JavaScript code that a redirect should be performed.
                    filterContext.Result = new JsonResult { Data = "_Logon_" };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login");
                }
            }
        }
    }
}