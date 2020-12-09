using HI.ExMethods;
using UnityEngine;

namespace HI
{
	public class HiActiveAllChild : MonoBehaviour
	{
		void Awake()
		{
			this.gameObject.SetActiveAllChild(true);
		}
	}
}
