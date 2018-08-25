using SSIT.DataField;
using SSIT.EncodeBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SSIT.QualityManage.Interface
{
    public class TheChosenChildren : ParamClassBase<TheChosenChildren>
    {
        public override string GetTableName()
        {
            return "TheChosenChildren";
        }

        [DataField("TheChosenChildrenTypePK", Type = DbType.Int32, IsPrimaryKey = true)]
        public int TheChosenChildrenTypePK { get; set; }       
    }
}
