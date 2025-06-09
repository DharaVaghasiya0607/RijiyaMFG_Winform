
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class PriDiscountDiffProperty
    {
        public Guid DISCOUNT_ID { get; set; }

        public string S_CODE { get; set; }
        public string C_CODE { get; set; }
        public string Q_CODE { get; set; }
        public string FL_CODE { get; set; }
        public string CUT_CODE { get; set; }
        public string POL_CODE { get; set; }
        public string SYM_CODE { get; set; }

        public string RAPDATE{ get; set; }

        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public double DISCOUNTDIFF { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
