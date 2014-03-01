using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Forms
{
    public class InputHidden : FormItem
    {
        private string _value;

        public InputHidden(string ItemId, string ItemName, string Value = "")
            : base(ItemId, ItemName) 
        {
            _value = Value;
        }

        public override string ToString()
        {
            string code = "";
            code += "<input type=\"hidden\" value=\"" + this._value + "\" id=\"" + this._itemId + "\" name=\"" + this._itemName + "\">";

            return code;
        }
    }
}
