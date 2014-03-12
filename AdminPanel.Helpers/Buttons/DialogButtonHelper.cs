using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.Buttons
{
    public static class DialogButtonHelper
    {
        /// <summary>
        /// Cria um botão que quando clicado abre um dialog/modal com os dados da url definiada. 
        /// Se o parametro Parameters for diferente de null ou string.empty ele será passado via método post para a url. 
        /// Em caso de passagem via GET concatene o dados no parametro URL.
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="ButtonId"></param>
        /// <param name="Text"></param>
        /// <param name="Url"></param>
        /// <param name="Parameters">Para envio de parametro via POST preencha essa string da seguinte forma "param1:valor, param2:valor2, param3:10"</param>
        /// <returns></returns>
        public static DialogButton DialogButton(this HtmlHelper Html, string ButtonId, string Text, string Url, string DialogTitle, string Parameters = "")
        {
            return new DialogButton(Html, ButtonId, Text, Url, Parameters, DialogTitle);
        }
    }

    public class DialogButton : BaseHelper
    {
        private string Text;
        private string Url;
        private string ButtonId;
        private string DialogTitle;
        public string Parameters { get; set; }


        public DialogButton(HtmlHelper Html, string ButtonId, string Text, string Url, string Parameters,string DialogTitle)
            : base(Html)
        {
            // TODO: Complete member initialization
            this.Text = Text;
            this.Url = Url;
            this.ButtonId = ButtonId;
            this.Parameters = Parameters;
            this.DialogTitle = DialogTitle;
        }

        public DialogButton(HtmlHelper Html, string ButtonId, string Text, string Url)
            : base(Html)
        {
            // TODO: Complete member initialization
            this.Text = Text;
            this.Url = Url;
            this.ButtonId = ButtonId;
        }

        public override string ToString()
        {
            string code = "";

            code += "<a href=\"javascript:void(0);\" class=\"btn btn-default btn-xs\" id=\"" + ButtonId + "\"><i class=\"fa fa-gear fa-lg\"></i> " + Text + "</a>";
            code += "<script>\n";
            code += "$(document).ready(function() {\n";
            code += "   $('#" + ButtonId + "').click(function(){\n";
            code += "       var url = '" + Url + "';\n";
            code += "       var button = $(this);\n";
            code += "       button.children().addClass('fa-refresh');\n";
            code += "       button.children().removeClass('fa-gear');\n";
            code += "       button.children().addClass('fa-spin');\n";
            code += "       button.addClass('disabled');\n";
            code += "       var dialog_" + ButtonId + " = $('<div class=\"modal-body no-padding\" id=\"dialog_" + ButtonId + "Dialog\" style=\"display:none\"></div>').appendTo('body');\n";
            code += "       dialog_" + ButtonId + ".dialog({\n";
            code += "           close: function(event,ui){\n";
            code += "               dialog_" + ButtonId + ".remove()\n";
            code += "               button.children().removeClass('fa-refresh');\n";
            code += "               button.children().addClass('fa-gear');\n";
            code += "               button.children().removeClass('fa-spin');\n";
            code += "               button.removeClass('disabled');\n";
            code += "           },\n";
            code += "           position: ['center',20],\n";
            code += "           modal: true\n";
            code += "       });\n";
            code += "       dialog_" + ButtonId + ".load(url,\n";
            if(Parameters.Trim().Length != 0)
                code += "           {" + Parameters + "},\n";
            code += "           function(responseText, textStatus,XMLHttpRequest){\n";
            code += "               $(\"<b>" + DialogTitle + "</b>\").appendTo($('.ui-dialog-titlebar .ui-dialog-title'))\n";
            code += "           }\n";
            code += "       );\n";
            code += "       return false;\n";
            code += "   });\n";
            code += " });\n";
            code += "</script>\n";
            return code;
        }

        protected override string Html()
        {
            throw new NotImplementedException();
        }

        protected override string Script()
        {
            throw new NotImplementedException();
        }
    }
}
