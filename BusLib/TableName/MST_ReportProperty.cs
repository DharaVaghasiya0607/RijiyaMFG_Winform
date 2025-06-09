using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class MST_ReportProperty
    {
        public Int32 COSTCENTER_ID { get; set; }
        public string SHORTNAME { get; set; }
        public string COSTCENTERNAME { get; set; }

        public Int32 REPORT_ID { get; set; }
        public Int32 SRNO { get; set; }
        public string REPORTGROUP { get; set; }
        public string REPORTGROUPNEW { get; set; }

        public string REPORTNAME { get; set; }
        public string FORMNAME { get; set; }
        public Int32 SEQUENCENO { get; set; }
        public string SPNAME { get; set; }
        public string REPORTVIEW { get; set; }
        public string DISPLAYFONTNAME { get; set; }
        public double DISPLAYFONTSIZE { get; set; }
        public string PRINTFONTNAME { get; set; }
        public double PRINTFONTSIZE { get; set; }
        public string PRINTORIENTATION { get; set; }

        public bool ISACTIVE { get; set; }

        public bool ISPRINTFIRMNAME { get; set; }
        public bool ISPRINTFIRMADDRESS { get; set; }
        public bool ISPRINTFILTERCRITERIA { get; set; }
        public bool ISPRINTHEADINGONEACHPAGE { get; set; }
        public bool ISPRINTDATETIME { get; set; }

        public string REMARK { get; set; }

        public string XMLDATA { get; set; }
        public string XMLDATAGROUP { get; set; }

        public string FROMCOLLECTIONDATE { get; set; }
        public string TOCOLLECTIONDATE { get; set; }
        public string FROMCHEQUEDATE { get; set; }
        public string TOCHEQUEDATE { get; set; }

        public string REPORTTYPE { get; set; }
        public string COMPANY_ID  { get; set; }
        public string LOCATION_ID { get; set; }

        public string STOCKTYPE { get; set; }
        public string KAPAN_ID  { get; set; }
        public string KAPANNAME { get; set; }
        public string PRICEDATE { get; set; }
        
        public string FROMPROCESS_ID  { get; set; }
        public string TOPROCESS_ID { get; set; }
        public string NEXTPROCESS_ID { get; set; }
        
        public string FROMDEPARTMENT_ID  { get; set; }
        public string TODEPARTMENT_ID  { get; set; }

        public string FROMEMPLOYEE_ID { get; set; }
        public string TOEMPLOYEE_ID { get; set; }
        
        public string FROMMANAGER_ID { get; set; }
        public string TOMANAGER_ID { get; set; }
        
        public string FROMFACTORYHEAD_ID { get; set; }
        public string TOFACTORYHEAD_ID { get; set; }

        public string KAPANMANAGER_ID { get; set; }

        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string GROUPBY { get; set; }

        public string LOT_ID { get; set; }
        public string BARCODE { get; set; }
        public string PACKETNO { get; set; }
        public string JANGEDNO  { get; set; }

        public string RETURNVALUE { get; set; }
        public string RETURNMESSAGETYPE { get; set; }
        public string RETURNMESSAGEDESC { get; set; }

        public string PARTY_ID { get; set; }

        public string SUPPLIER_ID { get; set; }

        public string BROCKER_ID { get; set; }

        public string MSIZE_ID { get; set; }

        public string MINES { get; set; }

        public string ARTICLE { get; set; }

        public string ROUGH { get; set; }

        public string FROMRECEIVEDATE { get; set; }

        public string TORECEIVEDATE { get; set; }

        public string FROMINVOICEDATE { get; set; }

        public string TOINVOICEDATE { get; set; }


        public string TABLENAME { get; set; } 


        public string SHIFTTYPE { get; set; }
        public string PacketGroup { get; set; }
        public string PacketCategory { get; set; }
        public int FromPacketNo { get; set; }
        public int ToPacketNo { get; set; }
        public string KapanStatus { get; set; }
        public string KapanCreateFromDate { get; set; }
        public string KapanCreateToDate { get; set; }
        public string ClvIssueFromDate { get; set; }
        public string ClvIssueToDate { get; set; }
        public string ClvCompleteFromDate { get; set; }
        public string ClvCompleteToDate { get; set; }
        public string MFGIssueFromDate { get; set; }
        public string MFGIssueToDate { get; set; }
        public string MFGCompleteFromDate { get; set; }
        public string MFGCompleteToDate { get; set; }
        public string PolishReceiveFromDate { get; set; }
        public string PolishReceiveToDate { get; set; }
        public string MumbaiReceiveFromDate { get; set; }
        public string MumbaiReceiveToDate { get; set; }

        public string DIAMONDTYPE { get; set; } //DHARA : 26-03-2024

        public string ROUGHCODE { get; set; } // DHARA : 10-10-2024
        public string TAG { get; set; } // DHARA : 11-10-2024

    }

}
