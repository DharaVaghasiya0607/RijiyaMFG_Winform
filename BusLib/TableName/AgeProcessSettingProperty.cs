using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class AgeProcessSettingProperty
    {
        public Guid @AGINGPROCESS_ID { get; set; }

        public string KAPANNAME { get; set; }
        public int @PROCESS_ID { get; set; }
        public double  FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public double @AGINGMINUTE { get; set; }
        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
