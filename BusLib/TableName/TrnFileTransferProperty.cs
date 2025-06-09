using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnFileTransferProperty
    {

        public string ID { get; set; }
        public int PROCESS_ID { get; set; }
        public string PROCESSNAME { get; set; }
        public string KAPANNAME { get; set; }
        public int PACKETNO { get; set; }
        public string TAG { get; set; }
        public Guid PACKET_ID { get; set; }
        public Int64 EMPLOYEE_ID { get; set; }
        public string EMPLOYEECODE { get; set; }
        public string FILENAME { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
