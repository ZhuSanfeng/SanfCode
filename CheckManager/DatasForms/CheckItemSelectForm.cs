using SSIT.EncodeBase;
using SSIT.PropertyBase;
using SSIT.QM.CheckInterface;
using SSIT.QMBase;
using SSITEncode.QueryBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSIT.QM.CheckManager.DatasForms
{
    public partial class CheckItemSelectForm : Form
    {
        ObjectGrid<CheckOrder> sampleGrid;
        public CheckItemSelectForm(string lotid)
        {
            InitializeComponent();
            sampleGrid = new ObjectGrid<CheckOrder>();
            sampleGrid.Dock = DockStyle.Fill;
            radPanel1.Controls.Add(sampleGrid);
            sampleGrid.SelectedChanged += sampleGrid_SelectedChanged;
            sampleGrid.Selection.SelectionMode = SourceGrid.GridSelectionMode.Row;
            string clause = string.Format("lotid like '%{0}%' and ( sampleitemstate = {1} or sampleitemstate = {2})", lotid, (int)CheckOrderStateEnum.Complete, (int)CheckOrderStateEnum.Approve);
            var ec =  Encode.EncodeData.GetDatas<CheckOrder>(clause, "sampleid desc",20);
            sampleGrid.Fields = FieldSelectSettings<CheckOrder>.Instance.Fields.ToDescriptionList();
            sampleGrid.Init();
            sampleGrid.SetGrid(ec);
        }

        void sampleGrid_SelectedChanged(object sender, EventArgs e)
        {
            rbtOK.Enabled = sampleGrid.SelectedRow >= 0;
            SelectedSampleItem = sampleGrid.SelectedItem;
        }

        public CheckOrder SelectedSampleItem { get; set; }
    }
}
