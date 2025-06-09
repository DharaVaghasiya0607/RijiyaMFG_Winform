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
using BusLib.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using AxoneMFGRJ.Report;
using BusLib.Rapaport;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmPredictionCopyModule : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionCopy ObjTrn = new BOTRN_PredictionCopy();
        BOFindRap ObjRap = new BOFindRap();

        DataTable DTabPrdType = new DataTable();
        DataTable DtabFoundPacket = new DataTable();
        DataTable DTabCopyPackets = new DataTable();
        EmployeeActionRightsProperty EmployeeRightsProperty = new EmployeeActionRightsProperty();

        #region Property Settings

        public FrmPredictionCopyModule()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            EmployeeRightsProperty = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;


            DTabPrdType = ObjRap.GetPreditctionType(EmployeeRightsProperty.PRDTYPE_ID);
            CmbFromPrdType.Items.Clear();
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 7 || Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 || 
                    Val.ToInt(DRow["PRDTYPE_ID"]) == 11 || Val.ToInt(DRow["PRDTYPE_ID"]) == 4 || Val.ToInt(DRow["PRDTYPE_ID"]) == 14 ||
                    Val.ToInt(DRow["PRDTYPE_ID"]) == 12)
                    CmbFromPrdType.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
            }

            CmbFromPrdType.SelectedIndex = 0;

            DTabPrdType = ObjRap.GetPreditctionType(EmployeeRightsProperty.PRDTYPE_ID);
            CmbToPrdType.Items.Clear();
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 7 || Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 ||
                    Val.ToInt(DRow["PRDTYPE_ID"]) == 11 || Val.ToInt(DRow["PRDTYPE_ID"]) == 4 || Val.ToInt(DRow["PRDTYPE_ID"]) == 14 || 
                    Val.ToInt(DRow["PRDTYPE_ID"]) == 12)
                    CmbToPrdType.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
            }
            CmbToPrdType.SelectedIndex = 0;


            this.Show();
            BtnShow_Click(null, null);

            if (Val.ToInt32(EmployeeRightsProperty.PRDTYPE_ID) != 0)
            {
                DTabPrdType = new BOFindRap().GetPreditctionType(EmployeeRightsProperty.PRDTYPE_ID);

                CmbFromPrdType.Items.Clear();
                foreach (DataRow DRow in DTabPrdType.Rows)
                {
                    if (Val.ToInt(DRow["PRDTYPE_ID"]) == 6 || Val.ToInt(DRow["PRDTYPE_ID"]) == 7
                        || Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 || Val.ToInt(DRow["PRDTYPE_ID"]) == 11)
                    {
                        CmbFromPrdType.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
                        CmbToPrdType.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
                    }
                }
                CmbFromPrdType.SelectedIndex = 0;
                CmbToPrdType.SelectedIndex = 0;
            }
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


        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtPacketNo.Text) == 0)
                {
                    txtKapanName.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    txtKapanName.Focus();
                    return;
                }

                if (Val.ISNumeric(txtTag.Text) == true)
                {
                    Char c = (Char)(64 + Val.ToInt(txtTag.Text));
                    txtTag.Text = c.ToString();
                }

                this.Cursor = Cursors.WaitCursor;

                DataRow DRowNotFound = null;
                int IntI = 0;
                for (IntI = 0; IntI < DtabFoundPacket.Rows.Count; IntI++)
                {
                    DataRow DR = DtabFoundPacket.Rows[IntI];
                    if (txtKapanName.Text.Trim() == Val.ToString(DR["KAPANNAME"]).Trim()
                       && txtPacketNo.Text.Trim() == Val.ToString(DR["PACKETNO"]).Trim()
                       && txtTag.Text.Trim() == Val.ToString(DR["TAG"]).Trim()
                       )
                    {
                        DRowNotFound = DR;
                        break;
                    }
                }

                bool ISExists = false;

                foreach (DataRow DRowFound in DTabCopyPackets.Rows)
                {
                    if (txtKapanName.Text.Trim() == Val.ToString(DRowFound["KAPANNAME"]).Trim()
                       && txtPacketNo.Text.Trim() == Val.ToString(DRowFound["PACKETNO"]).Trim()
                       && txtTag.Text.Trim() == Val.ToString(DRowFound["TAG"]).Trim()
                       )
                    {
                        ISExists = true;
                    }
                }

                if (DRowNotFound != null && ISExists == false)
                {
                    TrnStockTallyProperty Property = new TrnStockTallyProperty();
                    Property.STOCKTALLYDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                    Property.PACKET_ID = Val.ToInt64(DRowNotFound["PACKET_ID"]);
                    Property.KAPANNAME = Val.ToString(DRowNotFound["KAPANNAME"]);
                    Property.PACKETNO = Val.ToInt32(DRowNotFound["PACKETNO"]);
                    Property.TAG = Val.ToString(DRowNotFound["TAG"]);
                    Property.CARAT = Val.Val(DRowNotFound["CARAT"]);
                    Property.TRN_ID = Val.ToInt64(DRowNotFound["TRN_ID"]);
                    Property.FOUNDSTATUS = "FOUND";
                    Property = ObjTrn.Save(Property);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        DataRow DRNew = DTabCopyPackets.NewRow();

                        DRNew["ISFOUND"] = true;
                        DRNew["TRN_ID"] = DRowNotFound["TRN_ID"];
                        DRNew["PACKET_ID"] = DRowNotFound["PACKET_ID"];
                        DRNew["KAPANNAME"] = DRowNotFound["KAPANNAME"];
                        DRNew["PACKETNO"] = DRowNotFound["PACKETNO"];
                        DRNew["PACKETTAG"] = DRowNotFound["PACKETTAG"];
                        DRNew["TAG"] = DRowNotFound["TAG"];
                        DRNew["CARAT"] = DRowNotFound["CARAT"];
                        DRNew["EMPLOYEE_ID"] = DRowNotFound["EMPLOYEE_ID"];
                        DRNew["EMPLOYEECODE"] = DRowNotFound["EMPLOYEECODE"];
                        DRNew["DEPARTMENT_ID"] = DRowNotFound["DEPARTMENT_ID"];
                        DRNew["DEPARTMENTNAME"] = DRowNotFound["DEPARTMENTNAME"];

                        DTabCopyPackets.Rows.Add(DRNew);

                        DtabFoundPacket.Rows[IntI].Delete();
                        DtabFoundPacket.AcceptChanges();
                        DTabCopyPackets.AcceptChanges();
                    }

                }
                //else if (DRowNotFound !=null)
                else if (ISExists)
                {
                    Global.Message(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " : ALREADY SCANNED IN COPY PACKET DETAIL GRID");
                }
                else if (DRowNotFound == null)
                {
                    Global.Message(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " : NOT FOUND PLEASE CHECK.");
                }

                if (GrdDetFound.RowCount > 1)
                {
                    GrdDetFound.FocusedRowHandle = GrdDetFound.RowCount - 1;
                }

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtKapanName.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
                    txtDepartment.Tag = string.Empty;

                if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                    txtEmployee.Tag = string.Empty;

                DtabFoundPacket.Rows.Clear();

                this.Cursor = Cursors.WaitCursor;
                DtabFoundPacket = ObjTrn.GetPredictionCopyPacketDetail(Val.ToInt(txtDepartment.Tag), Val.ToInt64(txtEmployee.Tag), Val.ToBoolean(ChkWithExtraStock.Checked),"");

                DTabCopyPackets = DtabFoundPacket.Clone();

                MainGridNotFound.DataSource = DtabFoundPacket;
                MainGridNotFound.Refresh();

                MainGridFound.DataSource = DTabCopyPackets;
                MainGridFound.Refresh();

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtKapanName.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable DTab = ObjTrn.Print(Val.SqlDate(DTPTransferDate.Value.ToShortDateString()), Val.ToInt(txtDepartment.Tag), Val.ToInt64(txtEmployee.Tag));
                if (DTab.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.MessageError("There Is No Data For Print");
                    return;
                }
                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowWithPrint("StockTallyPrint", DTab);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTCODE,DEPARTMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);

                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
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

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "AUTOCONFIRM,EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void txtTag_Leave(object sender, EventArgs e)
        {
            txtKapanName.Focus();
        }

        private void BtnCopyPrd_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbFromPrdType.Text.Trim().Length == 0)
                {
                    Global.Message("Please Select From Pridiction");
                    return;
                }
                if (CmbToPrdType.Text.Trim().Length == 0)
                {
                    Global.Message("Please Select To Pridiction");
                    return;
                }
                if (txtEmployeeName.Text.Trim().Length == 0)
                {
                    Global.Message("Please Select Employee Name");
                    return;
                }

                if (Val.ToInt(CmbFromPrdType.Tag) == Val.ToInt(CmbToPrdType.Tag))
                {
                    Global.Message("Same From Prediction And To Prediction Is Not Allow To Copy...");
                    CmbToPrdType.Focus();
                    return;
                }

                if (Global.Confirm("Are You Sure You Want Copy Prd Data?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;


                string Xml = string.Empty;
                DtabFoundPacket.TableName = "Table1";
                using (StringWriter sw = new StringWriter())
                {
                    DtabFoundPacket.WriteXml(sw);
                    Xml = sw.ToString();
                }



                int IntRes = ObjTrn.CopyPridiction(Xml, Val.ToInt(Val.ToString(CmbFromPrdType.Tag)), Val.ToInt(Val.ToString(CmbToPrdType.Tag)), Val.ToInt64(txtEmployeeName.Tag));
                this.Cursor = Cursors.Default;

                if (IntRes != -1)
                {
                    Global.Message("Data Copied Successfully");
                   

                }
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                CmbFromPrdType.SelectedIndex = 0;
                CmbToPrdType.SelectedIndex = 0;
                txtKapanName.Focus();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void CmbFromPrdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Name = Val.ToString(CmbFromPrdType.SelectedItem);
                DataRow[] D = DTabPrdType.Select("PRDTYPENAME ='" + Name + "' ");

                if (D.Length != 0)
                {
                    DataRow DRow = D[0];
                    CmbFromPrdType.Tag = Val.ToString(DRow["PrdType_ID"]);
                    DRow = null;
                }

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }

        }

        private void txtToEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void txtEmployeeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployeeName.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtEmployeeName.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        txtEmpCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
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

        private void FrmPredictionCopyModule_Load(object sender, EventArgs e)
        {

        }

        private void CmbToPrdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Name = Val.ToString(CmbToPrdType.SelectedItem);
                DataRow[] D = DTabPrdType.Select("PRDTYPENAME ='" + Name + "' ");

                if (D.Length != 0)
                {
                    DataRow DRow = D[0];
                    CmbToPrdType.Tag = Val.ToString(DRow["PrdType_ID"]);
                    DRow = null;
                }

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
        }

        private void RemoveSelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetFound.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO REMOVE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataRow Drow = GrdDetFound.GetDataRow(GrdDetFound.FocusedRowHandle);

                        DataRow DRNew = DtabFoundPacket.NewRow();
                        DRNew["ISFOUND"] = false;
                        DRNew["TRN_ID"] = Drow["TRN_ID"];
                        DRNew["PACKET_ID"] = Drow["PACKET_ID"];
                        DRNew["KAPANNAME"] = Drow["KAPANNAME"];
                        DRNew["PACKETNO"] = Drow["PACKETNO"];
                        DRNew["PACKETTAG"] = Drow["PACKETTAG"];
                        DRNew["TAG"] = Drow["TAG"];
                        DRNew["CARAT"] = Drow["CARAT"];
                        DRNew["EMPLOYEE_ID"] = Drow["EMPLOYEE_ID"];
                        DRNew["EMPLOYEECODE"] = Drow["EMPLOYEECODE"];
                        DRNew["DEPARTMENT_ID"] = Drow["DEPARTMENT_ID"];
                        DRNew["DEPARTMENTNAME"] = Drow["DEPARTMENTNAME"];
                        DtabFoundPacket.Rows.Add(DRNew);
                        DtabFoundPacket.AcceptChanges();

                        DataView dv = DtabFoundPacket.DefaultView;
                        dv.Sort = "KAPANNAME ASC, PACKETNO ASC";
                        MainGridNotFound.DataSource = dv;
                        MainGridNotFound.Refresh();
                        
                        DTabCopyPackets.Rows.RemoveAt(GrdDetFound.FocusedRowHandle);
                        DTabCopyPackets.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void txtEmpCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployeeName.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtEmployeeName.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        txtEmpCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
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

        private void txtKapanPacketTag_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String str1 = txtKapanPacketTag.Text.Trim().Replace("\r\n", ",");
                txtKapanPacketTag.Text = str1;
                txtKapanPacketTag.Select(txtKapanPacketTag.Text.Length, 0);

                bool IsFound = false;
                string[] Str = str1.Split(',');

                BtnPacketTagSearch.Text = "Search (" + txtKapanPacketTag.Text.Split(',').Length + ")";

                //if (IsFound)
                //{
                //    DtabLiveStockDetail.DefaultView.Sort = "SEL DESC";
                //    DtabLiveStockDetail = DtabLiveStockDetail.DefaultView.ToTable();
                //    MainGrdDetail.DataSource = DtabLiveStockDetail;
                //    MainGrdDetail.RefreshDataSource();
                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnPacketTagSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtKapanPacketTag.Text.Trim().Equals(string.Empty))
                {
                    Global.Message("Please Enter PacketNo..");
                    txtKapanPacketTag.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                MainGridNotFound.DataSource = null;
                MainGridFound.DataSource = null;

                string[] SplitPacketNo = txtKapanPacketTag.Text.Split(',');

                DataTable DTabPacketNo = new DataTable("Table");
                DTabPacketNo.Columns.Add(new DataColumn("PACKETTAG", typeof(string)));

                for (int i = 0; i < SplitPacketNo.Length; i++)
                {
                    DataRow DR = DTabPacketNo.NewRow();
                    DR["PACKETTAG"] = SplitPacketNo[i].ToString().Replace("\r\n", "");
                    DTabPacketNo.Rows.Add(DR);
                }
                DTabPacketNo.AcceptChanges();
                string StrXMLPacketNo = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabPacketNo.WriteXml(sw);
                    StrXMLPacketNo = sw.ToString();
                }
                DtabFoundPacket = ObjTrn.GetPredictionCopyPacketDetail(Val.ToInt(txtDepartment.Tag), Val.ToInt64(txtEmployee.Tag), Val.ToBoolean(ChkWithExtraStock.Checked), StrXMLPacketNo);

                if (DtabFoundPacket.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    MainGridNotFound.DataSource = DtabFoundPacket;
                    GrdDetNotFound.RefreshData();
                    return;
                }

                //DataTable DTabNotFoundPacket = DtabFoundPacket
                MainGridFound.DataSource = DtabFoundPacket;
                GrdDetFound.RefreshData();

                //var idsNotInB = DTabPacketNo.AsEnumerable().Select(r => r.Field<Guid>("PACKETTAG"))
                //.Except(DtabFoundPacket.AsEnumerable().Select(r => r.Field<Guid>("PACKETTAG")));

                //IEnumerable<DataRow> dt1Enum = DTabPacketNo.AsEnumerable();
                //IEnumerable<DataRow> dt2Enum = DtabFoundPacket.AsEnumerable();
                //DataTable dtUncommon = dt1Enum.Except(dt2Enum).CopyToDataTable();

                //var diffIDs = DtabFoundPacket.AsEnumerable().Select(r => r.Field<string>("PACKETTAG")).Except(DTabPacketNo.AsEnumerable().Select(r => r.Field<string>("PACKETTAG")));
                //DataTable Table3 = (
                //    from row in DtabFoundPacket.AsEnumerable()
                //    join name  in diffIDs on row.Field<string>("PACKETTAG") equals name
                //    select row).CopyToDataTable();

                //DataTable dtMerged = (from a in DTabPacketNo.AsEnumerable()
                //                      join b in DtabFoundPacket.AsEnumerable() on a["PACKETTAG"].ToString() equals b["PACKETTAG"].ToString() into g
                //                        where g.Count() > 0
                //                        select a).CopyToDataTable();
                
                var Data =  DTabPacketNo.AsEnumerable().Where(r => !DtabFoundPacket.AsEnumerable()
                        .Any(r2 => r["PACKETTAG"].ToString().Trim().ToLower() == r2["PACKETTAG"].ToString().Trim().ToLower()));
                if (Data.Count() > 0)
                {
                    DataTable DtNotFound = DTabPacketNo.AsEnumerable().Where(r => !DtabFoundPacket.AsEnumerable()
                        .Any(r2 => r["PACKETTAG"].ToString().Trim().ToLower() == r2["PACKETTAG"].ToString().Trim().ToLower())).CopyToDataTable();

                    if (DtNotFound.Rows.Count > 0)
                    {
                        MainGridNotFound.DataSource = DtNotFound;
                        GrdDetNotFound.RefreshData();
                    }
                }

                string str = "";
                //if (idsNotInB.Count() > 0)
                //{
                //    DataTable DtabNotFound = (from row in DTabPacketNo.AsEnumerable()
                //                              join id in idsNotInB
                //                        on row.Field<Guid>("PACKETTAG") equals id
                //                        select row).CopyToDataTable();

                //    MainGridNotFound.DataSource = DtabNotFound;
                //    GrdDetNotFound.RefreshData();
                //}
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }
    }
}
