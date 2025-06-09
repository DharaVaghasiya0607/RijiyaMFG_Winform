using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;
using System.Collections;
using BusLib.TableName;
using BusLib.Rapaport;


namespace AxoneMFGRJ
{
    public partial class FrmSearchBoxForRapCalc : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable DTab;

        public string ColumnsToHide = "";

        public bool AllowFirstColumnHide = false;

        public string ColumnHeaderCaptions = "";

        public bool ISPostBack = false;
        public string ISPostBackColumn = "";  //In Which Column Post Back Value Is Moved For eg. ItemName

        public string SearchText = "";
        public string SearchField = "";
        public string ValueMember = "";
        public string SelectedValue = "";
         AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        public DataRow DRow { get; set; }

        public double mDiaMin = 0;

        public string mStrParameterDetailXML = "";

        public FrmSearchBoxForRapCalc()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            AttachFormDefaultEvent();
            Val.FormGeneralSetting(this);

            txtPassForEditBack_TextChanged(null, null);

            this.ShowDialog();
        }
        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.FormKeyPress = true;

            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);

        }


        private void FrmSearch_Load(object sender, EventArgs e)
        {
            MainGrid.DataSource = DTab;

            txtPassForEditBack_TextChanged(null, null);

            Hashtable list = new Hashtable();

            if (!Val.ToString(ColumnHeaderCaptions).Trim().Equals(string.Empty))
            {
                string[] split = ColumnHeaderCaptions.Split(',');
                for (int IntI = 0; IntI < split.Length; IntI++)
                {
                    if (split[IntI] == "")
                    {
                        continue;
                    }
                    string[] ColSplit = split[IntI].Split('=');
                    GrdDet.Columns[ColSplit[0]].Caption = ColSplit[1];
                }
            }
            else
            {
                try
                {
                    if (System.IO.File.Exists(Application.StartupPath + "//GridHeaderCaption.txt") == true)
                    {
                        string[] Actor = System.IO.File.ReadAllLines(Application.StartupPath + "//GridHeaderCaption.txt");

                        foreach (string Str in Actor)
                        {
                            if (Str == "")
                            {
                                continue;
                            }
                            string[] S = Str.Split('=');
                            list.Add(Val.RTrim(Val.LTrim(S[0])), Val.RTrim(Val.LTrim(S[1])));
                        }

                    }
                }
                catch (Exception ec)
                {
                    Global.Message(ec.Message);
                }
            }

            foreach (DevExpress.XtraGrid.Columns.GridColumn Column in this.GrdDet.Columns)
            {
                if (ColumnsToHide.Contains(Column.Name.ToString().Replace("col", "")) == true)
                {
                    Column.Visible = false;
                }
                //if (Column.FieldName != "")
                //{
                //    Column.Caption = Column.FieldName.Replace("_", " ");
                //    Column.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Column.FieldName.ToLower());
                //}


                if (list != null && list.Count != 0)
                {
                    string sCaption = Val.ToString(list[Column.FieldName]);
                    if (sCaption != "")
                    {
                        Column.Caption = sCaption;
                    }
                }

                Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Column.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            }



            MainGrid.RefreshDataSource();
            GrdDet.BestFitMaxRowCount = 100;
            GrdDet.BestFitColumns();
            GrdDet.FocusedRowHandle = 0;
            txtSeach.Focus();
            txtSeach.Text = SearchText;
            txtSeach.SelectionStart = txtSeach.Text.Length + 1;
            txtSeach.SelectionLength = 0;
            txtSeach.DeselectAll();

        }

        private void SelectRow()
        {
            try
            {
                if ((GrdDet.SelectedRowsCount > 0))
                {
                    DRow = GrdDet.GetDataRow(GrdDet.GetSelectedRows()[0]);
                    this.Close();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    DRow = null;
                    if (ISPostBack == true)
                    {
                        if (Global.Confirm("No Row Found. You Want To PostBack ") == System.Windows.Forms.DialogResult.Yes)
                        {
                            DRow = DTab.NewRow();


                            if (ColumnsToHide.Contains("LEDGER_ID"))
                            {
                                AxoneMFGRJ.Masters.FrmLedger FrmLedger = new AxoneMFGRJ.Masters.FrmLedger();
                                FrmLedger.ShowForm(txtSeach.Text);
                                DRow["LEDGER_NAME"] = txtSeach.Text.ToUpper();
                            }
                            else
                            {
                                DRow[ISPostBackColumn] = txtSeach.Text.ToUpper();
                            }
                        }
                        this.Close();

                    }
                    else
                    {
                        Global.MessageError("No row selected.");
                        this.Close();
                    }
                }
                //else
                //{
                //    DRow = null;
                //    Global.Message("No row selected.");
                //    this.Close();

                //}
            }
            catch (Exception ex)
            {
                // KGKDiamond.Class.Global.Message(ex.Message.ToString());                
            }
        }

        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectRow();
            }
        }

        private void GrdDet_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                SelectRow();
            }
        }

        private void FrmSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SelectRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                GrdDet.FocusedRowHandle = GrdDet.FocusedRowHandle + 1;
            }
            else if (e.KeyCode == Keys.Up)
            {
                GrdDet.FocusedRowHandle = GrdDet.FocusedRowHandle - 1;
            }
        }

        private void ChangeData()
        {
            try
            {
                DataView myDataView = new DataView(DTab);

                string[] StrSplit = SearchField.Split(',');

                string RowFilter = "";
                for (int IntI = 0; IntI < StrSplit.Length; IntI++)
                {
                    if (txtSeach.Text.Length != 0)
                    {
                       
                        if (Val.ISNumeric(txtSeach.Text) && (SearchField.Contains("EMPLOYEECODE") || SearchField.Contains("LEDGERCODE") || SearchField.Contains("PROCESSCODE")))
                        {
                            RowFilter += " Convert(" + StrSplit[IntI] + ",'System.String')" + " = " + "'" + txtSeach.Text + "' ";
                        }
                        else
                        {
                            RowFilter += " Convert(" + StrSplit[IntI] + ",'System.String')" + " Like " + "'" + txtSeach.Text + "%' ";
                        }
                        if (IntI != StrSplit.Length - 1)
                        {
                            RowFilter += " Or ";
                        }
                    }

                }

                myDataView.RowFilter = RowFilter;

                //myDataView.RowFilter = "Convert(" + _FrmSearchProperty.SearchField + ",'System.String')" + " Like " + "'" + txtSearch.Text + "%'";

                //myDataView.Sort = _FrmSearchProperty.SearchField;
                MainGrid.DataSource = myDataView;

                //dgvSearch.Sort(dgvSearch.Columns[_FrmSearchProperty.SearchField], _FrmSearchProperty.SearchOrder);
            }
            catch (Exception ex)
            {
                //  KGKDiamond.Class.Global.Message(ex.ToString());
            }
        }

        private void txtSeach_TextChanged(object sender, EventArgs e)
        {
            ChangeData();
        }

        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SEL")
            {
                if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "SEL")) == true)
                {
                    GrdDet.SetRowCellValue(e.RowHandle, "SEL", false);
                }
                else
                {
                    GrdDet.SetRowCellValue(e.RowHandle, "SEL", true);
                }
            }
           
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            SelectRow();
            this.Hide();
        }

        private void txtSeach_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void MainGrid_Click(object sender, EventArgs e)
        {

        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    GrdDet.ExportToXlsx(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [" + svDialog.FileName + ".xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void txtPassForEditBack_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtPassForEditBack.Tag) != "" && Val.ToString(txtPassForEditBack.Tag).ToUpper() == txtPassForEditBack.Text.ToUpper())
                {
                    GrdDet.Columns["VALUE"].OptionsColumn.AllowEdit = true;
                    GrdDet.Columns["KEY"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    GrdDet.Columns["VALUE"].OptionsColumn.AllowEdit = false;
                    GrdDet.Columns["KEY"].OptionsColumn.AllowEdit = false;
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
                if (e.Column.FieldName == "VALUE")
                {

                    this.Cursor = Cursors.WaitCursor;

                    double DouDiscount = 0;
                    string StrSelectedParaName = "";

                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    if (DRow == null)
                        return;

                    ParameterDiscountProperty Property = new ParameterDiscountProperty();
                    Property.OPE = Val.ToString(DRow["KEY"]);
                    Property.NEWVALUE = Val.Val(DRow["VALUE"]);

                    //Property.F_CARAT = Val.Val(DRow["F_CARAT"]);
                    //Property.T_CARAT = Val.Val(DRow["T_CARAT"]);
                    //Property.S_CODE = Val.ToString(DRow["S_CODE"]);
                    //Property.C_CODE = Val.ToString(DRow["C_CODE"]);
                    //Property.C_NAME = Val.ToString(DRow["C_NAME"]);
                    //Property.Q_CODE = e.Column.FieldName;
                    //Property.Q_NAME = e.Column.Caption;
                    //Property.RAPDATE = Val.SqlDate(Val.ToString(DRow["RAPDATE"]));
                    //Property.PARAMETER_ID = Val.ToString(DRow["PARAMETER_ID"]);
                    //Property.PARAMETER_VALUE = Val.ToString(DRow["PARAMETER_VALUE"]);

                    Property = new BOFindRap().ParameterBackUpdate(Property, mStrParameterDetailXML,mDiaMin);

                    lblMessage.Text = Property.ReturnMessageDesc;
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            try
            {
                DataRow dr = GrdDet.GetFocusedDataRow();

                if (Val.ToString(dr["KEY"]).ToUpper() == "TOTALDISCOUNT" || Val.ToString(dr["KEY"]).ToUpper() == "RATE" || Val.ToString(dr["KEY"]).ToUpper() == "CARAT" || Val.ToString(dr["KEY"]).ToUpper() == "AMOUNT" || Val.ToString(dr["KEY"]).ToUpper() == "ORIGINALRATE")
                    GrdDet.Columns["VALUE"].OptionsColumn.AllowEdit = false;
                else
                {
                    if (Val.ToString(txtPassForEditBack.Tag) != "" && Val.ToString(txtPassForEditBack.Tag).ToUpper() == txtPassForEditBack.Text.ToUpper())
                    {
                        GrdDet.Columns["VALUE"].OptionsColumn.AllowEdit = true;
                    }
                }

            }
            catch(Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }


        }
    }
}