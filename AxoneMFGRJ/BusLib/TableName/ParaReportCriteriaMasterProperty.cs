using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class ParaReportCriteriaMasterProperty
    {
        public Int32 CRITERIA_ID { get; set; }

        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }

        public Int32 FROMSHAPE_ID { get; set; }
        public Int32 TOSHAPE_ID { get; set; }

        public Int32 FROMCOLOR_ID { get; set; }
        public Int32 TOCOLOR_ID { get; set; }

        public Int32 FROMCLARITY_ID { get; set; }
        public Int32 TOCLARITY_ID { get; set; }

        public Int32 FROMCUT_ID { get; set; }
        public Int32 TOCUT_ID { get; set; }

        public string REMARK { get; set; }
        public bool ISACTIVE { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
