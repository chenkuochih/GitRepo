namespace WindowsFormsQr
{
    partial class CQY
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.message_label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.message_lable = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.qrCodeGraphicControl = new Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeGraphicControl();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsTab = true;
            this.textBox1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(35, 163);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(392, 118);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(31, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "请输入30个以内的字符构建二维码：";
            // 
            // message_label
            // 
            this.message_label.AutoSize = true;
            this.message_label.Location = new System.Drawing.Point(13, 366);
            this.message_label.Name = "message_label";
            this.message_label.Size = new System.Drawing.Size(0, 15);
            this.message_label.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(342, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // message_lable
            // 
            this.message_lable.AutoSize = true;
            this.message_lable.ForeColor = System.Drawing.Color.Red;
            this.message_lable.Location = new System.Drawing.Point(32, 324);
            this.message_lable.Name = "message_lable";
            this.message_lable.Size = new System.Drawing.Size(55, 15);
            this.message_lable.TabIndex = 4;
            this.message_lable.Text = "      ";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // qrCodeGraphicControl
            // 
            this.qrCodeGraphicControl.ErrorCorrectLevel = Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.M;
            this.qrCodeGraphicControl.Location = new System.Drawing.Point(468, 75);
            this.qrCodeGraphicControl.Name = "qrCodeGraphicControl";
            this.qrCodeGraphicControl.QuietZoneModule = Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Two;
            this.qrCodeGraphicControl.Size = new System.Drawing.Size(296, 291);
            this.qrCodeGraphicControl.TabIndex = 6;
            this.qrCodeGraphicControl.Text = "春力是猪";
            // 
            // CQY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.qrCodeGraphicControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.message_lable);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.message_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "CQY";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label message_label;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label message_lable;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeGraphicControl qrCodeGraphicControl;
    }
}

