using HI.ExMethods;
using UnityEngine;

namespace HI
{
	public class HiDisableChilds : MonoBehaviour
	{
		void Awake()
		{
			this.gameObject.SetActiveChilds(false);
		}
	}
}
