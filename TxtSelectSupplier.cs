using SSIT.QualityManage.Interface;
using SSITControls.Common;
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
    [ToolboxItem(true)]
    [Designer(typeof(ForbidHeightDesigner)), DefaultEvent("Click")]
    public partial class TxtSelectSupplier : SSITControls.SelectTextBox.SelectTextbox
    {
        public List<int> supplier = null;
        public TxtSelectSupplier()
        {
            InitializeComponent();
        }

        private void TxtSelectDefinition_SetValueChanged(object sender, ValueEventArgs e)
        {
            Supplier sup = Supplier.Instance.Datas.FirstOrDefault(x => x.SupPK == (int)e.Value);
            if (sup != null)
            {
                string Value = sup.ParamName;
                this.Value = Value;
                this.Tag = sup;
            }
        }

        private void TxtSelectDefinition_Click(object sender, EventArgs e)
        {
            
            SelectSupplierForm form = new   SelectSupplierForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                Supplier sam = form.SelectedItem;
                if (sam != null)
                {
                    SetValue(sam.SupPK);
                    //this.Value = mm.ParamName;
                    //this.Tag = mm;

                }
            }
        }
    }
}
