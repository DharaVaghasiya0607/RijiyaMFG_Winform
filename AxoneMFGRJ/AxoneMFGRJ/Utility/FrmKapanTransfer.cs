using AxoneMFGRJ.Utility;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Utility
{
    public partial class FrmKapanTransfer : Form
    {
        BODevGridSelection ObjSelMaster;
        BODevGridSelection ObjSelTransaction;
        BODevGridSelection ObjSelPricing;

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabMasterData = new DataTable();
        DataTable DTabTransactionData = new DataTable();
        DataTable DTabPricingData = new DataTable();

        bool ISTransactionSaveSuccessfully = false;
        bool ISMasterSaveSuccessfully = false;
        bool ISPricingSaveSuccessfully = false;

        string mStrRapDate = "";

        bool ISTransactionDeleteSuccessfully = false;

        #region ShowForm

        public FrmKapanTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            DataSet DSet = new BOMST_FormPermission().GetDataKapanTransferTableList();

            if (DSet.Tables.Count > 0)
            {
                DTabMasterData = DSet.Tables[0];
                DTabTransactionData = DSet.Tables[1];
                DTabPricingData = DSet.Tables[2];
            }

            

            MainGrdTransaction.DataSource = DTabTransactionData;
            GrdDetTransaction.RefreshData();


            //For Transaction
            if (MainGrdTransaction.RepositoryItems.Count == 0)
            {
                ObjSelTransaction = new BODevGridSelection();
                ObjSelTransaction.View = GrdDetTransaction;
                ObjSelTransaction.ClearSelection();
                ObjSelTransaction.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjSelTransaction.ClearSelection();
            }

            

            //GrdDetMaster.BestFitColumns();
            //GrdDetTransaction.BestFitColumns();
            //GrdDetPricing.BestFitColumns();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(BOConfiguration.ConnectionString);
            txtSourceServer.Text = Val.ToString(builder.DataSource);
            txtSourceDBName.Text = Val.ToString(builder.InitialCatalog);
            txtSourceUsername.Text = Val.ToString(builder.UserID);
            txtSourcePassword.Text = Val.ToString(builder.Password);

            SqlConnectionStringBuilder builderTransferDB = new SqlConnectionStringBuilder(BOConfiguration.ConnectionString_TransferDB);
            txtDestinationServer.Text = Val.ToString(builderTransferDB.DataSource);
            txtDestinationDBName.Text = Val.ToString(builderTransferDB.InitialCatalog);
            txtDestinationUsername.Text = Val.ToString(builderTransferDB.UserID);
            txtDestinationPassword.Text = Val.ToString(builderTransferDB.Password);

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";


            DataTable DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
            DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
            DTabRapDate = DTabRapDate.DefaultView.ToTable();

        }

        private void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyPress = false;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(DTabMasterData);
            ObjFormEvent.ObjToDisposeList.Add(DTabTransactionData);
            ObjFormEvent.ObjToDisposeList.Add(DTabPricingData);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Obj);
        }


        #endregion


        private string BulkCopyMaster(string pStrTableName, DataRow pDRow)
        {
            string Str = string.Empty;
            try
            {
                SqlConnectionStringBuilder Src = new SqlConnectionStringBuilder();
                Src.DataSource = txtSourceServer.Text;
                Src.InitialCatalog = txtSourceDBName.Text;
                Src.UserID = txtSourceUsername.Text;
                Src.Password = txtSourcePassword.Text;

                SqlConnectionStringBuilder Dest = new SqlConnectionStringBuilder();
                Dest.DataSource = txtDestinationServer.Text;
                Dest.InitialCatalog = txtDestinationDBName.Text;
                Dest.UserID = txtDestinationUsername.Text;
                Dest.Password = txtDestinationPassword.Text;

                string Source = Src.ConnectionString;
                string Destination = Dest.ConnectionString;


                ////#P : 09-03-2021

                //using (SqlConnection sourceCon = new SqlConnection(Source))
                //{
                //    sourceCon.Open();
                //    using (SqlConnection destinationCon = new SqlConnection(Destination))
                //    {
                //        destinationCon.Open();
                //        CreateCopyTable(pStrTableName, sourceCon, destinationCon);
                //    }
                //    Str = "DONE";
                //}
                //return Str;

                //End : #P : 09-03-2021


                using (SqlConnection sourceCon = new SqlConnection(Source))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * From " + pStrTableName + " With(NOLOCK) ", sourceCon);
                    sourceCon.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        using (SqlConnection destinationCon = new SqlConnection(Destination))
                        {
                            using (SqlBulkCopy bc = new SqlBulkCopy(destinationCon))
                            {
                                destinationCon.Open();

                                SqlCommand cmd1 = new SqlCommand("Truncate Table " + pStrTableName + " ", destinationCon);
                                cmd1.CommandTimeout = 999999999;
                                cmd1.ExecuteNonQuery();

                                bc.BatchSize = 10000;
                                bc.NotifyAfter = 5000;
                                bc.BulkCopyTimeout = 0;
                                bc.SqlRowsCopied += (sender, eventArgs) =>
                                {
                                    pDRow["STATUS"] = "Running : " + eventArgs.RowsCopied + " loaded....";
                                    //lblMsg.Text = "In " + bc.BulkCopyTimeout + " Sec " + eventArgs.RowsCopied + "Copied.";
                                };

                                bc.DestinationTableName = pStrTableName;

                                for (int col = 0; col < rdr.FieldCount; col++)
                                {
                                    bc.ColumnMappings.Add(rdr.GetName(col), rdr.GetName(col));
                                }

                                bc.WriteToServer(rdr);

                                destinationCon.Close();
                                sourceCon.Close();

                                Str = "DONE";
                                ISMasterSaveSuccessfully = true;
                            }
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                ISMasterSaveSuccessfully = false;
                Str = "Error !! " + EX.Message;
            }

            return Str;
        }
        private string BulkCopyPricing(string pStrTableName, DataRow pDRow, string pStrRapDate)
        {
            string Str = string.Empty;
            try
            {
                SqlConnectionStringBuilder Src = new SqlConnectionStringBuilder();
                Src.DataSource = txtSourceServer.Text;
                Src.InitialCatalog = txtSourceDBName.Text;
                Src.UserID = txtSourceUsername.Text;
                Src.Password = txtSourcePassword.Text;

                SqlConnectionStringBuilder Dest = new SqlConnectionStringBuilder();
                Dest.DataSource = txtDestinationServer.Text;
                Dest.InitialCatalog = txtDestinationDBName.Text;
                Dest.UserID = txtDestinationUsername.Text;
                Dest.Password = txtDestinationPassword.Text;

                string Source = Src.ConnectionString;
                string Destination = Dest.ConnectionString;

                using (SqlConnection sourceCon = new SqlConnection(Source))
                {
                    sourceCon.Open();
                    SqlCommand cmd = new SqlCommand();
                    //cmd.CommandTimeout = 999999999;
                    cmd.Parameters.Add(new SqlParameter("@TABLENAME", pStrTableName));
                    cmd.Parameters.Add(new SqlParameter("@KAPANNAME", ""));
                    cmd.Parameters.Add(new SqlParameter("@RAPDATE", Val.SqlDate(pStrRapDate)));
                    cmd.Parameters.Add(new SqlParameter("@OPE", "SEARCH_PRICING"));
                    cmd.CommandText = "UTI_KapanTransferCopyDetail";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sourceCon;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        using (SqlConnection destinationCon = new SqlConnection(Destination))
                        {
                            using (SqlBulkCopy bc = new SqlBulkCopy(destinationCon))
                            {
                                destinationCon.Open();

                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Parameters.Add(new SqlParameter("@TABLENAME", pStrTableName));
                                cmd1.Parameters.Add(new SqlParameter("@KAPANNAME", ""));
                                cmd1.Parameters.Add(new SqlParameter("@RAPDATE", Val.SqlDate(pStrRapDate)));
                                cmd1.Parameters.Add(new SqlParameter("@OPE", "DELETE_PRICING"));
                                cmd1.CommandText = "UTI_KapanTransferCopyDetail";
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Connection = destinationCon;
                                cmd1.CommandTimeout = 999999999;
                                cmd1.ExecuteNonQuery();

                                bc.BatchSize = 10000;
                                bc.NotifyAfter = 5000;
                                bc.BulkCopyTimeout = 0;
                                bc.SqlRowsCopied += (sender, eventArgs) =>
                                {
                                    pDRow["STATUS"] = "Running : " + eventArgs.RowsCopied + " loaded....";
                                    //lblMsg.Text = "In " + bc.BulkCopyTimeout + " Sec " + eventArgs.RowsCopied + "Copied.";
                                };

                                bc.DestinationTableName = pStrTableName;

                                for (int col = 0; col < rdr.FieldCount; col++)
                                {
                                    bc.ColumnMappings.Add(rdr.GetName(col), rdr.GetName(col));
                                }
                                bc.WriteToServer(rdr);

                                destinationCon.Close();
                                sourceCon.Close();

                                Str = "DONE";
                                ISPricingSaveSuccessfully = true;
                            }
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                ISPricingSaveSuccessfully = false;
                Str = "Error !! " + EX.Message;
            }

            return Str;
        }


        private string BulkCopyTransaction(string pStrTableName, DataRow pDRow, string pStrKapanName) //#P : 09-03-2021
        {
            string Str = string.Empty;
            try
            {
                SqlConnectionStringBuilder Src = new SqlConnectionStringBuilder();
                Src.DataSource = txtSourceServer.Text;
                Src.InitialCatalog = txtSourceDBName.Text;
                Src.UserID = txtSourceUsername.Text;
                Src.Password = txtSourcePassword.Text;

                SqlConnectionStringBuilder Dest = new SqlConnectionStringBuilder();
                Dest.DataSource = txtDestinationServer.Text;
                Dest.InitialCatalog = txtDestinationDBName.Text;
                Dest.UserID = txtDestinationUsername.Text;
                Dest.Password = txtDestinationPassword.Text;

                string Source = Src.ConnectionString;
                string Destination = Dest.ConnectionString;


                string StrDeleteQuery = "";

                using (SqlConnection sourceCon = new SqlConnection(Source))
                {
                    sourceCon.Open();
                    SqlCommand cmd = new SqlCommand();
                    //cmd.CommandTimeout = 999999999;
                    cmd.Parameters.Add(new SqlParameter("@TABLENAME", pStrTableName));
                    cmd.Parameters.Add(new SqlParameter("@KAPANNAME", pStrKapanName));
                    cmd.Parameters.Add(new SqlParameter("@OPE", "SEARCH"));
                    cmd.CommandText = "UTI_KapanTransferCopyDetail";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sourceCon;
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        using (SqlConnection destinationCon = new SqlConnection(Destination))
                        {
                            using (SqlBulkCopy bc = new SqlBulkCopy(destinationCon))
                            {
                                destinationCon.Open();

                                ////Jo Table ma KapnaName ni Column hase to KapanName thi Delete thase
                                //StrDeleteQuery = "";
                                //StrDeleteQuery = "IF Exists (Select 1 From sys.columns WHERE Name = N'KapanName' And Object_ID = Object_ID(N'" + pStrTableName + "')) Begin";
                                //StrDeleteQuery = StrDeleteQuery + " Delete From " + pStrTableName + " With(RowlocK) Where KapanName = '" + pStrKapanName + "'  End";

                                ////Jo Table ma Kapan_ID ni Column hase to Kapan_ID Nu Joi Lagine KapanName thi Delete thase
                                //StrDeleteQuery = StrDeleteQuery + " Else IF Exists (Select 1 From sys.columns WHERE Name = N'Kapan_ID' And Object_ID = Object_ID(N'" + pStrTableName + "')) Begin";
                                //StrDeleteQuery = StrDeleteQuery + " Delete A With(Rowlock) From " + pStrTableName + " A Inner Join Trn_Kapan Kp With(Nolock) On Kp.Kapan_ID = A.Kapan_ID Where Kp.KapanName = '" + pStrKapanName + "'  End";

                                //SqlCommand cmd1 = new SqlCommand(StrDeleteQuery, destinationCon);
                                //cmd1.CommandTimeout = 999999999;
                                //cmd1.ExecuteNonQuery();

                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Parameters.Add(new SqlParameter("@TABLENAME", pStrTableName));
                                cmd1.Parameters.Add(new SqlParameter("@KAPANNAME", pStrKapanName));
                                cmd1.Parameters.Add(new SqlParameter("@OPE", "DELETE"));
                                cmd1.CommandText = "UTI_KapanTransferCopyDetail";
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Connection = destinationCon;
                                cmd1.CommandTimeout = 999999999;
                                cmd1.ExecuteNonQuery();

                                bc.BatchSize = 10000;
                                bc.NotifyAfter = 5000;
                                bc.BulkCopyTimeout = 0;
                                bc.SqlRowsCopied += (sender, eventArgs) =>
                                {
                                    pDRow["STATUS"] = "Running : " + eventArgs.RowsCopied + " loaded....";
                                    //lblMsg.Text = "In " + bc.BulkCopyTimeout + " Sec " + eventArgs.RowsCopied + "Copied.";
                                };

                                bc.DestinationTableName = pStrTableName;

                                for (int col = 0; col < rdr.FieldCount; col++)
                                {
                                    bc.ColumnMappings.Add(rdr.GetName(col), rdr.GetName(col));
                                }


                                bc.WriteToServer(rdr);

                                destinationCon.Close();
                                sourceCon.Close();

                                Str = "DONE";
                                ISTransactionSaveSuccessfully = true;
                            }
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                ISTransactionSaveSuccessfully = false;
                Str = "Error !! " + EX.Message;
            }

            return Str;
        }


        private void CreateCopyTable(string StrDestinationTable, SqlConnection StrSourceConn, SqlConnection StrDestinationConn)
        {
            try
            {
                //#P : 09-03-2021
                DataTable DTBulkTransfer = new DataTable();

                //var checkTableIfExistsCommand = new SqlCommand("IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  '" + pStrTableName + "') SELECT 1 ELSE SELECT 0", destinationCon);
                SqlCommand cmd1 = new SqlCommand("IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  '" + StrDestinationTable + "') DROP TABLE " + StrDestinationTable + " ", StrDestinationConn);
                cmd1.CommandTimeout = 999999999;
                cmd1.ExecuteNonQuery();


                //StrSourceConn.Open();
                SqlCommand cmd2 = new SqlCommand("Select column_name,data_type,character_maximum_length From information_schema.columns Where table_name = '" + StrDestinationTable + "'", StrSourceConn);
                cmd2.CommandTimeout = 999999999;
                SqlDataAdapter ad = new SqlDataAdapter(cmd2);
                ad.Fill(DTBulkTransfer);
                StringBuilder createTableBuilder;

                //sbu = new StringBuilder(string.Format("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')) DROP TABLE [dbo].[{0}] ", StrDestinationTable));
                //sbu.Append("Create Table " + tableName + " (");

                //string dataType = string.Empty;

                createTableBuilder = new StringBuilder("CREATE TABLE [" + StrDestinationTable + "]");
                createTableBuilder.AppendLine("(");

                foreach (DataRow Dr in DTBulkTransfer.Rows)
                {
                    string StrColumnName = "";
                    string StrDataType = "";

                    StrColumnName = Val.ToString(Dr["column_name"]);
                    StrDataType = Val.ToString(Dr["character_maximum_length"]).Trim().Equals(string.Empty) ? Val.ToString(Dr["data_type"]) : Val.ToString(Dr["data_type"]) + "(" + Val.ToString(Dr["character_maximum_length"]) + ")";
                    //createTableBuilder.AppendLine("  [" + StrColumnName + "] VARCHAR(MAX),");
                    createTableBuilder.AppendLine("  [" + StrColumnName + "] " + StrDataType + ",");
                }
                createTableBuilder.Remove(createTableBuilder.Length - 1, 1);
                createTableBuilder.AppendLine(")");

                cmd1 = new SqlCommand(createTableBuilder.ToString(), StrDestinationConn);
                cmd1.CommandTimeout = 999999999;
                cmd1.ExecuteNonQuery();

                using (var bulkCopy = new SqlBulkCopy(StrDestinationConn))
                {
                    bulkCopy.DestinationTableName = StrDestinationTable;
                    bulkCopy.WriteToServer(DTBulkTransfer);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect, BODevGridSelection pSelection)
        {
            if (view.RowCount <= 0)
            {
                return null;
            }
            ArrayList AL = new ArrayList();
            DataTable DTabResult = new DataTable();
            DataTable DTabSource = ((DataView)view.DataSource).Table;            
            if (IsSelect)
            {
                AL = pSelection.GetSelectedArrayList();
                DTabResult = DTabSource.Clone();
                for (int i = 0; i < AL.Count; i++)
                {
                    DataRowView oDataRowView = AL[i] as DataRowView;
                    DTabResult.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }

            return DTabResult;
        }

        
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void bgWorkerTransaction_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDetTransaction, true, ObjSelTransaction);

            string[] StrKapans = Val.ToString(CmbKapan.Text).Trim().Replace(" ", "").Split(',');
            string StrKapanName = "";

            for (int IntK = 0; IntK < StrKapans.Length; IntK++)
            {
                StrKapanName = StrKapans[IntK];

                if (StrKapanName.Trim().Equals(string.Empty))
                    continue;

                for (int IntI = 0; IntI < GrdDetTransaction.RowCount; IntI++)
                {
                    DataRow DRow = GrdDetTransaction.GetDataRow(IntI);
                    if (Val.ToBoolean(GrdDetTransaction.GetRowCellValue(IntI, "COLSELECTCHECKBOX")) == true)
                    {
                        DRow["STATUS"] = "Running";
                        DRow["STATUS"] = BulkCopyTransaction(Val.ToString(DRow["TABLENAME"]), DRow, StrKapanName);
                    }
                }
            }
        }

        private void bgWorkerTransaction_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanelTransaction.Visible = false;
            BtnTransferTransaction.Enabled = true;
            BtnDeleteTransaction.Enabled = true;
            //Global.Message("TRANSACTION DATA TRANSFER DONE!!!!");

            if (ISTransactionSaveSuccessfully == false)
            {
                lblMessageTransaction.ForeColor = Color.Maroon;
                lblMessageTransaction.Text = "TRANSACTION DATA TRANSFER GETS SOME ERROR PLS CHECK...!!!!";
            }
            else
            {
                lblMessageTransaction.ForeColor = Color.DarkGreen;
                lblMessageTransaction.Text = "TRANSACTION DATA TRANSFER DONE...!!!!";
            }
            ISTransactionSaveSuccessfully = false;
        }

        

        private void BtnTransferTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(CmbKapan.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Kapan That You Wan't To Transfer...");
                    CmbKapan.Focus();
                    return;
                }

                progressPanelTransaction.Visible = true;
                BtnTransferTransaction.Enabled = false;
                BtnDeleteTransaction.Enabled = false;
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                if (!bgWorkerTransaction.IsBusy)
                {
                    bgWorkerTransaction.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                progressPanelTransaction.Visible = false;
                BtnTransferTransaction.Enabled = true;
                BtnDeleteTransaction.Enabled = true;
                ISTransactionSaveSuccessfully = false;
                Global.Message(ex.Message);
            }
        }

        
        private void BtnDeleteTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(CmbKapan.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Kapan That You Wan't To Delete...");
                    CmbKapan.Focus();
                    return;
                }

                if (Global.Confirm("Are You Sure To Delete Transaction Of Kapan's From Source Database.. ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                progressPanelTransactionDelete.Visible = true;
                BtnDeleteTransaction.Enabled = false;
                BtnTransferTransaction.Enabled = false;
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                if (!bgWorkerTransactionDelete.IsBusy)
                {
                    bgWorkerTransactionDelete.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                progressPanelTransactionDelete.Visible = false;
                BtnDeleteTransaction.Enabled = true;
                BtnTransferTransaction.Enabled = true;
                ISTransactionDeleteSuccessfully = false;
                Global.Message(ex.Message);
            }
        }

        private void bgWorkerTransactionDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDetTransaction, true, ObjSelTransaction);

            string[] StrKapans = Val.ToString(CmbKapan.Text).Trim().Replace(" ", "").Split(',');
            string StrKapanName = "";

            for (int IntK = 0; IntK < StrKapans.Length; IntK++)
            {
                StrKapanName = StrKapans[IntK];

                if (StrKapanName.Trim().Equals(string.Empty))
                    continue;

                for (int IntI = 0; IntI < GrdDetTransaction.RowCount; IntI++)
                {
                    DataRow DRow = GrdDetTransaction.GetDataRow(IntI);
                    if (Val.ToBoolean(GrdDetTransaction.GetRowCellValue(IntI, "COLSELECTCHECKBOX")) == true)
                    {
                        DRow["STATUS"] = "Running";
                        DRow["STATUS"] = BulkCopyTransactionDelete(Val.ToString(DRow["TABLENAME"]), DRow, StrKapanName);
                    }
                }
            }
        }

        private void bgWorkerTransactionDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanelTransactionDelete.Visible = false;
            BtnDeleteTransaction.Enabled = true;
            BtnTransferTransaction.Enabled = true;

            //Global.Message("TRANSACTION DATA TRANSFER DONE!!!!");
            if (ISTransactionDeleteSuccessfully == false)
            {
                lblDeleteTransaction.ForeColor = Color.Maroon;
                lblDeleteTransaction.Text = "TRANSACTION DATA DELETE GETS SOME ERROR PLS CHECK...!!!!";
            }
            else
            {
                lblDeleteTransaction.ForeColor = Color.DarkGreen;
                lblDeleteTransaction.Text = "TRANSACTION DATA DELETE SUCCESSFULLY...!!!!";
            }
            ISTransactionDeleteSuccessfully = false;
        }
        private string BulkCopyTransactionDelete(string pStrTableName, DataRow pDRow, string pStrKapanName) //#P : 09-03-2021
        {
            string Str = string.Empty;
            try
            {
                SqlConnectionStringBuilder Src = new SqlConnectionStringBuilder();
                Src.DataSource = txtSourceServer.Text;
                Src.InitialCatalog = txtSourceDBName.Text;
                Src.UserID = txtSourceUsername.Text;
                Src.Password = txtSourcePassword.Text;

                string Source = Src.ConnectionString;
                using (SqlConnection sourceCon = new SqlConnection(Source))
                {
                    sourceCon.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Parameters.Add(new SqlParameter("@TABLENAME", pStrTableName));
                    cmd1.Parameters.Add(new SqlParameter("@KAPANNAME", pStrKapanName));
                    cmd1.Parameters.Add(new SqlParameter("@OPE", "DELETE"));
                    cmd1.CommandText = "UTI_KapanTransferCopyDetail";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Connection = sourceCon;
                    cmd1.ExecuteNonQuery();

                    sourceCon.Close();

                    Str = "DONE";
                    ISTransactionDeleteSuccessfully = true;
                }
            }
            catch (Exception EX)
            {
                ISTransactionDeleteSuccessfully = false;
                Str = "Error !! " + EX.Message;
            }

            return Str;
        }
    }
}
