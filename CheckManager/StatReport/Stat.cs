using SSIT.EncodeBase;
using SSITSPC.ReView.SPC;
using System;
using System.Data;
using SSIT.DataField;
using SSIT.QMBase;
using SSIT.QM.CheckInterface;

namespace SSIT.QM.CheckManager.StatReport
{
	/// <summary>
	/// Stat 的摘要说明。
	/// </summary>
	public class Stat
	{

		private EncodeCollection<CheckOrder> _gc;

		private int _disquagroupcount = -1;
		
		private double _quagrouppercent;
		

		public Stat()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		public void Init ()
		{


		}

		public EncodeCollection<CheckOrder> Groups
		{
			get 
			{
				if (_gc == null)
					_gc = new EncodeCollection<CheckOrder> ();
				return _gc;
			}
            set
            {
                _gc = value;
            }
		}

		//统计组数
		[DataField ("groupcount", Type = DbType.Int32, Description = "统计数")]
		public int GroupCount
		{
            get { return Groups.Count; }
		}

		//不合格组数
        [DataField("disquagroupcount", Type = DbType.Int32, Description = "不合格数")]
        public int DisQuaGroupCount
        {
            get
            {
                if (this._disquagroupcount == -1)
                {
                    this._disquagroupcount = 0;
                    foreach (CheckOrder group in _gc)
                    {
                        if (group.QualifyJudge == QualifyJudgeEnum.False)
                            this._disquagroupcount++;
                    }
                }
                return _disquagroupcount;
            }
        }
 
		//组合格率
		[DataField ("quagrouppercent", Type = DbType.String, Description = "合格率%")]
		public string QuaGroupPercent
		{
			get 
			{
				this._quagrouppercent = 100f * (1f - (double)this.DisQuaGroupCount / _gc.Count);
				return _quagrouppercent.ToString ("f2");
			}
		}

        [DataField("lotquagrouppercent", Type = DbType.String, Description = "批合格率%")]
        public string LotQuaGroupPercent
        {
            get
            {
                this._quagrouppercent = 100f * (1f - (double)this.DisQuaGroupCount / _gc.Count);
                return _quagrouppercent.ToString("f2");
            }
        }

        //[DataField("baduse", Type = DbType.String, Description = "降级使用率")]
        public string UsageDecisionStat(int decision)
        {
            float cout = 0;
            foreach (CheckOrder group in _gc)
            {
                if (group.UsageDecisions == decision)
                    cout++;
            }
            return cout.ToString();
            //return (100f * cout / _gc.Count).ToString("f2");

        }

        //[DataField("usegood", Type = DbType.String, Description = "发布合格率")]
        public string UseGoodRate
        {
            get
            {
                float cout = 0;
                foreach (CheckOrder group in _gc)
                {
                    if (group.UsageDecisions == 0)// || string.IsNullOrWhiteSpace(group.UsageDecisions))
                        cout++;
                }
                return (100f * cout / _gc.Count).ToString("f2");
            }
        }

        [DataField("lotquantity", Type = DbType.String, Description = "批总数")]
        public string LotQuantity
        {
            get
            {
                float cout = 0;
                foreach (CheckOrder group in _gc)
                {
                    cout += group.LotQuantity;
                }
                return cout.ToString();
            }
        }
		public virtual FieldValue GetFieldValue (string ColumnName)
		{
			foreach (FieldValue fv in FieldManager.GetFieldValues(this))
			{
				if (fv.field.ColumnName == ColumnName)
					return fv;
			}
			return null;
		}

		public virtual object GetFieldValue (DataFieldAttribute field)
		{
			FieldValue fv = this.GetFieldValue (field.ColumnName);
			if (fv != null)
				return fv.value;
			return null;
		}
	}
}
