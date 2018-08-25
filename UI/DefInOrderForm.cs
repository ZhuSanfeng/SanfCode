using SSIT.QualityManage.Interface;
using SSIT.EncodeBase;
using SSIT.MM;
using SSIT.QueryBase;
using SSITControls.DataGridView;
using SSITControls.WaitForm;
using SSITEncode.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using YHDataInterface.SSITMM;

namespace SSIT.QualityManage.UI
{

    public partial class DefInOrderForm : Telerik.WinControls.UI.RadForm
    {
        private SSITGridView<MMInOrder> OrderGV;
        private string[] CenterColumnName = new String[] { "OrderStateName" };
        ToolStripButton btnAdd;
        ToolStripButton btnModify;
        ToolStripButton btnDiscard;
        public DefInOrderForm()
        {
            InitializeComponent();
            dateTimeRange1.StartValue = System.DateTime.Now.Date.AddDays(-7);
            dateTimeRange1.EndValue = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            InitGrid();
            OrderGV.GridDbClick += OrderGV_GridDbClick;
            OrderGV.GridClick += OrderGV_GridClick;
            btnAdd = OrderGV.AddToolBarButton("添加", QualityManage.Properties.Resources.AddMaterisls, true, ToolStripItemDisplayStyle.ImageAndText);
            btnAdd.Enabled = true;
            btnAdd.Click += btnAdd_Click;

            btnDiscard = OrderGV.AddToolBarButton("废弃", QualityManage.Properties.Resources.Discard, true, ToolStripItemDisplayStyle.ImageAndText);
            btnDiscard.Enabled = false;
            btnDiscard.Click += btnDiscard_Click;

            btnModify = OrderGV.AddToolBarButton("修改", QualityManage.Properties.Resources.Release, true, ToolStripItemDisplayStyle.ImageAndText);
            btnModify.Enabled = false;
            btnModify.Click += btnModify_Click;
        }

        private void InitGrid()
        {
            #region 表格属性设定
            OrderGV = new SSITGridView<MMInOrder>();
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
            OrderGV.FillGrid(GetDatas());
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
            GridCellElement cellElement = e.CellElement;
            GridViewDataColumn ColumnItem = e.CellElement.ColumnInfo as GridViewDataColumn;

            if (ColumnItem.OwnerTemplate is MasterGridViewTemplate)
            {
                //布尔型居中 指定字段居中
                if (ColumnItem != null &&
                        ColumnItem.DataType == typeof(bool) || CenterColumnName.Contains(ColumnItem.Name))
                {
                    e.CellElement.TextAlignment = ContentAlignment.MiddleCenter;
                }
            }
        }

        private EncodeCollection<MMInOrder> GetDatas()
        {
            var Helper = new QueryHelper<MMInOrder>();
            Helper.Add("SynTime", this.dateTimeRange1);

            if (!rtbBatch.Text.IsNullOrWhiteSpace())
            {
                var LotIDs = rtbBatch.Text.ToLikeString();
                Helper.Add("BatchID", LotIDs);
                
            }
            if (!rtbOrderID.Text.IsNullOrWhiteSpace())
            {
                var orderIDs = rtbOrderID.Text.ToLikeString();
                Helper.Add("OrderID", orderIDs);
            }
            if (!rtbPurOrderID.Text.IsNullOrWhiteSpace())
            {
                var purIDs = rtbPurOrderID.Text.ToLikeString();
                Helper.Add("PurchaseOrderPK", purIDs);
            }
            var ec = Helper.GetDatas();
            EncodeCollection<MMInOrder> sec = new EncodeCollection<MMInOrder>();
            EncodeCollection<MMInOrder> tec = new EncodeCollection<MMInOrder>();
            if (!rtbMMDef.Text.IsNullOrWhiteSpace())
            {
                var mmHelper = new QueryHelper<MMDefinition>();
                var MMnames = rtbMMDef.Text.ToLikeString();
                mmHelper.Add("paramname", MMnames);
                string mmclause = mmHelper.GetSQLCondition();
                var mmec = mmHelper.GetDatas();
                List<int> DefIDs = new List<int>();
                
                
               foreach (var data in mmec)
               {
                   DefIDs.Add(data.ParamID);
               }
               
                foreach (var item in ec)
                {
                    if (DefIDs.Contains(item.DefPK))
                    {
                        sec.Add(item);
                    }
                }
                if (sec.Count == 0)
                {
                    var mmidHelper = new QueryHelper<MMDefinition>();
                    var MMids = rtbMMDef.Text.ToLikeString();
                    mmidHelper.Add("DefID", MMids);
                    string mmidclause = mmHelper.GetSQLCondition();
                    mmec = mmidHelper.GetDatas();
                    DefIDs.Clear();
                    foreach (var data in mmec)
                    {
                        DefIDs.Add(data.ParamID);
                    }

                    foreach (var item in ec)
                    {
                        if (DefIDs.Contains(item.DefPK))
                        {
                            sec.Add(item);
                        }
                    }
                }
                
            }
            else
            {
                sec = ec;
            }
            if (!rtbSupplier.Text.IsNullOrWhiteSpace())
            {
                var supHelper = new QueryHelper<Supplier>();
                var supnames = rtbSupplier.Text.ToLikeString();
                supHelper.Add("paramname", supnames);
                string supclause = supHelper.GetSQLCondition();
                var supec = supHelper.GetDatas();
                List<int> SupIDs = new List<int>();
                foreach (var data in supec)
                {
                    SupIDs.Add(data.ParamID);
                }

                foreach (var item in sec)
                {
                    if (SupIDs.Contains(item.SupPK))
                    {
                        tec.Add(item);
                    }
                }
                return tec;
            }
            return sec;
            
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

       

        

        void OrderGV_GridDbClick(object sender, GridViewCellEventArgs e)
        {
            //btnEdit_Click(sender, null);
        }

        void OrderGV_GridClick(object sender, GridViewCellEventArgs e)
        {
            if (OrderGV.SelectedItem != null)
            {
                var order = OrderGV.SelectedItems[0];
                if(order.OrderState==  MMDefInOrderStateEnum.Creat)
                {
                    btnDiscard.Enabled = true;
                    btnModify.Enabled = true;

                }
                else
                {
                    btnDiscard.Enabled = false;
                    btnModify.Enabled = false;
                }
            }
            else
            {
                btnDiscard.Enabled = false;
                btnModify.Enabled = false;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (OrderGV.SelectedItems.Count == 1)
            {
                //修改
                var order = OrderGV.SelectedItems[0];

                DefInOrderModifyForm form = new DefInOrderModifyForm(order);
                if (form.ShowDialog() == DialogResult.OK)
                {

                    OrderGV.Refresh(true);
                };
            }
            else {
                MessageBox.Show("请选择单行数据进行修改");
                return;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            
              NewDefInOrderForm form = new NewDefInOrderForm();
            if (form.ShowDialog()==DialogResult.OK)
            {
                OrderGV.Datas.Add(form.OrderItem);
                OrderGV.Refresh(true);
            };
            
        }
        private void btnDiscard_Click(object sender, EventArgs e)
        {
            if (OrderGV.SelectedItems.Count > 0&& OrderGV.SelectedItem!=null)
            {
                if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("确定废弃？"
            , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
                {
                    return;
                }
                var Datas = new EncodeCollection<MMInOrder>();
                for (int i = 0; i < OrderGV.SelectedItems.Count; i++)
                {
                    var order = OrderGV.SelectedItems[i];
                    order.OrderState =   MMDefInOrderStateEnum.Discard;
                    //var Helper = new QueryHelper<RetainedSampleOrderItem>();
                    //Helper.Add();
                    order.State = DataState.Changed;
                    Datas.Add(order);
                }
                Datas.SaveDatas();
                OrderGV.Refresh(true);
               
            }
        }

        private void qtButton_Click(object sender, EventArgs e)
        {
            ExecuteQuery();
        }
    }
}
