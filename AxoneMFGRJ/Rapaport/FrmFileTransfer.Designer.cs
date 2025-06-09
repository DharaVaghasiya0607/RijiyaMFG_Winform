namespace AxoneMFGRJ.Rapaport
{
    partial class FrmFileTransfer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFileTransfer));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.txtTag = new AxonContLib.cTextBox(this.components);
            this.panel5 = new AxonContLib.cPanel(this.components);
            this.PanelFileProcess = new AxonContLib.cFlowLayoutPanel(this.components);
            this.txtKapanName = new AxonContLib.cTextBox(this.components);
            this.txtPacketNo = new AxonContLib.cTextBox(this.components);
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtEmployee = new AxonContLib.cTextBox(this.components);
            this.cLabel18 = new AxonContLib.cLabel(this.components);
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTag
            // 
            this.txtTag.ActivationColor = true;
            this.txtTag.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtTag.AllowTabKeyOnEnter = false;
            this.txtTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTag.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTag.Format = "";
            this.txtTag.IsComplusory = false;
            this.txtTag.Location = new System.Drawing.Point(370, 52);
            this.txtTag.Name = "txtTag";
            this.txtTag.SelectAllTextOnFocus = true;
            this.txtTag.Size = new System.Drawing.Size(45, 23);
            this.txtTag.TabIndex = 5;
            this.txtTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTag.ToolTips = "";
            this.txtTag.WaterMarkText = null;
            this.txtTag.Validated += new System.EventHandler(this.txtTag_Validated);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.PanelFileProcess);
            this.panel5.Location = new System.Drawing.Point(12, 22);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(149, 269);
            this.panel5.TabIndex = 153;
            // 
            // PanelFileProcess
            // 
            this.PanelFileProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelFileProcess.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.PanelFileProcess.Location = new System.Drawing.Point(0, 0);
            this.PanelFileProcess.Margin = new System.Windows.Forms.Padding(0);
            this.PanelFileProcess.Name = "PanelFileProcess";
            this.PanelFileProcess.Size = new System.Drawing.Size(145, 265);
            this.PanelFileProcess.TabIndex = 35;
            // 
            // txtKapanName
            // 
            this.txtKapanName.ActivationColor = true;
            this.txtKapanName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtKapanName.AllowTabKeyOnEnter = false;
            this.txtKapanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKapanName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKapanName.Format = "";
            this.txtKapanName.IsComplusory = false;
            this.txtKapanName.Location = new System.Drawing.Point(179, 52);
            this.txtKapanName.Name = "txtKapanName";
            this.txtKapanName.SelectAllTextOnFocus = true;
            this.txtKapanName.Size = new System.Drawing.Size(101, 23);
            this.txtKapanName.TabIndex = 1;
            this.txtKapanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKapanName.ToolTips = "";
            this.txtKapanName.WaterMarkText = null;
            // 
            // txtPacketNo
            // 
            this.txtPacketNo.ActivationColor = true;
            this.txtPacketNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPacketNo.AllowTabKeyOnEnter = false;
            this.txtPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPacketNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacketNo.Format = "";
            this.txtPacketNo.IsComplusory = false;
            this.txtPacketNo.Location = new System.Drawing.Point(285, 52);
            this.txtPacketNo.Name = "txtPacketNo";
            this.txtPacketNo.SelectAllTextOnFocus = true;
            this.txtPacketNo.Size = new System.Drawing.Size(80, 23);
            this.txtPacketNo.TabIndex = 3;
            this.txtPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPacketNo.ToolTips = "";
            this.txtPacketNo.WaterMarkText = null;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.Appearance.Options.UseForeColor = true;
            this.BtnShow.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnShow.ImageOptions.SvgImage")));
            this.BtnShow.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnShow.Location = new System.Drawing.Point(179, 97);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(186, 34);
            this.BtnShow.TabIndex = 8;
            this.BtnShow.Text = "Transfer";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // cLabel2
            // 
            this.cLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(421, 25);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(120, 24);
            this.cLabel2.TabIndex = 6;
            this.cLabel2.Text = "EmpCode";
            this.cLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel2.ToolTips = "";
            // 
            // cLabel8
            // 
            this.cLabel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(370, 25);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(45, 24);
            this.cLabel8.TabIndex = 4;
            this.cLabel8.Text = "Tag";
            this.cLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel8.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(285, 25);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(80, 24);
            this.cLabel1.TabIndex = 2;
            this.cLabel1.Text = "SrNo";
            this.cLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel1.ToolTips = "";
            // 
            // txtEmployee
            // 
            this.txtEmployee.ActivationColor = false;
            this.txtEmployee.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtEmployee.AllowTabKeyOnEnter = false;
            this.txtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployee.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployee.Format = "";
            this.txtEmployee.IsComplusory = false;
            this.txtEmployee.Location = new System.Drawing.Point(421, 52);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.SelectAllTextOnFocus = true;
            this.txtEmployee.Size = new System.Drawing.Size(120, 23);
            this.txtEmployee.TabIndex = 7;
            this.txtEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmployee.ToolTips = "";
            this.txtEmployee.WaterMarkText = null;
            this.txtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployee_KeyPress);
            // 
            // cLabel18
            // 
            this.cLabel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel18.ForeColor = System.Drawing.Color.Black;
            this.cLabel18.Location = new System.Drawing.Point(179, 25);
            this.cLabel18.Name = "cLabel18";
            this.cLabel18.Size = new System.Drawing.Size(101, 24);
            this.cLabel18.TabIndex = 0;
            this.cLabel18.Text = "Lot";
            this.cLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel18.ToolTips = "";
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseForeColor = true;
            serializableAppearanceObject1.Options.UseTextOptions = true;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject2.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject2.Options.UseFont = true;
            serializableAppearanceObject2.Options.UseForeColor = true;
            serializableAppearanceObject2.Options.UseTextOptions = true;
            serializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject3.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject3.Options.UseFont = true;
            serializableAppearanceObject3.Options.UseForeColor = true;
            serializableAppearanceObject3.Options.UseTextOptions = true;
            serializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject4.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject4.Options.UseFont = true;
            serializableAppearanceObject4.Options.UseForeColor = true;
            serializableAppearanceObject4.Options.UseTextOptions = true;
            serializableAppearanceObject4.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnView.ImageOptions.SvgImage")));
            this.BtnView.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnView.Location = new System.Drawing.Point(370, 97);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(171, 34);
            this.BtnView.TabIndex = 156;
            this.BtnView.Text = "View";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // FrmFileTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(556, 301);
            this.Controls.Add(this.BtnView);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.txtTag);
            this.Controls.Add(this.cLabel18);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.txtKapanName);
            this.Controls.Add(this.cLabel1);
            this.Controls.Add(this.txtPacketNo);
            this.Controls.Add(this.cLabel8);
            this.Controls.Add(this.BtnShow);
            this.Controls.Add(this.cLabel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmFileTransfer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FILE TRANSFER";
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel18;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private AxonContLib.cPanel panel5;
        private AxonContLib.cFlowLayoutPanel PanelFileProcess;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtEmployee;
        private AxonContLib.cTextBox txtTag;
        private AxonContLib.cTextBox txtKapanName;
        private AxonContLib.cTextBox txtPacketNo;
        private DevExpress.XtraEditors.SimpleButton BtnView;


    }
}