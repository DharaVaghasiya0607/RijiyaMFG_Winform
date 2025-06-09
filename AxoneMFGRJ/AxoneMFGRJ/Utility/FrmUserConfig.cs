using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using AxoneMFGRJ;
using BusLib.Configuration;

namespace AxoneMFGRJ.Utility
{
    public partial class FrmUserConfig : Form
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        string mStrFile = Application.StartupPath + "//UserConfig.xml";

        public FrmUserConfig()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSettingForPopup(this);
            AttachFormDefaultEvent();

            this.ShowDialog();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (File.Exists(mStrFile))
            {
                File.Delete(mStrFile);
            }

            XDocument doc = new XDocument(
               new XDeclaration("1.0", null, "yes"),
               new XComment("Created with the XDocument class."),

               new XElement("UserConfig",

               new XElement("LocalDownload",
               new XAttribute("path", txtLocalDownloadPath.Text)),
               
               new XElement("LocalOutputPath",
               new XAttribute("path", txtLocalOutputPath.Text))
            ));
            doc.Save(mStrFile);
            doc = null;

            Global.gStrLocalDownloadPath = txtLocalDownloadPath.Text;
            Global.gStrLocalOutputPath = txtLocalOutputPath.Text;

            Global.Message("DATA SAVED SUCCESSFULLY");
            this.Close();

        }
        private void FrmUserConfig_Load(object sender, EventArgs e)
        {
            GetConfigData();
        }

        public void GetConfigData()
        {
            try
            {
                if (File.Exists(mStrFile))
                {
                    XDocument doc = XDocument.Load(mStrFile);
                    txtLocalDownloadPath.Text = Val.ToString(doc.Element("UserConfig").Elements("LocalDownload").Attributes("path").First().Value);
                    txtLocalOutputPath.Text = Val.ToString(doc.Element("UserConfig").Elements("LocalOutputPath").Attributes("path").First().Value);

                    Global.gStrLocalDownloadPath = txtLocalDownloadPath.Text;
                    Global.gStrLocalOutputPath = txtLocalOutputPath.Text;

                }
            }
            catch (Exception ex)
            {
                Global.MessageError("No Config Data Found, You Have To Set After Login");
            }
            
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
