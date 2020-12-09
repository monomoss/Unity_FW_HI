using UnityEngine;
using UnityEngine.UI;
using HI.Abstract;

namespace HI
{
	public class HiCameraZoom : HiSingletonMono<HiCameraZoom>
	{
		private enum EnTouchActionState
		{
			None,
			Down,
			Zoom
		}

		private EnTouchActionState cpTouchState = EnTouchActionState.None;
		private readonly int cpTouchBoundary = 20;

		private HiTouchInput cpInput = null;
		private Vector2 cpTouch1Position = Vector2.zero;
		private Vector2 cpTouch2Position = Vector2.zero;
		private float cpCameraFieldOfView = 0f;

		public int ZoomMinNum = 135;
		public int ZoomMaxNum = 80;

		private int cpTouchCount = 0;
		//private float cpInitTouchDist = 0f;

		private bool cpTouchBlock = false;
		private Scrollbar cpScrollbar = null;

#if Symbol_Html5
		private readonly float cpResistivity = 5.0f;
#else
		private readonly float cpResistivity = 10.0f;
#endif


		void Start()
		{
			cpInput = HiTouchInput.Instance;
			//TouchTypeSetting();

			//SetFieldOfView();
			//HiCameraZoom.Instance.SetZoom(130, 90, 1.5f);
		}

		void Update()
		{
			TouchProcess();
		}


		//public void SetZoom(int min, int max)
		//{
		//	cpZoomMin = min;
		//	cpZoomMax = max;
		//}

		public void SetCameraSize(float _ratio = 0.25f)
		{
			float value = ZoomMinNum + ((ZoomMaxNum - ZoomMinNum) * _ratio);
			if (Camera.main != null)
			{
				Camera.main.fieldOfView = value;
			}
		}

		public bool GSetTouchBlock
		{
			get
			{
				return cpTouchBlock;
			}
			set
			{
				cpTouchBlock = value;
			}
		}

		public void SetScrollbar(Scrollbar _scrollbar)
		{
			if(cpScrollbar != null)
			{
				cpScrollbar.onValueChanged.RemoveAllListeners();
			}
			if(_scrollbar != null)
			{
				cpScrollbar = _scrollbar;
				cpScrollbar.onValueChanged.AddListener(delegate { OnValueChanged(); });
			}
		}



		private void OnValueChanged()
		{
			Camera.main.fieldOfView = ZoomMinNum + ((ZoomMaxNum - ZoomMinNum) *cpScrollbar.value);
		}

		private void TouchProcess()
		{
			cpTouchCount = cpInput.GetTouchCount();

			if (HiTouchInput.Instance.GetInputType == HiTouchInput.EnInputType.Single)
			{
				if (cpTouchBlock == true)
				{
					return;
				}
				if (cpTouchCount <= 0)
				{
					cpTouchState = EnTouchActionState.None;
					return;
				}
				if (cpTouchCount == 1)
				{
					TouchProcessSingle();
				}
				else
				{
					TouchProcessMulti();
				}
			}
			else
			{
				if (cpTouchCount <= 0)
				{
					cpTouchState = EnTouchActionState.None;
					return;
				}
				if (cpTouchCount == 1 && cpTouchBlock == false)
				{
					TouchProcessSingle();
				}
				if (cpTouchCount >= 2)
				{
					TouchProcessMulti();
				}
			}
		}

		private void TouchProcessSingle()
		{
			if (cpInput.GetTouchCount() != cpTouchCount)
			{
				cpTouchState = EnTouchActionState.None;
				return;
			}
			
			if (cpTouchState == EnTouchActionState.None)
			{
				cpTouch1Position = cpInput.GetTouch(0).position;
				cpTouchState = EnTouchActionState.Down;
			}
			if (cpTouchState == EnTouchActionState.Down)
			{
				cpTouch2Position = cpInput.GetTouch(0).position;
				if (Vector2.Distance(cpTouch1Position, cpTouch2Position) > cpTouchBoundary)
				{
					cpCameraFieldOfView = Camera.main.fieldOfView;
					cpTouch1Position = cpTouch2Position = cpInput.GetTouch(0).position;
					cpTouchState = EnTouchActionState.Zoom;
				}
			}
			if (cpTouchState == EnTouchActionState.Zoom)
			{
				/*
				cpTouch1Position = cpInput.GetTouch(0).position;
				cpTouch2Position = Vector2.zero;
				if (cpInput.GetTouchCount() == cpTouchCount)
				{
					if (Vector2.Distance(cpTouch1Position, cpTouch2Position) > cpInitTouchDist + 1)
					{
						ZoomIN();
					}
					else if (Vector2.Distance(cpTouch1Position, cpTouch2Position) < cpInitTouchDist - 1)
					{
						ZoomOUT();
					}
					cpInitTouchDist = Vector2.Distance(cpTouch1Position, cpTouch2Position);
				}
				*/

				/*
				cpTouch1Position = cpInput.GetTouch(0).position;
				cpTouch2Position = Vector2.zero;
				if (cpInput.GetTouchCount() == cpTouchCount)
				{
					if (Mathf.Abs(cpTouch1Position.y - cpTouch2Position.y) > cpInitTouchDist + 1)
					{
						ZoomIN();
					}
					else if (Mathf.Abs(cpTouch1Position.y - cpTouch2Position.y) < cpInitTouchDist - 1)
					{
						ZoomOUT();
					}
					cpInitTouchDist = Mathf.Abs(cpTouch1Position.y - cpTouch2Position.y);
				}
				*/

				cpTouch2Position = cpInput.GetTouch(0).position;
				float _distance = (cpTouch1Position.y - cpTouch2Position.y) / cpResistivity;
				if (cpInput.GetTouchCount() == cpTouchCount)
				{
					//if (cpTouch1Position.y < cpTouch2Position.y)
					//{
					//	ZoomIN(cpCameraFieldOfView, _distance);
					//}
					//else if (cpTouch1Position.y > cpTouch2Position.y)
					//{
					//	ZoomOUT(cpCameraFieldOfView, _distance);
					//}

					SetCameraZoom(cpCameraFieldOfView, _distance);
				}
			}
		}

		private void TouchProcessMulti()
		{
			if (cpInput.GetTouchCount() != cpTouchCount)
			{
				cpTouchState = EnTouchActionState.None;
				return;
			}
			
			if (cpTouchState == EnTouchActionState.None)
			{
				cpTouch1Position = cpInput.GetTouch(0).position;
				cpTouchState = EnTouchActionState.Down;
			}
			if (cpTouchState == EnTouchActionState.Down)
			{
				cpTouch2Position = cpInput.GetTouch(0).position;
				if (Vector2.Distance(cpTouch1Position, cpTouch2Position) > cpTouchBoundary)
				{
					cpTouchState = EnTouchActionState.Zoom;
				}
			}
			if (cpTouchState == EnTouchActionState.Zoom)
			{
				cpTouch1Position = cpInput.GetTouch(0).position;
				cpTouch2Position = cpInput.GetTouch(1).position;
				if (cpInput.GetTouchCount() == cpTouchCount)
				{
					//if (Vector2.Distance(cpTouch1Position, cpTouch2Position) > cpInitTouchDist + 1)
					//{
					//	ZoomIN();
					//}
					//else if (Vector2.Distance(cpTouch1Position, cpTouch2Position) < cpInitTouchDist - 1)
					//{
					//	ZoomOUT();
					//}
					//cpInitTouchDist = Vector2.Distance(cpTouch1Position, cpTouch2Position);
				}
			}
		}

		private void SetCameraZoom(float _y0 = 0, float _yGap = 0)
		{
			Camera.main.fieldOfView = _y0 + _yGap;

			if (Camera.main.fieldOfView <= ZoomMaxNum)
			{
				Camera.main.fieldOfView = ZoomMaxNum;
			}
			else if (Camera.main.fieldOfView >= ZoomMinNum)
			{
				Camera.main.fieldOfView = ZoomMinNum;
			}
		}

		/*
		private void TouchTypeSetting()
		{
			switch (HiTouchInput.Instance.GetInputType)
			{
				case HiTouchInput.EnInputType.Single:
					cpTouchCount = 1;
					break;
				case HiTouchInput.EnInputType.Multi:
					cpTouchCount = 2;
					break;
			}
		}
		*/
	}
}
