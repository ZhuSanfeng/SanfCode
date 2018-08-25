namespace SSIT.QualityManage.UI
{
    partial class NewDefInOrderForm
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.rtbCheckLot = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
            this.rtbPurOrderID = new Telerik.WinControls.UI.RadTextBox();
            this.rb_Cancel = new Telerik.WinControls.UI.RadButton();
            this.rt_Ok = new Telerik.WinControls.UI.RadButton();
            this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
            this.rtbNote = new Telerik.WinControls.UI.RadTextBox();
            this.rtbCarID = new Telerik.WinControls.UI.RadTextBox();
            this.rtbCount = new Telerik.WinControls.UI.RadTextBox();
            this.rtbLot = new Telerik.WinControls.UI.RadTextBox();
            this.stbSupplier = new SSITControls.SelectTextBox.SelectTextbox();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.DefPK = new SSIT.MM.TxtSelectDefinition();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtbCheckLot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbPurOrderID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rb_Cancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rt_Ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbCarID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbLot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.DefPK);
            this.radPanel1.Controls.Add(this.rtbCheckLot);
            this.radPanel1.Controls.Add(this.radLabel8);
            this.radPanel1.Controls.Add(this.rtbPurOrderID);
            this.radPanel1.Controls.Add(this.rb_Cancel);
            this.radPanel1.Controls.Add(this.rt_Ok);
            this.radPanel1.Controls.Add(this.radLabel7);
            this.radPanel1.Controls.Add(this.rtbNote);
            this.radPanel1.Controls.Add(this.rtbCarID);
            this.radPanel1.Controls.Add(this.rtbCount);
            this.radPanel1.Controls.Add(this.rtbLot);
            this.radPanel1.Controls.Add(this.stbSupplier);
            this.radPanel1.Controls.Add(this.radLabel6);
            this.radPanel1.Controls.Add(this.radLabel5);
            this.radPanel1.Controls.Add(this.radLabel4);
            this.radPanel1.Controls.Add(this.radLabel3);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(806, 447);
            this.radPanel1.TabIndex = 0;
            // 
            // rtbCheckLot
            // 
            this.rtbCheckLot.Location = new System.Drawing.Point(169, 162);
            this.rtbCheckLot.Name = "rtbCheckLot";
            this.rtbCheckLot.Size = new System.Drawing.Size(240, 24);
            this.rtbCheckLot.TabIndex = 14;
            // 
            // radLabel8
            // 
            this.radLabel8.Location = new System.Drawing.Point(67, 164);
            this.radLabel8.Name = "radLabel8";
            this.radLabel8.Size = new System.Drawing.Size(86, 22);
            this.radLabel8.TabIndex = 13;
            this.radLabel8.Text = "检  验  批 ：";
            // 
            // rtbPurOrderID
            // 
            this.rtbPurOrderID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbPurOrderID.Location = new System.Drawing.Point(169, 26);
            this.rtbPurOrderID.Name = "rtbPurOrderID";
            this.rtbPurOrderID.Size = new System.Drawing.Size(240, 24);
            this.rtbPurOrderID.TabIndex = 12;
            // 
            // rb_Cancel
            // 
            this.rb_Cancel.Image = global::SSIT.QualityManage.Properties.Resources.cancel1;
            this.rb_Cancel.Location = new System.Drawing.Point(430, 361);
            this.rb_Cancel.Name = "rb_Cancel";
            this.rb_Cancel.Size = new System.Drawing.Size(137, 43);
            this.rb_Cancel.TabIndex = 11;
            this.rb_Cancel.Text = "取消";
            this.rb_Cancel.Click += new System.EventHandler(this.rb_Cancel_Click);
            // 
            // rt_Ok
            // 
            this.rt_Ok.Image = global::SSIT.QualityManage.Properties.Resources.Save;
            this.rt_Ok.Location = new System.Drawing.Point(195, 361);
            this.rt_Ok.Name = "rt_Ok";
            this.rt_Ok.Size = new System.Drawing.Size(137, 43);
            this.rt_Ok.TabIndex = 11;
            this.rt_Ok.Text = "保存";
            this.rt_Ok.Click += new System.EventHandler(this.rt_Ok_Click);
            // 
            // radLabel7
            // 
            this.radLabel7.Location = new System.Drawing.Point(67, 213);
            this.radLabel7.Name = "radLabel7";
            this.radLabel7.Size = new System.Drawing.Size(86, 22);
            this.radLabel7.TabIndex = 10;
            this.radLabel7.Text = "备         注：";
            // 
            // rtbNote
            // 
            this.rtbNote.AcceptsReturn = true;
            this.rtbNote.AcceptsTab = true;
            this.rtbNote.AutoSize = false;
            this.rtbNote.Location = new System.Drawing.Point(169, 213);
            this.rtbNote.Multiline = true;
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.Size = new System.Drawing.Size(603, 83);
            this.rtbNote.TabIndex = 9;
            // 
            // rtbCarID
            // 
            this.rtbCarID.Location = new System.Drawing.Point(532, 162);
            this.rtbCarID.Name = "rtbCarID";
            this.rtbCarID.Size = new System.Drawing.Size(240, 24);
            this.rtbCarID.TabIndex = 8;
            // 
            // rtbCount
            // 
            this.rtbCount.Location = new System.Drawing.Point(532, 118);
            this.rtbCount.Name = "rtbCount";
            this.rtbCount.Size = new System.Drawing.Size(240, 24);
            this.rtbCount.TabIndex = 8;
            // 
            // rtbLot
            // 
            this.rtbLot.Location = new System.Drawing.Point(169, 118);
            this.rtbLot.Name = "rtbLot";
            this.rtbLot.Size = new System.Drawing.Size(240, 24);
            this.rtbLot.TabIndex = 8;
            // 
            // stbSupplier
            // 
            this.stbSupplier.AllowClear = false;
            this.stbSupplier.ButtonFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbSupplier.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stbSupplier.Location = new System.Drawing.Point(532, 63);
            this.stbSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.stbSupplier.Name = "stbSupplier";
            this.stbSupplier.NullText = "未选择";
            this.stbSupplier.Padding = new System.Windows.Forms.Padding(1);
            this.stbSupplier.ReadOnly = false;
            this.stbSupplier.SelectText = "选择";
            this.stbSupplier.Size = new System.Drawing.Size(240, 29);
            this.stbSupplier.TabIndex = 7;
            this.stbSupplier.TextBoxFont = new System.Drawing.Font("Segoe UI", 8.25F);
            this.stbSupplier.Value = "";
            this.stbSupplier.Click += new System.EventHandler(this.stbSupplier_Click);
            // 
            // radLabel6
            // 
            this.radLabel6.Location = new System.Drawing.Point(436, 70);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(86, 22);
            this.radLabel6.TabIndex = 5;
            this.radLabel6.Text = "供  货  商 ：";
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(436, 164);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(90, 22);
            this.radLabel5.TabIndex = 4;
            this.radLabel5.Text = "车          号：";
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(436, 120);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(90, 22);
            this.radLabel4.TabIndex = 3;
            this.radLabel4.Text = "数          量：";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(67, 120);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(86, 22);
            this.radLabel3.TabIndex = 2;
            this.radLabel3.Text = "批         次：";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(67, 70);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(90, 22);
            this.radLabel2.TabIndex = 1;
            this.radLabel2.Text = "物          料：";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(67, 28);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(96, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "采购订单号：";
            // 
            // DefPK
            // 
            this.DefPK.AllowClear = false;
            this.DefPK.ButtonFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DefPK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DefPK.Location = new System.Drawing.Point(169, 70);
            this.DefPK.Name = "DefPK";
            this.DefPK.NullText = "未选择";
            this.DefPK.Padding = new System.Windows.Forms.Padding(1);
            this.DefPK.ReadOnly = false;
            this.DefPK.SelectText = "选择";
            this.DefPK.Size = new System.Drawing.Size(240, 29);
            this.DefPK.TabIndex = 15;
            this.DefPK.TextBoxFont = new System.Drawing.Font("Segoe UI", 8.25F);
            this.DefPK.Value = "";
            // 
            // NewDefInOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 447);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewDefInOrderForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建到货单";
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtbCheckLot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbPurOrderID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rb_Cancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rt_Ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbCarID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbLot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel7;
        private Telerik.WinControls.UI.RadTextBox rtbNote;
        private Telerik.WinControls.UI.RadTextBox rtbCarID;
        private Telerik.WinControls.UI.RadTextBox rtbCount;
        private Telerik.WinControls.UI.RadTextBox rtbLot;
        private SSITControls.SelectTextBox.SelectTextbox stbSupplier;
        private Telerik.WinControls.UI.RadButton rb_Cancel;
        private Telerik.WinControls.UI.RadButton rt_Ok;
        private Telerik.WinControls.UI.RadTextBox rtbPurOrderID;
        private Telerik.WinControls.UI.RadTextBox rtbCheckLot;
        private Telerik.WinControls.UI.RadLabel radLabel8;
        private MM.TxtSelectDefinition DefPK;
    }
}