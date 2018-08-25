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
using SSIT.MM;
using SSIT.MM.Settings;

namespace SSIT.QualityManage.Settings
{
    public partial class SupAndMMRelationPage : UCSupAndMMRelationPage
    {
        public override ReturnValue SaveDatas()
        {
            return base.SaveDatas();
        }
        public SupAndMMRelationPage()
            : base()
        {
            
            var btImport = new ToolStripButton("导入");
            btImport.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            btImport.Image = Properties.Resource.ImportItem;
            btImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btImport.Click += btImport_Click;
            toolStrip1.Items.Add(btImport);
            InitializeComponent();

            
            radGroupBox2.Controls.Add(grid);
            grid.Dock = DockStyle.Fill;
            //grid.Init();
            this.Text = "供应商与物料关联";						//Page名称
        }
        public override void RefreshGrid()
        {

            base.RefreshGrid();
            rlcSupplier.Items.Clear();
            foreach (Supplier data in Supplier.Instance.GetEnableCollection())//提取供应商数据
            {
               rlcSupplier.Items.Add(new Telerik.WinControls.UI.RadListDataItem(data.ParamName, data));
            }
            if (rlcSupplier.Items.Count > 0)
            {
                rlcSupplier.SelectedIndex = 0;
            }

        }

        private void btImport_Click(object sender, EventArgs e)
        {
            SelectMMDefForm form = new SelectMMDefForm();
            EncodeCollection<SupAndMMRelation> ec = new EncodeCollection<SupAndMMRelation>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var definitions = form.SelectedItems;
                if (definitions != null)
                {
                    foreach (var def     in definitions)
                    {
                       
                       if(SupAndMMRelation.Instance.Datas.FirstOrDefault(p=>p.Enable && p.DefPK ==def.DefPK && p.SupPK == grid.SupPK) == null) { 
                        SupAndMMRelation supAndMMRelation = new SupAndMMRelation
                        {
                            DefID = def.DefID,
                            SupPK = grid.SupPK,
                            ParamName = def.DefName,
                            State = DataState.New
                        };
                            grid.Encodes.Add(supAndMMRelation);
                        grid.InsertRow(grid.RowsCount-1, supAndMMRelation);
                       // ec.Add(supAndMMRelation);
                    }
                    }
                }
                
               // grid.InsertRow(,ec);
            }
            //List<MMDefinition> mmdefList = form.SelectedItems;
            
        }

        private void rlcSupplier_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (rlcSupplier.SelectedIndex < 0) return;
            var sup =(Supplier) rlcSupplier.SelectedItem.Value;
            grid.SupPK = sup.ParamID;
            grid.Init();
        }
    }

    public class UCSupAndMMRelationPage : SettingPagePanel<SupAndMMRelationGrid, SupAndMMRelation>	//将Grid添加到Page上
    {

    }
}
