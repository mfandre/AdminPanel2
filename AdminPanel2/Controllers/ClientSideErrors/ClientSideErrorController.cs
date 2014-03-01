using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Helpers.Tables;
using DataAccess.ClientSideErrors;
using Entity.Models.ClientSideErrors;

namespace AdminPanel2.Controllers.ClientSideErrors
{
    public class ClientSideErrorController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public void Occurred(ClientSideJavaScriptException error)
        {
            ClientSideJavascriptExceptionDAO dao = new ClientSideJavascriptExceptionDAO();
            dao.Create(error);
        }

        public JsonResult ReadAll()
        {
            ClientSideJavascriptExceptionDAO dao = new ClientSideJavascriptExceptionDAO();
            IQueryable<ClientSideJavaScriptException> errors = dao.ReadAll();

            DataTableParser<ClientSideJavaScriptException> dtParser = new DataTableParser<ClientSideJavaScriptException>(Request, errors);
            return Json(dtParser.Parse(), JsonRequestBehavior.AllowGet);
        }

    }
}
