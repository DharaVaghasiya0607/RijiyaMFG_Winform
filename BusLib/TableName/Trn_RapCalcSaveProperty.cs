using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class Trn_RapSaveProperty
    {
        public Int64 ID { get; set; }
        public Int64 SLIPNO { get; set; }
        public Int64 PRD_ID { get; set; }
        public Int64 PACKET_ID { get; set; }
        public int PRDTYPE_ID	 { get; set; }
        public string PRDTYPE	 { get; set; }
        public string KAPANNAME	 { get; set; }
        public int PLANNO	 { get; set; }
        public int PACKETNO	 { get; set; }
        public int TAGSRNO	 { get; set; }
        public string MTAG { get; set; }
        public string TAG	 { get; set; }
        public Int64 EMPLOYEE_ID	 { get; set; }
        public Int64 MANAGER_ID	 { get; set; }

         public bool IsKachhuVajan { get; set; }

        public int SHAPE_ID	 { get; set; }
        public int CLARITY_ID	 { get; set; }
        public int COLOR_ID	 { get; set; }
        public int COLORSHADE_ID	 { get; set; }
        public int CUT_ID	 { get; set; }
        public int POL_ID	 { get; set; }
        public int SYM_ID	 { get; set; }
        public int FL_ID	 { get; set; }
        public int MILKY_ID	 { get; set; }
        public int LBLC_ID	 { get; set; }
        public int NATTS_ID	 { get; set; }
        public int TENSION_ID	 { get; set; }

        public int SAKHAT_ID { get; set; }

        public int BLACKINC_ID	 { get; set; }
        public int OPENINC_ID	 { get; set; }
        public int WHITEINC_ID	 { get; set; }
        public int LUSTER_ID	 { get; set; }
        public int HA_ID	 { get; set; }
        public int PAV_ID	 { get; set; }
        public int LUSTER	 { get; set; }
        public int EYECLEAN_ID	 { get; set; }
        public int NATURAL_ID	 { get; set; }
        public int GRAIN_ID	 { get; set; }
        public int LAB_ID { get; set; }

        public double GCARAT { get; set; }
        public int GCOLOR_ID { get; set; }
        public int GCLARITY_ID { get; set; }
        public int GCUT_ID { get; set; }
        public int GPOL_ID { get; set; }
        public int GSYM_ID { get; set; }

        public bool ISNOBGM { get; set; }
        public bool ISNOBLACK { get; set; }

        public int UPCOLOR_ID { get; set; }
        public string UPCOLORCODE { get; set; }
        public string UPCOLORNAME { get; set; }
        
        public double UPCOLORDISCOUNT { get; set; }
        public double UPCOLORAMOUNTDISCOUNT { get; set; }
        public double UPCOLORRAPAPORT { get; set; }
        public double UPCOLORPRICEPERCARAT { get; set; }
        public double UPCOLORAMOUNT { get; set; }

        public int DOWNCOLOR_ID { get; set; }
        public string DOWNCOLORCODE { get; set; }
        public string DOWNCOLORNAME { get; set; }
        public double DOWNCOLORDISCOUNT { get; set; }
        public double DOWNCOLORAMOUNTDISCOUNT { get; set; }
        public double DOWNCOLORRAPAPORT { get; set; }
        public double DOWNCOLORPRICEPERCARAT { get; set; }
        public double DOWNCOLORAMOUNT { get; set; }

        public int UPCLARITY_ID { get; set; }
        public string UPCLARITYCODE { get; set; }
        public string UPCLARITYNAME { get; set; }
        public double UPCLARITYDISCOUNT { get; set; }
        public double UPCLARITYAMOUNTDISCOUNT { get; set; }
        public double UPCLARITYRAPAPORT { get; set; }
        public double UPCLARITYPRICEPERCARAT { get; set; }
        public double UPCLARITYAMOUNT { get; set; }

        public int DOWNCLARITY_ID { get; set; }
        public string DOWNCLARITYCODE { get; set; }
        public string DOWNCLARITYNAME { get; set; }
        public double DOWNCLARITYDISCOUNT { get; set; }
        public double DOWNCLARITYAMOUNTDISCOUNT { get; set; }
        public double DOWNCLARITYRAPAPORT { get; set; }
        public double DOWNCLARITYPRICEPERCARAT { get; set; }
        public double DOWNCLARITYAMOUNT { get; set; }

        public double BALANCECARAT { get; set; }
        public double CARAT { get; set; }

        public Int64 TRN_ID { get; set; }


        //#Dhara : 06-06-2022
        public Int32 PCS { get; set; }
        public Int64 MAINPACKET_ID { get; set; }
        public double LOTCARAT { get; set; }
        public Int64 KAPAN_ID { get; set; }
        //#End : 06-06-2022

        public double DISCOUNT	 { get; set; }
        public double AMOUNTDISCOUNT	 { get; set; }
        public double RAPAPORT	 { get; set; }
        public double PRICEPERCARAT	 { get; set; }
        public double AMOUNT	 { get; set; }

        public double GDISCOUNT { get; set; }
        public double GAMOUNTDISCOUNT { get; set; }
        public double GRAPAPORT { get; set; }
        public double GPRICEPERCARAT { get; set; }
        public double GAMOUNT { get; set; }

        //Add : Pinali : 07-9-2019

        public double MDISCOUNT { get; set; }
        public double MPRICEPERCARAT { get; set; }
        public double MAMOUNT { get; set; }
        public double MGDISCOUNT { get; set; }
        public double MGPRICEPERCARAT { get; set; }
        public double MGAMOUNT { get; set; }

        //End : Pinali : 07-09-2019

        //Add : Pinali : 26-05-2020
        public double MKAVRAPAPORT { get; set; }
        public double MKAVDISCOUNT { get; set; }
        public double MKAVPRICEPERCARAT  { get; set; }
        public double MKAVAMOUNT { get; set; }

        public double EXPRAPAPORT { get; set; }
        public double EXPDISCOUNT { get; set; }
        public double EXPPRICEPERCARAT { get; set; }
        public double EXPAMOUNT { get; set; }


        //#P : 04-09-2020
        public double SURATEXPLABCHARGE { get; set; }
        public double SURATEXPBEFORERAPAPORT { get; set; }
        public double SURATEXPBEFOREDISCOUNT { get; set; }
        public double SURATEXPBEFOREPRICEPERCARAT { get; set; }
        public double SURATEXPBEFOREAMOUNT { get; set; }

        public double SURATEXPAFTERRAPAPORT { get; set; }
        public double SURATEXPAFTERDISCOUNT { get; set; }
        public double SURATEXPAFTERPRICEPERCARAT { get; set; }
        public double SURATEXPAFTERAMOUNT { get; set; }

        public string GRDRESULTSTATUS { get; set; }
        public string CURRENTGRDRESULTSTATUS { get; set; }

        public string HELIUMTABLEPC { get; set; }
        public string HELIUMRATIO { get; set; }
        public string HELIUMTOTALDEPTH { get; set; }

        public bool ISCONFIRMGRADER { get; set; }
        //End : #P : 04-09-2020

        public string REFKAPANNAME { get; set; }
        public Int64 REFKAPAN_ID { get; set; }
        public Int32 REFPACKETNO { get; set; }
        public string REFTAG { get; set; }

        //#P : 13-01-2021
        public double MUMBAIEXPLABCHARGE { get; set; }
        public double MUMBAIEXPBEFORERAPAPORT { get; set; }
        public double MUMBAIEXPBEFOREDISCOUNT { get; set; }
        public double MUMBAIEXPBEFOREPRICEPERCARAT { get; set; }
        public double MUMBAIEXPBEFOREAMOUNT { get; set; }

        public double MUMBAIEXPAFTERRAPAPORT { get; set; }
        public double MUMBAIEXPAFTERDISCOUNT { get; set; }
        public double MUMBAIEXPAFTERPRICEPERCARAT { get; set; }
        public double MUMBAIEXPAFTERAMOUNT { get; set; }
        //End : #P : 13-01-2021

        public double RAPNETRAPAPORT { get; set; }
        public double RAPNETDISCOUNT { get; set; }
        public double RAPNETPRICEPERCARAT { get; set; }
        public double RAPNETAMOUNT { get; set; }

        public string RAPNETLINK { get; set; }
        public string LABRESULTSTATUS { get; set; }
        public string CURRENTLABRESULTSTATUS { get; set; }

        public double RCHKREPDIFFAMOUNT { get; set; }
        public double RCHKREPDIFFPER { get; set; }
        public string RCHKREPCOMMENT { get; set; }

        public string ENTRYMODE { get; set; }
        //End : Pinali : 26-05-2020


        public bool ISCOPYROUGHMKBLPLANINTOFINALTFLAGPRD { get; set; }  //#P : 19-08-2020


        public string RAPDATE	 { get; set; }
        public Int64 COMPANY_ID	 { get; set; }
        public string ENTRYDATE { get; set; }

        public string LABPROCESS { get; set; }
        public string LABSELECTION { get; set; }
        public double DIAMIN { get; set; }
        public double DIAMAX { get; set; }
        public double HEIGHT { get; set; }
        
        public bool ISMIXRATE  { get; set; }
        public bool ISBYPASSVALIDATION { get; set; }

        public bool ISBYPASSVALIDATIONFORPARAMETER { get; set; }
        public string GIANONGIA { get; set; }

        //Add : Pinali : 04-11-2019 : When Enter PCN Grd/By/Lab Entry then this Grd/By/Lab Entry Is also Stored In RefPacketsDetail so at that time set IsPcnGrdByLabEntry = 1
        public bool ISPCNGRDBYLABENTRY { get; set; }
        public Int64 PCNGRDBYLAB_ID { get; set; }

        public string REPORTNO { get; set; }
        //End : Pinali : 04-11-2019

        public bool ISSAVEWITHPASSWORD { get; set; }  //#P : 27-01-2020

        public bool ISFINAL { get; set; }

        public bool TFLAG { get; set; } //Add : Pinali : 15-09-2019 : Coz of first delete and then Insert Functionlity Issue On TFlag(On Update).

        public bool ISCHANGETFLAG { get; set; } //Add : Pinali : 08-12-2020 : Tflag Padi gyo hoy and Final Flag Change kare tyare Flag ma value malse 1.(For Update Breaking Entry)

        public DataRow DRowDisRegular { get; set; }
        public DataRow DRowDisGraph { get; set; }
        public DataRow DRowDisUpColor { get; set; }
        public DataRow DRowDisUpClarity { get; set; }
        public DataRow DRowDisDownColor { get; set; }
        public DataRow DRowDisDownClarity { get; set; }
        
        public string DRowDisRegularXML { get; set; }
        public string REMARK { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

        public string Ope { get; set; }     //Used in KapanLiveStock Form(On Delete)


        public string SHAPECODE { get; set; }
        public string CLARITYCODE { get; set; }
        public string COLORCODE { get; set; }
        public string COLORSHADECODE { get; set; }
        public string CUTCODE { get; set; }
        public string POLCODE { get; set; }
        public string SYMCODE { get; set; }
        public string FLCODE { get; set; }
        public string MILKYCODE { get; set; }
        public string LBLCCODE { get; set; }
        public string NATTSCODE { get; set; }
        public string TENSIONCODE { get; set; }
        public string BLACKINCCODE { get; set; }
        public string OPENINCCODE { get; set; }
        public string WHITEINCCODE { get; set; }
        public string LUSTERCODE { get; set; }
        public string HACODE { get; set; }
        public string PAVCODE { get; set; }
        public string EYECLEANCODE { get; set; }
        public string NATURALCODE { get; set; }
        public string GRAINCODE { get; set; }
        public string LABCODE { get; set; }

        public string GCOLORCODE { get; set; }
        public string GCLARITYCODE { get; set; }
        public string GCUTCODE { get; set; }
        public string GPOLCODE { get; set; }
        public string GSYMCODE { get; set; }

        public string SHAPENAME { get; set; }
        public string CLARITYNAME { get; set; }
        public string COLORNAME { get; set; }
        public string COLORSHADENAME { get; set; }
        public string CUTNAME { get; set; }
        public string POLNAME { get; set; }
        public string SYMNAME { get; set; }
        public string FLNAME { get; set; }
        public string MILKYNAME { get; set; }
        public string LBLCNAME { get; set; }
        public string NATTSNAME { get; set; }
        public string TENSIONNAME { get; set; }
        public string BLACKINCNAME { get; set; }
        public string OPENINCNAME { get; set; }
        public string WHITEINCNAME { get; set; }
        public string LUSTERNAME { get; set; }
        public string HANAME { get; set; }
        public string PAVNAME { get; set; }
        public string EYECLEANNAME { get; set; }
        public string NATURALNAME { get; set; }
        public string GRAINNAME { get; set; }
        public string GCOLORNAME { get; set; }
        public string GCLARITYNAME { get; set; }
        public string GCUTNAME { get; set; }
        public string GPOLNAME { get; set; }
        public string GSYMNAME { get; set; }

        public Int64 COPYFROMEMPLOYEE_ID { get; set; }
        public string COPYFROMPRD_ID { get; set; }
        public string COPYFROM_ID { get; set; }
        public Int64 COPYTOEMPLOYEE_ID { get; set; }
        public string COPYTOPRD_ID { get; set; }
        public string COPYTO_ID { get; set; }
        public bool ISDIFF { get; set; }

        //#P : 08-07-2020 : For Tender RapFind
        public double DISCOUNTMANUAL { get; set; }
        public string XMLDETAIL { get; set; }
        public int CLARITY_ID1 { get; set; }
        public int COLOR_ID1 { get; set; }
        public int CLARITY_ID2 { get; set; }
        public int COLOR_ID2 { get; set; }
        public string CLARITYCODE1 { get; set; }
        public string COLORCODE1 { get; set; }
        public string CLARITYCODE2 { get; set; }
        public string COLORCODE2 { get; set; }

        public string SAWINGTYPE { get; set; }
		public bool ISEXCELIMPORT { get; set; }
		public double ROUGHWEIGHT { get; set; }
		public double KACHUVAJAN { get; set; }
		public double WIDTH { get; set; }
		public double LENGTH { get; set; }
        public Int32 RATIO { get; set; }

        public double DIAMETER { get; set; }

        public int TO_ID { get; set; }
        public string TOCODE { get; set; }
        public string TONAME { get; set; }

        public int CO_ID { get; set; }
        public string COCODE { get; set; }
        public string CONAME { get; set; }

        public int PO_ID { get; set; }
        public string POCODE { get; set; }
        public string PONAME { get; set; }

        public Int64 KAPANMAINMANAGER_ID { get; set; }//gUNJAN:27032023

        public int Rejection_ID { get; set; } // Dhara : 16-02-2024

    }
    public class Trn_SinglePrdProcessLockProperty
    {
        public string KAPANNAME { get; set; }
        public int PACKETNO { get; set; }
        public string TAG { get; set; }
        public Int64 PACKET_ID { get; set; }
        public Int64 EMPLOYEE_ID { get; set; }
        public int PRDTYPE_ID { get; set; }
        public int LOCKPRDTYPE_ID { get; set; }
        public bool ISLOCK { get; set; }
        public bool ISUNLOCK { get; set; }
        public string REMARK { get; set; }


        

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

    }
}
