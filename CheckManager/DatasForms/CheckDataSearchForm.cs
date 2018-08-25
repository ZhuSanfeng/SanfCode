using SSIT.EncodeBase;
using SSIT.PropertyBase;
using SSIT.QM.CheckInterface;
using SSITEncode.QueryBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SSIT.QM.CheckManager.DatasForms
{
    public partial class CheckDataSearchForm : Telerik.WinControls.UI.RadForm
    {
        ObjectGrid<CheckData> _grid;
        public CheckDataSearchForm()
        {
            InitializeComponent();
            _grid = new ObjectGrid<CheckData> { Dock = DockStyle.Fill };
            _grid.Selection.SelectionMode = SourceGrid.GridSelectionMode.Row;
            radPanel1. Controls.Add(_grid);
            RefreshGrid();
        }

        void RefreshGrid()
        {
            _grid.Fields = FieldSelectSettings<CheckData>.Instance.Fields.ToDescriptionList();
            _grid.Init();
            _grid.SetGrid(_grid.Encodes);
        }

        private void tsbSetting_Click(object sender, EventArgs e)
        {
            FieldSelectForm<CheckData> form = new FieldSelectForm<CheckData>();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RefreshGrid();
            }
        }

        private void rbtQuery_Click(object sender, EventArgs e)
        {
            EncodeCollection<CheckData> ec = Encode.EncodeData.GetDatas<CheckData>();
            _grid.SetGrid(ec);
        }
    }
}
