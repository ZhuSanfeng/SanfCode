using SSIT.EncodeBase;
using SSIT.MM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using System.Linq;
using SSITEncode.Common;
using YHDataInterface.SSITMM;
using SSIT.QualityManage.Interface;

namespace SSIT.QualityManage.UI
{
    public partial class SelectSupandMMForm : Telerik.WinControls.UI.RadForm
    {
        List<int> unIds;
        List<MMTypeEnum> CategoryIds;

        public SelectSupandMMForm( List<int> Ids = null)
        {
            InitializeComponent();
            unIds = Ids;
            LoadInfo();
        }

        public SelectSupandMMForm(List<MMTypeEnum> categoryIds = null,List<int> Ids = null)
        {
            InitializeComponent();
            unIds = Ids;
            CategoryIds = categoryIds;
            LoadInfo();
        }

        public void SetMultiSelect(bool bMulti)
        {
            if(bMulti)
            {
                rbSelectAll.Visible = true;
                rbSelectNone.Visible = true;
                rlvMM.ShowCheckBoxes = true;
            }
            else
            {
                rbSelectAll.Visible = false;
                rbSelectNone.Visible = false;
                rlvMM.ShowCheckBoxes = false;
            }
        }

        public void LoadInfo()
        {
            rlvMM.ShowCheckBoxes = true;
            rlvMM.ListControl.Columns.Clear();
            rlvMM.ListControl.Columns.Add("DefID", "物料编号");
            rlvMM.ListControl.Columns.Add("Uom", "物料名称");
            rlvMM.ListControl.Columns[0].Width = 200;
            rlvMM.ListControl.Columns[1].Width = 100;
            rlvMM.ListControl.ItemCheckedChanged += ListControl_ItemCheckedChanged;
            rlvMM.Items.Clear();

            rlcSup.Items.Clear();
           

            rlcSup.BeginUpdate();

            foreach (var type in Supplier.Instance.GetEnableCollection())
            {
                //if (CategoryIds == null || CategoryIds.Contains((MMTypeEnum)type.MMCategory))
                //{
                //    rlcMMType.Items.Add(new RadListDataItem(type.ParamName, type.SupPK));
                //}
                rlcSup.Items.Add(new RadListDataItem(type.ParamName, type.SupPK));
            }
            rlcSup.EndUpdate();
            if (rlcSup.Items.Count > 0)
            {
                rlcSup.SelectedIndex = 0;
            }
        }

        private void ListControl_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (rbSelectAll.Visible)
            {
                rbtOK.Enabled = (rlvMM.CheckedItems.Count > 0);
            }
        }
        private void rlvMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!rbSelectAll.Visible)
            {
                rbtOK.Enabled = (rlvMM.SelectedIndex >= 0);
            }
        }

        private void SetToggleState(ToggleState State)
        {
            var Datas = rlvMM.Items;
            foreach (Telerik.WinControls.UI.ListViewDataItem data in Datas)
            {
                data.CheckState = State;
            }
        }

        private void rbSelectAll_Click(object sender, EventArgs e)
        {
            SetToggleState(ToggleState.On);
        }

        private void rbSelectNone_Click(object sender, EventArgs e)
        {
            SetToggleState(ToggleState.Off);
        }

        public EncodeCollection<SupAndMMRelation> SelectedItems
        {
            get
            {
                EncodeCollection<SupAndMMRelation> ec = new EncodeCollection<SupAndMMRelation>();
                if (rlvMM.ShowCheckBoxes)
                {
                    foreach (var data in rlvMM.ListControl.CheckedItems)
                    {
                        ec.Add((SupAndMMRelation)data.Value);
                    }
                    return ec;
                }
                else
                {
                    foreach (var data in rlvMM.ListControl.SelectedItems)
                    {
                        ec.Add((SupAndMMRelation)data.Value);
                    }
                    return ec;
                }
            }
        }

        public SupAndMMRelation SelectedItem
        {
            get
            {
                if (rlvMM.ShowCheckBoxes)
                {
                    foreach (var data in rlvMM.ListControl.CheckedItems)
                    {
                        return (SupAndMMRelation)data.Value;
                    }
                }
                else
                {
                    foreach (var data in rlvMM.ListControl.SelectedItems)
                    {
                        return (SupAndMMRelation)data.Value;
                    }
                }
                return null;
            }
        }
        private void rlcMMType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            group.Enabled = (rlcSup.SelectedIndex >= 0);
            if (rlcSup.SelectedIndex >= 0)
            {
                int suppk = (int)rlcSup.SelectedItem.Value;
                rlvMM.Items.Clear();
                var ecDef = SupAndMMRelation.Instance.Datas.Where(p => p.Enable &&  p.SupPK == suppk).OrderBy(p => p.DefID);

                var lstDef = ecDef.ToList().ToEncodeCollection();
                rlvMM.Fill<SupAndMMRelation>(lstDef, new string[] { "DefID", "DefName" });

                if (rlvMM.Items.Count > 0)
                    rlvMM.SelectedIndex = 0;

            }

        }

       
        private void rbtOK_Click(object sender, EventArgs e)
        {

        }

        private void rbtCancel_Click(object sender, EventArgs e)
        {

        }


    }
}
