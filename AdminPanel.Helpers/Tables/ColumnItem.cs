using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Tables
{
    public enum ColumnType
    {
        String,
        Int,
        Decimal,
        Date,
        DateTime
    }

    public class ColumnItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColData">Attributo do objeto que será renderizado nessa colua</param>
        /// <param name="ColName">Texto do Header</param>
        /// <param name="fxd">Coluna será fixa?</param>
        /// <param name="vsbl">Coluna será visivel?</param>
        /// <param name="Tplt">Template que será usado para renderizar cada row dessa coluna</param>
        public ColumnItem(string ColData, string ColName, ColumnType ColumnType, bool Fxd = false, bool Vsbl = true, string Tplt = "", int Width = -1)
        {
            this.ColumnName = ColName;
            this.ColumnData = ColData;
            this.Fixed = Fxd;
            this.Visible = Visible;
            this.Template = Tplt;
            this.Type = ColumnType;
            this.Width = Width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColData">Attributo do objeto que será renderizado nessa colua</param>
        /// <param name="ColName">Texto do Header</param>
        /// <param name="fxd">Coluna será fixa?</param>
        /// <param name="vsbl">Coluna será visivel?</param>
        /// <param name="ColumnType">Formato tipo da coluna</param>
        /// <param name="Width">comprimento da coluna</param>
        public ColumnItem(string ColData, string ColName, bool Fxd, bool Vsbl, ColumnType ColumnType, int Width = -1)
        {
            this.ColumnName = ColName;
            this.ColumnData = ColData;
            this.Fixed = Fxd;
            this.Visible = Visible;
            this.Type = ColumnType;
            this.Width = Width;
        }

        public ColumnType Type { get; set; }

        public string ColumnName { get; set; }

        public string Template { get; set; }

        public string ColumnData { get; set; }
        
        public bool Fixed {get; set;}

        public bool Visible { get; set; }

        public int Width { get; set; }

        public override string ToString()
        {
            string code = "";
            code += "               {\n"; 
            code += "                   \"sTitle\":\"" + this.ColumnName + "\",\n";
            if (this.ColumnData == null)
                code += "                   \"mData\": null,\n";
            else
                code += "                   \"mData\": \"" + this.ColumnData + "\",\n";
            if(this.Width != -1)
                code += "                   \"sWidth\":\"" + this.Width + "px\",\n";
            code += "                   fnCreatedCell: function (nTd, sData, oData, iRow, iCol) {\n";
            //formatando os valores da coluna de acordo com o tipo
            if (this.Type == ColumnType.DateTime || this.Type == ColumnType.Date)
            {
                code += "                       var dx = new Date(parseInt(sData.substr(6)));\n";
                code += "                       var dd = dx.getDate();var mm = dx.getMonth() + 1;var yy = dx.getFullYear();";
                code += "                       if (dd <= 9)dd = \"0\" + dd;\n";
                code += "                       if (mm  <= 9)mm = \"0\" + mm;\n";
                code += "                       $(nTd).html(dd + \"/\" + mm + \"/\" + yy);\n";
            }
            code += "                   }\n";
            code += "               }";
            return code;
        }

    }
}
