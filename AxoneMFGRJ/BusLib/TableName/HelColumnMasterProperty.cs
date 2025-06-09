using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class HelColumnMasterProperty
    {
        public Int32 ID { get; set; }
        public string COLNAME { get; set; }
        public string DATATYPE { get; set; }
        public bool ISACTIVE { get; set; }
       
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
