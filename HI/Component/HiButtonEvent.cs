using UnityEngine;
using UnityEngine.UI;

namespace HI
{
	public class HiButtonEvent : Button
	{
		public int EventIndex = -1;

		protected override void Awake()
		{
			Button _btn = this.GetComponent<Button>();
			if(_btn != null)
			{
				_btn.onClick.AddListener(delegate { OnClick(); });
			}
		}

		private void OnClick()
		{
			HiSound.Instance.PlayAudio_Sound(HiSound.SOUND.Click);
			int _index = EventIndex;
			if (_index >= 0 && _index < (int)HiEventBtnID.Max)
			{
				HiEvent.EventDispatch((HiEventBtnID)_index);
			}
			else
			{
				throw new System.Exception("Error ▶ HiButtonEvent > Unregistered button events. > index=" + _index);
			}
		}
	}
}
