using HI.Abstract;
using UnityEngine;

namespace HI
{
	public class HiObjectPath : HiSingleton<HiObjectPath>
	{
		private GameObject gameView = null;
		public GameObject GetGameView
		{
			get
			{

				return gameView;
			}
			set
			{
				gameView = value;
			}
		}
	}
}
