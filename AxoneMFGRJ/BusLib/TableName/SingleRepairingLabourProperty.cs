using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class SingleRepairingLabourProperty
    {
        public Guid REPLABOUR_ID { get; set; }
        public string REPDATE { get; set; }

        public Guid KAPAN_ID { get; set; }
        public string KAPANNAME { get; set; }

        public Guid PACKET_ID { get; set; }
        public int PACKETNO { get; set; }
        public string TAG { get; set; }

        public double ISSUECARAT { get; set; }

        public int SHAPE_ID { get; set; }
        public int RCOLOR_ID { get; set; }
        public int RCLARITY_ID { get; set; }
        public int RCUT_ID { get; set; }
        public int RPOL_ID { get; set; }
        public int RSYM_ID { get; set; }
        public int RFL_ID { get; set; }

        public double RPRICEPERCARAT { get; set; }
        public double RAMOUNT { get; set; }

        public double POLISHCARAT { get; set; }

        public int PCOLOR_ID { get; set; }
        public int PCLARITY_ID { get; set; }
        public int PCUT_ID { get; set; }
        public int PPOL_ID { get; set; }
        public int PSYM_ID { get; set; }
        public int PFL_ID { get; set; }

        public double PPRICEPERCARAT { get; set; }
        public double PAMOUNT { get; set; }

        public Int64 EMPLOYEE_ID { get; set; }

        public double LABOURRATE { get; set; }
        public double LABOURAMOUNT { get; set; }

        public double LABOURPER { get; set; }

        public string REMARK { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}


