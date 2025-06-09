using BusLib.TableName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using AxonDataLib;
using System.Data;
using System.Collections;
using System.IO;

namespace BusLib.Transaction  
{
	public class BOTRN_RapMistakePrdEntry
	{
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
		AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

		public DataTable GetDataForSelectedRow(string StrTag, string StrPrdType, Int64 IntEmployee, string StrKapan,int IntPktNo, string StrPrd)
		{
			DataTable Dtab = new DataTable();
			Ope.ClearParams();
			Ope.AddParams("TAG", StrTag, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PRDTYPE", StrPrdType, DbType.String, ParameterDirection.Input);
			Ope.AddParams("EMPLOYEE_ID", IntEmployee, DbType.String, ParameterDirection.Input);
			Ope.AddParams("KAPANNAME", StrKapan, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PACKETNO", IntPktNo, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PRD_ID", StrPrd, DbType.String, ParameterDirection.Input);

			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "Trn_RapMistakePrdEntryGetDataForSelectedRow", CommandType.StoredProcedure);

			return Dtab;
		}

		public DataTable GetDataForExcelExport(string StrPrd_ID,string StrPrdType, Int64 intEmployee_ID, int intPktNo, string StrKapanName, string StrTag )
		{
			DataTable Dtab = new DataTable();
			Ope.ClearParams();

			Ope.AddParams("PRD_ID", StrPrd_ID, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PRDTYPE", StrPrdType, DbType.String, ParameterDirection.Input);
			Ope.AddParams("EMPLOYEE_ID", intEmployee_ID, DbType.Int64, ParameterDirection.Input);
			Ope.AddParams("PACKETNO", intPktNo, DbType.Int32, ParameterDirection.Input);
			Ope.AddParams("KAPANNAME", StrKapanName, DbType.String, ParameterDirection.Input);
			Ope.AddParams("TAG", StrTag, DbType.String, ParameterDirection.Input);

			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "Trn_RapMistakePrdEntryExcelExport", CommandType.StoredProcedure);
			return Dtab;
		}
	
		public DataTable GetPredictionDataForPrint(string StrPrdType, Int64 IntEmployee, string StrKapan, int IntPktNo, string StrPrd)
		{
			DataTable Dtab = new DataTable();
			Ope.ClearParams();
		//	Ope.AddParams("TAG", StrTag, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PRDTYPE", StrPrdType, DbType.String, ParameterDirection.Input);
			Ope.AddParams("EMPLOYEE_ID", IntEmployee, DbType.String, ParameterDirection.Input);
			Ope.AddParams("KAPANNAME", StrKapan, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PACKETNO", IntPktNo, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PRD_ID", StrPrd, DbType.String, ParameterDirection.Input);

			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "RP_RapMistakePrdEntryGetDataForPrint", CommandType.StoredProcedure);

			return Dtab;
		}

		public string DeleteAll(string Strid)
		{
			string Str1 = "";
			Str1 = "Delete From Trn_SinglePrdRapMistake With(RowLock) Where Prd_ID = '" + Strid + "'";
			Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str1, CommandType.Text);

			return "";
		}
		public int DeleteFromGrid(string Strid)
		{
			string Str1 = "";
			Str1 = "Delete From Trn_SinglePrdRapMistake With(RowLock) Where RapMistake_ID = '" + Strid + "'";
			int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str1, CommandType.Text);

			return IntRes;
		}

		public RapMistakePrdEntryProperty FindRapWithUpDown(RapMistakePrdEntryProperty pClsProperty)
		{
			DataTable DTab = new DataTable();
			Ope.ClearParams();

			Ope.AddParams("L_Code", "", DbType.String, ParameterDirection.Input);
			Ope.AddParams("Carat", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
			Ope.AddParams("Shape_Code", pClsProperty.SHAPECODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Color_Code", pClsProperty.COLORCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Clarity_Code", pClsProperty.CLARITYCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Lab", pClsProperty.LABCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Cut_Code", pClsProperty.CUTCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Polish_Code", pClsProperty.POLCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Symmetry_Code", pClsProperty.SYMCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Flursance_Code", pClsProperty.FLCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("LbLc_Code", pClsProperty.LBLCCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Nts_Code", pClsProperty.NATTSCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Milky_Code", pClsProperty.MILKYCODE, DbType.String, ParameterDirection.Input);

			//Ope.AddParams("Diameter", 0, DbType.Double, ParameterDirection.Input);
			Ope.AddParams("Diameter", pClsProperty.DIAMIN, DbType.Double, ParameterDirection.Input); //23-08-2020


			Ope.AddParams("ColorShade_Code", pClsProperty.COLORSHADECODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("OpenInclusion_Code", pClsProperty.OPENINCCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("BlackInclusion_Code", pClsProperty.BLACKINCCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("WhiteInclusion_Code", pClsProperty.WHITEINCCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Pavallion_Code", pClsProperty.PAVCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("CanadaMark_Code", "", DbType.String, ParameterDirection.Input);
			Ope.AddParams("EyeClean_Code", pClsProperty.EYECLEANCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Luster_Code", pClsProperty.LUSTERCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Natural_Code", pClsProperty.NATURALCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Grain_Code", pClsProperty.GRAINCODE, DbType.String, ParameterDirection.Input);

			Ope.AddParams("FutureDis", 0, DbType.Double, ParameterDirection.Input);
			Ope.AddParams("PlanningType", "", DbType.String, ParameterDirection.Input);
			Ope.AddParams("ManualDiscount", 0, DbType.Double, ParameterDirection.Input);
			Ope.AddParams("RapDate", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
			Ope.AddParams("IsGetBack", 1, DbType.String, ParameterDirection.Input);
			Ope.AddParams("Rate", "", DbType.String, ParameterDirection.Output);

			Ope.AddParams("GCarat", pClsProperty.GCARAT, DbType.Double, ParameterDirection.Input);
			Ope.AddParams("GCut_Code", pClsProperty.GCUTCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("GPolish_Code", pClsProperty.GPOLCODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("GSymmetry_Code", pClsProperty.GSYMCODE, DbType.String, ParameterDirection.Input);

			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdFindRap", CommandType.StoredProcedure);

			pClsProperty.DISCOUNT = 0;
			pClsProperty.AMOUNTDISCOUNT = 0;
			pClsProperty.PRICEPERCARAT = 0;
			pClsProperty.RAPAPORT = 0;
			pClsProperty.AMOUNT = 0;
			pClsProperty.ISMIXRATE = false;
			pClsProperty.GIANONGIA = "GIA";

		

			foreach (DataRow DRow in DTab.Rows)
			{
				if (Val.ToString(DRow["Ope"]) == "Regular")
				{

					pClsProperty.DISCOUNT = Val.Val(DRow["TotalDiscount"]);
					pClsProperty.AMOUNTDISCOUNT = Val.Val(DRow["TotalAmountDiscount"]);
					pClsProperty.PRICEPERCARAT = Val.Val(DRow["Rate"]);
					pClsProperty.RAPAPORT = Val.Val(DRow["OriginalRate"]);
					pClsProperty.AMOUNT = Val.Val(DRow["Amount"]);

					//Add : Pinali : 07-09-2019
					pClsProperty.MDISCOUNT = Val.Val(DRow["MTotalDiscount"]);
					pClsProperty.MPRICEPERCARAT = Val.Val(DRow["MRate"]);
					pClsProperty.MAMOUNT = Val.Val(DRow["MAmount"]);
					//End : Pinali : 07-09-2019

					pClsProperty.COLOR_ID = Val.ToInt(DRow["Color_ID"]);
					pClsProperty.COLORCODE = Val.ToString(DRow["ColorCode"]);
					pClsProperty.COLORNAME = Val.ToString(DRow["ColorName"]);

					pClsProperty.CLARITY_ID = Val.ToInt(DRow["Clarity_ID"]);
					pClsProperty.CLARITYCODE = Val.ToString(DRow["ClarityCode"]);
					pClsProperty.CLARITYNAME = Val.ToString(DRow["ClarityName"]);

					pClsProperty.ISMIXRATE = Val.ToBoolean(DRow["ISMIXRATE"]);
					if (pClsProperty.ISMIXRATE == true)
					{
						pClsProperty.GIANONGIA = "MIX";
					}
					else if (pClsProperty.ISMIXRATE == false)
					{
						pClsProperty.GIANONGIA = "GIA";
					}


					DRow.Table.TableName = "ROW";

					//string originalXmlString = string.Empty;
					//using (StringWriter sw = new StringWriter())
					//{
					//	DRow.Table.WriteXml(sw);
					//	originalXmlString = sw.ToString();
					//}
					//
					//pClsProperty.DRowDisRegularXML = originalXmlString;
				}
				
			}

			return pClsProperty;
		}

		public DataRow GetPacketDataRow(string pStrKapanName, int pIntPacketNo, string pStrTag)
		{
			string Str = "SELECT * From Trn_SinglePacketMaster With(Nolock) WHERE 1=1 And KapanName = '" + pStrKapanName + "' And PacketNo = " + pIntPacketNo + "";
			return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
		}

		public DataTable GetPredictionData(string pPrdTypeID = "",string StrKapnName = "Milan",int intPktNo = 0, Int64 Employee = 0, string strPrd = "")
		{
			DataTable DTab = new DataTable();
			Ope.ClearParams();
			Ope.AddParams("PRD_ID", pPrdTypeID, DbType.String, ParameterDirection.Input);
			Ope.AddParams("KAPANNAME", StrKapnName, DbType.Int32, ParameterDirection.Input);
			Ope.AddParams("PACKETNO", intPktNo, DbType.Guid, ParameterDirection.Input);
			Ope.AddParams("EMPLOYEE_ID", Employee, DbType.Int64, ParameterDirection.Input);
			Ope.AddParams("PRDTYPE", strPrd, DbType.Int64, ParameterDirection.Input);
			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RapMistakePrdEntryGetDataForSelectedRow", CommandType.StoredProcedure);

			return DTab;
		}
		public DataTable GetAllParameterTable()
		{
			DataTable DTab = new DataTable();
			Ope.ClearParams();
			string Str = "Select * From MST_Para With(NOLOCK) Where ISActive = 1";
			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
			return DTab;
		}

		public DataTable MistakeEntryGetData(string StrFromDate, string StrToDate, string prdtype,Int64 intEmployeeID ,string StrKapanName,Int32 IntPacketNo, string StrTag)
		{
			DataTable Dtab = new DataTable();

			Ope.ClearParams();
			Ope.AddParams("FROMDATE", StrFromDate, DbType.String, ParameterDirection.Input);
			Ope.AddParams("TODATE", StrToDate, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PRDTYPE", prdtype, DbType.String, ParameterDirection.Input);
			Ope.AddParams("EMPLOYEE_ID", intEmployeeID, DbType.Int64, ParameterDirection.Input);
			Ope.AddParams("KAPANNAME", StrKapanName, DbType.String, ParameterDirection.Input);
			Ope.AddParams("PACKETNO", IntPacketNo, DbType.Int32, ParameterDirection.Input);
			Ope.AddParams("TAG", StrTag, DbType.String, ParameterDirection.Input);

			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "Trn_RapMistakePrdEntryGetData", CommandType.StoredProcedure);

			return Dtab;
		}

		public RapMistakePrdEntryProperty Delete(RapMistakePrdEntryProperty Property)
		{
			Ope.ClearParams();

			Ope.AddParams("PRD_ID", Property.PRD_ID, DbType.String, ParameterDirection.Input);
			Ope.AddParams("ACTION", Property.ENTRYMODE, DbType.String, ParameterDirection.Input);
			Ope.AddParams("RETURNMESSAGETYPE", Property.ReturnMessageType, DbType.String, ParameterDirection.Output);
			Ope.AddParams("RETURNMESSAGEDESC", Property.ReturnMessageDesc, DbType.String, ParameterDirection.Output);

			ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RapMistakePrdEntrySave", CommandType.StoredProcedure);
			if (AL.Count != 0)
			{
				Property.ReturnMessageType = Val.ToString(AL[0]);
				Property.ReturnMessageDesc = Val.ToString(AL[1]);
			}
			return Property;

		}

		public RapMistakePrdEntryProperty Save(RapMistakePrdEntryProperty pClsProperty)
		{
			try
			{
				Ope.ClearParams();

				Ope.AddParams("ID", pClsProperty.ID, DbType.String, ParameterDirection.Input);
				Ope.AddParams("PRD_ID", pClsProperty.PRD_ID, DbType.String, ParameterDirection.Input);
				Ope.AddParams("RAPMISTAKEDATE", pClsProperty.RAPMISTAKEDATE, DbType.String, ParameterDirection.Input);
				Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
				Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("PRDTYPE", pClsProperty.PRDTYPE, DbType.String, ParameterDirection.Input);
				Ope.AddParams("EMPLOYEECODE", pClsProperty.EMPLOYEECODE, DbType.String, ParameterDirection.Input);
				Ope.AddParams("PLANNO", pClsProperty.PLANNO, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
				Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("TAGSRNO", pClsProperty.TAGSRNO, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
				Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
				Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
				Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
				Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("COLORSHADE_ID", pClsProperty.COLORSHADE_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("CUT_ID", pClsProperty.CUT_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("POL_ID", pClsProperty.POL_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("SYM_ID", pClsProperty.SYM_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("MILKY_ID", pClsProperty.MILKY_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);

				Ope.AddParams("BLACKINC_ID", pClsProperty.BLACKINC_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("OPENINC_ID", pClsProperty.OPENINC_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("WHITEINC_ID", pClsProperty.WHITEINC_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("LUSTER_ID", pClsProperty.LUSTER_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("HA_ID", pClsProperty.HA_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("PAV_ID", pClsProperty.PAV_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("EYECLEAN_ID", pClsProperty.EYECLEAN_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("NATURAL_ID", pClsProperty.NATURAL_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("GRAIN_ID", pClsProperty.GRAIN_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("DISCOUNT", pClsProperty.DISCOUNT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("AMOUNTDISCOUNT", pClsProperty.AMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("RAPAPORT", pClsProperty.RAPAPORT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("PRICEPERCARAT", pClsProperty.PRICEPERCARAT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("GIANONGIA", pClsProperty.GIANONGIA, DbType.String, ParameterDirection.Input);

				
				Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
				Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.String, ParameterDirection.Input);
			//	Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.Date, ParameterDirection.Input);
				Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.String, ParameterDirection.Input);
				Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
				Ope.AddParams("ISFINAL", pClsProperty.ISFINAL, DbType.Boolean, ParameterDirection.Input);
				Ope.AddParams("HOSTNAME", Config.ComputerName, DbType.String, ParameterDirection.Input);
				Ope.AddParams("TFLAG", pClsProperty.TFLAG, DbType.Boolean, ParameterDirection.Input);  //Add : Pinali : 15-09-2019
				Ope.AddParams("ISNOBGM", pClsProperty.ISNOBGM, DbType.Boolean, ParameterDirection.Input); //#P
				Ope.AddParams("ISNOBLACK", pClsProperty.ISNOBLACK, DbType.Boolean, ParameterDirection.Input); //#P

				Ope.AddParams("PCNGRDBYLAB_ID", pClsProperty.PCNGRDBYLAB_ID, DbType.String, ParameterDirection.Input);
				Ope.AddParams("ISPCNGRDBYLABENTRY", pClsProperty.ISPCNGRDBYLABENTRY, DbType.Boolean, ParameterDirection.Input);
				Ope.AddParams("ISMIXRATE", pClsProperty.ISMIXRATE, DbType.Boolean, ParameterDirection.Input);
				Ope.AddParams("ROUGHCARAT", pClsProperty.ROUGHCARAT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("BALANCECARAT", pClsProperty.BALANCECARAT2, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("LAB_ID", pClsProperty.LAB_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("ISDIFF", pClsProperty.ISDIFF, DbType.Boolean, ParameterDirection.Input);
				Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
				Ope.AddParams("MANUALRAPAPORT", pClsProperty.RAPAPORT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("MANUALDISCOUNT", pClsProperty.MANUALDISCOUNT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("MANUALPRICEPERCARAT", pClsProperty.MANUALPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("MANUALAMOUNT", pClsProperty.MANUALAMOUNT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("SUMAMOUNT", pClsProperty.SUMAMOUNT, DbType.Double, ParameterDirection.Input);
				Ope.AddParams("MAXAMTFLAG", pClsProperty.MAXAMTFLAG, DbType.Boolean, ParameterDirection.Input);
				
				Ope.AddParams("ACTION", pClsProperty.ENTRYMODE, DbType.String, ParameterDirection.Input);
				


				Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);
				Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
				Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

				ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RapMistakePrdEntrySave", CommandType.StoredProcedure);
				if (AL.Count != 0)
				{
					pClsProperty.ReturnValue = Val.ToString(AL[0]);
					pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
					pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
				}
			}
			catch (System.Exception ex)
			{
				pClsProperty.ReturnValue = "";
				pClsProperty.ReturnMessageType = "FAIL";
				pClsProperty.ReturnMessageDesc = ex.Message;

			}
			return pClsProperty;
		}
	}
}
