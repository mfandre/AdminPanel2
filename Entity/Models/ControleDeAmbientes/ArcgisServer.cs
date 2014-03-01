using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Entity.Models
{
    public class ArcgisServer : EntityBase
    {
        public int ArcgisServerId { get; set; }

        public Cliente Cliente { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Url { get; set; }

        /// <summary>
        /// Examples: 10, 10.1, 9.3...
        /// </summary>
        public string ArcgisVersion { get; set; }

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
