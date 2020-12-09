using HI.Abstract;
using UnityEngine;

namespace HI.Utility
{
	public class HiColor : HiSingleton<HiColor>
	{
		private Color color_basic = Color.white;
		private Color color_disabled = new Color(0.6f, 0.6f, 0.6f, 0.3f);


		public Color GetBasicColor()
		{
			return color_basic;
		}

		public Color GetDisabledColor()
		{
			return color_disabled;
		}
	}
}
