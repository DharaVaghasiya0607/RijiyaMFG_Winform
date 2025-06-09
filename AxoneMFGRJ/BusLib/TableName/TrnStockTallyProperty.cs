using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnStockTallyProperty
    {

        public Int64 STOCKTALLY_ID { get; set; }
        public string STOCKTALLYDATE { get; set; }
        public Int64 PACKET_ID { get; set; }
        public string KAPANNAME { get; set; }
        public int PACKETNO { get; set; }
        public Int64 EMPLOYEE_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int PROCESS_ID { get; set; }
        public string TAG { get; set; }
        public double CARAT { get; set; }
        public Int64 TRN_ID { get; set; }
        public string FOUNDSTATUS { get; set; }
        public string BARCODE { get; set; }
        public int PKTSRNO { get; set; }

        public int SCANDEPARTMENT_ID { get; set; }
        public Int64 MANAGER_ID { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
