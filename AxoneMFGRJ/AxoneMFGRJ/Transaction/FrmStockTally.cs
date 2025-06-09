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
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmStockTally : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_StockTally ObjTrn = new BOTRN_StockTally();
        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabFound = new DataTable();
        DataTable DTabNotFound = new DataTable();
        DataTable DTabExtra = new DataTable();

        DataSet DS = new DataSet();
        DataTable DTabDept = new DataTable();
        DataTable DTabEmp = new DataTable();
        DataTable DTabDetails = new DataTable();
        DataTable DTabRejection = new DataTable();
        string mStrReportTitle = "";
        bool IsRefreshStockTally = false;
        bool IsNextImage = true;

        #region Property Settings

        public FrmStockTally()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();


            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            txtPasswordForDelete.Tag = ObjPer.PASSWORD;

            string department = BOConfiguration.gEmployeeProperty.DEPARTMENTNAME;
            int departement_ID = BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;
            if (department == "ADMIN-DN")
            {
                txtSDepartment.Enabled = true;
            }
            else
            {
                txtSDepartment.Text = department;
                txtSDepartment.Tag = departement_ID;
            }

            if (departement_ID == 4891 || departement_ID == 4892 || departement_ID == 4893) //CLV-BV,CLV-KK,CLV-RK hoy to j DirectScan Button Visible thase otherwise Not : #P : 23-09-2022 
            {
                BtnDirectScan.Visible = true;
            }
            else
            {
                BtnDirectScan.Visible = false;
            }

            this.Show();
            //BtnShow_Click(null, null);
            RbtBarcode_CheckedChanged(null, null);
            txtPasswordForDelete_TextChanged(null, null);
            DTPTransferDate.MaxDate = DateTime.Parse(Val.ToString(DateTime.Now.Date.AddSeconds(86400 - 1)));
            BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A6;
            panelSecondGrid.Visible = false;
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

                string StrScanDepartmentName = "";
                Int32 IntScanDepartment_ID = 0;

                StrScanDepartmentName = Val.ToInt32(txtDepartment.Tag) == 0 ? Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME) : Val.ToString(txtDepartment.Text);
                IntScanDepartment_ID = Val.ToInt32(txtDepartment.Tag) == 0 ? Val.ToInt32(BOConfiguration.gEmployeeProperty.DEPARTMENT_ID) : Val.ToInt32(txtDepartment.Tag);

                int IntI = 0;
                for (IntI = 0; IntI < DTabNotFound.Rows.Count; IntI++)
                {
                    DataRow DR = DTabNotFound.Rows[IntI];
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

                foreach (DataRow DRowFound in DTabFound.Rows)
                {
                    if (txtKapanName.Text.Trim() == Val.ToString(DRowFound["KAPANNAME"]).Trim()
                       && txtPacketNo.Text.Trim() == Val.ToString(DRowFound["PACKETNO"]).Trim()
                       && txtTag.Text.Trim() == Val.ToString(DRowFound["TAG"]).Trim()
                        //&& (Val.ToString(txtDepartment.Tag).Trim().Equals(string.Empty) || Val.ToString(txtDepartment.Tag) == Val.ToString(DRowFound["DEPARTMENT_ID"]).Trim())
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
                    Property.SCANDEPARTMENT_ID = IntScanDepartment_ID;
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag) != 0 ? Val.ToInt64(txtManager.Tag) : Val.ToInt64(DRowNotFound["MANAGER_ID"]);
                    Property.BARCODE = Val.ToString(DRowNotFound["BARCODE"]);
                    Property.PKTSRNO = Val.ToInt32(DRowNotFound["PKTSRNO"]);

                    Property = ObjTrn.Save(Property);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        DataRow DRNew = DTabFound.NewRow();

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
                        DRNew["MANAGER_ID"] = DRowNotFound["MANAGER_ID"];
                        DRNew["MANAGERNAME"] = DRowNotFound["MANAGERNAME"];
                        DRNew["SCANDEPARTMENTNAME"] = StrScanDepartmentName;
                        DRNew["BARCODE"] = DRowNotFound["BARCODE"];
                        DRNew["PKTSRNO"] = DRowNotFound["PKTSRNO"];

                        DTabFound.Rows.Add(DRNew);

                        DTabNotFound.Rows[IntI].Delete();
                        DTabNotFound.AcceptChanges();
                        DTabFound.AcceptChanges();
                    }


                    //#p : 11-03-2021 : Jo Extra Valo Stock Found ma aave to Extra ni grid mathi remove thay jase..
                    for (int IntK = 0; IntK < DTabExtra.Rows.Count; IntK++)
                    {
                        DataRow DRExtra = DTabExtra.Rows[IntK];
                        if (txtKapanName.Text.Trim() == Val.ToString(DRExtra["KAPANNAME"]).Trim()
                           && txtPacketNo.Text.Trim() == Val.ToString(DRExtra["PACKETNO"]).Trim()
                           && txtTag.Text.Trim() == Val.ToString(DRExtra["TAG"]).Trim()
                           )
                        {
                            DTabExtra.Rows[IntK].Delete();
                            DTabExtra.AcceptChanges();
                            break;
                        }
                    }


                }
                //else if (DRowNotFound !=null)
                else if (ISExists)
                {
                    //Global.Message(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " : ALREADY SCANNED IN FOUND GRID");
                    lblMessage.Text = txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " : Already Scanned In FOUND Grid";
                }
                else if (DRowNotFound == null)
                {

                    foreach (DataRow DrExtra in DTabExtra.Rows)
                    {
                        if (txtKapanName.Text.Trim() == Val.ToString(DrExtra["KAPANNAME"]).Trim()
                           && txtPacketNo.Text.Trim() == Val.ToString(DrExtra["PACKETNO"]).Trim()
                           && txtTag.Text.Trim() == Val.ToString(DrExtra["TAG"]).Trim()
                           )
                        {
                            lblMessage.Text = txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " : Already Scanned In EXTRA Grid";
                            Global.Message(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " : Already Scanned In EXTRA Grid.");
                            txtKapanName.Text = string.Empty;
                            txtPacketNo.Text = string.Empty;
                            txtTag.Text = string.Empty;
                            txtKapanName.Focus();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }

                    Global.Message(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " : Is Not In Your Stock So this Packet Transfer Into Extra Stock.");

                    TrnStockTallyProperty Property = new TrnStockTallyProperty();
                    Property.STOCKTALLYDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                    Property.PACKET_ID = 0;
                    Property.KAPANNAME = txtKapanName.Text;
                    Property.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                    Property.TAG = Val.ToString(txtTag.Text);
                    Property.CARAT = 0.00;
                    Property.TRN_ID = 0;
                    Property.FOUNDSTATUS = "EXTRA";
                    //Property.EMPLOYEE_ID = Val.ToInt32(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                    //Property.DEPARTMENT_ID = IntScanDepartment_ID;
                    Property.SCANDEPARTMENT_ID = IntScanDepartment_ID;
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag) != 0 ? Val.ToInt64(txtManager.Tag) : 0; //Val.ToInt64(DRowNotFound["MANAGER_ID"])
                    Property.BARCODE = "";
                    Property.PKTSRNO = 0;
                    Property = ObjTrn.Save(Property);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        DataRow DRNew = DTabExtra.NewRow();

                        DRNew["KAPANNAME"] = txtKapanName.Text;
                        DRNew["PACKETNO"] = Val.ToInt32(txtPacketNo.Text);
                        DRNew["TAG"] = Val.ToString(txtTag.Text);
                        DRNew["PACKETTAG"] = txtPacketNo.Text + txtTag.Text;
                        DRNew["SCANDEPARTMENTNAME"] = StrScanDepartmentName;
                        DRNew["BARCODE"] = "";
                        DRNew["PKTSRNO"] = 0;

                        DTabExtra.Rows.Add(DRNew);
                        DTabExtra.AcceptChanges();
                    }
                }

                if (GrdDetFound.RowCount > 1)
                {
                    GrdDetFound.FocusedRowHandle = GrdDetFound.RowCount - 1;
                }
                if (GrdDetExtra.RowCount > 1)
                {
                    GrdDetExtra.FocusedRowHandle = GrdDetExtra.RowCount - 1;
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
                //if (txtDepartment.Text.Length == 0)
                //{
                //    Global.MessageError("Department Name Is Requried");
                //    txtDepartment.Focus();
                //    return;
                //}

                ChkPending.Checked = false; // K : 07/12/2022
                ChkConfirmed.Checked = false;
                this.Cursor = Cursors.WaitCursor;
                GrdDetNotFound.Columns["CONFDATE"].ClearFilter();             

                this.Cursor = Cursors.Default;
                if ((Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty)) && (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty)) && (Val.ToString(txtManager.Text).Trim().Equals(string.Empty))
                    && (Val.ToString(txtPasswordForDelete.Tag).Trim().ToUpper() != txtPasswordForDelete.Text.Trim().ToUpper())
                    )
                {
                    Global.MessageError("Atleast One Filter Is Required From Department , Employee And Manager.");
                    return;
                }

                if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
                    txtDepartment.Tag = string.Empty;

                if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                    txtEmployee.Tag = string.Empty;

                if (Val.ToString(txtManager.Text).Trim().Equals(string.Empty))
                    txtManager.Tag = string.Empty;

                //if ((Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty)) && (Val.ToString(txtManager.Text).Trim().Equals(string.Empty)))
                //{
                //    txtManager.Tag = string.Empty;
                //    Global.Message("Employee Or Manager One of the Filter is Required..");
                //    return;
                //}

                if (IsRefreshStockTally == true)
                {
                    if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                    {
                        txtEmployee.Tag = Val.ToInt64(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                    }
                    if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
                    {
                        txtDepartment.Tag = Val.ToInt64(BOConfiguration.gEmployeeProperty.DEPARTMENT_ID);
                    }
                }

                this.Cursor = Cursors.WaitCursor;

                DataSet DS = ObjTrn.GetData(Val.SqlDate(DTPTransferDate.Value.ToShortDateString()), Val.ToInt(txtDepartment.Tag), Val.ToInt64(txtEmployee.Tag), Val.ToBoolean(ChkWithExtraStock.Checked), Val.ToInt64(txtManager.Tag));

                DTabNotFound.Rows.Clear();
                DTabFound.Rows.Clear();
                DTabExtra.Rows.Clear();

                DTabNotFound = DS.Tables[0];
                DTabFound = DS.Tables[1];
                DTabExtra = DS.Tables[2];

                MainGridNotFound.DataSource = DTabNotFound;
                MainGridNotFound.Refresh();

                MainGridFound.DataSource = DTabFound;
                MainGridFound.Refresh();

                MainGridExtra.DataSource = DTabExtra;
                MainGridExtra.Refresh();

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                RbtBarcode.Checked = true;
                //txtKapanName.Focus();
                txtBarcode.Focus();
                PanelPacketNo.Visible = false;
                PanelPktSerialNo.Visible = false;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnDeleteStockTally_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure For Delete ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            TrnStockTallyProperty Property = new TrnStockTallyProperty();

            Property.EMPLOYEE_ID = !Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty) ? Val.ToInt64(txtEmployee.Tag) : Val.ToInt64(BOConfiguration.gEmployeeProperty.LEDGER_ID);
            Property.DEPARTMENT_ID = !Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty) ? Val.ToInt32(txtDepartment.Tag) : Val.ToInt32(BOConfiguration.gEmployeeProperty.DEPARTMENT_ID);
            Property.MANAGER_ID = Val.ToString(txtManager.Text).Trim().Equals(string.Empty) ? Val.ToInt64(txtManager.Tag) : 0;
            Property.STOCKTALLYDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());

            Property.FOUNDSTATUS = "FOUND";
            Property = ObjTrn.Delete(Property);
            this.Cursor = Cursors.Default;
            Global.Message(Property.ReturnMessageDesc);
            if (Property.ReturnMessageType == "SUCCESS")
            {
                DTabExtra.Rows.Clear();
                DTabFound.Rows.Clear();
                DTabNotFound.Rows.Clear();
                txtDepartment.Focus();
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

                        DTabNotFound.Rows.Clear();
                        DTabFound.Rows.Clear();
                        DTabExtra.Rows.Clear();
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

                        DTabNotFound.Rows.Clear();
                        DTabFound.Rows.Clear();
                        DTabExtra.Rows.Clear();
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

        private void txtPasswordForDelete_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtPasswordForDelete.Tag) != "" && Val.ToString(txtPasswordForDelete.Tag).ToUpper() == txtPasswordForDelete.Text.ToUpper())
                {
                    BtnDeleteStockTally.Visible = true;
                    //btnRefreshStockTally.Visible = true;
                }
                else
                {
                    BtnDeleteStockTally.Visible = false;
                    //btnRefreshStockTally.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void RbtBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtBarcode.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                txtBarcode.Focus();
            }
            else if (RbtPacketNo.Checked)
            {
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                txtKapanName.Focus();
            }
            else if (RbtPktSerialNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }
            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
        }

        private void txtBarcode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text.Trim().Length == 0)
                {
                    //txtBarcode.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DataRow DRowNotFound = null;
                string StrScanDepartmentName = "";
                Int32 IntScanDepartment_ID = 0;
                StrScanDepartmentName = Val.ToInt32(txtDepartment.Tag) == 0 ? Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME) : Val.ToString(txtDepartment.Text);
                IntScanDepartment_ID = Val.ToInt32(txtDepartment.Tag) == 0 ? Val.ToInt32(BOConfiguration.gEmployeeProperty.DEPARTMENT_ID) : Val.ToInt32(txtDepartment.Tag);

                string BARCODENO = "", KAPANNAME = "", PACKETNO = "", TAG = ""; //KAPANNAME = "", PACKETNO = "", TAG = "", 

                BARCODENO = txtBarcode.Text.Trim();

                int IntI = 0;
                for (IntI = 0; IntI < DTabNotFound.Rows.Count; IntI++)
                {
                    DataRow DR = DTabNotFound.Rows[IntI];
                    if (txtBarcode.Text.Trim() == Val.ToString(DR["BARCODE"]).Trim())
                    {
                        DRowNotFound = DR;
                        //KAPANNAME = Val.ToString(DRowNotFound["KAPANNAME"]);
                        //PACKETNO = Val.ToString(DRowNotFound["PACKETNO"]);
                        //TAG = Val.ToString(DRowNotFound["TAG"]);
                        BARCODENO = Val.ToString(DRowNotFound["BARCODE"]);
                        break;
                    }
                }

                bool ISExists = false;

                foreach (DataRow DRowFound in DTabFound.Rows)
                {
                    if (txtBarcode.Text.Trim() == Val.ToString(DRowFound["BARCODE"]).Trim())
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
                    Property.SCANDEPARTMENT_ID = IntScanDepartment_ID;
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag) != 0 ? Val.ToInt64(txtManager.Tag) : Val.ToInt64(DRowNotFound["MANAGER_ID"]);
                    Property.BARCODE = Val.ToString(DRowNotFound["BARCODE"]);
                    Property.PKTSRNO = Val.ToInt32(DRowNotFound["PKTSRNO"]);

                    Property = ObjTrn.Save(Property);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        DataRow DRNew = DTabFound.NewRow();

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
                        DRNew["MANAGER_ID"] = DRowNotFound["MANAGER_ID"];
                        DRNew["MANAGERNAME"] = DRowNotFound["MANAGERNAME"];
                        DRNew["SCANDEPARTMENTNAME"] = StrScanDepartmentName;
                        DRNew["BARCODE"] = DRowNotFound["BARCODE"];
                        DRNew["PKTSRNO"] = DRowNotFound["PKTSRNO"];

                        DTabFound.Rows.Add(DRNew);

                        DTabNotFound.Rows[IntI].Delete();
                        DTabNotFound.AcceptChanges();
                        DTabFound.AcceptChanges();
                    }


                    //#p : 11-03-2021 : Jo Extra Valo Stock Found ma aave to Extra ni grid mathi remove thay jase..
                    for (int IntK = 0; IntK < DTabExtra.Rows.Count; IntK++)
                    {
                        DataRow DRExtra = DTabExtra.Rows[IntK];
                        if (BARCODENO == Val.ToString(DRExtra["BARCODE"]).Trim())
                        {
                            DTabExtra.Rows[IntK].Delete();
                            DTabExtra.AcceptChanges();
                            break;
                        }
                    }


                }
                //else if (DRowNotFound !=null)
                else if (ISExists)
                {
                    //Global.Message(BARCODENO + " : ALREADY SCANNED IN FOUND GRID");
                    lblMessage.Text = BARCODENO + " : Already Scanned In FOUND Grid";
                }
                else if (DRowNotFound == null)
                {
                    foreach (DataRow DrExtra in DTabExtra.Rows)
                    {
                        if (BARCODENO == Val.ToString(DrExtra["BARCODE"]).Trim())
                        {
                            //Global.Message(BARCODENO + " : ALREADY SCANNED IN EXTRA GRID");
                            lblMessage.Text = BARCODENO + " : Already Scanned In EXTRA Grid";
                            txtBarcode.Text = string.Empty;
                            txtBarcode.Focus();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }

                    Global.Message(BARCODENO + " : Is Not In Your Stock So this Packet Transfer Into Extra Stock.");

                    TrnStockTallyProperty Property = new TrnStockTallyProperty();
                    Property.STOCKTALLYDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                    Property.PACKET_ID = 0;
                    Property.KAPANNAME = KAPANNAME;
                    Property.PACKETNO = Val.ToInt32(PACKETNO);
                    Property.TAG = TAG;
                    Property.CARAT = 0.00;
                    Property.TRN_ID = 0;
                    Property.FOUNDSTATUS = "EXTRA";
                    Property.EMPLOYEE_ID = Val.ToInt32(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                    Property.DEPARTMENT_ID = IntScanDepartment_ID;
                    Property.SCANDEPARTMENT_ID = IntScanDepartment_ID;
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag) != 0 ? Val.ToInt64(txtManager.Tag) : 0; //Val.ToInt64(DRowNotFound["MANAGER_ID"])
                    Property.BARCODE = txtBarcode.Text;
                    Property.PKTSRNO = 0;

                    Property = ObjTrn.Save(Property);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        DataRow DRNew = DTabExtra.NewRow();

                        DRNew["KAPANNAME"] = KAPANNAME;
                        DRNew["PACKETNO"] = Val.ToInt32(PACKETNO);
                        DRNew["TAG"] = TAG;
                        DRNew["PACKETTAG"] = PACKETNO + TAG;
                        DRNew["SCANDEPARTMENTNAME"] = StrScanDepartmentName;
                        DRNew["BARCODE"] = txtBarcode.Text;
                        DRNew["PKTSRNO"] = 0;

                        DTabExtra.Rows.Add(DRNew);
                        DTabExtra.AcceptChanges();
                    }
                }

                if (GrdDetFound.RowCount > 1)
                {
                    GrdDetFound.FocusedRowHandle = GrdDetFound.RowCount - 1;
                }
                if (GrdDetExtra.RowCount > 1)
                {
                    GrdDetExtra.FocusedRowHandle = GrdDetExtra.RowCount - 1;
                }

                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtSrNoSerialNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtSrNoKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtSrNoSerialNo.Text) == 0)
                {
                    txtSrNoKapanName.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DataRow DRowNotFound = null;
                string StrScanDepartmentName = "";
                Int32 IntScanDepartment_ID = 0;
                StrScanDepartmentName = Val.ToInt32(txtDepartment.Tag) == 0 ? Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME) : Val.ToString(txtDepartment.Text);
                IntScanDepartment_ID = Val.ToInt32(txtDepartment.Tag) == 0 ? Val.ToInt32(BOConfiguration.gEmployeeProperty.DEPARTMENT_ID) : Val.ToInt32(txtDepartment.Tag);

                string KAPANNAME = "", PACKETNO = "", TAG = "", PKTSERIALNO = ""; //, 

                KAPANNAME = txtSrNoKapanName.Text.Trim();
                PKTSERIALNO = txtSrNoSerialNo.Text.Trim();

                int IntI = 0;
                for (IntI = 0; IntI < DTabNotFound.Rows.Count; IntI++)
                {
                    DataRow DR = DTabNotFound.Rows[IntI];
                    if (txtSrNoKapanName.Text.Trim() == Val.ToString(DR["KAPANNAME"]).Trim()
                       && txtSrNoSerialNo.Text.Trim() == Val.ToString(DR["PKTSRNO"]).Trim())
                    {
                        DRowNotFound = DR;
                        KAPANNAME = Val.ToString(DRowNotFound["KAPANNAME"]);
                        PKTSERIALNO = Val.ToString(DRowNotFound["PKTSRNO"]);
                        //PACKETNO = Val.ToString(DRowNotFound["PACKETNO"]);
                        //TAG = Val.ToString(DRowNotFound["TAG"]);
                        break;
                    }
                }

                bool ISExists = false;

                foreach (DataRow DRowFound in DTabFound.Rows)
                {
                    if (txtSrNoKapanName.Text == Val.ToString(DRowFound["KAPANNAME"]).Trim()
                       && txtSrNoSerialNo.Text == Val.ToString(DRowFound["PKTSRNO"]).Trim()
                        //&& PACKETNO == Val.ToString(DRowFound["PACKETNO"]).Trim()
                        //&& TAG == Val.ToString(DRowFound["TAG"]).Trim()
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
                    Property.SCANDEPARTMENT_ID = IntScanDepartment_ID;
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag) != 0 ? Val.ToInt64(txtManager.Tag) : Val.ToInt64(DRowNotFound["MANAGER_ID"]);
                    Property.BARCODE = Val.ToString(DRowNotFound["BARCODE"]);
                    Property.PKTSRNO = Val.ToInt32(DRowNotFound["PKTSRNO"]);

                    Property = ObjTrn.Save(Property);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        DataRow DRNew = DTabFound.NewRow();

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
                        DRNew["MANAGER_ID"] = DRowNotFound["MANAGER_ID"];
                        DRNew["MANAGERNAME"] = DRowNotFound["MANAGERNAME"];
                        DRNew["SCANDEPARTMENTNAME"] = StrScanDepartmentName;
                        DRNew["BARCODE"] = DRowNotFound["BARCODE"];
                        DRNew["PKTSRNO"] = DRowNotFound["PKTSRNO"];

                        DTabFound.Rows.Add(DRNew);

                        DTabNotFound.Rows[IntI].Delete();
                        DTabNotFound.AcceptChanges();
                        DTabFound.AcceptChanges();
                    }


                    //#p : 11-03-2021 : Jo Extra Valo Stock Found ma aave to Extra ni grid mathi remove thay jase..
                    for (int IntK = 0; IntK < DTabExtra.Rows.Count; IntK++)
                    {
                        DataRow DRExtra = DTabExtra.Rows[IntK];
                        if (KAPANNAME == Val.ToString(DRExtra["KAPANNAME"]).Trim()
                           && PKTSERIALNO == Val.ToString(DRExtra["PKTSRNO"]).Trim()
                           )
                        {
                            DTabExtra.Rows[IntK].Delete();
                            DTabExtra.AcceptChanges();
                            break;
                        }
                    }


                }
                //else if (DRowNotFound !=null)
                else if (ISExists)
                {
                    //Global.Message(KAPANNAME + "-" + PKTSERIALNO + " : ALREADY SCANNED IN FOUND GRID");
                    lblMessage.Text = KAPANNAME + "-" + PKTSERIALNO + " : Already Scanned In FOUND Grid";
                }
                else if (DRowNotFound == null)
                {
                    foreach (DataRow DrExtra in DTabExtra.Rows)
                    {
                        if (KAPANNAME == Val.ToString(DrExtra["KAPANNAME"]).Trim()
                           && PKTSERIALNO == Val.ToString(DrExtra["PKTSRNO"]).Trim())
                        {
                            //Global.Message(KAPANNAME + "-" + PKTSERIALNO + " : ALREADY SCANNED IN EXTRA GRID");
                            lblMessage.Text = KAPANNAME + "-" + PKTSERIALNO + " : Already Scanned In EXTRA Grid";
                            txtSrNoKapanName.Text = string.Empty;
                            txtSrNoSerialNo.Text = string.Empty;
                            txtSrNoKapanName.Focus();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }

                    Global.Message(KAPANNAME + "-" + PKTSERIALNO + " : Is Not In Your Stock So this Packet Transfer Into Extra Stock.");

                    TrnStockTallyProperty Property = new TrnStockTallyProperty();
                    Property.STOCKTALLYDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                    Property.PACKET_ID = 0;
                    Property.KAPANNAME = txtSrNoKapanName.Text;
                    Property.PACKETNO = Val.ToInt32(PACKETNO);
                    Property.TAG = TAG;
                    Property.CARAT = 0.00;
                    Property.TRN_ID = 0;
                    Property.FOUNDSTATUS = "EXTRA";
                    Property.SCANDEPARTMENT_ID = IntScanDepartment_ID;
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag) != 0 ? Val.ToInt64(txtManager.Tag) : 0; //Val.ToInt64(DRowNotFound["MANAGER_ID"])
                    Property.BARCODE = "";
                    Property.PKTSRNO = Val.ToInt32(txtSrNoSerialNo.Text);

                    Property = ObjTrn.Save(Property);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        DataRow DRNew = DTabExtra.NewRow();

                        DRNew["KAPANNAME"] = txtSrNoKapanName.Text;
                        DRNew["PACKETNO"] = Val.ToInt32(PACKETNO);
                        DRNew["TAG"] = TAG;
                        DRNew["PACKETTAG"] = PACKETNO + TAG;
                        DRNew["SCANDEPARTMENTNAME"] = StrScanDepartmentName;
                        DRNew["BARCODE"] = "";
                        DRNew["PKTSRNO"] = Val.ToInt32(txtSrNoSerialNo.Text);

                        DTabExtra.Rows.Add(DRNew);
                        DTabExtra.AcceptChanges();
                    }
                }

                if (GrdDetFound.RowCount > 1)
                {
                    GrdDetFound.FocusedRowHandle = GrdDetFound.RowCount - 1;
                }
                if (GrdDetExtra.RowCount > 1)
                {
                    GrdDetExtra.FocusedRowHandle = GrdDetExtra.RowCount - 1;
                }

                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                txtSrNoKapanName.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnDirectScan_Click(object sender, EventArgs e)
        {

            try
            {
                DataRow[] Dr = DTabNotFound.Select("ISFOUND = True");
                if (Dr == null || Dr.Length <= 0)
                {
                    Global.Message("Please Select Stone's That You Want To Direct Scan..");
                    return;
                }

                DataTable DtDirectScanStock = Dr.CopyToDataTable();
                string StrDirectScanXml = "";

                using (StringWriter sw = new StringWriter())
                {
                    DtDirectScanStock.TableName = "Table1";
                    DtDirectScanStock.WriteXml(sw);
                    StrDirectScanXml = sw.ToString();
                }

                string StrScanDepartmentName = Val.ToInt32(txtDepartment.Tag) == 0 ? Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME) : Val.ToString(txtDepartment.Text);
                int IntScanDepartment_ID = Val.ToInt32(txtDepartment.Tag) == 0 ? Val.ToInt32(BOConfiguration.gEmployeeProperty.DEPARTMENT_ID) : Val.ToInt32(txtDepartment.Tag);

                TrnStockTallyProperty Property = new TrnStockTallyProperty();

                Property.STOCKTALLYDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                Property.SCANDEPARTMENT_ID = IntScanDepartment_ID;
                Property.FOUNDSTATUS = "FOUND";
                //Property.SCANDEPARTMENT_ID = IntScanDepartment_ID;
                Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);

                DataTable DTabSaveKpSummary = ObjTrn.SaveDirectScan(Property, StrDirectScanXml);

                if (DTabSaveKpSummary.Rows.Count == 0)
                {
                    Global.Message("Oops Something Went Wrong...Pls Contact To Your Administrator..!");
                    return;
                }

                Property.ReturnMessageType = Val.ToString(DTabSaveKpSummary.Rows[0]["RETURNMESSAGETYPE"]);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    foreach (DataRow DRowNotFound in DtDirectScanStock.Rows)
                    {

                        DataRow DRNew = DTabFound.NewRow();

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
                        DRNew["MANAGER_ID"] = DRowNotFound["MANAGER_ID"];
                        DRNew["MANAGERNAME"] = DRowNotFound["MANAGERNAME"];
                        DRNew["SCANDEPARTMENTNAME"] = StrScanDepartmentName;
                        DRNew["BARCODE"] = DRowNotFound["BARCODE"];
                        DRNew["PKTSRNO"] = DRowNotFound["PKTSRNO"];

                        DTabFound.Rows.Add(DRNew);

                        DTabNotFound.Select("PACKET_ID = '" + DRowNotFound["PACKET_ID"].ToString() + "'")
                         .ToList<DataRow>()
                         .ForEach(r => DTabNotFound.Rows.Remove(r));
                    }
                    DTabNotFound.AcceptChanges();
                    DTabFound.AcceptChanges();


                    //SlipPrint  Code
                    Global.Message("Start Printing..Please Wait");
                    this.Cursor = Cursors.WaitCursor;
                    if (DTabSaveKpSummary.Rows.Count == 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError("There Is No Data For Print");
                        return;
                    }
                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowWithPrint("StockTallySlipPrint", DTabSaveKpSummary);
                    this.Cursor = Cursors.Default;
                    //End : SlipPrint Code

                    ChkAllNotFoundStock.Checked = false;
                }




            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void ChkAllNotFoundStock_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GrdDetNotFound.DataRowCount; i++)
            {
                DataRow row = GrdDetNotFound.GetDataRow(i);
                row["ISFOUND"] = ChkAllNotFoundStock.Checked;
            }
        }

        private void ChkAllFoundStock_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GrdDetFound.DataRowCount; i++)
            {
                DataRow row = GrdDetFound.GetDataRow(i);
                row["ISFOUND"] = ChkAllFoundStock.Checked;
            }
        }

        private void BtnStockTallySlipPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] Dr = DTabFound.Select("ISFOUND = True");
                if (Dr == null || Dr.Length <= 0)
                {
                    Global.Message("Please Select Stone's That You Want To Print..");
                    return;
                }

                string StrEmpMngr = txtManager.Text.Trim().Equals(string.Empty) ? "Emp" : "Mngr";
                string StrMessage = Val.ToInt(DTabNotFound.Rows.Count) > 0 ? "Partial Stock Tally" : "Stock Tally Successfully";
                // K : 07/12/2022                
                string StrMessageTally = "";
                
             
                DataRow[] ConfDate = DTabNotFound.Select("CONFDATE IS NOT NULL");
                if(ConfDate.Length != 0)
                {
                    StrMessageTally = "Not Tally";
                }
                else
                {
                    StrMessageTally = "Tally";
                }                   

                if (txtEmployee.Text.Trim().Equals(string.Empty) && txtManager.Text.Trim().Equals(string.Empty))
                {
                    Global.Message("Employee Or Manager One of the Filter Is Required..");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (RbtEmployeeWisePrint.Checked) //EmployeeWise Print
                {

                    DataTable DTabFoundSelection = Dr.CopyToDataTable();

                    if (DTabFoundSelection.DefaultView.ToTable(true, "EMPLOYEECODE").Rows.Count > 1)
                    {
                        this.Cursor = Cursors.Default;
                        Global.Message("Opps... You Are Selecting Muliple Employee Packets For Print. Please Select Only Single Employee Packets");
                        return;
                    }

                    string StrScanEmployeeCode = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERCODE);
                    string StrScanHostName = Val.ToString(BOConfiguration.ComputerName);
                    string StrScanIPAddress = Val.ToString(BOConfiguration.ComputerIP);

                    DataColumnCollection columns = DTabFoundSelection.Columns;
                    if (!columns.Contains("PCS"))
                    {
                        DTabFoundSelection.Columns.Add(new DataColumn("PCS", typeof(Int32)));
                    }

                    string StrPrintDataXml = string.Empty;
                    DTabFoundSelection.TableName = "Table1";
                    using (StringWriter sw = new StringWriter())
                    {
                        DTabFoundSelection.WriteXml(sw);
                        StrPrintDataXml = sw.ToString();
                    }

                    DataTable DtabSlipPrint = ObjTrn.PrintHistorySave(StrPrintDataXml, StrScanEmployeeCode, StrScanHostName, StrScanIPAddress, StrEmpMngr, StrMessage, StrMessageTally); //Save History Of SlipPrint Data

                    /* 
                     DataTable DtabSlipPrint = DTabFoundSelection.AsEnumerable()
                      .OrderBy(r => r.Field<string>("KAPANNAME"))
                         //.GroupBy(r => r.Field<string>("KAPANNAME"))
                        .GroupBy(r => new { KAPANNAME = r["KAPANNAME"], EMPLOYEECODE = r["EMPLOYEECODE"] })
                      .Select(g =>
                      {
                          var row = DTabFoundSelection.NewRow();
                          row["EMPLOYEECODE"] = g.Key.EMPLOYEECODE;
                          row["KAPANNAME"] = g.Key.KAPANNAME;
                          row["PCS"] = g.Count();
                          row["CARAT"] = g.Sum(r => r.Field<decimal>("CARAT"));
                          return row;
                      }).CopyToDataTable();


                     DtabSlipPrint.Columns.Add(new DataColumn("SCANHOSTNAME", typeof(string)));
                     DtabSlipPrint.Columns.Add(new DataColumn("SCANIPADDRESS", typeof(string)));
                     DtabSlipPrint.Columns.Add(new DataColumn("SCANEMPLOYEECODE", typeof(string)));
                     DtabSlipPrint.Columns.Add(new DataColumn("EMPMNGR", typeof(string)));
                     DtabSlipPrint.Columns.Add(new DataColumn("STOCKTALLYMESSAGE", typeof(string)));

                     DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("SCANEMPLOYEECODE", StrScanEmployeeCode));
                     DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("SCANIPADDRESS", StrScanIPAddress));
                     DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("SCANHOSTNAME", StrScanHostName));
                     DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("EMPMNGR", StrEmpMngr));
                     DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("STOCKTALLYMESSAGE", StrMessage));
                     */

                    DTabFoundSelection.Columns.Remove("PCS");
                    DTabFoundSelection.AcceptChanges();


                    if (DtabSlipPrint.Rows.Count == 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError("There Is No Data For Print");
                        return;
                    }
                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowWithPrint("StockTallySlipPrint", DtabSlipPrint);
                }
                else //ManagerWise Print
                {
                    DataTable DTabFoundSelection = Dr.CopyToDataTable();

                    if (DTabFoundSelection.DefaultView.ToTable(true, "MANAGERNAME").Rows.Count > 1)
                    {
                        this.Cursor = Cursors.Default;
                        Global.Message("Opps... You Are Selecting Muliple Manager Packets For Print. Please Select Only Single Employee Packets");
                        return;
                    }

                    string StrScanEmployeeCode = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERCODE);
                    string StrScanHostName = Val.ToString(BOConfiguration.ComputerName);
                    string StrScanIPAddress = Val.ToString(BOConfiguration.ComputerIP);


                    DataColumnCollection columns = DTabFoundSelection.Columns;
                    if (!columns.Contains("PCS"))
                    {
                        DTabFoundSelection.Columns.Add(new DataColumn("PCS", typeof(Int32)));
                    }

                    string StrPrintDataXml = string.Empty;
                    DTabFoundSelection.TableName = "Table1";
                    using (StringWriter sw = new StringWriter())
                    {
                        DTabFoundSelection.WriteXml(sw);
                        StrPrintDataXml = sw.ToString();
                    }

                    DataTable DtabSlipPrint = ObjTrn.PrintHistorySave(StrPrintDataXml, StrScanEmployeeCode, StrScanHostName, StrScanIPAddress, StrEmpMngr, StrMessage,StrMessageTally); //Save History Of SlipPrint Data

                    /*
                    DataTable DtabSlipPrint = DTabFoundSelection.AsEnumerable()
                     .OrderBy(r => r.Field<string>("KAPANNAME"))
                        //.GroupBy(r => r.Field<string>("KAPANNAME"))
                       .GroupBy(r => new { KAPANNAME = r["KAPANNAME"], MANAGERNAME = r["MANAGERNAME"] })
                     .Select(g =>
                     {
                         var row = DTabFoundSelection.NewRow();
                         row["EMPLOYEECODE"] = g.Key.MANAGERNAME;
                         row["KAPANNAME"] = g.Key.KAPANNAME;
                         row["PCS"] = g.Count();
                         row["CARAT"] = g.Sum(r => r.Field<decimal>("CARAT"));
                         return row;
                     }).CopyToDataTable();


                    DtabSlipPrint.Columns.Add(new DataColumn("SCANHOSTNAME", typeof(string)));
                    DtabSlipPrint.Columns.Add(new DataColumn("SCANIPADDRESS", typeof(string)));
                    DtabSlipPrint.Columns.Add(new DataColumn("SCANEMPLOYEECODE", typeof(string)));
                    DtabSlipPrint.Columns.Add(new DataColumn("EMPMNGR", typeof(string)));
                    DtabSlipPrint.Columns.Add(new DataColumn("STOCKTALLYMESSAGE", typeof(string)));

                    DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("SCANEMPLOYEECODE", StrScanEmployeeCode));
                    DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("SCANIPADDRESS", StrScanIPAddress));
                    DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("SCANHOSTNAME", StrScanHostName));
                    DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("EMPMNGR", StrEmpMngr));
                    DtabSlipPrint.AsEnumerable().ToList().ForEach(p => p.SetField<string>("STOCKTALLYMESSAGE", StrMessage));
                    */

                    DTabFoundSelection.Columns.Remove("PCS");
                    DTabFoundSelection.AcceptChanges();


                    if (DtabSlipPrint.Rows.Count == 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError("There Is No Data For Print");
                        return;
                    }
                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowWithPrint("StockTallySlipPrint", DtabSlipPrint);
                }
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (txtSDepartment.Text == string.Empty)
                {
                    txtSDepartment.Tag = string.Empty;
                }
                DS = ObjTrn.GetDetailData(Val.SqlDate(DTPStockDate.Value.ToShortDateString()), Val.ToInt(txtSDepartment.Tag));
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BtnShowDetail.Enabled = true;

                DTabDetails = DS.Tables[0];
                DTabDept = DS.Tables[1];
                DTabEmp = DS.Tables[2];
                DTabRejection = DS.Tables[3];

                GrdDet.BeginUpdate();
                GrdDept.BeginUpdate();
                GrdEmp.BeginUpdate();
                GrdRejection.BeginUpdate();

                MainGridDet.DataSource = DTabDetails;
                GrdDet.RefreshData();

                MainGridDept.DataSource = DTabDept;
                GrdDept.RefreshData();

                MainGridEmp.DataSource = DTabEmp;
                GrdEmp.RefreshData();

                MainGridRejection.DataSource = DTabRejection; // K : 07/12/2022
                GrdRejection.RefreshData();

                GrdDet.BestFitColumns();
                GrdDept.BestFitColumns();
                GrdEmp.BestFitColumns();
                GrdRejection.BestFitColumns();

                GrdDet.EndUpdate();
                GrdDept.EndUpdate();
                GrdEmp.EndUpdate();
                GrdRejection.EndUpdate();

            }
            catch (Exception Ex)
            {
                BtnShowDetail.Enabled = true;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void txtSDepartment_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtSDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtSDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
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

        private void BtnShowDetail_Click(object sender, EventArgs e)
        {
            try
            {
                DTabDetails.Rows.Clear();
                DTabDept.Rows.Clear();
                DTabEmp.Rows.Clear();
                BtnShowDetail.Enabled = false;
                if (!BackgroundWorker.IsBusy)
                {
                    BackgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }
        }

        private void GrdDept_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GrdDet.Columns["DEPARTMENTNAME"].ClearFilter();
            GrdDet.Columns["DEPARTMENTNAME"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("DEPARTMENTNAME='" + Val.ToString(GrdDept.GetFocusedRowCellValue("DEPARTMENTNAME")) + "'");

            GrdEmp.Columns["DEPARTMENTNAME"].ClearFilter();
            GrdEmp.Columns["DEPARTMENTNAME"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("DEPARTMENTNAME='" + Val.ToString(GrdDept.GetFocusedRowCellValue("DEPARTMENTNAME")) + "'");
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString(mStrReportTitle, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            string StrFilter = "Date : " + DTPStockDate.Text;
            StrFilter = StrFilter + ", Stock Detail";
            //// ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString(StrFilter, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            BrickPageNo.Font = new Font("verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        public void CommonPrintFuction(DevExpress.XtraGrid.GridControl pMainGrid)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = pMainGrid;
                link.Landscape = false;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        public void CommonExcelExportFuction(DevExpress.XtraGrid.GridControl pMainGrid, string pStrFileName)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = pStrFileName;
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = pMainGrid,
                        Landscape = false,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [" + pStrFileName + ".xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            if (DTabDept.Rows.Count <= 0)
            {
                return;
            }
            mStrReportTitle = "Stock Detail (Department Wise)";
            CommonExcelExportFuction(MainGridDept, "StockDepartmentWise");
        }

        private void lblDeptPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTabDept.Rows.Count <= 0)
                {
                    return;
                }
                mStrReportTitle = "Stock Detail (Department Wise)";
                //CommonPrintFuction(MainGridDept);
                this.Cursor = Cursors.WaitCursor;
                DataTable DTab = new DataTable();
                DTab = DTabDept.Copy();

                DataColumn DCol = new DataColumn("REPORTTITLE", typeof(string));
                DCol.DefaultValue = mStrReportTitle;
                DTab.Columns.Add(DCol);

                DTab.Columns["DEPARTMENTNAME"].ColumnName = "PARTICULARS";
                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowWithPrint("StockTallyDeptSlipPrint", DTab);
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void lblEmpExport_Click(object sender, EventArgs e)
        {
            if (DTabEmp.Rows.Count <= 0)
            {
                return;
            }
            mStrReportTitle = "Stock Detail (Employee Wise)";
            CommonExcelExportFuction(MainGridEmp, "StockEmployeeWise");
        }

        private void lblEmpPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTabEmp.Rows.Count <= 0)
                {
                    return;
                }
                mStrReportTitle = "Stock Detail (Employee Wise)";
                //CommonPrintFuction(MainGridEmp);
                this.Cursor = Cursors.WaitCursor;
                DataTable DTab = new DataTable();
                DTab = DTabEmp.Copy();

                DataColumn DCol = new DataColumn("REPORTTITLE", typeof(string));
                DCol.DefaultValue = mStrReportTitle;
                DTab.Columns.Add(DCol);

                DTab.Columns["EMPLOYEECODE"].ColumnName = "PARTICULARS";
                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowWithPrint("StockTallyDeptSlipPrint", DTab);
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void lblDetExport_Click(object sender, EventArgs e)
        {
            if (DTabDetails.Rows.Count <= 0)
            {
                return;
            }
            mStrReportTitle = "Stock Detail";
            CommonExcelExportFuction(MainGridDet, "StockDetail");
        }

        private void lblDetPrint_Click(object sender, EventArgs e)
        {
            if (DTabDetails.Rows.Count <= 0)
            {
                return;
            }
            mStrReportTitle = "Stock Detail";
            CommonPrintFuction(MainGridDet);

        }

        private void GrdDept_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (Val.ToInt32(GrdDept.GetRowCellValue(e.RowHandle, "NOTFOUNDSTOCK")) == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
            }

        }

        private void GrdEmp_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (Val.ToInt32(GrdEmp.GetRowCellValue(e.RowHandle, "NOTFOUNDSTOCK")) == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "FOUNDSTATUS")) == "NOTFOUND")
            {
                e.Appearance.BackColor = Color.White;
            }
            else
            {
                //e.Appearance.BackColor = Color.PaleGreen;
                e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
            }

        }

        private void btnRefreshStockTally_Click(object sender, EventArgs e)
        {
            //IsRefreshStockTally = true;
            //BtnDeleteStockTally_Click(null, null);
            //BtnShow_Click(null, null);
            //IsRefreshStockTally = false;
        }

        private void txtManager_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtManager.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtManager.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);

                        DTabNotFound.Rows.Clear();
                        DTabFound.Rows.Clear();
                        DTabExtra.Rows.Clear();
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

        private void GrdDetNotFound_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            DataRow DR = GrdDetNotFound.GetDataRow(e.RowHandle);                       
            if (Val.ISDate(DR["CONFDATE"]) == true)
            {
                e.Appearance.BackColor = lblConfiredGoods.BackColor;
            }
            else if (Val.ISDate(DR["CONFDATE"]) == false)
            {
                e.Appearance.BackColor = lblPendingsGoods.BackColor;
            }
        }        

        private void BtnLeft_Click_1(object sender, EventArgs e)
        {
            if (IsNextImage)
            {
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A6;
                panelSecondGrid.Visible = false;
                IsNextImage = false;
            }
            else
            {
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                panelSecondGrid.Visible = true;
                panelSecondGrid.Visible = true;
                IsNextImage = true;
            }
        }

        private void lblRejExport_Click(object sender, EventArgs e)
        {
            if (DTabRejection.Rows.Count <= 0)
            {
                return;
            }
            mStrReportTitle = "Rejection Detail";
            CommonExcelExportFuction(MainGridRejection, "RejectionDetail");
        }

        private void lblRejPrint_Click(object sender, EventArgs e)
        {
            if (DTabRejection.Rows.Count <= 0)
            {
                return;
            }
            mStrReportTitle = "Rejection Detail";
            CommonPrintFuction(MainGridRejection);
        }

        private void ChkConfirmed_CheckedChanged(object sender, EventArgs e) // K : 07/12/2022
        {
            if (ChkConfirmed.Checked == true)
            {
                ChkPending.Checked = false;
                this.Cursor = Cursors.WaitCursor;
                GrdDetNotFound.Columns["CONFDATE"].ClearFilter();
                GrdDetNotFound.Columns["CONFDATE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("CONFDATE IS NOT NULL");
                
                this.Cursor = Cursors.Default;
            } 
            else
            {
                GrdDetNotFound.Columns["CONFDATE"].ClearFilter();
            }
        }

        private void ChkPending_CheckedChanged(object sender, EventArgs e) // K : 07/12/2022
        {
            if (ChkPending.Checked == true)
            {
                ChkConfirmed.Checked = false;
                this.Cursor = Cursors.WaitCursor;
                GrdDetNotFound.Columns["CONFDATE"].ClearFilter();
                GrdDetNotFound.Columns["CONFDATE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("CONFDATE IS NULL");
                
                this.Cursor = Cursors.Default;
            }
            else
            {
                GrdDetNotFound.Columns["CONFDATE"].ClearFilter();
            }
        }
    }
}
