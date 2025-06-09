using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.Transaction;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusLib.TableName;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmKapanDashboard : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        
        DataTable DtabPacket = new DataTable();
        DataTable  DTabPacketLiveStock = new DataTable();

        #region Property Settings

        public FrmKapanDashboard()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
          //  BtnSearch_Click(null, null);

            CmbKapanStatus.Properties.Items["PENDING"].CheckState = CheckState.Checked;
            CmbKapanStatus.Properties.Items["RUNNING"].CheckState = CheckState.Checked; 
            
            CmbPacketStatus.SelectedIndex = 0;

            txtPass_TextChanged(null, null);
        }

        public void ShowForm(string pStr)
        {
            this.Text = "PROCESS RETURN";
            this.Name = "FrmProcessReturn";
            panel4.Visible = false;

            xtraTabControl1.TabPages.Remove(xtraTabPage2);
            xtraTabControl1.TabPages.Remove(xtraTabPage1);

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            //  BtnSearch_Click(null, null);

            CmbKapanStatus.Properties.Items["PENDING"].CheckState = CheckState.Checked;
            CmbKapanStatus.Properties.Items["RUNNING"].CheckState = CheckState.Checked;

            CmbPacketStatus.SelectedIndex = 0;

          

            txtPass_TextChanged(null, null);
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);            
        }

        #endregion


        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)+6
            //        this.Close();
            //}
            //if (e.KeyCode == Keys.F5)
            //{
            //    BtnSearch_Click(null, null);
            //}
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable DTabKapanLiveStock = ObjKapan.GetDataForKapanLiveStock("KAPANLIVESTOCK",Val.Trim(CmbKapanStatus.Properties.GetCheckedItems()));
            MainGridKapanLive.DataSource = DTabKapanLiveStock;
            MainGridKapanLive.Refresh();
            this.Cursor = Cursors.Default;
        }


        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }
        }

        private void repBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetKapanLive.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Global.Confirm("Are You Sure To Delete this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();
                    Property.Ope = "DELETE";
                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));
                  
                    Property = ObjKapan.EditKapan(Property);

                    Global.Message(Property.ReturnMessageDesc);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDetKapanLive.DeleteRow(GrdDetKapanLive.FocusedRowHandle);
                    }

                }

            }
            catch
            {

            }
        }

        private void repBtnKapanProcIssue_Click(object sender, EventArgs e)
        {
            try
            {
                string StrKapanName = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                string StrOwner = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("MANAGERNAME"));
                Guid StrKapan_ID = Guid.Parse(Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID")));
                
                if (GrdDetKapanLive.FocusedRowHandle < 0)
                {
                    StrKapanName = string.Empty;
                    StrKapan_ID = Guid.Empty;
                }
                else
                {
                    StrKapanName = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                    StrKapan_ID = Guid.Parse(Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID")));
                }
                //FrmPartyIssueWithPacket FrmPartyIssueWithPacket = new FrmPartyIssueWithPacket();
                //FrmPartyIssueWithPacket.MdiParent = Global.gMainRef;
                //FrmPartyIssueWithPacket.ShowForm(StrKapanName, StrKapan_ID);
                //FrmPartyIssueWithPacket.FormClosing += new FormClosingEventHandler(Form_Closing); 
            }
            catch
            {

            }
        }

        private void BtnPacketLiveStock_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DTabPacketLiveStock = ObjKapan.GetPacketLiveStock("PACKETLIVESTOCK", Val.ToString(CmbPacketStatus.SelectedItem));
            MainGrdPacket.DataSource = DTabPacketLiveStock;
            MainGrdPacket.Refresh();
            this.Cursor = Cursors.Default;

        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTabPacketLiveStock.Rows.Count > 0)
                {
                    DataRow[] DRow = DTabPacketLiveStock.Select("SEL=TRUE");

                    if (DRow.Length == 0)
                    {
                        Global.Message("Please Select Packet First");
                        return;
                    }

                    DataTable DTabRefrence = new DataTable();
                    if (DRow.Length != 0)
                    {
                        DTabRefrence = DRow.CopyToDataTable();
                    }

                    if (ValSave(DTabRefrence))
                    {
                        return;
                    }


                    DTabRefrence.DefaultView.Sort = "KAPAN_ID,PACKETNO";
                    DTabRefrence = DTabRefrence.DefaultView.ToTable();
                    FrmPacketReturn FrmPacketReturn = new FrmPacketReturn();
                    FrmPacketReturn.MdiParent = Global.gMainRef;
                    FrmPacketReturn.DTabRefrence = DTabRefrence;
                    FrmPacketReturn.FormClosing += new FormClosingEventHandler(FormPacketReturn_Closing);

                    FrmPacketReturn.ShowForm();
                }
                else
                {
                    Global.Message("Please Select Packet First");

                }

            }
            catch(Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
        private bool ValSave(DataTable DT)
        {
             int IntCol = 0, IntRow = 0;
             foreach (DataRow dr in DT.Rows)
             {
                 //For Update Validation
                 if (!Val.ToString(dr["RETURNDATE"]).Trim().Equals(string.Empty))
                 {
                     Global.Message("'PacketNo : "+dr["PACKETNO"] + "' Already Return Packet.. Please Select IssuePackets Only.");
                     IntCol = 0;
                     IntRow = dr.Table.Rows.IndexOf(dr);                     
                     return true;
                     
                 }
                 //end as
             }

              return false;
        }
        private void btnKapanProcess_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DataTable DtabKapanProcess = new DataTable();
            DtabKapanProcess = ObjKapan.GetDataForKapanLiveStock("KAPANPROCESSSTOCK", Val.Trim(CmbKapanStatus.Properties.GetCheckedItems()));

            MainGridKapanProcessStock.DataSource = DtabKapanProcess;
            MainGridKapanProcessStock.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void repBtnDeleteIssueEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdPacket.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Global.Confirm("Are You Sure To Delete This Packet IssueEntry?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnPacketCreationProperty Property = new TrnPacketCreationProperty();
                    Property.Ope = "ISSUEENTRY";
                    if (!Val.ToString(GrdPacket.GetFocusedRowCellValue("PACKET_ID")).Trim().Equals(string.Empty))
                        Property.PACKET_ID = Guid.Parse(Val.ToString(GrdPacket.GetFocusedRowCellValue("PACKET_ID")));

                    Property = ObjKapan.DeleteIssRetEntry(Property);

                    Global.Message(Property.ReturnMessageDesc);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        BtnPacketLiveStock_Click(null, null);
                        
                        if (GrdPacket.RowCount > 1)
                        {
                            GrdPacket.FocusedRowHandle = GrdPacket.RowCount - 1;
                        }
                    }
                    else
                    {
                        //txtItemGroupCode.Focus();
                    }
                    Property = null;

                }

            }
            catch(Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repBtnDelRetEntry_Click(object sender, EventArgs e)
        {
            try
            { 
               DataRow Dr = GrdPacket.GetDataRow(GrdPacket.FocusedRowHandle); 
                    
                if (GrdPacket.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Val.ToString(Dr["RETURNDATE"]).Trim().Equals(string.Empty) || Val.ToInt32(Dr["RETURNCARAT"]) !=0)
                    {
                        Global.Message("Return Entry Is Not Available");
                        return;
                    }


                    if (Global.Confirm("Are You Sure To Delete This Packet ReturnEntry?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnPacketCreationProperty Property = new TrnPacketCreationProperty();

                    if (!Val.ToString(GrdPacket.GetFocusedRowCellValue("PACKET_ID")).Trim().Equals(string.Empty))
                        Property.PACKET_ID = Guid.Parse(Val.ToString(GrdPacket.GetFocusedRowCellValue("PACKET_ID")));

                    else
                        Property.PACKET_ID = Guid.Empty;

                    Property.Ope = "RETURNENTRY";
                    Property = ObjKapan.DeleteIssRetEntry(Property);

                     Global.Message(Property.ReturnMessageDesc);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        BtnPacketLiveStock_Click(null, null);
                        
                        if (GrdPacket.RowCount > 1)
                        {
                            GrdPacket.FocusedRowHandle = GrdPacket.RowCount - 1;
                        }
                    }
                    else
                    {
                        //txtItemGroupCode.Focus();
                    }
                    Property = null;


                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repBtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetKapanLive.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Global.Confirm("Are You Sure To Update Status Of this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();
                    Property.Ope = "UPDATE";
                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));
                    Property.STATUS = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("STATUS"));
                    Property.KAPANCARAT = Val.Val(GrdDetKapanLive.GetFocusedRowCellValue("KAPANCARAT"));
                    Property.KAPANNAME = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                    Property = ObjKapan.EditKapan(Property);

                    Global.Message(Property.ReturnMessageDesc);

                    GrdDetKapanLive.RefreshData();

                    BtnSearch_Click(null, null);

                }

            }
            catch
            {

            }
        }

        private void BtnKapanLiveStockAutoFit_Click(object sender, EventArgs e)
        {
            GrdDetKapanLive.BestFitColumns();
        }

        private void BtnKapanProcessAutoFit_Click(object sender, EventArgs e)
        {
            GrdDetKapanProcessStock.BestFitColumns();
        }

        private void BtnPacketLiveStockAutoFit_Click(object sender, EventArgs e)
        {
            GrdPacket.BestFitColumns();
        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int IntI = 0; IntI < GrdPacket.RowCount; IntI++)
            {
                if (GrdPacket.IsRowVisible(IntI) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible)
                {
                    GrdPacket.SetRowCellValue(IntI, "SEL", ChkAll.Checked);
                    DTabPacketLiveStock.Rows[IntI]["SEL"] = ChkAll.Checked;
                }
            }
            DTabPacketLiveStock.AcceptChanges();
        }

        private void GrdPacket_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.Column.FieldName == "JANGEDNO")
            {
                try
                {

                    string StrJangedNo = Val.ToString(GrdPacket.GetFocusedRowCellValue("JANGEDNO"));

                    if (Global.Confirm("Are You Sure To Print The Janged # " + StrJangedNo + " ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        DataTable DTabPrint = new BOTRN_PacketCreate().GetJangedPrintData(StrJangedNo, "");

                        if (DTabPrint.Rows.Count <= 10)
                        {
                            int RowCount = 10 - DTabPrint.Rows.Count;

                            DataRow DRowFirst = DTabPrint.Rows[0];

                            for (int i = 0; i < RowCount; i++)
                            {
                                DataRow DRNew = DTabPrint.NewRow();
                                DRNew["JangedNo"] = DRowFirst["JangedNo"];
                                DRNew["CompanyName"] = DRowFirst["CompanyName"];
                                DRNew["CompAddress"] = DRowFirst["CompAddress"];
                                DRNew["PartyName"] = DRowFirst["PartyName"];
                                DRNew["PartyAddress"] = DRowFirst["PartyAddress"];
                                DRNew["ManagerName"] = DRowFirst["ManagerName"];
                                DTabPrint.Rows.Add(DRNew);
                            }
                        }

                      
                        Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();

                        FrmReportViewer.ShowForm("JangedPrintMFG", DTabPrint);
                        this.Cursor = Cursors.Default;
                    }


                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;

                    Global.Message(ex.Message);

                }
            }
        }

        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("KapanLiveStock", GrdDetKapanLive);
        }

        private void BtnKapanProcessExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("KapanProcessStock", GrdDetKapanProcessStock);
        }

        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PacketLiveStock", GrdPacket);
        }

        private void BtnRejection_Click(object sender, EventArgs e)
        {
            try
            {
                string StrKapanName = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                string KapanCarat = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANCARAT"));

                string StrKapan_ID = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));
                
                FrmRejectionTransfer FrmRejectionTransfer = new FrmRejectionTransfer();
                FrmRejectionTransfer.MdiParent = Global.gMainRef;
                FrmRejectionTransfer.ShowForm(StrKapan_ID, StrKapanName, KapanCarat, "KAPAN");
                FrmRejectionTransfer.FormClosing += new FormClosingEventHandler(Form_Closing); 
            }
            catch
            {

            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            BtnSearch_Click(null, null);
        }
        private void FormPacketReturn_Closing(object sender, FormClosingEventArgs e)
        {
            BtnPacketLiveStock_Click(null, null);
        }

        private void GrdDetKapanLive_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Clicks == 2)
            {
                TrnKapanCreateProperty KapanProperty = new TrnKapanCreateProperty();
                KapanProperty.KAPAN_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));

                if (e.Column.FieldName == "REJECTIONCARAT")
                {
                    DataTable DtData = ObjKapan.GetRejectionData("KAPAN",KapanProperty);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    // FrmPopupGrid.DTab = DtData;                   
                    FrmPopupGrid.CountedColumn = "REJECTIONNAME";
                    FrmPopupGrid.SummrisedColumn = "PCS,CARAT,AMOUNT";
                    FrmPopupGrid.ColumnsToHide = "REJECTION_ID";
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Rejection List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;

                    FrmPopupGrid.Width = 1000;
                    //FrmPopupGrid.Size = this.Size;

                    FrmPopupGrid.GrdDet.Columns["REJECTIONNAME"].Caption = "Rejection";
                    FrmPopupGrid.GrdDet.Columns["PCS"].Caption = "Pcs";
                    FrmPopupGrid.GrdDet.Columns["CARAT"].Caption = "Carat";
                    FrmPopupGrid.GrdDet.Columns["REJECTIONDATE"].Caption = "Rej. Date";
                    FrmPopupGrid.GrdDet.Columns["RATE"].Caption = "Rate";
                    FrmPopupGrid.GrdDet.Columns["AMOUNT"].Caption = "Amount";
                    FrmPopupGrid.GrdDet.Columns["REMARK"].Caption = "Remark";

                    FrmPopupGrid.GrdDet.Columns["REJECTIONNAME"].Width = 100;
                    FrmPopupGrid.GrdDet.Columns["REJECTIONDATE"].Width = 100;
                    FrmPopupGrid.GrdDet.Columns["REMARK"].Width = 150;
                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
            }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text.ToUpper() == BusLib.Configuration.BOConfiguration.gEmployeeProperty.PASSWORD)
            {
                GrdDetKapanLive.Columns["KAPANNAME"].OptionsColumn.AllowEdit = true;
                GrdDetKapanLive.Columns["KAPANCARAT"].OptionsColumn.AllowEdit = true;
                GrdDetKapanLive.Columns["STATUS"].OptionsColumn.AllowEdit = true;

                GrdPacket.Columns["UPDATE"].OptionsColumn.AllowEdit = true;
                //GrdPacket.Columns["SHAPENAME"].OptionsColumn.AllowEdit = true;
                //GrdPacket.Columns["PURITYNAME"].OptionsColumn.AllowEdit = true;
                //GrdPacket.Columns["CHARNINAME"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["ISSUEPCS"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["ISSUECARAT"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["RETURNPCS"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["RETURNCARAT"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["LOSSCARAT"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["KACHAPCS"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["KACHACARAT"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["CANCELPCS"].OptionsColumn.AllowEdit = true;
                GrdPacket.Columns["CANCELCARAT"].OptionsColumn.AllowEdit = true;

            }
            else
            {
                GrdDetKapanLive.Columns["KAPANNAME"].OptionsColumn.AllowEdit = false;
                GrdDetKapanLive.Columns["KAPANCARAT"].OptionsColumn.AllowEdit = false;
                GrdDetKapanLive.Columns["STATUS"].OptionsColumn.AllowEdit = false;

                GrdPacket.Columns["UPDATE"].OptionsColumn.AllowEdit = false;
                //GrdPacket.Columns["SHAPENAME"].OptionsColumn.AllowEdit = false;
                //GrdPacket.Columns["PURITYNAME"].OptionsColumn.AllowEdit = false;
                //GrdPacket.Columns["CHARNINAME"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["ISSUEPCS"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["ISSUECARAT"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["RETURNPCS"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["RETURNCARAT"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["LOSSCARAT"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["KACHAPCS"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["KACHACARAT"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["CANCELPCS"].OptionsColumn.AllowEdit = false;
                GrdPacket.Columns["CANCELCARAT"].OptionsColumn.AllowEdit = false;

            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTabPacketLiveStock.Rows.Count > 0)
                {
                    DataRow[] DRow = DTabPacketLiveStock.Select("SEL=TRUE");

                    if (DRow.Length == 0)
                    {
                        Global.Message("Please Select Packet First");
                        return;
                    }

                    DataTable DTabRefrence = new DataTable();
                    if (DRow.Length != 0)
                    {
                        DTabRefrence = DRow.CopyToDataTable();
                    }

                    if (ValSave(DTabRefrence))
                    {
                        return;
                    }


                    DTabRefrence.DefaultView.Sort = "KAPAN_ID,PACKETNO";
                    DTabRefrence = DTabRefrence.DefaultView.ToTable();
                    FrmPacketUpdate FrmPacketUpdate = new FrmPacketUpdate();
                    FrmPacketUpdate.MdiParent = Global.gMainRef;
                    FrmPacketUpdate.DTabRefrence = DTabRefrence;
                    FrmPacketUpdate.FormClosing += new FormClosingEventHandler(FormPacketReturn_Closing);

                    FrmPacketUpdate.ShowForm();
                }
                else
                {
                    Global.Message("Please Select Packet First");

                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GrdDetKapanLive_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0 )
            {
                return;
            }

            string Status = GrdDetKapanLive.GetRowCellValue(e.RowHandle, "STATUS").ToString();
            if (Status == "PENDING")
            {
                e.Appearance.BackColor = Color.Transparent;
            }
            else if (Status == "RUNNING")
            {
                e.Appearance.BackColor = lblRunning.BackColor;
            }
            else if (Status == "COMPLETE")
            {
                e.Appearance.BackColor = lblComplete.BackColor;
            }
        }

        private void GrdPacket_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            double ExpPer = Val.Val(GrdPacket.GetRowCellValue(e.RowHandle, "EXPPER").ToString());
            double ReturnPer = Val.Val(GrdPacket.GetRowCellValue(e.RowHandle, "RETURNPER").ToString());
            double Diff = Math.Round(ExpPer - ReturnPer, 2);
            if (Diff > 0 )
            {
                e.Appearance.BackColor = lblDiff.BackColor;
            }
            
        }

        private void BtnPacketUpdate_Click(object sender, EventArgs e)
        {
            if (GrdPacket.FocusedRowHandle < 0)
            {
                return;
            }
            if (Global.Confirm("Are You Sure You Want To Update ? ") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            DataRow DRow = GrdPacket.GetDataRow(GrdPacket.FocusedRowHandle);

            TrnPacketCreationProperty Property = new TrnPacketCreationProperty();
            Property.PACKET_ID = Guid.Parse(DRow["PACKET_ID"].ToString());
            Property.ISSUEPCS = Val.ToInt(DRow["ISSUEPCS"]);
            Property.ISSUECARAT = Val.Val(DRow["ISSUECARAT"]);
            Property.RETURNPCS = Val.ToInt(DRow["RETURNPCS"]);
            Property.RETURNCARAT = Val.Val(DRow["RETURNCARAT"]);
            Property.LOSSCARAT = Val.Val(DRow["LOSSCARAT"]);
            Property.KACHAPCS = Val.ToInt(DRow["KACHAPCS"]);
            Property.KACHACARAT = Val.Val(DRow["KACHACARAT"]);
            Property.CANCELPCS = Val.ToInt(DRow["CANCELPCS"]);
            Property.CANCELCARAT = Val.Val(DRow["CANCELCARAT"]);

            Property = ObjKapan.PacketUpdateFromLiveStock(Property);
            Global.Message(Property.ReturnMessageDesc);
            Property = null;


        }

        private void GrdPacket_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            DataRow DRow = GrdPacket.GetDataRow(e.RowHandle);

            switch (e.Column.FieldName.ToUpper())
            {
                case "RETURNPER":
                    double DouPer = Val.Val(DRow["RETURNPER"]);
                    double DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    double DouReadyCarat = Math.Round((DouIssCarat * DouPer) / 100, 4);
                    double DouLossCarat = Math.Round(DouIssCarat - DouReadyCarat, 4);

                    if (DouReadyCarat > DouIssCarat)
                    {
                        Global.Message("Return Carat Is Greater Than Issue");
                        DRow["RETURNCARAT"] = 0;
                        DRow["LOSSCARAT"] = 0;
                    }
                    else
                    {
                        DRow["RETURNCARAT"] = DouReadyCarat;
                        DRow["LOSSCARAT"] = DouLossCarat;
                    }

                    break;
                case "RETURNCARAT":
                    DouReadyCarat = Val.Val(DRow["RETURNCARAT"]);
                    DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    DouPer = Math.Round((DouReadyCarat / DouIssCarat) * 100, 4);
                    DouLossCarat = Math.Round(DouIssCarat - DouReadyCarat, 4);

                    if (DouReadyCarat > DouIssCarat)
                    {
                        Global.Message("Return Carat Is Greater Than Issue");
                        DRow["RETURNPER"] = 0;
                        DRow["LOSSCARAT"] = 0;
                    }
                    else
                    {
                        DRow["RETURNPER"] = DouPer;
                        DRow["LOSSCARAT"] = DouLossCarat;
                    }
                    break;
                case "LOSSCARAT":
                    DouLossCarat = Val.Val(DRow["LOSSCARAT"]);
                    DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    DouReadyCarat = Math.Round(DouIssCarat - DouLossCarat, 4);
                    DouPer = Math.Round((DouReadyCarat / DouIssCarat) * 100, 4);

                    if (DouReadyCarat > DouIssCarat)
                    {
                        Global.Message("Return Carat Is Greater Than Issue");
                        DRow["RETURNPER"] = 0;
                        DRow["RETURNCARAT"] = 0;
                    }
                    else
                    {
                        DRow["RETURNPER"] = DouPer;
                        DRow["RETURNCARAT"] = DouReadyCarat;
                    }
                    //GrdDet.SetRowCellValue(e.RowHandle, "READYPER", DouPer);

                    break;
                case "KACHAPCS":
                    DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    int IntIssPcs = Val.ToInt(DRow["ISSUEPCS"]);
                    int IntKachaPcs = Val.ToInt(DRow["KACHAPCS"]);

                    double KachaCarat = DouPer = Math.Round((DouIssCarat / IntIssPcs) * IntKachaPcs, 4);

                    DRow["KACHACARAT"] = KachaCarat;

                    break;
                case "CANCELPCS":
                    DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    IntIssPcs = Val.ToInt(DRow["ISSUEPCS"]);
                    int IntCancelPcs = Val.ToInt(DRow["CANCELPCS"]);

                    double CancelCarat = DouPer = Math.Round((DouIssCarat / IntIssPcs) * IntCancelPcs, 4);

                    DRow["CANCELCARAT"] = CancelCarat;

                    break;
                default:
                    break;
            }

        }

        private void FrmKapanDashboard_Load(object sender, EventArgs e)
        {
            GrdPacket.Bands["BandLabour"].Visible = true;
            GrdDetKapanProcessStock.Bands["BandLabourInKapanStockProcess"].Visible = true;
        }

        private void GrdPacket_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (txtPass.Text.ToUpper() != BusLib.Configuration.BOConfiguration.gEmployeeProperty.PASSWORD)
            {
                e.Cancel = true;
            }
            else
            {
                if (Val.ISDate(Val.ToString(GrdPacket.GetFocusedRowCellValue("RETURNDATE"))) == false)
                {
                    if (GrdPacket.FocusedColumn.FieldName == "RETURNPCS" ||
                        GrdPacket.FocusedColumn.FieldName == "RETURNCARAT" ||
                        GrdPacket.FocusedColumn.FieldName == "KACHAPCS" ||
                        GrdPacket.FocusedColumn.FieldName == "KACHACARAT" ||
                        GrdPacket.FocusedColumn.FieldName == "CANCELPCS" ||
                        GrdPacket.FocusedColumn.FieldName == "CANCELCARAT" ||
                        GrdPacket.FocusedColumn.FieldName == "LOSSCARAT"
                        )
                        e.Cancel = true;
                }
            }
        }

    }
}
