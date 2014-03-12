using System.Collections.Generic;
using System.Web.Mvc;
using AdminPanel.Helpers.Forms;
using AdminPanel.Helpers.Tabs;

namespace AdminPanel.Helpers.Widget
{
    public static class TabHelper
    {
        public static TabComponent TabComponent(this HtmlHelper Html, string TabComponentId)
        {
            return new TabComponent(Html, TabComponentId);
        }
    }

    public class TabComponent : BaseHelper
    {
        private string _tabComponentId;
        private List<Tab> _listTabs = new List<Tab>();
        private HtmlHelper _htmlHelper;

        public TabComponent(HtmlHelper helper, string TabComponentId)
            : base(helper)
        {
            this._tabComponentId = TabComponentId;
            this._htmlHelper = helper;
        }

        public TabComponent AddTab(Tab tab)
        {
            _listTabs.Add(tab);
            return this;
        }

        public override string ToString()
        {
            string tabMenu = "";
            string tabBody = "";
            foreach (Tab t in this._listTabs)
            {
                tabMenu += "<li><a href=\"#" + t.TabId + "\">" + t.Title + "</a></li>\n";
                tabBody += t.ToString() + "\n";
            }

            string code = "";
            code += "<div class=\"well well-sm well-light\">\n";
            code += "   <div id=\"" + this._tabComponentId + "\">\n";
            code += "       <ul>\n";
            code += tabMenu;
            code += "       </ul>\n";
            code += tabBody;
            code += "   </div>\n";
            code += "</div>\n";
            code += "<script>$('#" + this._tabComponentId + "').tabs();</script>\n";
            return code;
        }

        protected override string Html()
        {
            throw new System.NotImplementedException();
        }

        protected override string Script()
        {
            throw new System.NotImplementedException();
        }
    }
}