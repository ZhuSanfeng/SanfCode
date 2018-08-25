using SSIT.QualityManage.Interface;
using SSITControls.DataGridView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSIT.QM.SampleInterface;
using Telerik.WinControls.UI;
using SSIT.EncodeBase;
using SSIT.QualityManage.UI;
using SSITEncode.Common;
using SSIT.QueryBase;
using YHDataInterface.SSITMM;
using SSITControls.WaitForm;
using SSIT.QM.SampleManager;
using SSIT.QM.CheckManager;

namespace SSIT.QualityManage
{
    public partial class DefInOrderSampleRelationForm : Telerik.WinControls.UI.RadForm
    {
     
        SSITRelationGridView<MMInOrder, SampleOrder> GV;
        protected System.Threading.SynchronizationContext _sc;
        ToolStripButton btnAdd;
        ToolStripButton btnAddItem;
        ToolStripButton tsbDel;
        ToolStripButton tsbDiscard;
        //ToolStripButton tsbEdit;
        public DefInOrderSampleRelationForm()
        {
            _sc = System.Threading.SynchronizationContext.Current;
            InitializeComponent();
            dateTimeRange1.StartValue = System.DateTime.Now.AddDays(-6);
            dateTimeRange1.EndValue = System.DateTime.Now.AddDays(1);
            InitGrid();

            GV.GridDbClick += GV_GridDbClick;
            GV.GridClick += GV_GridClick;
                
               

            btnAdd = GV.AddToolBarButton("添加到货单", Properties.Resources.AddMaterisls, true, ToolStripItemDisplayStyle.ImageAndText);
            btnAdd.Enabled = true;
            btnAdd.Click += btnAdd_Click;
            btnAddItem = GV.AddToolBarButton("添加样品单", Properties.Resources.CreateOrder, true, ToolStripItemDisplayStyle.ImageAndText);
            btnAddItem.Enabled = false;
            btnAddItem.Click += btnAddItem_Click;
           
            tsbDiscard = GV.AddToolBarButton("废弃到货单", Properties.Resources.Discard48, true, ToolStripItemDisplayStyle.ImageAndText);
            tsbDiscard.Enabled = false;
            tsbDiscard.Click += tsbDiscard_Click;

            tsbDel = GV.AddToolBarButton("删除到货单", Properties.Resources.DeleteOrder, true, ToolStripItemDisplayStyle.ImageAndText);
            tsbDel.Enabled = false;
            tsbDel.Click += tsbDel_Click;

        }

        private void GV_GridDbClick1(object sender, GridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InitGrid()
            {
                GV = new SSITRelationGridView<MMInOrder, SampleOrder>();

                GV.Dock = DockStyle.Fill;
                GV.AutoExpandGroups = true;
                GV.MultiSelect = true;
                GV.AllowDeleteRow = false;
                GV.AllowAddNewRow = false;
                GV.AllowEditRow = false;
                GV.AllowBottomToolBar = true;
                GV.AllowBottomToolBarFilter = false;
                GV.AllowDragToGroup = true;
                GV.AllowPaging = false;
                GV.AllowScrollbarsInHierarchy = false;
                GV.RomoveChildTableRelationFields = true;
                GV.AddDefaultSort("OperationDate", ListSortDirection.Descending);



                //过滤:
                GV.GridFilterMode = FilterMode.CustomFilter;
                //GV.SelectionChanged += GV_SelectionChanged;
                //GV.GridClick += GV_GridClick;
                // GV.StyleChanged += GV_StyleChanged;
                GV.MasterStyleChanged += GV_StyleChanged;
                GV.CellFormatting += GV_CellFormatting;
                GV.InitColumns();
                GV.AddRelation("Relation1", "CheckLot", "SourceOrderID");
                //GV.HiddenColumns(false, "OrderID");    //将关系Guid列隐藏
                 EncodeCollection<MMInOrder> mminorder = GetDatas();
                EncodeCollection<SampleOrder> sampleorder = new EncodeCollection<SampleOrder>(SampleOrder.Instance.Datas);

                GV.FillGrid(mminorder,sampleorder);
                GridPanel.Controls.Add(GV);
            }
            private void GV_StyleChanged(object sender, EventArgs e)
            {
                //ConditionalFormattingObject CellStyle = new ConditionalFormattingObject("OrderStateName", ConditionTypes.Equal, "已创建", "", false);
                //CellStyle.CellForeColor = Color.Blue;


                ColumnColor Style1 = new ColumnColor();
                Style1.ForeColor = Color.Blue;
                //Style1.BackColor = Color.Red;
                GV.SetMasterColor("OrderStateName", Style1, ConditionTypes.Equal, "已创建", "", false);

                ColumnColor Style2 = new ColumnColor();
                Style2.ForeColor = Color.Red;

                GV.SetMasterColor("OrderStateName", Style2, ConditionTypes.Equal, "已销毁", "", false);

                ColumnColor Style3 = new ColumnColor();
                Style3.ForeColor = Color.Red;
                GV.SetMasterColor("OrderStateName", Style3, ConditionTypes.Equal, "已废弃", "", false);



                //ColumnColor Style4 = new ColumnColor();
                //Style2.BackColor = Color.Red;
                //GV.SetColor("OrderStateName", Style4, ConditionTypes.Equal, "已废弃", "", false);
            }

            private void GV_CellFormatting(object sender, CellFormattingEventArgs e)
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

                var mminorder = GetDatas();
                List<string> idList = new List<string>();
                foreach (var data in mminorder)
                {
                    idList.Add(data.OrderID);
                }
                SampleOrder.Instance.Datas = null;
                EncodeCollection<SampleOrder> sampleorder =
                   new EncodeCollection<SampleOrder>(SampleOrder.Instance.Datas.Where(p => idList.Contains(p.SourceOrderID)));

                Wait.SetLable("正在填充表格,请稍候...");
                GV.FillGridByThread(mminorder,sampleorder);
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
                NewDefInOrderForm form = new  NewDefInOrderForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                GV.MasterDatas.Add(form.OrderItem);
                GV.ChildDatas.Add(form.newSampleOrder);
                GV.Refresh(GV.MasterDatas, GV.ChildDatas);
                }

            }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (GV.ParentItemSelected && GV.SelectedItems != null && GV.SelectedItems.Count == 1)
            {
                    
                var order = GV.SelectedItems[0];
                if(order.OrderState== MMDefInOrderStateEnum.Discard)
                {
                    MessageBox.Show("该到货单已废弃，无法添加样品单");
                    return;
                }
                CreateSampleOrder(order);
                   
            }
            else
            {
                MessageBox.Show("请选择单行留样记录");
                return;
            }

        }

        private void tsbDiscard_Click(object sender, EventArgs e)
        {
            if (GV.ParentItemSelected && GV.SelectedItems != null && GV.SelectedItems.Count > 0)
            {
                if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show(string.Format("是否确定废弃所选的[{0}]个样品？", GV.SelectedItems.Count)
             , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
                {
                    return;
                }
                var Datas = new EncodeCollection<MMInOrder>();
                for (int i = 0; i < GV.SelectedItems.Count; i++)
                {
                    var order = GV.SelectedItems[i];
                    var items = SampleOrder.Instance.Datas.FirstOrDefault(p => p.SourceOrderID==order.CheckLot);
                    if(items!=null)
                    {
                        MessageBox.Show("该工单下已产生样品工单，无法废弃");
                        continue;
                    }
                    order.OrderState =  MMDefInOrderStateEnum.Discard;
                    //var Helper = new QueryHelper<RetainedSampleOrderItem>();
                    //Helper.Add();
                    order.State = DataState.Changed;
                    Datas.Add(order);
                }
                Datas.SaveDatas();
                GV.Refresh(GV.MasterDatas, GV.ChildDatas);
            }

        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (GV.ParentItemSelected && GV.SelectedItems != null && GV.SelectedItems.Count > 0)
            {
                
                if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show(string.Format("是否确定删除所选的[{0}]个样品？", GV.SelectedItems.Count)
             , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
                {
                    return;
                }
                var Datas = new EncodeCollection<MMInOrder>();
                for (int i = 0; i < GV.SelectedItems.Count; i++)
                {
                    var order = GV.SelectedItems[i];
                    var items = SampleOrder.Instance.Datas.FirstOrDefault(p => p.SourceOrderID == order.CheckLot);
                    if (items != null)
                    {
                        MessageBox.Show("该工单下已产生样品工单，无法删除");
                        continue;
                    }
                    //order.OrderState = MMDefInOrderStateEnum.Discard;
                    //var Helper = new QueryHelper<RetainedSampleOrderItem>();
                    //Helper.Add();
                    order.State = DataState.Deleted;
                    Datas.Add(order);
                    GV.MasterDatas.Remove(order);
                }
                Datas.SaveDatas();
                GV.Refresh(GV.MasterDatas, GV.ChildDatas);
            }

        }
        private void tsbEdit_Click(object sender, EventArgs e)
            {

                //if (GV.ParentItemSelected && GV.SelectedItems != null && GV.SelectedItems.Count == 1)
                //{
                //    if (GV.SelectedItems[0].SampleState > CheckOrderStateEnum.Submit)
                //    {
                //        ReturnValue.ShowMessage(string.Format("样品状态为【{0}】，不能进行编辑", GV.SelectedItems[0].GetSampleState));
                //        return;
                //    }
                //    CreateSampleOrderForm form = new CreateSampleOrderForm(GV.SelectedItems[0]);
                //    form.Text = "编辑样品";
                //    if (form.ShowDialog() == DialogResult.OK)
                //    {
                //        GV.RefreshSelectedRow();
                //    }
                //tw_Create.DockState = Telerik.WinControls.UI.Docking.DockState.Docked;
                //tw_Create.Text = "编辑小样";
                //tw_Create.Tag = SelectedSampleItem;
                //rtbBarcode.ReadOnly = true;
                //nudSampleCount.Value = 1;
                //nudSampleCount.ReadOnly = true;
                //rtbSampleID.ReadOnly = false;
                // }
            }


            //private void tsbDiscard_Click(object sender, EventArgs e)
            //{
            //    var rv = User.ProcessRight((WorkManShipItemRight)Tag, WorkManShipItemRightEnum.CanDiscard);
            //    if (!rv.Success)
            //    {
            //        ReturnValue.ShowError(rv.Message, "权限不够");
            //        return;
            //    }
            //    var ec = _Grid.SelectedItems;
            //    var allEc = _Grid.Datas;
            //    if (ec.Count > 0)
            //    {
            //        if (ReturnValue.ShowYesNo(string.Format("是否确定废弃所选的[{0}]个样品？", ec.Count)) == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            foreach (SampleOrder order in _Grid.SelectedItems)
            //            {
            //                if (order.SampleState == CheckOrderStateEnum.Complete
            //                    || order.SampleState == CheckOrderStateEnum.Approve
            //                    || order.SampleState == CheckOrderStateEnum.Discard)
            //                {
            //                    ReturnValue.ShowMessage(string.Format("状态为【{0}】小样不能废弃", SelectedSampleItem.GetSampleState));
            //                    return;
            //                }

            //                order.SampleState = CheckOrderStateEnum.Discard;
            //                order.State = DataState.Changed;
            //                rv = Encode.EncodeData.SaveDatas<SampleOrder>(new EncodeCollection<SampleOrder>(order));
            //                if (!rv.Success)
            //                {
            //                    ReturnValue.ShowError(rv.Message);
            //                }
            //                //ReturnValue.ShowMessage(rv);
            //                allEc.Remove(order);
            //            }
            //        }

            //        ReturnValue.ShowMessage(string.Format("操作成功，共有{0}个样品被废弃", ec.Count));
            //        _Grid.Refresh(true);
            //    }
            //}

            void GV_GridDbClick(object sender, GridViewCellEventArgs e)
            {
                if(!GV.ChildItemSelected && GV.ParentItemSelected)
            {
                var mminorder = GV.SelectedItem;
                if(mminorder.OrderState == MMDefInOrderStateEnum.Discard)
                {
                    MessageBox.Show("该工单已被废弃，无法添加样品工单");
                    return;
                }
                CreateSampleOrder(mminorder);
                return;
            }
                if(GV.ChildItemSelected && !GV.ParentItemSelected)
            {
                var sampleorder = GV.ChildSelectedItem;
                if(sampleorder.SampleState== QMBase.CheckOrderStateEnum.Discard)
                {
                    MessageBox.Show("该工单已被废弃，无法添加检测工单");
                    return;
                }
                CreateCheckOrderForm form = new CreateCheckOrderForm(sampleorder);
                form.ShowDialog();
                return;
            }
            }

            void GV_GridClick(object sender, GridViewCellEventArgs e)
            {
                if (GV.ParentItemSelected && !GV.ChildItemSelected)
                {
                    btnAddItem.Enabled = true;
                    btnAddItem.Enabled = true;
                    tsbDel.Enabled = true;
                    tsbDiscard.Enabled = true;
                   // tsbEdit.Enabled = true;

                }
                else if (GV.ChildItemSelected && !GV.ParentItemSelected)
                {
                    btnAddItem.Enabled = false;
                    btnAddItem.Enabled = false;
                    tsbDel.Enabled = false;
                    tsbDiscard.Enabled = false;
                // tsbEdit.Enabled = false;
            }
                else
                {
                    btnAddItem.Enabled = false;
                    btnAddItem.Enabled = false;
                    tsbDel.Enabled = false;
                    tsbDiscard.Enabled = false;
                //tsbEdit.Enabled = false;
            }

            }

        public void CreateSampleOrder(MMInOrder mminorder)
        {

            var order = new SampleOrder
            {
                SampleQuantity = 50,
                SampleDate = DateTime.Today.ToString(EncodeConst.DateFormat),
                //SamplerPK = 1,
                State = DataState.New,
                SourceOrderID = mminorder.CheckLot,
                DefPK = mminorder.DefPK,
                SupPK=mminorder.SupPK,
                LotID=mminorder.BatchID,
                SampleType = SampleTypeEnum.PackagesComming
            };
            
            CreateSampleOrderForm form = new CreateSampleOrderForm(order);
            if (form.ShowDialog() == DialogResult.OK)
            {
                GV.ChildDatas.Add(form.OrderItem);
                GV.Refresh(GV.MasterDatas, GV.ChildDatas);
            }
        }
    }
}
