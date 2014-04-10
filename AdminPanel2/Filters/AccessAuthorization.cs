using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity.Models;

namespace AdminPanel2.Filters
{
    [HandleError]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AccessAuthorization : ActionFilterAttribute
    {
        public delegate bool CheckSessionDelegate(HttpSessionStateBase session);

        public static CheckSessionDelegate CheckSessionAlive;

        public static bool Checkuser(User u)
        {
            try
            {
                HttpContext ctx = HttpContext.Current;

                if (ctx.Session["usuario"] == null)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var Controller = context.ActionDescriptor.ControllerDescriptor.ControllerName;

            var Action = context.ActionDescriptor.ActionName;

            if (Controller != "Login" && (Action != "Index" || Action != "DoLogin"))
            {
                if (context.HttpContext.Session["usuario"] == null)
                {
                    if (context.HttpContext.Request.IsAjaxRequest())
                    {
                        // For AJAX requests, we're overriding the returned JSON result with a simple string,
                        // indicating to the calling JavaScript code that a redirect should be performed.
                        context.Result = new JsonResult { };
                        return;
                    }
                    context.HttpContext.Session.Abandon();
                    context.HttpContext.Response.Redirect("~/Login?Url=" + context.HttpContext.Request.Url.LocalPath);
                }
            }
        }
    }
}