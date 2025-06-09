using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnInwardProperty
    {
        
        public Int64  Inward_ID	 { get; set; }
        public string InwardType	 { get; set; }
        public string FinYear	 { get; set; }
        public int InwardNo		 { get; set; }
        public string InwardDate		 { get; set; }
        public Int64 Ledger_ID		 { get; set; }
        public Int64 Item_ID		 { get; set; }
        public string ChallanNo		 { get; set; }
        public string DesignNo		 { get; set; }
        public int Pcs		 { get; set; }
        public double RatePerPcs		 { get; set; }
        public double GrossAmount		 { get; set; }
        public string Remark		 { get; set; }
        public string EntryDate		 { get; set; }
        public Int64 EntryBy		 { get; set; }
        public string EntryIP		 { get; set; }
        public string ReturnDate		 { get; set; }
        public int ReturnPcs		 { get; set; }
        public int DamagePcs		 { get; set; }
        public double PendingAmount { get; set; }
	
        public string ReturnValueTrnID { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
