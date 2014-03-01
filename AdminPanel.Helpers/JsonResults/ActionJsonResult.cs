using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdminPanel.Helpers.JsonResults
{
    public class ActionJsonResult : JsonResult
    {
        public bool Success;
        public string Message;

        public ActionJsonResult(bool Success, string Message)
        {
            //this.Json(new { success = false, message = "Falha ao efetuar login. Verifique suas credenciais." }, JsonRequestBehavior.AllowGet);
            this.Success = Success;
            this.Message = Message;
            this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        
    }
}
