using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class KapanValuationProperty
    {
        public double LABRATE {get;set;}
        public double OTHERMFGEXP { get; set; }
        public double OTHEREXPENSEAMT { get; set; }
        public double POLISHAVG { get; set; }
        public double POLISHCONVERSIONRATE { get; set; }
        public double LOSTCONVERSIONRATE { get; set; }
        public string POLISHRECVDATE { get; set; }
        public string GHATCOMPLETEDATE { get; set; }
        public string MFGISSUEDATE { get; set; }
        public string COMPLETEDATE { get; set; }
        public string MUMBAIRECVDATE { get; set; }
        public string CLVCOMPLETEDATE { get; set; }
        public string CLVISSUEDATE { get; set; }
        public Int64 LOT_ID { get; set; }
        public Int64 KAPAN_ID { get; set; }

        public int POLISHISSUEPCS{get;set;}
        public double POLISHISSUECARAT {get;set;}
        public int POLISHREADYPCS {get ; set ; }
        public double POLISHREADYCARAT {get ; set ;}
        public int MKBLPC {get;set;}
        public double MKBLCARAT {get;set;}
        public double MKBLPER {get;set;}
        public double MKBLSIZE {get ; set ;}
        public int GALAXYISSUEPC {get;set;}
        public double GALAXYLABOUR {get;set;}
        public double QCLABOUR {get;set;}
        public double SARINLABOUR {get;set;}
        public double CLVWEIGHTLOSS { get; set; }

        public double KAPANCARAT { get; set; }
        public double KAPANRATE { get; set; }
        public double KAPANAMTRS { get; set; }
        public double KAPANAMTDOLLAR { get; set; }
        public double EXPPOLPER { get; set; }
        public double EXPMAKPER { get; set; }
        public double EXPDOLLAR { get; set; }
        public double EXPMAKCARAT { get; set; }
        public double EXPPOLCARAT { get; set; }
        public string REMARK { get; set; }

        public double POLISHAVGRS { get; set; }

        public double SIZE { get; set; }
        public int KACHAPCS { get; set; }
        public double GHATRECIEVECARAT { get; set; }
        public double KACHACARAT { get; set; }

        public double PADTAR { get; set; }
        public double PADTARAMT { get; set; }

        public double OUTCARAT { get; set; }
        public double OUTAVG { get; set; }
        public double OUTAMOUNT { get; set; }        
        public int OUTPCS { get; set; }
        public double OUTREJECTIONCARAT { get; set; }
        public double KAPANTOTALAMOUNT { get; set; }
        public double POLISHAMOUNT { get; set; }
        public double GALAXYAMOUNT { get; set; }

        public double MUMBAIAVG { get; set; }
        public double MUMBAICNVRATE { get; set; }
        public double MUMBAIAMOUNT { get; set; }
        public double PADTARAVG { get; set; }
        

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
