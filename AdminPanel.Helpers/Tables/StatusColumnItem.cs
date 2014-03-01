using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Tables
{
    public enum StatusColumnType
    {
        Success,
        Default,
        Warning,
        Important
    }


    public class StatusColumnItem : ColumnItem
    {
        StatusColumnType _Status;

        public StatusColumnItem(string ColData, string ColName,StatusColumnType Status, bool Fxd = false, bool Vsbl = true) : base(ColData, ColName,Fxd,Vsbl,ColumnType.String)
        {
            this._Status = Status;
            this.Template = "\"<span class='label label-" + Status.ToString().ToLower() + "'>\"+sData+\"</span>\"";
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
