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
using BusLib.Master;
using BusLib.Configuration;
using AxoneMFGRJ.Utility;
using System.Net;
using AxoneMFGRJ;
using AxoneMFGRJ.MDI;


namespace AxoneMFGRJ
{
    public partial class FrmEventBox : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable DTab;

        public string ColumnsToHide = "";

        public bool AllowFirstColumnHide = false;

        public string StrInoutText = "";


        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        BOFormPer ObjPer = new BOFormPer();

        public DataRow DRow { get; set; }

        public FrmEventBox()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            
            AttachFormDefaultEvent();
            Val.FormGeneralSetting(this);         
            this.Show();             
        }
        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.FormKeyPress = false;

            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);

        }
      
        private void FrmSearch_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtSeach_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmInputBox_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void TimSuvichar_Tick(object sender, EventArgs e)
        {
           
        }

        private void FrmEventBox_Load(object sender, EventArgs e)
        {
            var request = WebRequest.Create(Global.strEventUrl);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                pImage.Image = Bitmap.FromStream(stream);
                pImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();

            timer30Second.Stop();

            FrmMDI FrmMDI = new FrmMDI();
            ObjFormEvent.ObjToDisposeList.Add(FrmMDI);
            Global.gMainRef = FrmMDI;
            FrmMDI.ShowDialog();
        }

        private void timer30Second_Tick(object sender, EventArgs e)
        {
            try
            {
                BtnClose_Click(null,null);

            }
            catch (Exception EX)
            {
            }
        }


    }
}