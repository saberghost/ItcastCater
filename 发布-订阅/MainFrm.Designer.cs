namespace 发布_订阅
{
    partial class MainFrm
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
            this.btnPub = new System.Windows.Forms.Button();
            this.btnSub1 = new System.Windows.Forms.Button();
            this.btnSub2 = new System.Windows.Forms.Button();
            this.btnSub3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPub
            // 
            this.btnPub.Location = new System.Drawing.Point(197, 12);
            this.btnPub.Name = "btnPub";
            this.btnPub.Size = new System.Drawing.Size(75, 23);
            this.btnPub.TabIndex = 0;
            this.btnPub.Text = "发布者";
            this.btnPub.UseVisualStyleBackColor = true;
            this.btnPub.Click += new System.EventHandler(this.btnPub_Click);
            // 
            // btnSub1
            // 
            this.btnSub1.Location = new System.Drawing.Point(12, 12);
            this.btnSub1.Name = "btnSub1";
            this.btnSub1.Size = new System.Drawing.Size(75, 23);
            this.btnSub1.TabIndex = 1;
            this.btnSub1.Text = "订阅者1";
            this.btnSub1.UseVisualStyleBackColor = true;
            this.btnSub1.Click += new System.EventHandler(this.btnSub1_Click);
            // 
            // btnSub2
            // 
            this.btnSub2.Location = new System.Drawing.Point(12, 41);
            this.btnSub2.Name = "btnSub2";
            this.btnSub2.Size = new System.Drawing.Size(75, 23);
            this.btnSub2.TabIndex = 2;
            this.btnSub2.Text = "订阅者2";
            this.btnSub2.UseVisualStyleBackColor = true;
            this.btnSub2.Click += new System.EventHandler(this.btnSub2_Click);
            // 
            // btnSub3
            // 
            this.btnSub3.Location = new System.Drawing.Point(12, 70);
            this.btnSub3.Name = "btnSub3";
            this.btnSub3.Size = new System.Drawing.Size(75, 23);
            this.btnSub3.TabIndex = 3;
            this.btnSub3.Text = "订阅者3";
            this.btnSub3.UseVisualStyleBackColor = true;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 156);
            this.Controls.Add(this.btnSub3);
            this.Controls.Add(this.btnSub2);
            this.Controls.Add(this.btnSub1);
            this.Controls.Add(this.btnPub);
            this.Name = "MainFrm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPub;
        private System.Windows.Forms.Button btnSub1;
        private System.Windows.Forms.Button btnSub2;
        private System.Windows.Forms.Button btnSub3;
    }
}

