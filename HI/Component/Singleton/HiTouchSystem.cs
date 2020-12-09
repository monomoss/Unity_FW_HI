using HI.Abstract;
using HI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 컨텐츠에서 필요로 하는 모든 터치 정보를 해당 클래스에서 처리한다.
/// UNITY_EDITOR => 마우스 2개버튼으로 처리.
/// UNITY_IOS || UNITY_ANDROID => 터치,롱터치,2포인트 터치 처리.
/// </summary>
namespace HI
{
	public class HiTouchSystem : HiSingletonMono<HiTouchSystem>
	{
		public Text DebugText = null;


		public readonly float TouchAreaSize = 50;               //터치 영역 범위
		public readonly long LongTouchCheckTime = 1000 * 3;     //롱터치 체크 시간
		public enum TouchArea
		{
			None = -1,
			PhysicalArea,
			CanvasArea,
			Max
		}
		public enum TouchState
		{
			None = -2,
			TouchAndWait = -1,
			Touch,                  //터치
			TouchUp,                //터치업
			Touch_Long,             //롱터치
			TouchUp_Release,        //클릭
			TouchDrag_1Point,       //드래그1
			TouchDrag_2Point,       //드래그2
			Max
		}


		private HiUtile HiUtile = HiUtile.Instance;
		//private object[] ObservableList = new object[(int)TouchState.Max];
		private List<Action<TouchState, TouchArea, Touch[]>>[] ReceivingSetList = new List<Action<TouchState, TouchArea, Touch[]>>[(int)TouchState.Max];

		private bool IsTouch = false;
		private long LongTouchTime = 0;
		private TouchState State = TouchState.None;
		private TouchArea StartArea = TouchArea.None;
		private TouchArea CurrentArea = TouchArea.None;
		private Touch[] StartTouchs = null;
		private Touch[] CurrentTouchs = null;


		void Start()
		{
			//HiUtile = HiUtile.Instance;

			TouchDownProcess();
			TouchUpProcess();
			TouchDownProcess_2Point();

			//AddTouchEvent = Test_Touch;
			//AddTouchUpEvent = Test_Touch;
			//AddTouchLongEvent = Test_Touch;
			//AddTouchReleaseEvent = Test_Touch;
			//AddTouch1pointEvent = Test_Touch;
			//AddTouch2pointEvent = Test_Touch;

			//TouchUp(TouchState.TouchUp, null);
			//this.gameObject.AddComponent<ObservableUpdateTrigger>(gameObject).UpdateAsObservable();
		}


		//터치 로그. (확인용)
		private void Test_Touch(TouchState _state, TouchArea _area, Touch[] _touch)
		{
			if (DebugText.text.Length > 800)
			{
				DebugText.text = "";
			}

			////HiDebug.Log("+++++++++++++++++++++++++++++++++++");
			//GameObject _go = TouchGameObject(_touch[0].position);
			//if (_go != null)
			//{
			//	HiDebug.Log("터치 ::: ", "영역=", _area, "상태=" + _state, "Length=" + _touch.Length, "x=", _touch[0].position.x, "y=", _touch[0].position.y, _go.GetComponent<Block>().name);
			//	DebugText.text += "터치 ::: " + ",영역=" + _area + ",상태=" + _state + ",Length=" + _touch.Length + ",x=" + _touch[0].position.x + ",y=" + _touch[0].position.y + _go.GetComponent<Block>().name + "\n";
			//}
			//else
			//{
			//	HiDebug.Log("터치 ::: ", "영역=", _area, "상태=" + _state, "Length=" + _touch.Length, "x=", _touch[0].position.x, "y=", _touch[0].position.y, "null");
			//	DebugText.text += "터치 ::: " + ",영역=" + _area + ",상태=" + _state + ",Length=" + _touch.Length + ",x=" + _touch[0].position.x + ",y=" + _touch[0].position.y + "null" + "\n";
			//}
			////HiDebug.Log("+++++++++++++++++++++++++++++++++++");
		}


		void Update()
		{
			if (StartArea == TouchArea.CanvasArea)
			{
				return;
			}

			if (IsTouch == true)
			{
				CurrentTouchs = GetTouchs(TouchState.None) ?? CurrentTouchs;
				CurrentArea = GetTouchArea(CurrentTouchs[0]);
				//
				if (State == TouchState.None)
				{
					State = TouchState.Touch;
					SendTouchEvent(State, CurrentArea, CurrentTouchs);
					LongTouchTime = HiUtile.GetPlayTime() + LongTouchCheckTime;
				}
				//
				if (State == TouchState.Touch)
				{
					if (StartTouchs.Length >= 2 || CurrentTouchs.Length >= 2)
					{
						if (Math.Abs(StartTouchs[0].position.x - CurrentTouchs[0].position.x) >= TouchAreaSize ||
							Math.Abs(StartTouchs[0].position.y - CurrentTouchs[0].position.y) >= TouchAreaSize)
						{
							State = TouchState.TouchDrag_2Point;
						}
					}
					else
					{
						if (Math.Abs(StartTouchs[0].position.x - CurrentTouchs[0].position.x) >= TouchAreaSize ||
							Math.Abs(StartTouchs[0].position.y - CurrentTouchs[0].position.y) >= TouchAreaSize)
						{
							State = TouchState.TouchDrag_1Point;
						}

						else if (LongTouchTime < HiUtile.GetPlayTime())
						{
							State = TouchState.Touch_Long;
						}
					}
				}
				//
				CurrentTouchs = GetTouchs(State) ?? CurrentTouchs;
				if (State == TouchState.TouchDrag_1Point)
				{
					SendTouchEvent(State, CurrentArea, CurrentTouchs);
				}
				else if (State == TouchState.TouchDrag_2Point)
				{
					SendTouchEvent(State, CurrentArea, CurrentTouchs);
				}
				else if (State == TouchState.Touch_Long)
				{
					SendTouchEvent(State, CurrentArea, CurrentTouchs);
					State = TouchState.TouchAndWait;
				}
			}
			else
			{
				if (State != TouchState.None)
				{
					if (StartTouchs != null && CurrentTouchs != null)
					{
						if (Math.Abs(StartTouchs[0].position.x - CurrentTouchs[0].position.x) < TouchAreaSize &&
							Math.Abs(StartTouchs[0].position.y - CurrentTouchs[0].position.y) < TouchAreaSize)
						{
							State = TouchState.TouchUp_Release;
							SendTouchEvent(State, CurrentArea, CurrentTouchs);
						}
						State = TouchState.TouchUp;
						SendTouchEvent(State, CurrentArea, CurrentTouchs);
					}
				}
				State = TouchState.None;
			}
		}


		//터치 좌표 정보
		public static Touch[][] GetTouchDatas()
		{
			if (HiTouchSystem.Instance == null)
			{
				return null;
			}
			return HiTouchSystem.Instance.TouchDatas();
		}
		//터치 좌표 게임오브젝트
		public static GameObject GetTouchGameObject()
		{
			if (HiTouchSystem.Instance == null)
			{
				return null;
			}
			return HiTouchSystem.Instance.TouchGameObject(HiTouchSystem.Instance.CurrentTouchs[0].position);
		}

		//터치
		public Action<TouchState, TouchArea, Touch[]> AddTouchEvent
		{
			set
			{
				AddReceiver(TouchState.Touch, value);
				//TouchState _s = TouchState.Touch;
				//ReceivingSetList[(int)_s].Add(value);
			}
		}
		//터치업
		public Action<TouchState, TouchArea, Touch[]> AddTouchUpEvent
		{
			set
			{
				AddReceiver(TouchState.TouchUp, value);
			}
		}
		//클릭
		public Action<TouchState, TouchArea, Touch[]> AddTouchReleaseEvent
		{
			set
			{
				AddReceiver(TouchState.TouchUp_Release, value);
			}
		}
		//롱터치
		public Action<TouchState, TouchArea, Touch[]> AddTouchLongEvent
		{
			set
			{
				AddReceiver(TouchState.Touch_Long, value);
			}
		}
		//1포인트 드래그
		public Action<TouchState, TouchArea, Touch[]> AddTouch1pointEvent
		{
			set
			{
				AddReceiver(TouchState.TouchDrag_1Point, value);
			}
		}
		//2포인트 드래그
		public Action<TouchState, TouchArea, Touch[]> AddTouch2pointEvent
		{
			set
			{
				AddReceiver(TouchState.TouchDrag_2Point, value);
			}
		}


		private Touch[][] TouchDatas()
		{
			Touch[][] _ts = new Touch[][] { StartTouchs, CurrentTouchs };
			return _ts;
		}

		private GameObject TouchGameObject(Vector2 _position)
		{
			GameObject _target = null;
			//
			PointerEventData _ped = new PointerEventData(EventSystem.current);
			_ped.position = _position;
			List<RaycastResult> _results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(_ped, _results);
			if (_results.Count > 0)
			{
				_results.Clear();
				return _target;
			}
			//
			//if (StartArea == TouchArea.CanvasArea)
			//{
			//	return _target;
			//}
			//
			int _distance = 10;
			Ray _ray = default(Ray);
			_ray = Camera.main.ScreenPointToRay(_position);
			RaycastHit2D _hit2D = Physics2D.Raycast(_ray.origin, _ray.direction * _distance);
			if (_hit2D == true)
			{
				_target = _hit2D.collider.gameObject;
			}
			return _target;
		}
		
		private void AddReceiver(TouchState _state, Action<TouchState, TouchArea, Touch[]> _act)
		{
			int _index = (int)_state;
			ReceivingSetList[_index] = ReceivingSetList[_index] ?? new List<Action<TouchState, TouchArea, Touch[]>>();
			ReceivingSetList[_index].Add(_act);
			//(TouchState.Touch, value);
			//ReceivingSetList = new List<Action<TouchState, Touch[]>>[(int)TouchState.Max];
		}

		private void TouchDownProcess()
		{
			var _ob = this.UpdateAsObservable()
				.Select(_ => GetTouchCount(TouchState.TouchDrag_1Point))
				.Buffer(2, 1)
				.Where(_ => _.Last() == 1 && _.First() != _.Last())
				.First()
				.Repeat()
				.Subscribe(_ =>
				{
					CurrentTouchs = StartTouchs = GetTouchs(TouchState.Touch);
					CurrentArea = StartArea = GetTouchArea(StartTouchs[0]);
					IsTouch = true;
					//HiDebug.Log("TouchDownProcess=>StartTouchs => ", StartTouchs);
				});
		}

		private void TouchDownProcess_2Point()
		{
			var _ob = this.UpdateAsObservable()
				.Select(_ => GetTouchCount(TouchState.TouchDrag_2Point))
				.Buffer(2, 1)
				.Where(_ => _.Last() >= 2 && _.First() != _.Last())
				.First()
				.Repeat()
				.Subscribe(_ =>
				{
					CurrentTouchs = StartTouchs = GetTouchs(TouchState.TouchDrag_2Point);
					CurrentArea = StartArea = GetTouchArea(StartTouchs[0]);
					IsTouch = true;
					//HiDebug.Log("TouchDownProcess_2Point=>StartTouchs => ", StartTouchs);
				});
		}

		private void TouchUpProcess()
		{
			var _ob = this.UpdateAsObservable()
				.Select(_ => GetTouchCount(TouchState.TouchUp))
				.Buffer(2, 1)
				.Where(_ => _.Last() == 0 && _.First() != _.Last())
				.First()
				.Repeat()
				.Subscribe(_ =>
				{
					IsTouch = false;
				});
		}

		private void SendTouchEvent(TouchState _state, TouchArea _area, Touch[] _touchs)
		{
			//HiDebug.Log("_state => ", _state);
			List<Action<TouchState, TouchArea, Touch[]>> _sends = ReceivingSetList[(int)_state];
			if (_sends != null)
			{
				for (int _i = 0; _i < _sends.Count; _i++)
				{
					if (_sends[_i] != null)
					{
						_sends[_i](_state, _area, _touchs);
					}
				}
			}
		}

		private TouchArea GetTouchArea(Touch _touch)
		{
			PointerEventData _ped = new PointerEventData(EventSystem.current);
			_ped.position = _touch.position;
			List<RaycastResult> _results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(_ped, _results);
			if (_results.Count > 0)
			{
				_results.Clear();
				return TouchArea.CanvasArea;
			}
			return TouchArea.PhysicalArea;
		}

		private Touch[] GetTouchs(TouchState _state)
		{
			Touch[] _rts = null;
#if UNITY_EDITOR
			switch (_state)
			{
				case TouchState.None:
				case TouchState.TouchAndWait:
				case TouchState.Touch:
				case TouchState.Touch_Long:
				case TouchState.TouchDrag_1Point:
					_rts = new Touch[] { new Touch() };
					_rts[0].position = Input.mousePosition;
					break;
				case TouchState.TouchDrag_2Point:
					float _gap = UnityEngine.Random.Range(5f, 15f);
					_rts = new Touch[] { new Touch(), new Touch() };
					_rts[0].position = new Vector2(Input.mousePosition.x + _gap, Input.mousePosition.y + _gap);
					_rts[1].position = new Vector2(Input.mousePosition.x - _gap, Input.mousePosition.y - _gap);
					break;
			}
#elif UNITY_IOS || UNITY_ANDROID
			_rts = Input.touches;
#endif
			if (_rts != null && _rts.Length == 0)
			{
				_rts = null;
			}
			return _rts;
		}

		private int GetTouchCount(TouchState _state)
		{
			int _count = -1;
#if UNITY_EDITOR
			switch (_state)
			{
				case TouchState.None:
				case TouchState.TouchUp:
					if (Input.GetMouseButtonUp(0) == true || Input.GetMouseButtonUp(1) == true)
					{
						_count = 0;
					}
					break;
				case TouchState.Touch:
				case TouchState.Touch_Long:
				case TouchState.TouchDrag_1Point:
					if (Input.GetMouseButtonDown(0) == true)
					{
						_count = 1;
					}
					break;
				case TouchState.TouchDrag_2Point:
					if (Input.GetMouseButtonDown(1) == true)
					{
						_count = 2;
					}
					break;
			}
#elif UNITY_IOS || UNITY_ANDROID
			_count = Input.touchCount;
#endif
			return _count;

			//return 1;
		}
	}
}
