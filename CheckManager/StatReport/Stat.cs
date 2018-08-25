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
	/// Stat ��ժҪ˵����
	/// </summary>
	public class Stat
	{

		private EncodeCollection<CheckOrder> _gc;

		private int _disquagroupcount = -1;
		
		private double _quagrouppercent;
		

		public Stat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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

		//ͳ������
		[DataField ("groupcount", Type = DbType.Int32, Description = "ͳ����")]
		public int GroupCount
		{
            get { return Groups.Count; }
		}

		//���ϸ�����
        [DataField("disquagroupcount", Type = DbType.Int32, Description = "���ϸ���")]
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
 
		//��ϸ���
		[DataField ("quagrouppercent", Type = DbType.String, Description = "�ϸ���%")]
		public string QuaGroupPercent
		{
			get 
			{
				this._quagrouppercent = 100f * (1f - (double)this.DisQuaGroupCount / _gc.Count);
				return _quagrouppercent.ToString ("f2");
			}
		}

        [DataField("lotquagrouppercent", Type = DbType.String, Description = "���ϸ���%")]
        public string LotQuaGroupPercent
        {
            get
            {
                this._quagrouppercent = 100f * (1f - (double)this.DisQuaGroupCount / _gc.Count);
                return _quagrouppercent.ToString("f2");
            }
        }

        //[DataField("baduse", Type = DbType.String, Description = "����ʹ����")]
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

        //[DataField("usegood", Type = DbType.String, Description = "�����ϸ���")]
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

        [DataField("lotquantity", Type = DbType.String, Description = "������")]
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
