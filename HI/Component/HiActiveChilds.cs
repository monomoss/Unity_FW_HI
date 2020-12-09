using HI.ExMethods;
using UnityEngine;

namespace HI
{
	public class HiActiveChilds : MonoBehaviour
	{
		void Awake()
		{
			this.gameObject.SetActiveChilds(true);
		}
	}
}
