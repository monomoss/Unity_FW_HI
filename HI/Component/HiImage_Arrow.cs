using UnityEngine;

namespace HI
{
	public class HiImage_Arrow : MonoBehaviour
	{
		private int updateCount = 15;
		private Vector3 positionOffset = new Vector3(0.5f, 0, 0);
		private Vector3 offset = Vector3.zero;

		void Update()
		{
			++updateCount;
			if (updateCount >= 15)
			{
				updateCount = 0;
				if (offset == positionOffset)
				{
					offset = positionOffset * -1f;
				}
				else
				{
					offset = positionOffset;
				}
			}
			this.transform.localPosition += offset;
		}
	}
}
