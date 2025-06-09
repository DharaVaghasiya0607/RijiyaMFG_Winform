using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmLabourQuickUpload : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PriceHeadDetail ObjPriceDet = new BOTRN_PriceHeadDetail();
        DataTable DTab = new DataTable();

        #region Property Settings

        public FrmLabourQuickUpload()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSettingForPopup(this);
            AttachFormDefaultEvent();
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtYear.Focus();
            this.Show();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = false;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjPriceDet);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtShape.Text.Trim().Length == 0)
                {
                    Global.Message("Shape Is Required");
                    txtShape.Focus();
                    return;
                }
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

                this.Cursor = Cursors.WaitCursor;

                if (Global.Confirm("Are Your Sure You Want To Save This Labour Details ?") == System.Windows.Forms.DialogResult.No)
                    return;

                this.Cursor = Cursors.WaitCursor;

                int IntRes = 0;
                DataRow DRFirstRow = null;
                DataRow DRSecondRow = null;

                for (int IntRow = 0; IntRow < GrdDet.RowCount; IntRow++)
                {
                    if (IntRow == 0)
                    {
                        DRFirstRow = GrdDet.GetDataRow(IntRow);
                    }

                    else if (IntRow == 1)
                    {
                        DRSecondRow = GrdDet.GetDataRow(IntRow);
                    }
                    else
                    {
                        for (int IntCol = 0; IntCol < GrdDet.Columns.Count; IntCol++)
                        {
                            if (IntCol == 0 || IntCol == 1)
                            {
                                continue;
                            }
                            else
                            {
                                PriceHeadDetailProperty pClsProperty = new PriceHeadDetailProperty();
                                pClsProperty.SHAPENAME = txtShape.Text;// Val.ToString(DRFirstRow["C0"]);

                                pClsProperty.YY = Val.ToInt(txtYear.Text);
                                pClsProperty.MM = Val.ToInt(txtMonth.Text);

                                string Cla = Val.ToString(DRFirstRow["C" + IntCol.ToString()]).ToUpper().Replace("TO", "#").Replace(" ", "");
                                pClsProperty.FROMCLARITYNAME = Val.ToString(Cla.Split('#')[0]);
                                pClsProperty.TOCLARITYNAME = Val.ToString(Cla.Split('#')[1]);

                                string CutPolSym = Val.ToString(DRSecondRow["C" + IntCol.ToString()]).Replace("-", "#");

                                if (CutPolSym.Split('#').Length == 3)
                                {
                                    pClsProperty.CUTNAME = Val.ToString(CutPolSym.Split('#')[0]);
                                    pClsProperty.POLNAME = Val.ToString(CutPolSym.Split('#')[1]);
                                    pClsProperty.SYMNAME = Val.ToString(CutPolSym.Split('#')[2]);
                                }

                                else if (CutPolSym.Split('#').Length == 2)
                                {
                                    pClsProperty.CUTNAME = "";
                                    pClsProperty.POLNAME = Val.ToString(CutPolSym.Split('#')[0]);
                                    pClsProperty.SYMNAME = Val.ToString(CutPolSym.Split('#')[1]);
                                }

                                string StrCarat = Val.ToString(GrdDet.GetRowCellValue(IntRow, "C1")).ToUpper().Replace("TO", "#").Replace(" ", "");

                                if (StrCarat.Trim().Length == 0)
                                {
                                    continue;
                                }

                                pClsProperty.FROMCARAT = Val.Val(StrCarat.Split('#')[0]);
                                pClsProperty.TOCARAT = Val.Val(StrCarat.Split('#')[1]);

                                //string Col = Val.ToString(DRSecondRow["C0"]).ToUpper().Replace("TO", "#").Replace(" ", "");
                                //pClsProperty.FROMCOLORNAME = Val.ToString(Col.Split('#')[0]);
                                //pClsProperty.TOCOLORNAME = Val.ToString(Col.Split('#')[1]);

                                string Col = Val.ToString(GrdDet.GetRowCellValue(IntRow, "C0")).ToUpper().Replace("TO", "#").Replace(" ", "");
                                pClsProperty.FROMCOLORNAME = Val.ToString(Col.Split('#')[0]);
                                pClsProperty.TOCOLORNAME = Val.ToString(Col.Split('#')[1]);

                                pClsProperty.NVALUE = Val.Val(GrdDet.GetRowCellValue(IntRow, GrdDet.Columns[IntCol]));

                                pClsProperty.RAPDATE = Val.SqlDate(DateTime.Now.ToShortDateString());

                                pClsProperty = ObjPriceDet.QuickUpload(pClsProperty);
                                if (pClsProperty.ReturnMessageType == "FAIL")
                                {
                                    Global.Message(pClsProperty.ReturnMessageDesc);
                                    pClsProperty = null;
                                    return;
                                }
                                else
                                {
                                    pClsProperty = null;
                                    IntRes++;
                                }
                            }
                        }
                    }
                }

                if (IntRes != 0)
                {
                    Global.Message("LABOUR DETAIL SUCCESSFULLY UPLOADED");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void BtnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DTab = new DataTable();
                string clipboardFormats = Clipboard.GetText();

                string[] lines = clipboardFormats.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                if (lines.Length != 0)
                {
                    string[] Cols = lines[0].Split(new string[] { "\t" }, StringSplitOptions.None);
                    for (int IntCol = 0; IntCol < Cols.Length; IntCol++)
                    {
                        DTab.Columns.Add(new DataColumn("C" + IntCol.ToString(), typeof(string)));
                    }
                }

                MainGridDDet.DataSource = DTab;
                MainGridDDet.RefreshDataSource();
                GrdDet.RefreshData();

                for (int IntRow = 0; IntRow < lines.Length; IntRow++)
                {
                    string[] Cols = lines[IntRow].Split(new string[] { "\t" }, StringSplitOptions.None);

                    DataRow DRNew = DTab.NewRow();
                    for (int IntCol = 0; IntCol < Cols.Length; IntCol++)
                    {
                        DRNew["C" + IntCol.ToString()] = Val.ToString(Cols[IntCol]);
                    }
                    DTab.Rows.Add(DRNew);
                }
                MainGridDDet.RefreshDataSource();
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();
                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.Message);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtYear.Text = string.Empty;
                txtMonth.Text = string.Empty;
                DTab.Columns.Clear();
                DTab.Rows.Clear();

                MainGridDDet.DataSource = null;
                MainGridDDet.RefreshDataSource();
                GrdDet.RefreshData();
                txtYear.Focus();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnTruncateAll_Click(object sender, EventArgs e)
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
            if (txtShape.Text.Trim().Length == 0)
            {
                Global.Message("Shape Is Required For Delete ");
                txtShape.Focus();
                return;
            }

            if (Global.Confirm("Are You Sure To Delete " + txtShape.Text + " Labour Detail ? ") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            int IntRes = ObjPriceDet.DeleteShapeWiseDetail(Val.ToInt32(txtYear.Text), Val.ToInt32(txtMonth.Text), Val.ToInt(txtShape.Tag));
            if (IntRes != 0)
            {
                Global.Message("LABOUR DETAIL SUCCESSFULLY DELETED Of Shape : " + txtShape.Text);
            }

            this.Cursor = Cursors.Default;
        }

        private void txtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "SHAPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtShape.Text = Val.ToString(FrmSearch.mDRow["SHAPENAME"]);
                        txtShape.Tag = Val.ToString(FrmSearch.mDRow["SHAPE_ID"]);
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

        private void lblDownloadSample_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\PolishWorkerLabour.xlsx", "CMD");
        }

    }
}
