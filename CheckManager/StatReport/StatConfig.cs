using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SSIT.QueryBase;
using SSIT.DataField;

namespace SSIT.QM.CheckManager.StatReport
{
	/// <summary>
	/// StatConfig 的摘要说明。
	/// </summary>
	public class StatConfig : System.Windows.Forms.Form
	{
		public bool IsChanged = false;
        private SampleStatReportSettings _srs;
		private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancle;
        private GroupBox groupBox3;
        private EFieldSelect fsHead;
        private EFieldSelect fsStat;
        private GroupBox groupBox1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StatConfig()
		{
			_srs =SampleStatReportSettings.Instance;
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			LoadInfo ();
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancle = new System.Windows.Forms.Button();
            this.fsHead = new SSIT.QueryBase.EFieldSelect();
            this.fsStat = new SSIT.QueryBase.EFieldSelect();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fsHead);
            this.groupBox3.Location = new System.Drawing.Point(12, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(377, 242);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "请选择要显示的头字段";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fsStat);
            this.groupBox1.Location = new System.Drawing.Point(395, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 240);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择统计项目";
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOK.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btOK.Image = global::SSIT.QM.Properties.Resources.OK;
            this.btOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOK.Location = new System.Drawing.Point(424, 268);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(125, 47);
            this.btOK.TabIndex = 35;
            this.btOK.Text = "确定(&N)";
            this.btOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancle
            // 
            this.btCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancle.Image = global::SSIT.QM.Properties.Resources.cancel;
            this.btCancle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCancle.Location = new System.Drawing.Point(637, 268);
            this.btCancle.Name = "btCancle";
            this.btCancle.Size = new System.Drawing.Size(108, 47);
            this.btCancle.TabIndex = 36;
            this.btCancle.Text = "取消";
            this.btCancle.Click += new System.EventHandler(this.btCancle_Click);
            // 
            // fsHead
            // 
            this.fsHead.AllField = null;
            this.fsHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsHead.Location = new System.Drawing.Point(3, 17);
            this.fsHead.Name = "fsHead";
            this.fsHead.SelectField = null;
            this.fsHead.Size = new System.Drawing.Size(371, 222);
            this.fsHead.TabIndex = 0;
            // 
            // fsStat
            // 
            this.fsStat.AllField = null;
            this.fsStat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsStat.Location = new System.Drawing.Point(3, 17);
            this.fsStat.Name = "fsStat";
            this.fsStat.SelectField = null;
            this.fsStat.Size = new System.Drawing.Size(372, 220);
            this.fsStat.TabIndex = 0;
            // 
            // StatConfig
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(783, 330);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancle);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StatConfig";
            this.Text = "汇总设置";
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void LoadInfo ()
		{
			fsStat.AllField = FieldManager.GetFields (typeof (Stat));
			fsStat.SelectField = _srs.StatFields.Copy ();
  
			fsStat.LoadInfo ();

            //heads
            DataFieldAttribute fMaterialClass = new DataFieldAttribute { Description = "物料小类" };
            DataFieldAttribute fDefinition = new DataFieldAttribute { Description = "物料名称" };
            DataFieldAttribute fLotName = new DataFieldAttribute { Description = "批次名称" };
            DataFieldAttribute fHut = new DataFieldAttribute { Description = "位置" };
            DataFieldAttribute fProduceDate = new DataFieldAttribute { Description = "计划检测日期" };
            //DataFieldAttribute fCheckCategory = new DataFieldAttribute { Description = "项目类型" };
            DataFieldAttribute fSupplier = new DataFieldAttribute { Description = "供应商" };
            DataFieldAttribute fInspector = new DataFieldAttribute { Description = "检验员" };
            fsHead.AllField = new FieldCollection();
            fsHead.AllField.AddRange(new DataFieldAttribute[] { fProduceDate, fMaterialClass, fDefinition, fHut, fLotName, fSupplier,fInspector });
            fsHead.SelectField = _srs.HeadFields;
            fsHead.LoadInfo();
		}

		private void btOK_Click(object sender, System.EventArgs e)
		{
			_srs.StatFields = fsStat.SelectField;

            _srs.Save();
			this.IsChanged = fsStat.IsChanged;
			this.Close ();
		}

		private void btCancle_Click(object sender, System.EventArgs e)
		{
			this.Close ();
		}

	}
}
