using SSIT.EncodeBase;
using SSIT.QM.CheckInterface;
using SSIT.QueryBase;
using SSITControls.DataGridView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using SSITEncode.Common;
using SSIT.QMBase;
using SSITControls.WaitForm;
using SSIT.QM.CheckManager.DatasForms;
using SSIT.QM.SampleInterface;

namespace SSIT.QM.CheckManager
{
    public partial class CheckOrderForm : Telerik.WinControls.UI.RadForm
    {
        private SSITGridView<CheckOrder> OrderGV;
        ToolStripButton btnAdd;
        ToolStripButton btnAddData;
        ToolStripButton tsbDel;
        ToolStripButton tsbDiscard;
        protected System.Threading.SynchronizationContext _sc;
        public CheckOrderForm()
        {
            _sc = System.Threading.SynchronizationContext.Current;
            InitializeComponent();
           // dateTimeRange1.StartValue = System.DateTime.Now.Date.AddDays(-7);
            //dateTimeRange1.EndValue = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            InitGrid();
            checkQueryControl1.StartDateTime = System.DateTime.Now.AddDays(-7);
            checkQueryControl1.EndDateTime = System.DateTime.Now.AddDays(1);
            checkQueryControl1.QueryReturnData += checkQueryControl1_QueryReturnData;
            checkQueryControl1.QueryMessage += checkQueryControl1_QueryMessage;

            btnAdd = OrderGV.AddToolBarButton("创建检测工单", SSIT.QM.Properties.Resources.Add, true, ToolStripItemDisplayStyle.ImageAndText);
            btnAdd.Enabled = true;
            btnAdd.Click += btnAdd_Click;

            btnAddData = OrderGV.AddToolBarButton("检验数据录入", SSIT.QM.Properties.Resources.CreateOrder, true, ToolStripItemDisplayStyle.ImageAndText);
            btnAddData.Enabled = true;
            btnAddData.Click += btnAddData_Click;

            tsbDiscard = OrderGV.AddToolBarButton("废弃检测单", Properties.Resources.Discard48, true, ToolStripItemDisplayStyle.ImageAndText);
            tsbDiscard.Enabled = true;
            tsbDiscard.Click += tsbDiscard_Click;

            tsbDel = OrderGV.AddToolBarButton("删除检测单", Properties.Resources.DeleteOrder, true, ToolStripItemDisplayStyle.ImageAndText);
            tsbDel.Enabled = true;
            tsbDel.Click += tsbDel_Click;
        }
        private void InitGrid()
        {
            #region 表格属性设定
            OrderGV = new SSITGridView<CheckOrder>();
            OrderGV.Dock = DockStyle.Fill;
            OrderGV.AllowDeleteRow = false;
            OrderGV.AllowAddNewRow = false;
            OrderGV.AllowEditRow = false;
            OrderGV.AllowToolBar = true;
            OrderGV.AllowBottomToolBar = true;
            OrderGV.AllowBottomToolBarFilter = false;
            OrderGV.AllowDragToGroup = false;
            OrderGV.AllowPaging = false;
            OrderGV.MultiSelect = false;
            OrderGV.AllowCheckedColumn = false;
            #endregion

            #region 表格事件
            //过滤:
            OrderGV.GridFilterMode = FilterMode.None;
            OrderGV.StyleChanged += OrderGV_StyleChanged;
            OrderGV.CellFormatting += OrderGV_CellFormatting;
            OrderGV.GridDbClick += OrderGV_GridDbClick;
            #endregion

            OrderGV.InitColumns();
            //GV.AllowEditColumns();
            var Query = new QueryHelper<CheckOrder>();
            Query.Add("sampledate", DateTime.Now.Date.AddDays(-8).ToString(EncodeConst.DateTimeFormat), SSIT.DataField.Comparer.GreaterEqual);
            Query.Add("sampledate", DateTime.Now.Date.AddDays(1).ToString(EncodeConst.DateTimeFormat), SSIT.DataField.Comparer.Less);
            string[] StateNames = new string[] { SampleTypeEnum.PackagesComming.GetDescription() };
            Query.Add<SampleTypeEnum>("sampletype", StateNames);
            var str = Query.GetSQLCondition();
            var ec = Query.GetDatas();
            OrderGV.FillGrid(ec);
            GridPanel.Controls.Add(OrderGV);
        }

        private void OrderGV_StyleChanged(object sender, EventArgs e)
        {
            ColumnColor Style1 = new ColumnColor();
            Style1.ForeColor = Color.Blue;
            OrderGV.SetColor("OrderStateName", Style1, ConditionTypes.Equal, "已创建", "", false);

            ColumnColor Style2 = new ColumnColor();
            Style2.BackColor = Color.Pink;
            OrderGV.SetColor("OrderStateName", Style2, ConditionTypes.Equal, "已提交", "", false);

            ColumnColor Style3 = new ColumnColor();
            Style3.BackColor = Color.Green;
            OrderGV.SetColor("OrderStateName", Style3, ConditionTypes.Equal, "已审批", "", false);

            ColumnColor Style4 = new ColumnColor();
            Style2.BackColor = Color.Red;
            OrderGV.SetColor("OrderStateName", Style4, ConditionTypes.Equal, "已废弃", "", false);
        }

        private void OrderGV_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            //GridCellElement cellElement = e.CellElement;
            //GridViewDataColumn ColumnItem = e.CellElement.ColumnInfo as GridViewDataColumn;

            //if (ColumnItem.OwnerTemplate is MasterGridViewTemplate)
            //{
            //    //布尔型居中 指定字段居中
            //    if (ColumnItem != null &&
            //            ColumnItem.DataType == typeof(bool) || CenterColumnName.Contains(ColumnItem.Name))
            //    {
            //        e.CellElement.TextAlignment = ContentAlignment.MiddleCenter;
            //    }
            //}
        }

        private void OrderGV_GridDbClick(object sender, GridViewCellEventArgs e)
        {
            if (OrderGV.SelectedItem != null)
            {
                var checkorder = OrderGV.SelectedItem;
                if(checkorder.CheckOrderState== CheckOrderStateEnum.Discard)
                {
                    MessageBox.Show("该工单已废弃，无法录入数据");
                    return;
                }
                CheckDataInputForm form = new CheckDataInputForm(checkorder);
                form.ShowDialog();
                return;
            }
          
            
        }
        void checkQueryControl1_QueryMessage(string Message)
        {
            _sc.Post(new System.Threading.SendOrPostCallback(OnQueryMessage), Message);
        }
        void OnQueryMessage(object message)
        {
            ReturnValue.ShowMessage((string)message);
        }
        void checkQueryControl1_QueryReturnData(EncodeCollection<IEncode> datas)
        {
            EncodeCollection<CheckOrder> ec = new EncodeCollection<CheckOrder>();
            foreach (CheckOrder item in datas)
            {
                ec.Add(item);
            }
            
            OrderGV.FillGrid(ec);
        }

        //SSITControls.WaitForm.WaitForm Wait;                 //等待窗体
        //private void ExecuteQuery()
        //{
        //    Wait = new SSITControls.WaitForm.WaitForm();
        //    ExecuteThread();
        //    // Wait.ExecuteEvent = ExecuteThread;
        //    Wait.AllowCancel = true;
        //    Wait.AllowProcess = false;
        //    Wait.AllowTimer = false;
        //    Wait.AutoMessage = true;
        //    // Wait.Execute();
        //}

        //private void ExecuteThread()
        //{
        //    try
        //    {
        //        Wait.SetLable("正在进行数据查询,请稍候...");
        //        var OrderDatas = GetDatas();
        //        Wait.SetLable("正在填充表格,请稍候...");
        //        OrderGV.FillGridByThread(OrderDatas);
        //    }
        //    catch (Exception ex)
        //    {
        //        Wait.Message = ex.Message;
        //        if (Wait.JobState == WaitState.正在操作)
        //        {
        //            Wait.Close();
        //        }
        //    }
        //}
        private void btnAdd_Click(object sender, EventArgs e)
        {

            CreateCheckOrderForm form = new CreateCheckOrderForm(SampleTypeEnum.PackagesComming);
            form.Tag = Tag;
            form.ShowDialog();

        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            if (OrderGV.SelectedItems.Count== 1)
            {
                var order = OrderGV.SelectedItems[0];
                CheckDataInputForm form = new CheckDataInputForm(order);
                form.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("请选择单行数据进行修改");
                return;
            }

        }

        private void tsbDiscard_Click(object sender, EventArgs e)
        {
            if (OrderGV.SelectedItems != null && OrderGV.SelectedItems.Count > 0)
            {
                if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("确定废弃？"
             , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
                {
                    return;
                }
                var Datas = new EncodeCollection<CheckOrder>();
                for (int i = 0; i < OrderGV.SelectedItems.Count; i++)
                {
                    var order = OrderGV.SelectedItems[i];
                   
                    order.CheckOrderState =  CheckOrderStateEnum.Discard;
                    //var Helper = new QueryHelper<RetainedSampleOrderItem>();
                    //Helper.Add();
                    order.State = DataState.Changed;
                    Datas.Add(order);
                }
                Datas.SaveDatas();
                OrderGV.Refresh(OrderGV.Datas);
            }

        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (OrderGV.SelectedItems != null && OrderGV.SelectedItems.Count > 0)
            {
                if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("确定删除？"
             , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
                {
                    return;
                }
                var Datas = new EncodeCollection<CheckOrder>();
                for (int i = 0; i < OrderGV.SelectedItems.Count; i++)
                {
                    var order = OrderGV.SelectedItems[i];

                    //order.OrderState = MMDefInOrderStateEnum.Discard;
                    //var Helper = new QueryHelper<RetainedSampleOrderItem>();
                    //Helper.Add();
                    order.State = DataState.Deleted;
                    Datas.Add(order);
                    OrderGV.Datas.Remove(order);
                }
                Datas.SaveDatas();
                OrderGV.Refresh(OrderGV.Datas);
            }

        }
        private void rbQuery_Click(object sender, EventArgs e)
        {
            //ExecuteQuery();
        }
    }
}
