using UnityEngine;

namespace HI.Abstract
{
	public abstract class HiScrollViewItem : MonoBehaviour
	{
		private int itemIndex = -1;

		public int ItemIndex
		{
			get
			{
				return itemIndex;
			}
		}

		virtual public void ItemUpdate(int index)
		{
			itemIndex = index;
		}
	}
}
