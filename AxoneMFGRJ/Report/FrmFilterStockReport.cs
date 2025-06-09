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
        MST_ReportProperty mProperty = new MST_ReportProperty();
        System.Diagnostics.Stopwatch watch = null;

        BOMST_Report ObjReport = new BOMST_Report();
        DataTable mDTabFieldDetail = new DataTable();
        DataTable mDTabRowArea = new DataTable();
        DataTable mDTabColumnArea = new DataTable();
        DataTable mDTabDataArea = new DataTable();
        int mIntFilterHeight = 0;
        DataRow mDrow = null;
        DataSet mDS = new DataSet();
       string mStrReportGroupNew = string.Empty;
        string pStrShiftType = "";

        #region Property

       public FrmFilterStockReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            mStrReportGroupNew = "Stock Reports";

            AttachFormDefaultEvent();
            Val.FormGeneralSetting(this);
            lblTitle.Text = "Stock Reports";

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
                if (txtPacketNoFrom.Text.Length == 0) txtPacketNoFrom.Tag = string.Empty;
                if (txtFromEmployee.Text.Length == 0) txtFromEmployee.Tag = string.Empty;
                if (txtToEmployee.Text.Length == 0) txtToEmployee.Tag = string.Empty;
                if (txtTable.Text.Length == 0) txtTable.Tag = string.Empty;
                if (txtPacketCategory.Text.Length == 0) txtPacketCategory.Tag = string.Empty;
                if (txtPacketGroup.Text.Length == 0) txtPacketGroup.Tag = string.Empty;
                if (txtDiamondType.Text.Length == 0) txtDiamondType.Tag = string.Empty;
               

                if (mDrow == null)
                {
                    Global.Message("Select Atleast One Report For Generate");
                    return;
                }

                mProperty = new MST_ReportProperty();
                mProperty.REPORT_ID = Val.ToInt(mDrow["REPORT_ID"]);
                mProperty.REPORTTYPE = Val.ToString(mDrow["REPORTTYPE"]);
                mProperty.REMARK = Val.ToString(mDrow["REMARK"]);

                if (RbtFullStock.Checked == true)
                {
                    mProperty.STOCKTYPE = RbtFullStock.Tag.ToString();
                }
                else if (RbtDeptStock.Checked == true)
                {
                    mProperty.STOCKTYPE = RbtDeptStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    mProperty.STOCKTYPE = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    mProperty.STOCKTYPE = RbtOtherStock.Tag.ToString();
                }

                if (RbtAllShift.Checked == true)
                    mProperty.SHIFTTYPE = "";
                else if (RbtDayShift.Checked == true)
                    mProperty.SHIFTTYPE = "D";
                else
                    mProperty.SHIFTTYPE = "N";
               

                if (RbtKapanStatusAll.Checked == true)
                    mProperty.KapanStatus = "";
                if (RbtKapanStatusClvRunning.Checked == true)
                    mProperty.KapanStatus = "Clv Running";
                if (RbtKapanStatusMfgRunning.Checked == true)
                    mProperty.KapanStatus = "Mfg Running";
                if (RbtKapanStatusPolishRunning.Checked == true)
                    mProperty.KapanStatus = "Polish Running";
                if (RbtKapanStatusClvComplete.Checked == true)
                    mProperty.KapanStatus = "Clv Complete";
                if (RbtKapanStatusMfgComplete.Checked == true)
                    mProperty.KapanStatus = "Mfg Complete";
                if (RbtKapanStatusPolishComplete.Checked == true)
                    mProperty.KapanStatus = "Polish Complete";


                mProperty.KAPAN_ID = Val.ToString(txtKapan.Tag);
                mProperty.KAPANNAME = Val.ToString(txtKapan.Text);
                mProperty.FROMPROCESS_ID = Val.ToString(txtFromProcess.Tag);
                mProperty.TOPROCESS_ID = Val.ToString(txtToProcess.Tag);
                mProperty.NEXTPROCESS_ID = Val.ToString(txtNextProcess.Tag);
                mProperty.FROMDEPARTMENT_ID = Val.ToString(txtFromDepartment.Tag);
                mProperty.TODEPARTMENT_ID = Val.ToString(txtToDepartment.Tag);

                mProperty.FROMEMPLOYEE_ID = Val.ToString(txtFromEmployee.Tag);
                mProperty.TOEMPLOYEE_ID = Val.ToString(txtToEmployee.Tag);

                mProperty.FROMMANAGER_ID = Val.ToString(txtFromManager.Tag);
                mProperty.TOMANAGER_ID = Val.ToString(txtToManager.Tag);
                mProperty.FROMFACTORYHEAD_ID = Val.ToString(txtFromFactoryHead.Tag);
                mProperty.TOFACTORYHEAD_ID = Val.ToString(txtToFactoryHead.Tag);
                mProperty.LOT_ID = Val.ToString(txtLotNo.Tag);
                mProperty.KAPANMANAGER_ID = Val.ToString(txtKapanManager.Tag);
                mProperty.BARCODE = Val.ToString(txtBarcode.Text);
                mProperty.JANGEDNO = Val.ToString(txtSlipJangedNo.Text);
                mProperty.PACKETNO = Val.ToString(txtPacketNoFrom.Text);
                mProperty.TABLENAME = Val.ToString(txtTable.Tag); // Dhara : 15-09-2023
                mProperty.PacketCategory = Val.Trim(txtPacketCategory.Tag); // Dhara : 15-09-2023
                mProperty.PacketGroup = Val.Trim(txtPacketGroup.Tag); // Dhara : 15-09-2023

                mProperty.FromPacketNo = Val.ToInt(txtPacketNoFrom.Text); // Dhara : 15-09-2023
                mProperty.ToPacketNo = Val.ToInt(txtPacketNoTo.Text); // Dhara : 15-09-2023

                mProperty.DIAMONDTYPE = Val.ToString(txtDiamondType.Text);

                mProperty.PRICEDATE = null;
                if (Val.IsDate(txtPriceDate.Text) )
                {
                    mProperty.PRICEDATE = Val.SqlDate(txtPriceDate.Text);                    
                }

                if (DTPFromDate.Checked == true && DTPToDate.Checked == true)
                {
                    mProperty.FROMDATE = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    mProperty.TODATE = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                else
                {
                    mProperty.FROMDATE = null;
                    mProperty.TODATE = null;
                }

                if (DTPKapanCreateFrom.Checked == true && DTPKapanCreateTo.Checked == true)
                {
                    mProperty.KapanCreateFromDate = Val.SqlDate(DTPKapanCreateFrom.Value.ToShortDateString());
                    mProperty.KapanCreateToDate = Val.SqlDate(DTPKapanCreateTo.Value.ToShortDateString());
                }
                else
                {
                    mProperty.KapanCreateFromDate = null;
                    mProperty.KapanCreateToDate = null;
                }
                if (DTPClvIssueFrom.Checked == true && DTPClvIssueTo.Checked == true)
                {
                    mProperty.ClvIssueFromDate = Val.SqlDate(DTPClvIssueFrom.Value.ToShortDateString());
                    mProperty.ClvIssueToDate = Val.SqlDate(DTPClvIssueTo.Value.ToShortDateString());
                }
                else
                {
                    mProperty.ClvIssueFromDate = null;
                    mProperty.ClvIssueToDate = null;
                }
                if (DTPClvCompFrom.Checked == true && DTPClvCompTo.Checked == true)
                {
                    mProperty.ClvCompleteFromDate = Val.SqlDate(DTPClvCompFrom.Value.ToShortDateString());
                    mProperty.ClvCompleteToDate = Val.SqlDate(DTPClvCompTo.Value.ToShortDateString());
                }
                else
                {
                    mProperty.ClvCompleteFromDate = null;
                    mProperty.ClvCompleteToDate = null;
                }
                if (DTPMfgIssueFrom.Checked == true && DTPMfgIssueTo.Checked == true)
                {
                    mProperty.MFGIssueFromDate = Val.SqlDate(DTPMfgIssueFrom.Value.ToShortDateString());
                    mProperty.MFGIssueToDate = Val.SqlDate(DTPMfgIssueTo.Value.ToShortDateString());
                }
                else
                {
                    mProperty.MFGIssueFromDate = null;
                    mProperty.MFGIssueToDate = null;
                }

                if (DTPMfgCompFrom.Checked == true && DTPMfgCompTo.Checked == true)
                {
                    mProperty.MFGCompleteFromDate = Val.SqlDate(DTPMfgCompFrom.Value.ToShortDateString());
                    mProperty.MFGCompleteToDate = Val.SqlDate(DTPMfgCompTo.Value.ToShortDateString());
                }
                else
                {
                    mProperty.MFGCompleteFromDate = null;
                    mProperty.MFGCompleteToDate = null;
                }
                
                if (DTPPolishReceiveFrom.Checked == true && DTPPolishReceiveTo.Checked == true)
                {
                    mProperty.PolishReceiveFromDate = Val.SqlDate(DTPPolishReceiveFrom.Value.ToShortDateString());
                    mProperty.PolishReceiveToDate = Val.SqlDate(DTPPolishReceiveTo.Value.ToShortDateString());
                }
                else
                {
                    mProperty.PolishReceiveFromDate = null;
                    mProperty.PolishReceiveToDate = null;
                }

                if (DTPMumbaiReceiveFrom.Checked == true && DTPMumbaiReceiveTo.Checked == true)
                {
                    mProperty.MumbaiReceiveFromDate = Val.SqlDate(DTPMumbaiReceiveFrom.Value.ToShortDateString());
                    mProperty.MumbaiReceiveToDate = Val.SqlDate(DTPMumbaiReceiveTo.Value.ToShortDateString());
                }
                else
                {
                    mProperty.MumbaiReceiveFromDate = null;
                    mProperty.MumbaiReceiveToDate = null;
                }

                if (Val.ToString(mDrow["REPORTVIEW"]) == "GridView")
                {
                    mProperty.GROUPBY = GroupByBox.GetTagValue;
                }
                if (Val.ToString(mDrow["REPORTVIEW"]) == "PivotView")
                {
                    mProperty.GROUPBY = RowByBox.GetTagValue + "," + ColumnByBox.GetTagValue;
                }
                mProperty.SPNAME = Val.ToString(mDrow["SPNAME"]);
           
                if (RbtSummary.Checked == true)
                {
                    mProperty.REPORTTYPE = "S";
                }

                else if (RbtDetail.Checked == true)
                {
                    mProperty.REPORTTYPE = "D";
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                BtnGenerateReport.Enabled = false;
                PnlLoding.Visible = true;

                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                }
                watch = System.Diagnostics.Stopwatch.StartNew();
                backgroundWorker1.RunWorkerAsync();


                
            }
            catch (Exception ex)
            {
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

            txtPacketNoFrom.Text = string.Empty;
            txtPacketNoFrom.Tag = string.Empty;
            
            txtFromEmployee.Text = string.Empty;
            txtFromEmployee.Tag = string.Empty;
            
            txtToEmployee.Text = string.Empty;
            txtToEmployee.Tag = string.Empty;

            txtPacketNoFrom.Text = string.Empty;
            txtPacketNoTo.Text = string.Empty;

            txtTable.Text = string.Empty;
            txtTable.Text = string.Empty;

            txtPacketCategory.Text = string.Empty;
            txtPacketCategory.Text = string.Empty;

            txtPacketGroup.Text = string.Empty;
            txtPacketGroup.Text = string.Empty;

            RbtKapanStatusAll.Checked = false;
            RbtAllShift.Checked = false;

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
                        PanelGrid.Height = 600;
                        GroupByBox.Height = 600;
                        GrpPanel.AutoScroll = false;
                    }
                    else if (Val.ToString(mDrow["REPORTVIEW"]) == "PivotView")
                    {
                        PanelPivot.Visible = true;
                        GrpPanel.AutoScroll = true;
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
            if (txtPacketNoFrom.Text.Length != 0)
            {
                Str = Str + "PacketNo : " + txtPacketNoFrom.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (txtSlipJangedNo.Text.Length != 0)
            {
                Str = Str + "Slip JangedNo : " + txtSlipJangedNo.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }
            if (DTPFromDate.Checked == true && DTPToDate.Checked == true)
            {
                Str = Str + "Transaction Date : " + DTPFromDate.Text + " To " + DTPToDate.Text + "\n";
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
                    FrmSearch.mStrSearchField = "EMPLOYEENAME,EMPLOYEECODE";
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
                    FrmSearch.mStrSearchField = "EMPLOYEENAME,EMPLOYEECODE";
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
                    FrmSearch.mStrSearchField = "EMPLOYEENAME,EMPLOYEECODE";
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
                    FrmSearch.mStrSearchField = "EMPLOYEENAME,EMPLOYEECODE";
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
                    FrmSearch.mStrSearchField = "EMPLOYEENAME,EMPLOYEECODE";
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
                    FrmSearch.mStrSearchField = "EMPLOYEENAME,EMPLOYEECODE";
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
                    FrmSearch.mStrSearchField = "EMPLOYEENAME,EMPLOYEECODE";
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

        private void txtTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_TABLE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrColumnsToHide = "TABLE_ID";
                    FrmSearch.mStrSearchField = "TABLENAME";
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.ValueMemeter = "TABLE_ID";
                    FrmSearch.DisplayMemeter = "TABLENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtTable.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtTable.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            mDS = ObjReport.GenerateMaintainanceReport(mProperty);

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            BtnGenerateReport.Enabled = true;
            PnlLoding.Visible = false;
            watch.Stop();
            lblTime.Text = string.Format("{0:hh\\:mm\\:ss}", watch.Elapsed);

            if (Val.ToString(mDrow["REPORTVIEW"]) == "GridView")
            {
                FrmGridReportViewerWithBand FrmGridReportViewerWithBand = new FrmGridReportViewerWithBand();
                FrmGridReportViewerWithBand.MdiParent = Global.gMainRef;
                FrmGridReportViewerWithBand.ShowForm(mDS,
                    mDrow,
                    mDTabFieldDetail,
                    GroupByBox.GetTagValue,
                    GroupByBox.GetTextValue,
                    GetFilterString(),
                    ChkNoGrouping.Checked,
                    GroupByBox.GetTagValue,
                    GroupByBox.GetTextValue,
                    mProperty,
                    DTPFromDate.Value.ToShortDateString(),
                    DTPToDate.Value.ToShortDateString(),
                    "",
                    mIntFilterHeight,
                    pStrShiftType
                    );
            }

            else if (Val.ToString(mDrow["REPORTVIEW"]) == "PivotView")
            {
                FrmPivotReportViewer FrmPReportViewer = new FrmPivotReportViewer();
                FrmPReportViewer.MdiParent = Global.gMainRef;
                FrmPReportViewer.ShowForm(mDS,
                                    mDrow,
                                    mDTabFieldDetail,
                                    GroupByBox.GetTagValue,
                                    GroupByBox.GetTextValue,
                                    GetFilterString(),
                                    ChkNoGrouping.Checked,
                                    GroupByBox.GetTagValue,
                                    GroupByBox.GetTextValue,
                                    mProperty,
                                    DTPFromDate.Value.ToShortDateString(),
                                    DTPToDate.Value.ToShortDateString(),
                                    "",
                                    RowByBox.GetTagValue,
                                    ColumnByBox.GetTagValue,
                                    DataByBox.GetTagValue,
                                    mIntFilterHeight,
                                    pStrShiftType
                    );
            }

        }

        private void txtRoughType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_DIAMONDTYPE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "DIAMONDTYPE";
                    FrmSearch.ValueMemeter = "DIAMONDTYPE";
                    FrmSearch.DisplayMemeter = "DIAMONDTYPE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtDiamondType.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtDiamondType.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtPacketCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PACKETCATEGORY);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "PACKETCATEGORYNAME,PACKETCATEGORYCODE";
                    FrmSearch.mStrColumnsToHide = "PACKETCATEGORY_ID";
                    FrmSearch.ValueMemeter = "PACKETCATEGORY_ID";
                    FrmSearch.DisplayMemeter = "PACKETCATEGORYNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtPacketCategory.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtPacketCategory.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtPacketGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PACKETGROUP);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "PACKETGROUPNAME,PACKETGROUPCODE";
                    FrmSearch.mStrColumnsToHide = "PACKETGROUP_ID";
                    FrmSearch.ValueMemeter = "PACKETGROUP_ID";
                    FrmSearch.DisplayMemeter = "PACKETGROUPNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtPacketGroup.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtPacketGroup.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtRoughCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_KAPANREMARK);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "ROUGHCODE";
                    FrmSearch.ValueMemeter = "ROUGHCODE";
                    FrmSearch.DisplayMemeter = "ROUGHCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtRoughCode.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtRoughCode.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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
    }

}