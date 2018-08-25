using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SSIT.QualityManage.Interface;
using SSIT.EncodeBase;
using SSIT.PropertyBase;

namespace SSIT.QualityManage.Settings
{
    public partial class SupplierGrid : EncodeGrid<Supplier>
    {

        public const int isupID = 0;
        public const int isupName = 1;
        public const int isupProperty = 2;
        public const int isupArea = 3;
        public const int iregAddr = 4;
        public const int icorporation = 5;
        public const int icontact = 6;
        public const int icontactAddr = 7;

        public SupplierGrid() : base()
        {
            
        }

        public override void Init()				//初始化Grid
        {
            base.Init();
            //	供应商编号	供应商名称	企业性质	地区	公司注册地址	法人	联系人	联系地址

            InsertColumn(isupID, "供应商编号", ColumnStyle.Common);	//Grid的展示列——第一列的列头名称
            InsertColumn(isupName, "供应商名称", ColumnStyle.Common);
            InsertColumn(isupProperty, "企业性质", ColumnStyle.Common);
            InsertColumn(isupArea, "地区", ColumnStyle.Common);
            InsertColumn(iregAddr, "公司注册地址", ColumnStyle.Common);
            InsertColumn(icorporation, "法人", ColumnStyle.Common);
            InsertColumn(icontact, "联系人", ColumnStyle.Common);
            InsertColumn(icontactAddr, "联系人地址", ColumnStyle.Common);
            _encodes = Supplier.Instance.Datas;


            LoadGrid(Supplier.Instance.GetEnableCollection());
            this.NewRow();
        }

        public override void InsertRow(int Row, Supplier encode)
        {
            base.InsertRow(Row, encode);
            FillColumn(Row, isupID, encode.SupplierID);
            FillColumn(Row, isupName, encode.ParamName);
            FillColumn(Row, isupProperty, encode.SupProperty);
            FillColumn(Row, isupArea, encode.SupArea);
            FillColumn(Row, iregAddr, encode.RegAddr);
            FillColumn(Row, icorporation, encode.Corporation);
            FillColumn(Row, icontact, encode.Contact);
            FillColumn(Row, icontactAddr, encode.ContactAddr);
        }

        public override bool UpdateRow(int row)
        {
            if (!CheckValues(row))
                return false;
            var Row = row - FixedRows;

            var encode = GetEncode(Row);

            string supID = GetStringValue(Row,isupID);
            string supName = GetStringValue(Row, isupName);
            string supProperty = GetStringValue(Row, isupProperty);
            string supArea = GetStringValue(Row, isupArea);
            string regAddr = GetStringValue(Row, iregAddr);
            string corporation = GetStringValue(Row, icorporation);
            string contact = GetStringValue(Row, icontact);
            string contactAddr = GetStringValue(Row, icontactAddr);


            if (encode.SupplierID == supID && encode.ParamName == supName && encode.SupProperty==supProperty && encode.SupArea ==supArea
                &&encode.RegAddr == regAddr && encode.Corporation == corporation && encode.Contact == contact && encode.ContactAddr ==contactAddr)
            {
                return false;
            }
            var clone = encode.CloneItem();
            clone.SupplierID = supID;
            clone.ParamName = supName;
            clone.SupProperty = supProperty;
            clone.SupArea = supArea;
            clone.RegAddr = regAddr;
            clone.Corporation = corporation;
            clone.Contact = contact;
            clone.ContactAddr = contactAddr;

            if (IsDuplicate(row, clone))
                return false;
            return true;
        }



        public override void DeleteRow(int row)
        {
            int Row = row + FixedRows;

            if (this[Row, 0] == null)
                return;

            Supplier encode = (Supplier)this[Row, 0].Tag;

            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("确定要删除供应商么“" + encode.ParamName + "”么？"
            , "注意", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
            {
                return;
            }

            //string clause = string.Format("SupID = {0}", encode.SupplierID);
            //EncodeCollection<MMInOrder> ecLoc = Encode.EncodeData.GetDatas<MMInOrder>(clause, "", 1);
            //if (ecLoc.Count > 0)
            //{
            //    ReturnValue.ShowError("该信息已被其他表引用，不能被删除！");
            //    return;
            //}
            base.DeleteRow(row);
            //更新数据
        }
    }
}
