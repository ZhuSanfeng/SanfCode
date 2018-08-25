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

namespace SSIT.QM.CheckManager
{
    public partial class CheckDataForm : Telerik.WinControls.UI.RadForm
    {
        private SSITGridView<CheckData> OrderGV;
        ToolStripButton btnAdd;
        public CheckDataForm()
        {
            InitializeComponent();
            dateTimeRange1.StartValue = System.DateTime.Now.Date.AddDays(-7);
            dateTimeRange1.EndValue = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            InitGrid();
            btnAdd = OrderGV.AddToolBarButton("添加", SSIT.QM.Properties.Resources.Add, true, ToolStripItemDisplayStyle.ImageAndText);
            btnAdd.Enabled = true;
            btnAdd.Click += btnAdd_Click;
        }
        private void InitGrid()
        {
            #region 表格属性设定
            OrderGV = new SSITGridView<CheckData>();
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
            #endregion

            OrderGV.InitColumns();
            //GV.AllowEditColumns();
           //OrderGV.FillGrid(CheckData.Instance.Datas);
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
            Style2.BackColor = Color.Green;
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

        private EncodeCollection<CheckOrder> GetDatas()
        {
            var query = new QueryHelper<CheckOrder>();
            query.Add("createtime", this.dateTimeRange1);
            if (!rtbCheckOrder.Text.IsNullOrWhiteSpace())
            {
                var CheckOrderIDs = rtbCheckOrder.Text.ToLikeString();
                query.Add("checkorderid", CheckOrderIDs);
            }
            if (rcddState.CheckedItems.Count > 0)
            {
                string[] StateNames = rcddState.CheckedItems.Select(x => x.Text).ToList().ToArray();
                query.Add<CheckOrderStateEnum>("checkorderstate", StateNames);
            }
            var ec = query.GetDatas();
            return ec;
        }

        SSITControls.WaitForm.WaitForm Wait;                 //等待窗体
        private void ExecuteQuery()
        {
            Wait = new SSITControls.WaitForm.WaitForm();
            ExecuteThread();
            // Wait.ExecuteEvent = ExecuteThread;
            Wait.AllowCancel = true;
            Wait.AllowProcess = false;
            Wait.AllowTimer = false;
            Wait.AutoMessage = true;
            // Wait.Execute();
        }

        private void ExecuteThread()
        {
            try
            {
                Wait.SetLable("正在进行数据查询,请稍候...");
                var OrderDatas = GetDatas();
                Wait.SetLable("正在填充表格,请稍候...");
                OrderGV.FillGridByThread(OrderDatas);
            }
            catch (Exception ex)
            {
                Wait.Message = ex.Message;
                if (Wait.JobState == WaitState.正在操作)
                {
                    Wait.Close();
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            CreateCheckOrderForm form = new CreateCheckOrderForm();
            form.Tag = Tag;
            form.ShowDialog();

        }
        private void rbQuery_Click(object sender, EventArgs e)
        {
            ExecuteQuery();
        }
    }
}

