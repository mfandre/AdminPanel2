using System.Collections.Generic;
using System.Web.Mvc;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.Widget
{
    public static class WidgetHelper
    {
        public static PanelWidget CorePanelWidgets(this HtmlHelper Html, string PanelWidgetId)
        {
            return new PanelWidget(Html, PanelWidgetId);
        }
    }

    public class PanelWidget : BaseHelper
    {
        private string _panelWidgetId;
        private List<Widget> _listWidgets = new List<Widget>();
        private HtmlHelper _htmlHelper;

        public PanelWidget(HtmlHelper helper, string PanelWidgetId) 
            : base(helper)
        {
            this._panelWidgetId = PanelWidgetId;
            this._htmlHelper = helper;
        }

        public PanelWidget AddWidget(Widget item)
        {
            _listWidgets.Add(item);
            return this;
        }

        public override string ToString()
        {
            string code = "";
            code += "<section id=\"widget-grid\">\n";
            code += "   <div class=\"row\">\n";
            code += "       <article class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\n";
            foreach (Widget w in this._listWidgets)
            {
                code += w.ToString();
            }
            code += "       </article>\n";
			code += "	</div>\n";
            code += "</section>\n";
            //code += "<script>setup_widgets_desktop();</script>\n";
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