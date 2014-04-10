using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Buttons.Behaviors
{
    public class RefreshDatatableBehavior : IButtonBehavior
    {
        private string DatatableId;

        /// <summary>
        /// Ao ser clicado da um refresh nos dados da datatable cujo Id foi passado no construtor
        /// </summary>
        /// <param name="DatatableId"></param>
        public RefreshDatatableBehavior(string DatatableId)
        {
            this.DatatableId = DatatableId;
        }

        public override string DoBehavior()
        {
            string code = "";

            code += "<script>\n";
            code += "$(document).ready(function() {\n";
            code += "   $('#" + ButtonId + "').click(function(){\n";
            code += "           $('#" + this.DatatableId + "').dataTable().fnDraw(); //atualizando a datatable\n";
            code += "   });\n";
            code += " });\n";
            code += "</script>\n";
            return code;
        }
    }
}
