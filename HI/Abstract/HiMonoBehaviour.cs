using UnityEngine;
using System.Reflection;
using HI.ExMethods;
using HI;

namespace HI.Abstract
{
	public abstract class HiMonoBehaviour : MonoBehaviour
	{
		protected bool IsActivated = false;

		public HiMonoBehaviour()
		{
			MethodInfo _info1 = this.GetType().GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			MethodInfo _info2 = this.GetType().GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			if (_info1 != null || _info2 != null)
			{
				throw new System.Exception("Error ▶ HiMonoBehaviour > Method Restrictions. (Awake,Start is not available)");
			}

			//if (this.GetType().GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) != null &&
			//	this.GetType().GetMethod("OnEnable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) != null)
			//{
			//	throw new System.Exception("Error ▶ Method Restrictions : OnEnable, Start can not be used together.");
			//}

			//if (this as _Main == null)
			//if (this.GetType().ToString() != "_Main")
		}

		virtual protected void OnFirstEnable()
		{
			IsActivated = true;
		}

		virtual protected void OnEnable()
		{
			if (IsActivated == false)
			{
				OnFirstEnable();
			}
		}

		


		//public T GetComponent<T>()
		//{
		//	T t = base.GetComponent<T>();
		//	return t;
		//}

		//protected void SetActiveWithAllChild(GameObject target, bool isActive)
		//{
		//	target.SetActiveAllChild(isActive);
		//	target.SetActive(isActive);
		//}

		//protected void SetActiveWithChilds(GameObject target, bool isActive)
		//{
		//	target.SetActiveChilds(isActive);
		//	target.SetActive(isActive);
		//}
	}
}
