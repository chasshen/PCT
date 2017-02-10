namespace PCT
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.传感器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOHeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOCH4C2H2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.o2CO2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.箱压PBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.口压PMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.环境参数AmbientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.传感器ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1076, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 传感器ToolStripMenuItem
            // 
            this.传感器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cOHeToolStripMenuItem,
            this.cOCH4C2H2ToolStripMenuItem,
            this.o2CO2ToolStripMenuItem,
            this.箱压PBToolStripMenuItem,
            this.口压PMToolStripMenuItem,
            this.环境参数AmbientToolStripMenuItem});
            this.传感器ToolStripMenuItem.Name = "传感器ToolStripMenuItem";
            this.传感器ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.传感器ToolStripMenuItem.Text = "传感器";
            // 
            // cOHeToolStripMenuItem
            // 
            this.cOHeToolStripMenuItem.Name = "cOHeToolStripMenuItem";
            this.cOHeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.cOHeToolStripMenuItem.Text = "CO/He";
            // 
            // cOCH4C2H2ToolStripMenuItem
            // 
            this.cOCH4C2H2ToolStripMenuItem.Name = "cOCH4C2H2ToolStripMenuItem";
            this.cOCH4C2H2ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.cOCH4C2H2ToolStripMenuItem.Text = "CO/CH4/C2H2";
            // 
            // o2CO2ToolStripMenuItem
            // 
            this.o2CO2ToolStripMenuItem.Name = "o2CO2ToolStripMenuItem";
            this.o2CO2ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.o2CO2ToolStripMenuItem.Text = "O2/CO2";
            // 
            // 箱压PBToolStripMenuItem
            // 
            this.箱压PBToolStripMenuItem.Name = "箱压PBToolStripMenuItem";
            this.箱压PBToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.箱压PBToolStripMenuItem.Text = "箱压PB";
            // 
            // 口压PMToolStripMenuItem
            // 
            this.口压PMToolStripMenuItem.Name = "口压PMToolStripMenuItem";
            this.口压PMToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.口压PMToolStripMenuItem.Text = "口压PM";
            // 
            // 环境参数AmbientToolStripMenuItem
            // 
            this.环境参数AmbientToolStripMenuItem.Name = "环境参数AmbientToolStripMenuItem";
            this.环境参数AmbientToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.环境参数AmbientToolStripMenuItem.Text = "环境参数Ambient";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 262);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "PowerCube-Tools";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 传感器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOHeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOCH4C2H2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem o2CO2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 箱压PBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 口压PMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 环境参数AmbientToolStripMenuItem;
    }
}

