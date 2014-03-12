using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Entity.Models.ClientSideErrors
{
    public class ClientSideJavaScriptException : EntityBase
    {
        public int ClientSideJavaScriptExceptionId { get; set; }
        public string ErrorMsg { get; set; }
        public string Url { get; set; }
        public string Line { get; set; }
        public string Uri { get; set; }
        public string Screenshot { get; set; }

        private DateTime _dataReg;
        public DateTime DataReg
        {
            get
            {
                if (_dataReg == null || _dataReg == DateTime.MinValue)
                    _dataReg = DateTime.Now;
                return _dataReg;
            }
            set
            {
                _dataReg = value;
            }
        }
    }
}
