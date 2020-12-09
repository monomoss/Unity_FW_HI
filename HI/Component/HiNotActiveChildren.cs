using HI.ExMethods;
using UnityEngine;

namespace HI
{
	public class HiNotActiveChildren : MonoBehaviour
	{
		void Awake()
		{
			this.gameObject.SetActiveAllChild(false);
		}
	}
}
