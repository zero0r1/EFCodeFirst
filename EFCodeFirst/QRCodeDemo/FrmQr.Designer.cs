namespace QRCodeDemo
{
    partial class FrmQr
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQr));
            this.buttonSave = new System.Windows.Forms.Button();
            this.iconSize = new System.Windows.Forms.NumericUpDown();
            this.labelIconsize = new System.Windows.Forms.Label();
            this.selectIconBtn = new System.Windows.Forms.Button();
            this.iconPath = new System.Windows.Forms.TextBox();
            this.labelIcon = new System.Windows.Forms.Label();
            this.labelECC = new System.Windows.Forms.Label();
            this.comboBoxECC = new System.Windows.Forms.ComboBox();
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            this.textBoxQRCode = new System.Windows.Forms.TextBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(411, 475);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(92, 23);
            this.buttonSave.TabIndex = 21;
            this.buttonSave.Text = "Save QR code";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // iconSize
            // 
            this.iconSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconSize.Location = new System.Drawing.Point(103, 446);
            this.iconSize.Name = "iconSize";
            this.iconSize.Size = new System.Drawing.Size(125, 21);
            this.iconSize.TabIndex = 20;
            // 
            // labelIconsize
            // 
            this.labelIconsize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelIconsize.AutoSize = true;
            this.labelIconsize.Location = new System.Drawing.Point(43, 450);
            this.labelIconsize.Name = "labelIconsize";
            this.labelIconsize.Size = new System.Drawing.Size(65, 12);
            this.labelIconsize.TabIndex = 19;
            this.labelIconsize.Text = "Icon size:";
            // 
            // selectIconBtn
            // 
            this.selectIconBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectIconBtn.Location = new System.Drawing.Point(442, 446);
            this.selectIconBtn.Name = "selectIconBtn";
            this.selectIconBtn.Size = new System.Drawing.Size(61, 23);
            this.selectIconBtn.TabIndex = 18;
            this.selectIconBtn.Text = "Select";
            this.selectIconBtn.UseVisualStyleBackColor = true;
            this.selectIconBtn.Click += new System.EventHandler(this.selectIconBtn_Click);
            // 
            // iconPath
            // 
            this.iconPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconPath.Location = new System.Drawing.Point(288, 447);
            this.iconPath.Name = "iconPath";
            this.iconPath.Size = new System.Drawing.Size(148, 21);
            this.iconPath.TabIndex = 17;
            // 
            // labelIcon
            // 
            this.labelIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelIcon.AutoSize = true;
            this.labelIcon.Location = new System.Drawing.Point(251, 450);
            this.labelIcon.Name = "labelIcon";
            this.labelIcon.Size = new System.Drawing.Size(35, 12);
            this.labelIcon.TabIndex = 16;
            this.labelIcon.Text = "Icon:";
            // 
            // labelECC
            // 
            this.labelECC.AutoSize = true;
            this.labelECC.Location = new System.Drawing.Point(39, 49);
            this.labelECC.Name = "labelECC";
            this.labelECC.Size = new System.Drawing.Size(59, 12);
            this.labelECC.TabIndex = 15;
            this.labelECC.Text = "ECC-Level";
            // 
            // comboBoxECC
            // 
            this.comboBoxECC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxECC.FormattingEnabled = true;
            this.comboBoxECC.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.comboBoxECC.Location = new System.Drawing.Point(99, 46);
            this.comboBoxECC.Name = "comboBoxECC";
            this.comboBoxECC.Size = new System.Drawing.Size(125, 20);
            this.comboBoxECC.TabIndex = 14;
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(42, 72);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Size = new System.Drawing.Size(462, 367);
            this.pictureBoxQRCode.TabIndex = 13;
            this.pictureBoxQRCode.TabStop = false;
            // 
            // textBoxQRCode
            // 
            this.textBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQRCode.Location = new System.Drawing.Point(42, 22);
            this.textBoxQRCode.Name = "textBoxQRCode";
            this.textBoxQRCode.Size = new System.Drawing.Size(394, 21);
            this.textBoxQRCode.TabIndex = 12;
            this.textBoxQRCode.Text = "1234567890";
            this.textBoxQRCode.TextChanged += new System.EventHandler(this.textBoxQRCode_TextChanged);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerate.Location = new System.Drawing.Point(442, 22);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(62, 21);
            this.buttonGenerate.TabIndex = 11;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // FrmQr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 521);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.iconSize);
            this.Controls.Add(this.labelIconsize);
            this.Controls.Add(this.selectIconBtn);
            this.Controls.Add(this.iconPath);
            this.Controls.Add(this.labelIcon);
            this.Controls.Add(this.labelECC);
            this.Controls.Add(this.comboBoxECC);
            this.Controls.Add(this.pictureBoxQRCode);
            this.Controls.Add(this.textBoxQRCode);
            this.Controls.Add(this.buttonGenerate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQr";
            this.Text = "QRDemo";
            this.Load += new System.EventHandler(this.FrmQr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.NumericUpDown iconSize;
        private System.Windows.Forms.Label labelIconsize;
        private System.Windows.Forms.Button selectIconBtn;
        private System.Windows.Forms.TextBox iconPath;
        private System.Windows.Forms.Label labelIcon;
        private System.Windows.Forms.Label labelECC;
        private System.Windows.Forms.ComboBox comboBoxECC;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.TextBox textBoxQRCode;
        private System.Windows.Forms.Button buttonGenerate;
    }
}

