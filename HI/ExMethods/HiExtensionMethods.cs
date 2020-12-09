using HI.Utility;
using UnityEngine;

namespace HI.ExMethods
{
	public static class HiExtensionMethods
	{
		//GameObject
		public static void DestroyChilds(this GameObject target)
		{
			Transform temp1 = target.transform;
			Transform temp2 = null;
			for (int i = 0; i < temp1.childCount; i++)
			{
				temp2 = temp1.GetChild(i);
				GameObject.Destroy(temp2.gameObject);
			}
		}
		public static void SetActiveChilds(this GameObject target, bool value)
		{
			Transform temp1 = target.transform;
			Transform temp2 = null;

			//target.gameObject.SetActive(value);
			for (int i = 0; i < temp1.childCount; i++)
			{
				temp2 = temp1.GetChild(i);
				if (IsExceptNameCheck(temp2.gameObject))
				{
					continue;
				}
				temp2.gameObject.SetActive(value);
			}
		}
		public static void SetActiveAllChild(this GameObject target, bool value)
		{
			Transform temp1 = target.transform;
			Transform temp2 = null;

			//target.gameObject.SetActive(value);
			for (int i = 0; i < temp1.childCount; i++)
			{
				temp2 = temp1.GetChild(i);
				if (IsExceptNameCheck(temp2.gameObject))
				{
					continue;
				}
				temp2.gameObject.SetActive(value);
				if (temp2.childCount > 0)
				{
					SetActiveAllChild(temp2.gameObject, value);
				}
			}
		}


		//RectTransform
		public static void SetRectSize(this RectTransform target, float width, float height)
		{
			target.sizeDelta = new Vector2(width, height);
		}


		//Vector3
		//=============
		
		
		
		private static  bool IsExceptNameCheck(GameObject go)
		{
			if (go.name.Length >= 3 && go.name.Substring(0, 3) == "___")
			{
				return true;
			}
			return false;
		}
	}
}
