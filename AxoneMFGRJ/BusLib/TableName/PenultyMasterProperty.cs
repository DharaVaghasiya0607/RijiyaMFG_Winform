using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class PenultyMasterProperty
    {
        public Int32 POINT_ID { get; set; }
        public string POINTNAME { get; set; }
        public Int32 NOOFPCS { get; set; }
        public double RATE { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
