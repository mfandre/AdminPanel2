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
        private int _scrollXInner = 100;

        /// <summary>
        /// Html Id da Datatable, lembre-se que deve ser único por página
        /// </summary>
        private string _tableId;

        /// <summary>
        /// Url que retornara os dados para a datatable
        /// </summary>
        private string _dataSource;

        /// <summary>
        /// Id (html) da Datatable pai para fazer o filtro na Datatable filha quando se clicar em alguma row do pai.
        /// </summary>
        private string _parentTableId = null;
        private string _parentJoinField = null;
        private string _childJoinField = null;

        /// <summary>
        /// Lista de colunas da Datatable
        /// </summary>
        private List<ColumnItem> _listColumns = new List<ColumnItem>();

        /// <summary>
        /// Geralmente botoes de operacoes da Datatable
        /// </summary>
        private List<BaseHelper> _toolbarActions = new List<BaseHelper>();

        /// <summary>
        /// Filtro avancado, adicionar campos dinamicamente para busca
        /// </summary>
        private bool _advancedFilter = false;

        /// <summary>
        /// POST ou GET
        /// </summary>
        private string _method = "GET";

        public Table(HtmlHelper Helper, string TableId, string DataSource) 
            : base(Helper)
        {
            this._tableId = TableId;
            this._dataSource = DataSource;
        }

        public Table setAsTableDetail(string ParentTableId, string ParentJoinField, string ChildJoinField)
        {
            this._parentTableId = ParentTableId;
            this._parentJoinField = ParentJoinField;
            this._childJoinField = ChildJoinField;
            this._tableId = _tableId + "ChildOf" + ParentTableId;
            return this;
        }

        public Table setMethod(string method)
        {
            this._method = method;
            return this;
        }

        public Table setAdvancedFilter(bool hasAdvancedFilter)
        {
            this._advancedFilter = hasAdvancedFilter;
            return this;
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

        private string HtmlAdvancedFilter()
        {
            if (!this._advancedFilter)
                return "";

            string columnOptions = "";
            string operationOptions = "";
            string code = "";

            foreach (ColumnItem c in _listColumns)
            {
                if (c is DialogColumnItem || c is ActionColumnItem)
                    continue;
                columnOptions += "   					<option value=\"" + c.ColumnData + "\">" + c.ColumnName + "</option>\n";                
            }


            code += "           <div id=\"" + this._tableId + "PreFilter\" class=\"padding-10\">\n";
            code += "               <div>\n";
            code += "                   <h4>Filter</h4>\n";
            code += "                   <div id=\"" + this._tableId + "PreFilterForm\" class=\"padding-10\">\n";


            code += "                   <form class=\"smart-form\" novalidate=\"novalidate\">\n";
            code += "                       <div class=\"row-fluid\">\n";
			code += "				            <section class=\"col col-3\">\n";
			code += "           		            <label class=\"select\">\n";
			code += "           		            <select name=\"column[]\">\n";
			code += "           			            <option value=\"0\" selected=\"\" disabled=\"\">Colunas</option>\n";
            code += columnOptions;
			code += "           		            </select> <i></i> </label>\n";
			code += "           	            </section>\n";
            code += "				            <section class=\"col col-3\">\n";
            code += "           		            <label class=\"select\">\n";
            code += "           		            <select name=\"operation[]\">\n";
            code += "           			            <option value=\"0\" selected=\"\" disabled=\"\">Operações</option>\n";
            code += "           		            </select> <i></i> </label>\n";
            code += "           	            </section>\n";
            code += "				            <section class=\"col col-3\">\n";
            code += "                               <label class=\"input\">\n";
			code += "   								<input type=\"text\" name=\"valor\" placeholder=\"Valor\">\n";
            code += "   							</label>\n";
            code += "           	            </section>\n";
            code += "				            <section class=\"col col-3\">\n";
            code += "   							<a id=\"" + this._tableId + "PreFilterFormAddBtn\" class=\"btn btn-success\" href=\"javascript:void(0);\" style=\"padding: 6px 12px;\">Add</a>\n";
            code += "   							<a class=\"btn btn-primary\" href=\"javascript:void(0);\" style=\"padding: 6px 12px;\">Pesquisar</a>\n";
            code += "                           </section>\n";
            code += "                       </div>\n";
            code += "                   </form>\n";
            

            code += "                   </div>\n";
            code += "               </div>\n";
            code += "           </div>\n";

            return code;
        }

        private string HtmlToolbar()
        {
            string code = "";
            
            code += "           <div id=\"" + this._tableId + "Toolbar\" style=\"margin:10px\">\n";
            foreach (BaseHelper btn in this._toolbarActions)
            {
                code += btn;
            }
            code += "           </div>\n";
            return code;
        }

        protected override string Html()
        {
            string code = "";
            //Estrutura HTML
            //code += HtmlAdvancedFilter();
            code += HtmlToolbar();
            code += "           <table id=\"" + this._tableId + "\" class=\"table table-striped table-bordered table-hover\" style=\"width: 100%;\">\n";
            code += "               <thead>\n";
            code += "                   <tr>\n";
            code += "                   </tr>\n";
            code += "               </thead>\n";
            code += "               <tbody>\n";
            code += "               </tbody>\n";
            code += "           </table>\n";

            return code;
        }

        protected override string Script()
        {
            string code = "";
            //JavaScript associado
            code += "<script>\n";
            code += "   $(document).ready(function() {\n";

            //Script filter
            code += "       $('#" + this._tableId + "PreFilterFormAddBtn').click(function(){\n";
            code += "           var $clone = $($('#" + this._tableId + "PreFilterForm .smart-form .row-fluid')[0]).children().clone();\n";
            code += "           $($clone.children()[3]).remove(\"a\").end().append(\"<a class='btn btn-danger' href='javascript:void(0);' style='padding: 6px 12px;'>Remove</a>\").appendTo($('#" + this._tableId + "PreFilterForm .smart-form'));\n";
            code += "       });\n";

            code += "       $(\"#" + this._tableId + "PreFilter\").accordion({\n";
            code += "	        autoHeight : false,\n";
            code += "	        heightStyle : \"content\",\n";
            code += "	        collapsible : true,\n";
            code += "	        animate : 300,\n";
            code += "	        icons: { header: \"fa fa-plus\", activeHeader: \"fa fa-minus\"},\n";
            code += "	        header : \"h4\"\n";
            code += "       });\n";

            //Table
            code += "        var " + this._tableId + " = $('#" + this._tableId + "').DataTable({\n";
            code += "            \"sPaginationType\" : \"bootstrap\",\n";
            code += "            \"sDom\" : \"R<'dt-top-row'Clf>r<'dt-wrapper't><'dt-row dt-bottom-row'<'row'<'col-sm-6'i><'col-sm-6 text-right'p>>\",";
            code += "            \"bServerSide\": true,\n";
            code += "            \"sAjaxSource\": \"" + this._dataSource + "\",\n";
            code += "            \"fnServerData\": function ( sSource, aoData, fnCallback, oSettings ) {\n";
            code += "                       $('#" + this._tableId + "_processing').show();\n";
            
            if (this._parentTableId != null)
            {
                //logica para nao carregar a primeira vez a table filha
                code += "                   if($('#" + this._parentTableId + "').dataTable().$('tr.row_selected').length == 0){\n";
                code += "                       $('#" + this._tableId + "_processing').hide();\n";
                code += "                       return;\n";
                code += "                   }\n";

                //logica para quando o pai estiver selecionado, pegar o valor do joinfield e filtrar a grid filha
                code += "                       $( \"table[id$='ChildOf" + this._parentTableId + "']\" ).each(function(idx,dt){\n";
                code += "                           $($('#" + this._parentTableId + "').dataTable().fnGetNodes()).each(function(jdx,el){\n";
                code += "                               if ($(el).hasClass('row_selected')){\n";
                code += "                                   var valueofParentJoinField = $('#" + this._parentTableId + "').dataTable().fnGetData(jdx)." + this._parentJoinField + ";\n";
                code += "                                   aoData.push({ name : \"" + this._childJoinField + "\", value : valueofParentJoinField });\n";
                code += "                                   return false;\n";
                code += "                               }\n";
                code += "                           });\n";
                code += "                       });\n";
            }
            code += "               oSettings.jqXHR = $.ajax( {\n";
            code += "                   \"dataType\": 'json',\n";
            code += "                   \"type\": \"" + this._method + "\",\n";
            code += "                   \"url\": sSource,\n";
            code += "                   \"data\": aoData,\n";
            code += "                   \"success\": fnCallback\n";
            code += "               } );\n";
            code += "            },\n";
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
            code = code.Remove(code.Length - 2) + "\n"; // removendo a ultima virgula
            code += "            ],\n";
            code += "           \"aoColumnDefs\": [],\n";
            code += "            \"oLanguage\": {\n";
            code += "                \"sProcessing\": \"<div class='progress progress-striped active' style='width: 80%;margin: 0 auto;'><div class='progress-bar'  role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width: 100%'><span class='sr-only'>Processando</span></div></div>\"\n";
            code += "            },\n";
            code += "            \"fnDrawCallback\" : function(oSettings) {\n";
            
            //Script select row
            code += "               $(\"#" + this._tableId + " tbody tr\").click( function( e ) {\n";
            //code += "                   //$('[id=\"ChildOf" + this._parentTableId + "\"]').dataTable({\"fnServerParams\": function ( aoData ) {aoData.push(\"Id\" : 3)}}).fnDraw();\n";
            code += "                   if ( $(this).hasClass('row_selected') ) {\n";
            code += "                       $(this).removeClass('row_selected');\n";
            code += "                   }\n";
            code += "                   else {\n";
            code += "                       $('#" + this._tableId + "').dataTable().$('tr.row_selected').removeClass('row_selected');\n";
            code += "                       $(this).addClass('row_selected');\n";
            code += "                       //Fazendo filtrando o gripd pelo registro selecionado no pai\n";
            code += "                       $( \"table[id$='ChildOf" + this._tableId + "']\" ).each(function(idx,dt){\n";
            code += "                               var datatable = $(dt).dataTable();\n";
            code += "                               datatable.fnClearTable();\n";
            code += "                       });\n";
            code += "                   }\n";
            code += "               });\n";

            //Botão columns
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
                    code += "              width       : $(window).width(),\n";
                    code += "              height      : $(window).height(),\n";
                    code += "              close: function(event,ui){\n";
                    code += "                  dialog_" + d.DialogColumnItemId + ".remove()\n";
                    code += "                  button.children().removeClass('fa-refresh');\n";
                    code += "                  button.children().addClass('fa-gear');\n";
                    code += "                  button.children().removeClass('fa-spin');\n";
                    code += "                  button.removeClass('disabled');\n";
                    if (d.RefreshOnClose) code += "                  $('#" + this._tableId + "').dataTable().fnDraw(); //atualizando a datatable\n";
                    code += "              },\n";
                    code += "              position: ['center',20],\n";
                    code += "              modal: true\n";
                    code += "          });\n";
                    code += "          dialog_" + d.DialogColumnItemId + ".load(url+'/'+$(this).data('id'),\n";
                    code += "              function(responseText, textStatus,XMLHttpRequest){\n";
                    code += "                  $(\"<b>" + d.DialogTitle + "</b>\").appendTo($('.ui-dialog-titlebar .ui-dialog-title'))\n";
                    code += "              }\n";
                    code += "          );\n";
                    code += "          return false;\n";
                    code += "      });\n";
                }
                else if (this._listColumns[i] is ActionColumnItem)
                {
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
