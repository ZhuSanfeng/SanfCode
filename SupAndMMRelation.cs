using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSIT.EncodeBase;
using SSIT.DataField;
using System.Data;
using YHDataInterface.SSITMM;
using SSITEncode.Common;

namespace SSIT.QualityManage.Interface
{
   public class SupAndMMRelation : ParamClassBase<SupAndMMRelation>
    {
        public SupAndMMRelation()
        {
            IsExemption = IsExemptionEnum.convention;
        }
        public override string GetTableName()
        {
            return "ssit_qm_supandmmrelation";
        }

        [DataField("SupPK", Type = DbType.Int32, IsPrimaryKey = true, Description = "SupPK")]   //供应商ID
        public int SupPK { get; set; }

        [DataField(null, Description = "供应商名称")]
        public string SupplierName {
            get
            {
                return Supplier.Instance.GetNamebyID(SupPK);
            }
        }



        [DataField("DefPK", Type = DbType.Int32, IsPrimaryKey = true,Description ="DefID")]   //物料ID
        public int DefPK { get; set; }


        [DataField(null, Description = "物料名称")]
        public string DefName
        {
            get
            {
                var mmdef = MMDefinition.Instance.GetNamebyID(DefPK);
                return  mmdef; 
            }
        }

        [DataField("DefID",Size = 64, Description = "物料编号")]
        public string DefID
        {
            get;set;
        }

       

        [DataField("IsExemption",Type = DbType.Int16)]
        public IsExemptionEnum IsExemption
        {
            get;set;
        }

        [DataField(null,Description ="检查类型")]
        public string GetExemption
        {
            get
            {
                return IsExemption.GetDescription();
            }
        }
        

    }
}
