using System.Collections.Generic;
using System.Web.Mvc;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.Tables
{
    public static class DataTableHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="TableId">ID html da tabela, lembre-se de ser unico por página</param>
        /// <param name="DataSource">Url que retona uma listagem (resultado)</param>
        /// <returns></returns>
        public static Table Table(this HtmlHelper Html, string TableId, string DataSource)
        {
            return new Table(Html, TableId, DataSource);
        }
    }

    public class Table : BaseHelper
    {
        private string _tableId;
        private string _dataSource;
        private List<ColumnItem> _listColumns = new List<ColumnItem>();
        private HtmlHelper _htmlHelper;
        private int _scrollXInner = 100;
        private List<BaseHelper> _toolbarActions = new List<BaseHelper>();

        public Table(HtmlHelper Helper, string TableId, string DataSource) 
            : base(Helper)
        {
            this._tableId = TableId;
            this._htmlHelper = Helper;
            this._dataSource = DataSource;
        }

        public Table setScrollXInner(int size)
        {
            this._scrollXInner = size;
            return this;
        }

        public Table addAction(BaseHelper button)
        {
            this._toolbarActions.Add(button);
            return this;
        }

        public Table AddColumn(ColumnItem col)
        {
            this._listColumns.Add(col);
            return this;
        }

        public Table AddColumn(string ColumnData, string ColumnName, string Template = "", bool Fixed = false, ColumnType Type = ColumnType.String,int Width = -1)
        {
            ColumnItem col = new ColumnItem(ColumnData, ColumnName,Type, Fixed, true,Template ,Width);
            if (this._listColumns.Count == 0 || !col.Fixed)
            {
                this._listColumns.Add(col);
                return this;
            }

            //procurando se existe alguma colua fixa definida
            int posFixed = this._listColumns.FindLastIndex(c => c.Fixed == true);
            if (posFixed == -1)
                this._listColumns.Insert(0, col);
            else
                this._listColumns.Insert(posFixed + 1, col);
            return this;
        }

        public override string ToString()
        {
            string code = "";
            bool HasActionButton = false;
            //Estrutura HTML
            code += "           <div id=\"" + this._tableId + "Toolbar\" style=\"margin:10px\">\n";
            foreach (BaseHelper btn in this._toolbarActions)
            {
                code += btn;
            }
            code += "           </div>\n";
            code += "           <table id=\"" + this._tableId + "\" class=\"table table-striped table-bordered smart-form table-hover\" style=\"width: 100%;\">\n";
            code += "               <thead>\n";
            code += "                   <tr>\n";
            //foreach(ColumnItem col in this._listColumns)
            //    code += "           <th>" + col.ColumnName + "</th>\n";
            code += "                   </tr>\n";
            code += "               </thead>\n";
            code += "               <tbody>\n";
            code += "               </tbody>\n";
            code += "           </table>\n";

            //JavaScript associado
            code += "<script>\n";
            code += "   $(document).ready(function() {\n";
            code += "        var " + this._tableId + " = $('#" + this._tableId + "').DataTable({\n";
            code += "            \"sPaginationType\" : \"bootstrap\",\n";
            code += "            \"sDom\" : \"R<'dt-top-row'Clf>r<'dt-wrapper't><'dt-row dt-bottom-row'<'row'<'col-sm-6'i><'col-sm-6 text-right'p>>\",";
            code += "            \"bServerSide\": true,\n";
            code += "            \"sAjaxSource\": \"" + this._dataSource + "\",\n";
            code += "            \"bProcessing\": true,\n";
            code += "            \"bAutoWidth\": true,\n";
            //code += "            \"sScrollX\": \"100%\",\n";
            //code += "            \"sScrollXInner\": \"" + this._scrollXInner + "%\",\n";
		    //code += "            \"bScrollCollapse\": true,\n";
            code += "            \"aoColumns\": [\n";
            foreach (ColumnItem col in this._listColumns)
            {
                code += col + "   ,\n";
            }
            code = code.Remove(code.Length-2) + "\n"; // removendo a ultima virgula
            code += "            ],\n";
            code += "           \"aoColumnDefs\": [],\n";
            code += "            \"oLanguage\": {\n";
            code += "                \"sProcessing\": \"<div class='progress progress-striped active' style='width: 80%;margin: 0 auto;'><div class='progress-bar'  role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width: 100%'><span class='sr-only'>Processando</span></div></div>\"\n";
            code += "            },\n";
            code += "            \"fnDrawCallback\" : function(oSettings) {\n";
            code += "               $('.ColVis_Button').addClass('btn btn-default btn-sm').html('Columns <i class=\"icon-arrow-down\"></i>');\n";
            //Handler do ActionButton
            for (int i = 0; i < this._listColumns.Count; i++)
            {
                if (this._listColumns[i] is DialogColumnItem)
                {
                    DialogColumnItem d = this._listColumns[i] as DialogColumnItem;
                    code += "   var buttons = $('." + d.DialogColumnItemId + "');\n";
                    code += "   for (var i = 0; i < buttons.length; i++)\n";
                    code += "       $(buttons[i]).click(function(){\n";
                    code += "          var url = '" + d.Url + "';\n";
                    code += "          var button = $(this);\n";
                    code += "          button.children().addClass('fa-refresh');\n";
                    code += "          button.children().removeClass('fa-gear');\n";
                    code += "          button.children().addClass('fa-spin');\n";
                    code += "          button.addClass('disabled');\n";
                    code += "          var dialog_" + d.DialogColumnItemId + " = $('<div class=\"modal-body no-padding\" id=\"dialog_" + d.DialogColumnItemId + "Dialog\" style=\"display:none\"></div>').appendTo('body');\n";
                    code += "          dialog_" + d.DialogColumnItemId + ".dialog({\n";
                    code += "              close: function(event,ui){\n";
                    code += "                  dialog_" + d.DialogColumnItemId + ".remove()\n";
                    code += "                  button.children().removeClass('fa-refresh');\n";
                    code += "                  button.children().addClass('fa-gear');\n";
                    code += "                  button.children().removeClass('fa-spin');\n";
                    code += "                  button.removeClass('disabled');\n";
                    code += "                  $('#" + this._tableId + "').dataTable().fnDraw(); //atualizando a datatable\n";
                    code += "              },\n";
                    code += "              position: ['center',20],\n";
                    code += "              modal: true\n";
                    code += "          });\n";
                    code += "          dialog_" + d.DialogColumnItemId + ".load(url+'/'+$(this).data('id'),\n";
                    code += "              function(responseText, textStatus,XMLHttpRequest){\n";
                    code += "                  $(\"<b>Teste</b>\").appendTo($('.ui-dialog-titlebar .ui-dialog-title'))\n";
                    code += "              }\n";
                    code += "          );\n";
                    code += "          return false;\n";
                    code += "      });\n";
                }
                else if (this._listColumns[i] is ActionColumnItem)
                {
                    HasActionButton = true;
                    ActionColumnItem c = this._listColumns[i] as ActionColumnItem;
                    code += "   var buttons = $('." + c.ActionColumnItemId + "');\n";
                    code += "   for (var i = 0; i < buttons.length; i++)\n";
                    code += "       $(buttons[i]).click(function(){\n";
                    code += "           var url = '" + c.Url + "';\n";
                    code += "           var button = $(this);\n";
                    code += "           button.children().addClass('fa-refresh');\n";
                    code += "           button.children().removeClass('fa-gear');\n";
                    code += "           button.children().addClass('fa-spin');\n";
                    code += "           button.addClass('disabled');\n";
                    code += "           $.ajax({\n";
                    code += "               url: url,\n";
                    code += "               data: { \"id\":button.data(\"id\") },\n"; //sempre vai passar a coluna ID
                    code += "               success: function (data) {\n";
                    code += "                   if (data.success) {\n";
                    code += "                       $.smallBox({\n";
                    code += "                           title : \"Sucesso\",\n";
                    code += "                           content : data.message,\n";
                    code += "                          color : \"#739E73\",\n";
                    code += "                          icon : \"fa fa-check-circle\"\n";
                    code += "                      });";
                    code += "                  }\n";
                    code += "                  else{\n";
                    code += "                      $.smallBox({\n";
                    code += "                           title : \"Erro\",\n";
                    code += "                           content : data.message,\n";
                    code += "                           color : \"#C46A69\",\n";
                    code += "                           icon : \"fa fa-times-circle\"\n";
                    code += "                       });";
                    code += "                   }\n";
                    code += "                   button.children().removeClass('fa-refresh');\n";
                    code += "                   button.children().removeClass('fa-spin');\n";
                    code += "                   button.removeClass('disabled');\n";
                    code += "                   button.children().addClass('fa-gear');\n";
                    code += "                   $('#" + this._tableId + "').dataTable().fnDraw(); //atualizando a datatable\n";
                    code += "               },\n";
                    code += "               error: function (jqXHR, textStatus, errorThrown) {\n";
                    code += "                   $.smallBox({\n";
                    code += "                       title : \"Erro\",\n";
                    code += "                       content : textStatus,\n";
                    code += "                       color : \"#C46A69\",\n";
                    code += "                       icon : \"fa fa-times-circle\"\n";
                    code += "                   });";
                    code += "                   button.children().removeClass('fa-refresh');\n";
                    code += "                   button.children().removeClass('fa-spin');\n";
                    code += "                   button.removeClass('disabled');\n";
                    code += "                   button.children().addClass('fa-gear');\n";
                    code += "               }\n";
                    code += "           });\n";
                    code += "           return false;\n";
                    code += "       });\n";
                }
            }
            code += "            },\n";
            //Essa logica serve para alterar a coluna que sofrerá o primeiro order by
            //if (HasActionButton)
            //{
            //    Alterando parametros que serao enviados para o servidor
            //    code += "                    \"fnServerParams\": function ( aoData ) {\n";
            //    code += "                        oSettings = this.fnSettings();\n";
            //    code += "                        for (ao in aoData) { \n";
            //    code += "                            name = aoData[ao].name;\n";
            //    code += "                            value = aoData[ao].value;\n";
            //    code += "                            if (name == \"iSortCol_0\") {\n";
            //    code += "                               while(";
            //    for (int i = 0; i < this._listColumns.Count; i++) if (this._listColumns[i] is ActionColumnItem) code += "aoData[ao].value == " + i + " || ";
            //    code = code.Remove(code.Length - 3);
            //    code += ")\n";
            //    code += "                               { aoData[ao].value++; }\n";
            //    code += "                            }\n";
            //    code += "                       }\n";
            //    code += "                    }\n";
            //}
            code += "        }).fnSetFilteringDelay(500);\n";
            code += "    });\n";
            code += "</script>\n";

            return code;
        }
    }
}
