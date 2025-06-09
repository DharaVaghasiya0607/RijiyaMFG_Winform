using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
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
    public partial class FrmDollarLabourQuickUpload : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleDollarLabourDetail ObjDollarLabourDet = new BOTRN_SingleDollarLabourDetail();
        DataTable DTab = new DataTable();
        DataTable DtabCreateHd2 = new DataTable();
        DataTable DtabColor = new DataTable();

        #region Property Settings

        public FrmDollarLabourQuickUpload()
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

            DtabColor = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_COLOR);

            DtabCreateHd2.Columns.Add("PRICE_ID", typeof(System.Guid));
            DtabCreateHd2.Columns.Add("SHAPE_ID", typeof(System.Int32));
            DtabCreateHd2.Columns.Add("SHAPENAME", typeof(System.String));
            DtabCreateHd2.Columns.Add("FROMCARAT", typeof(System.Decimal));
            DtabCreateHd2.Columns.Add("TOCARAT", typeof(System.Decimal));
            DtabCreateHd2.Columns.Add("COLORNAME", typeof(System.String));
            DtabCreateHd2.Columns.Add("CLARITYNAME", typeof(System.String));
            DtabCreateHd2.Columns.Add("NVALUE", typeof(System.Decimal));
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = false;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjDollarLabourDet);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtShape.Text.Trim().Length == 0)
                //{
                //    Global.Message("Shape Is Required");
                //    txtShape.Focus();
                //    return;
                //}
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

                //if (Val.Val(txtMonth.Text) > 12 || Val.Val(txtMonth.Text) <= 0)
                //{
                //    Global.Message("Your Month IS Invalid, Must Be Between 0 To 12");
                //    txtMonth.Focus();
                //    return;
                //}

                this.Cursor = Cursors.WaitCursor;

                if (Global.Confirm("Are Your Sure You Want To Save This Dollar Labour Details ?") == System.Windows.Forms.DialogResult.No)
                    return;

                this.Cursor = Cursors.WaitCursor;

                DtabCreateHd2.Rows.Clear();

                int IntRes = 0;
                DataRow DRFirstRow = null;
                DataRow DRSecondRow = null;

              
                PriceHeadDetailProperty pClsProperty = new PriceHeadDetailProperty();


                int Flag = 0;
                int FlagNewBlock = 0;

                ArrayList ALH2 = new ArrayList();

                //for (int i = 0; i < GrdDet.RowCount; i++)
                //{

                //    for (int j = 0; j < GrdDet.Columns.Count - 1; j++)
                //    {
                //        if (Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j])).Equals(string.Empty))
                //            continue;

                //        DataRow[] drColor = DtabColor.Select("COLORNAME = '" + Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j])) + "'");
                        
                //        //Fetch FromCarat and ToCarat
                //        if (Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j + 1])).Equals(string.Empty)) //Fetch FromCarat & ToCarat
                //        {
                //            string StrCarat = Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[0])).ToUpper().Replace("-", "#").Replace(" ", "");
                //            if (StrCarat.Trim().Length == 0)
                //                continue;

                //            pClsProperty.FROMCARAT = Val.Val(StrCarat.Split('#')[0]);
                //            pClsProperty.TOCARAT = Val.Val(StrCarat.Split('#')[1]);
                //            FlagNewBlock = 1;
                //        }
                        
                //        else if (drColor.Length > 0 || Flag == 1)
                //        {
                //            DataRow DCR = DtabCreateHd2.NewRow();

                //            DCR["PRICE_ID"] = Guid.NewGuid();
                //            DCR["SHAPE_ID"] = Val.ToInt32(txtShape.Tag);
                //            DCR["SHAPENAME"] = Val.ToString(txtShape.Text);

                //            if (Flag == 0)
                //            {
                //                pClsProperty.COLORNAME = Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j]));
                //            }

                //            DCR["FROMCARAT"] = pClsProperty.FROMCARAT;
                //            DCR["TOCARAT"] = pClsProperty.TOCARAT;
                //            DCR["COLORNAME"] = pClsProperty.COLORNAME;

                //            DCR["CLARITYNAME"] = Val.ToString(Val.ToString(ALH2[j]));

                //            DCR["NVALUE"] = Val.ToDecimal(GrdDet.GetRowCellValue(i, GrdDet.Columns[j + 1]));

                //            DtabCreateHd2.Rows.Add(DCR);
                //            Flag = 1;
                //        }
                //        else if (FlagNewBlock == 0) //Bind Clarity : Only One Time.....
                //        {
                //            ALH2.Add(GrdDet.GetRowCellValue(i, GrdDet.Columns[j + 1]));
                //        }
                        
                //        //if (Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j])).Length > 0 || Flag == 1)
                //        //{
                //        //    if ((IsShapeFound == 1 && ALH2.Count == 0) || FlagNewBlock == 1)
                //        //        ALH2.Add(GrdDet.GetRowCellValue(i - 1, GrdDet.Columns[j + 1]));

                //        //    Flag = 1;
                //        //    IsShapeFound = 0;
                //        //}
                //    }
                //    Flag = 0;
                //}


                for (int i = 0; i < GrdDet.RowCount; i++)
                {

                    for (int j = 0; j < GrdDet.Columns.Count - 1; j++)
                    {
                        if (Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j])).Equals(string.Empty))
                            continue;

                        DataRow[] drColor = DtabColor.Select("COLORNAME = '" + Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j])) + "'");

                        //Fetch FromCarat and ToCarat
                        //if (Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j + 1])).Equals(string.Empty)) //Fetch FromCarat & ToCarat
                        //{
                        //    string StrCarat = Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[0])).ToUpper().Replace("-", "#").Replace(" ", "");
                        //    if (StrCarat.Trim().Length == 0)
                        //        continue;

                        //    pClsProperty.FROMCARAT = Val.Val(StrCarat.Split('#')[0]);
                        //    pClsProperty.TOCARAT = Val.Val(StrCarat.Split('#')[1]);
                        //    FlagNewBlock = 1;
                        //}

                        if (drColor.Length > 0 || Flag == 1)
                        {
                            DataRow DCR = DtabCreateHd2.NewRow();

                            DCR["PRICE_ID"] = Guid.NewGuid();
                            DCR["SHAPE_ID"] = Val.ToInt32(txtShape.Tag);
                            DCR["SHAPENAME"] = Val.ToString(txtShape.Text);

                            string StrCarat = Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[0])).ToUpper().Replace("-", "#").Replace(" ", "");
                            //if (StrCarat.Trim().Length == 0)
                            //    continue;

                            pClsProperty.FROMCARAT = Val.Val(StrCarat.Split('#')[0]);
                            pClsProperty.TOCARAT = Val.Val(StrCarat.Split('#')[1]);

                            if (Flag == 0)
                            {
                                pClsProperty.COLORNAME = Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j]));
                            }

                            DCR["FROMCARAT"] = pClsProperty.FROMCARAT;
                            DCR["TOCARAT"] = pClsProperty.TOCARAT;
                            DCR["COLORNAME"] = pClsProperty.COLORNAME;

                            DCR["CLARITYNAME"] = Val.ToString(Val.ToString(ALH2[j]));

                            DCR["NVALUE"] = Val.Val(GrdDet.GetRowCellValue(i, GrdDet.Columns[j + 1]));

                            DtabCreateHd2.Rows.Add(DCR);
                            Flag = 1;
                        }
                        else if (FlagNewBlock == 0) //Bind Clarity : Only One Time.....
                        {
                            ALH2.Add(GrdDet.GetRowCellValue(i, GrdDet.Columns[j + 1]));
                        }

                        //if (Val.ToString(GrdDet.GetRowCellValue(i, GrdDet.Columns[j])).Length > 0 || Flag == 1)
                        //{
                        //    if ((IsShapeFound == 1 && ALH2.Count == 0) || FlagNewBlock == 1)
                        //        ALH2.Add(GrdDet.GetRowCellValue(i - 1, GrdDet.Columns[j + 1]));

                        //    Flag = 1;
                        //    IsShapeFound = 0;
                        //}
                    }
                    Flag = 0;
                }
                int RCount = DtabCreateHd2.Rows.Count;

                string DollarLabourDetailForXML = string.Empty;
                DtabCreateHd2.TableName = "DollarLabourDetail";
                using (StringWriter sw = new StringWriter())
                {
                    DtabCreateHd2.WriteXml(sw);
                    DollarLabourDetailForXML = sw.ToString();
                }

                pClsProperty.YY = Val.ToInt32(txtYear.Text);
                pClsProperty.MM = Val.ToInt32(txtMonth.Text);

                pClsProperty = ObjDollarLabourDet.DollarLabourQuickUpload(pClsProperty, DollarLabourDetailForXML);

                Global.Message(pClsProperty.ReturnMessageDesc);

                //if (pClsProperty.ReturnMessageType == "SUCCESS")
                //{
                //    Global.Message("LABOUR DETAIL SUCCESSFULLY UPLOADED");
                //}
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
            if (txtShape.Text.Trim().Length == 0)
            {
                Global.Message("Shape Is Required For Delete ");
                txtShape.Focus();
                return;
            }

            if (Global.Confirm("Are You Sure To Delete " + txtShape.Text + " Dollar Labour Detail ? ") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            int IntRes = ObjDollarLabourDet.DeleteShapeWiseDollarLabourDetail( Val.ToInt32(txtYear.Text), Val.ToInt(txtShape.Tag));
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
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\DollarWorkerLabour.xlsx", "CMD");

            try
            {
                    //System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\ManualEmployeeLabourFileFormatForMakable.xlsx", "CMD");
                string StrFilePathDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\DollarWorkerLabour.xlsx";
                    if (File.Exists(StrFilePathDestination))
                    {
                        File.Delete(StrFilePathDestination);
                    }
                    File.Copy(Application.StartupPath + "\\Format\\DollarWorkerLabour.xlsx", StrFilePathDestination);
                    System.Diagnostics.Process.Start(StrFilePathDestination, "CMD");
                
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
