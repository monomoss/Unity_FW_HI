using UnityEngine;
using UnityEngine.UI;

namespace HI
{
	public class HiText_Sequential : MonoBehaviour
	{
		public Text TXT_textField = null;
		public string loadingStr;

		private int lengthCount = 0;
		private int updateCount = 0;

		void OnEnable()
		{
			lengthCount = 0;
			updateCount = 0;
			TXT_textField.text = loadingStr;
		}

		void Update()
		{
			if(Time.timeScale == 0.0f)
			{
				return;
			}

			++updateCount;
			if (updateCount == 10)
			{
				++lengthCount;
				updateCount = 0;

				if (lengthCount > loadingStr.Length)
				{
					lengthCount = 0;
				}
				TXT_textField.text = loadingStr.Substring(0, lengthCount);
			}
		}
	}
}
