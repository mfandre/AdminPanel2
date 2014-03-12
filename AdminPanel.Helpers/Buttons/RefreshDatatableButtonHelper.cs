using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.Buttons
{
    public static class RefreshDatatableButtonHelper
    {
        /// <summary>
        /// Cria um botão que quando clicado da um refresh na datatable especificada
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="ButtonId"></param>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static RefreshDatatableButton RefreshDatatableButton(this HtmlHelper Html, string ButtonId, string Text, string DatatableId)
        {
            return new RefreshDatatableButton(Html, ButtonId, Text, DatatableId);
        }
    }

    public class RefreshDatatableButton : BaseHelper
    {
        private string Text;
        private string ButtonId;
        private string DatatableId;

        public RefreshDatatableButton(HtmlHelper Html, string ButtonId, string Text,string DatatableId)
            : base(Html)
        {
            // TODO: Complete member initialization
            this.Text = Text;
            this.ButtonId = ButtonId;
            this.DatatableId = DatatableId;
        }

        public override string ToString()
        {
            string code = "";

            code += "<a href=\"javascript:void(0);\" class=\"btn btn-default btn-xs\" id=\"" + ButtonId + "\"><i class=\"fa fa-refresh fa-lg\"></i> " + Text + "</a>";
            code += "<script>\n";
            code += "$(document).ready(function() {\n";
            code += "   $('#" + ButtonId + "').click(function(){\n";
            code += "           $('#" + this.DatatableId + "').dataTable().fnDraw(); //atualizando a datatable\n";
            code += "   });\n";
            code += " });\n";
            code += "</script>\n";
            return code;
        }

        protected override string Script()
        {
            throw new NotImplementedException();
        }

        protected override string Html()
        {
            throw new NotImplementedException();
        }
    }
}
