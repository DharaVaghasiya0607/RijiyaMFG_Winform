using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Google.API.Translate;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmParameterDiscount : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_ParameterDiscount ObjTrn = new BOTRN_ParameterDiscount();
        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabDiscountData = new DataTable();
        DataTable DTabRapaportData = new DataTable();
        DataTable DTabRangeData = new DataTable();
        DataTable DTabExpense = new DataTable();
        DataTable DTabRapaportCriteria = new DataTable();
        DataTable DTabPara = new DataTable();

        BODevGridSelection ObjGridSelection;

        #region Property Settings

        public FrmParameterDiscount()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            ObjPer.GetFormPermission(Val.ToString(this.Tag));

            BtnAddNewRow.Enabled = ObjPer.ISINSERT;
            GrdDet.OptionsBehavior.Editable = ObjPer.ISUPDATE;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            FillParameterType();
            FillRapaportDate();

            txtUsername.Text = new BOMST_FormPermission().GetRapnetUserName();
            txtPassword.Text = new BOMST_FormPermission().GetRapnetPassword();

            DTabPara = new BOFindRap().GetAllParameterTable();
         }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = false;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);

        }

        #endregion

        #region Parameter Discount Method


        private void BtnSave_Click(object sender, EventArgs e)
        {

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = lblTitle.Text + "_" + Val.ToString(CmbRapDate.SelectedItem);
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrid,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

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

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("PARAMETER DISCOUNT DATA", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString(lblTitle.Text + " And " + Val.ToString(CmbRapDate.SelectedItem), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (Val.ToString(CmbParameter.SelectedItem) == "")
            {
                Global.Message("Please Select Parameter ID First");
                CmbParameter.Focus();
                return;
            }
            if (Val.ToString(CmbRapDate.SelectedItem) == "")
            {
                Global.Message("Please Select Rap Date");
                CmbRapDate.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            double DouFromSize = 0;
            double DouToSize = 0;


            if (Val.ToString(CmbSize.SelectedItem) != "")
            {
                DouFromSize = Val.Val(Val.ToString(CmbSize.SelectedItem).Split('-')[0]);
                DouToSize = Val.Val(Val.ToString(CmbSize.SelectedItem).Split('-')[1]);
            }


            string StrType = "";
         
            DTabDiscountData = ObjTrn.GetParameterDiscountData("DISCOUNT", Val.ToString(CmbParameter.SelectedItem), Val.ToString(CmbRapDate.SelectedItem), Val.ToString(CmbShape.SelectedItem), DouFromSize, DouToSize);

            MainGrid.DataSource = DTabDiscountData;
            MainGrid.Refresh();

            // GrdDet.BestFitColumns();

            this.Cursor = Cursors.Default;
        }

        public void FillParameterType()
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable DTabParameter = ObjTrn.GetParameterDiscountData("PARAMETER", "", "", "", 0, 0);
            CmbParameter.Items.Clear();
            foreach (DataRow DRow in DTabParameter.Rows)
            {
                CmbParameter.Items.Add(Val.ToString(DRow["PARAMETERTYPE"]));
            }
            DTabParameter.Dispose();
            DTabParameter = null;
            this.Cursor = Cursors.Default;
        }

        private void CmbParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Val.ToString(CmbParameter.SelectedItem) == "")
            {
                Global.Message("Please Select Parameter ID First");
                CmbParameter.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            lblTitle.Text = Val.ToString(CmbParameter.SelectedItem);

            DataTable DTabParameter = ObjTrn.GetParameterDiscountData("RAPDATE", Val.ToString(CmbParameter.SelectedItem), "", "", 0, 0);
            DTabParameter.DefaultView.Sort = "RAPDATE DESC";
            DTabParameter = DTabParameter.DefaultView.ToTable();

            CmbRapDate.Items.Clear();
            CmbSize.Items.Clear();
            CmbShape.Items.Clear();
            foreach (DataRow DRow in DTabParameter.Rows)
            {
                CmbRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            DTabParameter.Dispose();
            DTabParameter = null;

            this.Cursor = Cursors.Default;
        }

        private void CmbRapDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Val.ToString(CmbParameter.SelectedItem) == "")
            {
                Global.Message("Please Select Parameter ID First");
                CmbParameter.Focus();
                return;
            }
            if (Val.ToString(CmbRapDate.SelectedItem) == "")
            {
                Global.Message("Please Select Rap Date");
                CmbRapDate.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            DataTable DTabParameter = ObjTrn.GetParameterDiscountData("SHAPE", Val.ToString(CmbParameter.SelectedItem), Val.ToString(CmbRapDate.SelectedItem), "", 0, 0);
            CmbShape.Items.Clear();
            CmbShape.Items.Add("");
            foreach (DataRow DRow in DTabParameter.Rows)
            {
                CmbShape.Items.Add(Val.ToString(DRow["SHAPE"]));
            }
            DTabParameter.Dispose();
            DTabParameter = null;

            DataTable DTabSize = ObjTrn.GetParameterDiscountData("SIZE", Val.ToString(CmbParameter.SelectedItem), Val.ToString(CmbRapDate.SelectedItem), Val.ToString(CmbShape.SelectedItem), 0, 0);
            CmbSize.Items.Clear();
            CmbSize.Items.Add("");
            foreach (DataRow DRow in DTabSize.Rows)
            {
                CmbSize.Items.Add(Val.ToString(DRow["SIZE"]));
            }
            DTabSize.Dispose();
            DTabSize = null;

            this.Cursor = Cursors.Default;
        }

        private void CmbShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Val.ToString(CmbParameter.SelectedItem) == "")
            {
                Global.Message("Please Select Parameter ID First");
                CmbParameter.Focus();
                return;
            }
            if (Val.ToString(CmbRapDate.SelectedItem) == "")
            {
                Global.Message("Please Select Rap Date");
                CmbRapDate.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            DataTable DTabSize = ObjTrn.GetParameterDiscountData("SIZE", Val.ToString(CmbParameter.SelectedItem), Val.ToString(CmbRapDate.SelectedItem), Val.ToString(CmbShape.SelectedItem), 0, 0);
            CmbSize.Items.Clear();
            CmbSize.Items.Add("");
            foreach (DataRow DRow in DTabSize.Rows)
            {
                CmbSize.Items.Add(Val.ToString(DRow["SIZE"]));
            }
            DTabSize.Dispose();
            DTabSize = null;
            this.Cursor = Cursors.Default;
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GrdDet.PostEditor();
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Column.FieldName.Contains("Q"))
            {

                DataRow DRow = GrdDet.GetDataRow(e.RowHandle);

                if (Val.ToString(DRow["SHAPECODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["COLORNAME"]).Trim().Equals(string.Empty) || Val.Val(DRow["FROMCARAT"]) <= 0 || Val.Val(DRow["TOCARAT"]) <= 0)
                    return;

                ParameterDiscountProperty Property = new ParameterDiscountProperty();

                Property.FROMCARAT = Val.Val(DRow["FROMCARAT"]);
                Property.TOCARAT = Val.Val(DRow["TOCARAT"]);
                Property.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);
                Property.COLORCODE = Val.ToString(DRow["COLORCODE"]);
                Property.COLORNAME = Val.ToString(DRow["COLORNAME"]);
                Property.QCODE = e.Column.FieldName;
                Property.QNAME = e.Column.Caption;
                Property.RAPDATE = Val.SqlDate(Val.ToString(DRow["RAPDATE"]));
                Property.PARAMETERTYPE = Val.ToString(DRow["PARAMETERTYPE"]);
                Property.PARAMETERVALUE = Val.ToString(DRow["PARAMETERVALUE"]);

                if (Property.FROMCARAT == 0 || Property.TOCARAT == 0 || Property.SHAPECODE == "" || Property.COLORCODE == "" || Property.QCODE == "" || Property.PARAMETERTYPE == "")
                {
                    Global.Message("Some Data Has Been Missing");
                    return;
                }

                double OldValue = Val.Val(GrdDet.ActiveEditor.OldEditValue);

                //Property.OLDVALUE = Val.Val(DRow[e.Column.FieldName, DataRowVersion.Original]);

                Property.OLDVALUE = Val.Val(GrdDet.ActiveEditor.OldEditValue);
                Property.NEWVALUE = Val.Val(DRow[e.Column.FieldName, DataRowVersion.Default]);
                Property = ObjTrn.SaveParameterDiscount(Property);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    lblMessage.Text = e.Column.Caption + " Value Updateed From [ " + Property.OLDVALUE.ToString() + " ]  To [ " + Property.NEWVALUE.ToString() + " ] ";
                }
                else
                {
                    lblMessage.Text = "Error......";
                }

                GrdDet.RefreshData();
                DTabDiscountData.AcceptChanges();

                Property = null;
            }
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParameterDiscountProperty Property = new ParameterDiscountProperty();

            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                if (GrdDet.FocusedRowHandle < 0)
                {
                    return;
                }
                DataRow DRow = GrdDet.GetFocusedDataRow();
                Property.OPE = "DISCOUNT";
                Property.FROMCARAT = Val.Val(DRow["FROMCARAT"]);
                Property.TOCARAT = Val.Val(DRow["TOCARAT"]);
                Property.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);

                Property.FC_CODE = Val.ToString(DRow["COLORCODE"]);
                Property.FC_NAME = Val.ToString(DRow["COLORNAME"]);
                Property.FQ_CODE = GrdDet.FocusedColumn.FieldName;
                Property.FQ_NAME = GrdDet.FocusedColumn.Caption;

                Property.TC_CODE = Val.ToString(DRow["COLORCODE"]);
                Property.TC_NAME = Val.ToString(DRow["COLORNAME"]);
                Property.TQ_CODE = GrdDet.FocusedColumn.FieldName;
                Property.TQ_NAME = GrdDet.FocusedColumn.Caption;

                Property.RAPDATE = Val.SqlDate(Val.ToString(DRow["RAPDATE"]));
                Property.PARAMETERTYPE = Val.ToString(DRow["PARAMETERTYPE"]);
                Property.PARAMETERVALUE = Val.ToString(DRow["PARAMETERVALUE"]);
            }
            DataTable DTabHistory = ObjTrn.GetPatameterDiscountHistory(Property);

            if (DTabHistory.Rows.Count != 0)
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = DTabHistory;

                FrmSearch.mColumnsToHide = "";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();

                if (FrmSearch.mDRow != null)
                {
                }

                FrmSearch.Hide();
                FrmSearch.Dispose();
                FrmSearch = null;
            }
            else
            {
                Global.Message("NO HISTORY FOUND");
            }
            DTabHistory.Dispose();
            DTabHistory = null;
            Property = null;

        }

        #endregion

        #region Rapaport

        public void FillRapaportDate()
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable DTabParameter = ObjTrn.GetOriginalRapData("RAPDATE", "", "", 0, 0);
            DTabParameter.DefaultView.Sort = "RAPDATE DESC";
            DTabParameter = DTabParameter.DefaultView.ToTable();

            CmbRapaportRapDate.Items.Clear();
            CmbRapaportShape.Items.Clear();
            CmbRapaportSize.Items.Clear();
            foreach (DataRow DRow in DTabParameter.Rows)
            {
                CmbRapaportRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            DTabParameter.Dispose();
            DTabParameter = null;
            this.Cursor = Cursors.Default;
        }

        private void CmbRapaportRapDate_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Val.ToString(CmbRapaportRapDate.SelectedItem) == "")
            {
                Global.Message("Please Select Rap Date");
                CmbRapaportRapDate.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            DataTable DTabParameter = ObjTrn.GetOriginalRapData("SHAPE", Val.ToString(CmbRapaportRapDate.SelectedItem), "", 0, 0);
            CmbRapaportShape.Items.Clear();
            CmbRapaportShape.Items.Add("");
            foreach (DataRow DRow in DTabParameter.Rows)
            {
                CmbRapaportShape.Items.Add(Val.ToString(DRow["SHAPE"]));
            }
            DTabParameter.Dispose();
            DTabParameter = null;

            DataTable DTabSize = ObjTrn.GetOriginalRapData("SIZE", Val.ToString(CmbRapaportRapDate.SelectedItem), Val.ToString(CmbRapaportShape.SelectedItem), 0, 0);
            CmbRapaportSize.Items.Clear();
            CmbRapaportSize.Items.Add("");
            foreach (DataRow DRow in DTabSize.Rows)
            {
                CmbRapaportSize.Items.Add(Val.ToString(DRow["SIZE"]));
            }
            DTabSize.Dispose();
            DTabSize = null;

            this.Cursor = Cursors.Default;
        }

        private void CmbRapaportShape_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Val.ToString(CmbRapaportRapDate.SelectedItem) == "")
            {
                Global.Message("Please Select Rap Date");
                CmbRapaportRapDate.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            DataTable DTabSize = ObjTrn.GetOriginalRapData("SIZE", Val.ToString(CmbRapaportRapDate.SelectedItem), Val.ToString(CmbRapaportShape.SelectedItem), 0, 0);
            CmbRapaportSize.Items.Clear();
            CmbRapaportSize.Items.Add("");
            foreach (DataRow DRow in DTabSize.Rows)
            {
                CmbRapaportSize.Items.Add(Val.ToString(DRow["SIZE"]));
            }
            DTabSize.Dispose();
            DTabSize = null;
            this.Cursor = Cursors.Default;
        }

        private void BtnRapaportGetData_Click(object sender, EventArgs e)
        {
            if (Val.ToString(CmbRapaportRapDate.SelectedItem) == "")
            {
                Global.Message("Please Select Rap Date");
                CmbRapaportRapDate.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            double DouFromSize = 0;
            double DouToSize = 0;

            if (Val.ToString(CmbRapaportSize.SelectedItem) != "")
            {
                DouFromSize = Val.Val(Val.ToString(CmbRapaportSize.SelectedItem).Split('-')[0]);
                DouToSize = Val.Val(Val.ToString(CmbRapaportSize.SelectedItem).Split('-')[1]);
            }

            DTabRapaportData = ObjTrn.GetOriginalRapData("RAPVALUE", Val.ToString(CmbRapaportRapDate.SelectedItem), Val.ToString(CmbRapaportShape.SelectedItem), DouFromSize, DouToSize);

            MainGridRapaport.DataSource = DTabRapaportData;
            MainGridRapaport.Refresh();

            // GrdDet.BestFitColumns();

            this.Cursor = Cursors.Default;
        }



        private void BtnDownloadRap_Click(object sender, EventArgs e)
        {

            try
            {
                //if (txtUsername.Text.Trim().Length == 0)
                //{
                //    Global.Message("Rapnet Username And Password Is Required");
                //    txtUsername.Focus();
                //    return;
                //}
                //if (txtPassword.Text.Trim().Length == 0)
                //{
                //    Global.Message("Rapnet Username And Password Is Required");
                //    txtPassword.Focus();
                //    return;
                //}
                if (Global.Confirm("Are You Sure To Revised Your Pricing With Latest Rapaport Date ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                DataTable DTabRound = DownRap("Round");
                DataTable DTabPear = DownRap("Pear");

                //string StrFilePath = System.Windows.Forms.Application.StartupPath + "\\round.csv";
                //DataTable DTabRound = Global.GetDataTableFromCsv(StrFilePath, false);

                //StrFilePath = System.Windows.Forms.Application.StartupPath + "\\Pear.csv";
                //DataTable DTabPear = Global.GetDataTableFromCsv(StrFilePath, false);

                if (DTabRound == null || DTabRound.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.Message("Round Shape Data Not Found From Rapnet Server\n\nOr May Be Invalid Username and Password");
                    return;
                }
                if (DTabPear == null || DTabPear.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;

                    Global.Message("Pear Shape Data Not Found From RapNet Server\n\nOr May Be Invalid Username and Password");
                    return;
                }

                DTabRound.Columns["F1"].ColumnName = "S_CODE";
                DTabRound.Columns["F2"].ColumnName = "Q_CODE";
                DTabRound.Columns["F3"].ColumnName = "C_CODE";
                DTabRound.Columns["F4"].ColumnName = "F_CARAT";
                DTabRound.Columns["F5"].ColumnName = "T_CARAT";
                DTabRound.Columns["F6"].ColumnName = "RAPVALUE";
                DTabRound.Columns["F7"].ColumnName = "RAPDATE";
                DTabRound.TableName = "TABLE";

                DTabPear.Columns["F1"].ColumnName = "S_CODE";
                DTabPear.Columns["F2"].ColumnName = "Q_CODE";
                DTabPear.Columns["F3"].ColumnName = "C_CODE";
                DTabPear.Columns["F4"].ColumnName = "F_CARAT";
                DTabPear.Columns["F5"].ColumnName = "T_CARAT";
                DTabPear.Columns["F6"].ColumnName = "RAPVALUE";
                DTabPear.Columns["F7"].ColumnName = "RAPDATE";
                DTabPear.TableName = "TABLE";


                string RoundXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabRound.WriteXml(sw);
                    RoundXml = sw.ToString();
                }
                string PearXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabPear.WriteXml(sw);
                    PearXml = sw.ToString();
                }

                //string StrRapDate = Val.SqlDate(DTabRound.Rows[0]["RAPDATE"].ToString());
                //string StrRapDate = Val.SqlDate(Val.ToString(DateTime.Now));   ///Changed : 07-08-2020 : coz CurrentDate Store karva mate SqlDate conversion kadhi nakhyu 6e..
                string StrRapDate = Val.ToString(DateTime.Now);
                string StrRapDateFinal = "";

                //Commented : 07-08-2020
                //if (!Val.SqlDate(StrRapDate).Equals(string.Empty))
                //    StrRapDateFinal = Val.SqlDate(StrRapDate);
                //else
                    StrRapDateFinal = StrRapDate;

                ParameterDiscountProperty Property = new ParameterDiscountProperty();
                Property.RoundXml = RoundXml;
                Property.PearXml = PearXml;
                Property.RAPDATE = Val.SqlDate(StrRapDateFinal);

                Property = ObjTrn.UpdateRapnetWithAllDiscount(Property);
                this.Cursor = Cursors.Default;

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    string StrDate = "";
                    if (!Val.SqlDate(StrRapDate).Equals(string.Empty))
                    {
                        StrDate = StrRapDate;
                    }
                    else
                    {
                        //StrDate = DateTime.Parse(Val.ToString(DTabRound.Rows[0]["RapDate"])).ToString("dd/MM/yyyy");
                        StrDate = DateTime.Parse(Val.ToString(DateTime.Now)).ToString("dd/MM/yyyy");
                    }

                    Global.Message("SUCCESSFULLY UPLOAD & REVISED DISCOUNT DATA WITH\n\nRAPAPORT : " + StrDate + "\n\nPARAMETER DISCOUNT : " + StrDate + "\n\nRANGE VALUE DISCOUNT : " + StrDate + "\n\nSO, KINDLY CHECK IN RAPCALC");

                    DTabDiscountData.Rows.Clear();
                    DTabRapaportData.Rows.Clear();
                    DTabRangeData.Rows.Clear();

                    FillParameterType();
                    FillRapaportDate();
                }
                else if (Property.ReturnMessageType == "FAIL")
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                }
                
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }

        }


        private DataTable DownRap(string pStrFileName)
        {
            try
            {
                string URL;
                string URLAuth = "https://technet.rapaport.com/HTTP/Authenticate.aspx";
                WebClient webClient = new WebClient();

                NameValueCollection formData = new NameValueCollection();

                formData["Username"] = txtUsername.Text;
                formData["Password"] = txtPassword.Text;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                byte[] responseBytes = webClient.UploadValues(URLAuth, "POST", formData);
                string ResultAuth = Encoding.UTF8.GetString(responseBytes);

                if (ResultAuth == "")
                {
                    return null;
                }

                // ROUND

                URL = "https://technet.rapaport.com/HTTP/Prices/CSV2_" + pStrFileName + ".aspx";

                WebRequest webRequest = WebRequest.Create(URL);

                webRequest.Method = "POST";
                webRequest.ContentType = "Application/X-Www-Form-Urlencoded";
                webRequest.Headers.Add(System.Net.HttpRequestHeader.AcceptEncoding, "Gzip");

                Stream reqStream = webRequest.GetRequestStream();
                string postData = "ticket=" + ResultAuth;
                byte[] postArray = Encoding.ASCII.GetBytes(postData);
                reqStream.Write(postArray, 0, postArray.Length);
                reqStream.Close();

                WebResponse webResponse = webRequest.GetResponse();

                StreamReader str;

                if (webResponse.Headers.Get("Content-Encoding") != null && webResponse.Headers.Get("Content-Encoding").ToLower() == "gzip")
                {
                    str = new StreamReader(new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress));
                }
                else
                {
                    str = new StreamReader(webResponse.GetResponseStream());
                }

                string Result = str.ReadToEnd();

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + pStrFileName + ".csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + pStrFileName + ".csv");
                }

                using (TextWriter tw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\" + pStrFileName + ".csv", false))
                {
                    tw.Write(Result);
                }

                string StrFilePath = System.Windows.Forms.Application.StartupPath + "\\" + pStrFileName + ".csv";
                DataTable DTabRap = Global.GetDataTableFromCsv(StrFilePath, false);
                return DTabRap;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return null;
            }

        }

        #endregion

        private void BtnExportRapaport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Rapaport", GrdDetRapaport);
        }

        private void lblRoundDownload_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "csv";
                svDialog.Title = "csv file";
                svDialog.FileName = "round.csv";
                svDialog.Filter = "csv files(*.csv)|*.csv|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    this.Cursor = Cursors.WaitCursor;

                    string Filepath = svDialog.FileName;

                    string URL;
                    string URLAuth = "https://technet.rapaport.com/HTTP/Authenticate.aspx";
                    WebClient webClient = new WebClient();

                    NameValueCollection formData = new NameValueCollection();

                    formData["Username"] = txtUsername.Text;
                    formData["Password"] = txtPassword.Text;
                    byte[] responseBytes = webClient.UploadValues(URLAuth, "POST", formData);
                    string ResultAuth = Encoding.UTF8.GetString(responseBytes);

                    if (ResultAuth == "")
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }



                    // ROUND

                    URL = "https://technet.rapaport.com/HTTP/Prices/CSV2_round.aspx";

                    WebRequest webRequest = WebRequest.Create(URL);

                    webRequest.Method = "POST";
                    webRequest.ContentType = "Application/X-Www-Form-Urlencoded";
                    webRequest.Headers.Add(System.Net.HttpRequestHeader.AcceptEncoding, "Gzip");

                    Stream reqStream = webRequest.GetRequestStream();
                    string postData = "ticket=" + ResultAuth;
                    byte[] postArray = Encoding.ASCII.GetBytes(postData);
                    reqStream.Write(postArray, 0, postArray.Length);
                    reqStream.Close();

                    WebResponse webResponse = webRequest.GetResponse();

                    StreamReader str;

                    if (webResponse.Headers.Get("Content-Encoding") != null && webResponse.Headers.Get("Content-Encoding").ToLower() == "gzip")
                    {
                        str = new StreamReader(new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        str = new StreamReader(webResponse.GetResponseStream());
                    }

                    string Result = str.ReadToEnd();

                    using (TextWriter tw = new StreamWriter(Filepath, false))
                    {
                        tw.Write(Result);
                    }
                    this.Cursor = Cursors.Default;
                    System.Diagnostics.Process.Start(Filepath, "CMD");
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }

        private void lblPearDownload_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "csv";
                svDialog.Title = "csv file";
                svDialog.FileName = "pear.csv";
                svDialog.Filter = "csv files(*.csv)|*.csv|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    this.Cursor = Cursors.WaitCursor;
                    string Filepath = svDialog.FileName;

                    string URL;
                    string URLAuth = "https://technet.rapaport.com/HTTP/Authenticate.aspx";
                    WebClient webClient = new WebClient();

                    NameValueCollection formData = new NameValueCollection();

                    formData["Username"] = txtUsername.Text;
                    formData["Password"] = txtPassword.Text;
                    byte[] responseBytes = webClient.UploadValues(URLAuth, "POST", formData);
                    string ResultAuth = Encoding.UTF8.GetString(responseBytes);

                    if (ResultAuth == "")
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    // ROUND

                    URL = "https://technet.rapaport.com/HTTP/Prices/CSV2_pear.aspx";

                    WebRequest webRequest = WebRequest.Create(URL);

                    webRequest.Method = "POST";
                    webRequest.ContentType = "Application/X-Www-Form-Urlencoded";
                    webRequest.Headers.Add(System.Net.HttpRequestHeader.AcceptEncoding, "Gzip");

                    Stream reqStream = webRequest.GetRequestStream();
                    string postData = "ticket=" + ResultAuth;
                    byte[] postArray = Encoding.ASCII.GetBytes(postData);
                    reqStream.Write(postArray, 0, postArray.Length);
                    reqStream.Close();

                    WebResponse webResponse = webRequest.GetResponse();

                    StreamReader str;

                    if (webResponse.Headers.Get("Content-Encoding") != null && webResponse.Headers.Get("Content-Encoding").ToLower() == "gzip")
                    {
                        str = new StreamReader(new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        str = new StreamReader(webResponse.GetResponseStream());
                    }

                    string Result = str.ReadToEnd();

                    using (TextWriter tw = new StreamWriter(Filepath, false))
                    {
                        tw.Write(Result);
                    }
                    this.Cursor = Cursors.Default;
                    System.Diagnostics.Process.Start(Filepath, "CMD");
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }

        private void txtParameterDiscountShape_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("SHAPECODE", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
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

        private void txtParameterDiscountColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORCODE,COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);
                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("COLORCODE", Val.ToString(FrmSearch.mDRow["COLORCODE"]));
                        GrdDet.SetFocusedRowCellValue("COLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
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

        private void BtnAddNewRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(CmbParameter.SelectedItem) == "")
                {
                    Global.Message("Please Select Parameter ID First");
                    CmbParameter.Focus();
                    return;
                }
                if (Val.ToString(CmbRapDate.SelectedItem) == "")
                {
                    Global.Message("Please Select Rap Date");
                    CmbRapDate.Focus();
                    return;
                }

                if (DTabDiscountData.Columns.Count > 0)
                {

                    string StrSize = CmbSize.Text;
                    string StrFromSize = "";
                    string StrToSize = "";
                    if (!StrSize.Trim().Equals(string.Empty))
                    {
                        StrFromSize = StrSize.ToString().Substring(0, Val.ToString(StrSize).LastIndexOf("-"));
                        StrToSize = StrSize.ToString().Substring(Val.ToString(StrSize).LastIndexOf("-") + 1);
                    }

                    DataRow Dr = DTabDiscountData.NewRow();
                    Dr["RAPDATE"] = CmbRapDate.Text;
                    Dr["PARAMETERTYPE"] = CmbParameter.Text;
                    Dr["FROMCARAT"] = Val.Val(StrFromSize);
                    Dr["TOCARAT"] = Val.Val(StrToSize);
                    Dr["SHAPECODE"] = CmbShape.Text;
                    DTabDiscountData.Rows.Add(Dr);

                    GrdDet.FocusedRowHandle = Dr.Table.Rows.IndexOf(Dr);
                    GrdDet.FocusedColumn = !Val.ToString(CmbShape.Text).Equals(string.Empty) && !Val.ToString(CmbSize.Text).Equals(string.Empty) ? GrdDet.VisibleColumns[3] : GrdDet.VisibleColumns[0];
                    GrdDet.Focus();
                    GrdDet.ShowEditor();
                }
                //else
                //{
                //    BtnShow_Click(null, null);

                //    string StrSize = CmbSize.Text;
                //    string StrFromSize = "";
                //    string StrToSize = "";
                //    if (!StrSize.Trim().Equals(string.Empty))
                //    {
                //        StrFromSize = StrSize.ToString().Substring(0, Val.ToString(StrSize).LastIndexOf("-"));
                //        StrToSize = StrSize.ToString().Substring(Val.ToString(StrSize).LastIndexOf("-") + 1);
                //    }

                //    DataRow Dr = DTabDiscountData.NewRow();
                //    Dr["RAPDATE"] = CmbRapDate.Text;
                //    Dr["PARAMETER_ID"] = CmbParameter.Text;
                //    Dr["F_CARAT"] = Val.Val(StrFromSize);
                //    Dr["T_CARAT"] = Val.Val(StrToSize);
                //    Dr["S_CODE"] = CmbShape.Text;
                //    DTabDiscountData.Rows.Add(Dr);

                //    GrdDet.FocusedRowHandle = Dr.Table.Rows.IndexOf(Dr);
                //    GrdDet.FocusedColumn = (!Val.ToString(CmbShape.Text).Equals(string.Empty) && !Val.ToString(CmbSize.Text).Equals(string.Empty)) ? GrdDet.VisibleColumns[3] : GrdDet.VisibleColumns[0];
                //    GrdDet.Focus();
                //    GrdDet.ShowEditor();
                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnUpdateDiscFileWise_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(CmbParameter.SelectedItem) == "")
                {
                    Global.Message("Please Select Parameter ID First");
                    CmbParameter.Focus();
                    return;
                }
                if (Val.ToString(CmbRapDate.SelectedItem) == "")
                {
                    Global.Message("Please Select Rap Date");
                    CmbRapDate.Focus();
                    return;
                }

                if (txtFileName.Text.Trim().Length == 0)
                {
                    Global.Message("Please Select File First");
                    txtFileName.Focus();
                    return;
                }

                if (Global.Confirm("Are Your Sure You Want To Save Excel File Records..?") == System.Windows.Forms.DialogResult.No)
                    return;

                this.Cursor = Cursors.WaitCursor;

                DataTable DTab = Global.SprireGetDataTableFromExcel(txtFileName.Text, Val.ToString(CmbParaDiscountSheetName.SelectedItem), false);

                
                if (DTab == null)
                {
                    this.Cursor = Cursors.Default;
                    Global.MessageError("Data Not Found");
                    return;
                }


                foreach (DataColumn col in DTab.Columns)
                {
                    col.ColumnName = Val.Trim(col.ColumnName);
                }

                DTab.TableName = "TABLE";
                DTab.AcceptChanges();

                string StrXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    StrXml = sw.ToString();
                }

                ParameterDiscountProperty Property = new ParameterDiscountProperty();

                Property.RAPDATE = Val.SqlDate(Val.ToString(CmbRapDate.SelectedItem));
                Property.PARAMETERTYPE = Val.ToString(CmbParameter.SelectedItem);
                Property.PARAMETERVALUE = "";
                Property.XML = StrXml;
                Property = ObjTrn.SaveParameterDiscountUsingXml(Property);
                
                this.Cursor = Cursors.Default;
                Global.Message(Property.ReturnMessageDesc);
                
                Property = null;
                DTab.Dispose();
                DTab = null;
                
                return;
               
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExportSelected_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DtabSelect = GetTableOfSelectedRows(GrdDet, true);
                if (DtabSelect.Rows.Count <= 0)
                {
                    Global.Message("Please Select Records From The List that you want to Export");
                    return;
                }
                string Filepath = "";
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                svDialog.FileName = "Cut-Pol-Sym-Fl Matrix";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    Filepath = svDialog.FileName;
                }
                else
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                DataTable DtabDetailExportDistinct = DtabSelect.DefaultView.ToTable(true, "SHAPE", "FROMCARAT", "TOCARAT", "CUT", "POLISH", "SYM", "FLO");

                int StartRow = 0;
                int StartColumn = 0;
                int EndRow = 0;
                int EndColumn = 0;
                int totalRow = 0;


                //string pathExcel = Application.StartupPath + "\\" + "TestingByPinali" + ".xlsx";

                if (File.Exists(Filepath))
                {
                    File.Delete(Filepath);
                }
                FileInfo workBook = new FileInfo(Filepath);

                using (ExcelPackage xlPackage = new ExcelPackage(workBook))
                {

                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Round");

                    StartRow = 0;
                    StartColumn = 1;
                    EndRow = 0;
                    EndColumn = 30;

                    DtabDetailExportDistinct.DefaultView.Sort = "SHAPE,FROMCARAT,TOCARAT,CUT,POLISH,SYM,FLO ASC";

                    DtabDetailExportDistinct = DtabDetailExportDistinct.DefaultView.ToTable();
                    int IntSeqNo = 0;
                    foreach (DataRow DrDetailDisc in DtabDetailExportDistinct.Rows)
                    {
                        StartRow = EndRow + 2;
                        StartColumn = 1;
                        EndColumn = 1;
                        EndRow = EndRow + 2;
                        IntSeqNo = IntSeqNo + 1;

                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = "SrNo.";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 9;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120));

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = "Shape";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 9;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120));


                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = "From Cts";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 9;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120));

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = "To Cts";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 9;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120));

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = "Cut";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 9;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120));

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = "Pol";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 9;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120));

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = "Symm";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 9;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120));

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = "FL";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 9;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120));

                        StartRow = StartRow + 1;
                        StartColumn = 1;
                        EndColumn = 1;
                        EndRow = EndRow + 1;

                        //worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = DrDetailDisc["SRNO"];
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = IntSeqNo;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Black);

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;

                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = DrDetailDisc["SHAPE"];
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Black);

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = DrDetailDisc["FROMCARAT"];
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Black);

                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = DrDetailDisc["TOCARAT"];
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Black);


                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = DrDetailDisc["CUT"];
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Black);


                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = DrDetailDisc["POLISH"];
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Black);


                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = DrDetailDisc["SYM"];
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Black);


                        StartColumn = StartColumn + 1;
                        EndColumn = EndColumn + 1;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Value = DrDetailDisc["FLO"];
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Black);

                        DataTable dt = DTabDiscountData.Select("SHAPE = '" + DrDetailDisc["SHAPE"].ToString() + "' AND FROMCARAT = " + DrDetailDisc["FROMCARAT"] +
                                                               " AND TOCARAT = " + DrDetailDisc["TOCARAT"] +
                                                               " AND CUT = '" + DrDetailDisc["CUT"] + "'" +
                                                               " AND POLISH = '" + DrDetailDisc["POLISH"] + "'" +
                                                               " AND SYM = '" + DrDetailDisc["SYM"] + "'" +
                                                               " AND FLO = '" + DrDetailDisc["FLO"] + "'", "", System.Data.DataViewRowState.CurrentRows).CopyToDataTable();

                        DataTable DtabPivot = dt;

                        DtabPivot.Columns.Remove("RAPDATE");
                        DtabPivot.Columns.Remove("SHAPE");
                        DtabPivot.Columns.Remove("SHAPE_ID");
                        DtabPivot.Columns.Remove("FROMCARAT");
                        DtabPivot.Columns.Remove("TOCARAT");
                        DtabPivot.Columns.Remove("CUT");
                        DtabPivot.Columns.Remove("CUT_ID");
                        DtabPivot.Columns.Remove("POLISH");
                        DtabPivot.Columns.Remove("POLISH_ID");
                        DtabPivot.Columns.Remove("SYM");
                        DtabPivot.Columns.Remove("SYM_ID");
                        DtabPivot.Columns.Remove("FLO_ID");
                        DtabPivot.Columns.Remove("FLO");
                        DtabPivot.Columns.Remove("COLOR_ID");


                        StartRow = StartRow + 1;
                        StartColumn = 1;
                        EndColumn = DtabPivot.Columns.Count;
                        EndRow = StartRow + DtabPivot.Rows.Count;

                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].LoadFromDataTable(DtabPivot, true);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = "Verdana";
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = 8;



                        worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Font.Bold = true;
                        //worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Font.Color.SetColor(Color.FromArgb(192, 0, 0));
                        worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 120)); //Dark Blue

                        worksheet.Cells[StartRow + 1, 1, EndRow, StartColumn].Style.Font.Color.SetColor(Color.FromArgb(31, 78, 120));
                        worksheet.Cells[StartRow + 1, 1, EndRow, StartColumn].Style.Font.Bold = true;
                        worksheet.Cells[StartRow + 1, 1, EndRow, StartColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow + 1, 1, EndRow, StartColumn].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(189, 215, 238)); //Light Blue

                        //worksheet.Cells[EndRow, 1, EndRow, EndColumn].Style.Font.Bold = true;
                        //worksheet.Cells[EndRow, 1, EndRow, EndColumn].Style.Font.Color.SetColor(Color.Blue);

                    }

                    xlPackage.Save();
                    this.Cursor = Cursors.Default;
                    if (Global.Confirm("Data Export Successfully, Do You Want To Open File ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Filepath, "CMD");
                    }

                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect) //Add : Pinali : 02-08-2019
        {
            if (view.RowCount <= 0)
            {
                return null;
            }
            ArrayList aryLst = new ArrayList();


            DataTable resultTable = new DataTable();
            DataTable sourceTable = null;
            sourceTable = ((DataView)view.DataSource).Table;

            if (IsSelect)
            {
                aryLst = ObjGridSelection.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }

            return resultTable;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            CmbParaDiscountSheetName.Items.Clear();
            OpenFileDialog Open = new OpenFileDialog();
            Open.Filter = "Excel Files|*.xls;*.xlsx";
            if (Open.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = Open.FileName;
                Global.SprirelGetSheetNameFromExcel(CmbParaDiscountSheetName, txtFileName.Text);
            }
        }

        private void lblSampleFileDownload_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(Application.StartupPath+"\\Format\\ParaDiscountUploadFormat.xlsx","CMD");
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\DiscountFormat.xlsx", "CMD");
        }

        private void BtnUpdateDisNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(CmbParameter.SelectedItem) == "")
                {
                    Global.Message("Please Select Parameter ID First");
                    CmbParameter.Focus();
                    return;
                }
                if (Val.ToString(CmbRapDate.SelectedItem) == "")
                {
                    Global.Message("Please Select Rap Date");
                    CmbRapDate.Focus();
                    return;
                }

                if (txtFileName.Text.Trim().Length == 0)
                {
                    Global.Message("Please Select File First");
                    txtFileName.Focus();
                    return;
                }

                if (Global.Confirm("Are Your Sure You Want To Save Excel File Records..?") == System.Windows.Forms.DialogResult.No)
                    return;

                this.Cursor = Cursors.WaitCursor;

                DataTable DTabFile = Global.SprireGetDataTableFromExcel(txtFileName.Text, Val.ToString(CmbParaDiscountSheetName.SelectedItem), false);

                if (DTabFile == null || DTabFile.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.MessageError("Data Not Found");
                    return;
                }


                DataTable DTab = new DataTable("TABLE");
                DTab.Columns.Add(new DataColumn("SHAPE", typeof(string)));
                DTab.Columns.Add(new DataColumn("COLOR", typeof(string)));
                DTab.Columns.Add(new DataColumn("FCARAT", typeof(double)));
                DTab.Columns.Add(new DataColumn("TCARAT", typeof(double)));
                DTab.Columns.Add(new DataColumn("PARAMETERVALUE", typeof(string)));
                DTab.Columns.Add(new DataColumn("FL", typeof(double)));
                DTab.Columns.Add(new DataColumn("IF", typeof(double)));
                DTab.Columns.Add(new DataColumn("VVS1", typeof(double)));
                DTab.Columns.Add(new DataColumn("VVS2", typeof(double)));
                DTab.Columns.Add(new DataColumn("VS1", typeof(double)));
                DTab.Columns.Add(new DataColumn("VS2", typeof(double)));
                DTab.Columns.Add(new DataColumn("SI1", typeof(double)));
                DTab.Columns.Add(new DataColumn("SI2", typeof(double)));
                DTab.Columns.Add(new DataColumn("SI3", typeof(double)));
                DTab.Columns.Add(new DataColumn("I1", typeof(double)));
                DTab.Columns.Add(new DataColumn("I2", typeof(double)));
                DTab.Columns.Add(new DataColumn("I3", typeof(double)));
                DTab.Columns.Add(new DataColumn("I4", typeof(double)));
                DTab.Columns.Add(new DataColumn("I5", typeof(double)));
                DTab.Columns.Add(new DataColumn("I6", typeof(double)));
                DTab.Columns.Add(new DataColumn("I7", typeof(double)));
                DTab.Columns.Add(new DataColumn("I8", typeof(double)));

                DataRow DRNew = null;

                string StrParameterValue = string.Empty;
                string StrShape = string.Empty;
                string StrSize = string.Empty;
                string StrFromCarat = string.Empty;
                string StrToCarat = string.Empty;

                int IntShapeRowIndex = 0;
                int IntShapeColIndex = 0;

                int IntColorColIndex = 0;

                int IntSizeRowIndex = 0;
                int IntSizeColIndex = 0;
                int IntParaMeterRowIndex = 0;
                int IntParaMeterColIndex = 0;
                int IntClarityRowIndex = 0;

                for (int IntRow = 0; IntRow < DTabFile.Rows.Count; IntRow++)
                {

                    if (IntRow % 13 == 0)
                    {
                        IntShapeRowIndex = IntRow;
                        IntParaMeterRowIndex = IntRow;

                        continue;
                    }

                    if ((IntRow % 13) == 1)
                    {
                        IntSizeRowIndex = IntRow;
                        IntClarityRowIndex = IntRow;
                        continue;
                    }

                    for (int IntCol = 0; IntCol < DTabFile.Columns.Count; )
                    {
                        //if (IntCol % 13 == 0) //Consider Clarity With I3
                        if (IntCol % 18 == 0)   //Consider Clarity With I8
                        {
                            IntParaMeterColIndex = IntCol;
                            IntShapeColIndex = IntCol + 1;
                            IntColorColIndex = IntCol;
                            IntSizeColIndex = IntCol;
                            IntCol++;
                            continue;
                        }
                        DRNew = DTab.NewRow();

                        DRNew["SHAPE"] = Val.ToString(DTabFile.Rows[IntShapeRowIndex][IntShapeColIndex]);
                        DRNew["COLOR"] = Val.ToString(DTabFile.Rows[IntRow][IntColorColIndex]);
                        DRNew["FCARAT"] = Val.ToString(DTabFile.Rows[IntSizeRowIndex][IntSizeColIndex]).Split('-')[0];
                        DRNew["TCARAT"] = Val.ToString(DTabFile.Rows[IntSizeRowIndex][IntSizeColIndex]).Split('-')[1];
                        DRNew["PARAMETERVALUE"] = Val.ToString(DTabFile.Rows[IntParaMeterRowIndex][IntParaMeterColIndex]);

                        DRNew["FL"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 0]);
                        DRNew["IF"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 1]);
                        DRNew["VVS1"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 2]);
                        DRNew["VVS2"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 3]);
                        DRNew["VS1"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 4]);
                        DRNew["VS2"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 5]);
                        DRNew["SI1"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 6]);
                        DRNew["SI2"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 7]);
                        DRNew["SI3"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 8]);
                        DRNew["I1"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 9]);
                        DRNew["I2"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 10]);
                        DRNew["I3"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 11]);
                        DRNew["I4"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 12]);
                        DRNew["I5"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 13]);
                        DRNew["I6"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 14]);
                        DRNew["I7"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 15]);
                        DRNew["I8"] = Val.Val(DTabFile.Rows[IntRow][IntCol + 16]);

                        DTab.Rows.Add(DRNew);

                        //IntCol = IntCol + 12; //Consider Clarity With I3
                        IntCol = IntCol + 17; //Consider Clarity with I8

                    }

                }

                DTab.AcceptChanges();

                DTab.DefaultView.Sort = "SHAPE,FCARAT,TCARAT,COLOR";
                DTab = DTab.DefaultView.ToTable();

                string StrXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    StrXml = sw.ToString();
                }

                ParameterDiscountProperty Property = new ParameterDiscountProperty();

                Property.RAPDATE = Val.SqlDate(Val.ToString(CmbRapDate.SelectedItem));
                Property.PARAMETERTYPE = Val.ToString(CmbParameter.SelectedItem);
                Property.PARAMETERVALUE = "";
                Property.XML = StrXml;
                Property = ObjTrn.SaveParameterDiscountUsingXml(Property);

                this.Cursor = Cursors.Default;
                Global.Message(Property.ReturnMessageDesc);

                Property = null;
                DTab.Dispose();
                DTab = null;

                return;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnSaveExtraLabour_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "Please Insert Record then Save";
                string ReturnMessageType = "";
                if (CmbRapDateExtra.SelectedItem == null)
                {
                    Global.Message("Pelese Select Rap Date");
                    return;
                }
                //if (count <= 1)
                //{
                //    return;
                //}
                foreach (DataRow Dr in DTabExpense.GetChanges().Rows)
                {
                    LabExpenseMasterProperty Property = new LabExpenseMasterProperty();

                    if (Val.Val(Dr["FROMCARAT"]) == 0 || Val.Val(Dr["TOCARAT"]) == 0)
                        continue;

                    Property.PARAMETERTYPE = Val.ToString(CmbParameterExtra.SelectedItem);
                    Property.RAPDATE = Val.SqlDate(Val.ToString(CmbRapDateExtra.SelectedItem));
                    Property.PARAMETERVALUE = Val.ToString(Dr["PARAMETER_VALUE"]);
                    
                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);

                    if (Dr.RowState == DataRowState.Modified)
                    {
                        Property.FROMCARATOLD = Val.Val(Dr["FROMCARAT", DataRowVersion.Original]);
                        Property.TOCARATOLD = Val.Val(Dr["TOCARAT", DataRowVersion.Original]);
                    }
                    else
                    {
                        Property.FROMCARATOLD = Val.Val(Dr["FROMCARAT"]);
                        Property.TOCARATOLD = Val.Val(Dr["TOCARAT"]);
                    }
                    
                    Property.RATE = Val.Val(Dr["RATE"]);

                    Property = ObjTrn.ExtraLabourSave(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DTabExpense.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    BtnExtraLabourShow_Click(null, null);
                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnExtraLabourShow_Click(object sender, EventArgs e)
        {
            DTabExpense = ObjTrn.ExtraLabourGetData(Val.ToString(CmbRapDateExtra.SelectedItem), Val.ToString(CmbParameterExtra.SelectedItem));
            DTabExpense.Rows.Add(DTabExpense.NewRow());
            MainGridExtraLabour.DataSource = DTabExpense;
            MainGridExtraLabour.Refresh();
        }

        private void CmbParameterExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Val.ToString(CmbParameterExtra.SelectedItem) == "")
            {
                Global.Message("Please Select Parameter ID First");
                CmbParameter.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            DataTable DTabParameter = ObjTrn.GetParameterDiscountData("RAPDATE", Val.ToString(CmbParameterExtra.SelectedItem), "", "", 0, 0);
            DTabParameter.DefaultView.Sort = "RAPDATE DESC";
            DTabParameter = DTabParameter.DefaultView.ToTable();

            CmbRapDateExtra.Items.Clear();
            foreach (DataRow DRow in DTabParameter.Rows)
            {
                CmbRapDateExtra.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            DTabParameter.Dispose();
            DTabParameter = null;

            this.Cursor = Cursors.Default;
        }

        private void txtExtraChargeRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (GrdDetExtraLabour.IsLastRow == true &&
                    Val.Val(GrdDetExtraLabour.GetFocusedRowCellValue("TOCARAT")) != 0 &&
                    Val.Val(GrdDetExtraLabour.GetFocusedRowCellValue("RATE")) != 0 &&
                    Val.ToString(GrdDetExtraLabour.GetFocusedRowCellValue("PARAMETERVALUE")) != "")
                {
                    DataRow DRNew = DTabExpense.NewRow();
                    DTabExpense.Rows.Add(DRNew);
                }    
            }
            
        }

        private void txtRapaportShape_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRapaportShape.Text = Val.ToString(FrmSearch.mDRow["SHAPENAME"]);
                        txtRapaportShape.Tag = Val.ToString(FrmSearch.mDRow["SHAPE_ID"]);
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

        private void txtRapaportSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SIZE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.PRI_ORIGINAL_BACKSIZE);
                    FrmSearch.mColumnsToHide = "FROMCARAT,TOCARAT";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRapaportSize.Text = Val.ToString(FrmSearch.mDRow["SIZE"]);
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

        private void txtRapaportClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYCODE,CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);
                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRapaportClarity.Text = Val.ToString(FrmSearch.mDRow["CLARITYNAME"]);
                        txtRapaportClarity.Tag = Val.ToString(FrmSearch.mDRow["CLARITY_ID"]);
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

        private void BtnRapaportCriteriaSave_Click(object sender, EventArgs e)
        {
            if (txtRapaportShape.Text.Trim() == "")
            {
                Global.MessageError("Shape Is Required");
                txtRapaportShape.Focus();
                return;
            }
            if (txtRapaportClarity.Text.Trim() == "")
            {
                Global.MessageError("Clarity Is Required");
                txtRapaportClarity.Focus();
                return;
            }
            if (txtRapaportSize.Text.Trim() == "")
            {
                Global.MessageError("Size Is Required");
                txtRapaportSize.Focus();
                return;
            }
            DTabRapaportCriteria = ObjTrn.RapaportCriteriaGetData(Val.ToInt(txtRapaportShape.Tag),Val.ToInt(txtRapaportClarity.Tag), Val.Val( txtRapaportSize.Text.Split('-')[0]),Val.Val( txtRapaportSize.Text.Split('-')[1]));
            MainGridRapaportCriteria.DataSource = DTabRapaportCriteria;
            MainGridRapaportCriteria.Refresh();
        }

        private void GrdDetRapaportCriteria_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (txtRapaportShape.Text.Trim() == "")
            {
                Global.MessageError("Shape Is Required");
                txtRapaportShape.Focus();
                return;
            }
            if (txtRapaportClarity.Text.Trim() == "")
            {
                Global.MessageError("Clarity Is Required");
                txtRapaportClarity.Focus();
                return;
            }
            if (txtRapaportSize.Text.Trim() == "")
            {
                Global.MessageError("Size Is Required");
                txtRapaportSize.Focus();
                return;
            }

            GrdDetRapaportCriteria.PostEditor();
            if (e.RowHandle < 0)
            {
                return;
            }

            DataRow DRow = GrdDetRapaportCriteria.GetDataRow(e.RowHandle);

            RapaportCriteriaProperty Property = new RapaportCriteriaProperty();

            Property.SHAPE_ID = Val.ToInt(txtRapaportShape.Tag);
            Property.CLARITY_ID = Val.ToInt(txtRapaportClarity.Tag);
            Property.FROMCARAT = Val.Val(txtRapaportSize.Text.Split('-')[0]);
            Property.TOCARAT = Val.Val(txtRapaportSize.Text.Split('-')[1]);
            Property.COLORCODE = e.Column.FieldName;
            Property.PARAMETERTYPE = Val.ToString(DRow["PARAMETERTYPE"]);
            Property.PARAMETERVALUECODE = Val.ToString(DRow[e.Column.FieldName, DataRowVersion.Default]);

            Property = ObjTrn.RapaportCriteriaSave(Property);

            if (Property.ReturnMessageType == "SUCCESS")
            {
                lblRapaportCriteriaMessage.Text = e.Column.Caption + " Value Updated Of " + Property.PARAMETERTYPE + " : " + Property.PARAMETERVALUECODE + "";
                GrdDetRapaportCriteria.RefreshData();
                DTabRapaportCriteria.AcceptChanges();
            }
            else
            {
                lblRapaportCriteriaMessage.Text = Property.ReturnMessageDesc;
                DTabRapaportCriteria.Rows[e.RowHandle][e.Column.FieldName] =  "";
            }

          
            Property = null;
        }

        private void BtnRapaportCriteriaExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("RapaportCriteria.xlsx", GrdDetRapaportCriteria);
        }

        private void BtnRapaportCriteriaExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDetRapaportCriteria_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                return;
            }

            DTabPara.DefaultView.RowFilter = "PARATYPE='" + Val.ToString(GrdDetRapaportCriteria.GetFocusedRowCellValue("PARAMETERTYPE")) + "'";
            DTabPara.DefaultView.Sort = "sequenceno";
            DataTable DT = DTabPara.DefaultView.ToTable();
            MainGridMaster.DataSource = DT;
            MainGridMaster.Refresh();
            GrdDetMaster.ViewCaption = Val.ToString(GrdDetRapaportCriteria.GetFocusedRowCellValue("PARAMETERTYPE"));
        }

    }
}
