
using SSIT.EncodeBase;
using SSIT.PropertyBase;
using SSIT.QM.SampleManager.SettingForms;
using SSIT.UserInfo;
using SSITEncode.Common;
using SSITEncode.QueryBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Linq;
using SSIT.QMBase;
using SSIT.QMBase.PropertyPages;
using SSITControls.DataGridView;

using YHDataInterface.SSITFactory;
using SSIT.QM.CheckInterface;
using SSIT.QMBase.CodeSettings;

namespace SSIT.QM.CheckManager.DatasForms
{
    public partial class CheckDataInputForm : Telerik.WinControls.UI.RadForm
    {
        SSITGridView<CheckOrder> _Grid = null;
        //ObjectGrid<CheckOrder> _sampleOrderGrid;
        CheckOrder _sampleOrder;
        //InputGridBase _inputGrid;
        public bool IsReadOnly = false;
        public CheckDataInputForm(CheckOrder order)
        {
            //test
            //order.CheckOrderState = CheckOrderStateEnum.Submit;
            _sampleOrder = order;

            InitializeComponent();

           _Grid = new SSITGridView<CheckOrder>()
           {
               Dock = DockStyle.Fill,
               MultiSelect = true,
               AllowDeleteRow = false,
               AllowAddNewRow = false,
               AllowEditRow = false,
               AllowToolBar = true,
               AllowBottomToolBar = false,
               AllowBottomToolBarFilter = false,
               AllowDragToGroup = false,
               AllowPaging = false,
               GridFilterMode = FilterMode.CustomFilter
           };
           _Grid.ToolBar.Visible = false;
           twSampleOrder.Controls.Add(_Grid);
            EncodeCollection<CheckOrder> ec = new EncodeCollection<CheckOrder>();
            ec.Add(order);
            _Grid.FillGrid(ec);
            //_sampleOrderGrid.SetGrid(ec);
            tsbSampleCount.Text = order.CheckQuantity.ToString();
            tsbNote.Text = order.Note;
            LoadImage(order.SampleID);
            LoadInfo();
        }

        private void LoadImage(string orderID)
        {
            try
            {
                int ImageSize = 100;
                Dictionary<string, Image> dicImage = SampleCheckItemImages.LoadImagesbyOrderID(orderID);

                foreach (string key in dicImage.Keys)
                {
                    Image img = dicImage[key];
                    if (img == null)
                    {
                        continue;
                    }

                    RadGroupBox gbImage = new RadGroupBox();
                    gbImage.Width = ImageSize + 20;
                    gbImage.Height = ImageSize + 20;
                    gbImage.Text = key;

                    PictureBox pbImage = new PictureBox();
                    pbImage.Image = ImageFuncs.CreateThumbnail(img, ImageSize, ImageSize);
                    pbImage.Tag = img;
                    pbImage.Width = ImageSize;
                    pbImage.Height = ImageSize;
                    pbImage.Dock = DockStyle.Fill;
                    pbImage.Click += PictureBox_Click;

                    gbImage.Controls.Add(pbImage);
                    flowPanel.Controls.Add(gbImage);
                }
            }
            catch
            {
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox pb = (PictureBox)sender;
                SSITControls.ImageViewer.frmViewImg formShowImage = new SSITControls.ImageViewer.frmViewImg((Image)pb.Tag);

                formShowImage.ShowDialog();
            }
            catch
            { }
        }

        void SetOrderState()
        {
             tsbCommit.Enabled = _sampleOrder.CheckOrderState < CheckOrderStateEnum.Complete;
            tsbApprove.Enabled = _sampleOrder.CheckOrderState == CheckOrderStateEnum.Complete;
            //发布前的样品数都可以改 
            tsbAllPass.Enabled = tsbSampleCount.Enabled = _sampleOrder.CheckOrderState <= CheckOrderStateEnum.Complete;
            if (_sampleOrder.CheckOrderState == CheckOrderStateEnum.Submit 
                || _sampleOrder.CheckOrderState == CheckOrderStateEnum.Running 
                || _sampleOrder.CheckOrderState == CheckOrderStateEnum.Complete)
                SetPageEnable(true);
            else
            {
                SetPageEnable(false);
            }
            _Grid.Refresh();
            //_sampleOrderGrid.ReloadRow(1);
        }

        public void LoadInfo()
        {
            rpvCheckCategory.Pages.Clear();
            Dictionary<string, EncodeCollection<CheckItem>> dic = new Dictionary<string, EncodeCollection<CheckItem>>();
            EncodeCollection<CheckItem> ecCheckItems = null;
            if (_sampleOrder.GetPlanCheckItemCount <= 0)
            {
               ecCheckItems = DefinitionCheckItemCombine.GetCheckItemsby(_sampleOrder.DefPK, MMTypEnum.QM);

            }
            else
            {
                EncodeCollection<CheckItem> ec = new EncodeCollection<CheckItem>();
                foreach (short id in _sampleOrder.PlanCheckItems)
                {
                    CheckItem checkitem = CheckItem.Instance.Itemof(id);
                    if (checkitem != null)
                    {
                        ec.Add(checkitem);
                    }
                }
                ecCheckItems = ec;
            }
            if (ecCheckItems != null)
                foreach (CheckItem item in ecCheckItems)
                {
                    string CheckCategoryName = item.GetCheckCategory();
                    if (!string.IsNullOrEmpty(CheckCategoryName))
                    {
                        if (!dic.ContainsKey(CheckCategoryName))
                        {
                            dic.Add(CheckCategoryName, new EncodeCollection<CheckItem>());
                        }
                        dic[CheckCategoryName].Add(item);
                    }
                }
            EncodeCollection<CheckData> datas = CheckData.LoadDatasbySampleID(_sampleOrder.SampleID);
            int maxIndex = 0;
            if (datas.Count > 0)
            {
                foreach (var item in datas)
                {
                    maxIndex = Math.Max(item.SampleIndex, maxIndex);
                }
            }
            if (maxIndex >= _sampleOrder.CheckQuantity)
            {
                _sampleOrder.CheckQuantity = maxIndex;
            }
            SetOrderState();
            foreach (string key in dic.Keys)
            {
                Telerik.WinControls.UI.RadPageViewPage page = new Telerik.WinControls.UI.RadPageViewPage(key);
                InputGridBase grid = new InputGridBase { Dock = DockStyle.Fill };
                page.Controls.Add(grid);
                if (_sampleOrder != null)
                {
                    grid.SampleCount = _sampleOrder.CheckQuantity;
                }
                page.Tag = grid;
                rpvCheckCategory.Pages.Add(page);
                grid.Order = _sampleOrder;
                grid.Fields = CheckItemsToInputFields(dic[key]);
                grid.Init();
                grid.LoadDatas(datas);
                //grid.Columns.AutoSize(true);
            }
            
            if (datas.Count > 0)
            {
                DataStat();
            }
        }

        //EncodeCollection<CheckData> LoadData()
        //{
        //    string clause = string.Format("sampleid='{0}'",_sampleOrder.SampleID);
        //    EncodeCollection<CheckData> ec =  Encode.EncodeData.GetDatas<CheckData>(clause, string.Empty, -1);
        //    return ec;
        //}
        List<InputField> CheckItemsToInputFields(EncodeCollection<CheckItem> ec)
        {
            List<InputField> list = new List<InputField>();
            foreach (CheckItem item in ec)
            {
                InputField field = new InputField
                {
                    FieldName = item.ToString(),
                    ID = item.ParamID,
                    ValueType = item.ValueTypeID,
                    CheckType = item.GetCheckType(),
                    QualifyRate = 100f,
                    ReadOnly = IsReadOnly,
                    Precision = item.Precision
                };
                if (_sampleOrder != null)
                {
                    CheckStandard.MMTypeID = (int)MMTypEnum.QM;
                    CheckStandard cs = CheckStandard.Instance.GetCurrentStandard(_sampleOrder.DefPK, item.ParamID);
                    if (cs != null)
                    {
                        if (!string.IsNullOrWhiteSpace(cs.EntStandardStr))
                        {
                            field.StandardStr = cs.EntStandardStr;
                        }
                        else if (!string.IsNullOrWhiteSpace(cs.NatStandardStr))
                        {
                            field.StandardStr = cs.NatStandardStr;
                        }
                        field.QualifyRate = cs.QualifyRate;
                    }

                }
                list.Add(field);
            }
            return list;
        }


        private void rdtSampleDate_ValueChanged(object sender, EventArgs e)
        {
            //string date = rdtSampleDate.Value.ToString(EncodeConst.DateFormat);
            //string clause = string.Format("plancheckdate>='{0} 00:00:00' AND plancheckdate<='{0} 23:59:59' AND samplestate={1}"
            //    , date, (int)SampleStateEnum.Approve);

            //EncodeCollection<SampleItem> ec =   Encode.EncodeData.GetDatas<SampleItem>(clause, "sampleorerid desc", -1);


        }

        private void SampleDataInputForm_Shown(object sender, EventArgs e)
        {
            foreach (RadPageViewPage item in rpvCheckCategory.Pages)
            {
                InputGridBase grid = (InputGridBase)item.Tag;
                grid.Columns.AutoSize(true);
            }
        }

        private void SampleDataInputForm_Load(object sender, EventArgs e)
        {

        }

        private void tsbCommit_Click(object sender, EventArgs e)
        {

            if (ReturnValue.ShowYesNo("是否确定提交数据，提交成功后数据不能更改，检验工单状态将设置为[已完成]") == System.Windows.Forms.DialogResult.Yes)
            {
                if (_sampleOrder != null)
                {
                    _sampleOrder.CheckOrderState = CheckOrderStateEnum.Complete;
                    _sampleOrder.ActualCheckDate = DateTime.Now.ToString(EncodeConst.DateTimeFormat);
                    if (User.CurrentUser != null)
                        _sampleOrder.ActualInspector = User.CurrentUser.ParamName;
                    _sampleOrder.State = DataState.Changed;
                    _sampleOrder.FinishedCheckItemString = CheckData.DataCommitSetting(_sampleOrder.SampleID);
                    ReturnValue rv = Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(_sampleOrder));
                    ReturnValue.ShowMessage(rv);
                    if (rv.Success)
                    {
                        //_sampleOrderGrid.ReloadRow(1);
                        SetOrderState();
                    }
                }
                else
                {
                    ReturnValue.ShowMessage("检验工单对象不存在");
                }
            }
        }

        void SetPageEnable(bool enable)
        {
            //foreach (Telerik.WinControls.UI.RadPageViewPage page in rpvCheckCategory.Pages)
            //{
            //    foreach (Control c in page.Controls)
            //    {
            //        c.Enabled = enable;
            //    }

            //}

            IsReadOnly = !enable;
            
            tsbCommit.Enabled = enable;
            if (enable)
            {
                this.Text = "检测数据录入";
            }
            else
            {
                this.Text = "检测数据查看 (不能修改)";
            }
        }

        private void tsbApprove_Click(object sender, EventArgs e)
        {
            string username = User.CurrentUser != null ? User.CurrentUser.ParamName : string.Empty;
            UsageDecisionsForm form = new UsageDecisionsForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var ud = form.UsageDecisionResult;
                if(ud == null)
                {
                    return;
                }
                ReturnValue rv = CheckOrder.UpdateSampleOrderState( _sampleOrder.SampleID, username, CheckOrderStateEnum.Approve, ud.ParamID);

                if (rv.Success)
                {
                    _sampleOrder.UsageDecisions = ud.ParamID;
                    _sampleOrder.CheckOrderState = CheckOrderStateEnum.Approve;
                    _sampleOrder.Auditor = username;
                    SetOrderState();
                }
                ReturnValue.ShowMessage(rv);
                this.Close();
            }
        }

        private void tsbAllPass_Click(object sender, EventArgs e)
        {
            SetLogicPassForm form = new SetLogicPassForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                switch (form.SelectResult)
                {
                    case SelectResultEnum.Row:
                        if (rpvCheckCategory.SelectedPage != null && rpvCheckCategory.SelectedPage.Controls.Count > 0)
                        {
                            InputGridBase grid = (InputGridBase)rpvCheckCategory.SelectedPage.Controls[0];
                            int row = grid.SelectedRow;
                            if (row > 0)
                            {
                                ReturnValue.ShowMessage(grid.SetLogicPass(row - grid.FixedRows));
                            }
                            else
                            {
                                ReturnValue.ShowMessage("没有选中行");
                            }
                        }
                        break;
                    case SelectResultEnum.Page:
                        if (rpvCheckCategory.SelectedPage != null && rpvCheckCategory.SelectedPage.Controls.Count > 0)
                        {
                            InputGridBase grid = (InputGridBase)rpvCheckCategory.SelectedPage.Controls[0];
                            ReturnValue.ShowMessage(grid.SetAllLogicPass());

                        }
                        break;
                    case SelectResultEnum.All:

                        foreach (Telerik.WinControls.UI.RadPageViewPage page in rpvCheckCategory.Pages)
                        {
                            foreach (Control c in page.Controls)
                            {
                                if (c is InputGridBase)
                                {
                                    InputGridBase grid = (InputGridBase)c;
                                    ReturnValue rv = grid.SetAllLogicPass();
                                    if (!rv.Success)
                                    {
                                        ReturnValue.ShowMessage(rv);
                                        return;
                                    }
                                }
                            }
                        }
                        ReturnValue.ShowMessage(new ReturnValue(true));
                        break;
                }
            }
        }

        private void tsbStat_Click(object sender, EventArgs e)
        {
            DataStat();
        }

        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        public   QualifyJudgeEnum DataStat()
        {
            //List<QualifyJudgeEnum> results = new List<QualifyJudgeEnum>();
            foreach (Telerik.WinControls.UI.RadPageViewPage page in rpvCheckCategory.Pages)
            {
                foreach (Control c in page.Controls)
                {
                    if (c is InputGridBase)
                    {
                        InputGridBase grid = (InputGridBase)c;
                        grid.QualifyJudgeAll();
                        //QualifyJudgeEnum result = grid.QualifyJudgeAll();
                        //results.Add(result);
                    }
                }
            }
            var rv = CheckOrder.DataStat(_sampleOrder.SampleID);
            //QualifyJudgeEnum finalresult = QualifyJudgeEnum.Empty;
            //if (results.Contains(QualifyJudgeEnum.UnFinish))
            //    finalresult = QualifyJudgeEnum.UnFinish;
            //else if (results.Contains(QualifyJudgeEnum.False))
            //    finalresult = QualifyJudgeEnum.False;
            //else if (results.Contains(QualifyJudgeEnum.UnJudge) || !results.Contains(QualifyJudgeEnum.Pass))
            //    finalresult = QualifyJudgeEnum.UnJudge;
            //else finalresult = QualifyJudgeEnum.Pass;
            _sampleOrder.QualifyJudge =(QualifyJudgeEnum) rv.ErrNum;// finalresult;
            _Grid.Refresh();
            //_sampleOrderGrid.ReloadRow(1);
            if (rv.Success)
            {
                lbInfo.ForeColor = Color.RoyalBlue;
            }
            else
            {
                lbInfo.ForeColor = Color.Crimson;
            }
            lbInfo.Text = "合格判定信息:" + rv.Message;
            return _sampleOrder.QualifyJudge;
        }

        private void tsbSampleCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                int d;
                if (int.TryParse(tsbSampleCount.Text, out d))
                {
                    if (d > 0 && d < 100)
                    {
                        if (_sampleOrder.CheckQuantity != d)
                        {
                            var ret = MessageBox.Show(string.Format("是否确定把样品数从[{0}]个改为[{1}]个", _sampleOrder.CheckQuantity, d), "注意！", MessageBoxButtons.OKCancel);
                            if (ret == System.Windows.Forms.DialogResult.OK)
                            {
                                _sampleOrder.CheckQuantity = d;
                                _Grid.Refresh();
                                //_sampleOrderGrid.ReloadRow(1);
                                LoadInfo();
                               // return;
                            }
                        }
                    }
                }

                tsbSampleCount.Text = _sampleOrder.CheckQuantity.ToString();
            }
        }

        private void tsbNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (_sampleOrder.Note != tsbNote.Text)
                {
                    _sampleOrder.Note = tsbNote.Text;
                    _Grid.Refresh();
                    //_sampleOrderGrid.ReloadRow(1);
                    _sampleOrder.State = DataState.Changed;
                    Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(_sampleOrder));
                }
            }

        }

    }
}
