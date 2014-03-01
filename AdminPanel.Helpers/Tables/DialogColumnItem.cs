using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Tables
{
    public class DialogColumnItem : ColumnItem
    {
        private string ButtonText;
        public string Url { get; set; }
        public string DialogColumnItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActionColumnItemId">Nesse caso ele é usado pra definir a class(css) do elemento, pois como esse botão existirá em cada linha não pdoe ser ID. É através dessa CLASS que é definido o handler para a ação.</param>
        /// <param name="ColData">Campo do model que será enviado para a chamada AJAX</param>
        /// <param name="ColName">Identificador que ficará no header da tabela</param>
        /// <param name="ButtonText">Texto do botão</param>
        /// <param name="Url">Url para açao (ajax)</param>
        public DialogColumnItem(string DialogColumnItemId, string ColData, string ColName, string ButtonText, string Url, int Width = -1)
            : base(ColData, ColName, false, true, ColumnType.String, Width)
        {
            this.DialogColumnItemId = DialogColumnItemId;
            this.ButtonText = ButtonText;
            this.Url = Url;
        }

        public override string ToString()
        {
            string code = "";

            code += "               {\n";
            code += "                   \"bSortable\" : false,\n";
            code += "                   \"sTitle\":\"" + this.ColumnName + "\",\n";
            code += "                   \"mData\": \"" + this.ColumnData + "\",\n";
            if (this.Width != -1)
                code += "                   \"sWidth\":\"" + this.Width + "px\",\n";
            code += "                   fnCreatedCell: function (nTd, sData, oData, iRow, iCol) {\n";
            code += "                       $(nTd).html(\"<a data-id='\"+sData+\"' class='btn btn-default btn-xs " + DialogColumnItemId + "'><i class='fa fa-gear fa-lg'></i> " + ButtonText + "</a>\");\n";
            code += "                   }\n";
            code += "               }";
            return code;
        }
    }
}
