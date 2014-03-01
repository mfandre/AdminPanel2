using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Forms
{
    public class InputTextWithLabel : FormItem
    {
        private string _label;
        private string _tooltip;
        private string _value;
        private bool _required;
        
        public InputTextWithLabel(string ItemId, string ItemName, string Label, string Tooltip, bool Required = false, string Value = "")
            : base(ItemId, ItemName) 
        {
            _label = Label;
            _required = Required;
            _tooltip = Tooltip;
            _value = Value;
        }

        public override string ToString()
        {
            string req = "";
            if (this._required)
                req = "required";

            //<section>
            //    <label class="label">E-mail</label>
            //    <label class="input"> 
            //        <i class="icon-append fa fa-user"></i>
            //        <input type="email" name="email">
            //        <b class="tooltip tooltip-top-right"><i class="fa fa-user txt-color-teal"></i> Please enter email address/username</b>
            //    </label>
            //</section>

            string code = "";
            code += "<section>";
            code += "   <label class=\"label\" for=\"" + this._itemId + "\">" + this._label + "</label>";
            code += "   <label class=\"input\">";
            code += "       <input type=\"text\" value=\"" + this._value + "\" id=\"" + this._itemId + "\" name=\"" + this._itemName + "\" " + req + ">";
            code += "       <b class=\"tooltip tooltip-top-right\"><i class=\"fa fa-info-circle txt-color-teal\"></i> " + this._tooltip + "</b>";
            code += "   </label>";
            code += "</section>";

            return code;
        }
    }
}
