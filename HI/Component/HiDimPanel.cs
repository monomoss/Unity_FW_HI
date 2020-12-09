using UnityEngine;
using UnityEngine.UI;

namespace HI
{
	public class HiDimPanel : MonoBehaviour
	{
		void Awake()
		{
			HiEvent.AddEventListener<DG_Normal>(HiEventID.Dim_Hide, HideDimPanel);
			HiEvent.AddEventListener<DG_Normal>(HiEventID.Dim_Show_0, ShowDimPanel0);
			HiEvent.AddEventListener<DG_Normal>(HiEventID.Dim_Show_5, ShowDimPanel5);
			this.gameObject.SetActive(false);
		}

		private void HideDimPanel()
		{
			if (HiLoading.Instance == null ||
				HiPopup.Instance == null ||
				HiLoading.Instance.gameObject.activeSelf == true ||
				HiPopup.Instance.gameObject.activeSelf == true)
			{
				return;
			}
			this.gameObject.SetActive(false);
		}

		private void ShowDimPanel0()
		{
			this.GetComponent<Image>().color = new Color(0, 0, 0, 0.0f);
			this.gameObject.SetActive(true);
		}

		private void ShowDimPanel5()
		{
			this.GetComponent<Image>().color = new Color(0, 0, 0, 0.7f);
			this.gameObject.SetActive(true);
		}
	}
}
