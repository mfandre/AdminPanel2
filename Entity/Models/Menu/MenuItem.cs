using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Entity.Models.Menu
{
    public class MenuItem : EntityBase
    {
        public int MenuItemId {get; set;}
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool Leaf { get; set; }
        public bool Root { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual MenuItem Parent { get; set; }

        public virtual List<MenuItem> Children { get; set; }

        private DateTime _dataReg;
        public DateTime DataReg
        {
            get
            {
                if (_dataReg == null)
                    _dataReg = DateTime.Now;
                return _dataReg;
            }
            set
            {
                _dataReg = value;
            }
        }

        private string _estReg;
        public string EstReg
        {
            get
            {
                if (_estReg == null || _estReg == "")
                    _estReg = "A";
                return _estReg;
            }
            set
            {
                if (value == null || value == "")
                    _estReg = "A";
                _estReg = value;
            }
        }
    }
}
