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
    public partial class FrmSalaryTypePer : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SalaryTypePer ObjTrn = new BOTRN_SalaryTypePer();
        DataTable DtabPer = new DataTable();

        bool IsNextImage = true;

        public FORMTYPE mFormType = FORMTYPE.ALL_OTHER;

        public enum FORMTYPE
        {
            OFFICESALARY = 0,
            ALL_OTHER = 1
        }


        #region Property Settings

        public FrmSalaryTypePer()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            mFormType = pFormType;

            if (mFormType == FORMTYPE.OFFICESALARY)
            {
                GrdDet.Columns["SALARYTYPE"].Visible = false;
                GrdDet.Columns["FROMCARAT"].Visible = true;
                GrdDet.Columns["TOCARAT"].Visible = true;
                this.Text = "OFFICE SALARY PER";
            }
            else
            {
                GrdDet.Columns["SALARYTYPE"].Visible = true;
                GrdDet.Columns["FROMCARAT"].Visible = false;
                GrdDet.Columns["TOCARAT"].Visible = false;
                this.Text = "SALARY TYPE PER";
            }

            BtnAdd_Click(null, null);

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
            //DtabPer.Rows.Clear();
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtCopyToYear.Text = DateTime.Now.Year.ToString();
            Fill();
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

                if (txtYear.Text.Length < 4)
                {
                    Global.Message("Your Year IS Invalid");
                    txtYear.Focus();
                    return;
                }
                string ReturnMessageValue = "";
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";


                foreach (DataRow Dr in DtabPer.Rows)
                {
                    MSTSalaryTypePerProperty Property = new MSTSalaryTypePerProperty();

                    if (mFormType == FORMTYPE.OFFICESALARY && Val.Val(Dr["FROMCARAT"]) == 0 && Val.Val(Dr["TOCARAT"]) == 0)
                        continue;

                    //if (Val.Val(Dr["PER"]) == 0)
                    //{
                    //    break;
                    //}
                    Property.WEDGES_ID = Val.ToString(Dr["WAGES_ID"]).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["WAGES_ID"]));
                    Property.YY = Val.ToInt(txtYear.Text);
                    Property.MM = Val.ToInt(txtMonth.Text);

                    Property.SALERYTYPE_ID =  mFormType == FORMTYPE.OFFICESALARY ? 1659 : Val.ToInt32(Dr["SALARYTYPE_ID"]);
                    Property.SLARYTYPE = mFormType == FORMTYPE.OFFICESALARY ? "ENGINEER/SIGNER" : Val.ToString(Dr["SALARYTYPE"]);
                    Property.PER = Val.Val(Dr["PER"]);

                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);

                    Property = ObjTrn.Save(Property);

                    ReturnMessageValue = Property.ReturnValue;
                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;

                }
                DtabPer.AcceptChanges();
               
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
            DtabPer.Rows.Clear();

            int IntSalaryType_ID = 0;

            IntSalaryType_ID = mFormType == FORMTYPE.OFFICESALARY ? 1659 : 0;

            DtabPer = ObjTrn.Fill(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text), IntSalaryType_ID);

            if (mFormType == FORMTYPE.OFFICESALARY)
            {
                DtabPer.Rows.Add(DtabPer.NewRow());
            }

            MainGrid.DataSource = DtabPer;
            MainGrid.Refresh();

            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.FocusedRowHandle = 0;
            GrdDet.Focus();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("SalaryTypeList", GrdDet);
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
                        if (Val.ToString(Drow["WAGES_ID"]) == "")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabPer.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabPer.AcceptChanges();
                        }
                        else
                        {
                            MSTSalaryTypePerProperty Property = new MSTSalaryTypePerProperty();
                            Property.WEDGES_ID = Guid.Parse(Val.ToString(Drow["WAGES_ID"]));
                            Property = ObjTrn.Delete(Property);

                            if (Property.ReturnMessageType == "SUCCESS")
                            {
                                Global.Message("ENTRY DELETED SUCCESSFULLY");
                                DtabPer.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                                DtabPer.AcceptChanges();
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

          
            Int32 StrSalaryType_ID = 0;

            StrSalaryType_ID = mFormType == FORMTYPE.OFFICESALARY ? 1659 : 0;  //1659: EngineerSinger(OfficeSalary)

            int IntRes = ObjTrn.CopyPasteDollarTypeLabourPerData(Val.ToInt(txtYear.Text), Val.ToInt32(txtMonth.Text), Val.ToInt(txtCopyToYear.Text), Val.Trim(ChkCmbMonth.Properties.GetCheckedItems()), StrSalaryType_ID);
            this.Cursor = Cursors.Default;

            if (IntRes != -1)
            {
                Global.Message("Data Copied Successfully");
                txtCopyToYear.Text = string.Empty;
                ChkCmbMonth.SelectedText = string.Empty;
                return;
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

        private void repTxtPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    GrdDet.PostEditor();
                    if (mFormType == FORMTYPE.OFFICESALARY && Val.Val(dr["FROMCARAT"]) != 0 && Val.Val(dr["TOCARAT"]) != 0
                        && GrdDet.IsLastRow) //&& Val.Val(dr["PER"]) != 0 
                    {
                        DtabPer.Rows.Add(DtabPer.NewRow());
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

    }
}
