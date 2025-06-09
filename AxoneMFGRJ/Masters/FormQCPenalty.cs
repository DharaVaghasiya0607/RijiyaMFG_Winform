using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Masters
{
    public partial class FormQCPenalty : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PenaltyIncentive ObjPenalty = new BOTRN_PenaltyIncentive();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
        DataTable DtabPenalty = new DataTable();

        public FormQCPenalty()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        public void Clear()
        {
            txtRemark.Tag = string.Empty;
            DTPPanultyDate.Text = Val.ToString(DateTime.Now);
            DTPPanultyDate.Text = string.Empty;
            txtEmployeeCode.Text = string.Empty;
            txtEmployeeCode.Tag = string.Empty;         
            txtKapanName.Text = string.Empty;
            txtKapanName.Tag = string.Empty;
            txtPacketNo.Text = string.Empty;
            txtTag.Text = string.Empty;
            txtNoOfPcs.Text = string.Empty;
            txtRemark.Text = string.Empty;
            DTPFromDate.Text = Val.ToString(DateTime.Now);
            DTPToDate.Text = Val.ToString(DateTime.Now);
            txtRemark.Tag = string.Empty;
            DTPPanultyDate.Focus();

        }

        private void txtEmployeeCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtEmployeeCode.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID,AUTOCONFIRM";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployeeCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtEmployeeCode.Tag = Val.ToInt64(FrmSearch.mDRow["EMPLOYEE_ID"]);                        
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                    //txtKapanName.Focus();
                }
            }
            catch (Exception ex)       
            {
                Global.MessageError(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeCode.Text.Trim().Length == 0)
                {
                    Global.MessageError("Employee Name Is Required");
                    txtEmployeeCode.Focus();
                    return;
                }
                if (txtKapanName.Text.Trim().Length == 0)
                {
                    Global.MessageError("Kapan Name Is Required");
                    txtKapanName.Focus();
                    return;
                }
                if (Val.ToInt(txtPacketNo.Text) == 0)
                {
                    Global.MessageError("Packet No Is Required");
                    txtPacketNo.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    Global.MessageError("Tag Is Required");
                    txtTag.Focus();
                    return;
                }
                if (Val.ToInt(txtNoOfPcs.Text) == 0)
                {
                    Global.MessageError("Panulty Pcs Is Required");
                    txtNoOfPcs.Focus();
                    return;
                }
                
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                QCPanultyProperty Property = new QCPanultyProperty();

                Property.TRN_ID = Val.ToInt64(txtRemark.Tag);
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployeeCode.Tag);
                Property.PENALTYDATE = Val.SqlDate(DTPPanultyDate.Text);
                Property.EMPLOYEENAME = Val.ToString(txtEmployeeCode.Text);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.KAPAN_ID = Val.ToInt(txtKapanName.Tag);
                Property.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                Property.TAG = Val.ToString(txtTag.Text);
                Property.NOOFPCS = Val.ToInt(txtNoOfPcs.Text);
                Property.REMARK = Val.ToString(txtRemark.Text);

                Property = ObjPenalty.SavePanulty(Property);

                ReturnMessageDesc = Property.ReturnMessageDesc;
                ReturnMessageType = Property.ReturnMessageType;

                DtabPenalty.AcceptChanges();
                
              
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    FillPanulty();
                    Clear();
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    DTPPanultyDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
       
        public void FetchValue(DataRow DR)
        {

            txtRemark.Tag = Val.ToString(DR["TRN_ID"]);
            txtEmployeeCode.Tag = Val.ToInt64(DR["EMPLOYEE_ID"]);
            if (Val.IsDate(Val.ToString(DR["PENALTYDATE"])))
            {
                DTPPanultyDate.Value = DateTime.Parse(Val.ToString(DR["PENALTYDATE"]));
            }
            txtEmployeeCode.Text = Val.ToString(DR["EMPLOYEENAME"]);
            txtKapanName.Text = Val.ToString(DR["KAPANNAME"]);
            txtKapanName.Tag = Val.ToString(DR["KAPAN_ID"]);
            txtPacketNo.Text = Val.ToString(DR["PACKETNO"]);
            txtTag.Text = Val.ToString(DR["TAG"]);
            txtNoOfPcs.Text = Val.ToString(DR["NOOFPCS"]);
            txtRemark.Text = Val.ToString(DR["REMARK"]);

            DTPPanultyDate.Focus();
           
        }

       public void FillPanulty()
       {
           DtabPenalty = ObjPenalty.FillPanulty(Val.SqlDate(DTPFromDate.Text), Val.SqlDate(DTPToDate.Text), Val.ToString(CmbKapan.Text));
           MainGrid.DataSource = DtabPenalty;
           MainGrid.Refresh();
       } 

        //public void FillTrnDetails(Int64 TRN_ID)
        //{
        //    this.Cursor = Cursors.WaitCursor;
        //    DataTable DTab = ObjPenalty.GetTrnDetail(TRN_ID);

        //}

        private void GrdPanulty_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Clicks == 2)
            {
                DataRow DR = GrdPanulty.GetDataRow(e.RowHandle);
                FetchValue(DR);
                DR = null;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFromDate = null;
                string StrDate = null;
                string StrKapanName = "";

                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                StrDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                StrKapanName = Val.Trim(CmbKapan.Properties.GetCheckedItems().ToString());

                this.Cursor = Cursors.WaitCursor;
                //GrdPanulty.BeginUpdate(); 
                DataTable DTabData = ObjPenalty.FillPanulty(StrFromDate, StrDate, StrKapanName);

                MainGrid.DataSource = DTabData;
                //GrdPanulty.EndUpdate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjPenalty.FindKapan();
                    FrmSearch.mColumnsToHide = "KAPAN_ID,MANAGER_ID,MANAGERNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
                       
                    }
                    else
                    {
                        txtKapanName.Text = Val.ToString(DBNull.Value);
                        txtKapanName.Tag = Val.ToString(DBNull.Value);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                    txtPacketNo.Focus();
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                QCPanultyProperty Property = new QCPanultyProperty();

                Property.TRN_ID = Val.ToInt64(txtRemark.Tag);
              
                Property = ObjPenalty.DeletePanulty(Property);

                ReturnMessageDesc = Property.ReturnMessageDesc;
                ReturnMessageType = Property.ReturnMessageType;

                DtabPenalty.AcceptChanges();

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    FillPanulty();
                    Clear();
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    DTPPanultyDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable DTab = ObjTrn.GetQCPanultyCompleteGetData(Val.ToString(txtKapanName.Text), Val.ToInt32(txtPacketNo.Text), Val.ToString(txtTag.Text));
                if (DTab.Rows.Count == 0)
                {
                    Global.Message("PACKET IS NOT COMPLETE" + " : " + Val.ToString(txtKapanName.Text) + "-" + Val.ToString(txtPacketNo.Text) + Val.ToString(txtTag.Text));
                }
                else
                {
                    txtEmployeeCode.Text = DTab.Rows[0]["QCLEDGERCODE"].ToString();
                    txtEmployeeCode.Tag = Val.ToInt32(DTab.Rows[0]["QCEMPLOYEE_ID"]);
                    txtCarat.Text = Val.ToString(DTab.Rows[0]["CARAT"]);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
      
    }
}
