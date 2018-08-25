using SSIT.EncodeBase;
using SSIT.Equipment.UI;
using SSIT.MM;
using SSIT.QM.SampleInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using YHDataInterface.Equipment;
using YHDataInterface.SSITMM;
using System.Linq;
using SSIT.UserInfo;
using SSIT.QMBase;
using SSIT.QM.CheckInterface;
using SSIT.QM.SampleManager.SettingForms;
using SSITEncode.Common;
using SSIT.QMBase.UI;

namespace SSIT.QM.CheckManager
{
    public partial class CreateCheckOrderForm : SSITControls.BaseAutoEditForm.BaseAutoEditForm
    {
        public CheckOrder OrderItem { get; set; }
        public List<CheckItem> SelectedCheckItems = new List<CheckItem>();
        public CreateCheckOrderForm()
        {
            InitializeComponent();
            ckbAuto.CheckState = CheckState.Checked;

            OrderItem = new CheckOrder
            {
                CheckQuantity = 1,
                
                SampleDate = DateTime.Today.ToString(EncodeConst.DateFormat),
                //SamplerPK = 1,
               State = DataState.New
            };
           // OrderItem.SampleType = SampleTypeEnum.Process;
            //OrderItem.SampleID = OrderItem.SampleID = EncodeHelper.GetOrderID<CheckOrder>("SampleID", "S");
            Init();

            //调用基类的RefreshForm方法 ，从实体获取数据 刷新界面上的控件
            RefreshForm<CheckOrder>(OrderItem); //调用基类方法，刷新控件值
        }

        public CreateCheckOrderForm(SampleTypeEnum type)
        {
            InitializeComponent();
            ckbAuto.CheckState = CheckState.Checked;

            OrderItem = new CheckOrder
            {
                CheckQuantity = 1,
                CheckType= CheckTypeEnum.Normal,
                SampleDate = DateTime.Today.ToString(EncodeConst.DateFormat),
                //SamplerPK = 1,
                State = DataState.New
            };
            OrderItem.SampleType = type;
            SampleType.Enabled = false;
            
            //OrderItem.SampleID = OrderItem.SampleID = EncodeHelper.GetOrderID<CheckOrder>("SampleID", "S");
            Init();

            //调用基类的RefreshForm方法 ，从实体获取数据 刷新界面上的控件
            RefreshForm<CheckOrder>(OrderItem); //调用基类方法，刷新控件值
        }


        public CreateCheckOrderForm(SampleOrder sample)
        {
            OrderItem =  CheckOrder.CreateCheckOrderbySample(sample);
            InitializeComponent();
            ckbAuto.CheckState = CheckState.Unchecked;
            GetSampleState.Text = sample.GetSampleState;
            GetSampleState.Enabled = false;
            SampleType.Enabled = false;
            OrderItem.CheckType = CheckTypeEnum.Normal;
            Init();
            //调用基类的RefreshForm方法 ，从实体获取数据 刷新界面上的控件
            RefreshForm<CheckOrder>(OrderItem); //调用基类方法，刷新控件值

        }
            /// <summary>
            /// 初始化 部分控件的属性 或数据源
            /// </summary>
            private void Init()
        {
            lvSeleted.Groups.Clear();
            lvSeleted.Items.Clear();
            SampleType.DataSource = SampleTypeClass.Datas;
            SampleType.DisplayMember = "Value";
            SampleType.ValueMember = "Key";
            CheckType.DataSource = CheckTypeClass.Datas;
            CheckType.DisplayMember = "Value";
            CheckType.ValueMember = "Key";
            //设置编辑状态，可以编辑的控件
            // dicStateEnabledControls.Add((int)OrderItem.SampleState, new List<Control> { });
            List<Control> lstEditingHealthCheckedControl = new List<Control> { DefPK };
            dicHealthCheckedControls.Add((int)CheckOrderStateEnum.Editing, lstEditingHealthCheckedControl);
            DefPK.MMTypes = new List<MMTypeEnum>(new MMTypeEnum[] { YHDataInterface.SSITMM.MMTypeEnum.包装物 });
        }

        #region 工单数据操作 ,刷新 健康检查 ，保存 等
        private bool HealthChecked()
        {
            var b = HealthChecked((int)OrderItem.CheckOrderState);
            if (!b)
            {
                ReturnValue.ShowMessage("请选择物料!");
            }
            return b;
            
        }
        private ReturnValue Save()
        {
            ReturnValue rv = new ReturnValue(false);
            RefreshDatas<CheckOrder>(OrderItem);
            string plancheckitemstring = "";
            var spectorlist = ucOperator1.SelectedUsers;
            var inspectoridlist = spectorlist.Select(p => p.ParamName).ToList();
            int userflag = 0;
            //保存计划检测项目
            if (SelectedCheckItems != null && SelectedCheckItems.Count > 0)
            {
                var list = SelectedCheckItems.Select(p => p.ParamID).ToList();
                 plancheckitemstring = list.ListIntToString();
            }
            if(rcbMutiOrder.Checked)
            {
                Dictionary<int, List<CheckItem>> dic = new Dictionary<int, List<CheckItem>>();
                foreach (var item in SelectedCheckItems)
                {
                    if (!dic.ContainsKey(item.CheckCategoryID))
                    {
                        dic.Add(item.CheckCategoryID, new List<CheckItem>());
                    }
                    dic[item.CheckCategoryID].Add(item);
                }
                EncodeCollection<CheckOrder> ecOrder = new EncodeCollection<CheckOrder>();
                foreach (var items in dic.Values)
                {
                    var order = (CheckOrder)OrderItem.Clone();
                    order.IDENTITY = null;
                    if (spectorlist.Count != 0)
                    {
                        var userorder = userflag % spectorlist.Count();
                        order.PlanInspector = spectorlist[userflag].ParamName;
                        userflag++;
                    }
                    var list = items.Select(p => p.ParamID).ToList();
                    plancheckitemstring = list.ListIntToString();
                    order.PlanCheckItemString = plancheckitemstring;
                    if (order.State == DataState.New)
                    {
                        
                        order.PlanCheckItemString = plancheckitemstring;
                        order.CreateTime = DateTime.Now.ToString(EncodeConst.DateTimeFormat);
                        order.Creator = User.CurrentUser?.ParamName;
                        order.CheckOrderID = EncodeHelper.GetOrderID<CheckOrder>("CheckOrderID", "C");
                        if (ckbAuto.Checked)
                        {
                            order.SampleID = EncodeHelper.GetOrderID<CheckOrder>("SampleID", "S");
                        }
                    }
                    
                    order.CheckOrderState = CheckOrderStateEnum.Submit;
                    rv = EncodeBase.Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(order));
                    if (!rv.Success)
                    {
                        ReturnValue.ShowMessage("样品创建失败:" + rv.Message);
                        return rv;
                    }
                }
                return rv;
            }
            
            
            //OrderItem.SampleType = (SampleTypeEnum)SampleType.SelectedValue;
            if (OrderItem.State == DataState.New)
            {
                OrderItem.PlanInspector = inspectoridlist.ListToString();
                OrderItem.PlanCheckItemString = plancheckitemstring;
                OrderItem.CreateTime = DateTime.Now.ToString(EncodeConst.DateTimeFormat);
                OrderItem.Creator = User.CurrentUser?.ParamName;
                OrderItem.CheckOrderID= EncodeHelper.GetOrderID<CheckOrder>("CheckOrderID", "C");
                if (ckbAuto.Checked)
                {
                    OrderItem.SampleID = EncodeHelper.GetOrderID<CheckOrder>("SampleID", "S");
                }

            }
            else if(OrderItem.PlanCheckItemString != plancheckitemstring)
            {
                OrderItem.PlanInspector = inspectoridlist.ListToString();
                OrderItem.PlanCheckItemString = plancheckitemstring;
                OrderItem.State = DataState.Changed;
            }
            OrderItem.CheckOrderState = CheckOrderStateEnum.Submit;
            rv = EncodeBase.Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(OrderItem));
            if (!rv.Success)
            {
                ReturnValue.ShowMessage("样品创建失败:" + rv.Message);
                return rv;
            }
            return rv;
            //修改
        }
        #endregion

        #region 相关事件
        /// <summary>
        /// 自定义控件，必须有SetValueChanged ，基类才会自动调用 ，给自定义控件赋值
        /// 控件名MMDefPK ，和实体里的MMDefPK属性名一致
        /// 加载时会将实体的MMDefPK属性（int类型)，在参数e中传递过来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DefPK_SetValueChanged(object sender, SSITControls.Common.ValueEventArgs e)
        {
            var checkItems = DefinitionCheckItemCombine.GetCheckItemsby((int)e.Value, MMTypEnum.QM);// .GetFirstItem("defpk = " + (int)e.Value);
            LoadCheckItems(checkItems);
        }


        private void cbAuto_CheckedChanged(object sender, EventArgs e)
        {
            SampleID.ReadOnly = ckbAuto.Checked;
        }
        #endregion

        private void radButton_Save_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("确定保存"
            , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
            {
                return;
            }
            if (HealthChecked())
            {
               
                var rv = Save();
                if (rv.Success)
                {
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {

            }
        }

        private void rbtSelectCheckItems_Click(object sender, EventArgs e)
        {
            AllCheckItemSelectionForm form = new AllCheckItemSelectionForm(SelectedCheckItems, MMTypEnum.All);
            
            if (form.ShowDialog() == DialogResult.OK)
            {
                var checkItems = form.SelectedCheckItems;
                LoadCheckItems(checkItems);
            }
        }

        void LoadCheckItems(List<CheckItem> checkItems)
        {
            SelectedCheckItems = checkItems;
            lvSeleted.Groups.Clear();
            lvSeleted.Items.Clear();
            
            foreach (var item in checkItems)
            {
                if (!lvSeleted.Items.ContainsKey(item.ParamID.ToString()))
                {
                    ListViewItem lvi = new ListViewItem(item.ToString());
                    lvi.Name = item.ParamID.ToString();
                    lvi.Tag = item;
                    string categoryname = item.GetCheckCategory();
                    if (lvSeleted.Groups[categoryname] == null)
                    {
                        ListViewGroup group = new ListViewGroup(categoryname, categoryname);
                        lvSeleted.Groups.Add(group);
                    }
                    lvi.Group = lvSeleted.Groups[categoryname];
                    lvSeleted.Items.Add(lvi);
                }
            }
        }

    }
}
