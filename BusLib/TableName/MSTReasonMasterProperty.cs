using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class MSTReasonMasterProperty
    {
        public Int32 REASON_ID { get; set; }

        public string REASONCODE { get; set; }
        public string REASONNAME { get; set; }
        
        public Int32 SEQUENCENO{ get; set; }
        public string REMARK { get; set; }
        
        public bool ISACTIVE { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
