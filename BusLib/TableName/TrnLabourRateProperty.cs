using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnLabourRateProperty
    {
        public Int32 PROCESS_ID { get; set; }
        public Int32 CHARNI_ID { get; set; }
        
        public string LABOURTYPE { get; set; }
        public double LABOURRATE { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
