using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnLabourPcsProperty
    {
        public Guid ID { get; set; }


        public int YY { get; set; }
        public int MM { get; set; }
        public Int32 FROMPCS { get; set; }
        public Int32 TOPCS { get; set; }
        public double PER { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
