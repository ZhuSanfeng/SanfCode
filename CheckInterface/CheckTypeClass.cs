using System;
using System.Linq;
using System.Text;
using SSIT.EncodeBase;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using SSITEncode.Common;

namespace SSIT.QM.CheckInterface
{
    /// <summary>
    /// 检验类型
    /// </summary>
    public enum CheckTypeEnum : int {
        [Description("常规")]Normal = 0,
        [Description("复检")]ReCheck = 1,
        [Description("首检")] First = 2,
        [Description("末检")] Last = 3,
        [Description("巡检")]Routing = 4
    };

     public class CheckTypeClass
    {
        public CheckTypeEnum Key { get; set; }
        public string Value { get; set; }

        static public List<CheckTypeClass> Datas
        {
            get
            {
               var ids = CheckTypeEnum.GetValues(typeof(CheckTypeEnum));
                List<CheckTypeClass> datas = new List<CheckTypeClass>();
                foreach(CheckTypeEnum key in ids)
               
                {
                    datas.Add(new CheckTypeClass { Key = key, Value = key.GetDescription() });
                }
                return datas;
            }
        }
    }

}
