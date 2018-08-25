namespace SSIT.QM.CheckManager.DatasForms
{
    partial class CheckDataCopyForm
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
            this.radDock1 = new Telerik.WinControls.UI.Docking.RadDock();
            this.twInput = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.rpvCheckCategory = new Telerik.WinControls.UI.RadPageView();
            this.toolTabStrip2 = new Telerik.WinControls.UI.Docking.ToolTabStrip();
            this.twSampleOrder = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.rbtOK = new Telerik.WinControls.UI.RadButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.documentContainer1 = new Telerik.WinControls.UI.Docking.DocumentContainer();
            this.documentTabStrip1 = new Telerik.WinControls.UI.Docking.DocumentTabStrip();
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).BeginInit();
            this.radDock1.SuspendLayout();
            this.twInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rpvCheckCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip2)).BeginInit();
            this.toolTabStrip2.SuspendLayout();
            this.twSampleOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).BeginInit();
            this.documentContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).BeginInit();
            this.documentTabStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radDock1
            // 
            this.radDock1.ActiveWindow = this.twSampleOrder;
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
            this.radDock1.Size = new System.Drawing.Size(992, 570);
            this.radDock1.TabIndex = 0;
            this.radDock1.TabStop = false;
            this.radDock1.Text = "radDock1";
            // 
            // twInput
            // 
            this.twInput.Caption = null;
            this.twInput.Controls.Add(this.rpvCheckCategory);
            this.twInput.Font = new System.Drawing.Font("宋体", 9F);
            this.twInput.Location = new System.Drawing.Point(6, 6);
            this.twInput.Name = "twInput";
            this.twInput.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.twInput.Size = new System.Drawing.Size(970, 441);
            this.twInput.Text = "toolWindow1";
            // 
            // rpvCheckCategory
            // 
            this.rpvCheckCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvCheckCategory.Location = new System.Drawing.Point(0, 0);
            this.rpvCheckCategory.Name = "rpvCheckCategory";
            this.rpvCheckCategory.Size = new System.Drawing.Size(970, 441);
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
            this.twSampleOrder.Controls.Add(this.rbtOK);
            this.twSampleOrder.Controls.Add(this.panel1);
            this.twSampleOrder.Controls.Add(this.label2);
            this.twSampleOrder.Font = new System.Drawing.Font("宋体", 9F);
            this.twSampleOrder.Location = new System.Drawing.Point(1, 24);
            this.twSampleOrder.Name = "twSampleOrder";
            this.twSampleOrder.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.twSampleOrder.Size = new System.Drawing.Size(980, 77);
            this.twSampleOrder.Text = "选择批次";
            this.twSampleOrder.ToolCaptionButtons = Telerik.WinControls.UI.Docking.ToolStripCaptionButtons.AutoHide;
            // 
            // rbtOK
            // 
            this.rbtOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.rbtOK.Image = global::SSIT.QM.Properties.Resources.export48;
            this.rbtOK.Location = new System.Drawing.Point(849, 14);
            this.rbtOK.Name = "rbtOK";
            this.rbtOK.Size = new System.Drawing.Size(120, 51);
            this.rbtOK.TabIndex = 23;
            this.rbtOK.Text = "导入";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(833, 83);
            this.panel1.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(673, -435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
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
            this.documentTabStrip1.Size = new System.Drawing.Size(982, 453);
            this.documentTabStrip1.TabIndex = 0;
            this.documentTabStrip1.TabStop = false;
            this.documentTabStrip1.TabStripVisible = false;
            // 
            // SampleDataCopyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 570);
            this.Controls.Add(this.radDock1);
            this.Name = "SampleDataCopyForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "同生产日期批次检测数据导入";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).EndInit();
            this.radDock1.ResumeLayout(false);
            this.twInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rpvCheckCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip2)).EndInit();
            this.toolTabStrip2.ResumeLayout(false);
            this.twSampleOrder.ResumeLayout(false);
            this.twSampleOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtOK)).EndInit();
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
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton rbtOK;
    }
}
