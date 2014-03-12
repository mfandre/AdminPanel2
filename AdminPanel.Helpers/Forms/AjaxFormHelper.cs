using System.Collections.Generic;
using System.Web.Mvc;

namespace AdminPanel.Helpers.Forms
{
    public static class AjaxFormHelper
    {
        public static FormAjax FormAjax(this HtmlHelper Html, string FormId, string FormAction, FormMethod Method, string Title)
        {
            return new FormAjax(Html, FormId, FormAction, Method, Title);
        }

        public static FormAjax FormAjax(this HtmlHelper Html, string FormId, string FormAction, FormMethod Method)
        {
            return new FormAjax(Html, FormId, FormAction, Method, null);
        } 
    }

    public enum FormMethod
    {
        GET,
        POST
    }

    public class FormAjax : BaseHelper
    {
        private string _title;
        private string _formId;
        private string _formAction;
        private string _submitButtonText = "Submit";
        private FormMethod _method;
        private List<FormItem> _listItems = new List<FormItem>();
        private HtmlHelper _htmlHelper;

        public FormAjax(HtmlHelper helper, string formId, string formAction,FormMethod method, string Title) 
            : base(helper)
        {
            this._formId = formId;
            this._formAction = formAction;
            this._method = method;
            this._htmlHelper = helper;
            this._title = Title;
        }

        public FormAjax AddFormItem(FormItem item)
        {
            _listItems.Add(item);
            return this;
        }

        public FormAjax SetSubmitButtonText(string text)
        {
            _submitButtonText = text;
            return this;
        }

        public override string ToString()
        {
            string code = "";
            code += "<form id='" + this._formId + "' class=\"smart-form client-form\">";
            if(this._title != null)
                code += "   <header>" + this._title + "</header>";
            code += "<fieldset>";
            foreach (FormItem item in this._listItems)
            {
                code += item;
            }
            code += "<section>";
            code += "   <div class=\"progress progress-striped active\" id=\"" + this._formId + "Progressbar\" style=\"display:none;\">";
            code += "       <div class=\"progress-bar\" role=\"progressbar\" aria-valuenow=\"100\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: 100%\"><span class=\"sr-only\">Processando</span></div>";
            code += "   </div>";
            code += "</section>";
            code += "</fieldset>";
            code += "<footer>";
            code += "    <button type=\"button\" class=\"btn btn-primary\" id=\"" + this._formId + "_submit" + "\">" + this._submitButtonText + "</button>";
            code += "</footer>";
            code += "</form>";
            
            code += "<script>\n";
            code += "$(function () {\n";
            //Enviar form quando apertar enter
            code += "   $('input').keydown(function(e) {\n";
            code += "       if (e.keyCode == 13) {\n";
            code += "           $(\"#" + this._formId + "_submit" + "\").click();\n";
            code += "           return false;\n";
            code += "       }\n";
            code += "   });\n";

            //Validate fields - colocando o caor vermelha no fields nao preenchidos ou errados
            code += "   $(\"#" + this._formId + "\").validate({\n";
            code += "       errorPlacement: function(error, element) {\n";
            code += "           error.insertAfter(element.next());\n";
            code += "       }\n";
            code += "   });\n";

            //Inicializando datepicker
            code += "   $('.date-picker').datepicker({\n";
			code += "       dateFormat : 'dd/mm/yy',\n";
			code += "       prevText : '<i class=\"fa fa-chevron-left\"></i>',\n";
			code += "       nextText : '<i class=\"fa fa-chevron-right\"></i>'\n";
            code += "   });\n";

            //Iniciando Masks
            code += "   if ($('#" + this._formId + " [data-mask]').length){\n";				
			code += "		$('[data-mask]').each(function(){\n";
			code += "			$this = $(this);\n";
			code += "			var mask = $this.attr('data-mask') || 'error...',\n";
			code += "				mask_placeholder = $this.attr('data-mask-placeholder') || 'X';\n";
			code += "			$this.mask(mask, {\n";
			code += "				placeholder : mask_placeholder\n";
			code += "			});\n";
			code += "		})\n";
            code += "	}\n";

            //Evento de click no botão de submit - logica que envia o form via ajax
            code += "   $(\"#" + this._formId + "_submit" + "\").click(function() {\n";
            code += "       if(!$(\"#" + this._formId + "\").valid()){";
            code += "           $.smallBox({\n";
			code += "               title : \"Aviso\",\n";
			code += "               content : \"Preencha os campos corretamente\",\n";
            code += "               color : \"#C79121\",\n";
            //code += "               timeout: 4000,\n";
			code += "               icon : \"fa fa-exclamation-circle\"\n";
            code += "           });";
            code += "           return;\n";
            code += "       }\n";
            code += "       $(\"#" + this._formId + "Progressbar\").show();\n";
            code += "       $.ajax({\n";
            code += "           url: \"" + this._formAction + "\",\n";
            code += "           type: '" + this._method + "',\n";
            code += "           data: $(\"#" + this._formId + "\").serialize(),\n";
            code += "           success: function (data) {\n";
            code += "               if (data.success) {\n";
            code += "                   $.smallBox({\n";
            code += "                       title : \"Sucesso\",\n";
            code += "                       content : data.message,\n";
            code += "                       color : \"#739E73\",\n";
            //code += "                       timeout: 4000,\n";
            code += "                       icon : \"fa fa-check-circle\"\n";
            code += "                   });";
            //code += "                   noty({text: data.message, type: 'success', layout:'topCenter'});\n";
            code += "                   if(data.redirect != null && data.redirect != \"\")";
            code += "                       window.location.replace(data.redirect);";
            code += "               }\n";
            code += "               else{\n";
            code += "                   $.smallBox({\n";
            code += "                       title : \"Erro\",\n";
            code += "                       content : data.message,\n";
            code += "                       color : \"#C46A69\",\n";
            //code += "                       timeout: 4000,\n";
            code += "                       icon : \"fa fa-times-circle\"\n";
            code += "                   });";
            //code += "                   noty({text: data.message, type: 'error', layout:'topCenter'});\n";
            code += "               }\n";
            code += "               $(\"#" + this._formId + "Progressbar\").hide();";
            code += "           },\n";
            code += "           error: function (jqXHR, textStatus, errorThrown) {\n";
            code += "                   $.smallBox({\n";
            code += "                       title : \"Erro\",\n";
            code += "                       content : textStatus,\n";
            code += "                       color : \"#C46A69\",\n";
            //code += "                       timeout: 4000,\n";
            code += "                       icon : \"fa fa-times-circle\"\n";
            code += "                   });";
            //code += "               noty({text: textStatus, type: 'error', layout:'topCenter'});\n";
            code += "               $(\"#" + this._formId + "Progressbar\").hide();";
            code += "           }\n";
            code += "       });\n";
            code += "   });\n";
            code += "});\n";
            code += "</script>\n";

            return code;
        }



        protected override string Html()
        {
            throw new System.NotImplementedException();
        }

        protected override string Script()
        {
            throw new System.NotImplementedException();
        }
    }
}