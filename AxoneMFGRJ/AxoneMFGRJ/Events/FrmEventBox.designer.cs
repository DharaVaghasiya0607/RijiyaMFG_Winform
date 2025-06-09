namespace AxoneMFGRJ
{
    partial class FrmEventBox
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
            this.pImage = new System.Windows.Forms.PictureBox();
            this.PBox = new AxonContLib.cPanel(this.components);
            this.cPanel3 = new AxonContLib.cPanel(this.components);
            this.cPanel2 = new AxonContLib.cPanel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cPanel1 = new AxonContLib.cPanel(this.components);
            this.timer30Second = new System.Windows.Forms.Timer(this.components);
            this.BtnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pImage)).BeginInit();
            this.PBox.SuspendLayout();
            this.cPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pImage
            // 
            this.pImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pImage.Location = new System.Drawing.Point(0, 32);
            this.pImage.Name = "pImage";
            this.pImage.Size = new System.Drawing.Size(676, 607);
            this.pImage.TabIndex = 0;
            this.pImage.TabStop = false;
            // 
            // PBox
            // 
            this.PBox.BackColor = System.Drawing.Color.White;
            this.PBox.Controls.Add(this.BtnClose);
            this.PBox.Controls.Add(this.cPanel3);
            this.PBox.Controls.Add(this.cPanel2);
            this.PBox.Controls.Add(this.cPanel1);
            this.PBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.PBox.Location = new System.Drawing.Point(0, 0);
            this.PBox.Name = "PBox";
            this.PBox.Size = new System.Drawing.Size(676, 32);
            this.PBox.TabIndex = 5;
            // 
            // cPanel3
            // 
            this.cPanel3.BackColor = System.Drawing.Color.Green;
            this.cPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.cPanel3.Location = new System.Drawing.Point(339, 0);
            this.cPanel3.Name = "cPanel3";
            this.cPanel3.Size = new System.Drawing.Size(159, 32);
            this.cPanel3.TabIndex = 8;
            // 
            // cPanel2
            // 
            this.cPanel2.BackColor = System.Drawing.Color.White;
            this.cPanel2.Controls.Add(this.cLabel2);
            this.cPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.cPanel2.Location = new System.Drawing.Point(156, 0);
            this.cPanel2.Name = "cPanel2";
            this.cPanel2.Size = new System.Drawing.Size(183, 32);
            this.cPanel2.TabIndex = 7;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cLabel2.ForeColor = System.Drawing.Color.Navy;
            this.cLabel2.Location = new System.Drawing.Point(13, 9);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(156, 14);
            this.cLabel2.TabIndex = 38;
            this.cLabel2.Text = "!! AXONE INFOTECH !!";
            this.cLabel2.ToolTips = "";
            // 
            // cPanel1
            // 
            this.cPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.cPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.cPanel1.Location = new System.Drawing.Point(0, 0);
            this.cPanel1.Name = "cPanel1";
            this.cPanel1.Size = new System.Drawing.Size(156, 32);
            this.cPanel1.TabIndex = 6;
            // 
            // timer30Second
            // 
            this.timer30Second.Enabled = true;
            this.timer30Second.Interval = 5000;
            this.timer30Second.Tick += new System.EventHandler(this.timer30Second_Tick);
            // 
            // BtnClose
            // 
            this.BtnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnClose.Image = global::AxoneMFGRJ.Properties.Resources.btnexit;
            this.BtnClose.Location = new System.Drawing.Point(644, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(32, 32);
            this.BtnClose.TabIndex = 9;
            this.BtnClose.Text = "button1";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // FrmEventBox
            // 
            this.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 639);
            this.Controls.Add(this.pImage);
            this.Controls.Add(this.PBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmEventBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "!! AXONE INFOTECH !!";
            this.Load += new System.EventHandler(this.FrmEventBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSearch_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pImage)).EndInit();
            this.PBox.ResumeLayout(false);
            this.cPanel2.ResumeLayout(false);
            this.cPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pImage;
        private AxonContLib.cPanel PBox;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cPanel cPanel3;
        private AxonContLib.cPanel cPanel2;
        private AxonContLib.cPanel cPanel1;
        private System.Windows.Forms.Timer timer30Second;
        private System.Windows.Forms.Button BtnClose;





    }
}