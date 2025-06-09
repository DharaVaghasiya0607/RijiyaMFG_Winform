using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class LabExpenseMasterProperty
    {
        public Int32 LABEXPENSE_ID { get; set; }
        public string RAPDATE { get; set; }
        public string PARAMETERTYPE { get; set; }
        public string PARAMETERVALUE { get; set; }

        public double FROMCARATOLD { get; set; }
        public double TOCARATOLD { get; set; }

        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public double RATE { get; set; }

        public string SIZENAME { get; set; }

        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
