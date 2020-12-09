using UnityEngine;
using HI.Utility;
using System.Collections.Generic;
using HI.Abstract;
using UnityEngine.EventSystems;
using System.Collections;

namespace HI
{
	public class HiMain : HiSingletonMono<HiMain>
	{
		public GameObject Main = null;
		public HiHashtable ObjectLocker = new HiHashtable();

		private List<Component> Components = new List<Component>();


		override protected void Awake()
		{
			base.Awake();
			DisabledAllGameObject();
			AppendComponents();
			this.gameObject.SetActive(true);
			SetDragThreshold();
		}
		
		//
		new public T GetComponent<T>() where T : Component
		{
			T target = null;
			for (int i = 0; i < Components.Count; i++)
			{
				target = Components[i] as T;
				if (target != null)
				{
					return target;
				}
			}
			return null;
		}


		//공용 싱글 컨퍼넌트 등록
		private void AppendComponents()
		{
			Components.Add(HiUtile.Instance.GetComponentEx<HiTouchInput>(this.gameObject));
			Components.Add(HiUtile.Instance.GetComponentEx<HiCameraZoom>(this.gameObject));
			Components.Add(HiUtile.Instance.GetComponentEx<HiEveryTimeEvent>(this.gameObject));
		}


		//모든 게임오브젝트 비활성화
		private void DisabledAllGameObject()
		{
			bool value = false;
			GameObject go = null;
			GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();
			for (int i = 0; i < temp.Length; i++)
			{
				go = temp[i];
				go.SetActive(value);
				if (go.name.Length >= 3 && go.name.Substring(0, 3) == "___")
				{
					GameObject.Destroy(go);
				}
			}
		}


		//[SerializeField]
		private EventSystem eventSystem = null;
		//[SerializeField]
		private float dragThresholdCM = 0.5f;
		//
		private const float inchToCm = 2.54f;
		private void SetDragThreshold()
		{
			if (eventSystem != null)
			{
				eventSystem.pixelDragThreshold = (int)(dragThresholdCM * Screen.dpi / inchToCm);
			}
		}
	}
}
