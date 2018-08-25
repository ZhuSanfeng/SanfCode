using SSIT.QualityManage.Interface;
using SSIT.EncodeBase;
using SSIT.MM;
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

namespace SSIT.QualityManage.UI
{
    public partial class DefInOrderModifyForm : Telerik.WinControls.UI.RadForm
    {
        MMInOrder OrderItem = new MMInOrder();
        public DefInOrderModifyForm(MMInOrder order)
        {
            InitializeComponent();
            OrderItem = order;
            rtbPurOrderID.Text = OrderItem.PurchaseOrderPK;
            var def = MMDefinition.Instance.Datas.FirstOrDefault(p => p.Enable && p.DefID == OrderItem.DefID);
            stbMMDef.Value = MMDefinition.Instance.GetNamebyID(def.ParamID);

            var sup = Supplier.Instance.Datas.FirstOrDefault(p => p.Enable && p.SupplierID == OrderItem.SupID);
            stbSupplier.Value = Supplier.Instance.GetNamebyID(sup.ParamID);
            rtbCheckLot.Text = OrderItem.CheckLot;
            rtbBatch.Text = OrderItem.BatchID;
            rtbCarID.Text = OrderItem.CarID;
            rtbCount.Text = OrderItem.DefCount.ToString();
            rtbNote.Text = OrderItem.Note;

        }

        private void stbMMDef_Click(object sender, EventArgs e)
        {
            List<int> idl = new List<int>();
            SelectMMForm form = new SelectMMForm(idl);
            form.SetMultiSelect(false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var mm = form.SelectedItem;
                if (mm != null)
                {
                    stbMMDef.Value = mm.DefName;
                }
            }
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
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("确定修改？"
            , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
            {
                return;
            }
            if (stbMMDef.Value.IsNullOrWhiteSpace())
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
            if (rtbBatch.Text.IsNullOrWhiteSpace())
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
                MessageBox.Show("请输入检验批次");
                return;
            }
            if (rtbCarID.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("请输入车号");
                return;
            }
            OrderItem.SynTime = DateTime.Now.ToString(EncodeConst.DateTimeFormat);//获取当前时间
           // OrderItem.OrderID = MMInOrder.GetNewOrderID(System.DateTime.Today);
            OrderItem.BatchID = rtbBatch.Text;
            OrderItem.CarID = rtbCarID.Text;
            OrderItem.DefCount = int.Parse(rtbCount.Text);
            OrderItem.PurchaseOrderPK = rtbPurOrderID.Text;
            OrderItem.CheckLot = rtbCheckLot.Text;
            var sup = Supplier.Instance.Datas.FirstOrDefault(p => p.Enable && p.ParamName.Equals(stbSupplier.Value));
            OrderItem.SupPK = sup.ParamID;
            var mm = MMDefinition.Instance.Datas.FirstOrDefault(p => p.Enable && p.ParamName.Equals(stbMMDef.Value));
            OrderItem.DefPK = mm.ParamID;
            OrderItem.Note = rtbNote.Text;
            OrderItem.State = DataState.Changed;
            var rv=Encode.EncodeData.SaveDatas(OrderItem);
            if (rv.Success)
            {
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

        }
    }
}
