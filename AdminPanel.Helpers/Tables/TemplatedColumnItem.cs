using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Tables
{
 
    public class TemplatedColumnItem : ColumnItem
    {
        /// <summary>
        /// É obrigatorio ter o {data} dentro do template por ele será substituido pelo valor da tabela
        /// </summary>
        /// <param name="ColData"></param>
        /// <param name="ColName"></param>
        /// <param name="Template">Example: "<i>{data}</i>" <b>{data}</b></param>
        /// <param name="Fxd"></param>
        /// <param name="Vsbl"></param>
        public TemplatedColumnItem(string ColData, string ColName, string Template, bool Fxd = false, bool Vsbl = true)
            : base(ColData, ColName, Fxd, Vsbl, ColumnType.String)
        {
            Template = Template.Replace("\"", "'");
            Template = Template.Replace("{data}","\"+sData+\"");
            this.Template = "\"" + Template + "\"";
        }

        public override string ToString()
        {
            string code = "";
            code += "               {\n";
            code += "                   \"sTitle\":\"" + this.ColumnName + "\",\n";
            code += "                   \"mData\": \"" + this.ColumnData + "\",\n";
            if (this.Width != -1)
                code += "                   \"sWidth\":\"" + this.Width + "px\",\n";
            code += "                   fnCreatedCell: function (nTd, sData, oData, iRow, iCol) {\n";
            code += "                       $(nTd).html(" + this.Template + ");\n";
            code += "                   }\n";
            code += "               }";
            return code;
        }
    }




}
