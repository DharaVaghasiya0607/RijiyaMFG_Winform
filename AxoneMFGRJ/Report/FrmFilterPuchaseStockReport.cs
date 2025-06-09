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
    public partial class FrmFilterPuchaseStockReport : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        BOMST_Report ObjReport = new BOMST_Report();
        DataTable mDTabFieldDetail = new DataTable();
        DataTable mDTabRowArea = new DataTable();
        DataTable mDTabColumnArea = new DataTable();
        DataTable mDTabDataArea = new DataTable();
        int mIntFilterHeight = 0;//Gunjan:14/03/2023
        DataRow mDrow = null;
       string mStrReportGroupNew = string.Empty;

       #region Property

       public FrmFilterPuchaseStockReport()
       {
           InitializeComponent();
       }

        public void ShowForm()
        {
            mStrReportGroupNew = "Purchase Reports";
            

            AttachFormDefaultEvent();
            Val.FormGeneralSetting(this);
            lblTitle.Text = "Purchase Reports";

            txtFromInvoiceDate.EditValue = DateTime.Now;  //Add by Gunjan:14/03/2023
            txtToInvoiceDate.EditValue = DateTime.Now;

            this.ActiveControl = txtFromInvoiceDate;

            txtFromReceiveDate.EditValue = DateTime.Now;
            txtToReceiveDate.EditValue = DateTime.Now;

            this.ActiveControl = txtFromReceiveDate;//end

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

                if (txtParty.Text.Length == 0) txtParty.Tag = string.Empty;  //Add by Gunjan:14/03/2023
                if (txtSupplier.Text.Length == 0) txtSupplier.Tag = string.Empty;
                if (txtBroker.Text.Length == 0) txtBroker.Tag = string.Empty;
                if (txtLotNo.Text.Length == 0) txtLotNo.Tag = string.Empty;
                if (txtArticle.Text.Length == 0) txtArticle.Tag = string.Empty;
                if (txtRough.Text.Length == 0) txtRough.Tag = string.Empty;
                if (txtMines.Text.Length == 0) txtMines.Tag = string.Empty;
                if (txtMsize.Text.Length == 0) txtMsize.Tag = string.Empty;//end
               

                MST_ReportProperty Property = new MST_ReportProperty();

                if (mDrow == null)
                {
                    Global.Message("Select Atleast One Report For Generate");
                    return;
                }

                Property.REPORT_ID = Val.ToInt(mDrow["REPORT_ID"]);
                Property.REPORTTYPE = Val.ToString(mDrow["REPORTTYPE"]);


                Property.REPORT_ID = Val.ToInt(mDrow["REPORT_ID"]);
                Property.REPORTTYPE = Val.ToString(mDrow["REPORTTYPE"]);

                Property.PARTY_ID = Val.ToString(txtParty.Tag);  //Add by Gunjan:14/03/2023
                Property.SUPPLIER_ID = Val.ToString(txtSupplier.Tag);
                Property.BROCKER_ID = Val.ToString(txtBroker.Tag);
                Property.LOT_ID = Val.ToString(txtLotNo.Tag);
                Property.MSIZE_ID = Val.ToString(txtMsize.Text);
                Property.MINES = Val.ToString(txtMines.Text);

                Property.ARTICLE = Val.ToString(txtArticle.Tag);
                Property.ROUGH = Val.ToString(txtRough.Tag);

                if (Val.IsDate(txtFromReceiveDate.Text) && Val.IsDate(txtToReceiveDate.Text))
                {
                    Property.FROMRECEIVEDATE = Val.SqlDate(txtFromReceiveDate.Text);
                    Property.TORECEIVEDATE = Val.SqlDate(txtToReceiveDate.Text);
                }
                else
                {
                    Property.FROMRECEIVEDATE = null;
                    Property.TORECEIVEDATE = null;
                }

                if (Val.IsDate(txtFromInvoiceDate.Text) && Val.IsDate(txtToInvoiceDate.Text))
                {
                    Property.FROMINVOICEDATE = Val.SqlDate(txtFromInvoiceDate.Text);
                    Property.TOINVOICEDATE = Val.SqlDate(txtToInvoiceDate.Text);
                }
                else
                {
                    Property.FROMINVOICEDATE = null;
                    Property.TOINVOICEDATE = null;
                }//end

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
               // DataSet DS = ObjReport.GenerateMaintainanceReport(Property);//Gunjan:14/03/2023
                DataSet DS = ObjReport.GenerateMaintainanceReportNew(Property);//Gunjan:14/03/2023

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
                        "",//txtFromDate.Text,
                        "",//txtToDate.Text,
                        "",
                        0,
                        ""
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
                                        "",//txtFromDate.Text,
                                        "",//txtToDate.Text,
                                        "",
                                        RowByBox.GetTagValue,
                                        ColumnByBox.GetTagValue,
                                        DataByBox.GetTagValue,
                                        0,
                                        ""

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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)  //Add by Gunjan:14/03/2023
        {
            txtParty.Text = string.Empty;
            txtParty.Tag = string.Empty;

            txtSupplier.Text = string.Empty;
            txtSupplier.Tag = string.Empty;

            txtBroker.Text = string.Empty;
            txtBroker.Tag = string.Empty;

            txtLotNo.Text = string.Empty;
            txtLotNo.Tag = string.Empty;

            txtArticle.Text = string.Empty;
            txtArticle.Tag = string.Empty;

            txtRough.Text = string.Empty;
            txtRough.Tag = string.Empty;

            txtMines.Text = string.Empty;
            txtMines.Tag = string.Empty;

            txtMsize.Text = string.Empty;
            txtMsize.Tag = string.Empty;



            txtFromReceiveDate.Text = string.Empty;
            txtToReceiveDate.Tag = string.Empty;

            txtFromInvoiceDate.Text = string.Empty;
            txtToInvoiceDate.Tag = string.Empty;

        }

        #endregion

        #region Operation

        public string GetFilterString()  //Add by Gunjan:14/03/2023
        {
            mIntFilterHeight = 0;

            string Str = "Filter:";


            if (Val.IsDate(txtFromInvoiceDate.Text) == true && Val.IsDate(txtToInvoiceDate.Text) == true)
            {
                Str = Str + "Invoice Date : " + txtFromInvoiceDate.Text + " To " + txtToInvoiceDate.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (Val.IsDate(txtFromReceiveDate.Text) == true && Val.IsDate(txtToReceiveDate.Text) == true)
            {
                Str = Str + "Receive Date : " + txtFromReceiveDate.Text + " To " + txtToReceiveDate.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtParty.Text.Length != 0)
            {
                Str = Str + "Party : " + txtParty.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtSupplier.Text.Length != 0)
            {
                Str = Str + "Supplier : " + txtSupplier.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtBroker.Text.Length != 0)
            {
                Str = Str + "Brocker : " + txtBroker.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtLotNo.Text.Length != 0)
            {
                Str = Str + "Lot No : " + txtLotNo.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtArticle.Text.Length != 0)
            {
                Str = Str + "Article : " + txtArticle.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtRough.Text.Length != 0)
            {
                Str = Str + "Rought : " + txtRough.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtMines.Text.Length != 0)
            {
                Str = Str + "Mines : " + txtMines.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            if (txtMsize.Text.Length != 0)
            {
                Str = Str + "M-Size : " + txtMsize.Text + "\n";
                mIntFilterHeight = mIntFilterHeight + 15;
            }

            return Str;
        }

        #endregion

        #region KeyPress 

        //Add by Gunjan:14/03/2023
        private void txtParty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtParty.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtParty.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtBroker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_LEDGERBROKER);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "BROKERNAME";
                    FrmSearch.mStrColumnsToHide = "BROKER_ID";
                    FrmSearch.ValueMemeter = "BROKER_ID";
                    FrmSearch.DisplayMemeter = "BROKERNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtBroker.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtBroker.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtRough_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_ROUGHNAME);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "ROUGHNAME";
                    FrmSearch.mStrColumnsToHide = "ROUGHNAME";
                    FrmSearch.ValueMemeter = "ROUGHNAME";
                    FrmSearch.DisplayMemeter = "ROUGHNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtRough.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        //txtBroker.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_LEDGERPURCHASE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mStrColumnsToHide = "LEDGER_ID";
                    FrmSearch.ValueMemeter = "LEDGER_ID";
                    FrmSearch.DisplayMemeter = "LEDGERNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtSupplier.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtSupplier.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtMsize_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_MSIZE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "MSizeName";
                    FrmSearch.mStrColumnsToHide = "MSizeName";
                    FrmSearch.ValueMemeter = "MSizeName";
                    FrmSearch.DisplayMemeter = "MSizeName";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtMsize.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        //txtBroker.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtMines_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_MINES);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "MinesName";
                    FrmSearch.mStrColumnsToHide = "MinesName";
                    FrmSearch.ValueMemeter = "MinesName";
                    FrmSearch.DisplayMemeter = "MinesName";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtMines.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        //txtBroker.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void txtArticle_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_ARTICLE);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "ArticleName";
                    FrmSearch.mStrColumnsToHide = "ArticleName";
                    FrmSearch.ValueMemeter = "ArticleName";
                    FrmSearch.DisplayMemeter = "ArticleName";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtArticle.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        //txtBroker.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        //end
        #endregion
    }

}