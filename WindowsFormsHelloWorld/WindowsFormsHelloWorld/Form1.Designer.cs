namespace WindowsFormsHelloWorld
{
    partial class Form1
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
            this.English_button = new System.Windows.Forms.Button();
            this.Chinese_button = new System.Windows.Forms.Button();
            this.display = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // English_button
            // 
            this.English_button.Location = new System.Drawing.Point(368, 341);
            this.English_button.Name = "English_button";
            this.English_button.Size = new System.Drawing.Size(75, 23);
            this.English_button.TabIndex = 0;
            this.English_button.Text = "English";
            this.English_button.UseVisualStyleBackColor = true;
            this.English_button.Click += new System.EventHandler(this.English_button_Click);
            // 
            // Chinese_button
            // 
            this.Chinese_button.Location = new System.Drawing.Point(368, 272);
            this.Chinese_button.Name = "Chinese_button";
            this.Chinese_button.Size = new System.Drawing.Size(75, 23);
            this.Chinese_button.TabIndex = 1;
            this.Chinese_button.Text = "Chinese";
            this.Chinese_button.UseVisualStyleBackColor = true;
            this.Chinese_button.Click += new System.EventHandler(this.Chinese_button_Click);
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(356, 169);
            this.display.Name = "display";
            this.display.ReadOnly = true;
            this.display.Size = new System.Drawing.Size(100, 25);
            this.display.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.display);
            this.Controls.Add(this.Chinese_button);
            this.Controls.Add(this.English_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button English_button;
        private System.Windows.Forms.Button Chinese_button;
        private System.Windows.Forms.TextBox display;
    }
}

