using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.Widget
{
    public class Widget
    {
        private string _title;
        private string _widgetId;
        private bool Toolbar;
        private List<BaseHelper> _items = new List<BaseHelper>();
        private List<BaseHelper> _toolbarItems = new List<BaseHelper>();

        public Widget(string Id, string Title)
        {
            this._title = Title;
            this._widgetId = Id;
        }

        public Widget AddItem(BaseHelper item)
        {
            this._items.Add(item);
            return this;
        }

        public Widget AddToolbarItem(BaseHelper item)
        {
            this._toolbarItems.Add(item);
            return this;
        }

        public override string ToString()
        {
            string code = "";
            code += "<div class=\"jarviswidget jarviswidget-color-blueDark\" id=\"" + this._widgetId + "\" data-widget-editbutton=\"false\" data-widget-colorbutton=\"false\">\n";
            code += "   <header><span class=\"widget-icon\"> <i class=\"fa fa-table\"></i></span><h2>" + this._title + "</h2></header>\n";
            code += "   <div>\n";
            code += "       <div class=\"jarviswidget-editbox\"></div>\n";
            code += "       <div class=\"widget-body no-padding\">\n";
            if (_toolbarItems.Count > 0)
            {
                code += "           <div class=\"widget-body-toolbar\">\n";
                foreach (BaseHelper tlbItem in _toolbarItems)
                {
                    code += tlbItem.ToString();
                }
                code += "           </div>\n";
            }
            foreach (BaseHelper h in this._items)
            {
                code += h.ToString();
            }
            code += "       </div>\n";
            code += "   </div>\n";
            code += "</div>\n";

            return code;
        }
    }
}
