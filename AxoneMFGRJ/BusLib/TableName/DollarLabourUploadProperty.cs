using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public  class DollarLabourUploadProperty
    {
        public int LABOUR_ID { get; set; }
        public Int32 YYYY { get; set; }
        public Int32 MM { get; set; }
        public Int32 SHAPE_ID { get; set; }
        public Int32 SIZE_ID { get; set; }
        public Int32 CUT_ID { get; set; }
        public Int32 SYM_ID { get; set; }
        public Int32 POL_ID { get; set; }
        public double RATE { get; set; }
        public double BONUSPER { get; set; }
        public string LABOURTYPE { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }
}
