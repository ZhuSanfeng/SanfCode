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
    public class ucReportStat : ReportBaseUct
    {
        private EncodeCollection<CheckOrder> _gc;
        public ucReportStat()
            : base()
        {
            this.Title = "质量检验结果统计表";
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
            try
            {
                ReportView.ActiveWorksheet.Cells.Clear();
            }
            finally
            {
                ReportView.ReleaseLock();
            }
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
            //decisions
            List<int> listDecision = new List<int>();
            foreach (DataFieldAttribute field in srs.DecisionsFields)
            {
                //if (!listDecision.Contains(field.))
                //{
                //    listDecision.Add(field.ColumnName);
                //}
            }
            int colCount = listHead.Count + srs.StatFields.Count + listDecision.Count;
            int HeadCount = listHead.Count - 1;
            int tmpCol = HeadCount + srs.StatFields.Count + 1;
            ReportView.GetLock();

                ReportView.ActiveWorksheet.Cells.Clear();
                ReportView.ActiveWorksheet.Name = "质检工单统计表";
                SpreadsheetGear.IRange irange = ReportView.ActiveWorksheet.Cells;
                SpreadsheetGear.IBorders border = null;
                try
                {
                irange.HorizontalAlignment = SpreadsheetGear.HAlign.Left;
                irange.VerticalAlignment = SpreadsheetGear.VAlign.Center;

                irange[0, 0].Value = "中粮长城葡萄酒（烟台）有限公司";
                irange[1, 0].Value = "质量检验数据统计分析表";

                irange[2, 0].Value = string.Format("统计期间：{0} 至 {1}", gc[0].PlanCheckDate, gc[gc.Count - 1].PlanCheckDate);
                for (int i = 0; i < 2; i++)
                {
                    irange[i, 0, i, colCount].MergeCells = true;
                    irange[i, 0, i, colCount].HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                    irange[i, 0].Font.Bold = false;
                    irange[i, 0].Font.Size = 20;
                }
                for (int i = 0; i < listHead.Count; i++)
                {
                    irange[3, i].Value = listHead[i];//string.Join(" ", listHead.ToArray());
                    irange[3, i, 4, i].MergeCells = true;
                }

                 border = irange[2, 0, 2, colCount].Borders;
                border[SpreadsheetGear.BordersIndex.EdgeBottom].Weight = SpreadsheetGear.BorderWeight.Thin;
                border = irange[3, 0, 3, colCount].Borders;
                border[SpreadsheetGear.BordersIndex.EdgeBottom].Weight = SpreadsheetGear.BorderWeight.Thin;
                irange[3, 0, 3, colCount].Font.Bold = false;
                irange[3, 0, 3, colCount].Font.Size = 14;

                if (HeadCount < 0)
                {
                    HeadCount = 0;
                }
                for (int l = 0; l < srs.StatFields.Count; l++)
                {
                    irange[3, HeadCount + l + 1].Value = srs.StatFields[l].Description;
                    irange[3, HeadCount + l + 1].Font.Bold = false;
                    irange[3, HeadCount + l + 1, 4, HeadCount + l + 1].MergeCells = true;
                }
                //decisions

                if (listDecision.Count > 0)
                {
                    irange[3, tmpCol].Value = "使用决策情况统计";
                    irange[3, tmpCol, 3, colCount].HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                    irange[3, tmpCol, 3, tmpCol + listDecision.Count].MergeCells = true;
                    irange[4, tmpCol].Value = "合格率%";
                    for (int i = 1; i <= listDecision.Count; i++)
                    {
                        irange[4, tmpCol + i].Value = listDecision[i - 1];
                    }
                }
            }
            catch (Exception err)
            {
                ReturnValue.ShowMessage(err.Message);
                return;
            }
            finally
            {
                ReportView.ReleaseLock();
            }
            int newrow = 5;
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
                try
                {
                    for (int l = 0; l < listHead.Count; l++)
                    {
                        FieldValue fv = FieldManager.GetFieldValue(bs.Groups[0], listHead[l]);
                        if (fv != null)
                        {
                            irange[newrow, l].Value = fv.GetSafeValue();
                            irange[newrow, l].Font.Bold = true;
                        }
                    }
                    // irange[newrow, 0].Value = key;// bs.GetBrand();
                    border = irange[newrow, 0, newrow, colCount].Borders;
                    border[SpreadsheetGear.BordersIndex.EdgeTop].Weight = SpreadsheetGear.BorderWeight.Thin;


                    for (int l = 0; l < bs.Stats.Count; l++)
                    {

                        for (int j = 0; j < srs.StatFields.Count; j++)
                        {
                            object data = bs.Stats[l].GetFieldValue(srs.StatFields[j]);
                            if (data != null)
                            {
                                irange[newrow, j + HeadCount + 1].NumberFormat = SetNumberFormat(data.ToString());
                                irange[newrow, j + HeadCount + 1].Value = data;// bs.Stats[l].GetFieldValue(srs.StatFields[j]);
                            }
                        }
                        irange[newrow, tmpCol].Value = bs.Stats[l].UseGoodRate;
                        for (int i = 1; i <= listDecision.Count; i++)
                        {
                            irange[newrow, tmpCol + i].Value = bs.Stats[l].UsageDecisionStat(listDecision[i - 1]);
                        }
                    }

                    newrow += 1;
                }
                catch (Exception err)
                {
                    ReturnValue.ShowMessage(err.Message);
                    break;
                }
                finally
                {
                    ReportView.ReleaseLock();
                }
            }
            
            ReportView.GetLock();
            try
            {
                irange[4, 0, newrow, colCount].HorizontalAlignment = SpreadsheetGear.HAlign.Right;
                ReportView.ActiveWorksheetWindowInfo.SplitRows = 5;
                ReportView.ActiveWorksheetWindowInfo.FreezePanes = true;
                irange[3, 0, 5, colCount].Columns.AutoFit();
                ReportView.ActiveWorksheet.Cells.Rows.AutoFit();
            }
            finally
            {
                ReportView.ReleaseLock();
            }
        }

        #region IQuery 成员

        public override void Config()
        {
            EStatConfig form = new EStatConfig();
            if (form.ShowDialog() == DialogResult.OK)
            {
                QueryResult(_gc);
            }
        }
        #endregion

    }
}
