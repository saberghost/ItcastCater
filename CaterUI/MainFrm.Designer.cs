namespace CaterUI
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMember = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDish = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabHallInfo = new System.Windows.Forms.TabControl();
            this.ilTableInfo = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::CaterUI.Properties.Resources.menuBg;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManager,
            this.menuMember,
            this.menuTable,
            this.menuDish,
            this.menuOrder,
            this.menuQuit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(520, 72);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuManager
            // 
            this.menuManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuManager.Image = global::CaterUI.Properties.Resources.menuManager;
            this.menuManager.Name = "menuManager";
            this.menuManager.Size = new System.Drawing.Size(76, 68);
            this.menuManager.Text = "toolStripMenuItem1";
            this.menuManager.Click += new System.EventHandler(this.menuManager_Click);
            // 
            // menuMember
            // 
            this.menuMember.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuMember.Image = global::CaterUI.Properties.Resources.menuMember;
            this.menuMember.Name = "menuMember";
            this.menuMember.Size = new System.Drawing.Size(76, 68);
            this.menuMember.Text = "toolStripMenuItem2";
            this.menuMember.Click += new System.EventHandler(this.menuMember_Click);
            // 
            // menuTable
            // 
            this.menuTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuTable.Image = global::CaterUI.Properties.Resources.menuTable;
            this.menuTable.Name = "menuTable";
            this.menuTable.Size = new System.Drawing.Size(76, 68);
            this.menuTable.Text = "toolStripMenuItem3";
            this.menuTable.Click += new System.EventHandler(this.menuTable_Click);
            // 
            // menuDish
            // 
            this.menuDish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuDish.Image = global::CaterUI.Properties.Resources.menuDish;
            this.menuDish.Name = "menuDish";
            this.menuDish.Size = new System.Drawing.Size(76, 68);
            this.menuDish.Text = "toolStripMenuItem4";
            this.menuDish.Click += new System.EventHandler(this.menuDish_Click);
            // 
            // menuOrder
            // 
            this.menuOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuOrder.Image = global::CaterUI.Properties.Resources.menuOrder;
            this.menuOrder.Name = "menuOrder";
            this.menuOrder.Size = new System.Drawing.Size(76, 68);
            this.menuOrder.Text = "toolStripMenuItem5";
            this.menuOrder.Click += new System.EventHandler(this.menuOrder_Click);
            // 
            // menuQuit
            // 
            this.menuQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuQuit.Image = global::CaterUI.Properties.Resources.menuQuit;
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(76, 68);
            this.menuQuit.Text = "toolStripMenuItem6";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // tabHallInfo
            // 
            this.tabHallInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHallInfo.Location = new System.Drawing.Point(0, 72);
            this.tabHallInfo.Name = "tabHallInfo";
            this.tabHallInfo.SelectedIndex = 0;
            this.tabHallInfo.Size = new System.Drawing.Size(520, 302);
            this.tabHallInfo.TabIndex = 1;
            // 
            // ilTableInfo
            // 
            this.ilTableInfo.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTableInfo.ImageStream")));
            this.ilTableInfo.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTableInfo.Images.SetKeyName(0, "desk1.png");
            this.ilTableInfo.Images.SetKeyName(1, "desk2.png");
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 374);
            this.Controls.Add(this.tabHallInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrm";
            this.Text = "MainFrm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuManager;
        private System.Windows.Forms.ToolStripMenuItem menuMember;
        private System.Windows.Forms.ToolStripMenuItem menuTable;
        private System.Windows.Forms.ToolStripMenuItem menuDish;
        private System.Windows.Forms.ToolStripMenuItem menuOrder;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
        private System.Windows.Forms.TabControl tabHallInfo;
        private System.Windows.Forms.ImageList ilTableInfo;
    }
}