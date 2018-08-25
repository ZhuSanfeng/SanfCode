namespace SSIT.QM.CheckManager.DatasForms
{
    partial class CheckDataInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckDataInputForm));
            this.radDock1 = new Telerik.WinControls.UI.Docking.RadDock();
            this.twInput = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbStat = new System.Windows.Forms.ToolStripButton();
            this.tsbCommit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbApprove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAllPass = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbSampleCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbNote = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripSeparator();
            this.lbInfo = new System.Windows.Forms.ToolStripLabel();
            this.rpvCheckCategory = new Telerik.WinControls.UI.RadPageView();
            this.toolTabStrip2 = new Telerik.WinControls.UI.Docking.ToolTabStrip();
            this.twSampleOrder = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.label2 = new System.Windows.Forms.Label();
            this.documentContainer1 = new Telerik.WinControls.UI.Docking.DocumentContainer();
            this.documentTabStrip1 = new Telerik.WinControls.UI.Docking.DocumentTabStrip();
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).BeginInit();
            this.radDock1.SuspendLayout();
            this.twInput.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rpvCheckCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip2)).BeginInit();
            this.toolTabStrip2.SuspendLayout();
            this.twSampleOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).BeginInit();
            this.documentContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).BeginInit();
            this.documentTabStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radDock1
            // 
            this.radDock1.ActiveWindow = this.twInput;
            this.radDock1.CausesValidation = false;
            this.radDock1.Controls.Add(this.toolTabStrip2);
            this.radDock1.Controls.Add(this.documentContainer1);
            this.radDock1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDock1.IsCleanUpTarget = true;
            this.radDock1.Location = new System.Drawing.Point(0, 0);
            this.radDock1.MainDocumentContainer = this.documentContainer1;
            this.radDock1.Name = "radDock1";
            this.radDock1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radDock1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radDock1.Size = new System.Drawing.Size(992, 726);
            this.radDock1.TabIndex = 0;
            this.radDock1.TabStop = false;
            this.radDock1.Text = "radDock1";
            // 
            // twInput
            // 
            this.twInput.Caption = null;
            this.twInput.Controls.Add(this.flowPanel);
            this.twInput.Controls.Add(this.toolStrip1);
            this.twInput.Controls.Add(this.rpvCheckCategory);
            this.twInput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.twInput.Location = new System.Drawing.Point(6, 6);
            this.twInput.Name = "twInput";
            this.twInput.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.twInput.Size = new System.Drawing.Size(970, 597);
            this.twInput.Text = "toolWindow1";
            // 
            // flowPanel
            // 
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowPanel.Location = new System.Drawing.Point(0, 437);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(970, 160);
            this.flowPanel.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbStat,
            this.tsbCommit,
            this.toolStripSeparator1,
            this.tsbApprove,
            this.toolStripSeparator2,
            this.tsbAllPass,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.tsbSampleCount,
            this.toolStripSeparator4,
            this.toolStripLabel2,
            this.tsbNote,
            this.toolStripLabel3,
            this.lbInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(970, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbStat
            // 
            this.tsbStat.Image = global::SSIT.QM.Properties.Resources.Stat48;
            this.tsbStat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStat.Name = "tsbStat";
            this.tsbStat.Size = new System.Drawing.Size(63, 24);
            this.tsbStat.Text = "统计";
            this.tsbStat.Click += new System.EventHandler(this.tsbStat_Click);
            // 
            // tsbCommit
            // 
            this.tsbCommit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tsbCommit.Enabled = false;
            this.tsbCommit.Image = global::SSIT.QM.Properties.Resources.Complete48;
            this.tsbCommit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCommit.Name = "tsbCommit";
            this.tsbCommit.Size = new System.Drawing.Size(93, 24);
            this.tsbCommit.Text = "提交数据";
            this.tsbCommit.ToolTipText = "提交数据（注：提交后数据不能更改）";
            this.tsbCommit.Click += new System.EventHandler(this.tsbCommit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbApprove
            // 
            this.tsbApprove.BackColor = System.Drawing.Color.LightGreen;
            this.tsbApprove.Enabled = false;
            this.tsbApprove.ForeColor = System.Drawing.Color.Black;
            this.tsbApprove.Image = global::SSIT.QM.Properties.Resources.Approve48;
            this.tsbApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApprove.Name = "tsbApprove";
            this.tsbApprove.Size = new System.Drawing.Size(63, 24);
            this.tsbApprove.Text = "发布";
            this.tsbApprove.ToolTipText = "确定后数据将发布";
            this.tsbApprove.Click += new System.EventHandler(this.tsbApprove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbAllPass
            // 
            this.tsbAllPass.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAllPass.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAllPass.Image = ((System.Drawing.Image)(resources.GetObject("tsbAllPass.Image")));
            this.tsbAllPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAllPass.Name = "tsbAllPass";
            this.tsbAllPass.Size = new System.Drawing.Size(133, 24);
            this.tsbAllPass.Text = "设置判断项全合格";
            this.tsbAllPass.Click += new System.EventHandler(this.tsbAllPass_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(58, 24);
            this.toolStripLabel1.Text = "样品数:";
            // 
            // tsbSampleCount
            // 
            this.tsbSampleCount.Name = "tsbSampleCount";
            this.tsbSampleCount.Size = new System.Drawing.Size(100, 27);
            this.tsbSampleCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsbSampleCount_KeyPress);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 24);
            this.toolStripLabel2.Text = "备注";
            // 
            // tsbNote
            // 
            this.tsbNote.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tsbNote.Name = "tsbNote";
            this.tsbNote.Size = new System.Drawing.Size(200, 27);
            this.tsbNote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsbNote_KeyPress);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(6, 27);
            // 
            // lbInfo
            // 
            this.lbInfo.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(99, 24);
            this.lbInfo.Text = "合格判定信息";
            // 
            // rpvCheckCategory
            // 
            this.rpvCheckCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rpvCheckCategory.Location = new System.Drawing.Point(-3, 23);
            this.rpvCheckCategory.Name = "rpvCheckCategory";
            this.rpvCheckCategory.Size = new System.Drawing.Size(996, 428);
            this.rpvCheckCategory.TabIndex = 0;
            this.rpvCheckCategory.Text = "radPageView1";
            // 
            // toolTabStrip2
            // 
            this.toolTabStrip2.CanUpdateChildIndex = true;
            this.toolTabStrip2.CausesValidation = false;
            this.toolTabStrip2.Controls.Add(this.twSampleOrder);
            this.toolTabStrip2.Location = new System.Drawing.Point(5, 5);
            this.toolTabStrip2.Name = "toolTabStrip2";
            // 
            // 
            // 
            this.toolTabStrip2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.toolTabStrip2.SelectedIndex = 0;
            this.toolTabStrip2.Size = new System.Drawing.Size(982, 103);
            this.toolTabStrip2.SizeInfo.AbsoluteSize = new System.Drawing.Size(315, 103);
            this.toolTabStrip2.SizeInfo.SplitterCorrection = new System.Drawing.Size(115, -97);
            this.toolTabStrip2.TabIndex = 1;
            this.toolTabStrip2.TabStop = false;
            // 
            // twSampleOrder
            // 
            this.twSampleOrder.Caption = null;
            this.twSampleOrder.Controls.Add(this.label2);
            this.twSampleOrder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.twSampleOrder.Location = new System.Drawing.Point(1, 26);
            this.twSampleOrder.Name = "twSampleOrder";
            this.twSampleOrder.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.twSampleOrder.Size = new System.Drawing.Size(980, 75);
            this.twSampleOrder.Text = "检测工单";
            this.twSampleOrder.ToolCaptionButtons = Telerik.WinControls.UI.Docking.ToolStripCaptionButtons.AutoHide;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(673, -435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "工单日期";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // documentContainer1
            // 
            this.documentContainer1.CausesValidation = false;
            this.documentContainer1.Controls.Add(this.documentTabStrip1);
            this.documentContainer1.Name = "documentContainer1";
            // 
            // 
            // 
            this.documentContainer1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.documentContainer1.SizeInfo.AbsoluteSize = new System.Drawing.Size(455, 408);
            this.documentContainer1.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Fill;
            this.documentContainer1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-115, 97);
            this.documentContainer1.TabIndex = 2;
            // 
            // documentTabStrip1
            // 
            this.documentTabStrip1.CanUpdateChildIndex = true;
            this.documentTabStrip1.CausesValidation = false;
            this.documentTabStrip1.Controls.Add(this.twInput);
            this.documentTabStrip1.Location = new System.Drawing.Point(0, 0);
            this.documentTabStrip1.Name = "documentTabStrip1";
            // 
            // 
            // 
            this.documentTabStrip1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.documentTabStrip1.SelectedIndex = 0;
            this.documentTabStrip1.Size = new System.Drawing.Size(982, 609);
            this.documentTabStrip1.TabIndex = 0;
            this.documentTabStrip1.TabStop = false;
            this.documentTabStrip1.TabStripVisible = false;
            // 
            // CheckDataInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 726);
            this.Controls.Add(this.radDock1);
            this.Name = "CheckDataInputForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "检测数据录入";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.SampleDataInputForm_Load);
            this.Shown += new System.EventHandler(this.SampleDataInputForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).EndInit();
            this.radDock1.ResumeLayout(false);
            this.twInput.ResumeLayout(false);
            this.twInput.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rpvCheckCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip2)).EndInit();
            this.toolTabStrip2.ResumeLayout(false);
            this.twSampleOrder.ResumeLayout(false);
            this.twSampleOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).EndInit();
            this.documentContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).EndInit();
            this.documentTabStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.Docking.RadDock radDock1;
        private Telerik.WinControls.UI.Docking.DocumentContainer documentContainer1;
        private Telerik.WinControls.UI.Docking.ToolWindow twSampleOrder;
        private Telerik.WinControls.UI.Docking.DocumentTabStrip documentTabStrip1;
        private Telerik.WinControls.UI.Docking.ToolWindow twInput;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.Docking.ToolTabStrip toolTabStrip2;
        private Telerik.WinControls.UI.RadPageView rpvCheckCategory;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCommit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbApprove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbAllPass;
        private System.Windows.Forms.ToolStripButton tsbStat;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tsbSampleCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tsbNote;
        private System.Windows.Forms.ToolStripSeparator toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel lbInfo;
    }
}
