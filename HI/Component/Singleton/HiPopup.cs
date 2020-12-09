using UnityEngine;
using HI;
using UnityEngine.UI;
using HI.Abstract;
using System;

namespace HI
{
	public class HiPopup : HiSingletonMono<HiPopup>
	{
		public Text MessageText;
		public Button Button1_1;
		public Button Button2_1;
		public Button Button2_2;

		private Text[] button1Texts = new Text[1];
		private Text[] button2Texts = new Text[2];
		//private DG_Normal CallbackFunction1;
		//private DG_Normal CallbackFunction2;

		override protected void Awake()
		{
			base.Awake();
			HiEvent.AddEventListener<DG_Normal>(HiEventID.Popup_Hide, HidePopupPanel);
			HiEvent.AddEventListener<DG_Normal>(HiEventID.Popup_Show, ShowPopupPanel);
			this.gameObject.SetActive(false);

			button1Texts[0] = Button1_1.transform.GetChild(0).GetComponent<Text>();
			button2Texts[0] = Button2_1.transform.GetChild(0).GetComponent<Text>();
			button2Texts[1] = Button2_2.transform.GetChild(0).GetComponent<Text>();
		}

		void OnEnable()
		{
			HiEvent.EventDispatch(HiEventID.Loading_Hide);
		}

		void OnDisable()
		{
			HiEvent.EventDispatch(HiEventID.Dim_Hide);
		}

		void Update()
		{
			HiEvent.EventDispatch(HiEventID.Dim_Show_5);
		}


		/*
		 * 아래 빌드관련 함수는 이벤트 인자값 전달을 가능하게 수정한 후에 이벤트 값을 받아서 처리하도록 수정해야함.
		 */
		//버튼 하나 팝업 만들기
		public void BuildPopup(string _mainMsg, string _buttonTxt, Action _callback = null)
		{
			if (_callback == null)
			{
				_callback = delegate { HiEvent.EventDispatch(HiEventID.Popup_Hide); };
			}
			MessageText.fontSize = 20;
			ButtonInit();
			MessageText.text = _mainMsg;
			button1Texts[0].text = _buttonTxt;
			//CallbackFunction1 = fun;
			Button1_1.gameObject.SetActive(true);
			Button1_1.onClick.AddListener(() => _callback());
		}

		//버튼 둘 팝업 만들기
		public void BuildPopup(string _mainMsg, string _buttonTxt1, string _buttonTxt2, Action<bool> _callback = null)
		{
			if (_callback == null)
			{
				_callback = delegate { HiEvent.EventDispatch(HiEventID.Popup_Hide); };
			}
			MessageText.fontSize = 20;
			ButtonInit();
			MessageText.text = _mainMsg;
			button2Texts[0].text = _buttonTxt1;
			button2Texts[1].text = _buttonTxt2;
			//CallbackFunction1 = fun;
			Button2_1.gameObject.SetActive(true);
			Button2_2.gameObject.SetActive(true);
			Button2_1.onClick.AddListener(() => _callback(false));
			Button2_2.onClick.AddListener(() => _callback(true));
		}

		public void FontSizeSetting(int _fontSize = 20)
		{
			MessageText.fontSize = _fontSize;
		}


		private void ShowPopupPanel()
		{
			if (Time.timeScale == 0.0f)
			{
				return;
			}
			this.gameObject.SetActive(true);
			//HiEvent.EventDispatch(HiEventID.Loading_Hide);
			//HiEvent.EventDispatch(HiEventID.Dim_Show);
		}

		private void HidePopupPanel()
		{
			if(Time.timeScale == 0.0f)
			{
				return;
			}
			HiSound.Instance.PlayAudio_Sound(HiSound.SOUND.Click);
			this.gameObject.SetActive(false);
			//HiEvent.EventDispatch(HiEventID.Loading_Show);
			//HiEvent.EventDispatch(HiEventID.Dim_Hide);
		}

		private void ButtonInit()
		{
			Button1_1.onClick.RemoveAllListeners();
			Button2_1.onClick.RemoveAllListeners();
			Button2_2.onClick.RemoveAllListeners();
			Button1_1.gameObject.SetActive(false);
			Button2_1.gameObject.SetActive(false);
			Button2_2.gameObject.SetActive(false);
		}
	}
}
