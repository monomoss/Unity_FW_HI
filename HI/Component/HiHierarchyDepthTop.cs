using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HI
{
	public class HiHierarchyDepthTop : MonoBehaviour
	{
		private Transform myTransform;

		void Start()
		{
			//myTransform = this.GetComponent<Transform>();
			myTransform = this.transform;
			HierarchyTop();
		}

		void LateUpdate()
		{
			HierarchyTop();
		}

		private void HierarchyTop()
		{
			myTransform.SetSiblingIndex(myTransform.parent.childCount+1);
		}
	}
}
