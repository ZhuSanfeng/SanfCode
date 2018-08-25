using SSIT.EncodeBase;
using SSIT.QM.CheckInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SSIT.QM.CheckManager.StatReport
{
    public partial class SampleStatReportRadForm : Telerik.WinControls.UI.RadForm
    {
        ucReportStat report;
        public SampleStatReportRadForm()
        {
            InitializeComponent();
            report = new ucReportStat { Dock = DockStyle.Fill };
            this.radPanel1.Controls.Add(report);
        }

        public void SetData(EncodeCollection<CheckOrder> datas)
        {
            report.QueryResult(datas);
        }

        private void tsbConfig_Click(object sender, EventArgs e)
        {
            report.Config();
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            report.Export();
        }
    }
}
