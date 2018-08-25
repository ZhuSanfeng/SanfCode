using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSIT.EncodeBase;
using SSIT.DataField;

namespace SSIT.QualityManage.Interface
{
   public class Supplier : ParamClassBase<Supplier>
    {
        public override string GetTableName()
        {
            return "ssit_qm_supplier";
        }

        [DataField("SupplierID", Size = 20, Description = "供应商编号", IsPrimaryKey = true)]
        public string SupplierID { get; set; }
        public int SupPK
        {
            get
            {
                return ParamID;
            }
            set
            {
                ParamID = value;
            }
        }

        [DataField(null, Description = "供应商名称")]
        public string SupplierName {
            get
            {
                return Supplier.Instance.GetNamebyID(ParamID);
            }
        }

        [DataField("SupProperty", Size = 64, Description = "企业性质")]
        public string SupProperty { get; set; }

        [DataField("SupArea", Size = 64, Description = "地区")]
        public string SupArea { get; set; }

        [DataField("RegAddr",Size =64,Description ="公司注册地址")]
        public string RegAddr { get; set; }

        [DataField("Corporation",Size =32,Description ="法人")]
        public string Corporation { get; set; }

        [DataField("Contact",Size =32,Description ="联系人")]
        public string Contact { get; set; }

        [DataField("ContactAddr", Size = 64, Description = "联系人地址")]
        public string ContactAddr { get; set; }

    }
}
