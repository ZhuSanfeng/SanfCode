using SSIT.DataField;
using SSIT.EncodeBase;
using SSIT.QM.CheckInterface;
using SSIT.ReportControl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SSIT.QM.CheckManager.StatReport
{
    public class ucReportStat_old : ReportBaseUct
    {
        private EncodeCollection<CheckOrder> _gc;
        public ucReportStat_old()
            : base()
        {
            this.Title = "质检工单统计表";
        }

        public void QueryResult(EncodeCollection<CheckOrder> collection)
        {
            if (collection == null)
                return;           
            _gc = collection;
            SetReportView();
        }
        override protected void SetReportView()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new SetReportViewHandler(SetReportView));
                return;
            }
            ReportView.GetLock();
            ReportView.ActiveWorksheet.Cells.Clear();
            ReportView.ReleaseLock();

            SampleStatReportSettings srs = SampleStatReportSettings.Instance;

            if (_gc == null)
                return;
            //清空                 
            if (_gc.Count == 0) return;
            //按样品排序
            EncodeCollection<CheckOrder> gc = _gc.Copy();
            Dictionary<string, System.Reflection.PropertyInfo> dicFieldPI = new Dictionary<string, System.Reflection.PropertyInfo>();
            //DataFieldAttribute fBrand = new DataFieldAttribute { ColumnName = "brand", Description = "牌号" };
            //head
            List<string> listHead = new List<string>();
            foreach (DataFieldAttribute field in srs.HeadFields)
            {
                if ( !dicFieldPI.ContainsKey(field.Description))
                {
                    dicFieldPI.Add(field.Description, FieldManager.FieldToProperty(typeof(CheckOrder), field.Description));
                    listHead.Add(field.Description);
                }
            }

            //关键字分类                
            Dictionary<string, BrandStat> diccon2 = new Dictionary<string, BrandStat>();
            Dictionary<string, BrandStat> diccon = new Dictionary<string, BrandStat>();
            StringBuilder sbHead = new StringBuilder();
            List<string> lsHead = new List<string>();
            foreach (CheckOrder cggroup in gc)
            {
                if (sbHead.Length > 0)
                    sbHead.Remove(0, sbHead.Length);
                foreach (DataFieldAttribute field in srs.HeadFields)
                {
                    object value = null;
                    if (dicFieldPI[field.Description] != null)
                        value = dicFieldPI[field.Description].GetValue(cggroup, null);
                    string valuestr = "";
                    if (value != null)
                        valuestr = value.ToString().Trim();
                    sbHead.Append(valuestr + " ");
                }
                if (!diccon2.ContainsKey(sbHead.ToString()))
                {
                    diccon2.Add(sbHead.ToString(), new BrandStat());
                    diccon2[sbHead.ToString()].Groups.Add(cggroup);
                    lsHead.Add(sbHead.ToString());
                }
                else
                    diccon2[sbHead.ToString()].Groups.Add(cggroup);
            }
            lsHead.Sort();
            for (int i = 0; i < lsHead.Count; i++)
            {
                diccon.Add(lsHead[i], diccon2[lsHead[i]]);
            }

            if (diccon.Count == 0) return;
            foreach (BrandStat bs in diccon.Values)
                bs.Init();

            ReportView.GetLock();
            ReportView.ActiveWorksheet.Cells.Clear();
            ReportView.ActiveWorksheet.Name = "质检工单统计表";
            SpreadsheetGear.IRange irange = ReportView.ActiveWorksheet.Cells;
            irange.HorizontalAlignment = SpreadsheetGear.HAlign.Left;
            irange.VerticalAlignment = SpreadsheetGear.VAlign.Center;
            irange[0, 0].Value = "质检工单统计表";
            irange[0, 0, 0, listHead.Count + srs.StatFields.Count].MergeCells = true;
            irange[0, 0, 0, listHead.Count + srs.StatFields.Count].HorizontalAlignment = SpreadsheetGear.HAlign.Center;
            for (int i = 0; i < listHead.Count; i++)
            {
                irange[1, i].Value = listHead[i];//string.Join(" ", listHead.ToArray());
            }
            //irange[1, listHead.Count].Value = "项目";
            irange[0, 0].Font.Bold = false;
            irange[0, 0].Font.Size = 18;

            SpreadsheetGear.IBorders border = irange[0, 0, 0, srs.StatFields.Count + listHead.Count].Borders;
            border[SpreadsheetGear.BordersIndex.EdgeBottom].Weight = SpreadsheetGear.BorderWeight.Thin;
            border = irange[1, 0, 1, srs.StatFields.Count + listHead.Count].Borders;
            border[SpreadsheetGear.BordersIndex.EdgeBottom].Weight = SpreadsheetGear.BorderWeight.Thin;
            irange[1, 0, 1, srs.StatFields.Count + listHead.Count].Font.Bold = false;
            irange[1, 0, 1, srs.StatFields.Count + listHead.Count].Font.Size = 14;
            
            int HeadCount = listHead.Count -1;
            if (HeadCount < 0)
            {
                HeadCount = 0;
            }
            for (int l = 0; l < srs.StatFields.Count; l++)
            {
                irange[1, HeadCount+l+1].Value = srs.StatFields[l].Description;
                irange[1, HeadCount + l + 1].Font.Bold = false;
            }
            ReportView.ReleaseLock();

            int newrow = 2;
           // List<BrandStat> listBS = new List<BrandStat>(diccon.Values);
            List<string> listBS = new List<string>(diccon.Keys);
            listBS.Sort();

            foreach(string key in listBS)
            {
                BrandStat bs = diccon[key];
                if (bAbort)
                {
                    break;
                }

                if (bs.Stats.Count == 0)
                    continue;

                Application.DoEvents();

                ReportView.GetLock();
                for (int l = 0; l < listHead.Count; l++)
                {
                  FieldValue fv =  FieldManager.GetFieldValue(bs.Groups[0], listHead[l]);
                  if (fv != null)
                  {
                    irange[newrow,l].Value =  fv.GetSafeValue();
                    irange[newrow, l].Font.Bold = true;
                  }
                }
               // irange[newrow, 0].Value = key;// bs.GetBrand();
                border = irange[newrow, 0, newrow, srs.StatFields.Count+HeadCount].Borders;
                border[SpreadsheetGear.BordersIndex.EdgeTop].Weight = SpreadsheetGear.BorderWeight.Thin;



                for (int l = 0; l < bs.Stats.Count; l++)
                {

                    int row = 0;

                    for (int j = 0; j < srs.StatFields.Count; j++)
                    {
                        object data = bs.Stats[l].GetFieldValue(srs.StatFields[j]);
                        irange[newrow + row , j+HeadCount+1].NumberFormat = SetNumberFormat(data.ToString());
                        irange[newrow + row, j + HeadCount + 1].Value = data;// bs.Stats[l].GetFieldValue(srs.StatFields[j]);
                    }
                }
                newrow += 1;
                ReportView.ReleaseLock();
            }
            ReportView.GetLock();
            ReportView.ActiveWorksheetWindowInfo.SplitRows = 2;
            ReportView.ActiveWorksheetWindowInfo.FreezePanes = true;
            ReportView.ActiveWorksheet.Cells.Columns.AutoFit();
            ReportView.ReleaseLock();
        }

        #region IQuery 成员

        public override void Config()
        {
            StatConfig form = new StatConfig();
            if (form.ShowDialog() == DialogResult.OK)
            {
                QueryResult(_gc);
            }
        }
        #endregion

    }
}
