﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Entity.Models
{
    public class Cliente : EntityBase
    {
        public int ClienteId { get; set; }
        public string Name { get; set; }
        public string Localidade { get; set; }

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

        public virtual List<Sde> ServidoresSde { get; set; }
        public virtual List<ArcgisServer> ArcgisServers { get; set; }

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
