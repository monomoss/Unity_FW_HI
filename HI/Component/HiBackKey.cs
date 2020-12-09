using UnityEngine;

namespace HI
{
	public class HiBackKey : MonoBehaviour
	{
		void Update()
		{
			if (Application.platform == RuntimePlatform.WindowsEditor ||
				Application.platform == RuntimePlatform.OSXEditor ||
				Application.platform == RuntimePlatform.Android)
			{
				if (Input.GetKey(KeyCode.Escape))
				{

				}
			}
		}
	}
}
