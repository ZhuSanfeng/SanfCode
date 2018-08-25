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
using YHDataInterface.SSITMM;

namespace SSIT.QualityManage.Settings
{
    public partial class SupAndMMRelationGrid : EncodeGrid<SupAndMMRelation>
    {

        public const int iDefID = 0;
        public const int iDefName = 1;
        public const int iExemption = 2;
        public int SupPK = -1;
        
        public SupAndMMRelationGrid() : base()
        {
            Opt = Operation.opt3;
        }

        public override void Init()				//初始化Grid
        {
            base.Init();
            //	物料编号	物料名称	是否免检

           InsertColumn(iDefID, "物料编号", ColumnStyle.ReadOnly);	//Grid的展示列——第一列的列头名称
           InsertColumn(iDefName, "物料名称", ColumnStyle.ReadOnly);
           
           InsertColumn(iExemption, "检查类型",  new string[] { "免检","常规检","调整检"});
          
            _encodes = SupAndMMRelation.Instance.Datas;
            EncodeCollection<SupAndMMRelation> supmmre = new EncodeCollection<SupAndMMRelation>();
            foreach(var item in SupAndMMRelation.Instance.GetEnableCollection())
            {
                if (item.SupPK == SupPK)
                {
                    supmmre.Add(item);
                }
            }

            LoadGrid(supmmre);
            //this.NewRow();
        }

        public override void InsertRow(int Row, SupAndMMRelation encode)
        {
            base.InsertRow(Row, encode);
            FillColumn(Row, iDefID, encode.DefID);
            FillColumn(Row, iDefName, encode.ParamName);
            FillColumn(Row, iExemption, encode.GetExemption);
            
        }

        public override bool UpdateRow(int row)
        {
            if (!CheckValues(row))
                return false;
            var Row = row - FixedRows;

            var encode = GetEncode(Row);

            string defID = GetStringValue(Row,iDefID);
            string defName = GetStringValue(Row, iDefName);
            string exemption = GetStringValue(Row, iExemption);            //string supProperty = GetStringValue(Row, iExemption);
            var mmdef = MMDefinition.Instance.Datas.FirstOrDefault(p => p.Enable && p.DefID == defID);
            IsExemptionEnum isexemption = IsExemptionEnum.convention;
            if (exemption.Equals("常规检")) isexemption = IsExemptionEnum.convention;
            if (exemption.Equals("免检")) isexemption = IsExemptionEnum.exemption;
            if (exemption.Equals("调整检")) isexemption = IsExemptionEnum.Adjustment;

            //if (encode.SupplierID == supID && encode.ParamName == supName && encode.SupProperty==supProperty && encode.SupArea ==supArea
            //    &&encode.RegAddr == regAddr && encode.Corporation == corporation && encode.Contact == contact && encode.ContactAddr ==contactAddr)
            //{
            //    return false;
            //}
            var clone = encode.CloneItem();
            clone.DefPK = mmdef.ParamID;
            clone.SupPK = SupPK;
            clone.ParamName = defName;
            clone.IsExemption = isexemption;
            
            //clone.SupplierID = supID;
            //clone.ParamName = supName;
            //clone.SupProperty = supProperty;
            //clone.SupArea = supArea;
            //clone.RegAddr = regAddr;
            //clone.Corporation = corporation;
            //clone.Contact = contact;
            //clone.ContactAddr = contactAddr;

            if (IsDuplicate(row, clone))
                return false;
            return true;
        }



        public override void DeleteRow(int row)
        {
            int Row = row + FixedRows;

            if (this[Row, 0] == null)
                return;

            SupAndMMRelation encode = (SupAndMMRelation)this[Row, 0].Tag;

            

            //string clause = string.Format("CollegePK = {0}", encode.ParamID);
            //EncodeCollection<Major> ecLoc = Encode.EncodeData.GetDatas<Major>(clause, "", 1);
            //if (ecLoc.Count > 0)
            //{
            //    ReturnValue.ShowError("该学院已经设置了专业信息，不能被删除！");
            //    return;
            //}
            base.DeleteRow(row);
            //更新数据
        }
    }
}
