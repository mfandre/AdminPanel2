using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdminPanel.Helpers.Forms
{
    public class BaseHelper
    {
        public HtmlHelper _helper;

        public BaseHelper(HtmlHelper helper)
        {
            this._helper = helper;
        }

        public void Render()
        {
            this._helper.ViewContext.Writer.Write(this.ToString());
        }
    }
}
