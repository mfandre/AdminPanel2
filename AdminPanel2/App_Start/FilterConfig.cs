using System.Web;
using System.Web.Mvc;
using AdminPanel2.App_Start;

namespace AdminPanel2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new SessionExpiresConfig());
            //filters.Add(new System.Web.Mvc.AuthorizeAttribute());
        }
    }
}