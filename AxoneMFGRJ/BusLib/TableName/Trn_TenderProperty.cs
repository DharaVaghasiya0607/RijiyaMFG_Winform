using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class Trn_TenderProperty
    {
        
        public string Rough_ID { get; set; }
        public string Lot_ID { get; set; }

        public int PacketNo { get; set; }
        public string RoughName { get; set; }
        public double MainCarat { get; set; }

        public Int32 Size_ID { get; set; }

        public string TenderDate { get; set; }
        public string TenderName { get; set; }
        public string LotNo { get; set; }

        public string Note { get; set; }
        public string CHECKBY { get; set; }

        public double IMPDOLLARRATE { get; set; }
        public double EXPDOLLARRATE { get; set; }
        public double ROUGHIMPORTPER { get; set; }
        public double ROUGHIMPORTAMOUNT { get; set; }
        public double PROFITPER { get; set; }
        public double PROFITAMOUNT { get; set; }
        public double LABOURPER { get; set; }
        public double LABOURAMOUNT { get; set; }

        public double FINALAMOUNT { get; set; }
        public double FINALAVG { get; set; }
        public string RAPDATE { get; set; }

        public string XmlDetail { get; set; }
        public double RoughCarat { get; set; }
        public double PolishCarat { get; set; }
        public double RoughAmount { get; set; }
        public double PolishAmount { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

    }
}
