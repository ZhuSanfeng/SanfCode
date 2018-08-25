using SSIT.EncodeBase;
using SSIT.PropertyBase;
using SSIT.QM.SampleManager.SettingForms;
using SSIT.UserInfo;
using SSITEncode.Common;
using SSITEncode.QueryBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Newtonsoft.Json.Linq;
using SSIT.QMBase;
using SSIT.QM.CheckInterface;

namespace SSIT.QM.CheckManager.DatasForms
{
    public partial class CheckDataCopyForm : Telerik.WinControls.UI.RadForm
    {
        ObjectGrid<CheckOrder> _sampleOrderGrid;
        CheckOrder _sampleOrder;

        public CheckOrder SelectedSample
        {
            get
            {
                return _sampleOrder;
            }
        }
        //InputGridBase _inputGrid;
        public bool IsReadOnly = false;
        public CheckDataCopyForm(EncodeCollection<CheckOrder> ec)
        {
            //test
            //order.SampleOrderState = SampleOrderStateEnum.Submit;
            _sampleOrder = ec[0];

            InitializeComponent();
            _sampleOrderGrid = new ObjectGrid<CheckOrder> { Dock = DockStyle.Fill };
            _sampleOrderGrid.Fields = FieldSelectSettings<CheckOrder>.Instance.Fields.ToDescriptionList();
            _sampleOrderGrid.Init();
           panel1.Controls.Add(_sampleOrderGrid);
            
            _sampleOrderGrid.SetGrid(ec);
            _sampleOrderGrid.SelectedChanged += OnSampleSelectedChanged;
            //tsbSampleCount.Text = _sampleOrder.SampleQuantity.ToString();
            //tsbNote.Text = _sampleOrder.Note;
            _sampleOrderGrid.SelectedRow = 0;
            LoadInfo();
        }

        void OnSampleSelectedChanged(object sender, EventArgs e)
        {
            var sample = _sampleOrderGrid.SelectedItem;
            if (sample != null)
            {
                if (sample != _sampleOrder)
                {
                    _sampleOrder = sample;
                    LoadInfo();
                }
            }
        }

        void SetOrderState()
        {
            SetPageEnable(false);
            // tsbCommit.Enabled = _sampleOrder.SampleOrderState < SampleOrderStateEnum.Complete;
            //tsbApprove.Enabled = _sampleOrder.SampleOrderState == SampleOrderStateEnum.Complete;
            ////发布前的样品数都可以改
            //tsbAllPass.Enabled = tsbSampleCount.Enabled = _sampleOrder.SampleOrderState <= SampleOrderStateEnum.Complete;
            //if (_sampleOrder.SampleOrderState == SampleOrderStateEnum.Submit 
            //    || _sampleOrder.SampleOrderState == SampleOrderStateEnum.Running 
            //    || _sampleOrder.SampleOrderState == SampleOrderStateEnum.Complete)
            //    SetPageEnable(true);
            //else
            //{
            //    SetPageEnable(false);
            //}
            _sampleOrderGrid.ReloadRow(1);
        }

        public void LoadInfo()
        {
            rpvCheckCategory.Pages.Clear();
            Dictionary<string, EncodeCollection<CheckItem>> dic = new Dictionary<string, EncodeCollection<CheckItem>>();
            EncodeCollection<CheckItem> ecCheckItems = null;
            if (_sampleOrder.GetPlanCheckItemCount <= 0)
            {
                //Definition def = SSIT.Bread.UI.MM.MMCommon.GetDefinitionbyID(_sampleOrder.DefinitionID);
                //MMCheckItemCombine combine = MMCheckItemCombine.Instance.GetItembyKey(_sampleOrder.DefinitionID);
                DefinitionCheckItemCombine combine = DefinitionCheckItemCombine.GetItemby(_sampleOrder.DefPK);
                if (combine != null)
                {
                    ecCheckItems = combine.GetCheckItems();
                }
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


        private void SampleDataInputForm_Shown(object sender, EventArgs e)
        {
            foreach (RadPageViewPage item in rpvCheckCategory.Pages)
            {
                InputGridBase grid = (InputGridBase)item.Tag;
                grid.Columns.AutoSize(true);
            }
        }

        void SetPageEnable(bool enable)
        {

            IsReadOnly = !enable;
            
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
            _sampleOrderGrid.ReloadRow(1);
            if (rv.Success)
            {
            }
            else
            {
            }
            return _sampleOrder.QualifyJudge;
        }

    }
}
