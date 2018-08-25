using System;
using System.Xml.Serialization;
using System.Collections;

namespace SSIT.QM.CheckManager.StatReport
{
	/// <summary>
	/// BrandStatCollection 的摘要说明。
	/// </summary>
	[Serializable]
	public class BrandStatCollection : CollectionBase
	{
		public BrandStat Add(BrandStat value)
		{
			// Use base class to process actual collection operation
			base.List.Add(value as object);

			return value;
		}

		public void AddRange(BrandStat[] values)
		{
			// Use existing method to add each array entry
			foreach(BrandStat item in values)
				Add(item);
		}

		public void Remove(BrandStat value)
		{
			// Use base class to process actual collection operation
			base.List.Remove(value as object);
		}

		public void Insert(int index, BrandStat value)
		{
			// Use base class to process actual collection operation
			base.List.Insert(index, value as object);
		}

		public bool Contains(BrandStat value)
		{
			// Use base class to process actual collection operation
			return base.List.Contains(value as object);
		}

		public bool Contains(BrandStatCollection values)
		{
			foreach(BrandStat c in values)
			{
				// Use base class to process actual collection operation
				if (Contains(c))
					return true;
			}

			return false;
		}

		public  BrandStat this[int index]
		{
			// Use base class to process actual collection operation
			get { return (base.List[index] as BrandStat); }
		}
				 
		public int IndexOf(BrandStat value)
		{
			// Find the 0 based index of the requested entry
			return base.List.IndexOf(value);
		}

		public BrandStatCollection Copy()
		{
			BrandStatCollection clone = new BrandStatCollection();

			// Copy each reference across
			foreach(BrandStat c in base.List)
				clone.Add(c);

			return clone;
		}
	}
}
