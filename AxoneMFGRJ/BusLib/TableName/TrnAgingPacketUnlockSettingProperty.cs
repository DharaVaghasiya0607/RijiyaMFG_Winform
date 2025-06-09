using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnAgingPacketUnlockSettingProperty
    {
        public Guid UNLOCK_ID { get; set; }

        public string UNLOCKTYPE { get; set; }
        public string UNLOCKDATE{ get; set; }

        public Guid KAPAN_ID { get; set; }
        public Guid PACKET_ID { get; set; }

        public string KAPANNAME { get; set; }
        public Int32 PACKETNO { get; set; }
        public string TAG { get; set; }

        public Int64 EMPLOYEE_ID { get; set; }
        public Int64 MANAGER_ID { get; set; }

        public Int32 DEPARTMENT_ID { get; set; }
        public Int32 PROCESS_ID { get; set; }
       
        public string REMARK { get; set; }

        public Guid TRN_ID { get; set; }
        public Guid TRNAGING_ID { get; set; }

        public double LASTFLAGCARAT { get; set; }
        public double LASTAGINGCARAT { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }

}
