using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Buttons.Behaviors
{
    public class ActionBehavior : IButtonBehavior
    {
        private string Parameters;
        private string Url;

        /// <summary>
        /// Cria um botão que quando clicado executa uma requisição para url (ajax). Se os parametro Parameters for
        /// diferente de null ou "" ele será passado via método post para a url. Em caso de passagem via GET concatene o dados 
        /// no parametro URL, por exemplo http://minhaurl.com/?param1=1&param2=Teste
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Parameters">Para envio de parametro via POST preencha essa string da seguinte forma "param1:valor, param2:valor2, param3:10"</param>
        public ActionBehavior(string Url,string Parameters = "")
        {
            this.Parameters = Parameters;
            this.Url = Url;
        }

        public override string DoBehavior()
        {
            string code = "";

            code += "<script>\n";
            code += "   function resetButton" + ButtonId + "(url){\n";
            code += "       $('#" + ButtonId + "').children().removeClass('fa-refresh');\n";
            code += "       $('#" + ButtonId + "').children().removeClass('fa-spin');\n";
            code += "       $('#" + ButtonId + "').removeClass('disabled');\n";
            code += "       $('#" + ButtonId + "').children().addClass('fa-gear');\n";
            code += "       $('#" + ButtonId + "').attr('href',url);\n";
            code += "   }\n";
            code += "   function requestUrl" + ButtonId + "(url){\n";
            code += "       $.ajax({\n";
            code += "           url: url,\n";
            if (Parameters != null && Parameters.Trim() != "")
                code += "           type: 'POST',\n";
            code += "           data: {" + Parameters + "},\n";
            code += "           success: function (data) {\n";
            code += "               if (data.success) {\n";
            code += "                   $.smallBox({\n";
            code += "                       title : \"Sucesso\",\n";
            code += "                       content : data.message,\n";
            code += "                       color : \"#739E73\",\n";
            code += "                       icon : \"fa fa-check-circle\"\n";
            code += "                   });";
            code += "               }\n";
            code += "               else{\n";
            code += "                   $.smallBox({\n";
            code += "                       title : \"Erro\",\n";
            code += "                       content : data.message,\n";
            code += "                       color : \"#C46A69\",\n";
            code += "                       icon : \"fa fa-times-circle\"\n";
            code += "                   });";
            code += "               }\n";
            code += "               $('#" + ButtonId + "').children().removeClass('fa-refresh');\n";
            code += "               $('#" + ButtonId + "').children().removeClass('fa-spin');\n";
            code += "               $('#" + ButtonId + "').removeClass('disabled');\n";
            code += "               $('#" + ButtonId + "').children().addClass('fa-gear');\n";
            code += "               $('#" + ButtonId + "').attr('href',url);\n";
            code += "           },\n";
            code += "           error: function (jqXHR, textStatus, errorThrown) {\n";
            code += "                   $.smallBox({\n";
            code += "                       title : \"Erro\",\n";
            code += "                       content : textStatus,\n";
            code += "                       color : \"#C46A69\",\n";
            code += "                       icon : \"fa fa-times-circle\"\n";
            code += "                   });";
            code += "                   $('#" + ButtonId + "').children().removeClass('fa-refresh');\n";
            code += "                   $('#" + ButtonId + "').children().removeClass('fa-spin');\n";
            code += "                   $('#" + ButtonId + "').removeClass('disabled');\n";
            code += "                   $('#" + ButtonId + "').children().addClass('fa-gear');\n";
            code += "                   $('#" + ButtonId + "').attr('href',url);\n";
            code += "           }\n";
            code += "       });\n";
            code += "   }\n";
            code += "$(document).ready(function() {\n";
            code += "   $('#" + ButtonId + "').click(function(){\n";
            code += "       var url = '" + Url + "';\n";
            code += "       var button = $(this);\n";
            code += "       button.children().addClass('fa-refresh');\n";
            code += "       button.children().removeClass('fa-gear');\n";
            code += "       button.children().addClass('fa-spin');\n";
            code += "       button.addClass('disabled');\n";
            code += "       button.attr('href','javascript:void(0);');\n";
            code += "       $.smallBox({\n";
            code += "           title : \"Confirmation\",\n";
            code += "           content : \"Confirmar? <p class='text-align-right'><a href='javascript:void(0);' onclick=\\\"requestUrl" + ButtonId + "('" + Url + "')\\\" class='btn btn-primary btn-sm'>Yes</a> <a href='javascript:void(0);' onclick=\\\"resetButton" + ButtonId + "('" + Url + "')\\\" class='btn btn-danger btn-sm'>No</a></p>\",\n";
            code += "           color : \"#296191\",\n";
            code += "           icon : \"fa fa-question swing animated\"\n";
            code += "       });\n";
            code += "       return false;\n";
            code += "   });\n";
            code += " });\n";
            code += "</script>\n";

            return "";
        }
    }
}
