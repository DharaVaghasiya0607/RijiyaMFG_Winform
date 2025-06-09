using AxoneMFGRJ.Masters;
using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmOrderEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_OrderMasterDetail ObjRough = new BOTRN_OrderMasterDetail();
        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabDetail = new DataTable();
        
        #region Property Settings

        public FrmOrderEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
       
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            this.Show();

            DataTable DTabParty = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PARAORDERPARTY);
            ChkCmbParty.Properties.DataSource = DTabParty;
            ChkCmbParty.Properties.ValueMember = "PARTY_ID";
            ChkCmbParty.Properties.DisplayMember = "PARTYNAME";

            DTPFromOrderDate.Value = DateTime.Now.AddMonths(-1);
            DTPToOrderDate.Value = DateTime.Now;
            FetchOrderData(-1);
        }

        public void ShowForm(Int64 pIntOrderID)
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;


            this.Show();

            DataTable DTabParty = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PARAORDERPARTY);
            ChkCmbParty.Properties.DataSource = DTabParty;
            ChkCmbParty.Properties.ValueMember = "PARTY_ID";
            ChkCmbParty.Properties.DisplayMember = "PARTYNAME";


            DTPFromOrderDate.Value = DateTime.Now.AddMonths(-1);
            DTPToOrderDate.Value = DateTime.Now;
            FetchOrderData(pIntOrderID);
            BtnSave.Enabled = false;
            BtnDelete.Enabled = false;
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjRough);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            if (txtParty.Text.Trim().Length == 0)
            {
                Global.Message("Party Name Is Required");
                txtParty.Focus();
                return false;
            }
            if (Val.Val(txtTotalPcs.Text) == 0)
            {
                Global.Message("Order Pcs Is Required");
                txtParty.Focus();
                return false;
            }

            return true;
        }

        #endregion

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            DTabDetail.Rows.Clear();

            txtOrderID.Text = string.Empty;
            txtOrderID.Tag = string.Empty;

            txtSystemOrderNo.Text = string.Empty;
            txtSystemOrderNo.Tag = string.Empty;
            txtManualOrderNo.Text = string.Empty;
            
            txtParty.Tag = string.Empty;
            txtParty.Text = string.Empty;
            txtTotalPcs.Text = string.Empty;
            txtPaymentTermsDays.Text = string.Empty;
            DTPDueDate.Checked = false;
            txtYourRemark.Text = string.Empty;
            txtPartyRemark.Text = string.Empty;
            DTPReceiveDate.Value = DateTime.Now;
            txtPaymentTermsDays.Text = string.Empty;
           

            CmbRoughType.SelectedIndex = 0;
            CmbOrderPriority.SelectedIndex = 0;
            
            DTPReceiveDate.Focus();
            DTPReceiveDate_Validated(null, null);
            CalculateSummary();
            DataRow DRow = DTabDetail.NewRow();
            DRow["SRNO"] = GrdDet.RowCount + 1;
            DTabDetail.Rows.Add(DRow);
            lblMode.Text = "Add Mode";

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (ValSave() == false)
                {
                    return;
                }
                if (Global.Confirm("Are you Sure Your Want To Save This Entry ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                if (lblMode.Text == "Add Mode")
                {
                    while (ObjRough.ISExistsSystemOrderNo(Val.ToString(txtSystemOrderNo.Text), Val.ToString(CmbRoughType.Text)) == true)
                    {
                        if (Global.Confirm("[" + txtSystemOrderNo.Text + "] No Is Already Generated\n\nDo You Want To Generate New ?") == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        } 
                        DTPReceiveDate_Validated(null, null);
                    }
                }

                DTabDetail.AcceptChanges();

                TrnOrderEntryProperty Property = new TrnOrderEntryProperty();
                Property.ORDER_ID = Val.ToInt64(txtOrderID.Text);
                Property.ORDERDATE = Val.SqlDate(DTPReceiveDate.Value.ToShortDateString());
                Property.ORDERYEAR = Val.ToInt32(DTPReceiveDate.Value.Year);
                Property.ORDERMONTH = Val.ToInt32(DTPReceiveDate.Value.Month);
                Property.ORDERNO = Val.ToInt32(txtSystemOrderNo.Tag);
                Property.PARTY_ID = Val.ToInt64(txtParty.Tag);
                Property.PARTYNAME= txtParty.Text;
                Property.ORDERDUE = Val.ToInt(txtPaymentTermsDays.Text);
                Property.ORDERDUEDATE = Val.SqlDate(DTPDueDate.Value.ToShortDateString());
                
                Property.SYSTEMORDERNO = txtSystemOrderNo.Text;
                Property.MANUALORDERNO = txtManualOrderNo.Text;
                
                Property.ROUGHTYPE = Val.ToString(CmbRoughType.SelectedItem);
                Property.ORDERPRIORITY = Val.ToString(CmbOrderPriority.SelectedItem);
                Property.ORDERSEQNO = Val.ToInt(txtOrderSequence.Text);

                Property.PARTYREMARK = txtPartyRemark.Text;
                Property.YOURREMARK = txtYourRemark.Text;
                
                DTabDetail.TableName = "Table1";
                string RoughDetailXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabDetail.WriteXml(sw);
                    RoughDetailXML = sw.ToString();
                }

                Property.XMLDETAIL = RoughDetailXML;

                Property = ObjRough.OrderSave(Property, RoughDetailXML);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    txtOrderID.Text = Property.ReturnValue;
                    BtnAdd_Click(null, null);
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    BtnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnBack_Click(null, null);
            //}
        }

        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataRow DR = GrdDet.GetFocusedDataRow();
            //    FetchValue(DR);
            //    DR = null;
            //}
        }


        public void FetchValue(DataRow DR)
        {
            lblMode.Text = "Edit Mode";
            txtOrderID.Text = Val.ToString(DR["ORDER_ID"]);

            DTPReceiveDate.Value = DateTime.Parse(Val.ToString(DR["ORDERDATE"]));

            txtSystemOrderNo.Text = Val.ToString(DR["SYSTEMORDERNO"]);
            txtSystemOrderNo.Tag = Val.ToString(DR["ORDERNO"]);

            txtManualOrderNo.Text = Val.ToString(DR["MANUALORDERNO"]);
            txtParty.Text = Val.ToString(DR["PARTYNAME"]);
            txtParty.Tag = Val.ToInt64(DR["PARTY_ID"]);
            txtPaymentTermsDays.Text = Val.ToString(DR["ORDERDUE"]);
            DTPDueDate.Checked = false;
            if (Val.IsDate(Val.ToString(DR["ORDERDUEDATE"])))
            {
                DTPDueDate.Value = DateTime.Parse(Val.ToString(DR["ORDERDUEDATE"]));
                DTPDueDate.Checked = true;
            }

            CmbOrderPriority.SelectedItem = Val.ToString(DR["ORDERPRIORITY"]);
            CmbRoughType.SelectedItem = Val.ToString(DR["ROUGHTYPE"]);
            txtOrderSequence.Text = Val.ToString(DR["ORDERSEQNO"]);
            txtPartyRemark.Text = Val.ToString(DR["PARTYREMARK"]);
            txtYourRemark.Text = Val.ToString(DR["YOURREMARK"]);

            lblStatus.Text = Val.ToString(DR["ORDERSTATUS"]);

            CalculateSummary();
            DTPReceiveDate.Focus();

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("List", GrdDet);
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        TrnOrderEntryProperty Property = new TrnOrderEntryProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.ORDERDETAIL_ID = Val.ToInt64(Drow["ORDERDETAIL_ID"]);
                        Property = ObjRough.OrderDelete("ORDERDETAILDELETE", Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message(Property.ReturnMessageDesc);
                            DTabDetail.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTabDetail.AcceptChanges();
                        }
                        else
                        {
                            Global.MessageError(Property.ReturnMessageDesc);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void txtSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PARTYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PARAORDERPARTY);

                    FrmSearch.mColumnsToHide = "PARTY_ID";
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "PARTYNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtParty.Text = Val.ToString(FrmSearch.mDRow["PARTYNAME"]);
                        txtParty.Tag = Val.ToString(FrmSearch.mDRow["PARTY_ID"]);
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

        private void repTxtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "SHAPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
                    }
                    
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtFromColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("FROMCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtToColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("TOCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtFromClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("FROMCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
                switch (e.Column.FieldName.ToUpper())
                {
                    case "ORDERPCS":
                        CalculateSummary();
                        break;
                }
                GrdDet.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
       
        public void CalculateSummary()
        {
            int IntTotalOrderPcs = 0;
            int IntTotalBookedPcs = 0;
            int IntTotalPendingPcs = 0;
            
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntTotalOrderPcs = IntTotalOrderPcs + Val.ToInt(DRow["ORDERPCS"]);
                IntTotalBookedPcs = IntTotalBookedPcs + Val.ToInt(DRow["BOOKEDPCS"]);
                IntTotalPendingPcs = IntTotalPendingPcs + Val.ToInt(DRow["PENDINGPCS"]);
            }

            txtTotalPcs.Text = IntTotalOrderPcs.ToString();
            txtBookedPcs.Text = IntTotalBookedPcs.ToString();
            txtPendingPcs.Text = IntTotalPendingPcs.ToString();

            txtPendingPer.Text = IntTotalOrderPcs == 0 ? "0" : Math.Round((Val.Val(IntTotalPendingPcs) / Val.Val(IntTotalOrderPcs)) * 100, 2).ToString();
            txtBookedPer.Text = IntTotalBookedPcs == 0 ? "0" : Math.Round((Val.Val(IntTotalBookedPcs) / Val.Val(IntTotalOrderPcs)) * 100, 2).ToString();
            
        }

        private void ChkDTPReceiveDate_ValueChanged(object sender, EventArgs e)
        {
            DTPDueDate.Checked = true;
            txtDueDays_Validated(null, null);
        }

        private void txtDueDays_Validated(object sender, EventArgs e)
        {
            if (Val.ISDate(DTPReceiveDate.Text) == true)
            {
                DTPDueDate.Value = DateTime.Parse(DTPReceiveDate.Value.ToShortDateString()).AddDays(Val.ToInt(txtPaymentTermsDays.Text));
            }
        }

        private void DTPDueDate_Validated(object sender, EventArgs e)
        {
            txtPaymentTermsDays.Text = Val.DateDiff(Microsoft.VisualBasic.DateInterval.Day, DTPReceiveDate.Value.ToShortDateString(), DTPDueDate.Value.ToShortDateString()).ToString();
        }

        private void txtExcRate_Validated(object sender, EventArgs e)
        {
            CalculateSummary();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFromOrderDate = null;
                string StrToOrderDate = null;
                string StrFromDueDate = null;
                string StrToDueDate = null;
                string StrPartyID = "";
                string StrStatus = "";               

                if (DTPFromOrderDate.Checked == true && DTPToOrderDate.Checked == true)
                {
                    StrFromOrderDate = Val.SqlDate(DTPFromOrderDate.Value.ToShortDateString());
                    StrToOrderDate = Val.SqlDate(DTPToOrderDate.Value.ToShortDateString());
                }
                if (DTPFromOrderDate.Checked == true && DTPToOrderDate.Checked == true)
                {
                    StrFromDueDate = Val.SqlDate(DTPFromDueDate.Value.ToShortDateString());
                    StrToDueDate = Val.SqlDate(DTPToDueDate.Value.ToShortDateString());
                }
                StrPartyID = Val.Trim(ChkCmbParty.Properties.GetCheckedItems().ToString());
                StrStatus = Val.Trim(ChkCmbStatus.Properties.GetCheckedItems().ToString());

                this.Cursor = Cursors.WaitCursor;
                GrdDetList.BeginUpdate();
                DataTable DTabData = ObjRough.GetDataForOrder(StrFromOrderDate, StrToOrderDate, StrFromDueDate, StrToDueDate, StrPartyID, StrStatus);
                MainGridList.DataSource = DTabData;
                GrdDetList.RefreshData();
                GrdDetList.EndUpdate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }


        public void FetchOrderData(Int64 InvoiceID)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataSet DS = ObjRough.GetOrderDetail(InvoiceID);

                if (DS.Tables.Count != 0)
                {
                    DTabDetail = DS.Tables[1];

                    GrdDet.BeginUpdate();
                    MainGrid.DataSource = DTabDetail;
                    DTPReceiveDate.Focus();
                    GrdDet.RefreshData();

                    DataRow DRow = DTabDetail.NewRow();
                    DRow["SRNO"] = GrdDet.RowCount + 1;
                    DTabDetail.Rows.Add(DRow);
                    GrdDet.BestFitColumns();
                    GrdDet.EndUpdate();
                }
                if (DS.Tables[0].Rows.Count != 0)
                {
                    DataRow DrDetail = DS.Tables[0].Rows[0];
                    FetchValue(DrDetail);
                }
              
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
         
        }

        private void GrdDetList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Clicks == 2)
            {
                FetchOrderData(Val.ToInt64(GrdDetList.GetFocusedRowCellValue("ORDER_ID")));
                xtraTabControl1.SelectedTabPageIndex = 0;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtOrderID.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please First Select Record That You Want To Delete");
                    return;
                }

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;
                TrnOrderEntryProperty Property = new TrnOrderEntryProperty();
                Property.ORDER_ID = Val.ToInt64(txtOrderID.Text);
                Property = ObjRough.OrderDelete("ORDERDELETE",Property);
               
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    BtnAdd_Click(null, null);
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    txtPartyRemark.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void DTPReceiveDate_Validated(object sender, EventArgs e)
        {
            if (lblMode.Text == "Add Mode")
            {
                DataRow DRow = ObjRough.FindNewOrderNo(Val.SqlDate(DTPReceiveDate.Value.ToShortDateString()), Val.ToString(CmbRoughType.SelectedItem));
                if (DRow == null)
                {
                    return;
                }
                else
                {
                    txtSystemOrderNo.Tag = Val.ToString(DRow["InvoiceNo"]);
                    txtSystemOrderNo.Text = Val.ToString(DRow["SystemInvoiceNo"]);

                    txtManualOrderNo.Tag = Val.ToString(DRow["InvoiceNo"]);
                    txtManualOrderNo.Text = Val.ToString(DRow["SystemInvoiceNo"]);
                }
           }
           
            //if (lblMode.Text == "Add Mode")
            //{
            //    txtManualInvoiceNo.Text = txtSystemInvoiceNo.Text;
            //}
        }

        private void txtTermsPer_TextChanged(object sender, EventArgs e)
        {
            CalculateSummary();
        }

        private void repTxtToClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("TOCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


        private void txtRepComment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COMMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "COMMENTNAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PARAORDERCOMMENT);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("COMMENT", Val.ToString(FrmSearch.mDRow["COMMENTNAME"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Val.Val(GrdDet.GetFocusedRowCellValue("ORDERPCS")) != 0 &&
                    GrdDet.IsLastRow == true
                    )
                {
                    DataRow DRowPre = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                    DataRow DRow = DTabDetail.NewRow();
                    DRow["SRNO"] = GrdDet.RowCount + 1;
                   
                    if (GrdDet.RowCount > 0)
                    {
                        DRow["SHAPENAME"] = DRowPre["SHAPENAME"];
                        DRow["FROMCOLORNAME"] = DRowPre["FROMCOLORNAME"];
                        DRow["TOCOLORNAME"] = DRowPre["TOCOLORNAME"];
                        DRow["FROMCLARITYNAME"] = DRowPre["FROMCLARITYNAME"];
                        DRow["TOCLARITYNAME"] = DRowPre["TOCLARITYNAME"];
                    }
                    DTabDetail.Rows.Add(DRow);
                    GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    GrdDet.FocusedColumn = GrdDet.Columns["SHAPENAME"];
                }
                else
                {
                    BtnSave.Focus();
                }
            }
            
        }


        private void CmbPaymentType_Validated(object sender, EventArgs e)
        {
            DataRow DRow = DTabDetail.NewRow();
            DRow["SRNO"] = GrdDet.RowCount + 1;
            DTabDetail.Rows.Add(DRow);

            GrdDet.Focus();
            GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
            GrdDet.FocusedColumn = GrdDet.Columns["LOTNO"];
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtOrderID.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please First Select Record That You Want To Delete");
                    return;
                }

                if (Global.Confirm("Are Your Sure To Cancel The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;
                TrnOrderEntryProperty Property = new TrnOrderEntryProperty();
                Property.ORDER_ID = Val.ToInt64(txtOrderID.Text);
                Property = ObjRough.OrderDelete("ORDERCANCEL", Property);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    BtnAdd_Click(null, null);
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    txtPartyRemark.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtYourRemark_Validated(object sender, EventArgs e)
        {
            DataRow DRow = DTabDetail.NewRow();
            DRow["SRNO"] = GrdDet.RowCount + 1;
            MainGrid.DataSource = DTabDetail;
            
            GrdDet.FocusedRowHandle = 0;
            GrdDet.FocusedColumn = GrdDet.Columns["SHAPENAME"];
        }

    }
}
