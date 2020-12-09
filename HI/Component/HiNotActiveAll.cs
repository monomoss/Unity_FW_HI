using HI.ExMethods;
using UnityEngine;

namespace HI
{
	public class HiNotActiveAll : MonoBehaviour
	{
		void Awake()
		{
			this.gameObject.SetActiveAllChild(false);
			this.gameObject.SetActive(false);
		}
	}
}
