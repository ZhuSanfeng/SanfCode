using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSIT.PropertyBase;
using System.Windows.Forms;
using SSIT.EncodeBase;
using SSITEncode.TextParser;
using System.IO;
using System.Drawing;
using SSIT.QualityManage.Interface;
using SSIT.QualityManage.Settings;

namespace SSIT.QualityManage.Settings
{
    public partial class SupplierPage : UCSupplierPage
    {
        public override ReturnValue SaveDatas()
        {
            return base.SaveDatas();
        }
        public SupplierPage()
            : base()
        {
            //grid.ColumnName = Text;
            this.BasePanel.Controls.Add(grid);
            grid.Dock = DockStyle.Fill;
            //grid.Init();
            this.Text = "供应商";						//Page名称
        }

    }

    public class UCSupplierPage : SettingPagePanel<SupplierGrid, Supplier>	//将Grid添加到Page上
    {

    }
}
