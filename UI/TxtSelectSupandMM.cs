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
    public partial class TxtSelectSupandMM : SSITControls.SelectTextBox.SelectTextbox
    {
        public List<int> supplier = null;
        public TxtSelectSupandMM()
        {
            InitializeComponent();
        }

        private void TxtSelectDefinition_SetValueChanged(object sender, ValueEventArgs e)
        {
            MMDefinition mm = MMDefinition.Instance.Datas.FirstOrDefault(x => x.DefPK == (int)e.Value);
            if (mm != null)
            {
                string Value = mm.ParamName;
                this.Value = Value;
                this.Tag = mm;
            }
        }

        private void TxtSelectDefinition_Click(object sender, EventArgs e)
        {
            List<int> Ids = null;
            SelectSupandMMForm form = new  SelectSupandMMForm(Ids);

            if (form.ShowDialog() == DialogResult.OK)
            {
                SupAndMMRelation sam = form.SelectedItem;
                if (sam != null)
                {
                    SetValue(sam.DefPK);
                    //this.Value = mm.ParamName;
                    //this.Tag = mm;

                }
            }
        }
    }
}
