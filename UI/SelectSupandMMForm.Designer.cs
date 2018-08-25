namespace SSIT.QualityManage.UI
{
    partial class SelectSupandMMForm
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
            this.rbtCancel = new Telerik.WinControls.UI.RadButton();
            this.rbtOK = new Telerik.WinControls.UI.RadButton();
            this.rbSelectNone = new Telerik.WinControls.UI.RadButton();
            this.rbSelectAll = new Telerik.WinControls.UI.RadButton();
            this.group = new Telerik.WinControls.UI.RadGroupBox();
            this.rlvMM = new SSITControls.FilterListView.FilterListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.radGroupBox3 = new Telerik.WinControls.UI.RadGroupBox();
            this.rlcSup = new Telerik.WinControls.UI.RadListControl();
            ((System.ComponentModel.ISupportInitialize)(this.rbtCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbSelectNone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbSelectAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.group)).BeginInit();
            this.group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).BeginInit();
            this.radGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlcSup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rbtCancel
            // 
            this.rbtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rbtCancel.Location = new System.Drawing.Point(1016, 159);
            this.rbtCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtCancel.Name = "rbtCancel";
            this.rbtCancel.Size = new System.Drawing.Size(147, 45);
            this.rbtCancel.TabIndex = 16;
            this.rbtCancel.Text = "取消";
            this.rbtCancel.Click += new System.EventHandler(this.rbtCancel_Click);
            // 
            // rbtOK
            // 
            this.rbtOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.rbtOK.Enabled = false;
            this.rbtOK.Location = new System.Drawing.Point(1016, 51);
            this.rbtOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtOK.Name = "rbtOK";
            this.rbtOK.Size = new System.Drawing.Size(147, 50);
            this.rbtOK.TabIndex = 15;
            this.rbtOK.Text = "确定";
            this.rbtOK.Click += new System.EventHandler(this.rbtOK_Click);
            // 
            // rbSelectNone
            // 
            this.rbSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbSelectNone.Location = new System.Drawing.Point(855, 555);
            this.rbSelectNone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbSelectNone.Name = "rbSelectNone";
            this.rbSelectNone.Size = new System.Drawing.Size(147, 30);
            this.rbSelectNone.TabIndex = 19;
            this.rbSelectNone.Text = "当前页全不选";
            this.rbSelectNone.Click += new System.EventHandler(this.rbSelectNone_Click);
            // 
            // rbSelectAll
            // 
            this.rbSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbSelectAll.Location = new System.Drawing.Point(679, 555);
            this.rbSelectAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbSelectAll.Name = "rbSelectAll";
            this.rbSelectAll.Size = new System.Drawing.Size(147, 30);
            this.rbSelectAll.TabIndex = 18;
            this.rbSelectAll.Text = "当前页全选";
            this.rbSelectAll.Click += new System.EventHandler(this.rbSelectAll_Click);
            // 
            // group
            // 
            this.group.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.group.Controls.Add(this.rlvMM);
            this.group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group.HeaderText = "物料";
            this.group.Location = new System.Drawing.Point(0, 0);
            this.group.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.group.Name = "group";
            this.group.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            this.group.Size = new System.Drawing.Size(661, 546);
            this.group.TabIndex = 22;
            this.group.Text = "物料";
            // 
            // rlvMM
            // 
            this.rlvMM.AllowFilter = true;
            this.rlvMM.AllowSearchSub = false;
            this.rlvMM.AllowSort = true;
            this.rlvMM.DataMember = "";
            this.rlvMM.DataSource = null;
            this.rlvMM.DisplayMember = "";
            this.rlvMM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlvMM.FillDataState = true;
            this.rlvMM.ImageList = null;
            this.rlvMM.Location = new System.Drawing.Point(3, 22);
            this.rlvMM.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.rlvMM.MultiSelect = false;
            this.rlvMM.Name = "rlvMM";
            this.rlvMM.SelectedIndex = -1;
            this.rlvMM.SelectedItem = null;
            this.rlvMM.ShowCheckBoxes = false;
            this.rlvMM.Size = new System.Drawing.Size(655, 522);
            this.rlvMM.TabIndex = 1;
            this.rlvMM.SelectedIndexChanged += new System.EventHandler(this.rlvMM_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.radGroupBox3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.group);
            this.splitContainer1.Size = new System.Drawing.Size(1007, 546);
            this.splitContainer1.SplitterDistance = 341;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 23;
            // 
            // radGroupBox3
            // 
            this.radGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox3.Controls.Add(this.rlcSup);
            this.radGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.radGroupBox3.HeaderText = "企业名称";
            this.radGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.radGroupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radGroupBox3.Name = "radGroupBox3";
            this.radGroupBox3.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            this.radGroupBox3.Size = new System.Drawing.Size(341, 546);
            this.radGroupBox3.TabIndex = 21;
            this.radGroupBox3.Text = "企业名称";
            // 
            // rlcSup
            // 
            this.rlcSup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlcSup.Location = new System.Drawing.Point(3, 22);
            this.rlcSup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rlcSup.Name = "rlcSup";
            this.rlcSup.Size = new System.Drawing.Size(335, 522);
            this.rlcSup.TabIndex = 0;
            this.rlcSup.Text = "radListControl1";
            this.rlcSup.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.rlcMMType_SelectedIndexChanged);
            // 
            // SelectSupandMMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 620);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.rbSelectNone);
            this.Controls.Add(this.rbSelectAll);
            this.Controls.Add(this.rbtCancel);
            this.Controls.Add(this.rbtOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SelectSupandMMForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "选择物料";
            ((System.ComponentModel.ISupportInitialize)(this.rbtCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbSelectNone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbSelectAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.group)).EndInit();
            this.group.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).EndInit();
            this.radGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rlcSup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton rbtCancel;
        private Telerik.WinControls.UI.RadButton rbtOK;
        private Telerik.WinControls.UI.RadButton rbSelectNone;
        private Telerik.WinControls.UI.RadButton rbSelectAll;
        private Telerik.WinControls.UI.RadGroupBox group;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox3;
        private Telerik.WinControls.UI.RadListControl rlcSup;
        private SSITControls.FilterListView.FilterListView rlvMM;
    }
}
