using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Forms
{
    public abstract class FormItem
    {
        public string _itemId {get; set;}
        public string _itemName { get; set; }

        public FormItem(string ItemId, string ItemName)
        {
            this._itemId = ItemId;
            this._itemName = ItemName;
        }
    }
}
