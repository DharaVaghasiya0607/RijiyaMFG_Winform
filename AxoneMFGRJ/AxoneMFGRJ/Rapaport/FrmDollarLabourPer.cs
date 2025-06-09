using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
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

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmDollarLabourPer : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_DollarLabourPer ObjTrn = new BOTRN_DollarLabourPer();
        DataTable DtabPara = new DataTable();

        bool IsNextImage = true;

        enum Months
        {
            Jan = 1,
            Feb,
            March,
            April,
            May,
            June,
            July,
            Aug,
            Sep,
            Oct,
            Nov,
            Dec
        };

        public FORMTYPE mFormType = FORMTYPE.OTHER;

        public enum FORMTYPE
        {
            DFPLUSMINUS = 0,
            OTHER = 1,
            WORKERSHAPEPER = 2,
            WORKERCUTPOLSYMPER = 3
        }


        #region Property Settings

        public FrmDollarLabourPer()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            mFormType = pFormType;

            if (mFormType == FORMTYPE.DFPLUSMINUS)
            {
                GrdDet.Columns["FROMAMOUNT"].Visible = true;
                GrdDet.Columns["TOAMOUNT"].Visible = true;
                GrdDet.Columns["DOLLARLABOURTYPE"].Visible = false;
                GrdDet.Columns["SHAPENAME"].Visible = false;
                GrdDet.Columns["CUTNAME"].Visible = false;
                GrdDet.Columns["POLNAME"].Visible = false;
                GrdDet.Columns["SYMNAME"].Visible = false;
                GrdDet.Columns["FROMCARAT"].Visible = false;
                GrdDet.Columns["TOCARAT"].Visible = false;
                PnlDollarLabourType.Visible = true;
                CmbLabourType.SelectedIndex = 0;
                CmbShapeType.Visible = true;
                lblShapeType.Visible = true;
            }
            else if (mFormType == FORMTYPE.WORKERSHAPEPER)
            {
                GrdDet.Columns["SHAPENAME"].Visible = true;
                GrdDet.Columns["CUTNAME"].Visible = false;
                GrdDet.Columns["POLNAME"].Visible = false;
                GrdDet.Columns["SYMNAME"].Visible = false;
                GrdDet.Columns["FROMAMOUNT"].Visible = false;
                GrdDet.Columns["TOAMOUNT"].Visible = false;
                GrdDet.Columns["FROMCARAT"].Visible = false;
                GrdDet.Columns["TOCARAT"].Visible = false;
                GrdDet.Columns["DOLLARLABOURTYPE"].Visible = false;
                PnlDollarLabourType.Visible = true;
                //CmbLabourType.SelectedIndex = 0;
                CmbLabourType.Text = "Worker Shape Per";
                CmbLabourType.Enabled = false;
                CmbShapeType.SelectedIndex = -1;
                CmbShapeType.Visible = false;
                lblShapeType.Visible = false;

            }
            else if (mFormType == FORMTYPE.WORKERCUTPOLSYMPER)
            {
                GrdDet.Columns["CUTNAME"].Visible = true;
                GrdDet.Columns["POLNAME"].Visible = true;
                GrdDet.Columns["SYMNAME"].Visible = true;
                GrdDet.Columns["SHAPENAME"].Visible = false;
                GrdDet.Columns["FROMAMOUNT"].Visible = false;
                GrdDet.Columns["TOAMOUNT"].Visible = false;
                GrdDet.Columns["FROMCARAT"].Visible = false;
                GrdDet.Columns["TOCARAT"].Visible = false;
                GrdDet.Columns["DOLLARLABOURTYPE"].Visible = false;
                PnlDollarLabourType.Visible = true;
                CmbShapeType.SelectedIndex = -1;
                CmbLabourType.Text = "Worker CutPolSym Per";
                CmbLabourType.Enabled = false;
                CmbShapeType.Visible = false;
                lblShapeType.Visible = false;
            }
            else
            {
                GrdDet.Columns["SHAPENAME"].Visible = false;
                GrdDet.Columns["FROMAMOUNT"].Visible = false;
                GrdDet.Columns["TOAMOUNT"].Visible = false;
                GrdDet.Columns["FROMCARAT"].Visible = false;
                GrdDet.Columns["TOCARAT"].Visible = false;
                GrdDet.Columns["CUTNAME"].Visible = false;
                GrdDet.Columns["POLNAME"].Visible = false;
                GrdDet.Columns["SYMNAME"].Visible = false;
                PnlDollarLabourType.Visible = false;
                GrdDet.Columns["DOLLARLABOURTYPE"].Visible = true;
                CmbLabourType.SelectedIndex = -1;
                CmbShapeType.Visible = true;
                lblShapeType.Visible = true;
            }


            DtabPara.Columns.Add("LABOUR_ID", typeof(System.Guid));
            DtabPara.Columns.Add("YY", typeof(System.Int32));
            DtabPara.Columns.Add("MM", typeof(System.Int32));
            DtabPara.Columns.Add("FROMCARAT", typeof(System.Double));
            DtabPara.Columns.Add("TOCARAT", typeof(System.Double));
            DtabPara.Columns.Add("FROMAMOUNT", typeof(System.Double));
            DtabPara.Columns.Add("TOAMOUNT", typeof(System.Double));
            DtabPara.Columns.Add("DOLLARLABOURTYPE", typeof(System.String));
            DtabPara.Columns.Add("PER", typeof(System.Double));


            BtnAdd_Click(null, null);
            Fill();
            CmbShapeType.SelectedIndex = 0;

            this.Show();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            DtabPara.Rows.Clear();
            DtabPara.Rows.Add(DtabPara.NewRow());

            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtCopyToYear.Text = DateTime.Now.Year.ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.Val(txtYear.Text) == 0)
                {
                    Global.Message("Year Is Required");
                    txtYear.Focus();
                    return;
                }
                if (Val.Val(txtMonth.Text) == 0)
                {
                    Global.Message("Month Is Required");
                    txtMonth.Focus();
                    return;
                }

                if (Val.Val(txtMonth.Text) > 12 || Val.Val(txtMonth.Text) <= 0)
                {
                    Global.Message("Your Month IS Invalid, Must Be Between 0 To 12");
                    txtMonth.Focus();
                    return;
                }

                string ReturnMessageValue = "";
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";


                foreach (DataRow Dr in DtabPara.Rows)
                {
                    TrnDollarLabourPerProperty Property = new TrnDollarLabourPerProperty();
                    string StrDollarLabourType = "";
                    StrDollarLabourType = Val.ToString(CmbLabourType.Text);

                    if (StrDollarLabourType.Length == 0
                        || (CmbLabourType.Text == "Worker Shape Per" && (Val.ToString(Dr["SHAPENAME"]).Trim().Equals(string.Empty) || Val.Val(Dr["PER"]) == 0))
                        || (CmbLabourType.Text == "Worker CutPolSym Per" && (Val.ToString(Dr["SHAPENAME"]).Trim().Equals(string.Empty) || ((Val.ToString(Dr["SHAPENAME"]) == "R") && Val.ToString(Dr["CUTNAME"]).Trim().Equals(string.Empty)) || Val.ToString(Dr["POLNAME"]).Trim().Equals(string.Empty) || Val.ToString(Dr["SYMNAME"]).Trim().Equals(string.Empty)))
                        || (CmbLabourType.Text != "Worker Shape Per" && CmbLabourType.Text != "Worker CutPolSym Per" && (Val.Val(Dr["FROMAMOUNT"]) == 0 || Val.Val(Dr["TOAMOUNT"]) == 0)))
                        continue;

                    if (Val.ToString(Dr["LABOUR_ID"]) == "")
                    {
                        Dr["LABOUR_ID"] = Guid.NewGuid();
                    }
                    Property.LABOUR_ID = Guid.Parse(Val.ToString(Dr["LABOUR_ID"]));
                    Property.YY = Val.ToInt(txtYear.Text);
                    Property.MM = Val.ToInt(txtMonth.Text);


                    Property.SHAPE_ID = Val.ToInt32(Dr["SHAPE_ID"]);
                    Property.CUT_ID = Val.ToString(Dr["CUTNAME"]).Trim().Length == 0 ? 0 : Val.ToInt32(Dr["CUT_ID"]);
                    Property.POL_ID = Val.ToInt32(Dr["POL_ID"]);
                    Property.SYM_ID = Val.ToInt32(Dr["SYM_ID"]);
                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);
                    Property.DOLLARLABOURTYPE = Val.ToString(CmbLabourType.Text);
                    //Property.SHAPETYPE = Val.ToString(CmbShapeType.Text);
                    Property.SHAPETYPE = mFormType == FORMTYPE.WORKERSHAPEPER || mFormType == FORMTYPE.WORKERCUTPOLSYMPER ? "" : Val.ToString(CmbShapeType.Text);
                    Property.PER = Val.Val(Dr["PER"]);


                    Property.FROMAMOUNT = Val.Val(Dr["FROMAMOUNT"]);
                    Property.TOAMOUNT = Val.Val(Dr["TOAMOUNT"]);

                    Property = ObjTrn.Save(Property);

                    ReturnMessageValue = Property.ReturnValue;
                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DtabPara.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                    //txtItemGroupCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            if (Val.Val(txtYear.Text) == 0)
            {
                Global.Message("Year Is Required");
                txtYear.Focus();
                return;
            }
            if (Val.Val(txtMonth.Text) == 0)
            {
                Global.Message("Month Is Required");
                txtMonth.Focus();
                return;
            }

            if (Val.Val(txtMonth.Text) > 12 || Val.Val(txtMonth.Text) <= 0)
            {
                Global.Message("Your Month IS Invalid, Must Be Between 0 To 12");
                txtMonth.Focus();
                return;
            }
            DtabPara.Rows.Clear();

            string StrDollarLabourType = "";
            StrDollarLabourType = Val.ToString(CmbLabourType.Text);

            string pShapeType = "";
            pShapeType = Val.ToString(CmbShapeType.Text);

            DtabPara = ObjTrn.Fill(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text), StrDollarLabourType, pShapeType);
            DataRow DrNew = DtabPara.NewRow();
            DtabPara.Rows.Add(DrNew);
            MainGrid.DataSource = DtabPara;
            MainGrid.Refresh();

            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.FocusedRowHandle = DrNew.Table.Rows.IndexOf(DrNew);
            GrdDet.Focus();
            GrdDet.ShowEditor();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PcsLabourList", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    GrdDet.PostEditor();
                    if (((CmbLabourType.Text == "Worker Shape Per" && Val.ToString(dr["SHAPENAME"]).Length != 0 && Val.Val(dr["PER"]) != 0)
                        || (CmbLabourType.Text == "Worker CutPolSym Per" && Val.ToString(dr["SHAPENAME"]).Length != 0 && (Val.ToString(dr["SHAPENAME"]) == "R" || Val.ToString(dr["CUTNAME"]).Length != 0) && Val.ToString(dr["POLNAME"]).Length != 0 && Val.ToString(dr["SYMNAME"]).Length != 0)
                        || (Val.ToString(dr["FROMAMOUNT"]).Length != 0 && Val.ToString(dr["TOAMOUNT"]).Length != 0 && mFormType == FORMTYPE.DFPLUSMINUS))
                        && GrdDet.IsLastRow) //&& Val.Val(dr["PER"]) != 0 
                    {
                        DtabPara.Rows.Add(DtabPara.NewRow());
                    }
                    else if (GrdDet.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        if (Val.ToString(Drow["LABOUR_ID"]) == "")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabPara.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabPara.AcceptChanges();
                        }
                        else
                        {
                            TrnDollarLabourPerProperty Property = new TrnDollarLabourPerProperty();
                            Property.LABOUR_ID = Guid.Parse(Val.ToString(Drow["LABOUR_ID"]));
                            Property = ObjTrn.Delete(Property);

                            if (Property.ReturnMessageType == "SUCCESS")
                            {
                                Global.Message("ENTRY DELETED SUCCESSFULLY");
                                DtabPara.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                                DtabPara.AcceptChanges();
                                Fill();
                            }
                            else
                            {
                                Global.Message("ERROR IN DELETE ENTRY");
                            }


                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            Fill();
        }

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            if (IsNextImage)
            {
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A1;
                PnlCopyPaste.Visible = false;
                IsNextImage = false;
            }
            else
            {
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A2;
                PnlCopyPaste.Visible = true;
                IsNextImage = true;
                txtCopyToYear.Focus();
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {

            if (Val.Val(txtYear.Text) == 0)
            {
                Global.Message("Year Is Required");
                txtYear.Focus();
                return;
            }
            if (Val.ToString(txtMonth.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Month Is Required");
                txtMonth.Focus();
                return;
            }

            if (Val.Val(txtCopyToYear.Text) == 0)
            {
                Global.Message("Copy To Year Is Required");
                txtCopyToYear.Focus();
                return;
            }
            if (Val.ToString(ChkCmbMonth.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Copy To Month Is Required");
                ChkCmbMonth.Focus();
                return;
            }

            if (Val.ToString(ChkCmbMonth.Properties.GetCheckedItems()).Trim().Contains(Val.ToString(txtMonth.Text)))
            {
                Global.Message("You Can Not Copy Same Month Data");
                ChkCmbMonth.Focus();
                return;
            }

            if (Global.Confirm("Are You Sure You Want Transfer Labour Data?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            string StrDollarType = "";
            StrDollarType = mFormType == FORMTYPE.DFPLUSMINUS ? Val.ToString(CmbLabourType.Text) : "";

            int IntRes = ObjTrn.CopyPasteDollarTypeLabourPerData(Val.ToInt(txtYear.Text), Val.ToInt32(txtMonth.Text), Val.ToInt(txtCopyToYear.Text), Val.Trim(ChkCmbMonth.Properties.GetCheckedItems()), StrDollarType);
            this.Cursor = Cursors.Default;

            if (IntRes != -1)
            {
                Global.Message("Data Copied Successfully");
                txtCopyToYear.Text = string.Empty;
                ChkCmbMonth.Properties.ValueMember = string.Empty;
                return;
            }

        }

        private void repTxtShapeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "SHAPE_ID,IMAGE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.PostEditor();

                        GrdDet.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));

                        if (CmbLabourType.Text == "Worker CutPolSym Per")
                        {
                            DataRow Dr = GrdDet.GetFocusedDataRow();
                            string StrComb = Val.ToString(GrdDet.EditingValue) + "-" + Val.ToString(Dr["CUTNAME"]) + "-" + Val.ToString(Dr["POLNAME"]) + "-" + Val.ToString(Dr["SYMNAME"]);
                            if (CheckDuplicateCPSDetail(DtabPara, "SHAPENAME", Val.ToString(GrdDet.EditingValue), "CUTNAME", Val.ToString(Dr["SHAPENAME"]), "POLNAME", Val.ToString(Dr["POLNAME"]), "SYMNAME", Val.ToString(Dr["SYMNAME"]), GrdDet.FocusedRowHandle, "'" + StrComb + "' Combination"))
                            {
                                GrdDet.SetFocusedRowCellValue("CUTNAME", string.Empty);
                                GrdDet.SetFocusedRowCellValue("CUT_ID", 0);
                                return;
                            }
                        }
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("SHAPE_ID", 0);
                        GrdDet.SetFocusedRowCellValue("SHAPENAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtCutName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CUTCODE,CUTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CUT);
                    FrmSearch.mColumnsToHide = "CUT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.PostEditor();

                        GrdDet.SetFocusedRowCellValue("CUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
                        GrdDet.SetFocusedRowCellValue("CUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));

                        DataRow Dr = GrdDet.GetFocusedDataRow();
                        string StrComb = Val.ToString(Dr["SHAPENAME"]) + "-" + Val.ToString(GrdDet.EditingValue) + "-" + Val.ToString(Dr["POLNAME"]) + "-" + Val.ToString(Dr["SYMNAME"]);
                        if (CheckDuplicateCPSDetail(DtabPara, "SHAPENAME", Val.ToString(Dr["SHAPENAME"]), "CUTNAME", Val.ToString(GrdDet.EditingValue), "POLNAME", Val.ToString(Dr["POLNAME"]), "SYMNAME", Val.ToString(Dr["SYMNAME"]), GrdDet.FocusedRowHandle, "'" + StrComb + "' Combination"))
                        {
                            GrdDet.SetFocusedRowCellValue("CUTNAME", string.Empty);
                            GrdDet.SetFocusedRowCellValue("CUT_ID", 0);
                            return;
                        }
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("CUT_ID", 0);
                        GrdDet.SetFocusedRowCellValue("CUTNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtPolName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "POLCODE,POLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_POL);
                    FrmSearch.mColumnsToHide = "POL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("POL_ID", Val.ToString(FrmSearch.mDRow["POL_ID"]));
                        GrdDet.SetFocusedRowCellValue("POLNAME", Val.ToString(FrmSearch.mDRow["POLCODE"]));

                        DataRow Dr = GrdDet.GetFocusedDataRow();
                        string StrComb = Val.ToString(Dr["SHAPENAME"]) + "-" + Val.ToString(Dr["CUTNAME"]) + "-" + Val.ToString(GrdDet.EditingValue) + "-" + Val.ToString(Dr["SYMNAME"]);
                        if (CheckDuplicateCPSDetail(DtabPara, "SHAPENAME", Val.ToString(Dr["SHAPENAME"]), "CUTNAME", Val.ToString(Dr["CUTNAME"]), "POLNAME", Val.ToString(GrdDet.EditingValue), "SYMNAME", Val.ToString(Dr["SYMNAME"]), GrdDet.FocusedRowHandle, "'" + StrComb + "' Combination"))
                        {
                            GrdDet.SetFocusedRowCellValue("POLNAME", string.Empty);
                            GrdDet.SetFocusedRowCellValue("POL_ID", 0);
                            return;
                        }
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("POL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("POLNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtSymName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SYMCODE,SYMNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SYM);
                    FrmSearch.mColumnsToHide = "SYM_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("SYM_ID", Val.ToString(FrmSearch.mDRow["SYM_ID"]));
                        GrdDet.SetFocusedRowCellValue("SYMNAME", Val.ToString(FrmSearch.mDRow["SYMCODE"]));

                        DataRow Dr = GrdDet.GetFocusedDataRow();
                        string StrComb = Val.ToString(Dr["SHAPENAME"]) + "-" + Val.ToString(Dr["CUTNAME"]) + "-" + Val.ToString(Dr["POLNAME"]) + "-" + Val.ToString(GrdDet.EditingValue);
                        if (CheckDuplicateCPSDetail(DtabPara, "SHAPENAME", Val.ToString(Dr["SHAPENAME"]), "CUTNAME", Val.ToString(Dr["CUTNAME"]), "POLNAME", Val.ToString(Dr["POLNAME"]), "SYMNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "'" + StrComb + "' Combination"))
                        {
                            GrdDet.SetFocusedRowCellValue("SYMNAME", string.Empty);
                            GrdDet.SetFocusedRowCellValue("SYM_ID", 0);
                            return;
                        }
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("SYM_ID", 0);
                        GrdDet.SetFocusedRowCellValue("SYMNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void CmbLabourType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbLabourType.Text == "Worker Shape Per")
            {
                GrdDet.Columns["SHAPENAME"].Visible = true;
                GrdDet.Columns["CUTNAME"].Visible = false;
                GrdDet.Columns["POLNAME"].Visible = false;
                GrdDet.Columns["SYMNAME"].Visible = false;
                GrdDet.Columns["FROMAMOUNT"].Visible = false;
                GrdDet.Columns["TOAMOUNT"].Visible = false;
                GrdDet.Columns["FROMCARAT"].Visible = false;
                GrdDet.Columns["TOCARAT"].Visible = false;
                GrdDet.Columns["DOLLARLABOURTYPE"].Visible = false;
                PnlDollarLabourType.Visible = true;
                //CmbLabourType.SelectedIndex = 0;
                CmbLabourType.Text = "Worker Shape Per";
                CmbShapeType.SelectedIndex = -1;
                CmbShapeType.Visible = false;
                lblShapeType.Visible = false;
                DtabPara.Clear();
            }
            else if (CmbLabourType.Text == "Worker CutPolSym Per")
            {
                GrdDet.Columns["CUTNAME"].Visible = true;
                GrdDet.Columns["POLNAME"].Visible = true;
                GrdDet.Columns["SYMNAME"].Visible = true;
                GrdDet.Columns["CUTNAME"].VisibleIndex = 1;
                GrdDet.Columns["POLNAME"].VisibleIndex = 2;
                GrdDet.Columns["SHAPENAME"].Visible = true;
                GrdDet.Columns["FROMAMOUNT"].Visible = false;
                GrdDet.Columns["TOAMOUNT"].Visible = false;
                GrdDet.Columns["FROMCARAT"].Visible = false;
                GrdDet.Columns["TOCARAT"].Visible = false;
                GrdDet.Columns["DOLLARLABOURTYPE"].Visible = false;
                PnlDollarLabourType.Visible = true;
                CmbShapeType.SelectedIndex = -1;
                CmbLabourType.Text = "Worker CutPolSym Per";
                CmbShapeType.Visible = false;
                lblShapeType.Visible = false;
                DtabPara.Clear();
            }
            else
            {
                GrdDet.Columns["FROMAMOUNT"].Visible = true;
                GrdDet.Columns["TOAMOUNT"].Visible = true;
                GrdDet.Columns["DOLLARLABOURTYPE"].Visible = false;
                GrdDet.Columns["SHAPENAME"].Visible = false;
                GrdDet.Columns["CUTNAME"].Visible = false;
                GrdDet.Columns["POLNAME"].Visible = false;
                GrdDet.Columns["SYMNAME"].Visible = false;
                GrdDet.Columns["FROMCARAT"].Visible = false;
                GrdDet.Columns["TOCARAT"].Visible = false;
                PnlDollarLabourType.Visible = true;
                CmbShapeType.Visible = true;
                CmbShapeType.SelectedIndex = 0;
                lblShapeType.Visible = true;
                DtabPara.Clear();
            }

        }
        public bool CheckDuplicateCPSDetail(DataTable Dt, string ColShape, string ColShapeValue, string ColCut, string ColCutValue, string ColPol, string ColPolValue, string ColSym, string ColSymValue, int IntRowIndex, string StrMsg)
        {
            Dt.AcceptChanges();

            if (Val.ToString(ColCut).Trim().Equals(string.Empty))
                return false;

            var Result = from row in Dt.AsEnumerable()
                         where Val.ToString(row[ColShape]).ToUpper() == Val.ToString(ColShapeValue).ToUpper()
                               && (ColShape == "R" || Val.ToString(row[ColCut]).ToUpper() == Val.ToString(ColCutValue).ToUpper())
                               && Val.ToString(row[ColPol]).ToUpper() == Val.ToString(ColPolValue).ToUpper()
                               && Val.ToString(row[ColSym]).ToUpper() == Val.ToString(ColSymValue).ToUpper()
                               && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;

            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

    }
}
