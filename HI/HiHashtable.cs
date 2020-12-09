using HI.Abstract;
using System.Collections;
using UnityEngine;

namespace HI
{
	public class HiHashtable : Hashtable
	{
		public override void Add(object key, object value)
		{
			if (base.ContainsKey(key) == true)
			{
				base[key] = value;
			}
			else
			{
				base.Add(key, value);
			}
		}
	}
}
