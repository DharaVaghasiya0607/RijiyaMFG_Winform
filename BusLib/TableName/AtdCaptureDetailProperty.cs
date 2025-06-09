using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class AtdCaptureDetailProperty
    {
        public Int64 EMP_ID { get; set; }

        public string EMPDEVICECODE { get; set; }
        public string EMPCODE { get; set; }
        public string LOGDATE { get; set; }
        public string LOGTIME { get; set; }
        public string LOGDATETIME { get; set; }
        public string RFID { get; set; }
        public string EMPNAME { get; set; }

        public string PUCHDERACTION { get; set; }
        public bool ISADDMANUALPUNCH { get; set; } 


        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }




    }

}
