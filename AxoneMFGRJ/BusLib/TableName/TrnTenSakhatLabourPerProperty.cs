using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnTenSakhatLabourPerProperty
    {
        public Guid LABOUR_ID { get; set; }
        public int YY { get; set; }
        public int MM { get; set; }
        public double PER { get; set; }
        public string LABOURTYPE { get; set; }
        public int LABOURTYPE_ID { get; set; }
        public int LABOUR_SRNO { get; set; }
        public string LABOURTYPENAME { get; set; }
               
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
