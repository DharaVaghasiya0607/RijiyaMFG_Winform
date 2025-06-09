using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnSinglePacketCreationProperty
    {
        public Int64 PACKET_ID { get; set; }
        public Int64 MAINPACKET_ID { get; set; }
        public Int64 PARENTPACKET_ID { get; set; }
        public Int32 PACKETGRADE_ID { get; set; } //KRINA
        public Int32 PACKETCATEGORY_ID { get; set; } //KRINA

        public Int64 KAPAN_ID { get; set; }

        //For Repairing Kapan - 30-04-2019

        public string PREVKAPANNAME { get; set; }
        public Int64 PREVPACKET_ID { get; set; }
        public Int32 PREVPACKETNO { get; set; }
        public string PREVPACKETTAG { get; set; }

        public double PREVPACKETCARAT { get; set; } //#P : 25-10-2019

        //End : 30-04-2019

        public double EXPDOLLARREVICE { get; set; }//urvisha add by :13-04-2023
        public double EXPDOLLAR { get; set; }      //urvisha add by :13-04-2023


        public string KAPANNAME { get; set; }
        public Int64 MAINMANAGER_ID { get; set; }

        public string KAPANCATEGORY { get; set; }

        public Int32 PACKETNO { get; set; }

        public Int32 PKTSERIALNO { get; set; }

        public string TAG { get; set; }
        public Int32 TAGSRNO { get; set; }


        //public double EXPDOLLARREVICE { get; set; }
        //public double EXPDOLLAR { get; set; }


        public string BARCODE { get; set; }
        public string RFIDTAG { get; set; }
        public Int32 LOTPCS { get; set; }
        public double LOTCARAT { get; set; }
        public Int32 BALANCEPCS { get; set; }
        public double BALANCECARAT { get; set; }
        public Int32 SECONDPCS { get; set; }
        public double SECONDCARAT { get; set; }

        public Int32 EXTRAPCS { get; set; }
        public double EXTRACARAT { get; set; }

        public Int32 REJECTIONPCS { get; set; }
        public double REJECTIONCARAT { get; set; }
        public Int32 REASON_ID { get; set; }

        public Int32 LOSTPCS { get; set; }
        public double LOSTCARAT { get; set; }
        public Int32 LOSSPCS { get; set; }
        public double LOSSCARAT { get; set; }
        public double MIXINGLESSPLUS { get; set; }
        public double MAKCARAT { get; set; }
        public string MAKDATE { get; set; }
        public double POLCARAT { get; set; }
        public Int16 POLTHREAD { get; set; }
        public string POLDATE { get; set; }
        public string ENTRYDATE { get; set; }
        public Int64 TRN_ID { get; set; }
        public string ENTRYTYPE { get; set; }
        public Int64 PREVTRN_ID { get; set; }
        public string PREVENTRYTYPE { get; set; }

        public double TRANSFERCARAT { get; set; } //#P : 29-08-2022

        public Int64 MARKER_ID { get; set; }
        public string MARKERCODE { get; set; }
        public Int64 FROMEMPLOYEE_ID { get; set; }
        public Int64 TOEMPLOYEE_ID { get; set; }


        public Int64 FROMMANAGER_ID { get; set; }
        public Int64 TOMANAGER_ID { get; set; }


        public Int32 FROMDEPARTMENT_ID { get; set; }
        public Int32 TODEPARTMENT_ID { get; set; }
        public Int32 FROMPROCESS_ID { get; set; }
        public Int32 TOPROCESS_ID { get; set; }
        public Int32 NEXTPROCESS_ID { get; set; }

        public Int32 JUMPRETURNPROCESS_ID { get; set; }

        public string GRADINGSTATUS { get; set; }

        public Guid MARPRD_ID { get; set; }
        public Guid CHKPRD_ID { get; set; }
        public Guid MAKPRD_ID { get; set; }
        public Guid FACGRD_ID { get; set; }
        public Guid BOMGRD_ID { get; set; }
        public Guid LABGRD_ID { get; set; }

        public Int32 SHAPE_ID { get; set; }
        public Int32 CLARITY_ID { get; set; }
        public Int32 COLOR_ID { get; set; }
        public Int32 COLORSHADE_ID { get; set; }
        public Int32 CUT_ID { get; set; }
        public Int32 POL_ID { get; set; }
        public Int32 SYM_ID { get; set; }
        public Int32 FL_ID { get; set; }
        public Int32 MILKY_ID { get; set; }
        public Int32 LBLC_ID { get; set; }
        public Int32 NATTS_ID { get; set; }
        public Int32 TENSION_ID { get; set; }

        public Double EXPPER { get; set; }
        public Double EXPCARAT { get; set; }
        public Double DISCOUNT { get; set; }
        public Double AMOUNTDISCOUNT { get; set; }
        public Double RAPAPORT { get; set; }
        public Double PRICEPERCARAT { get; set; }
        public Double AMOUNT { get; set; }
        public string REMARK { get; set; }
        public string PACKETTYPE { get; set; }
        public Int64 SPLITTRN_ID { get; set; }
        public bool AUTOCONFIRM { get; set; }
        public Int64 JANGEDNO { get; set; }

        public int PRDSHAPE_ID { get; set; }
        public double PRDCTS { get; set; }

        public bool ISNEWTAG { get; set; }
        public string PRDTAG { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
        public string ReturnValueJangedNo { get; set; }
        public string ReturnValueTag { get; set; }
        public double EXPWEIGHT { get; set; }

        public string ReturnValuePacketID { get; set; }
        public string ReturnValueMaxPacketNo { get; set; }

        public string Ope { get; set; }     //Used in KapanLiveStock Form(On Delete)
        public Int64 JANGEDNOTRAN { get; set; }  //Used in KapanLiveStock Form(On Delete)

        public string JUMPISSTOTRN { get; set; }

        public bool ISREJECTION { get; set; } //hinal : 21-06-2022
        public double RATE { get; set; }

        public string STRXMLFORREJECTION { get; set; }
        public string OPEREJECT { get; set; }
        public double REJECTCARAT { get; set; }

        public Int32 PACKETGROUP_ID { get; set; }
        public Int32 ReturnValuePktSerialNo { get; set; }
        public string ReturnValueBarcode { get; set; }


        public Int64 KAPANMAINMANAGER_ID { get; set; }//GUNJAN:27/03/2023
    }

}
