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
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using AxoneMFGRJ.Report;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleGoodsFinalReturn : DevExpress.XtraEditors.XtraForm
    {
      
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
        
        DataTable DTabPacket = new DataTable();
        string mStrParentFormType = "";

        #region Property Settings

        public FrmSingleGoodsFinalReturn()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DTPTransferDate.Value = DateTime.Now;

            DTabPacket = new DataTable();
            DTabPacket.Columns.Add(new DataColumn("PACKET_ID",typeof(Guid)));
            DTabPacket.Columns.Add(new DataColumn("KAPAN_ID",typeof(Guid)));
            DTabPacket.Columns.Add(new DataColumn("KAPANNAME",typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TAG",typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("ISSUEPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("ISSUECARAT",typeof(double)));

            DTabPacket.Columns.Add(new DataColumn("READYPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("READYCARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("LOSSCARAT", typeof(double)));

            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEE_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEECODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEENAME", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("TOEMPLOYEE_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("TOEMPLOYEECODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("TOEMPLOYEENAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("TODESIGNATION", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("AUTOCONFIRM", typeof(bool)));
            
            DTabPacket.Columns.Add(new DataColumn("FROMDEPARTMENT_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("FROMDEPARTMENTNAME",typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("TODEPARTMENT_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TODEPARTMENTNAME", typeof(string)));
            
            DTabPacket.Columns.Add(new DataColumn("FROMPROCESS_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("FROMPROCESSNAME",typeof(string)));
            
            DTabPacket.Columns.Add(new DataColumn("OLDTRN_ID", typeof(Guid)));
            
            MainGrid.DataSource = DTabPacket;
            GrdDet.RefreshData();
            
            CalculateSummary();

            txtProcessTo.Focus();
            
            this.Show();          
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;
           
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();
           
        }

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }
            
        }

        private void txtToProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESSFINAL);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcessTo.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcessTo.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        DTabPacket.AcceptChanges();
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public bool ValSave()
        {
            if (txtProcessTo.Text.Trim().Length == 0)
            {
                Global.Message("For Process Field Is Required");
                txtProcessTo.Focus();
                return false;
            }
            return true;
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }
   
                this.Cursor = Cursors.WaitCursor;

                string EntryType = "EMPRET";

                int IntSrNo = 0;
                txtJangedNo.Text = string.Empty;

                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    if (Val.Val(DRow["ISSUECARAT"]) == 0 && Val.ToString(DRow["FROMEMPLOYEECODE"]).Length == 0 )
                    {
                        continue;
                    }

                    IntSrNo++;
                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                    Property.TRN_ID = 0;
                    Property.OLDTRN_ID = Val.ToInt64(DRow["OLDTRN_ID"].ToString());
                    Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"].ToString());
                    Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"].ToString());

                    Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"].ToString());
                    Property.PACKETNO = Val.ToInt(DRow["PACKETNO"].ToString());
                    Property.TAG = Val.ToString(DRow["TAG"].ToString());

                    Property.JANGEDNO = Val.ToInt64(txtJangedNo.Text);
                    Property.ENTRYSRNO = IntSrNo;
                    Property.ENTRYTYPE = EntryType;

                    Property.FROMDEPARTMENT_ID = Val.ToInt32(DRow["FROMDEPARTMENT_ID"]);
                    Property.TODEPARTMENT_ID = Val.ToInt32(DRow["TODEPARTMENT_ID"]);

                    Property.FROMEMPLOYEE_ID = Val.ToInt64(DRow["FROMEMPLOYEE_ID"]);
                    Property.TOEMPLOYEE_ID = Val.ToInt64(DRow["TOEMPLOYEE_ID"]);

                    Property.FROMPROCESS_ID = Val.ToInt32(DRow["FROMPROCESS_ID"]);
                    Property.TOPROCESS_ID = Val.ToInt32(txtProcessTo.Tag);
                    Property.NEXTPROCESS_ID = Val.ToInt32(txtProcessTo.Tag);

                    Property.ISSUEPCS = Val.ToInt32(DRow["ISSUEPCS"]);
                    Property.ISSUECARAT = Val.Val(DRow["ISSUECARAT"]);

                    Property.RETURNTYPE = "DONE";

                    Property.READYPCS = Val.ToInt32(DRow["READYPCS"]);
                    Property.READYCARAT = Val.Val(DRow["READYCARAT"]);

                    Property.RRPCS = 0;
                    Property.RRCARAT = 0;

                    Property.ISPOLISHFINAL = true;

                    Property.LOSTPCS = 0;
                    Property.LOSTCARAT = 0;
                    Property.LOSSCARAT = Val.Val(DRow["LOSSCARAT"]);
                    Property.MIXINGLESSPLUS = 0;

                    Property.TRANSDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                    Property.TRANSTYPE = EntryType;
                    Property.REMARK = "Final Return Jama";
                    Property.AUTOCONFIRM = Val.ToBoolean(DRow["AUTOCONFIRM"]);
                    Property = ObjTrn.TransferGoods(Property);
                    txtJangedNo.Text = Property.JANGEDNO.ToString();
                    if (Property.ReturnMessageType == "FAIL")
                    {
                        this.Cursor = Cursors.Default;
                        Global.Message(Property.ReturnMessageDesc);
                        this.Cursor = Cursors.WaitCursor;
                    }

                    Property = null;
                }
                    
                this.Cursor = Cursors.Default;
                if (Val.Val(txtJangedNo.Text) != 0)
                {
                    //Global.Message("Your Goods Successfully Transfer To : " + txtTransferTo.Text + "\n\nYour Slip Number : " + txtJangedNo.Text);
                    BtnPrint_Click(null, null);
                    DTabPacket.Rows.Clear();
                    txtProcessTo.Text = "";
                    txtProcessTo.Tag = "";
                    txtKapanName.Text = "";
                    txtPacketNo.Text = "";
                    txtTag.Text = "";
                    txtJangedNo.Text = "";
                    txtTotalCarat.Text = "";
                    txtTotalPcs.Text = "";
                    txtProcessTo.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }

            
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //DataTable DTab = ObjTrn.PopupJangedForPrint(Val.ToInt64(txtJangedNo.Text));
            //if (DTab.Rows.Count == 0)
            //{
            //    Global.Message("There Is No Data For Print");
            //    return;
            //}
            //FrmReportViewer FrmReportViewer = new FrmReportViewer();
            //FrmReportViewer.MdiParent = Global.gMainRef;
            //FrmReportViewer.ShowWithPrint("JangedPrint", DTab);
        }

        private void txtJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "JANGEDNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjTrn.PopupJangedForPrint("", 0);
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtJangedNo.Text = Val.ToString(FrmSearch.mDRow["JANGEDNO"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRequiredProcess_Validated(object sender, EventArgs e)
        {
            BtnSave.Focus();
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            if (txtProcessTo.Text.Trim().Length == 0)
            {
                Global.Message("Transfer Process IS Required");
                txtProcessTo.Focus();
                return;
            }

            string Str = txtKapanName.Text +"-"+txtPacketNo.Text+"-"+txtTag.Text;

            DataRow DRow = ObjTrn.GetFinalEmployeeReturnJamaPacketInfo(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt(txtProcessTo.Tag), txtProcessTo.Text);
            if (DRow == null)
            {
                Global.Message("Oops. "+Str+"  Packet Is Not In Stock. Please Check In Running Stock");

                txtKapanName.Text = "";
                txtPacketNo.Text = "";
                txtTag.Text = "";
                txtKapanName.Focus();
                return;
            }

            bool ISExists = false;
            foreach (DataRow DD in DTabPacket.Rows)
            {
                if (Val.ToString(DD["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"]))
                {
                    ISExists = true;
                    break;
                }
            }

            if (ISExists == false)
            {
                DataRow DRNew = DTabPacket.NewRow();

                DRNew["PACKET_ID"] = DRow["PACKET_ID"];
                DRNew["KAPAN_ID"] = DRow["KAPAN_ID"];
                DRNew["KAPANNAME"] = DRow["KAPANNAME"];
                DRNew["PACKETNO"] = DRow["PACKETNO"];
                DRNew["TAG"] = DRow["TAG"];
                DRNew["ISSUEPCS"] = DRow["ISSUEPCS"];
                DRNew["ISSUECARAT"] = DRow["ISSUECARAT"];

                DRNew["READYPCS"] = DRow["READYPCS"];
                DRNew["READYCARAT"] = DRow["READYCARAT"];

                DRNew["LOSSCARAT"] = DRow["LOSSCARAT"];

                DRNew["OLDTRN_ID"] = DRow["OLDTRN_ID"];
                DRNew["FROMEMPLOYEE_ID"] = DRow["FROMEMPLOYEE_ID"];
                DRNew["FROMEMPLOYEECODE"] = DRow["FROMEMPLOYEECODE"];
                DRNew["FROMEMPLOYEENAME"] = DRow["FROMEMPLOYEENAME"];
                DRNew["TOEMPLOYEE_ID"] = DRow["TOEMPLOYEE_ID"];
                DRNew["TOEMPLOYEECODE"] = DRow["TOEMPLOYEECODE"];
                DRNew["TOEMPLOYEENAME"] = DRow["TOEMPLOYEENAME"];
                DRNew["TODESIGNATION"] = DRow["TODESIGNATION"];
                DRNew["AUTOCONFIRM"] = DRow["AUTOCONFIRM"];
                DRNew["FROMDEPARTMENT_ID"] = DRow["FROMDEPARTMENT_ID"];
                DRNew["FROMDEPARTMENTNAME"] = DRow["FROMDEPARTMENTNAME"];
                DRNew["TODEPARTMENT_ID"] = DRow["TODEPARTMENT_ID"];
                DRNew["TODEPARTMENTNAME"] = DRow["TODEPARTMENTNAME"];
                DRNew["FROMPROCESS_ID"] = DRow["FROMPROCESS_ID"];
                DRNew["FROMPROCESSNAME"] = DRow["FROMPROCESSNAME"];

                DTabPacket.Rows.Add(DRNew);
                DTabPacket.AcceptChanges();
            }
            else
            {
                Global.Message(Str + " Is Already In Grid Pls Check");
            }
            txtKapanName.Text = "";
            txtPacketNo.Text = "";
            txtTag.Text = "";
            txtKapanName.Focus();
           

        }

        private void txtFinalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjTrn.GetFinalEmployeeReturnJama(Val.ToString(DRow["KAPANNAME"]), Val.ToInt(DRow["PACKETNO"]), Val.ToString(DRow["TAG"]), Val.ToInt(txtProcessTo.Tag), txtProcessTo.Text);
                    FrmSearch.mColumnsToHide = "PACKETNO,TAG,PACKET_ID,EMPLOYEE_ID,DEPARTMENT_ID";                    
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("FROMEMPLOYEE_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdDet.SetFocusedRowCellValue("FROMEMPLOYEECODE", Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]));
                        GrdDet.SetFocusedRowCellValue("FROMEMPLOYEENAME", Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]));
                        GrdDet.SetFocusedRowCellValue("FROMDESIGNATION", Val.ToString(FrmSearch.mDRow["DESIGNATIONNAME"]));
                        GrdDet.SetFocusedRowCellValue("AUTOCONFIRM", Val.ToString(FrmSearch.mDRow["AUTOCONFIRM"]));
                        GrdDet.SetFocusedRowCellValue("FROMDEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdDet.SetFocusedRowCellValue("FROMDEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
            switch (e.Column.FieldName.ToUpper())
            {
                case "READYCARAT":
                    double DouReady = Val.Val(DRow["READYCARAT"]);
                    double DouIssue = Val.Val(DRow["ISSUECARAT"]);
                    double DouLossCarat = Math.Round(DouIssue - DouReady, 3);

                    GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", DouLossCarat);
                    break;
                default:
                    break;
            }
        }

      
    }
}
