using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using SSIT.EncodeBase;
using SSIT.DataField;
using SSIT.UserInfo;
using SSIT.QMBase;
using YHDataInterface.SSITMM;
using SSIT.QM.SampleInterface;
using SSITEncode.Common;

namespace SSIT.QM.CheckInterface
{
    /// <summary>
    /// 样品的来源
    /// </summary>
    //public enum SampleSourceTypeEnum : int
    //{
    //    None = 0,
    //    MMReipt = 1 //物料收货
    //}
    //public enum CreateTypeEnum : int { Normal = 1, Sensory = 2, Check = 3 };

    public class CheckOrder : CheckOrderBase<CheckOrder>//DataClassBase<SampleItemOrder>
    {
        public CheckOrder()
            : base()
        {

        }

        #region 批次信息
        static Dictionary<string, MMLot> dicLot = new Dictionary<string, MMLot>();

        string _lotName = null;
        [DataField(null,Description = "批次名称")]
        public string LotName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_lotName))// != null)
                {
                    return _lotName;
                }
                if (String.IsNullOrWhiteSpace(LotID))
                {
                    return string.Empty;
                }
                if (dicLot.ContainsKey(LotID))
                {
                    if (dicLot[LotID] == null)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        _lotName = dicLot[LotID].LotName;
                    }
                    //return dicLotName[LotID];
                }
                else
                {
                    //var lot = SSIT.Bread.UI.MM.MMCommon.GetLotbyID(LotID);
                    var ecLot = Encode.EncodeData.GetDatas<MMLot>(string.Format("LotID='{0}'", LotID));
                    MMLot lot = null;
                    if (ecLot.Count > 0)
                    {
                        lot = ecLot[0];
                    }
                    if (lot != null)
                    {
                        dicLot.Add(LotID, lot);
                        _lotName = lot.LotName;
                    }
                    else
                    {
                        dicLot.Add(LotID, null);
                        _lotName = string.Empty;
                    }
                }
                return _lotName;
            }
            set
            {
                _lotName = value;
            }
        }

        float _lotQuantity = 0; 
        [DataField(null, Description = "批次数量")]
        public float LotQuantity
        {
            get
            {

                if (_lotQuantity != 0)
                {
                    return _lotQuantity;
                }

                else if (!string.IsNullOrWhiteSpace(LotName))
                {
                    if (dicLot.ContainsKey(LotID))
                    {
                        if (dicLot[LotID] != null)
                        {
                            return dicLot[LotID].InitQuantity;
                        }
                    }
                }
                return 0;// Decimal.Zero;
            }
            set
            {
                _lotQuantity = value;
            }
        }

        [DataField("nodefid", Type = DbType.Boolean)]//,Description="非一般物料")]
        public bool NoDefID { get; set; }

        //[DataField(null, Description = "一般物料")]
        public string IsNormalDefinition
        {
            get
            {
                if (NoDefID)
                {
                    return "否";
                }
                else
                {
                    return "是";
                }
            }
        }

        [DataField(null, Description="物料小类")]
        public string MMClassName
        {
            get
            {
                if (NoDefID)
                {
                    return string.Empty;
                }
                MMDefinition def = MMDefinition.Instance.Datas.FirstOrDefault(p => p.DefPK == DefPK);
                
                if (def != null)
                {
                    return def.MMClassName;
                }
                return string.Empty;
            }
        }


        [DataField(null,Description="物料编号")]
        public string DefinitionID
        {
            get
            {
                if (DefinitionItem != null)
                {
                    return DefinitionItem.DefID;
                }
                return string.Empty;
            }
        }

        [DataField("defpk",Type = DbType.Int32)]
        public int DefPK { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        MMDefinition _definition = null;
        [Newtonsoft.Json.JsonIgnore]
        public MMDefinition DefinitionItem
        {
            get
            {
                MMDefinition def = MMDefinition.Instance.Datas.FirstOrDefault(p => p.DefPK == DefPK);
                _definition = def;
                return _definition;
            }
            set
            {
                _definition = value;
            }
        }

        //[Newtonsoft.Json.JsonIgnore]
        [DataField(null, Description = "物料名称")]
        public string DefinitionName
        {
            get
            {
                if (NoDefID)
                    return DefinitionID;
                if (DefinitionItem != null)
                {
                    return DefinitionItem.DefName;
                }
                return string.Empty;

            }
        }

        [DataField("suppk",Type = DbType.Int32)]
        public int SupPK { get; set; }

        public IsExemptionEnum GetIsExemption
        {
            get
            {
                var supmm = SupAndMMRelation.Instance.Datas.FirstOrDefault(p => p.Enable && p.DefPK == DefPK && p.SupPK == SupPK);
                if (supmm != null) return supmm.IsExemption;
                return IsExemptionEnum.convention;
            }
        }

        [DataField("hutpk", Type = DbType.Int32)]// Size = 50, Description = "")]
        virtual public int HutPK { get; set; }

        [DataField(null, Description = "取样地点")]
        public string HutName
        {
            get
            {
                return YHDataInterface.Equipment.EquipmentItem.Instance.GetNamebyID(HutPK);
            }
        }

        //[DataField("workteamid",Size=10, Description = "班组编号")]
        //public string WorkTeamID{get;set;}
        #endregion

        [DataField("checktype",Type = DbType.Int16)]
        public CheckTypeEnum CheckType { get; set; }
        public string CheckTypeName
        {
            get
            {
                return CheckType.GetDescription();
            }
        }
           
           

        #region 检测项目信息

        [Newtonsoft.Json.JsonIgnore]
        [DataField(null, Description = "计划检测项目数")]
        public int GetPlanCheckItemCount
        {
            get
            {
                List<string> list = new List<string>();
                if (string.IsNullOrWhiteSpace(PlanCheckItemString))
                {
                    PlanCheckItemString = DefinitionCheckItemCombine.GetCheckItemstringby(DefPK, MMTypEnum.QM);
                }
                if (PlanCheckItems == null || PlanCheckItems.Count == 0)
                {
                    PlanCheckItems = SSITEncode.Common.STRING.StringToIntList(PlanCheckItemString);
                }
                return PlanCheckItems.Count;
            }
        }

        /// <summary>
        /// 计划需要检测项目List
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<int> PlanCheckItems { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int InputedCheckItemCount
        {
            get
            {
                return CheckData.GetInputedCount(SampleID);
            }
        }

        //[Newtonsoft.Json.JsonIgnore]
        //public List<int> FinishedCheckItems { get; set; }

        int _finishedCheckItemCount = -1;
        [Newtonsoft.Json.JsonIgnore]
        [DataField(null, Description = "已完成的检测项目数")]
        public int GetFinishedCheckItemCount
        {
            get
            {
                if (_finishedCheckItemCount < 0)
                {
                    //这个方法比较效率很差
                    _finishedCheckItemCount = CheckData.GetInputedCount(SampleID);
                }
                return _finishedCheckItemCount;

            }
        }

        #endregion
        // [DataField(null,Description = "检验项目数")]
        #region 工单信息
        //[DataField("checktype", Type = DbType.Int16)]
        //public CreateTypeEnum CreateType { get; set; }

        //[DataField(null, Description = "检验类型")]
        //public string CreateTypeString
        //{
        //    get
        //    {
        //       return CreateTypeClass.GetStringValue(CreateType);

        //    }
        //}
        [DataField("sampletype", Type = DbType.Int16)]
        public SampleTypeEnum SampleType { get; set; }


        [Newtonsoft.Json.JsonIgnore]
        [DataField(null, Description = "样品类型")]
        public string SampleTypeName
        {
            get
            {
                return SampleType.GetDescription();// SampleTypeClass.GetStringValue(SampleType);
            }
        }

        [DataField("sourceorderid",Size = 50,Description="来源单号")]
        public string SourceOrderID { get; set; }

        #endregion
        //public QualifyJudgeEnum SetQualifyJudge()
        //{
        //    EncodeCollection<CheckData> ec =  CheckData.LoadDatasbySampleID(SampleID);
        //    foreach (CheckData data in ec)
        //    {

        //    }
        //}
        #region 静态属性、方法
        static public string GetNewCheckID(DateTime dt)
        {
            string IdInit = "C" + dt.ToString("yyyyMMdd");
            EncodeCollection<CheckOrder> ec = Encode.EncodeData.GetDatas<CheckOrder>("sampleid Like '" + IdInit + "%'", "sampleid desc", 1);
            if (ec.Count > 0)
            {
                string strnum = ec[0].SampleID.Substring(IdInit.Length);
                int num;
                if (int.TryParse(strnum, out num))
                {
                    return IdInit + (num + 1).ToString("000");
                }
            }
            return IdInit + "001";
        }
        static public bool CheckSampleIDExist(string sampleid)
        {
            EncodeCollection<CheckOrder> ec = Encode.EncodeData.GetDatas<CheckOrder>("sampleid = '" + sampleid + "'", string.Empty, 1);
            return ec.Count > 0;
        }

        static public CheckOrder GetItembyID(string sampleid)
        {
            EncodeCollection<CheckOrder> ec = Encode.EncodeData.GetDatas<CheckOrder>("sampleid = '" + sampleid + "'", string.Empty, 1);
            if (ec.Count > 0)
                return ec[0];
            return null;
        }

        static public CheckOrder GetItembySourceOrderID(string sorceorderid)
        {
            EncodeCollection<CheckOrder> ec = Encode.EncodeData.GetDatas<CheckOrder>("sourceorderid = '" + sorceorderid + "'", string.Empty, 1);
            if (ec.Count > 0)
                return ec[0];
            return null;
        }


       public static string CheckIDsToNameStrings(List<int> list )
       {
           StringBuilder sb = new StringBuilder();
           foreach (short id in list)
           {
               CheckItem item = CheckItem.Instance.Itemof(id);
               if (item != null)
               {
                   sb.Append(item.ParamName);
                   sb.Append(" ");
               }
           }
           return sb.ToString().Trim();
       }
        #endregion

        #region 创建检查工单
        /// <summary>
        /// 创建检测工单 有实际批次
        /// </summary>
        /// <param name="lotID">批次ID</param>
        /// <param name="samplecount">样品数</param>
        /// <param name="dtSampleDate">样品日期</param>
        /// <returns></returns>
        static public ReturnValue AutoCreateCheckOrder(string lotID, string sourceOrderID, SampleTypeEnum sampleType, DateTime dtSampleDate, string creator, int samplecount = 1, string plancheckitemstring = "", bool firscheck = true, CheckTypeEnum checkType = CheckTypeEnum.Normal)
       {
           if (!string.IsNullOrWhiteSpace(sourceOrderID))
           {
               EncodeCollection<CheckOrder> ec = Encode.EncodeData.GetDatas<CheckOrder>("sourceorderid = '" + sourceOrderID + "'", string.Empty, 1);
               if (ec.Count > 0)
               {
                   return new ReturnValue(true, 1, "检验单已创建");
               }
           }
           //find lot
          // EncodeCollection<MMReiptOrder> ecMR = Encode.EncodeData.GetDatas<MMReiptOrder>(string.Format("sitlotid='{0}'", lotID), string.Empty, -1);
           var ecLot = Encode.EncodeData.GetDatas<MMLot>(string.Format("LotID='{0}'", lotID));
           MMLot lot = null;
           if (ecLot.Count > 0)
           {
               lot = ecLot[0];
           }
           if (lot != null)
           //if(ecMR.Count > 0 )
           {
               //MMReiptOrder mr = ecMR[0];
               //先创建SampleItem
               CheckOrder si = new CheckOrder { LotID = lot.LotID};//, HutID = lot.HutID, DefinitionID = lot. };
               //si.HutID = lot.HutID;
               si.CheckOrderID = CheckOrder.GetNewCheckID(dtSampleDate);
               si.CheckQuantity = samplecount;
               si.PlanCheckDate= si.SampleDate = dtSampleDate.ToString(EncodeConst.DateFormat);
               //si.CreateType = createType; 
               si.CheckOrderState = CheckOrderStateEnum.Submit;
               si.SourceOrderID = sourceOrderID;
               si.SampleType = sampleType;
               si.Creator = creator;
               si.CreateTime = dtSampleDate.ToString(EncodeConst.DateTimeFormat);
               si.PlanCheckItemString = plancheckitemstring;
               //si.FirstCheck = firscheck;
               //if(User.CurrentUser != null)
               //{
               //    si.ActualInspector = User.CurrentUser.ParamName;
               //}
               if (string.IsNullOrWhiteSpace(si.PlanCheckItemString))
               {
                   DefinitionCheckItemCombine combine = DefinitionCheckItemCombine.GetItemby(si.DefPK);
                   if (combine != null)
                   {
                       List<int> list =  CheckItem.Instance.GetIdList(combine.GetCheckItems());   
                       si.PlanCheckItemString =  SSITEncode.Common.STRING.IntListToString(list);
                   }
               }
               si.State = DataState.New;
               //EncodeCollection<SampleItemOrder> ec = new EncodeCollection<SampleItemOrder>(si);
               ReturnValue rv = Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(si));
               if (rv.Success)
               {
                   rv.ValueString = si.SampleID;
                   //SSIT.SystemManager.DataInterface.MessageContent.CreateNewMessage(BreadInterface.SampleTypeEnum.QMOrder, si.SampleID, creator);
               }
               return rv;
           }
           return new ReturnValue(false, -444, "找不到对应批次");
       }

        /// <summary>
        /// 没实际批次
        /// </summary>
        /// <param name="lotID"></param>
        /// <param name="sourceOrderID">源工单ID</param>
        /// <param name="sourceType"></param>
        /// <param name="dtSampleDate"></param>
        /// <param name="creator"></param>
        /// <param name="samplecount"></param>
        /// <param name="plancheckitemstring"></param>
        /// <param name="firscheck"></param>
        /// <param name="checkType"></param>
        /// <returns></returns>
       static public ReturnValue AutoCreateCheckOrder(int defpk, int hutid, string lotid, string sourceOrderID, SampleTypeEnum sampleType, DateTime dtSampleDate, string creator, int samplecount = 1, string plancheckitemstring = "", bool firscheck = true, CheckTypeEnum checkType = CheckTypeEnum.Normal)
       {
           if (!string.IsNullOrWhiteSpace(sourceOrderID))
           {
               EncodeCollection<CheckOrder> ec = Encode.EncodeData.GetDatas<CheckOrder>("sourceorderid = '" + sourceOrderID + "'", string.Empty, 1);
               if (ec.Count > 0)
               {
                   return new ReturnValue(true, 1, "检验单已创建");
               }
           }
           //find lot
           // EncodeCollection<MMReiptOrder> ecMR = Encode.EncodeData.GetDatas<MMReiptOrder>(string.Format("sitlotid='{0}'", lotID), string.Empty, -1);
           // MMLot lot = SSIT.Bread.UI.MM.MMCommon.GetLotbyID(lotID);
           //if (lot != null)
           //if(ecMR.Count > 0 )
           //{
           //MMReiptOrder mr = ecMR[0];
           //先创建SampleItem
           CheckOrder si = new CheckOrder { LotID = lotid, HutPK = hutid, DefPK = defpk };
           //si.HutID = lot.HutID;
           si.SampleID = CheckOrder.GetNewCheckID(dtSampleDate);
           si.CheckQuantity = samplecount;
           si.PlanCheckDate = si.SampleDate = dtSampleDate.ToString(EncodeConst.DateFormat);
           //si.CreateType = createType;
           si.CheckOrderState = CheckOrderStateEnum.Submit;
           si.SourceOrderID = sourceOrderID;
           si.SampleType = sampleType;
           si.Creator = creator;
           si.CreateTime = dtSampleDate.ToString(EncodeConst.DateTimeFormat);
           si.PlanCheckItemString = plancheckitemstring;
           //si.FirstCheck = firscheck;
           //if(User.CurrentUser != null)
           //{
           //    si.ActualInspector = User.CurrentUser.ParamName;
           //}
           if (string.IsNullOrWhiteSpace(si.PlanCheckItemString))
           {
               DefinitionCheckItemCombine combine = DefinitionCheckItemCombine.GetItemby(si.DefPK);
               if (combine != null)
               {
                   List<int> list = CheckItem.Instance.GetIdList(combine.GetCheckItems());
                   si.PlanCheckItemString = SSITEncode.Common.STRING.IntListToString(list);
               }
           }
           si.State = DataState.New;
           //EncodeCollection<SampleItemOrder> ec = new EncodeCollection<SampleItemOrder>(si);
           ReturnValue rv = Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(si));
           if (rv.Success)
           {
               rv.ValueString = si.SampleID;
               //SSIT.SystemManager.DataInterface.MessageContent.CreateNewMessage(BreadInterface.SampleTypeEnum.QMOrder, si.SampleID, creator);
           }
           return rv;

           // return new ReturnValue(false, -444, "找不到对应批次");
       }
       
        static public CheckOrder CreateCheckOrderbySample(SampleOrder sample)
        {
            var order = new CheckOrder
            {
                State = DataState.New,
                 CreateTime = DateTime.Now.ToString(EncodeConst.DateTimeFormat),
                  CheckOrderID = GetNewCheckID(DateTime.Today),
                   CheckOrderState = CheckOrderStateEnum.Submit,
                    Creator = User.CurrentUser?.ParamName,
                     CheckQuantity = 1,
                      DefPK = sample.DefPK,
                HutPK = sample.HutPK,
                        LotID = sample.LotID,
                         PlanCheckDate = sample.SampleDate,
                          SampleID = sample.SampleID,
                           SampleType = sample.SampleType,
                            SourceOrderID = sample.SourceOrderID,
                             SampleDate = sample.SampleDate,
                              
            };
            if (string.IsNullOrWhiteSpace(order.PlanCheckItemString))
            {
                DefinitionCheckItemCombine combine = DefinitionCheckItemCombine.GetItemby(order.DefPK);
                if (combine != null)
                {
                    List<int> list = CheckItem.Instance.GetIdList(combine.GetCheckItems());
                    order.PlanCheckItemString = SSITEncode.Common.STRING.IntListToString(list);
                }
            }
            return order;
            //ReturnValue rv = Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(order));
            //if (rv.Success)
            //{
            //    rv.ValueString = order.SampleID;
            //    //SSIT.SystemManager.DataInterface.MessageContent.CreateNewMessage(BreadInterface.SampleTypeEnum.QMOrder, si.SampleID, creator);
            //}
            //return rv;
        }
        #endregion

        #region 检测工单操作
        static public ReturnValue UpdateSampleOrderState(string orderid, string username, CheckOrderStateEnum orderstate, int usagedecisions)
       {
           ReturnValue rv = new ReturnValue(true);
           CheckOrder order = CheckOrder.GetItembyID(orderid);
           if (order == null)
           {
               return new ReturnValue(false, -444, "找不到对应工单");
           }
           //if (orderstate < 0 || orderstate > 5)
           //{
           //    return new ReturnValue(false, -444, "找不到对应状态");
           //}

           CheckOrderStateEnum afterstate = (CheckOrderStateEnum)orderstate;
           if (order.CheckOrderState != afterstate)
           {
               order.State = DataState.Changed;
               switch (afterstate)
               {
                   case CheckOrderStateEnum.Complete:
                      // SSIT.QM.CheckManager.DatasForms.SampleDataInputForm form = new SampleInterface.DatasForms.SampleDataInputForm(order);
                      // form.LoadInfo();
                       var dsRV = DataStat(orderid);
                       order.QualifyJudge = (QualifyJudgeEnum)dsRV.ErrNum; //form.DataStat();
                       order.ActualInspector = username;
                       order.CheckOrderState = CheckOrderStateEnum.Complete;
                       order.ActualCheckDate = DateTime.Now.ToString(EncodeConst.DateTimeFormat);
                       order.FinishedCheckItemString = CheckData.DataCommitSetting(order.SampleID);
                       break;
                   case CheckOrderStateEnum.Approve:
                       var dsRV1 = DataStat(orderid);
                       order.QualifyJudge = (QualifyJudgeEnum)dsRV1.ErrNum;
                       order.CheckOrderState = CheckOrderStateEnum.Approve;
                       order.UsageDecisions = usagedecisions;
                       order.Auditor = username;
                       order.ActualCheckDate = DateTime.Now.ToString(EncodeConst.DateTimeFormat);
                       order.FinishedCheckItemString = CheckData.DataCommitSetting(order.SampleID);

                       //联动
                       if (!string.IsNullOrWhiteSpace(order.SourceOrderID))
                       {
                           
                           SSIT.SystemManager.DataInterface.MessageContent.CreateNewMessage(order.SampleTypeName + " OrderID: " + order.SourceOrderID + " 已经完成检测并发布", order.Creator, order.Auditor);//(BreadInterface.SampleTypeEnum.QMOrder, order.SampleID, username);
                       }
                       else
                       {
                           SSIT.SystemManager.DataInterface.MessageContent.CreateNewMessage("检验工单:" + order.SampleID + " 已检", order.Creator, order.Auditor);
                       }
                       break;
                   default:
                       order.CheckOrderState = afterstate;
                       break;
               }
               return Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(order));
           }
           return new ReturnValue(true, 0, "状态没改变");
       }

        static public ReturnValue DataStat(string sampleid)
        {
            //A B C D 对应0，1，2，3
            var ec = CheckData.LoadDatasbySampleID(sampleid);
            if (ec.Count == 0)
            {
                return new ReturnValue(true, (int)QualifyJudgeEnum.UnFinish, "未完成");
            }

            List<int> checkitems = new List<int>();
            var order = ec[0].Order;
            if (order != null && order.GetPlanCheckItemCount > 0)
            {
                checkitems = order.PlanCheckItems;
            }
            Dictionary<int, List<CheckData>> dicData = new Dictionary<int, List<CheckData>>();
            foreach (CheckData item in ec)
            {
                if (string.IsNullOrWhiteSpace(item.DataValue))
                {
                    continue;
                }
                if (!dicData.ContainsKey(item.CheckItemID))
                {
                    dicData.Add(item.CheckItemID, new List<CheckData>());
                    
                }
                dicData[item.CheckItemID].Add(item);
                if (checkitems.Contains(item.CheckItemID))
                {
                    checkitems.Remove(item.CheckItemID);
                }
            }
            Dictionary<CheckItemTypeEnum, int> dicFalseCount = new Dictionary<CheckItemTypeEnum, int>();
            foreach (var key in dicData.Keys)
            {
                var qj = QualifyOneCheckItemJudge(dicData[key]);
                if (qj == QualifyJudgeEnum.False)
                {
                    CheckItem item = CheckItem.Instance.GetItembyKey(key.ToString());
                    if (item != null)
                    {
                        if (!dicFalseCount.ContainsKey(item.CheckTypeID))
                        {
                            dicFalseCount.Add(item.CheckTypeID, 0);
                        }
                        dicFalseCount[item.CheckTypeID]++;

                    }

                }
            }

            ReturnValue rv = null;
            if (dicFalseCount.Count == 0)
            {
                if (checkitems.Count > 0)
                {
                    return new ReturnValue(true, (int)QualifyJudgeEnum.UnFinish, "合格,但有项目未完成");
                }
                else
                {
                    return new ReturnValue(true, (int)QualifyJudgeEnum.Pass, "合格");
                }

            }
            if (dicFalseCount.ContainsKey(CheckItemTypeEnum.A))
            {
                rv = new ReturnValue(false, (int)QualifyJudgeEnum.False, "有1个以上A类不合格");
            }
            else if (dicFalseCount.ContainsKey(CheckItemTypeEnum.B) && dicFalseCount[CheckItemTypeEnum.B] >= 2)
            {
                rv = new ReturnValue(false, (int)QualifyJudgeEnum.False, "有2个以上B类不合格");
            }
            else if (dicFalseCount.ContainsKey(CheckItemTypeEnum.D))
            {
                rv = new ReturnValue(false, (int)QualifyJudgeEnum.False, "出现D类不合格");
            }
            else if (dicFalseCount.ContainsKey(CheckItemTypeEnum.C))
            {
                rv = new ReturnValue(true, (int)QualifyJudgeEnum.UnJudge, "有C类不合格");
            }
            else if (dicFalseCount.ContainsKey(CheckItemTypeEnum.B))
            {
                rv = new ReturnValue(true, (int)QualifyJudgeEnum.Pass, "有1个B类不合格");
            }
            if (rv != null)
            {
                if (checkitems.Count > 0)
                {
                    return new ReturnValue(true, (int)QualifyJudgeEnum.UnFinish, "有项目未完成,且" + rv.Message);
                }
                return rv;
            }
            return new ReturnValue(true);
        }

        /// <summary>
        /// 对一个checkitem进行判断
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        static public QualifyJudgeEnum QualifyOneCheckItemJudge(List<CheckData> datas)
        {
            if (datas.Count == 0)
            {
                return QualifyJudgeEnum.Empty;
            }
            CheckData first = datas[0];
            CheckStandard checkStandard = first.GetCheckStandar();

            if (checkStandard == null || string.IsNullOrWhiteSpace(first.StandardStr))
            {
                return QualifyJudgeEnum.UnJudge;
            }
            if (first.ValueType  == ValueTypeEnum.Selection)
            {
                return QualifyJudgeEnum.UnJudge;
            }
            // List<string> values = new List<string>();
           
            bool bUnFinish = true;
            int passcount = 0;
            for (int index = 0; index < datas.Count; index++)
            {
                
                //string value = (string)cell.Value;
                CheckData data = datas[index];
                if (!string.IsNullOrWhiteSpace(data.DataValue))
                {
                    bUnFinish = false;
                    //return QualifyJudgeEnum.UnFinish; 
                    //datas.Add(data);
                    bool isfalse = data.IsFalse;
                    data.SetPass();
                    if (isfalse != data.IsFalse)
                    {
                        data.State = DataState.Changed;
                        Encode.EncodeData.SaveDatas<CheckData>(new EncodeCollection<CheckData>(data));
                    }
                    
                    if (!data.IsFalse)
                    {
                        passcount++;
                    }
                    //values.Add(data);
                    // break;
                }
            }
            if (bUnFinish)
            {
                return QualifyJudgeEnum.UnFinish;
            }           

            float rate = 100f * passcount / datas.Count;
            
            if (rate >= checkStandard.QualifyRate)
            {
                return QualifyJudgeEnum.Pass;
            }
            else
            {
                return QualifyJudgeEnum.False;
            }
        }
        static public ReturnValue DataStat_old(string sampleid)
        {
            //A B C D 对应0，1，2，3
            var ec =  CheckData.LoadDatasbySampleID(sampleid);
            if (ec.Count == 0)
            {
                return new ReturnValue(true, (int)QualifyJudgeEnum.UnFinish,"未完成");
            }

            List<int> checkitems = new List<int>();
            var order = ec[0].Order;
            if (order != null && order.GetPlanCheckItemCount > 0)
            {
                checkitems = order.PlanCheckItems;
            }
            EncodeCollection<CheckData> ecFalse = new EncodeCollection<CheckData>();
            foreach (CheckData item in ec)
            {
                item.SetPass();
                if (item.IsFalse)
                {
                    ecFalse.Add(item);
                }
                if (checkitems.Contains(item.CheckItemID))
                {
                    checkitems.Remove(item.CheckItemID);
                }
            }
            if (ecFalse.Count == 0)
            {
                if (checkitems.Count > 0)
                {
                    return new ReturnValue(true, (int)QualifyJudgeEnum.UnFinish, "合格,但有项目未完成");
                }
                else
                {
                    return new ReturnValue(true, (int)QualifyJudgeEnum.Pass, "合格"); 
                }
                
            }
            Dictionary<CheckItemTypeEnum, EncodeCollection<CheckData>> dic = new Dictionary<CheckItemTypeEnum, EncodeCollection<CheckData>>();
            
            foreach (var item in ecFalse)
            {
                var checkitem =  CheckItem.Instance.GetItembyKey(item.CheckItemID.ToString());
                if (checkitem != null)
                {
                    if (!dic.ContainsKey(checkitem.CheckTypeID))
                    {
                        dic.Add(checkitem.CheckTypeID, new EncodeCollection<CheckData>());                        
                    }
                    dic[checkitem.CheckTypeID].Add(item);
                }
            }
            ReturnValue rv = null;
            if (dic.ContainsKey(CheckItemTypeEnum.A))
            {
                rv = new ReturnValue(false,(int) QualifyJudgeEnum.False, "有1个以上A类不合格");
            }
            else if (dic.ContainsKey(CheckItemTypeEnum.B) && dic[CheckItemTypeEnum.B].Count >= 2)
            {
                rv = new ReturnValue(false, (int)QualifyJudgeEnum.False, "有2个以上B类不合格");
            }
            else if (dic.ContainsKey(CheckItemTypeEnum.D))
            {
                rv = new ReturnValue(false, (int)QualifyJudgeEnum.False, "出现D类不合格");
            }
            else if (dic.ContainsKey(CheckItemTypeEnum.C))
            {
                rv = new ReturnValue(true, (int)QualifyJudgeEnum.UnJudge, "有C类不合格");
            }
            else if (dic.ContainsKey(CheckItemTypeEnum.B))
            {
                rv = new ReturnValue(true, (int)QualifyJudgeEnum.Pass, "有1个B类不合格");
            }
            if (rv != null)
            {
                if (checkitems.Count > 0)
                {
                    return new ReturnValue(true, (int)QualifyJudgeEnum.UnFinish, "有项目未完成,且" + rv.Message);
                }
                return rv;
            }
            return new ReturnValue(true);
        }
        #endregion
    }
}
