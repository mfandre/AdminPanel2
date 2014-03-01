using System.Collections.Generic;
using System.Web.Mvc;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.Tabs
{
    public class Tab
    {
        private List<BaseHelper> _listItems = new List<BaseHelper>();

        public string TabId { get; set; }
        public string Title { get; set; }

        public Tab(string TabId,string Title) 
        {
            this.TabId = TabId;
            this.Title = Title;
        }

        public Tab AddItem(BaseHelper item)
        {
            _listItems.Add(item);
            return this;
        }

        public override string ToString()
        {
            string code = "";
            code += "<div id=\"" + this.TabId + "\">\n";
            foreach (BaseHelper h in this._listItems)
            {
                code += h.ToString();
            }
            code += "</div>\n";
            return code;
        }  
    }
}