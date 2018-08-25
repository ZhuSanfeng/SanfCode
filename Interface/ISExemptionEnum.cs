using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SSIT.QualityManage.Interface
{
    public enum IsExemptionEnum : int
    {
        [Description("免检")] exemption = 0,
        [Description("常规检")] convention =1,
        [Description("调整检")] Adjustment = 2    
    };
}
