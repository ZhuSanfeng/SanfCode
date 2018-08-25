using SSIT.QualityManage.Interface;
using SSITEncode.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSIT.QualityManage.UI
{
    public partial class SelectSupplierForm : Telerik.WinControls.UI.RadForm
    {
        List<int> unIds;
        public SelectSupplierForm(List<int> Ids = null)
        {
            InitializeComponent();
            unIds = Ids;
            LoadInfo();
        }

        public void SetMultiSelect(bool bMulti)
        {
            if (bMulti)
            {
                
                flvSupplier.ShowCheckBoxes = true;
            }
            else
            {
               
                flvSupplier.ShowCheckBoxes = false;
            }
        }
        public Supplier SelectedItem
        {
            get
            {
                if (flvSupplier.ShowCheckBoxes)
                {
                    foreach (var data in flvSupplier.ListControl.CheckedItems)
                    {
                        return (Supplier)data.Value;
                    }
                }
                else
                {
                    foreach (var data in flvSupplier.ListControl.SelectedItems)
                    {
                        return (Supplier)data.Value;
                    }
                }
                return null;
            }
        }
        public void LoadInfo()
        {
            flvSupplier.ShowCheckBoxes = true;
            flvSupplier.ListControl.Columns.Clear();
            flvSupplier.ListControl.Columns.Add("SupplierID", "企业编号");
            flvSupplier.ListControl.Columns.Add("SupplierName", "企业名称");
            flvSupplier.ListControl.Columns[0].Width = 200;
            flvSupplier.ListControl.Columns[1].Width = 100;
            // flvSupplier.ListControl.ItemCheckedChanged += ListControl_ItemCheckedChanged;
            flvSupplier.Items.Clear();

            var ecSup = Supplier.Instance.Datas.Where(p => p.Enable && (unIds == null ? true : !unIds.Contains(p.ParamID)) && p.Enable).OrderBy(p => p.SupplierID);
            var lstDef = ecSup.ToList().ToEncodeCollection();
            flvSupplier.Fill<Supplier>(lstDef, new string[] { "SupplierID", "SupplierName" });

            if (flvSupplier.Items.Count > 0)
               flvSupplier.SelectedIndex = 0;
        }

        private void rb_ok_Click(object sender, EventArgs e)
        {

        }
    }
}
