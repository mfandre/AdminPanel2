using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Helpers.Tables;
using DataAccess.Menu;
using Entity.Models;
using Entity.Models.Menu;

namespace AdminPanel2.Controllers.Menu
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        /// <summary>
        /// Retorna o menu principal
        /// </summary>
        /// <returns></returns>
        public ActionResult MainMenu()
        {
            MenuDAO dao = new MenuDAO();

            List<MenuItem> menu = dao.ReadAll().ToList<MenuItem>();

            List<MenuItem> menuRtn = BuildMenu(menu);

            return PartialView("_MainMenu", menuRtn);
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retorna todos os itens do menu ativos
        /// </summary>
        /// <returns></returns>
        public JsonResult ReadAll()
        {
            MenuDAO dao = new MenuDAO();
            IEnumerable<MenuItem> menuItens = dao.ReadAll().ToList<MenuItem>().Select(x => new MenuItem { Name = x.Name, Root = x.Root, MenuItemId = x.MenuItemId, ParentId = x.ParentId, Children = null, Icon = x.Icon, Url = x.Url, Leaf = x.Leaf });

            DataTableParser<MenuItem> dtParser = new DataTableParser<MenuItem>(Request, menuItens.AsQueryable());
            return Json(dtParser.Parse(), JsonRequestBehavior.AllowGet);
        }

        private List<MenuItem> BuildMenu(List<MenuItem> root)
        {
            List<MenuItem> menuRoot = root.Where(it => it.Root == true).ToList();
            foreach (MenuItem it in menuRoot)
            {
                List<MenuItem> menuChild = root.Where(i => i.ParentId == it.MenuItemId).ToList();
                it.Children = menuChild;
                BuildMenu(it.Children);
            }

            return menuRoot;
        }

    }
}
