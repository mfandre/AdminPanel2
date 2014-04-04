using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.HelpHints
{
    public enum SingleHelpHintPosition
    {
        bottom,
        top,
        left,
        right
    }

    public class SingleHelpHint 
    {
        /// <summary>
        /// Exemplo: .Class ||  #Id
        /// </summary>
        private string _elementSelector;

        /// <summary>
        /// Menssagem de help
        /// </summary>
        private string _message;

        /// <summary>
        /// Ordem de visualização
        /// </summary>
        private int _step;

        private SingleHelpHintPosition _position;

        public SingleHelpHint(string ElementSelect, int Step, string Message, SingleHelpHintPosition Position)
        {
            this._elementSelector = ElementSelect;
            this._message = Message;
            this._step = Step;
            this._position = Position;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("element: \"" + this._elementSelector + "\",");
            sb.Append("intro: \"" + this._message + "\",");
            sb.Append("position: '" + this._position + "'");
            sb.Append("}");

            return sb.ToString();
        }
    }
}
