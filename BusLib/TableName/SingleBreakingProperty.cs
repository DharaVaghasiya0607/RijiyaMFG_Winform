using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class SingleBreakingProperty
    {
        public Guid BREAKING_ID { get; set; }

        public int BREAKINGTYPE_ID { get; set; }
        public string BREAKINGDATE { get; set; }

        public Guid KAPAN_ID { get; set; }
        public string KAPANNAME { get; set; }

        public Guid PACKET_ID { get; set; }
        public int PACKETNO { get; set; }
        public string TAG { get; set; }

        public Int64 EMPLOYEE_ID { get; set; }

        public double BFCARAT { get; set; }
        public int BFSHAPE_ID { get; set; }
        public int BFCOLOR_ID { get; set; }
        public int BFCLARITY_ID { get; set; }
        public int BFCUT_ID { get; set; }
        public int BFPOL_ID { get; set; }
        public int BFSYM_ID { get; set; }
        public int BFFL_ID { get; set; }
        public string BFRAPDATE { get; set; }
        public double BFRAPAPORT { get; set; }
        public double BFDISCOUNT { get; set; }
        public double BFPRICEPERCARAT { get; set; }
        public double BFAMOUNT { get; set; }


        public double AFCARAT { get; set; }
        public int AFSHAPE_ID { get; set; }
        public int AFCOLOR_ID { get; set; }
        public int AFCLARITY_ID { get; set; }
        public int AFCUT_ID { get; set; }
        public int AFPOL_ID { get; set; }
        public int AFSYM_ID { get; set; }
        public int AFFL_ID { get; set; }
        public string AFRAPDATE { get; set; }
        public double AFRAPAPORT { get; set; }
        public double AFDISCOUNT { get; set; }
        public double AFPRICEPERCARAT { get; set; }
        public double AFAMOUNT { get; set; }

        public double PENALTYAMOUNT { get; set; }
        public string REMARK { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
