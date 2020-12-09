using HI.Abstract;
using UnityEngine.UI;

namespace HI
{
	public class HiLoading : HiSingletonMono<HiLoading>
	{
		public Text loadingMessage;

		//override protected void OnFirstEnable()
		//{
		//	base.OnFirstEnable();
		//	HiEvent.AddEventListener<DG_Objects>(HiEventID.Loading_Hide.ToString(), HideLoading);
		//	HiEvent.AddEventListener<DG_Objects>(HiEventID.Loading_Show.ToString(), ShowLoading);
		//}

		override protected void Awake()
		{
			base.Awake();
			HiEvent.AddEventListener<DG_Objects>(HiEventID.Loading_Hide, HideLoading);
			HiEvent.AddEventListener<DG_Objects>(HiEventID.Loading_Show, ShowLoading);
			this.gameObject.SetActive(false);
		}

		//void OnEnable()
		//{
		//	SetMessage(HiMessage.Instance.GetMessage_AdsOnLoad());
		//}

		void OnDisable()
		{
			HiEvent.EventDispatch(HiEventID.Dim_Hide);
		}

		void Update()
		{
			HiEvent.EventDispatch(HiEventID.Dim_Show_5);
		}
		
		public void SetMessage(string msg)
		{
			if (loadingMessage != null)
			{
				loadingMessage.text = msg;
			}
		}

		private void ShowLoading(params object[] args)
		{
			this.gameObject.SetActive(true);
			//HiEvent.EventDispatch(HiEventID.Dim_Show);
		}

		private void HideLoading(params object[] args)
		{
			this.gameObject.SetActive(false);
			//HiEvent.EventDispatch(HiEventID.Dim_Hide);
		}
	}
}
