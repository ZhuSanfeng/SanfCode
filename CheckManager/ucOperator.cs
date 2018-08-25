using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSITControls.Common;
using SSIT.UserInfo;

namespace SSIT.RetainedSample.UI
{
    //[Designer(typeof(ForbidHeightDesigner))]

    public partial class ucOperator : UserControl
    {
        public ucOperator()
        {
            InitializeComponent();
        }

        public string strRoleIDs { get; set; }
        public List<User> SelectedUsers = new List<User>();

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly
        {
            set
            {
                btnSelectOperator.Visible = !value;
            }
            get
            {
                return !btnSelectOperator.Visible;
            }
        }
        public void SetValue(object value)
        {
            string sOperatorIDs = (string)value;

            SelectedUsers.Clear();
            StringBuilder sb = new StringBuilder();
            List<int> lstID = SSITEncode.Common.STRING.StringToIntList(sOperatorIDs);
            foreach (int id in lstID)
            {
                User user = User.Instance.Itemof(id);
                if (user != null)
                {
                    SelectedUsers.Add(user);
                }
            }
            SetUsers();
        }

        public object GetValue()
        {
            //返回操作员ID 
            StringBuilder sb = new StringBuilder();
            sb.Append(",");
            foreach (User user in SelectedUsers)
            {
                sb.Append(user.ParamID);
                sb.Append(",");
            }
            return  sb.ToString();
        }


        private void btnSelectOperator_Click(object sender, EventArgs e)
        {
            SSIT.UserManager.SelectUsersForm form = new SSIT.UserManager.SelectUsersForm(strRoleIDs, SelectedUsers);
            if (form.ShowDialog() == DialogResult.OK)
            {
                SelectedUsers = form.SelectedUsers;
                SetUsers();
            }
        }
        public void SetUsers()
        {
            StringBuilder sb = new StringBuilder();
            foreach (User user in SelectedUsers)
            {
                sb.Append(user.ParamName);
                sb.Append(" ");
            }
            txtOperatorString.Text = sb.ToString();
            
        }
    }
}
