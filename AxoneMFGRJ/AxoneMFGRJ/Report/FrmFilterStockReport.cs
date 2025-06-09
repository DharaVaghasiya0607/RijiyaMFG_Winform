using BusLib;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraTreeList;
using System;
using System.Data;
using System.Windows.Forms;
using Config = BusLib.Configuration.BOConfiguration;

namespace AxoneMFGRJ.Report
{
    public partial class FrmFilterStockReport : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        BOMST_Report ObjReport = new BOMST_Report();
        DataTable mDTabFieldDetail = new DataTable();
        DataTable mDTabRowArea = new DataTable();
        DataTable mDTabColumnArea = new DataTable();
        DataTable mDTabDataArea = new DataTable();
        int mIntFilterHeight = 0;
        DataRow mDrow = null;
       string mStrReportGroupNew = string.Empty;

        #region Property

       public FrmFilterStockReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            mStrReportGroupNew = "Stock Reports";

            txtFromDate.EditValue = DateTime.Now;
            txtToDate.EditValue = DateTime.Now;

            this.ActiveControl = txtFromDate;

            AttachFormDefaultEvent();
            Val.FormGeneralSetting(this);
            lblTitle.Text = "Stock Reports";

            //txtToDepartment.Text = Config.gEmployeeProperty.DEPARTMENTNAME.ToString();
            //txtToDepartment.Tag = Config.gEmployeeProperty.DEPARTMENT_ID.ToString();

            //txtToEmployee.Text = Config.gEmployeeProperty.LEDGERNAME.ToString();
            //txtToEmployee.Tag = Config.gEmployeeProperty.LEDGER_ID.ToString();

            this.Show();
        }

        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.FormKeyPress = true;

            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);

        }
        
        private void FrmSearch_Load(object sender, EventArgs e)
        {
            DataTable DTab = new BOMST_Report().GetDataSummaryForReportNew(mStrReportGroupNew);
            treLstAccGroupMaster.DataSource = DTab;
            treLstAccGroupMaster.ParentFieldName = "REPORT_ID";
            treLstAccGroupMaster.KeyFieldName = "REPORTGROUP_ID";
            treLstAccGroupMaster.CollapseAll();
        }
        
        private void FrmSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

#endregion

        #region Control Event
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (txtKapan.Text.Length == 0) txtKapan.Tag = string.Empty;
                if (txtFromProcess.Text.Length == 0) txtFromProcess.Tag = string.Empty;
                if (txtToProcess.Text.Length == 0) txtToProcess.Tag = string.Empty;
                if (txtNextProcess.Text.Length == 0) txtNextProcess.Tag = string.Empty;
                if (txtFromDepartment.Text.Length == 0) txtFromDepartment.Tag = string.Empty;
                if (txtToDepartment.Text.Length == 0) txtToDepartment.Tag = string.Empty;
                if (txtFromFactoryHead.Text.Length == 0) txtFromFactoryHead.Tag = string.Empty;
                if (txtToFactoryHead.Text.Length == 0) txtToFactoryHead.Tag = string.Empty;
                if (txtFromManager.Text.Length == 0) txtFromManager.Tag = string.Empty;
                if (txtToManager.Text.Length == 0) txtToManager.Tag = string.Empty;
                if (txtLotNo.Text.Length == 0) txtLotNo.Tag = string.Empty;
                if (txtKapanManager.Text.Length == 0) txtKapanManager.Tag = string.Empty;
                if (txtBarcode.Text.Length == 0) txtBarcode.Tag = string.Empty;
                if (txtKapanPktTag.Text.Length == 0) txtKapanPktTag.Tag = string.Empty;
                if (txtFromEmployee.Text.Length == 0) txtFromEmployee.Tag = string.Empty;
                if (txtToEmployee.Text.Length == 0) txtToEmployee.Tag = string.Empty;
                
                MST_ReportProperty Property = new MST_ReportProperty();

                if (mDrow == null)
                {
                    Global.Message("Select Atleast One Report For Generate");
                    return;
                }

                Property.REPORT_ID = Val.ToInt(mDrow["REPORT_ID"]);
                Property.REPORTTYPE = Val.ToString(mDrow["REPORTTYPE"]);
                Property.REMARK = Val.ToString(mDrow["REMARK"]);

                if (RbtFullStock.Checked == true)
                {
                    Property.STOCKTYPE = RbtFullStock.Tag.ToString();
                }
                else if (RbtDeptStock.Checked == true)
                {
                    Property.STOCKTYPE = RbtDeptStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    Property.STOCKTYPE = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    Property.STOCKTYPE = RbtOtherStock.Tag.ToString();
                }

                Property.KAPAN_ID = Val.ToString(txtKapan.Tag);
                Property.KAPANNAME = Val.ToString(txtKapan.Text);
                Property.FROMPROCESS_ID = Val.ToString(txtFromProcess.Tag);
                Property.TOPROCESS_ID = Val.ToString(txtToProcess.Tag);
                Property.NEXTPROCESS_ID = Val.ToString(txtNextProcess.Tag);
                Property.FROMDEPARTMENT_ID = Val.ToString(txtFromDepartment.Tag);
                Property.TODEPARTMENT_ID = Val.ToString(txtToDepartment.Tag);
                
                Property.FROMEMPLOYEE_ID = Val.ToString(txtFromEmployee.Tag);
                Property.TOEMPLOYEE_ID = Val.ToString(txtToEmployee.Tag);

                Property.FROMMANAGER_ID = Val.ToString(txtFromManager.Tag);
                Property.TOMANAGER_ID = Val.ToString(txtToManager.Tag);
                Property.FROMFACTORYHEAD_ID = Val.ToString(txtFromFactoryHead.Tag);
                Property.TOFACTORYHEAD_ID = Val.ToString(txtToFactoryHead.Tag);
                Property.LOT_ID = Val.ToString(txtLotNo.Tag);
                Property.KAPANMANAGER_ID = Val.ToString(txtKapanManager.Tag);
                Property.BARCODE = Val.ToString(txtBarcode.Text);
                Property.JANGEDNO = Val.ToString(txtSlipJangedNo.Text);
                Property.PACKETNO = Val.ToString(txtKapanPktTag.Text);

                Property.PRICEDATE = null;
                if (Val.IsDate(txtPriceDate.Text) )
                {
                    Property.PRICEDATE = Val.SqlDate(txtPriceDate.Text);                    
                }

                if (Val.IsDate(txtFromDate.Text) && Val.IsDate(txtToDate.Text))
                {
                    Property.FROMDATE = Val.SqlDate(txtFromDate.Text);
                    Property.TODATE = Val.SqlDate(txtToDate.Text);
                }
                else
                {
                    Property.FROMDATE = null;
                    Property.TODATE = null;
                }
                if (Val.ToString(mDrow["REPORTVIEW"]) == "GridView")
                {
                    Property.GROUPBY = GroupByBox.GetTagValue;
                }
                if (Val.ToString(mDrow["REPORTVIEW"]) == "PivotView")
                {
                    Property.GROUPBY = RowByBox.GetTagValue + "," + ColumnByBox.GetTagValue;
                }
                Property.SPNAME = Val.ToString(mDrow["SPNAME"]);
           
                if (RbtSummary.Checked == true)
                {
                    Property.REPORTTYPE = "S";
                }
                else if (RbtDetail.Checked == true)
                {
                    Property.REPORTTYPE = "D";
                }

                DataSet DS = ObjReport.GenerateMaintainanceReport(Property);

                if (Val.ToString(mDrow["REPORTVIEW"]) == "GridView")
                {
                    FrmGridReportViewerWithBand FrmGridReportViewerWithBand = new FrmGridReportViewerWithBand();
                    FrmGridReportViewerWithBand.MdiParent = Global.gMainRef;
                    FrmGridReportViewerWithBand.ShowForm(DS,
                        mDrow,
                        mDTabFieldDetail,
                        GroupByBox.GetTagValue,
                        GroupByBox.GetTextValue,
                        GetFilterString(),
                        ChkNoGrouping.Checked,
                        GroupByBox.GetTagValue,
                        GroupByBox.GetTextValue,
                        Property,
                        txtFromDate.Text,
                        txtToDate.Text,
                        "",
                        mIntFilterHeight
                        );
                }

                else if (Val.ToString(mDrow["REPORTVIEW"]) == "PivotView")
                {
                    FrmPivotReportViewer FrmPReportViewer = new FrmPivotReportViewer();
                    FrmPReportViewer.MdiParent = Global.gMainRef;
                    FrmPReportViewer.ShowForm(DS,
                                        mDrow,
                                        mDTabFieldDetail,
                                        GroupByBox.GetTagValue,
                                        GroupByBox.GetTextValue,
                                        GetFilterString(),
                                        ChkNoGrouping.Checked,
                                        GroupByBox.GetTagValue,
                                        GroupByBox.GetTextValue,
                                        Property,
                                        txtFromDate.Text,
                                        txtToDate.Text,
                                        "",
                                        RowByBox.GetTagValue,
                                        ColumnByBox.GetTagValue,
                                        DataByBox.GetTagValue,
                                        mIntFilterHeight
                        );
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
                return;
            }
       }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtKapan.Text = string.Empty;
            txtKapan.Tag = string.Empty;

            txtFromProcess.Text = string.Empty;
            txtFromProcess.Tag = string.Empty;

            txtToProcess.Text = string.Empty;
            txtToProcess.Tag = string.Empty;

            txtNextProcess.Text = string.Empty;
            txtNextProcess.Tag = string.Empty;

            txtFromDepartment.Text = string.Empty;
            txtFromDepartment.Tag = string.Empty;

            txtToDepartment.Text = string.Empty;
            txtToDepartment.Tag = string.Empty;

            txtFromFactoryHead.Text = string.Empty;
            txtFromFactoryHead.Tag = string.Empty;

            txtToFactoryHead.Text = string.Empty;
            txtToFactoryHead.Tag = string.Empty;

            txtFromManager.Text = string.Empty;
            txtFromManager.Tag = string.Empty;

            txtToManager.Text = string.Empty;
            txtToManager.Tag = string.Empty;

            txtLotNo.Text = string.Empty;
            txtLotNo.Tag = string.Empty;

            txtKapanManager.Text = string.Empty;
            txtKapanManager.Tag = string.Empty;

            txtBarcode.Text = string.Empty;
            txtBarcode.Tag = string.Empty;

            txtKapanPktTag.Text = string.Empty;
            txtKapanPktTag.Tag = string.Empty;
            
            txtFromEmployee.Text = string.Empty;
            txtFromEmployee.Tag = string.Empty;
            
            txtToEmployee.Text = string.Empty;
            txtToEmployee.Tag = string.Empty;

        }

        
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

          private void treLstAccGroupMaster_Click(object sender, EventArgs e)
        {
            try
            {
                TreeList tree = sender as TreeList;
                TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
                if (hi.Node != null)
                {
                    treLstAccGroupMaster.GetDataRecordByNode(hi.Node);

                    DataRowView DR = (DataRowView)treLstAccGroupMaster.GetDataRecordByNode(hi.Node);

                    mDrow = (DataRow)DR.Row;

                    DataSet DS = new BOMST_Report().GetData(Val.ToInt(mDrow["OLDREPORT_ID"]));

                    mDTabFieldDetail = DS.Tables[0].Copy();

                    lblTitle.Text = Val.ToString(mDrow["REPORTNAME"]);

                    mDTabFieldDetail.DefaultView.RowFilter = "ISGROUP = 1";
                    mDTabFieldDetail.DefaultView.Sort = "SRNO";
                    DataTable DTabGroupBy = mDTabFieldDetail.DefaultView.ToTable();
                    GroupByBox.DTab = DTabGroupBy;
                    GroupByBox.SetDefauleGroupBy(Val.ToString(mDrow["GROUPBYTAG"]), Val.ToString(mDrow["GROUPBYCAPTION"]));

                    mDTabColumnArea = mDTabFieldDetail.Copy();
                    mDTabRowArea = mDTabFieldDetail.Copy();

                    ColumnByBox.DTab = mDTabColumnArea;
                    ColumnByBox.SetDefauleGroupBy(Val.ToString(mDrow["COLUMNAREA"]), Val.ToString(mDrow["COLUMNAREA"]));

                    mDTabFieldDetail.DefaultView.RowFilter = "ISGROUP = 0";
                    mDTabFieldDetail.DefaultView.Sort = "SRNO";
                    mDTabDataArea = mDTabFieldDetail.DefaultView.ToTable().Copy();

                    RowByBox.DTab = mDTabRowArea;
                    RowByBox.SetDefauleGroupBy(Val.ToString(mDrow["ROWAREA"]), Val.ToString(mDrow["ROWAREA"]));

                    DataByBox.DTab = mDTabDataArea;
                    DataByBox.SetDefauleGroupBy(Val.ToString(mDrow["DATAAREA"]), Val.ToString(mDrow["DATAAREA"]));

                    PanelGrid.Visible = false;
                    PanelPivot.Visible = false;

                    if (Val.ToString(mDrow["REPORTVIEW"]) == "GridView")
                    {
                        PanelGrid.Visible = true;
                    }
                    else if (Val.ToString(mDrow["REPORTVIEW"]) == "PivotView")
                    {
                        PanelPivot.Visible = true;
                    }
                    DR = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

#endregion

        #region Filter

        public string GetFilterString()
        {
            mIntFilterHeight = 0;
            string Str = string.Empty;

            if (RbtFullStock.Checked == true)
            {
                Str = Str + "Stock Type : " + RbtFullStock.Text.ToString() + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            else if (RbtDeptStock.Checked == true)
            {
                Str = Str + "Stock Type : " + RbtDeptStock.Text.ToString() + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            else if (RbtMYStock.Checked == true)
            {
                Str = Str + "Stock Type : " + RbtMYStock.Text.ToString() + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            else if (RbtOtherStock.Checked == true)
            {
                Str = Str + "Stock Type : " + RbtOtherStock.Text.ToString() + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtKapan.Text.Length != 0)
            {
                Str = Str + "Kapan : " + txtKapan.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtFromProcess.Text.Length != 0)
            {
                Str = Str + "From Process : " + txtFromProcess.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtToProcess.Text.Length != 0)
            {
                Str = Str + "To Process : " + txtToProcess.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtNextProcess.Text.Length != 0)
            {
                Str = Str + "Sub Process : " + txtNextProcess.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtFromDepartment.Text.Length != 0)
            {
                Str = Str + "From Department : " + txtFromDepartment.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtToDepartment.Text.Length != 0)
            {
                Str = Str + "To Department : " + txtToDepartment.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            
            if (txtFromEmployee.Text.Length != 0)
            {
                Str = Str + "From Employee : " + txtFromEmployee.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtToEmployee.Text.Length != 0)
            {
                Str = Str + "To Employee : " + txtToEmployee.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtFromFactoryHead.Text.Length != 0)
            {
                Str = Str + "From FactoryHead : " + txtFromFactoryHead.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtToFactoryHead.Text.Length != 0)
            {
                Str = Str + "To FactoryHead : " + txtToFactoryHead.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtFromManager.Text.Length != 0)
            {
                Str = Str + "From Manager : " + txtFromManager.Text + "\n"; 
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtToManager.Text.Length != 0)
            {
                Str = Str + "To Manager : " + txtToManager.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtLotNo.Text.Length != 0)
            {
                Str = Str + "Lot No : " + txtLotNo.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtKapanManager.Text.Length != 0)
            {
                Str = Str + "Kapan Manager : " + txtKapanManager.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtBarcode.Text.Length != 0)
            {
                Str = Str + "Barcode : " + txtBarcode.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtKapanPktTag.Text.Length != 0)
            {
                Str = Str + "PacketNo : " + txtKapanPktTag.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtSlipJangedNo.Text.Length != 0)
            {
                Str = Str + "Slip JangedNo : " + txtSlipJangedNo.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (Val.IsDate(txtFromDate.Text) == true && Val.IsDate(txtToDate.Text) == true)
            {
                Str = Str + "Transaction Date : " + txtFromDate.Text + " To " + txtToDate.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (Val.IsDate(txtPriceDate.Text) == true )
            {
                Str = Str + "Price Date : " + txtPriceDate.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            return Str;
        }
        #endregion

        #region KeyPress

        private void txtNextProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Global.OnKeyPressEveToPopup(e))
            {
                this.Cursor = Cursors.WaitCursor;
                FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PROCESS);
                FrmSearch.mStrSearchText = e.KeyChar.ToString();
                FrmSearch.mStrSearchField = "PROCESSNAME";
                FrmSearch.mStrColumnsToHide = "PROCESS_ID";
                FrmSearch.ValueMemeter = "PROCESS_ID";
                FrmSearch.DisplayMemeter = "PROCESSNAME";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                e.Handled = true;
                if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                {
                    txtNextProcess.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                    txtNextProcess.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                }
                FrmSearch.Hide();
                FrmSearch.Dispose();
                FrmSearch = null;
            }
        }

        private void txtFromFactoryHead_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "EMPLOYEENAME";
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtFromFactoryHead.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtFromFactoryHead.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtToFactoryHead_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "EMPLOYEENAME";
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtToFactoryHead.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtToFactoryHead.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtFromManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "EMPLOYEENAME";
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtFromManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtFromManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtToManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "EMPLOYEENAME";
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtToManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtToManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
             }
        }

        private void txtLotNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.TRN_LOT);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "LOTNO";
                    FrmSearch.mStrColumnsToHide = "LOT_ID";
                    FrmSearch.ValueMemeter = "LOT_ID";
                    FrmSearch.DisplayMemeter = "LOTNO";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtLotNo.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtLotNo.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
           }
        }


        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOTRN_SinglePacketCreate().FindKapan();
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "KAPANNAME";
                    FrmSearch.mStrColumnsToHide = "KAPAN_ID";
                    FrmSearch.ValueMemeter = "KAPAN_ID";
                    FrmSearch.DisplayMemeter = "KAPANNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtKapan.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtFromProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrColumnsToHide = "PROCESS_ID";
                    FrmSearch.mStrSearchField = "PROCESSNAME";
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.ValueMemeter = "PROCESS_ID";
                    FrmSearch.DisplayMemeter = "PROCESSNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtFromProcess.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtFromProcess.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtToProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrColumnsToHide = "PROCESS_ID";
                    FrmSearch.mStrSearchField = "PROCESSNAME";
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.ValueMemeter = "PROCESS_ID";
                    FrmSearch.DisplayMemeter = "PROCESSNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtToProcess.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtToProcess.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtFromDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_DEPARTMENT);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrColumnsToHide = "DEPARTMENT_ID";
                    FrmSearch.mStrSearchField = "DEPARTMENTNAME";
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.ValueMemeter = "DEPARTMENT_ID";
                    FrmSearch.DisplayMemeter = "DEPARTMENTNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtFromDepartment.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtFromDepartment.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtToDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_DEPARTMENT);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrColumnsToHide = "DEPARTMENT_ID";
                    FrmSearch.mStrSearchField = "DEPARTMENTNAME";
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.ValueMemeter = "DEPARTMENT_ID";
                    FrmSearch.DisplayMemeter = "DEPARTMENTNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtToDepartment.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtToDepartment.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
           }
        }


        private void txtKapanManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "EMPLOYEENAME";
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtKapanManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtKapanManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        
        private void txtFromEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "EMPLOYEENAME";
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtFromEmployee.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtFromEmployee.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtToEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "EMPLOYEENAME";
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtToEmployee.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtToEmployee.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

#endregion

        private void txtPriceDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PRICEDATE,REMARK";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRICEHEAD);
                    FrmSearch.mColumnsToHide = "PRICE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPriceDate.Tag = Val.ToString(FrmSearch.mDRow["PRICE_ID"]);
                        txtPriceDate.Text = Val.ToString(FrmSearch.mDRow["PRICEDATE"]);
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
    }

}