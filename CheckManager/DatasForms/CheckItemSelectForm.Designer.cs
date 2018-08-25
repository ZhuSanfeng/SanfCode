namespace SSIT.QM.CheckManager.DatasForms
{
    partial class CheckItemSelectForm
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
            this.rbtCancel = new Telerik.WinControls.UI.RadButton();
            this.rbtOK = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtOK)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel1.Location = new System.Drawing.Point(2, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(715, 280);
            this.radPanel1.TabIndex = 0;
            this.radPanel1.Text = "radPanel1";
            // 
            // rbtCancel
            // 
            this.rbtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rbtCancel.Image = global::SSIT.QM.Properties.Resources.cancel;
            this.rbtCancel.Location = new System.Drawing.Point(596, 298);
            this.rbtCancel.Name = "rbtCancel";
            this.rbtCancel.Size = new System.Drawing.Size(110, 36);
            this.rbtCancel.TabIndex = 27;
            this.rbtCancel.Text = "关闭";
            // 
            // rbtOK
            // 
            this.rbtOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.rbtOK.Enabled = false;
            this.rbtOK.Image = global::SSIT.QM.Properties.Resources.OK;
            this.rbtOK.Location = new System.Drawing.Point(340, 294);
            this.rbtOK.Name = "rbtOK";
            this.rbtOK.Size = new System.Drawing.Size(121, 40);
            this.rbtOK.TabIndex = 26;
            this.rbtOK.Text = "确定";
            // 
            // SampleItemSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 344);
            this.Controls.Add(this.rbtCancel);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.rbtOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SampleItemSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择样品工单";
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtOK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton rbtCancel;
        private Telerik.WinControls.UI.RadButton rbtOK;
    }
}