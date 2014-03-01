using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Context;
using AdminPanel2.Utils;
using Entity.Models;
using Repository;
using System.Data.Entity.Validation;
using System.Threading;
using DataAccess.Usuario;
using AdminPanel.Helpers.Tables;

namespace AdminPanel2.Controllers
{
    public class DataTableController : Controller
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new MainContext());
        //
        // GET: /DataTable/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Insert(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Password = Crypt.EncodeSHA1(user.Password);
                    _unitOfWork.Repository<User>().Insert(user);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Registro Inserido com Sucesso." }, JsonRequestBehavior.AllowGet);
                }
                else
                    return this.Json(new { success = false, message = "Preencha corretamente." }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}\n", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                return this.Json(new { success = false, message = sb.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return this.Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SomeAction(int? Id)
        {
            if(Id.HasValue)
                return this.Json(new { success = true, message = Id }, JsonRequestBehavior.AllowGet);
            return this.Json(new { success = false, message = "Sem ID" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Monta a query primeiro pra depois consulta tudo (o processamento fica inteiro no servidor)
        /// </summary>
        /// <returns></returns>
        public JsonResult ListQueryable()
        {
            Thread.Sleep(2000);
            UsuarioDAO userDAO = new UsuarioDAO();
            IQueryable<User> allUsers = userDAO.GetAllIQueryable();

            DataTableParser<User> dtParser = new DataTableParser<User>(Request, allUsers);
            return Json(dtParser.Parse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Processa primeiro depois passa todos os dados para o parser que processa no lado do cliente o outro filtro
        /// </summary>
        /// <returns></returns>
        public JsonResult ListEnumerable()
        {
            Thread.Sleep(2000);
            UsuarioDAO userDAO = new UsuarioDAO();
            List<User> allUsers = userDAO.GetAllIEnumerable();

            DataTableParser<User> dtParser = new DataTableParser<User>(Request, allUsers.AsQueryable());
            return Json(dtParser.Parse(), JsonRequestBehavior.AllowGet);

        }

    }
}
