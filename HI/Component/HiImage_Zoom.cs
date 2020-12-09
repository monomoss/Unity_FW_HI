using UnityEngine;

namespace HI
{
	public class HiImage_Zoom : MonoBehaviour
	{
		private int updateCount = 10;
		private Vector3 scaleOffset = new Vector3(0.01f, 0.02f, 0);
		private Vector3 offset = Vector3.zero;

		void Update()
		{
			++updateCount;
			if (updateCount >= 10)
			{
				updateCount = 0;
				if (offset == scaleOffset)
				{
					offset = scaleOffset * -1f;
				}
				else
				{
					offset = scaleOffset;
				}
			}
			this.transform.localScale += offset;
		}
	}
}
