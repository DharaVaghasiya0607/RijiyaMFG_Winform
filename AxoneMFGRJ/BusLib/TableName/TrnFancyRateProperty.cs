using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnFancyRateProperty
    {
        public Int64 DETAIL_ID { get; set; }
        public Guid SIZEASSROT_ID { get; set; }
        public Guid INWARD_ID { get; set; }

        public Int32 SHAPE_ID { get; set; }

        public Int64 KAPAN_ID { get; set; }
        public string KAPANNAME { get; set; }

        public string KAPANGROUP { get; set; }
        public string LOTGROUP { get; set; }

        public Int32 SIZE_ID { get; set; }
        public string SIZENAME { get; set; }

        public double RATE { get; set; }
        public double AMOUNT { get; set; }
        public double CARAT { get; set; }
        
        public string REMARK { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

    }

}
