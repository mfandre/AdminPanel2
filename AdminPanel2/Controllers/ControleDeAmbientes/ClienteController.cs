using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Helpers.Tables;
using DataAccess.ControleDeAmbientes;
using Entity.Models;

namespace AdminPanel2.Controllers.ControleDeAmbientes
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/

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
        public JsonResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                ClienteDAO clienteDAO = new ClienteDAO();
                Object result = clienteDAO.Create(cliente);
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
            ClienteDAO clienteDAO = new ClienteDAO();
            Cliente cliente = clienteDAO.ReadOne(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                ClienteDAO clienteDAO = new ClienteDAO();
                Object result = clienteDAO.Update(cliente);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = false, message = "Preencha corretamente." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            Object result = clienteDAO.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Monta a query primeiro pra depois consulta tudo (o processamento fica inteiro no servidor)
        /// </summary>
        /// <returns></returns>
        public JsonResult ReadAll()
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            IQueryable<Cliente> clientes = clienteDAO.ReadAll();

            DataTableParser<Cliente> dtParser = new DataTableParser<Cliente>(Request, clientes);
            return Json(dtParser.Parse(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadAllServidoresSde(int ClienteId)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            IQueryable<Cliente> clientes = clienteDAO.GetServidoresSde(ClienteId);

            DataTableParser<Sde> dtParser = new DataTableParser<Sde>(Request, clientes.SingleOrDefault().ServidoresSde.AsQueryable().Select(
                /*Definindo os campos que serao apresentandos, alem de eliminar os atributos circular*/
                s => new Sde(){
                    ArcgisVersion = s.ArcgisVersion,
                    ConnDirect = s.ConnDirect,
                    DataReg = s.DataReg,
                    EstReg = s.EstReg,
                    Instance = s.Instance,
                    Login = s.Login,
                    Name = s.Name,
                    Password = s.Password,
                    SdeId = s.SdeId,
                    Server = s.Server,
                    Service = s.Service,
                    Version = s.Version
                }));
            return Json(dtParser.Parse(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadAllArcgisServer(int ClienteId)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            IQueryable<Cliente> clientes = clienteDAO.GetArcgisServers(ClienteId);

            DataTableParser<ArcgisServer> dtParser = new DataTableParser<ArcgisServer>(Request, clientes.SingleOrDefault().ArcgisServers.AsQueryable().Select(
                /*Definindo os campos que serao apresentandos, alem de eliminar os atributos circular*/
                s => new ArcgisServer()
                {
                    ArcgisVersion = s.ArcgisVersion,
                    Name = s.Name,
                    Url = s.Url,
                    Login = s.Login,
                    Password = s.Password,
                    DataReg = s.DataReg,
                    EstReg = s.EstReg
                }));
            return Json(dtParser.Parse(), JsonRequestBehavior.AllowGet);
        }
    }
}
