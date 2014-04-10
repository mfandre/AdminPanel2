using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Buttons.Behaviors
{
    public class DialogBehavior : IButtonBehavior
    {
        private string Parameters;
        private string Url;
        private string DialogTitle;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="DialogTitle"></param>
        /// <param name="Parameters">Para envio de parametro via POST preencha essa string da seguinte forma "param1:valor, param2:valor2, param3:10"</param>
        public DialogBehavior(string Url, string DialogTitle,string Parameters = "")
        {
            this.Parameters = Parameters;
            this.Url = Url;
            this.DialogTitle = DialogTitle;
        }

        public override string DoBehavior()
        {
            string code = "";

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
            if (Parameters.Trim().Length != 0)
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
            return "";
        }
    }
}
