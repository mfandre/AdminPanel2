using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Helpers.Buttons.Behaviors
{
    public abstract class IButtonBehavior
    {
        public abstract string DoBehavior();
        public string ButtonId { get; set; }
    }
}
