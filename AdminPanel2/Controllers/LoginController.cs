using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AdminPanel.Helpers.JsonResults;
using DataAccess.Usuario;
using Entity.Models;

namespace AdminPanel2.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DoLogin(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoginDAO loginDAO = new LoginDAO();
                    user.Password = Utils.Crypt.EncodeSHA1(user.Password);

                    user = loginDAO.CheckLogin(user);
                    if (user != null)
                    {
                        //FormsAuthentication.SetAuthCookie(user.Username, true);
                        
                        Session.Add("usuario", user);
                        
                        return Json(new { success = true, message = "Login efetuado com sucesso.", redirect = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
                    }
                }
                return this.Json(new { success = false, message = "Falha ao efetuar login. Verifique suas credenciais." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return this.Json(new { success = false, message = "O servidor respondeu com uma exceção. \n\nDetalhes:\n\n" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
