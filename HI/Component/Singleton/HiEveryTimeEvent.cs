using HI.Abstract;
using System.Collections.Generic;

namespace HI
{
	public class HiEveryTimeEvent : HiSingletonMono<HiEveryTimeEvent>
	{
		public enum EveryTimeMode
		{
			None,
			Fixed,
			Normal,
			Late,
		}
		public delegate void EveryTimeFunction();

		private bool UseFixedUpdate = false;
		private bool UseUpdate = false;
		private bool UseLateUpdate = false;

		private List<EveryTimeFunction> List_EveryTimeFixedItem = new List<EveryTimeFunction>();
		private List<EveryTimeFunction> List_EveryTimeItem = new List<EveryTimeFunction>();
		private List<EveryTimeFunction> List_EveryTimeLateItem = new List<EveryTimeFunction>();

		void FixedUpdate()
		{
			if (UseFixedUpdate == false)
			{
				return;
			}
		}

		void Update()
		{
			if (UseUpdate == false)
			{
				return;
			}
			foreach (EveryTimeFunction item in List_EveryTimeItem)
			{
				if (UseUpdate == true)
				{
					item();
				}
			}
		}

		void LateUpdate()
		{
			if (UseLateUpdate == false)
			{
				return;
			}
		}
		//==================================

		
		//=> public Function
		public void AddEventUpdate(EveryTimeFunction item, EveryTimeMode etm = EveryTimeMode.Normal)
		{
			switch (etm)
			{
				case EveryTimeMode.Normal:
					List_EveryTimeItem.Add(item);
					break;
				case EveryTimeMode.Fixed:
					List_EveryTimeFixedItem.Add(item);
					break;
				case EveryTimeMode.Late:
					List_EveryTimeLateItem.Add(item);
					break;
			}
			EveryTimeFlagUpdat();
		}
		public void RemoveEventUpdate(EveryTimeFunction item, EveryTimeMode etm = EveryTimeMode.Normal)
		{
			switch (etm)
			{
				case EveryTimeMode.Normal:
					List_EveryTimeItem.Remove(item);
					break;
				case EveryTimeMode.Fixed:
					List_EveryTimeFixedItem.Remove(item);
					break;
				case EveryTimeMode.Late:
					List_EveryTimeLateItem.Remove(item);
					break;
			}
			EveryTimeFlagUpdat();
		}

		//코루틴 --> MonoBehaviour를 상속받았으므로 따로 정의할 필요 없음. 그냥 사용.
		//====================


		private void EveryTimeFlagUpdat()
		{
			if (List_EveryTimeFixedItem.Count > 0)
			{
				UseFixedUpdate = true;
			}
			else
			{
				UseFixedUpdate = false;
			}

			if (List_EveryTimeItem.Count > 0)
			{
				UseUpdate = true;
			}
			else
			{
				UseUpdate = false;
			}

			if (List_EveryTimeLateItem.Count > 0)
			{
				UseLateUpdate = true;
			}
			else
			{
				UseLateUpdate = false;
			}
		}
	}
}
