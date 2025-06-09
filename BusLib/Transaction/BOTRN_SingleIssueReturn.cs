using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AxonDataLib;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;

namespace BusLib.Transaction
{
    public class BOTRN_SingleIssueReturn
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        public TrnSingleIssueReturnProperty TransferGoods(TrnSingleIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("OLDTRN_ID", pClsProperty.OLDTRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPTJANGEDNO", pClsProperty.DEPTJANGEDNO, DbType.Int64, ParameterDirection.Input); // Dhara : 19-08-2023
                Ope.AddParams("ENTRYSRNO", pClsProperty.ENTRYSRNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYTYPE", pClsProperty.ENTRYTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDEPARTMENT_ID", pClsProperty.FROMDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMEMPLOYEE_ID", pClsProperty.FROMEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMPROCESS_ID", pClsProperty.FROMPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPROCESS_ID", pClsProperty.TOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NEXTPROCESS_ID", pClsProperty.NEXTPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("FROMMANAGER_ID", pClsProperty.FROMMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ISSUEPCS", pClsProperty.ISSUEPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("READYPCS", pClsProperty.READYPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("READYCARAT", pClsProperty.READYCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RRPCS", pClsProperty.RRPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RRCARAT", pClsProperty.RRCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("SECONDPCS", pClsProperty.SECONDPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SECONDCARAT", pClsProperty.SECONDCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("EXTRAPCS", pClsProperty.EXTRAPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EXTRACARAT", pClsProperty.EXTRACARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("REJECTIONPCS", pClsProperty.REJECTIONPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("REJECTIONCARAT", pClsProperty.REJECTIONCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("LOSTPCS", pClsProperty.LOSTPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOSTCARAT", pClsProperty.LOSTCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOSSCARAT", pClsProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MIXINGLESSPLUS", pClsProperty.MIXINGLESSPLUS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RETURNTYPE", pClsProperty.RETURNTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TRANSDATE", pClsProperty.TRANSDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TRANSBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TRANSIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TRANSTYPE", pClsProperty.TRANSTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("AUTOCONFIRM", pClsProperty.AUTOCONFIRM, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISPOLISHFINAL", pClsProperty.ISPOLISHFINAL, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("PRD_ID", pClsProperty.PRD_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ISFROMFINALISSUE", pClsProperty.ISFROMFINALISSUE, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("EXPWEIGHT", pClsProperty.EXPWEIGHT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("PRDSHAPE_ID", pClsProperty.PRDSHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRDCTS", pClsProperty.PRDCTS, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("TEMPMARKER_ID", pClsProperty.TEMPMARKER_ID, DbType.Int64, ParameterDirection.Input); //hinal 01-01-2022

                Ope.AddParams("OPTIONTRANSFER", pClsProperty.OPTIONTRANSFER, DbType.String, ParameterDirection.Input);
                Ope.AddParams("JUMPISSTOTRN", pClsProperty.JUMPISSTOTRN, DbType.String, ParameterDirection.Input);
                Ope.AddParams("JANGEDNORET", pClsProperty.JANGEDNORet, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("JANGEDNOTRAN", pClsProperty.JANGEDNOTran, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ISENTRYFROMSPLITMODULE", pClsProperty.ISENTRYFROMSPLITMODULE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("JUMPRETURNPROCESS_ID", pClsProperty.JUMPRETURNPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("DOWNLOAD", pClsProperty.DOWNLOAD, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("COMPLETE", pClsProperty.COMPLETE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("CANCEL", pClsProperty.CANCEL, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REJECT", pClsProperty.REJECT, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("SHIFT", pClsProperty.SHIFT, DbType.String, ParameterDirection.Input); // DHARA : 08-07-2023

               // Ope.AddParams("PERCENTAGE", pClsProperty.PERCENTAGE, DbType.String, ParameterDirection.Input);//Gunjan : 17-02-2023

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNoRet", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNoTran", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNVALUEDEPTJANGEDNO", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueEmpRet_TRNID", "", DbType.String, ParameterDirection.Output); //#P : 04-10-2022 : Used When Split Transaction With Jump(Transfer) Functionality

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnSave", CommandType.StoredProcedure);//Add by gunjan:25/08/2023

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]); 
                    pClsProperty.ReturnValueJangedNo = Val.ToString(AL[3]);
                    pClsProperty.ReturnValueJangedNoRet = Val.ToString(AL[4]);
                    pClsProperty.ReturnValueJangedNoTran = Val.ToString(AL[5]);
                    pClsProperty.RETURNVALUEDEPTJANGEDNO = Val.ToString(AL[6]);//Comment by Gunjan:25/08/2023
                    pClsProperty.ReturnValueEmpRet_TRNID = Val.ToString(AL[7]);
                    pClsProperty.JANGEDNO = Val.ToInt64(pClsProperty.ReturnValueJangedNo);
                    pClsProperty.JANGEDNORet = Val.ToInt64(pClsProperty.ReturnValueJangedNoRet);
                    pClsProperty.JANGEDNOTran = Val.ToInt64(pClsProperty.ReturnValueJangedNoTran);
                    pClsProperty.DEPTJANGEDNO = Val.ToInt64(pClsProperty.RETURNVALUEDEPTJANGEDNO);
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


        public TrnSingleIssueReturnProperty ValiDationForReturnWithSplitProcess(TrnSingleIssueReturnProperty pClsProperty) // Dhara : 14-03-2024
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMDEPARTMENT_ID", pClsProperty.FROMDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMEMPLOYEE_ID", pClsProperty.FROMEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMPROCESS_ID", pClsProperty.FROMPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPROCESS_ID", pClsProperty.TOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NEXTPROCESS_ID", pClsProperty.NEXTPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("READYCARAT", pClsProperty.READYCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOSSCARAT", pClsProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_CheckPacketReturnWithSplitProcessValidation", CommandType.StoredProcedure);

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




        public TrnSingleIssueReturnProperty UpdateArtistLowestPlanTick(string pStrXmlArtistDetail, TrnSingleIssueReturnProperty pClsProperty) //#P : Used In Artist Lowest Plan : 15-07-2020
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("XMLARTISTDETAIL", pStrXmlArtistDetail, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdArtiestLowerPlanUpdate", CommandType.StoredProcedure);

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

        public TrnSinglePacketCreationProperty SplitPacketSave(TrnSinglePacketCreationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("MAINPACKET_ID", pClsProperty.MAINPACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pClsProperty.MAINMANAGER_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("LOTPCS", pClsProperty.LOTPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOTCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("REASON_ID", pClsProperty.REASON_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("MARKER_ID", pClsProperty.MARKER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("FROMEMPLOYEE_ID", pClsProperty.FROMEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("FROMMANAGER_ID", pClsProperty.FROMMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("FROMDEPARTMENT_ID", pClsProperty.FROMDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("FROMPROCESS_ID", pClsProperty.FROMPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPROCESS_ID", pClsProperty.TOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NEXTPROCESS_ID", pClsProperty.NEXTPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("JUMPRETURNPROCESS_ID", pClsProperty.JUMPRETURNPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLORSHADE_ID", pClsProperty.COLORSHADE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MILKY_ID", pClsProperty.MILKY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TENSION_ID", pClsProperty.TENSION_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETTYPE", pClsProperty.PACKETTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SPLITTRN_ID", pClsProperty.SPLITTRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("AUTOCONFIRM", pClsProperty.AUTOCONFIRM, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("EXPWEIGHT", pClsProperty.EXPWEIGHT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("PRDSHAPE_ID", pClsProperty.PRDSHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRDCTS", pClsProperty.PRDCTS, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("PACKETGROUP_ID", pClsProperty.PACKETGROUP_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PACKETGRADE_ID", pClsProperty.PACKETGRADE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PACKETCATEGORY_ID", pClsProperty.PACKETCATEGORY_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ISNEWTAG", pClsProperty.ISNEWTAG, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("PRDTAG", pClsProperty.PRDTAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("JUMPISSTOTRN", pClsProperty.JUMPISSTOTRN, DbType.String, ParameterDirection.Input); //#P : 06-06-2022
                //Ope.AddParams("JANGEDNORET", pClsProperty.JANGEDNORet, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("JANGEDNOTRAN", pClsProperty.JANGEDNOTRAN, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input); //HINAL : 21-06-2022
                Ope.AddParams("ISREJECTION", pClsProperty.ISREJECTION, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueTag", "", DbType.String, ParameterDirection.Output);  
                Ope.AddParams("ReturnValuePktSerialNo", "", DbType.Int32, ParameterDirection.Output);
                Ope.AddParams("ReturnValueBarcode", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNoTran", "", DbType.String, ParameterDirection.Output); //#P : 06-06-2022

                
                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnSplitPacketCreateNew", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                    pClsProperty.ReturnValuePacketID = Val.ToString(AL[3]);
                    pClsProperty.ReturnValueTag = Val.ToString(AL[4]);
                    pClsProperty.ReturnValuePktSerialNo = Val.ToInt32(AL[5]);
                    pClsProperty.ReturnValueBarcode = Val.ToString(AL[6]);
                    pClsProperty.JANGEDNOTRAN = Val.ToInt64(Val.ToString(AL[7]));
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

        public TrnSingleIssueReturnProperty ConfirmJanged(string pStrOpe, TrnSingleIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("CONFDATE", pClsProperty.CONFDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("CONFBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("CONFIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnConfirm", CommandType.StoredProcedure);

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


        public DataTable PopupJangedForPrint(string pStrFormType, Int64 pIntJangedNo = 0, string pStrDate = null, string pStrOpe = "", Int64 pIntDeptJangedNo = 0)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPTJANGEDNO", pIntDeptJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TRANSDATE", pStrDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnGetJangedNoPrint", CommandType.StoredProcedure);

            return DTab;
        }
        public DataTable PopupJangedForParcelPrint(string pStrFormType, Int64 pIntJangedNo = 0, string pStrDate = null, string pStrOpe = "") //#P : 24-08-2022
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TRANSDATE", pStrDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnGetJangedNoPrintForParcel", CommandType.StoredProcedure);
            return DTab;
        }


        public DataTable PopupJangedForPrintForLab(Int64 pIntJangedNo, double pDouExcRate)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EXCRATE", pDouExcRate, DbType.Double, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnGetJangedNoPrintForLab", CommandType.StoredProcedure);

            return DTab;
        }


        public DataSet GetPendingConfirmationData(Double pDouFromCarat, Double pDouToCarat)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("FROMCARAT", pDouFromCarat, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TOCARAT", pDouToCarat, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_SingleIssueReturnConfirmGetData", CommandType.StoredProcedure);

            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t End", BLL.GlobalDec.gStrComputerIP);

            return DS;
        }

        public DataTable GetPendingConfirmationDataWithScan(Int64 pIntJangedNo)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("CARAT", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnConfirmPktInfoGetData", CommandType.StoredProcedure);


            return DTab;
        }


        public DataTable GetCompleteGetData(Int64 pIntEmployee_ID) // Dhara : 08-04-2023
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketCompleteGetData", CommandType.StoredProcedure);

            return DTab;
        }

        public DataTable GetQCPanultyCompleteGetData(string pStrKapanName, Int32 pIntPacketNo, string pStrTag) // Dhara : 12-04-2023
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketQCPanulatyGetData", CommandType.StoredProcedure);

            return DTab;
        }



        /*
        public DataRow GetPacketInfo(string pStrKapanName,int pIntPacketNo,string pStrTag)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
           
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);

           return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketGetDataByPk", CommandType.StoredProcedure);

        }
        */
        public DataRow GetPacketInfo(string pStrKapanName, int pIntPacketNo, string pStrTag, string pIntBarcodeNo, Int32 pIntSrno, Int64 pIntJangedNo)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();

            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);

            Ope.AddParams("BARCODENO", pIntBarcodeNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PKTSERIALNO", pIntSrno, DbType.Int32, ParameterDirection.Input);

            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketGetDataByPk", CommandType.StoredProcedure);

        }

        public DataTable GetTransactionViewData(string Kapan, string SubLot, string SubLot1, int FromPacketNo, int ToPacketNo, string Tag, string FromDate, string ToDate, bool ISCurrent, string Barcode, Int64 pIntJangedNo)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("KAPANNAME", Kapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SUBLOT", SubLot, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SUBLOT1", SubLot1, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMPACKETNO", FromPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPACKETNO", ToPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", Tag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMTRANSDATE", FromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TOTRANSDATE", ToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ISCURRENT", ISCurrent, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("BARCODE", Barcode, DbType.String, ParameterDirection.Input); // K : 06/12/2022
            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnTransactionView", CommandType.StoredProcedure);
            return DTab;
        }


        public TrnSingleIssueReturnProperty DeleteTransaction(TrnSingleIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerMACID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnDelete", CommandType.StoredProcedure);

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



        public TrnSingleIssueReturnProperty UpdateTransaction(TrnSingleIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMPROCESS_ID", pClsProperty.FROMPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPROCESS_ID", pClsProperty.TOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NEXTPROCESS_ID", pClsProperty.NEXTPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                //Ope.AddParams("FROMDEPARTMENT_ID", pClsProperty.FROMDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("FROMEMPLOYEE_ID", pClsProperty.FROMEMPLOYEE_ID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("FROMMANAGER_ID", pClsProperty.FROMMANAGER_ID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("READYCARAT", pClsProperty.READYCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOSSCARAT", pClsProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOSTCARAT", pClsProperty.LOSTCARAT, DbType.Double, ParameterDirection.Input); //#p : 04-10-2022

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerMACID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);



                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnUpdate", CommandType.StoredProcedure);

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

        public TrnSingleIssueReturnProperty UpdateExpTransaction(TrnSingleIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("EXPCARAT", pClsProperty.EXPCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnExpCtsUpdate", CommandType.StoredProcedure);

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

        public DataTable GetNotificationData()
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleNotificationPendingJanged", CommandType.StoredProcedure);

            return DTab;
        }

        public int InsertNotification(string pStrNotificationType, string pStrMessage)
        {
            string Str = "Insert Into trn_Notification With(ROWLOCK) (id,NType,NMessage,EntryBy,EntryIP,EntryDate) ";
            Str = Str + "Values(NEWID(),";
            Str = Str + "'" + pStrNotificationType + "',";
            Str = Str + "'" + pStrMessage + "',";
            Str = Str + "'" + Config.gEmployeeProperty.LEDGER_ID + "',";
            Str = Str + "'" + Config.ComputerIP + "',";
            Str = Str + "GETDATE())";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);

        }

        public int UpdateLoguteDateTime() //Add : Pinali : 16-10-2019 : Used When Exe Close
        {
            string Str = "Update HST_Login With(ROWLOCK) SET LogoutDateTime = GETDATE() WHERE HST_ID = " + "'" + Config.gEmployeeProperty.LOGINHST_ID + "'";
            //Str = Str + "Values(NEWID(),";
            //Str = Str + "'" + pStrNotificationType + "',";
            //Str = Str + "'" + pStrMessage + "',";
            //Str = Str + "'" + Config.gEmployeeProperty.LEDGER_ID + "',";
            //Str = Str + "'" + Config.ComputerIP + "',";
            //Str = Str + "GETDATE())";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);

        }

        //public DataRow GetFinalEmployeeIssPacketInfo(string pStrKapanName, int pIntPacketNo, string pStrTag, int pIntProcessID, string pStrProcessName)
        //{
        //    //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
        //    Ope.ClearParams();

        //    Ope.AddParams("OPE", "PACKETINFO", DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
        //    Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);

        //    Ope.AddParams("FROMDEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
        //    Ope.AddParams("FROMEMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

        //    return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnFinalIssueGetData", CommandType.StoredProcedure);
        //}
        public DataTable GetFinalEmployeeIssPacketInfo(Int64 pIntEmployeeID, string pStrKapanName, int pIntPacketNo, string pStrTag, string pStrBarcode, int pStrSrNoSerialNo, int pIntProcessID, string pStrProcessName) //Changed : Pinali : 10-08-2019
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            DataTable DTab = new DataTable();
            Ope.ClearParams();


            Ope.AddParams("OPE", "PACKETINFO", DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pStrBarcode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PKTSERIALNO", pStrSrNoSerialNo, DbType.Int32, ParameterDirection.Input);

            Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FROMDEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("FROMEMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FINALEMPLOYEE_ID", pIntEmployeeID, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnFinalIssueGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public DataTable GetFinalEmployeeIssEmployee(string pStrKapanName, int pIntPacketNo, string pStrTag, int pIntProcessID, string pStrProcessName)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("OPE", "EMPINFO", DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FROMDEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("FROMEMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnFinalIssueGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public DataRow GetFinalEmployeeReturnJamaPacketInfo(string pStrKapanName, int pIntPacketNo, string pStrTag, int pIntProcessID, string pStrProcessName)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();

            Ope.AddParams("OPE", "PACKETINFO", DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FROMDEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("FROMEMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnFinalReturnGetData", CommandType.StoredProcedure);

        }
        public DataTable GetFinalEmployeeReturnJama(string pStrKapanName, int pIntPacketNo, string pStrTag, int pIntProcessID, string pStrProcessName)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("OPE", "EMPINFO", DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FROMDEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("FROMEMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnFinalReturnGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable CheckStaffIssueValidationLockAmountSettingWise(DataTable DtPackets, int ForProcess_ID, Int64 pIntEmployee_ID) //Add : Pinali : 06-05-2019
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("TBL_StaffIssuePacketInfo", DtPackets, DbType.Object, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", ForProcess_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_CheckValidationLockAmountWise", CommandType.StoredProcedure);

            return DTab;
        }
        public DataTable CheckFinalIssueValForPrd(string pStrKapan, int pIntPacketNo, string pStrTag, string pStrPrd_ID) //Add : Pinali : 13-08-2019
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            string strQuery = "Select * From Trn_SinglePrd P With(Nolock) Where P.KapanName = '" + pStrKapan + "' and p.PacketNo = " + pIntPacketNo + " And Tag = '" + pStrTag + "' ";
            strQuery = strQuery + " And Prd_ID = '" + pStrPrd_ID + "'";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, strQuery, CommandType.Text);
            return DTab;
        }

        public DataTable TransferToMix(string pStrXmlParcelPacketDetail) //Add : Pinali : 06-09-2019 : Used In Grd Live Stock
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("XMLPARCELPACKETDETAIL", pStrXmlParcelPacketDetail, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketsTransferToMixSave", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable ValCheckPacketTFlagPrdExistsOrNot(string pStrXmlPacketsDetail) //Add : Pinali : 21-09-2019 : Used When Packets Split
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("XMLPACKETSDETAIL", pStrXmlPacketsDetail, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "ValCheckPacketsTFlagPrdExistsOrNot", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable CheckPacketFullTopValidation(string pStrCheckPacketsListXML) //Add : Pinali : 16-07-2020
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("XMLISSUEPACKETSLIST", pStrCheckPacketsListXML, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_CheckPacketFullTopValidation", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable CheckPacketProcessReturnValidation(string pStrCheckPacketsListXML,string pStrEntryType) //Add : Pinali : 20-08-2020 : 
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("XMLISSUEPACKETSLIST", pStrCheckPacketsListXML, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("ENTRYTYPE", pStrEntryType, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_CheckPacketReturnProcessValidation", CommandType.StoredProcedure);
            return DTab;
        }


        public DataTable CheckValidationFor4POKProcess(string pStrCheckPacketsListXML,   Int32 pIntProces_ID, Int32 pIntToDepartment_ID) //Add : Dhara : 24-06-2024
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("XMLISSUEPACKETSLIST", pStrCheckPacketsListXML, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("TODEPARTMENT_ID", pIntToDepartment_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPROCESS_ID", pIntProces_ID, DbType.Int32, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_CheckValidationFor4POkProcess", CommandType.StoredProcedure);

            return DTab;
        }


        public DataTable CheckFactoryIssueLockValidation(Int64 pIntBarcode, string pStrKapanname, Int32 pIntPacketNo, string pStrTag, Int32 pIntPktSrno, Int32 pIntProcess_ID, Int32 pIntDepartment_ID) //Add : Dhara : 10-06-2024 
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("BARCODE", pIntBarcode, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanname, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PKTSRNO", pIntPktSrno, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", pIntProcess_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartment_ID, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_CheckFactoryLockIssueValidation", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable CheckPacketPrdSameValidation(string pStrCheckPacketsListXML, int FromPrdtype_ID, int ToPrdtype_ID) //Add : Pinali : 04-06-2020
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("XMLCHECKPACKETSLIST", pStrCheckPacketsListXML, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("CHKFROMPRDTYPE_ID", FromPrdtype_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("CHKTOPRDTYPE_ID", ToPrdtype_ID, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_CheckValidationForSamePacketPrediction", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetDataForArtistLowerPlanTick(string pStrOpe, string pStrKapanName, int pIntPacketNo, string pStrTag) //#P : 15-07-2020 : Used In ArtiestLowerPlanTick
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdArtiestLowerPlanGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetDataForSplitWithPrd(string pStrKapanName, int pIntPacketNo, string pStrTag) // #D: 13/07/2021 : For Get Dat From Trn_SinglePrd With Prdtype_ID = 2 AND ISFINAL = 2 AND NOT EXISTS IN TRN_SINGLEPACKETMASTER
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SingleSplitWithPrdGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public Int64 SaveGroupJanged(string pStrGroupJangedXML)
        {
            string ReturnValue = "";
            string ReturnMessageType = "";
            string ReturnMessageDesc = "";
            string ReturnValueJangedNo = "";
            Int64 pIntJangedNo = 0;

            try
            {
                Ope.ClearParams();

                Ope.AddParams("GROUJANGEDXML", pStrGroupJangedXML, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SinglePacketCreateGroupJangednoSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    ReturnValue = Val.ToString(AL[0]);
                    ReturnMessageType = Val.ToString(AL[1]);
                    ReturnMessageDesc = Val.ToString(AL[2]);
                    ReturnValueJangedNo = Val.ToString(AL[3]);

                    pIntJangedNo = Val.ToInt64(ReturnValueJangedNo);
                }

            }
            catch (System.Exception ex)
            {
                ReturnValue = "";
                ReturnMessageType = "FAIL";
                ReturnMessageDesc = ex.Message;
            }
            return pIntJangedNo;

        }
        public DataSet GetPendingConfirmationDataParcel() // Dhara : 21-04-2022
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_ParcelIssueReturnConfirmGetData", CommandType.StoredProcedure);
            return DS;
        }
        public TrnSingleIssueReturnProperty DeleteParcelTransaction(TrnSingleIssueReturnProperty pClsProperty) // D: 21-04-2022
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_ParcelIssueReturnDelete", CommandType.StoredProcedure);

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
        public DataTable GetTransactionViewDataForParcel(string Kapan, string SubLot, string SubLot1, int FromPacketNo, int ToPacketNo, string Tag, string FromDate, string ToDate, bool ISCurrent, string pStrDepartment_ID) // Dhara : 21-04-22
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("KAPANNAME", Kapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SUBLOT", SubLot, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SUBLOT1", SubLot1, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMPACKETNO", FromPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPACKETNO", ToPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", Tag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMTRANSDATE", FromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TOTRANSDATE", ToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ISCURRENT", ISCurrent, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pStrDepartment_ID, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_MixIssueReturnTransactionView", CommandType.StoredProcedure);
            return DTab;
        } // Add: 21-04-2022 : Dhara
        public TrnSingleIssueReturnProperty TransferGoodsParcel(TrnSingleIssueReturnProperty pClsProperty) // Dhara : 21-04-2022
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("MAINPACKET_ID", pClsProperty.MAINPACKET_ID, DbType.Guid, ParameterDirection.Input); //#P : 11-04-2021

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYSRNO", pClsProperty.ENTRYSRNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYTYPE", pClsProperty.ENTRYTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMDEPARTMENT_ID", pClsProperty.FROMDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("FROMEMPLOYEE_ID", pClsProperty.FROMEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("FROMPROCESS_ID", pClsProperty.FROMPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPROCESS_ID", pClsProperty.TOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NEXTPROCESS_ID", pClsProperty.NEXTPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("FROMMANAGER_ID", pClsProperty.FROMMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ISSUEPCS", pClsProperty.ISSUEPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("READYPCS", pClsProperty.READYPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("READYCARAT", pClsProperty.READYCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("RRPCS", pClsProperty.RRPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RRCARAT", pClsProperty.RRCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("SECONDPCS", pClsProperty.SECONDPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SECONDCARAT", pClsProperty.SECONDCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("EXTRAPCS", pClsProperty.EXTRAPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EXTRACARAT", pClsProperty.EXTRACARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("LOSTPCS", pClsProperty.LOSTPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOSTCARAT", pClsProperty.LOSTCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("LOSSCARAT", pClsProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MIXINGLESSPLUS", pClsProperty.MIXINGLESSPLUS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RETURNTYPE", pClsProperty.RETURNTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TRANSDATE", pClsProperty.TRANSDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TRANSBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TRANSIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TRANSTYPE", pClsProperty.TRANSTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("AUTOCONFIRM", pClsProperty.AUTOCONFIRM, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ISMERGE", pClsProperty.ISMERGE, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_MixIssueReturnSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                    pClsProperty.ReturnValueJangedNo = Val.ToString(AL[3]);

                    pClsProperty.JANGEDNO = Val.ToInt64(pClsProperty.ReturnValueJangedNo);
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
        public TrnSingleIssueReturnProperty ConfirmJangedForParcel(string pStrOpe, TrnSingleIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);

                //Ope.AddParams("CONFDATE", pClsProperty.CONFDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CONFBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("CONFIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_MixIssueReturnConfirm", CommandType.StoredProcedure);

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
        public DataTable CheckProcessName(Int64 pIntDepartment_ID) //Urvisha : 09-08-2022
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("DEPARTMENT_ID", pIntDepartment_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_DepartmentPopupProcessGetData", CommandType.StoredProcedure);
            return DTab;

        }
        public string SingleTransactionUpdate(string strXml, int Employee_ID, int Department_ID, int Manager_ID, int Process_ID, string EntryType)
        {
            Ope.ClearParams();

            Ope.AddParams("XML", strXml, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("TOEMPLOYEE_ID", Employee_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TODEPARTMENT_ID", Department_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOMANAGER_ID", Manager_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPROCESS_ID", Process_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("ENTRYTYPE", EntryType, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);


            int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "TRN_SingleTransactionUpdate", CommandType.StoredProcedure);

            if (IntRes > 0)
            {
                return "SUCCESS";
            }
            else
            {
                return "FAIL";
            }
        }
        public DataTable GetDataForTransfer(string StrOpe, string pStrDisplay, string StrXML) // Krina : 17/09/2022
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);
            Ope.AddParams("XML", StrXML, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketGradingGetDataForTransfer", CommandType.StoredProcedure);
            return DTab;

        }
        public int FileUploadDownloadHistorySave(string Trn_ID,
            string Packet_ID,
            string PacketNo,
            string ButtonName,
            string Operation,
            int Process_ID,
            string ResponseType,
            string ResponseMessage,
            string FileName,
            string ServerPath,
            string LocalPath,
            int FileSize)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", Trn_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", Packet_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", PacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BUTTONNAME", ButtonName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPERATION", Operation, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", Process_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RESPONSETYPE", ResponseType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RESPONSEMESSAGE", ResponseMessage, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FILENAME", FileName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SERVERPATH", ServerPath, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LOCALPATH", LocalPath, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FILESIZE", FileSize, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Trn_UploadDownloadHistorySave", CommandType.StoredProcedure);

            }
            catch (System.Exception ex)
            {

            }
            return -1;
        }
        public int GetMaxDeptJangedNoStr() //#K : 19-10-2022
        {
            string Query = "";
            DataTable Dtab = new DataTable();
            int IntDeptJangedNo = 0;
            Ope.ClearParams();

            Ope.AddParams("RETURNVALUEJDEPTANGEDNO", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnMaxDeptJangedGenerate", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                IntDeptJangedNo = Val.ToInt(AL[0]);
            }
            else
            {
                IntDeptJangedNo = -1;
            }
            return IntDeptJangedNo;
        }
        public string SaveNewDeptJangedNoStr(string newJangedXml, string StrConfDate) //#K : 19-10-2022
        {
            string ReturnValue = "";
            string ReturnMessageType = "";
            string ReturnMessageDesc = "";
            try
            {
                Ope.ClearParams();

                Ope.AddParams("NEWJANGEDXML", newJangedXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("CONFDATE", StrConfDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("CONFBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("CONFIP", Config.ComputerMACID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SinglePacketConfirmationSaveDeptJangedNo", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    ReturnValue = Val.ToString(AL[0]);
                    ReturnMessageType = Val.ToString(AL[1]);
                    ReturnMessageDesc = Val.ToString(AL[2]);
                }
            }
            catch (System.Exception ex)
            {
                ReturnValue = "";
                ReturnMessageType = "FAIL";
                ReturnMessageDesc = ex.Message;
            }
            return ReturnMessageDesc;

        }
        public DataTable PopupDeptJangedForPrint(int pStrJangedNo, string pStrDate = null, string pStrOpe = "") //Krina
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("DEPTJANGEDNO", pStrJangedNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("JANGEDDATE", pStrDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SlipPrintDeptJangedNoWise", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable PopupDeptJangedMultiForPrint(string pStrJangedNo, string pStrDate = null, string pStrOpe = "") //Krina
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("DEPTJANGEDNO", pStrJangedNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDDATE", pStrDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SlipPrintDeptJangedNoWise", CommandType.StoredProcedure);
            return DTab;
        }

        //public DataTable GetQCPanultyCompleteGetData(string pStrKapanName, Int32 pIntPacketNo, string pStrTag) // Dhara : 12-04-2023
        //{
        //    Ope.ClearParams();
        //    DataTable DTab = new DataTable();

        //    Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
        //    Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
        //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketQCPanulatyGetData", CommandType.StoredProcedure);

        //    return DTab;
        //}//Gunjan:19/04/2023

        public TrnSingleIssueReturnProperty UpdateLotCarat(TrnSingleIssueReturnProperty pClsProperty) // DHARA : 27032024
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOTCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnLotCaratUpdate", CommandType.StoredProcedure);

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

        public TrnSingleIssueReturnProperty UpdateLossCarat(TrnSingleIssueReturnProperty pClsProperty) // DHARA : 27032024
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnLossCaratUpdate", CommandType.StoredProcedure);

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

        public TrnSingleIssueReturnProperty UpdateLotPCS(TrnSingleIssueReturnProperty pClsProperty) // DHARA : 27032024
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOTPCS", pClsProperty.LOTPCS, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SingleIssueReturnPcsUpdate", CommandType.StoredProcedure);

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

        public string UpdatePacketGrade(Int64 pIntKapan_ID, Int32 pIntFromPkt, Int32 pIntToPkt, string pStrTag, int pIntPacketGarde_ID) // D: 18-02-2021
        {
            string ReturnValue = "";
            string ReturnMessageType = "";
            string ReturnMessageDesc = "";

            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pIntKapan_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMPACKET", pIntFromPkt, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKET", pIntToPkt, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETGRADE_ID", pIntPacketGarde_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PacketGradeUpdate", CommandType.StoredProcedure);
                if (AL.Count != 0)
                {
                    ReturnValue = Val.ToString(AL[0]);
                    ReturnMessageType = Val.ToString(AL[1]);
                    ReturnMessageDesc = Val.ToString(AL[2]);
                }

            }
            catch (Exception Ex)
            {
                ReturnValue = "";
                ReturnMessageType = "FAIL";
                ReturnMessageDesc = Ex.Message;
            }
            return ReturnMessageType;

        }

        public DataTable GetDataForEmployeeTransfer(string StrOpe, Int64 pIntManager_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", pIntManager_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_EmployeeTransferToWithManager", CommandType.StoredProcedure);
            return DTab;

        }
    }
}
