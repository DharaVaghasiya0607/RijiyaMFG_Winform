using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class MSTPrdTypeMasterProperty
    {
        public Int32 PRDTYPE_ID { get; set; }
        public string PRDTYPENAME  { get; set; }
        public string PRDTYPECODE { get; set; }
        public bool ISKAPAN { get; set; }
        public bool ISPACKETNO { get; set; }
        public bool ISTAG { get; set; }
        public bool ISEMPLOYEE	 { get; set; }
        public bool ISMANAGER { get; set; }
        public bool ISGRAPH	 { get; set; }
        public bool ISEXP	 { get; set; }
        public bool ISMAK	 { get; set; }
        public bool ISPOL	 { get; set; }
        public bool TFLAG { get; set; }
        public int SEQUENCENO { get; set; }
        public bool ISACTIVE	 { get; set; }
        public string REMARK { get; set; }
        public string REQPRDTYPE_ID { get; set; }
		public string DESIGNATION_ID { get; set; }

        public double RAPCALCPER { get; set; }

        public string PARAMCHECKPRDTYPE_ID { get; set; }
        public string PARAMCHECKBREAKINGTYPE_ID { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
