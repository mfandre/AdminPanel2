using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Forms
{
    public class InputPasswordWithLabel : FormItem
    {
        private string _label;
        private string _tooltip;
        private bool _required;

        public InputPasswordWithLabel(string ItemId, string ItemName, string Label, string Tooltip, bool Required = false)
            : base(ItemId, ItemName) 
        {
            _label = Label;
            _required = Required;
            _tooltip = Tooltip;
        }

        public override string ToString()
        {
            string req = "";
            if (this._required)
                req = "required";

            string code = "";
            code += "<section>";
            code += "   <label class=\"label\" for=\"" + this._itemId + "\">" + this._label + "</label>";
            code += "   <label class=\"input\">";
            code += "       <input type=\"password\" id=\"" + this._itemId + "\" name=\"" + this._itemName + "\" " + req + ">";
            code += "       <b class=\"tooltip tooltip-top-right\"><i class=\"fa fa-info-circle txt-color-teal\"></i> " + this._tooltip + "</b>";
            code += "   </label>";
            code += "</section>";

            return code;
        }
    }
}
