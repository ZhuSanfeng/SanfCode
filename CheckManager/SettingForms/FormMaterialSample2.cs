using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpreadsheetGear;
using SpreadsheetGear.Shapes;
using SpreadsheetGear.Windows.Forms;
using SSIT.EncodeBase;
using SSIT.QM.CheckInterface;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using ThoughtWorks.QRCode.Codec;
using SSIT.QMBase;

namespace SSIT.QM.SampleManager.SettingForms
{
    public partial class FormMaterialSample2 : RadForm
    {
        public FormMaterialSample2()
        {
            InitializeComponent();
            InitUI();
        }
        //private void Load(object sender, EventArgs e)
        //{
        //    Encode.EncodeData = new SSIT.Bread.ClientEncodeData();
        //    EncodeCollection<SSIT.QM.CheckInterface.SampleItemOrder> ec =
        //        SSIT.EncodeBase.Encode.EncodeData.GetDatas<SSIT.QM.CheckInterface.SampleItemOrder>();
        //    SampleItemOrder obj = ec[4];
        //    LoadBarcode(obj, new string[] { "RS", "TA", "TSO2", "VA", "YSO2", "106项农残", "Alc", "CO2", "Cu", "DE", "Fe", "FSO2", "Pb", "PH", "RS", "TA", "TCA", "TCA释放量", "TSO2", "VA", "YSO2", "氨基甲酸乙酯", "拔塞力", "百粒重" });
        //}

        public void InitUI()
        {

            wbv.GetLock();
            SpreadsheetGear.IWorkbookWindowInfo windowInfoBook = wbv.ActiveWorkbookWindowInfo;
            windowInfoBook.DisplayHorizontalScrollBar = !windowInfoBook.DisplayHorizontalScrollBar;
            windowInfoBook.DisplayVerticalScrollBar = !windowInfoBook.DisplayVerticalScrollBar;
            windowInfoBook.DisplayWorkbookTabs = !windowInfoBook.DisplayWorkbookTabs;

            SpreadsheetGear.IWorksheetWindowInfo windowInfoSheet = wbv.ActiveWorksheetWindowInfo;
            windowInfoSheet.DisplayHeadings = !windowInfoSheet.DisplayHeadings;

            SpreadsheetGear.IPageSetup pageSetup = wbv.ActiveWorksheet.PageSetup;
            pageSetup.FitToPages = true;
            pageSetup.FitToPagesTall = 10;
            pageSetup.FitToPagesWide = 8;
            pageSetup.CenterHorizontally = true;
            pageSetup.CenterVertically = true;
            pageSetup.Orientation = PageOrientation.Portrait;
            pageSetup.BottomMargin = 1;
            pageSetup.LeftMargin = 1;
            pageSetup.RightMargin = 1;
            pageSetup.TopMargin = 1;
            wbv.ReleaseLock();
        }

        string[] lsName = new string[] {"物料批次", "样品编号",
           "检验日期", "取样地点","样品数量","检验员"};
        public void SheetClear(WorkbookView wbv)
        {
            wbv.GetLock();
            wbv.ActiveWorksheet.Cells.Clear();
            wbv.ActiveWorksheet.Cells.Borders.Color = SpreadsheetGear.Colors.White;
            foreach (IShape shape in wbv.ActiveWorksheet.Shapes)
            {
                shape.Delete();
            }
            wbv.ReleaseLock();
        }

        public void LoadBarcode(CheckOrder Obj)
        {
            try
            {
                string deflot = string.Format("{0} {1}({2})", Obj.LotID, Obj.DefinitionName, Obj.DefinitionID);
                string[] colValues = new string[]
                {
                    deflot,Obj.SampleID,
                    Obj.PlanCheckDate, Obj.HutName,Obj.CheckQuantity.ToString(),Obj.PlanInspector
                };
                if (Obj.GetPlanCheckItemCount > 0)
                {
                    List<string> listCheckItems = new List<string>();
                    foreach(short id in Obj.PlanCheckItems)
                    {
                      string name =  CheckItem.Instance.GetNamebyID(id);
                        if(!string.IsNullOrWhiteSpace(name) && !listCheckItems.Contains(name))
                        {
                            listCheckItems.Add(name);
                        }
                    }
                    LoadBarcode(wbv, "样品条码卡", lsName, colValues, Obj.SampleID, listCheckItems.ToArray());
                }

            }
            catch (Exception)
            {
            }

        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="wbv">传入WorkbookView控件</param>
        /// <param name="Title">标题</param>
        /// <param name="ColumnNames">表头列表</param>
        /// <param name="Values">值列表</param>
        /// <param name="QRCodeValue">二维码值</param>
        public void LoadBarcode(WorkbookView wbv, string Title,
            string[] ColumnNames, string[] Values, string QRCodeValue, string[] Checkouts)
        {
            double Proportion = 0.6;
            WorkbookViewSheetClear(wbv);

            wbv.GetLock();
            IRange range = wbv.ActiveWorksheet.Cells;
            range[0, 0].RowHeight = 42 * Proportion;
            range.Borders.LineStyle = LineStyle.None;
            range.Font.Name = "楷体";
            range.Font.Bold = true;
            range.HorizontalAlignment = HAlign.Left;
            range.Font.Size = 12 * Proportion;
            range.ColumnWidth = 10 * Proportion;
            range[0, 0, 0, 4].Merge();
            range[0, 0].RowHeight = 40 * Proportion;
            range[0, 0].HorizontalAlignment = HAlign.Center;
            range[0, 0].VerticalAlignment = VAlign.Center;
            range[0, 0].Value = Title;
            range[0, 0].Font.Size = 36 * Proportion;
            range[2, 0, 2 + ColumnNames.Length - 1, 0].RowHeight = 20 * Proportion;

            range[1, 0].Value = ColumnNames[0] + ":";
            range[1, 1, 1, 5].Merge();
            range[1, 1].Value = Values[0];
            range[2, 0].Value = ColumnNames[1] + ":";
            range[2, 1].Value = Values[1];
            range[2, 1, 2, 5].Merge();
            range[3, 0].Value = ColumnNames[2] + ":";
            range[3, 1, 3, 2].Merge();
            range[3, 1].Value = Values[2];
            range[3, 3].Value = ColumnNames[3] + ":";
            range[3, 4].Value = Values[3];
            range[4, 0].Value = ColumnNames[4] + ":";
            range[4, 1].Value = Values[4];
            range[4, 3].Value = ColumnNames[5] + ":";
            range[4, 4].Value = Values[5];

            for (int i = 0; i < Checkouts.Length; i++)
            {
                range[6 + (i / 6 * 2), 0 + i % 6].Value = Checkouts[i];
                range[7 + (i / 6 * 2), 0 + i % 6].Value = "□";
                range[7 + (i / 6 * 2), 0 + i % 6].Font.Size = range[7 + (i / 6 * 2), 0 + i % 6].Font.Size * 1.2;
            }


            range[0, 0, 14, 5].Borders.Color = SpreadsheetGear.Colors.Black;
            range[0, 0, 14, 5].Borders[SpreadsheetGear.BordersIndex.InsideHorizontal].LineStyle = LineStyle.None;
            range[0, 0, 14, 5].Borders[SpreadsheetGear.BordersIndex.InsideVertical].LineStyle = LineStyle.None;

            range[3 + ColumnNames.Length, 0].Select();

            MemoryStream ms = new MemoryStream();
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeScale = 3;
                //生成图像
                qrCodeEncoder.Encode(QRCodeValue, Encoding.UTF8).
                    Save(ms, System.Drawing.Imaging.ImageFormat.Bmp); ;
            }
            catch (Exception ex)
            {
                ReturnValue.ShowMessage(new ReturnValue(false, -1, ex.Message));
            }

            byte[] bytes = ms.GetBuffer();
            wbv.ActiveWorksheet.Shapes.AddPicture(bytes, 335 * Proportion,
                2 * Proportion, 80 * Proportion, 80 * Proportion);

            wbv.ActiveWorksheetWindowInfo.Zoom = (int)(100 / Proportion);
            wbv.ReleaseLock();
        }

        public void WorkbookViewSheetClear(WorkbookView wbv)
        {
            wbv.GetLock();
            wbv.ActiveWorksheet.Cells.Clear();
            foreach (IShape shape in wbv.ActiveWorksheet.Shapes)
            {
                shape.Delete();
            }
            wbv.ReleaseLock();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            wbv.GetLock();
            wbv.Print(true);
            wbv.ReleaseLock();
        }
    }
}
