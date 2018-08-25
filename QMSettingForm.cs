using SSIT.EncodeBase;
using SSIT.PropertyBase;
using SSITEncode.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SSIT.QualityManage.Settings
{
    public class QMSettingForm : SettingRadForm, IHelp
    {
        #region 帮助属性实现
        public string HelpFileName
        {
            get
            {
                return "系统OPC关联配置.pdf";
            }
        }
        #endregion

        public override void LoadPages()
        {
            //SSITOPCServer.Instance.RefreshDatas(true);
            //SSITOPCGroup.Instance.RefreshDatas(true);
            //SSITOPCTag.Instance.RefreshDatas(true);

            this.Text = "系统OPC关联配置";
            PropertySettings ps = Settings;
            base.LoadPages(ps, "");
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            //判断权限
            //this.AllowEdit = SSIT.UserManager.Security.CheckLoginAndRight(UserManager.RoleSettings.MyRoleRight.OPCSetting_Edit);
            this.AllowEdit = true;

        }

        public static PropertySettings Settings
        {
            get
            {
                System.Reflection.Assembly l_ExecAs = System.Reflection.Assembly.GetExecutingAssembly();
                XmlSerializer Ser = new XmlSerializer(typeof(PropertySettings));
                return (PropertySettings)Ser.Deserialize
                    (l_ExecAs.GetManifestResourceStream("SSIT.QualityManage.Settings.QMSettings.config"));

            }
        }

        public override void Refresh()
        {
            //SSITOPCServer.Instance.RefreshDatas(true);
            //SSITOPCGroup.Instance.RefreshDatas(true);
            //SSITOPCTag.Instance.RefreshDatas(true);

            base.Refresh();
        }

    }
}
