using System;
using System.Xml.Serialization;
using System.Collections;

namespace SSIT.QM.CheckManager.StatReport
{
	/// <summary>
	/// StatCollection 的摘要说明。
	/// </summary>
	[Serializable]
	public class StatCollection : CollectionBase
	{
		public Stat Add(Stat value)
		{
			// Use base class to process actual collection operation
			base.List.Add(value as object);

			return value;
		}

		public void AddRange(Stat[] values)
		{
			// Use existing method to add each array entry
			foreach(Stat item in values)
				Add(item);
		}

		public void Remove(Stat value)
		{
			// Use base class to process actual collection operation
			base.List.Remove(value as object);
		}

		public void Insert(int index, Stat value)
		{
			// Use base class to process actual collection operation
			base.List.Insert(index, value as object);
		}

		public bool Contains(Stat value)
		{
			// Use base class to process actual collection operation
			return base.List.Contains(value as object);
		}

		public  Stat this[int index]
		{
			// Use base class to process actual collection operation
			get { return (base.List[index] as Stat); }
		}
				 
		public int IndexOf(Stat value)
		{
			// Find the 0 based index of the requested entry
			return base.List.IndexOf(value);
		}

		public StatCollection Copy()
		{
			StatCollection clone = new StatCollection();

			// Copy each reference across
			foreach(Stat c in base.List)
				clone.Add(c);

			return clone;
		}
	}
}
