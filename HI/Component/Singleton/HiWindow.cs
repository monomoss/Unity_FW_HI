using HI;
using HI.Abstract;
using HI.ExMethods;
using UnityEngine;

public class HiWindow : HiSingletonMono<HiWindow>
{
	public GameObject[] PF_Windows;

	private GameObject window;

	override protected void Awake()
	{
		base.Awake();
		//this.gameObject.SetActive(false);

		HiEvent.RemoveEventListener(HiEventID.Window_Show);
		HiEvent.RemoveEventListener(HiEventID.Window_Hide);

		HiEvent.AddEventListener<DG_Normal>(HiEventID.Window_Show, ShowWindowPanel);
		HiEvent.AddEventListener<DG_Normal>(HiEventID.Window_Hide, HideWindowPanel);
	}

	
	/*
	* 아래 빌드관련 함수는 이벤트 인자값 전달을 가능하게 수정한 후에 이벤트 값을 받아서 처리하도록 수정해야함.
	*/
	public void SetWindowID(HiWindowID _id)
	{
		int _index = (int)_id;
		if (_index < PF_Windows.Length)
		{
			window = GameObject.Instantiate(PF_Windows[_index], this.transform);
			//window.transform.parent = this.transform;
			window.transform.SetParent(this.transform);
			window.SetActive(false);
		}
	}


	private void ShowWindowPanel()
	{
		if (window != null)
		{
			window.SetActive(true);
		}
	}

	private void HideWindowPanel()
	{
		if (window != null)
		{
			GameObject.Destroy(window);
		}
		if (this.transform.childCount > 0)
		{
			this.gameObject.DestroyChilds();
		}
	}
}
