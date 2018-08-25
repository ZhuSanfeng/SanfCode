using SSIT.EncodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSIT.DataField;
using System.Data;
using SSIT.QMBase;

namespace SSIT.QM.CheckInterface
{
     public class CheckData : CheckDataBase
    {
         [Newtonsoft.Json.JsonIgnore]
         CheckOrder _order = null;

         [Newtonsoft.Json.JsonIgnore]
         public CheckOrder Order
         {
             get
             {
                 if(_order == null)
                 {
                     _order = CheckOrder.GetItembyID(SampleID);
                 }
                 if(_order == null)
                 {
                     _order = new CheckOrder();
                 }
                 return _order;
             }
             set
             {
                 _order = value;
             }
         }

         [Newtonsoft.Json.JsonIgnore]
         [DataField(null,Description="批次号")]
         public string LotID
         {
             get
             {
                 return Order.LotID;
             }
         }

         [Newtonsoft.Json.JsonIgnore]
         [DataField(null,Description="物料名称")]
         public string DefinitionName
         {
             get
             {
                 return Order.DefinitionName;
             }
         }


         public CheckStandard GetCheckStandar()
         {
             CheckStandard.MMTypeID = (int)MMTypEnum.QM;
             CheckStandard cs = CheckStandard.Instance.GetCurrentStandard(Order.DefPK, CheckItemID);
             return cs;
         }
         string standardstr = null;
         [DataField("standardstr", Size = 100)]
         override  public string StandardStr
         {
             get
             {
                 if (standardstr == null)
                 {
                    CheckStandard.MMTypeID = (int)MMTypEnum.QM;
                    CheckStandard cs = CheckStandard.Instance.GetCurrentStandard(Order.DefPK, CheckItemID);
                     if (cs == null)
                     {
                         return null;
                     }
                     //string standardstr = string.Empty;
                     if (!string.IsNullOrWhiteSpace(cs.EntStandardStr))
                     {
                         standardstr = cs.EntStandardStr;
                     }
                     else if (!string.IsNullOrWhiteSpace(cs.NatStandardStr))
                     {
                         standardstr = cs.NatStandardStr;
                     }
                 }
                 return standardstr;
             }
             set
             {
                 standardstr = value;
             }
         }


         #region 静态方法
         static public int GetInputedCount(string sampleid)
         {
             List<int> list = new List<int>();
             var ec = LoadDatasbySampleID(sampleid);
             foreach (var item in ec)
             {
                 if (!list.Contains(item.CheckItemID) && !string.IsNullOrWhiteSpace(item.DataValue))
                 {
                     list.Add(item.CheckItemID);
                 }
             }
             return list.Count;
         }

         static public string DataCommitSetting(string sampleid)
         {
             var ec = LoadDatasbySampleID(sampleid);
             List<string> list = new List<string>();
             foreach (var item in ec)
             {
                 string category = item.CheckCategoryName;
                 if (!string.IsNullOrWhiteSpace(category))
                 {
                     if (!list.Contains(category))
                     {
                         list.Add(category);
                     }
                 }
             }
             return string.Join(" ", list.ToArray());
         }
         public static CheckData FindData(EncodeCollection<CheckData> ec,string checkorderid,int checkitemid,int sampleindex)
         {
            List<CheckData> ret = new List<CheckData>(  ec.Where(item => item.CheckOrderID == checkorderid && item.CheckItemID == checkitemid && item.SampleIndex == sampleindex));
            if (ret.Count > 0)
                return ret[0];
            return null;
         }

         public static CheckData CreateItem(string checkorderid,int checkitemid,int sampleindex)
         {
             CheckData item = new CheckData { CheckOrderID = checkorderid, CheckItemID = checkitemid, SampleIndex = sampleindex };
             return item;
         }

         public static  EncodeCollection<CheckData> LoadDatasbySampleID(string sampleid)
         {
             if (string.IsNullOrWhiteSpace(sampleid))
             {
                 return new EncodeCollection<CheckData>();
             }
             var order = CheckOrder.GetItembyID(sampleid);
             if (order == null)
             {
                 return new EncodeCollection<CheckData>();
             }
             string clause = string.Format("sampleid='{0}'", sampleid);
             EncodeCollection<CheckData> ec = Encode.EncodeData.GetDatas<CheckData>(clause, string.Empty, -1);
             foreach (var item in ec)
             {
                 item.Order = order;
             }
             return ec;
         }

         public static CheckData FindLastData(string sampleid, int checkitemid)
         {
             string clause = string.Format("sampleid='{0}' and checkitemid = {1} ", sampleid, checkitemid);
             EncodeCollection<CheckData> ec = Encode.EncodeData.GetDatas<CheckData>(clause, "sampleindex desc", -1);
             if (ec.Count == 0)
             {
                 return null;
             }
             return ec[0];
         }
         #endregion

    }
}
