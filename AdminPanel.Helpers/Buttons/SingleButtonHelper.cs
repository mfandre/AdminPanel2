using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AdminPanel.Helpers.Buttons.Behaviors;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.Buttons
{
    public static class SingleButtonHelper
    {

        /// <summary>
        /// Botão unico, ve deverá setar o comportamento dele pelo SetBehavior()
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="ButtonId">ID html do elemento</param>
        /// <returns></returns>
        public static SingleButton SingleButton(this HtmlHelper Html, string ButtonId, string Text)
        {
            return new SingleButton(Html, ButtonId, Text);
        }
    }

    public class SingleButton : BaseHelper
    {
        private string Text;
        private string ButtonId;
        IButtonBehavior behave;

        public SingleButton(HtmlHelper Html, string ButtonId, string Text)
            : base(Html)
        {
            // TODO: Complete member initialization
            this.Text = Text;
            this.ButtonId = ButtonId;
        }

        public SingleButton SetBehavior(IButtonBehavior behave)
        {
            this.behave = behave;
            this.behave.ButtonId = this.ButtonId;
            return this;
        }

        protected override string Html()
        {
            string code = "";
            code += "<a href=\"javascript:void(0);\" class=\"btn btn-default btn-xs\" id=\"" + ButtonId + "\"><i class=\"fa fa-gear fa-lg\"></i> " + Text + "</a>";
            return code;
        }

        protected override string Script()
        {
            string code = "";
            code += this.behave.DoBehavior();
            return code;
        }
    }
}
