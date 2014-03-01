using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DataAccess.ControleDeAmbientes;
using Entity.Models;

namespace AdminPanel2.Controllers
{
    public class ServidoresSdeController : Controller
    {
        //
        // GET: /ServidoresSde/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Controller pra renderizar a VIEW
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Controller que recebe a chamada AJAX do fomulario de criacao
        /// </summary>
        /// <param name="servidor"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(Sde servidor)
        {
            if (ModelState.IsValid)
            {
                SdeDAO servidorDAO = new SdeDAO();
                Object result = servidorDAO.Create(servidor);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = false, message = "Preencha corretamente." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Controller pra renderizar a VIEW
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            SdeDAO servidorDAO = new SdeDAO();
            Sde servidor = servidorDAO.ReadOne(id);
            if (servidor == null)
            {
                return HttpNotFound();
            }
            return View(servidor);
        }

        [HttpPost]
        public ActionResult Edit(Sde servidor)
        {
            if (ModelState.IsValid)
            {
                SdeDAO servidorDAO = new SdeDAO();
                Object result = servidorDAO.Update(servidor);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = false, message = "Preencha corretamente." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteServidor(string term)
        {
            SdeDAO servidorDAO = new SdeDAO();
            Object result = servidorDAO.ReadAll().Where(s => s.Name.Contains(term)).Select(s => new {label = s.Name, value = s.SdeId});
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
