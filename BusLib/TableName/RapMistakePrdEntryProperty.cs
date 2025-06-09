using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
	public class RapMistakePrdEntryProperty
	{
		public string ID { get; set; }
	//	public string PRD_ID { get; set; }

		public Int64 SLIPNO { get; set; }
		public string PRD_ID { get; set; }
		public Guid PACKET_ID { get; set; }
		public int PRDTYPE_ID { get; set; }
		public string PRDTYPE { get; set; }
		public string KAPANNAME { get; set; }
		public int PLANNO { get; set; }
		public int PACKETNO { get; set; }
		public int TAGSRNO { get; set; }
		public string MTAG { get; set; }
		public string TAG { get; set; }
		public Int64 EMPLOYEE_ID { get; set; }
		public Int64 MANAGER_ID { get; set; }

		public string MANUALDISCDATE { get; set; }

		public string EMPLOYEECODE { get; set; }

		public int SHAPE_ID { get; set; }
		public int CLARITY_ID { get; set; }
		public int COLOR_ID { get; set; }
		public int COLORSHADE_ID { get; set; }
		public int CUT_ID { get; set; }
		public int POL_ID { get; set; }
		public int SYM_ID { get; set; }
		public int FL_ID { get; set; }
		public int MILKY_ID { get; set; }
		public int LBLC_ID { get; set; }
		public int NATTS_ID { get; set; }
		public int TENSION_ID { get; set; }

		public int SAKHAT_ID { get; set; }

		public int BLACKINC_ID { get; set; }
		public int OPENINC_ID { get; set; }
		public int WHITEINC_ID { get; set; }
		public int LUSTER_ID { get; set; }
		public int HA_ID { get; set; }
		public int PAV_ID { get; set; }
		public int LUSTER { get; set; }
		public int EYECLEAN_ID { get; set; }
		public int NATURAL_ID { get; set; }
		public int GRAIN_ID { get; set; }
		public int LAB_ID { get; set; }

		public double GCARAT { get; set; }
		public int GCOLOR_ID { get; set; }
		public int GCLARITY_ID { get; set; }
		public int GCUT_ID { get; set; }
		public int GPOL_ID { get; set; }
		public int GSYM_ID { get; set; }

		public bool ISNOBGM { get; set; }
		public bool ISNOBLACK { get; set; }

		public double MANUALRAPAPORT { get; set; }
		public double MANUALDISCOUNT { get; set; }
		public double MANUALPRICEPERCARAT { get; set; }
		public double MANUALAMOUNT { get; set; }

		public double ROUGHCARAT { get; set; }
		public double BALANCECARAT2 { get; set; }
		public double SUMAMOUNT { get; set; }
		public bool MAXAMTFLAG { get; set; }

	

		public double BALANCECARAT { get; set; }
		public double CARAT { get; set; }
		public double DISCOUNT { get; set; }
		public double AMOUNTDISCOUNT { get; set; }
		public double RAPAPORT { get; set; }
		public double PRICEPERCARAT { get; set; }
		public double AMOUNT { get; set; }


		public double MDISCOUNT { get; set; }
		public double MPRICEPERCARAT { get; set; }
		public double MAMOUNT { get; set; }
		public double MGDISCOUNT { get; set; }
		public double MGPRICEPERCARAT { get; set; }
		public double MGAMOUNT { get; set; }

		
		public bool ISCONFIRMGRADER { get; set; }
		
		public string ENTRYMODE { get; set; }

		public string RAPMISTAKEDATE { get; set; }
		
		public string RAPDATE { get; set; }
		public Int64 COMPANY_ID { get; set; }
		public string ENTRYDATE { get; set; }

		public string LABPROCESS { get; set; }
		public string LABSELECTION { get; set; }
		public double DIAMIN { get; set; }
		public double DIAMAX { get; set; }
		public double HEIGHT { get; set; }

		public bool ISMIXRATE { get; set; }
		public string GIANONGIA { get; set; }

		public bool ISPCNGRDBYLABENTRY { get; set; }
		public Guid PCNGRDBYLAB_ID { get; set; }

		public string FROMDATE { get; set; }
		public string TODATE { get; set; }



		public string REPORTNO { get; set; }

		public bool ISSAVEWITHPASSWORD { get; set; }  //#P : 27-01-2020

		public bool ISFINAL { get; set; }

		public bool TFLAG { get; set; } //Add : Pinali : 15-09-2019 : Coz of first delete and then Insert Functionlity Issue On TFlag(On Update).

		public bool ISCHANGETFLAG { get; set; } //Add : Pinali : 08-12-2020 : Tflag Padi gyo hoy and Final Flag Change kare tyare Flag ma value malse 1.(For Update Breaking Entry)

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
	}
}
