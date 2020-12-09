using UnityEngine;
using UnityEngine.UI;

namespace HI
{
	public class HiImage_Color : MonoBehaviour
	{
		private Color colorRed = new Color(1f, 0.5f, 0.5f);
		private Image targetImage;
		private int updateCount = 0;

		void Awake()
		{
			updateCount = 200;
			targetImage = this.GetComponent<Image>();
			HiEvent.AddEventListener<DG_Normal>(HiEventID.CubeSelectFailure, OnCubeSelectFailure);
		}

		private void OnCubeSelectFailure()
		{
			updateCount = 0;
		}

		private void ChangeColor()
		{
			if (targetImage.color == Color.white)
			{
				targetImage.color = colorRed;
			}
			else
			{
				targetImage.color = Color.white;
			}
		}

		void Update()
		{
			if (updateCount > 100)
			{
				return;
			}

			++updateCount;
			if (updateCount >= 80)
			{
				targetImage.color = Color.white;
			}
			else
			{
				if (updateCount % 5 == 0)
				{
					ChangeColor();
				}
			}
		}
	}
}
