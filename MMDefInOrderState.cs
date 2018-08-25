using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SSIT.QualityManage.Interface
{
    public enum MMDefInOrderStateEnum : int {
        [Description("已创建")] Creat   = 0,
        [Description("已废弃")] Discard = 4 };
   
}
