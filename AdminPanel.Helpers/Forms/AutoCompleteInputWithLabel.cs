using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Forms
{
    public class AutoCompleteInputWithLabel : FormItem
    {
        private string _label;
        private string _tooltip;
        private string _url;
        private bool _required;

        public AutoCompleteInputWithLabel(string ItemId, string ItemName, string Label, string Tooltip, string Url, bool Required = false)
            : base(ItemId, ItemName) 
        {
            _label = Label;
            _required = Required;
            _tooltip = Tooltip;
            _url = Url;
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
            code += "       <input type=\"text\" id=\"" + this._itemId + "\" " + req + ">";
            code += "       <input type=\"hidden\" id=\"" + this._itemId + "Hidden\" name=\"" + this._itemName + "\" " + req + ">";
            code += "       <b class=\"tooltip tooltip-top-right\"><i class=\"fa fa-info-circle txt-color-teal\"></i> " + this._tooltip + "</b>";
            code += "   </label>";
            code += "</section>";

            code += "<script>\n";
            code += "   $('#" + this._itemId + "').autocomplete({\n";
            code += "       source: \"" + this._url + "\",\n";
            code += "       minLength: 2,\n";
            code += "       select: function(event,ui){\n";
            code += "           $(this).val(ui.item.label);";
            code += "           $(\"input[id=" + this._itemId + "Hidden]\").val(ui.item.value);";
            code += "       }\n";
            code += "   });\n";
            code += "</script>\n";

            return code;
        }
    }
}
