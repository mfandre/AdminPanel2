using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Forms
{
    public class InputDateWithLabel : FormItem
    {
        private string _label;
        private string _format;
        private string _tooltip;
        private bool _required;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ItemId"></param>
        /// <param name="ItemName"></param>
        /// <param name="Label"></param>
        /// <param name="PlaceHolder"></param>
        /// <param name="Format">Formato do tipo dd/mm/yyyy</param>
        public InputDateWithLabel(string ItemId, string ItemName, string Label, string Tooltip, string Format, bool Required = false)
            : base(ItemId, ItemName) 
        {
            this._label = Label;
            this._tooltip = Tooltip;
            this._format = Format;
            this._required = Required;
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
            code += "       <input type=\"text\" class=\"date-picker\" id=\"" + this._itemId + "\" name=\"" + this._itemName + "\" data-mask=\"99/99/9999\" " + req + ">";
            code += "       <b class=\"tooltip tooltip-top-right\"><i class=\"fa fa-info-circle txt-color-teal\"></i> " + this._tooltip + "</b>";
            code += "   </label>";
            code += "</section>";

            return code;
        }
    }
}
