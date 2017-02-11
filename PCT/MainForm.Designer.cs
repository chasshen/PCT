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
            this.menu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu11 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu12 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu13 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu14 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu15 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu16 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnBtns = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAddMore = new System.Windows.Forms.Button();
            this.btnToZero = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.pnBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1076, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu1
            // 
            this.menu1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu11,
            this.menu12,
            this.menu13,
            this.menu14,
            this.menu15,
            this.menu16});
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(56, 21);
            this.menu1.Text = "传感器";
            // 
            // menu11
            // 
            this.menu11.Name = "menu11";
            this.menu11.Size = new System.Drawing.Size(172, 22);
            this.menu11.Text = "CO/He";
            // 
            // menu12
            // 
            this.menu12.Name = "menu12";
            this.menu12.Size = new System.Drawing.Size(172, 22);
            this.menu12.Text = "CO/CH4/C2H2";
            // 
            // menu13
            // 
            this.menu13.Name = "menu13";
            this.menu13.Size = new System.Drawing.Size(172, 22);
            this.menu13.Text = "O2/CO2";
            // 
            // menu14
            // 
            this.menu14.Name = "menu14";
            this.menu14.Size = new System.Drawing.Size(172, 22);
            this.menu14.Text = "箱压PB";
            // 
            // menu15
            // 
            this.menu15.Name = "menu15";
            this.menu15.Size = new System.Drawing.Size(172, 22);
            this.menu15.Text = "口压PM";
            // 
            // menu16
            // 
            this.menu16.Name = "menu16";
            this.menu16.Size = new System.Drawing.Size(172, 22);
            this.menu16.Text = "环境参数Ambient";
            this.menu16.Click += new System.EventHandler(this.menu16_Click);
            // 
            // pnBtns
            // 
            this.pnBtns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnBtns.Controls.Add(this.btnExit);
            this.pnBtns.Controls.Add(this.btnStop);
            this.pnBtns.Controls.Add(this.btnAddMore);
            this.pnBtns.Controls.Add(this.btnToZero);
            this.pnBtns.Controls.Add(this.btnStart);
            this.pnBtns.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnBtns.Location = new System.Drawing.Point(960, 25);
            this.pnBtns.Name = "pnBtns";
            this.pnBtns.Size = new System.Drawing.Size(116, 352);
            this.pnBtns.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(19, 291);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 48);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(19, 216);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 48);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnAddMore
            // 
            this.btnAddMore.Location = new System.Drawing.Point(19, 148);
            this.btnAddMore.Name = "btnAddMore";
            this.btnAddMore.Size = new System.Drawing.Size(75, 48);
            this.btnAddMore.TabIndex = 2;
            this.btnAddMore.Text = "增益";
            this.btnAddMore.UseVisualStyleBackColor = true;
            // 
            // btnToZero
            // 
            this.btnToZero.Location = new System.Drawing.Point(19, 81);
            this.btnToZero.Name = "btnToZero";
            this.btnToZero.Size = new System.Drawing.Size(75, 48);
            this.btnToZero.TabIndex = 1;
            this.btnToZero.Text = "校零";
            this.btnToZero.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(19, 15);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 48);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // pnInfo
            // 
            this.pnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInfo.Location = new System.Drawing.Point(0, 25);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(960, 352);
            this.pnInfo.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 377);
            this.Controls.Add(this.pnInfo);
            this.Controls.Add(this.pnBtns);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "PowerCube-Tools";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnBtns.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu1;
        private System.Windows.Forms.ToolStripMenuItem menu11;
        private System.Windows.Forms.ToolStripMenuItem menu12;
        private System.Windows.Forms.ToolStripMenuItem menu13;
        private System.Windows.Forms.ToolStripMenuItem menu14;
        private System.Windows.Forms.ToolStripMenuItem menu15;
        private System.Windows.Forms.ToolStripMenuItem menu16;
        private System.Windows.Forms.Panel pnBtns;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAddMore;
        private System.Windows.Forms.Button btnToZero;
    }
}

