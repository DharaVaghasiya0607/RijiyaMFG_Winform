using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnSingleIssueReturnProperty
    {
        public Int64 TRN_ID { get; set; }
        public Int64 OLDTRN_ID { get; set; }
        public Int64 COMPANY_ID { get; set; }
        public Int64 KAPAN_ID { get; set; }
        public Int64 PACKET_ID { get; set; }
        public Int64 MAINPACKET_ID { get; set; }
        public Int64 JANGEDNO { get; set; }
        public Int32 ENTRYSRNO { get; set; }
        public string ENTRYTYPE { get; set; }
        public Int32 FROMDEPARTMENT_ID { get; set; }
        public Int32 TODEPARTMENT_ID { get; set; }
        public Int64 FROMEMPLOYEE_ID { get; set; }
        public Int64 TOEMPLOYEE_ID { get; set; }


        public string FName { get; set; }
        public string Ext { get; set; }
        public string SPath { get; set; }
           
        public Int64 FROMMANAGER_ID { get; set; }
        public Int64 TOMANAGER_ID { get; set; }

        public Int64 INITIALEMPLOYEE_ID { get; set; } //#P : 13-10-2019

        public Int32 FROMPROCESS_ID { get; set; }
        public Int32 TOPROCESS_ID { get; set; }
        public Int32 NEXTPROCESS_ID { get; set; }


        public Int32 JUMPRETURNPROCESS_ID { get; set; }

        public Int32 ISSUEPCS { get; set; }
        public double ISSUECARAT { get; set; }
        public Int32 READYPCS { get; set; }
        public double READYCARAT { get; set; }
        public Int32 RRPCS { get; set; }
        public double RRCARAT { get; set; }
        public Int32 SECONDPCS { get; set; }
        public double SECONDCARAT { get; set; }

        public string KAPANNAME { get; set; }
        public int PACKETNO { get; set; }
        public string TAG { get; set; }


        public bool ISPOLISHFINAL { get; set; }

        public Int32 EXTRAPCS { get; set; }
        public double EXTRACARAT { get; set; }

        public Int32 REJECTIONPCS { get; set; }
        public double REJECTIONCARAT { get; set; }

        public Int32 LOSTPCS { get; set; }
        public double LOSTCARAT { get; set; }
        public double LOSSCARAT { get; set; }
        public double MIXINGLESSPLUS { get; set; }
        public string RETURNTYPE { get; set; }
        public string TRANSDATE { get; set; }
        public Int64 TRANSBY { get; set; }
        public string TRANSIP { get; set; }
        public string TRANSTYPE { get; set; }
        public string CONFDATE { get; set; }
        public Int64 CONFBY { get; set; }
        public string CONFIP { get; set; }
        public Int64 PREVTRN_ID { get; set; }
        public string PREVENTRYTYPE { get; set; }
        public string LASTMODIFIEDDATE { get; set; }
        public string REMARK { get; set; }

        public Int64 PRD_ID { get; set; }
        public Int32 PRDTYPE_ID { get; set; }

        public bool AUTOCONFIRM { get; set; }

        public bool ISFROMFINALISSUE { get; set; }
        public double EXPWEIGHT { get; set; }

        public Int64 JANGEDNORet { get; set; }
        public Int64 JANGEDNOTran { get; set; }

        public int PRDSHAPE_ID { get; set; }
        public double PRDCTS { get; set; }

        public bool ISENTRYFROMSPLITMODULE { get; set; }

        public double EXPCARAT { get; set; }

        public string SHIFT { get; set; } // DHARA : 08-07-2023

        public string ReturnValue { get; set; }
        public string ReturnValuePacketID { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
        public string ReturnValueJangedNo { get; set; }
        public string ReturnValueJangedNoRet { get; set; }
        public string ReturnValueJangedNoTran { get; set; }
        public string ReturnValueEmpRet_TRNID { get; set; }

        public string Ope { get; set; }  //Used in KapanLiveStock Form(On Delete)

        public Int64 TEMPMARKER_ID { get; set; } //hinal 01-01-2022
        public string BARCODE { get; set; } //#P : 25-02-2022
        public string OPTIONTRANSFER { get; set; }
        public string JUMPISSTOTRN { get; set; }

        public bool ISMERGE { get; set; } // Dhara : 21-04-2022

        public Int32 MACHINE_ID { get; set; }
        public double CARAT { get; set; }
        public bool DOWNLOAD { get; set; }
        public bool COMPLETE { get; set; }
        public bool CANCEL { get; set; }
        public bool REJECT { get; set; }
        public Int32 REJECTREASON_ID { get; set; }
        public string REJECTREMARK { get; set; }
        public string REJECTAPPROVALSTATUS { get; set; }

        public string StrKapanMergeXml { get; set; }//Gunjan:31-03-2023

        public string  PERCENTAGE { get; set; }
    }

}
