using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace HI
{
	public class HiDigitalClock : MonoBehaviour
	{
		public Text TXT_Label;

		private Stopwatch stopwatch = null;
		private string stopwatchStr;
		private bool isRunning = false;


		void Awake()
		{
			HiEvent.AddEventListener<DG_Normal>(HiEventID.GameClock_Start, ClockStart);
			HiEvent.AddEventListener<DG_Normal>(HiEventID.GameClock_Stop, ClockStop);
			stopwatch = new Stopwatch();
			//ClockStop();
		}

		void OnDestroy()
		{
			HiEvent.RemoveEventListener(HiEventID.GameClock_Start);
			HiEvent.RemoveEventListener(HiEventID.GameClock_Stop);
			stopwatch.Stop();
			stopwatch = null;
		}

		void Update()
		{
			if (Time.timeScale == 0)
			{
				stopwatch.Stop();
				return;
			}
			if (isRunning == true)
			{
				ClockStart();
				stopwatchStr = stopwatch.Elapsed.ToString();
				if (stopwatchStr.Length > 10)
				{
					TXT_Label.text = stopwatchStr.Substring(0, 10);
				}
				else
				{
					TXT_Label.text = stopwatchStr;
				}
			}
		}

		private void ClockReset()
		{
			if (stopwatch != null)
			{
				stopwatch.Reset();
			}
		}

		private void ClockStart()
		{
			if (stopwatch != null &&
				stopwatch.IsRunning == false)
			{
				isRunning = true;
				stopwatch.Start();
			}
		}

		private void ClockStop()
		{
			if (stopwatch != null &&
				stopwatch.IsRunning == true)
			{
				isRunning = false;
				stopwatch.Stop();
				S_ContentsGameInfo.Instance.GamePlayTime = stopwatch.ElapsedMilliseconds;
			}
		}
	}
}
