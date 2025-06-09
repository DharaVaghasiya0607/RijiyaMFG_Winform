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
    public partial class FrmTensionSakhatLabourPer : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_TesnSakhatLabourPer ObjTrn = new BOTRN_TesnSakhatLabourPer();
        DataTable DtabPara = new DataTable();

        bool IsNextImage = true;

        //public FORMTYPE mFormType = FORMTYPE.OTHER;

        //public enum FORMTYPE
        //{
        //    DFPLUSMINUS = 0,
        //    OTHER = 1,
        //    WORKERSHAPEPER = 2,
        //    WORKERCUTPOLSYMPER = 3
        //}

        #region Property Settings

        public FrmTensionSakhatLabourPer()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
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
            //DtabPara.Rows.Add(DtabPara.NewRow());

            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtCopyToYear.Text = DateTime.Now.Year.ToString();
            CmbLabourType.SelectedIndex = -1;
            txtYear.Focus();
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

                string StrLabourType = Val.ToString(CmbLabourType.Text).ToUpper() == "HARDNESS" ? "SAKHAT" : Val.ToString(CmbLabourType.Text).ToUpper();

                foreach (DataRow Dr in DtabPara.Rows)
                {
                    TrnTenSakhatLabourPerProperty Property = new TrnTenSakhatLabourPerProperty();
                    string StrDollarLabourType = "";
                    StrDollarLabourType = Val.ToString(CmbLabourType.Text);

                    if (Val.ToString(Dr["LABOUR_ID"]) == "")
                    {
                        Dr["LABOUR_ID"] = Guid.NewGuid();
                    }
                    Property.LABOUR_ID = Guid.Parse(Val.ToString(Dr["LABOUR_ID"]));
                    Property.YY = Val.ToInt(txtYear.Text);
                    Property.MM = Val.ToInt(txtMonth.Text);

                    Property.LABOURTYPENAME = Val.ToString(Dr["PARANAME"]);
                    Property.LABOURTYPE_ID = Val.ToInt(Dr["PARA_ID"]);

                    Property.LABOURTYPE = StrLabourType;
                    Property.PER = Val.Val(Dr["LABOURPER"]);

                    Property.LABOUR_SRNO = Val.ToInt(Dr["LABOUR_SRNO"]);
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
            if (CmbLabourType.SelectedIndex == -1)
            {
                Global.Message("Type Is Required");
                CmbLabourType.Focus();
                return;
            }
            DtabPara.Rows.Clear();

            string StrDollarLabourType = "";
            StrDollarLabourType = Val.ToString(CmbLabourType.Text).ToUpper() == "HARDNESS" ? "SAKHAT" : Val.ToString(CmbLabourType.Text).ToUpper();

            DtabPara = ObjTrn.Fill(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text), StrDollarLabourType);
            MainGrid.DataSource = DtabPara;
            MainGrid.Refresh();

            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.Focus();
            GrdDet.ShowEditor();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport(CmbLabourType.Text+ "LabourPerList", GrdDet);
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
                            TrnTenSakhatLabourPerProperty Property = new TrnTenSakhatLabourPerProperty();
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
            if (Val.ToString(CmbCopyToMonth.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Copy To Month Is Required");
                CmbCopyToMonth.Focus();
                return;
            }

            if (Global.Confirm("Are You Sure You Want Transfer Labour Data?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            string StrDolalrType = "";
            StrDolalrType = Val.ToString(CmbLabourType.Text) ;

            int IntRes = ObjTrn.CopyPasteTenSakhatLabourPerData(Val.ToInt(txtYear.Text), Val.ToInt32(txtMonth.Text), Val.ToInt(txtCopyToYear.Text), Val.ToInt(CmbCopyToMonth.SelectedIndex + 1), StrDolalrType);
            this.Cursor = Cursors.Default;

            if (IntRes != -1)
            {
                Global.Message("Data Copied Successfully");
                txtCopyToYear.Text = string.Empty;
                CmbCopyToMonth.SelectedText = string.Empty;
                return;
            }

        }

        private void CmbLabourType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbLabourType.Text == "Tension")
                GrdDet.Columns["PARANAME"].Caption = "Tension Name";
            else
                GrdDet.Columns["PARANAME"].Caption = "Sakhat Name";
            DtabPara.Rows.Clear();
            GrdDet.RefreshData();
        }
        
        //public bool CheckDuplicateCPSDetail(DataTable Dt, string ColShape, string ColShapeValue, string ColCut, string ColCutValue, string ColPol, string ColPolValue, string ColSym, string ColSymValue, int IntRowIndex, string StrMsg)
        //{
        //    Dt.AcceptChanges();

        //    if (Val.ToString(ColCut).Trim().Equals(string.Empty))
        //        return false;

        //    var Result = from row in Dt.AsEnumerable()
        //                 where Val.ToString(row[ColShape]).ToUpper() == Val.ToString(ColShapeValue).ToUpper()
        //                       && (ColShape == "R" || Val.ToString(row[ColCut]).ToUpper() == Val.ToString(ColCutValue).ToUpper())
        //                       && Val.ToString(row[ColPol]).ToUpper() == Val.ToString(ColPolValue).ToUpper()
        //                       && Val.ToString(row[ColSym]).ToUpper() == Val.ToString(ColSymValue).ToUpper()
        //                       && row.Table.Rows.IndexOf(row) != IntRowIndex
        //                 select row;

        //    if (Result.Any())
        //    {
        //        Global.Message(StrMsg + " ALREADY EXISTS.");
        //        return true;
        //    }
        //    return false;
        //}

    }
}
