using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AdminPanel.Helpers.Forms;

namespace AdminPanel.Helpers.HelpHints
{
    public static class TutorialHelper
    {
        /// <summary>
        /// Cria um tutorial da tela para o usuário
        /// </summary>
        /// <param name="Html"></param>
        /// <returns></returns>
        public static Tutorial Tutorial(this HtmlHelper Html)
        {
            return new Tutorial(Html);
        }
    }

    public class Tutorial : BaseHelper
    {
        List<SingleHelpHint> _listOfHints = new List<SingleHelpHint>();

        public Tutorial(HtmlHelper Helper)
            : base(Helper)
        {
        }

        public Tutorial addHints(string SelectorElement, string Message, SingleHelpHintPosition Position = SingleHelpHintPosition.bottom)
        {
            this._listOfHints.Add(new SingleHelpHint(SelectorElement, this._listOfHints.Count, Message, Position));
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("function startIntro(){");
            sb.Append("     var intro = introJs();");
            sb.Append("     intro.setOptions({");
            sb.Append("         steps: [");
            foreach (SingleHelpHint shh in this._listOfHints)
            {
                sb.Append(shh);
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1,1);
            sb.Append("         ]");
            sb.Append("     });");
            sb.Append("     intro.start();");//.onbeforechange(function(targetElement) {$(targetElement).show();}); 
            sb.Append("}");
            sb.Append("</script>");
            return sb.ToString();
        }

        protected override string Html()
        {
            return "";
        }

        protected override string Script()
        {
            return this.ToString();
        }
    }
}
