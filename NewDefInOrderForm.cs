using SSIT.QualityManage.Interface;
using SSIT.EncodeBase;
using SSIT.MM;
using SSIT.UserInfo;
using SSITEncode.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YHDataInterface.SSITMM;
using SSIT.QualityManage.Function;
using SSIT.QM.SampleInterface;

namespace SSIT.QualityManage.UI
{
    public partial class NewDefInOrderForm : Telerik.WinControls.UI.RadForm
    {
      public   MMInOrder OrderItem = new MMInOrder();
        public SampleOrder newSampleOrder = new SampleOrder(); 
        public NewDefInOrderForm()
        {
            InitializeComponent();
            DefPK.MMTypes=new List<MMTypeEnum>(new MMTypeEnum[] { YHDataInterface.SSITMM.MMTypeEnum.包装物 });

            OrderItem.State = DataState.New;
            OrderItem.Creator = User.CurrentUser?.ParamName;

        }


       

        private void stbSupplier_Click(object sender, EventArgs e)
        {
            List<int> idl = new List<int>();
            SelectSupplierForm form = new SelectSupplierForm(idl);
            form.SetMultiSelect(false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var sup = form.SelectedItem;
                if (sup != null)
                {
                    stbSupplier.Value = sup.SupplierName;
                }
            }
        }

        private void rt_Ok_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("确定保存？"
            , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
            {
                return;
            }
            if (DefPK.Value.IsNullOrWhiteSpace())
            {
                MessageBox.Show("请选择物料");
                return;
            }
            if (stbSupplier.Value.IsNullOrWhiteSpace())
            {
                MessageBox.Show("请选择供应商");
                return;
            }
            if (rtbPurOrderID.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("请输入采购单号");
                return;
            }
            if (rtbLot.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("请输入批次");
                return;
            }
            if (rtbCount.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("请输入数量");
                return;
            }
            if (rtbCheckLot.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("请输入检验批次号");
                return;
            }
            else
            {
                var items = MMInOrder.Instance.Datas.FirstOrDefault(p=>p.CheckLot==rtbCheckLot.Text);
                if (items != null)
                {
                    MessageBox.Show("检验批次号已存在！");
                    return;
                }
            }
            if (rtbCarID.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("请输入车号");
                return;
            }
            OrderItem.SynTime = DateTime.Now.ToString(EncodeConst.DateTimeFormat);//获取当前时间
            OrderItem.OrderID = MMInOrder.GetNewOrderID(System.DateTime.Today);
            OrderItem.BatchID = rtbLot.Text;
            OrderItem.CarID = rtbCarID.Text;
            OrderItem.DefCount = rtbCount.Text.DBValueToString().ToInt();
            OrderItem.PurchaseOrderPK = rtbPurOrderID.Text;
            OrderItem.CheckLot = rtbCheckLot.Text;
            var sup = Supplier.Instance.Datas.FirstOrDefault(p => p.Enable && p.ParamName.Equals(stbSupplier.Value));
            OrderItem.SupPK = sup.ParamID;
            var mm = MMDefinition.Instance.Datas.FirstOrDefault(p => p.Enable && p.ParamName.Equals(DefPK.Value));
            OrderItem.DefPK =mm.DefPK;
            OrderItem.Note = rtbNote.Text;
            OrderItem.OrderState = MMDefInOrderStateEnum.Creat;
            var rv =Encode.EncodeData.SaveDatas(OrderItem);
            if (rv.Success)
            {
               
                AutoCreateOrder aco = new AutoCreateOrder();
                aco.CreateSampleOrderByMMInOrder(OrderItem, false);
                newSampleOrder = aco.newSampleOrder;
                DialogResult = DialogResult.OK;
            }
            else
            {
                ReturnValue.ShowMessage(rv);
            }

            this.Close();
        }

        private void rb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
