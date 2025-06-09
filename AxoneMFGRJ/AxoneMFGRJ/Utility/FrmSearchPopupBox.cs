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


namespace AxoneMFGRJ
{
    public partial class FrmSearchPopupBox : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable mDTab;

        public string mColumnsToHide = "";

        public bool mAllowFirstColumnHide = false;

        public string mColumnHeaderCaptions = "";

        public bool mISPostBack = false;
        public string mISPostBackColumn = "";  //In Which Column Post Back Value Is Moved For eg. ItemName

        public string mSearchText = "";
        public string mSearchField = "";
        public string mValueMember = "";
        public string mSelectedValue = "";
         AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        public DataRow mDRow { get; set; }

        public FrmSearchPopupBox()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            AttachFormDefaultEvent();
            Val.FormGeneralSetting(this);
            this.ShowDialog();
        }
        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.FormKeyPress = true;

            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }


        private void FrmSearch_Load(object sender, EventArgs e)
        {
            GrdDet.BeginUpdate();
            MainGrid.DataSource = mDTab;

            Hashtable list = new Hashtable();

            if (!Val.ToString(mColumnHeaderCaptions).Trim().Equals(string.Empty))
            {
                string[] split = mColumnHeaderCaptions.Split(',');
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
                if (mColumnsToHide.Contains(Column.Name.ToString().Replace("col", "")) == true)
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
            GrdDet.EndUpdate();
            txtSeach.Focus();
            txtSeach.Text = mSearchText;
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
                    mDRow = GrdDet.GetDataRow(GrdDet.GetSelectedRows()[0]);
                    this.Close();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    mDRow = null;
                    if (mISPostBack == true)
                    {
                        if (Global.Confirm("No Row Found. You Want To PostBack ") == System.Windows.Forms.DialogResult.Yes)
                        {
                            mDRow = mDTab.NewRow();


                            if (mColumnsToHide.Contains("LEDGER_ID"))
                            {
                                AxoneMFGRJ.Masters.FrmLedger FrmLedger = new AxoneMFGRJ.Masters.FrmLedger();
                                FrmLedger.ShowForm(txtSeach.Text);
                                mDRow["LEDGER_NAME"] = txtSeach.Text.ToUpper();
                            }
                            else
                            {
                                mDRow[mISPostBackColumn] = txtSeach.Text.ToUpper();
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
                DataView myDataView = new DataView(mDTab);

                string[] StrSplit = mSearchField.Split(',');

                string RowFilter = "";
                for (int IntI = 0; IntI < StrSplit.Length; IntI++)
                {
                    if (txtSeach.Text.Length != 0)
                    {
                       
                        if (Val.ISNumeric(txtSeach.Text) && (mSearchField.Contains("EMPLOYEECODE") || mSearchField.Contains("LEDGERCODE") || mSearchField.Contains("PROCESSCODE")))
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

    }
}