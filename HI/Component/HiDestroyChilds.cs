using UnityEngine;
using HI.ExMethods;

namespace HI
{
	public class HiDestroyChilds : MonoBehaviour
	{
		void Awake()
		{
			Transform temp1 = this.transform;
			Transform temp2 = null;
			for (int i = 0; i < temp1.childCount; i++)
			{
				temp2 = temp1.GetChild(i);
				if (IsExceptionItem(temp2.gameObject) == false)
				{
					temp2.gameObject.DestroyChilds();
					GameObject.Destroy(temp2.gameObject);
				}
			}
		}

		private bool IsExceptionItem(GameObject go)
		{
			if(go.name == "_Always")
			{
				return true;
			}
			return false;
		}
	}
}
