using SSIT.DataField;
using SSIT.EncodeBase;
using SSITEncode.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YHDataInterface.SSITMM;

namespace SSIT.QualityManage.Interface
{
   public class MMInOrder : DataClassBase<MMInOrder>
    {
        //到货单
        public override string GetTableName()
        {
            return "ssit_qm_mminorder";
        }
        [DataField("OrderID", Size = 64, IsPrimaryKey = true, Description = "单号")]
        public string OrderID { get; set; }

        [DataField("PurchaseOrderPK", Size = 64, Description = "采购订单号")]//采购订单号
        public string PurchaseOrderPK { get; set; }

        [DataField("OrderState", Type = System.Data.DbType.Int16)]
        public MMDefInOrderStateEnum OrderState { get; set; }

        [DataField(null, Description = "状态")]
        public string OrderStateName
        {
            get
            {
                return OrderState.GetDescription();
            }
        }

        [DataField("DefPK", Type = DbType.Int32)]
        public int DefPK { get; set; }

        private MMDefinition _Def;

        [DataField(null, Description = "物料编号")]
        public string DefID
        {
            get
            {
                if (_Def == null)
                    _Def = MMDefinition.Instance.Datas.FirstOrDefault(x => x.ParamID == DefPK);
                if (_Def != null)
                    return _Def.DefID;
                return "";
            }
        }

        [DataField(null, Description = "物料名称")]
        public string DefName
        {
            get
            {
                if (_Def == null)
                    _Def = MMDefinition.Instance.Datas.FirstOrDefault(x => x.ParamID == DefPK);
                if (_Def != null)
                    return _Def.DefName;
                return "";
            }
        }

        [DataField("CheckLot",Size = 64,Description ="检验批",IsPrimaryKey =true)]
        public string CheckLot { get; set; }

        [DataField("BatchID",Size =128,Description ="物料批次号")]
        public string BatchID { get; set; }

        [DataField("DefCount", Type = DbType.Int32, Description = "数量")]
        public int DefCount { get; set; }

        [DataField("CarID",Size =32,Description ="车号")]
        public string CarID { get; set; }

        [DataField("SupPK",Type = DbType.Int32)]
        public int SupPK { get; set; }

        private Supplier _Sup;

        [DataField(null, Description = "企业编号")]
        public string SupID
        {
            get
            {
                if (_Sup == null)
                    _Sup = Supplier.Instance.Datas.FirstOrDefault(x => x.ParamID == SupPK);
                if (_Sup != null)
                    return _Sup.SupplierID;
                return "";
            }
        }

        [DataField(null, Description = "企业名称")]
        public string SupName
        {
            get
            {
                if (_Sup == null)
                    _Sup = Supplier.Instance.Datas.FirstOrDefault(x => x.ParamID == SupPK);
                if (_Sup != null)
                    return _Sup.SupplierName;
                return "";
            }
        }

        [DataField("Creator",Size =32,Description ="制单人")]//制单人
        public string Creator { get; set; }

        [DataField("Note",Size = 128,Description ="备注")]//备注
        public string Note { get; set; }

        [DataField("SynTime",Size = 32,Description ="同步时间")]//同步时间
        public string SynTime { get; set; }

       

        //[DataField("")]
        static public string GetNewOrderID(DateTime dt)
        {
            return EncodeHelper.GetOrderID<MMInOrder>("OrderID", "XY");
        }
    }
}
