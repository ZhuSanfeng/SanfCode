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
using SSIT.UserInfo;
using SSIT.QMBase;
using SSIT.QM.SampleInterface;
using SSITEncode.Common;
using SSIT.QMBase.CodeInterface;
using SSIT.QM.CheckInterface;

namespace SSIT.QM.CheckManager
{
    //public partial class CheckQueryControl : UserControl
    public partial class InWareCheckQueryControl : UCInWareQueryControl//QueryControlBase<CheckOrder>
    {
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
            selectDefinitionFieldControl1.Field = "defpk";//物料
            mutiSelectField2.Field = "checkorderstate";
           dateQueryField1.Field = "sampledate";
           dateQueryField2.Field = "sampledate";
           singleFieldTextbox1.Field = "guid";
           //singleFieldTextbox1.radTextBox.
           mutiSelectField3.Field = "qualifyjudge";
           mutiSelectField4.Field = "usagedecisions";
            mutiSelectField5.Field = "checktype";

           mutiSelectField1.Field = "sampletype";//样品类型

           //工单状态
           dic = new Dictionary<string, object>();
           dic.Add("", "");
            dic.Add(CheckOrderStateEnum.Editing.GetDescription(), CheckOrderStateEnum.Editing);
            dic.Add(CheckOrderStateEnum.Submit.GetDescription(), CheckOrderStateEnum.Submit);
           dic.Add(CheckOrderStateEnum.Running.GetDescription(), CheckOrderStateEnum.Running);
           dic.Add(CheckOrderStateEnum.Complete.GetDescription(), CheckOrderStateEnum.Complete);
           dic.Add(CheckOrderStateEnum.Auditor.GetDescription(), CheckOrderStateEnum.Auditor);
           dic.Add(CheckOrderStateEnum.Approve.GetDescription(), CheckOrderStateEnum.Approve);
           dic.Add(CheckOrderStateEnum.Discard.GetDescription(), CheckOrderStateEnum.Discard);
          var keys = dic.Keys.Where(p => p != CheckOrderStateEnum.Discard.GetDescription());
           mutiSelectField2.Init(dic);
           mutiSelectField2.SetSelectedItems(new List<string>(keys));
           //日期设定
           dateQueryField1.Value = System.DateTime.Now;
           dateQueryField2.Value = System.DateTime.Now;
           dateQueryField1.IsDateOnly = true;
           dateQueryField2.IsDateOnly = true;
           dateQueryField1.Comparer = SSIT.DataField.Comparer.GreaterEqual;
           dateQueryField2.Comparer = SSIT.DataField.Comparer.LessEqual;

           //合格状态 系统判定
           dic = new Dictionary<string, object>();
           dic.Add("", "");
           dic.Add("不合格", QualifyJudgeEnum.False);
           dic.Add("合格", QualifyJudgeEnum.Pass);
           dic.Add("未完成", QualifyJudgeEnum.UnFinish);
           dic.Add("不判定", QualifyJudgeEnum.UnJudge);
           mutiSelectField3.Init(dic);
           //手动判定
           dic = new Dictionary<string, object>();
           dic.Add("", "");
           foreach (var item in UsageDecisions.Instance.GetEnableCollection())
           {
               if (!dic.ContainsKey(item.ParamName))
               {
                   dic.Add(item.ParamName, item.ParamName);
               }
           }
           mutiSelectField4.Init(dic);
           //样品类型
           dic = new Dictionary<string, object>();
           dic.Add("", "");
           foreach (var data in SampleTypeClass.Datas)
           {
              // var key = SampleTypeClass.GetStringValue(id);
               if(!dic.ContainsKey(data.Value))
               dic.Add(data.Value,data.Key);
           }
           mutiSelectField1.Init(dic);
            var typekeys = dic.Keys.Where(p => p == SampleTypeEnum.PackageInWare.GetDescription());
            mutiSelectField1.SetSelectedItems(new List<string>(typekeys));
            mutiSelectField1.Enabled = false;
            //machine brand

        }

        
       #endregion

       public void QueryByOrder(string OrderID)
       {
           singleFieldTextbox1.Text = OrderID;
           dateQueryField1.Checked = false;
           dateQueryField2.Checked = false;
           btQuery_Click(btQuery, null);
       }

       public void QueryByLotName(string LotName,DateTime StartDate)
       {
           dateQueryField1.Checked = true;
           dateQueryField1.Value = StartDate;
           dateQueryField2.Checked = false;
           btQuery_Click(btQuery, null);
       }

       public InWareCheckQueryControl()
       {
           InitializeComponent();

           InitControl();
           QueryFields = new List<IQueryField>(new IQueryField[] { singleFieldTextbox1,               
               mutiSelectField1, mutiSelectField2,mutiSelectField3,mutiSelectField4,
               dateQueryField1, dateQueryField2 ,selectDefinitionFieldControl1
               
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
                if (FieldManager.IsHasField(new CheckOrder(), iqf.Field))
                    foreach (FieldCondition fc in iqf.GetFieldConditions())
                    {
                        fcc.Add(fc);
                    }
            }
            if (fcc.Count > 0)
            {
                fcc[fcc.Count - 1].logic = Logicaler.End;
            }
            //fcc.Add(new FieldCondition { ColumnName = "checktype", comparer = Comparer.Equel, data = (int)createType, logic = Logicaler.End });

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

    }

    public class UCInWareQueryControl : QueryControlBase<CheckOrder> { }
}
