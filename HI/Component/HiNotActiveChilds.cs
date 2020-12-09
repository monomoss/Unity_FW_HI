using HI.ExMethods;
using UnityEngine;

namespace HI
{
	public class HiNotActiveChilds : MonoBehaviour
	{
		void Awake()
		{
			this.gameObject.SetActiveChilds(false);
		}
	}
}
