using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class MajuriRateProperty
    {
        public Int32 MAJURI_ID { get; set; }

        public Int32 YYYY { get; set; }
        public Int32 MM { get; set; }

        public string SIZENAME { get; set; }
        public double FROMAMT { get; set; }
        public double TOAMT { get; set; }
        public double RATE { get; set; }

        public Int32 RATE_ID { get; set; }
        public string KAPANNAME { get; set; }
        public string RATETYPE { get; set; }
        public double MAJURIRATE { get; set; }

        public string MEMOREPORTNOTE { get; set; }

        public double OLDVALUE { get; set; }
        public double NEWVALUE { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
