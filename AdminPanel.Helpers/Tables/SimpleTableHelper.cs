using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.Tables
{
    public static class SimpleTableHelper
    {
        public static SimpleTable SimpleTable(this HtmlHelper Html, string TableId, string DataSource)
        {
            return new SimpleTable(Html, TableId, DataSource);
        }

        public static SimpleTable SimpleTable(this HtmlHelper Html, string TableId)
        {
            return new SimpleTable(Html, TableId);
        }
    }

    public class SimpleTable : BaseHelper
    {
        private HtmlHelper Html;
        private string TableId;
        private string DataSource;
        private List<List<string>> Data = new List<List<string>>();
        private List<string> Columns = new List<string>();

        public SimpleTable(HtmlHelper Html, string TableId) : base (Html)
        {
            // TODO: Complete member initialization
            this.Html = Html;
            this.TableId = TableId;
        }

        public SimpleTable(HtmlHelper Html, string TableId, string DataSource) : base (Html)
        {
            // TODO: Complete member initialization
            this.Html = Html;
            this.TableId = TableId;
            this.DataSource = DataSource;
        }

        public SimpleTable AddData(List<string> values)
        {
            Data.Add(values);
            return this;
        }

        public SimpleTable AddColumn(string name)
        {
            Columns.Add(name);
            return this;
        }

        public override string ToString()
        {
            if(Columns.Count != Data[0].Count)
                throw new Exception("numero de colunas diferente da quantidade de elementos da lista de dados");
            string code = "";

            code += "<table id=\"user\" class=\"table table-bordered table-striped\" style=\"clear: both\">\n";
            if (Columns.Count > 0)
            {
                code += "   <thead>\n";
                foreach (string col in Columns)
                {
                    code += "       <th>" + col + "</th>\n";
                }
                code += "   </thead>\n";
            }
            code += "   <tbody>\n";
            foreach (List<string> row in Data)
            {
                code += "       <tr>\n";
                for (int i = 0; i < Columns.Count; i++)
                {
                    code += "           <td>" + row[i] + "</td>\n";
                }
                code += "</tr>\n";
            }
			code += "   </tbody>\n";
            code += "</table>\n";

            return code;
        }

        
    }
}
