namespace SSIT.QualityManage.UI
{
    partial class SelectSupplierForm
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
            this.flvSupplier = new SSITControls.FilterListView.FilterListView();
            this.rb_ok = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.rb_ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // flvSupplier
            // 
            this.flvSupplier.AllowFilter = false;
            this.flvSupplier.AllowSearchSub = false;
            this.flvSupplier.AllowSort = true;
            this.flvSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flvSupplier.DataMember = "";
            this.flvSupplier.DataSource = null;
            this.flvSupplier.DisplayMember = "";
            this.flvSupplier.FillDataState = true;
            this.flvSupplier.ImageList = null;
            this.flvSupplier.Location = new System.Drawing.Point(-4, 0);
            this.flvSupplier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flvSupplier.MultiSelect = false;
            this.flvSupplier.Name = "flvSupplier";
            this.flvSupplier.SelectedIndex = -1;
            this.flvSupplier.SelectedItem = null;
            this.flvSupplier.ShowCheckBoxes = false;
            this.flvSupplier.Size = new System.Drawing.Size(567, 454);
            this.flvSupplier.TabIndex = 0;
            // 
            // rb_ok
            // 
            this.rb_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rb_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.rb_ok.Image = global::SSIT.QualityManage.Properties.Resources.OK;
            this.rb_ok.Location = new System.Drawing.Point(626, 247);
            this.rb_ok.Name = "rb_ok";
            this.rb_ok.Size = new System.Drawing.Size(137, 48);
            this.rb_ok.TabIndex = 1;
            this.rb_ok.Text = "确定";
            this.rb_ok.Click += new System.EventHandler(this.rb_ok_Click);
            // 
            // radButton1
            // 
            this.radButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.radButton1.Image = global::SSIT.QualityManage.Properties.Resources.cancel1;
            this.radButton1.Location = new System.Drawing.Point(626, 335);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(137, 48);
            this.radButton1.TabIndex = 1;
            this.radButton1.Text = "取消";
            // 
            // SelectSupplierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.rb_ok);
            this.Controls.Add(this.flvSupplier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SelectSupplierForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "供应商选择";
            ((System.ComponentModel.ISupportInitialize)(this.rb_ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SSITControls.FilterListView.FilterListView flvSupplier;
        private Telerik.WinControls.UI.RadButton rb_ok;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}