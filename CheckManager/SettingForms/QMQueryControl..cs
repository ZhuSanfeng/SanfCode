using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSIT.QueryBase;
using SSIT.DataField;
using SSIT.QMBase;
using SSIT.QM.SampleInterface;
using SSITEncode.Common;

namespace SSIT.QM.SampleManager.SettingForms
{
    //public partial class QMQueryControl : UserControl
    public partial class QMQueryControl : UCQueryControl//QueryControlBase<SampleOrder>
    {
        public SampleTypeEnum createType = SampleTypeEnum.PackagesComming;

        #region 属性方法

        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime StartDateTime
        {
            get { return this.dateQueryField1.Value; }
            set { this.dateQueryField1.Value = value; }
        }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime EndDateTime
        {
            get { return this.dateQueryField2.Value; }
            set { this.dateQueryField2.Value = value; }
        }
        #endregion

        #region 设置查询字段&&设置多行以及控件初始化

       private void InitControl()
       {
           Dictionary<string, object> dic;
           //字段设定
           mutiSelectField1.Field = "checktype";
           mutiSelectField2.Field = "sampleitemstate";
           dateQueryField1.Field = "plancheckdate";
           dateQueryField2.Field = "plancheckdate";
           singleFieldTextbox1.Field = "sampleid";

           mutiSelectField3.Field = "qualifyjudge";
           //检验类型
           dic = new Dictionary<string, object>();
           dic.Add("", "");
          // dic.Add("无", SampleTypeEnum.None);

           foreach (int myCode in Enum.GetValues(typeof(SampleTypeEnum)))
           {
               SampleTypeEnum EnumItem = (SampleTypeEnum)myCode;
                string strText = EnumItem.GetDescription();
               if (!string.IsNullOrWhiteSpace(strText))
               {
                   dic.Add(strText, EnumItem);
               }
           }
           //checktype
           dic = new Dictionary<string, object>();
           dic.Add("", "");

           mutiSelectField1.Init(dic);
           //工单状态
           dic = new Dictionary<string, object>();
           dic.Add("", "");
           dic.Add("待检", CheckOrderStateEnum.Submit);
           dic.Add("检验", CheckOrderStateEnum.Complete);
           dic.Add("已检", CheckOrderStateEnum.Approve);
           dic.Add("废弃", CheckOrderStateEnum.Discard);
           mutiSelectField2.Init(dic);
           mutiSelectField2.SetSelectedItems(new List<string>(new string[] { "待检", "检验", "已检" }));
           //日期设定
           dateQueryField1.Value = System.DateTime.Now;
           dateQueryField2.Value = System.DateTime.Now;
           dateQueryField1.IsDateOnly = true;
           dateQueryField2.IsDateOnly = true;
           dateQueryField1.Comparer = SSIT.DataField.Comparer.GreaterEqual;
           dateQueryField2.Comparer = SSIT.DataField.Comparer.LessEqual;

           //合格状态
           dic = new Dictionary<string, object>();
           dic.Add("", "");
           dic.Add("不合格", QualifyJudgeEnum.False);
           dic.Add("合格", QualifyJudgeEnum.Pass);
           dic.Add("未完成", QualifyJudgeEnum.UnFinish);
           dic.Add("不判定", QualifyJudgeEnum.UnJudge);
           mutiSelectField3.Init(dic);
       }
       #endregion

       public void QueryByOrder(string OrderID)
       {
           singleFieldTextbox1.Text = OrderID;
           dateQueryField1.Checked = false;
           dateQueryField2.Checked = false;
           btQuery_Click(btQuery, null);
       }


       public QMQueryControl()
       {
           InitializeComponent();
           InitControl();
           QueryFields = new List<IQueryField>(new IQueryField[] {mutiSelectField1, singleFieldTextbox1,
                mutiSelectField2,mutiSelectField3, 
               dateQueryField1, dateQueryField2 
            });
       }

        private bool _bQuery = true;
        private void btQuery_Click(object sender, EventArgs e)
        {
            _bQuery = !_bQuery;
            if (_bQuery)
            {
                this.Cursor = Cursors.Default;
                btQuery.Text = "查询";
                btQuery.ForeColor = System.Drawing.SystemColors.ControlText;
                //Smil.SqlServerInterface.ServerEncodeManager<Tobacco>.Instance.Close();
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                btQuery.Text = "停止查询";
                btQuery.ForeColor = Color.Red;
                QueryStart();
            }
        }

        public override void QueryStart()
        {
            backgroundWorker1.DoWork -= new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            FieldConditionCollection fcc = new FieldConditionCollection();
            foreach (IQueryField iqf in QueryFields)
            {
                if (iqf.Field == null)
                    continue;
                if (FieldManager.IsHasField(new SampleOrder(), iqf.Field))
                    foreach (FieldCondition fc in iqf.GetFieldConditions())
                    {
                        fcc.Add(fc);
                    }
            }
           // fcc.Add(new FieldCondition { ColumnName = "checktype", comparer = Comparer.Equel, data = (int)createType, logic = Logicaler.End });

            //if (createType == CreateTypeEnum.Normal)
            //{
            //    fcc.Add(new FieldCondition { ColumnName = "checktype", comparer = Comparer.Equel, data = (int), logic = Logicaler.End });
            //}
            //else
            //{
            //    fcc.Add(new FieldCondition { ColumnName = "checktype", comparer = Comparer.NoEquel, data = 2, logic = Logicaler.End });
            //}
            //每个表查完后都应该立即显示进程
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            else
                backgroundWorker1.RunWorkerAsync(fcc);
        }
        protected override void QueryEnd()
        {
            base.QueryEnd();
            btQuery.Text = "查询";
            _bQuery = true;
            btQuery.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        public void LoadInfo()
        {
            
        }

        public string Title
        {
            get { return string.Empty; }
        }

        public string WorkShipMan
        {
            get { return string.Empty; }
        }

    }

    public class UCQueryControl : QueryControlBase<SampleOrder> { }
}
