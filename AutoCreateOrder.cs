using System;
using System.Collections.Generic;
using System.Linq;
using SSIT.EncodeBase;
using SSIT.QM.CheckInterface;
using SSIT.QM.SampleInterface;
using SSIT.QMBase;
using SSIT.QualityManage.Interface;
using SSITEncode.Common;

namespace SSIT.QualityManage.Function
{
    public class AutoCreateOrder
    {
        public SampleOrder newSampleOrder = new SampleOrder();
        public bool CreateSampleOrderByMMInOrder(MMInOrder OrderItem,bool modifyOrNot)
        {
            bool isSuccess = false;
            SampleOrder SampleOrderItem = new SampleOrder();
            SampleOrderItem.DefPK = OrderItem.DefPK;              //物料PK
            SampleOrderItem.LotID = OrderItem.BatchID;           //批次号
            SampleOrderItem.SampleDate = OrderItem.SynTime;      //抽样时间
            SampleOrderItem.SourceOrderID = OrderItem.CheckLot;       //来源单号
            SampleOrderItem.Creator = OrderItem.Creator;             //创建人
            SampleOrderItem.CreateTime = OrderItem.SynTime;          //创建时间
            SampleOrderItem.SampleQuantity = 50;                    //样品数量
            SampleOrderItem.SampleType =  SampleTypeEnum.PackagesComming;  //样品类型
            SampleOrderItem.CheckType = CheckTypeEnum.Normal;      //检验类型
            SampleOrderItem.SupPK = OrderItem.SupPK;               //供应商
            //新建
            if (!modifyOrNot)
            {
                SampleOrderItem.SampleID = EncodeHelper.GetOrderID<SampleOrder>("SampleID", "S");//样品ID
                SampleOrderItem.State = DataState.New;
            }
            else
            {
                SampleOrderItem.State = DataState.Changed;
            }
            SampleOrderItem.SamplerPK = -1;
            
            ReturnValue rv = SSIT.EncodeBase.Encode.EncodeData.SaveDatas<SampleOrder>(new EncodeCollection<SampleOrder>(SampleOrderItem));
            if (rv.Success)
            {
                newSampleOrder = SampleOrderItem;
                ReturnValue.ShowMessage("已自动生成样品单！");
                CheckOrder CheckOrderItem = new CheckOrder
                {
                    SampleID = SampleOrderItem.SampleID,
                    LotID = SampleOrderItem.LotID,
                    HutPK = SampleOrderItem.HutPK,
                    DefPK = SampleOrderItem.DefPK,
                    SourceOrderID = SampleOrderItem.SourceOrderID,
                    SampleDate = SampleOrderItem.SampleDate,
                    CheckQuantity = 1,
                    CheckType = CheckTypeEnum.Normal,
                    State = DataState.New
                };
                string PlanCheckItemString = SSIT.QMBase.DefinitionCheckItemCombine.GetCheckItemstringby(CheckOrderItem.DefPK, SSIT.QMBase.MMTypEnum.QM);
                if (PlanCheckItemString.IsNullOrEmpty())
                {
                    var order = (CheckOrder)CheckOrderItem.Clone();
                    order.IDENTITY = null;
                    order.CreateTime = DateTime.Now.ToString(EncodeConst.DateTimeFormat);
                    order.Creator = null;
                    order.CheckOrderID = EncodeHelper.GetOrderID<CheckOrder>("CheckOrderID", "C");
                    string frontWorker = AllocationWorker(true);
                    order.PlanInspector = frontWorker;
                    rv = SSIT.EncodeBase.Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(order));
                    if (rv.Success)
                    {
                        isSuccess = true;
                        ReturnValue.ShowMessage("已自动生成检测单！");
                    }
                    return isSuccess;
                }
                Dictionary<int, List<CheckItem>> dick = new Dictionary<int, List<CheckItem>>();
                string[] checkItems = PlanCheckItemString.Split(',');
                foreach (string checkItem in checkItems)
                {
                    CheckItem ci = CheckItem.Instance.Datas.FirstOrDefault(p=> p.Enable && p.ParamID == int.Parse(checkItem));
                    if (!dick.ContainsKey(ci.CheckCategoryID))
                    {
                        dick.Add(ci.CheckCategoryID,new List<CheckItem>());
                    }
                    dick[ci.CheckCategoryID].Add(ci);
                }
                //根据dick的数量创建检测单
                foreach (var items in dick.Values)
                {
                    //用linq，根据value查字典的key，即检测大类ID
                    int CheckType = dick.FirstOrDefault(p => p.Value == items).Key; 
                    //CheckCategoryID   ——  8包材前道  9包材后道
                    var order = (CheckOrder)CheckOrderItem.Clone();
                    order.IDENTITY = null;
                    var list = items.Select(p => p.ParamID).ToList();
                    order.PlanCheckItemString = list.ListIntToString();
                    if (order.State == DataState.New)
                    {
                        order.CreateTime = DateTime.Now.ToString(EncodeConst.DateTimeFormat);
                        order.Creator = null;
                        order.CheckOrderID = EncodeHelper.GetOrderID<CheckOrder>("CheckOrderID", "C");
                        //调用分配函数
                        //包材前道
                        if (CheckType == 8)
                        {
                            string frontWorker = AllocationWorker(true);
                            order.PlanInspector = frontWorker;
                        }
                        //包材后道
                        if (CheckType == 9)
                        {
                            string backWorker = AllocationWorker(false);
                            order.PlanInspector = backWorker;
                        }
                    }
                    rv = SSIT.EncodeBase.Encode.EncodeData.SaveDatas<CheckOrder>(new EncodeCollection<CheckOrder>(order));
                    if (rv.Success)
                    {
                        isSuccess = true;
                        ReturnValue.ShowMessage("已自动生成检测单！");
                    }
                }
            }
            return isSuccess;
        }

        //随机分配函数
        public string AllocationWorker(bool isFront)
        {
            string TheChosenChild = null;   //被选中的孩子
            //前道工人
            if (isFront)
            {
                var frontType = TheChosenChildrenType.Instance.Datas.FirstOrDefault(p => p.Enable & p.ParamName == "包材前道");
                int frontTypeID = frontType.ParamID;
                var frontWorkers = Encode.EncodeData.GetDatas<TheChosenChildren>(string.Format("TheChosenChildrenTypePK={0}", frontTypeID), string.Empty, 1);
                int frontCount = frontWorkers.Count;
                if (frontCount > 0)
                {
                    Random random = new Random();
                    int choise = random.Next(1, frontCount + 1);
                    TheChosenChildren frontLuckyDog = frontWorkers[choise - 1];
                    TheChosenChild = frontLuckyDog.ParamName;
                }
            }
            //后道工人
            else
            {
                var backType = TheChosenChildrenType.Instance.Datas.FirstOrDefault(p => p.Enable & p.ParamName == "包材后道");
                int backTypeID = backType.ParamID;
                var backWorkers = Encode.EncodeData.GetDatas<TheChosenChildren>(string.Format("TheChosenChildrenTypePK={0}", backTypeID), string.Empty, 1);
                int backCount = backWorkers.Count;
                if (backCount > 0)
                {
                    Random random = new Random();
                    int choise = random.Next(1, backCount + 1);
                    TheChosenChildren backLuckyDog = backWorkers[choise - 1];
                    TheChosenChild = backLuckyDog.ParamName;
                }
            }         
            return TheChosenChild;
            /**                                                                    
 *            .,,       .,:;;iiiiiiiii;;:,,.     .,,                   
 *          rGB##HS,.;iirrrrriiiiiiiiiirrrrri;,s&##MAS,                
 *         r5s;:r3AH5iiiii;;;;;;;;;;;;;;;;iiirXHGSsiih1,               
 *            .;i;;s91;;;;;;::::::::::::;;;;iS5;;;ii:                  
 *          :rsriii;;r::::::::::::::::::::::;;,;;iiirsi,               
 *       .,iri;;::::;;;;;;::,,,,,,,,,,,,,..,,;;;;;;;;iiri,,.           
 *    ,9BM&,            .,:;;:,,,,,,,,,,,hXA8:            ..,,,.       
 *   ,;&@@#r:;;;;;::::,,.   ,r,,,,,,,,,,iA@@@s,,:::;;;::,,.   .;.      
 *    :ih1iii;;;;;::::;;;;;;;:,,,,,,,,,,;i55r;;;;;;;;;iiirrrr,..       
 *   .ir;;iiiiiiiiii;;;;::::::,,,,,,,:::::,,:;;;iiiiiiiiiiiiri         
 *   iriiiiiiiiiiiiiiii;;;::::::::::::::::;;;iiiiiiiiiiiiiiiir;        
 *  ,riii;;;;;;;;;;;;;:::::::::::::::::::::::;;;;;;;;;;;;;;iiir.       
 *  iri;;;::::,,,,,,,,,,:::::::::::::::::::::::::,::,,::::;;iir:       
 * .rii;;::::,,,,,,,,,,,,:::::::::::::::::,,,,,,,,,,,,,::::;;iri       
 * ,rii;;;::,,,,,,,,,,,,,:::::::::::,:::::,,,,,,,,,,,,,:::;;;iir.      
 * ,rii;;i::,,,,,,,,,,,,,:::::::::::::::::,,,,,,,,,,,,,,::i;;iir.      
 * ,rii;;r::,,,,,,,,,,,,,:,:::::,:,:::::::,,,,,,,,,,,,,::;r;;iir.      
 * .rii;;rr,:,,,,,,,,,,,,,,:::::::::::::::,,,,,,,,,,,,,:,si;;iri       
 *  ;rii;:1i,,,,,,,,,,,,,,,,,,:::::::::,,,,,,,,,,,,,,,:,ss:;iir:       
 *  .rii;;;5r,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,sh:;;iri        
 *   ;rii;:;51,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,.:hh:;;iir,        
 *    irii;::hSr,.,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,.,sSs:;;iir:         
 *     irii;;:iSSs:.,,,,,,,,,,,,,,,,,,,,,,,,,,,..:135;:;;iir:          
 *      ;rii;;:,r535r:...,,,,,,,,,,,,,,,,,,..,;sS35i,;;iirr:           
 *       :rrii;;:,;1S3Shs;:,............,:is533Ss:,;;;iiri,            
 *        .;rrii;;;:,;rhS393S55hh11hh5S3393Shr:,:;;;iirr:              
 *          .;rriii;;;::,:;is1h555555h1si;:,::;;;iirri:.               
 *            .:irrrii;;;;;:::,,,,,,,,:::;;;;iiirrr;,                  
 *               .:irrrriiiiii;;;;;;;;iiiiiirrrr;,.                    
 *                  .,:;iirrrrrrrrrrrrrrrrri;:.                        
 *                        ..,:::;;;;:::,,.                             
 */
        }
    }
}
