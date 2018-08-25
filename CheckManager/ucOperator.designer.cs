namespace SSIT.RetainedSample.UI
{
    partial class ucOperator
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOperatorString = new Telerik.WinControls.UI.RadTextBox();
            this.btnSelectOperator = new Telerik.WinControls.UI.RadButton();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperatorString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectOperator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOperatorString
            // 
            this.txtOperatorString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOperatorString.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOperatorString.Location = new System.Drawing.Point(0, 0);
            this.txtOperatorString.Name = "txtOperatorString";
            this.txtOperatorString.ReadOnly = true;
            this.txtOperatorString.Size = new System.Drawing.Size(218, 21);
            this.txtOperatorString.TabIndex = 1034;
            // 
            // btnSelectOperator
            // 
            this.btnSelectOperator.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectOperator.Location = new System.Drawing.Point(218, 0);
            this.btnSelectOperator.Name = "btnSelectOperator";
            this.btnSelectOperator.Size = new System.Drawing.Size(38, 22);
            this.btnSelectOperator.TabIndex = 1033;
            this.btnSelectOperator.Text = "选择";
            this.btnSelectOperator.Click += new System.EventHandler(this.btnSelectOperator_Click);
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.txtOperatorString);
            this.radPanel1.Controls.Add(this.btnSelectOperator);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(256, 22);
            this.radPanel1.TabIndex = 1035;
            // 
            // ucOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radPanel1);
            this.MaximumSize = new System.Drawing.Size(1024, 22);
            this.MinimumSize = new System.Drawing.Size(20, 22);
            this.Name = "ucOperator";
            this.Size = new System.Drawing.Size(256, 22);
            ((System.ComponentModel.ISupportInitialize)(this.txtOperatorString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectOperator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtOperatorString;
        private Telerik.WinControls.UI.RadButton btnSelectOperator;
        private Telerik.WinControls.UI.RadPanel radPanel1;
    }
}
