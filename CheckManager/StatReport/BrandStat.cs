using SSIT.EncodeBase;
using SSIT.QM.CheckInterface;
using System;
using System.Data;

namespace SSIT.QM.CheckManager.StatReport
{
	/// <summary>
	/// BrandStat ��ժҪ˵����
	/// </summary>
    public class BrandStat : IComparable<BrandStat>
	{
		public bool IsValid = false;

		private EncodeCollection<CheckOrder> _gc;
		private StatCollection _stats = new StatCollection ();
        private string _head;//����

		public BrandStat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			
		}

		public void Init ()
		{
			//���鼯�ϵ���ȷ��
			IsValid = false;
			if (_gc == null || _gc.Count == 0)
				return;

			IsValid = true;

            Stat stat = new Stat();
            stat.Groups = _gc;
            _stats.Add(stat);

		}

		public EncodeCollection<CheckOrder> Groups
		{
			get 
			{
				if (_gc == null)
					_gc = new EncodeCollection<CheckOrder> ();
				return _gc;
			}
		}


		public StatCollection Stats
		{
			get{return _stats;}
		}

        public string Head
        {
            get { return _head; }
            set { _head = value; }
        }

        #region IComparable<BrandStat> ��Ա

        public int CompareTo(BrandStat other)
        {
            if (other == null)
                return 0;
            if (this.Equals(other))
                return 0;
            return 0;
            //string b1 = this.GetBrand();
            //string b2 = other.GetBrand();
            
            //if (string.IsNullOrEmpty(b1))
            //{
            //    return -1;
            //}
            //if (string.IsNullOrEmpty(b2))
            //{
            //    return 0;
            //}
            //int ret= b1.CompareTo(b2);
            //return ret;
        }

        #endregion
    }
}
