using HI.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HI
{
	public class HiObjectRotation : MonoBehaviour
	{
		protected enum EnTouchAreaType
		{
			Full,
			Self,
			Selective
		}
		private enum EnTouchActionState
		{
			None,
			Fail,
			Down,
			DragX,
			DragY,
			Rotation
		}

		private EnTouchAreaType cpTouchAreaMode = EnTouchAreaType.Self;
		private EnTouchActionState cpTouchState = EnTouchActionState.None;
		private int cpTouchCount = 0;
		private readonly int cpTouchBoundary = 20;
#if Symbol_Html5
		private readonly float cpResistivity = 3.0f;
#else
		private readonly float cpResistivity = 4.0f;
#endif

		private HiTouchInput cpInput = null;
		private HiUtile cpUtil = null;
		private GameObject[] cpTouchTargets = null;
		private Vector2 cpTouch1Position;
		private Vector2 cpTouch2Position;

		private bool cpTouchBlock = false;


		protected void Start()
		{
			cpUtil = HiUtile.Instance;
			cpInput = HiTouchInput.Instance;
		}
		protected void Update()
		{
			TouchProcess();
		}
		protected void OnMouseDown()
		{
			if (EventSystem.current.IsPointerOverGameObject() == true)
			{
				cpTouchState = EnTouchActionState.Fail;
			}
		}
		/*
		IEnumerator OnMouseDown()
		{
			if (cpTouchBlock == true)
			{
				yield break;
			}
			cpTouchState = EnTouchActionState.None;
			while (cpInput.GetTouchCount() > 0)
			{
				cpTouchCount = cpInput.GetTouchCount();
				if (cpTouchCount == 1)
				{
					TouchProcessSingle();
				}
				else
				{
					TouchProcessMulti();
				}
				yield return null;
				//yield return new WaitForSeconds(0.04f);
			}
			yield return null;
		}
		*/

		
		protected void SetSelectiveTargets(params GameObject[] targets)
		{
			cpTouchTargets = targets;
		}

		protected void SetTouchAreaMode(EnTouchAreaType mode)
		{
			cpTouchAreaMode = mode;
		}

		protected bool GSetTouchBlock
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
			if (cpTouchState == EnTouchActionState.Fail)
			{
				return;
			}
			if (cpTouchState == EnTouchActionState.None)
			{
				if (IsTouchTarget(cpInput.GetTouchObject()) == true)
				{
					cpTouch1Position = cpInput.GetTouch(0).position;
					cpTouchState = EnTouchActionState.Down;
				}
				else
				{
					cpTouchState = EnTouchActionState.Fail;
					return;
				}
			}
			if (cpTouchState == EnTouchActionState.Down)
			{
				cpTouch2Position = cpInput.GetTouch(0).position;
				if (Vector2.Distance(cpTouch1Position, cpTouch2Position) > cpTouchBoundary)
				{
					cpTouch1Position = cpInput.GetTouch(0).position;
					if (Mathf.Abs(cpTouch1Position.x - cpTouch2Position.x) >= Mathf.Abs(cpTouch1Position.y - cpTouch2Position.y))
					{
						cpTouchState = EnTouchActionState.DragX;
					}
					else
					{
						cpTouchState = EnTouchActionState.DragY;
					}
				}
			}
			if (cpTouchState == EnTouchActionState.DragX || cpTouchState == EnTouchActionState.DragY)
			{
				this.transform.Rotate(new Vector3((cpTouch2Position.y - cpTouch1Position.y) / cpResistivity, (cpTouch1Position.x - cpTouch2Position.x) / cpResistivity, 0), Space.World);
				cpTouch1Position = cpTouch2Position;
				cpTouch2Position = cpInput.GetTouch(0).position;
				HiEvent.EventDispatch(HiEventID.CubeRotation);
			}
		}

		private void TouchProcessMulti()
		{
			if (cpInput.GetTouchCount() != cpTouchCount)
			{
				cpTouchState = EnTouchActionState.None;
				return;
			}
			if (cpTouchState == EnTouchActionState.Fail)
			{
				return;
			}
			if (cpTouchState == EnTouchActionState.None)
			{
				if (Vector2.Distance(cpInput.GetTouch(0).position, cpInput.GetTouch(1).position) < cpTouchBoundary)
				{
					cpTouchState = EnTouchActionState.Down;
					cpTouch1Position = Vector2.LerpUnclamped(cpInput.GetTouch(0).position, cpInput.GetTouch(1).position, 0.5f);
				}
				else
				{
					cpTouchState = EnTouchActionState.Fail;
					return;
				}
			}
			if (cpTouchState == EnTouchActionState.Down)
			{
				cpTouch2Position = Vector2.LerpUnclamped(cpInput.GetTouch(0).position, cpInput.GetTouch(1).position, 0.5f);
				if (Vector2.Distance(cpTouch1Position, cpTouch2Position) > cpTouchBoundary)
				{
					cpTouch1Position = Vector2.LerpUnclamped(cpInput.GetTouch(0).position, cpInput.GetTouch(1).position, 0.5f);
					if (Mathf.Abs(cpTouch1Position.x - cpTouch2Position.x) >= Mathf.Abs(cpTouch1Position.y - cpTouch2Position.y))
					{
						cpTouchState = EnTouchActionState.DragX;
					}
					else
					{
						cpTouchState = EnTouchActionState.DragY;
					}
				}
			}
			if (cpTouchState == EnTouchActionState.DragX || cpTouchState == EnTouchActionState.DragY)
			{
				this.transform.Rotate(new Vector3((cpTouch2Position.y - cpTouch1Position.y) / cpResistivity, (cpTouch1Position.x - cpTouch2Position.x) / cpResistivity, 0), Space.World);
				cpTouch1Position = cpTouch2Position;
				cpTouch2Position = Vector2.LerpUnclamped(cpInput.GetTouch(0).position, cpInput.GetTouch(1).position, 0.5f);
				HiEvent.EventDispatch(HiEventID.CubeRotation);
			}
		}

		private bool IsTouchTarget(GameObject target)
		{
			bool result = false;
			switch (cpTouchAreaMode)
			{
				case EnTouchAreaType.Full:
					result = true;
					break;
				case EnTouchAreaType.Self:
					if (target == this.gameObject)
					{
						result = true;
					}
					break;
				case EnTouchAreaType.Selective:
					if(cpUtil.IsInclude(target, cpTouchTargets))
					{
						result = true;
					}
					break;
			}
			return result;
		}
	}
}
