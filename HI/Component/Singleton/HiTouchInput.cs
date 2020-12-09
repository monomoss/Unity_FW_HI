using HI.Abstract;
using HI.Utility;
using UnityEngine;

namespace HI
{
	public class HiTouchInput : HiSingletonMono<HiTouchInput>
	{
		public enum EnInputType
		{
			Single,
			Multi
		}

		private EnInputType cpInputType = EnInputType.Single;
		private Touch[] cpTouchs = null;


		public int GetTouchCount()
		{
			InputTouchUpdate();
			if (cpTouchs == null)
			{
				return 0;
			}
			return cpTouchs.Length;
		}

		public Touch[] GetTouchs()
		{
			InputTouchUpdate();
			return cpTouchs;
		}

		public Touch GetTouch(int index)
		{
			InputTouchUpdate();
			if (cpTouchs != null && index < cpTouchs.Length)
			{
				return cpTouchs[index];
			}
			else
			{
				return default(Touch);
			}
		}

		public GameObject GetTouchObject()
		{
			RaycastHit hit;
			GameObject target = null;
			//마우스 포인트 근처 좌표를 만든다.
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//마우스 근처에 오브젝트가 있는지 확인
			if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
			{
				target = hit.collider.gameObject;
			}
			return target;
		}

		public EnInputType GetInputType
		{
			get
			{
				return cpInputType;
			}
		}

		public EnInputType SetInputType
		{
			set
			{
				cpInputType = value;
				if (cpInputType == EnInputType.Multi)
				{
					Input.multiTouchEnabled = true;
				}
				else
				{
					Input.multiTouchEnabled = false;
				}
			}
		}

		public Vector3 GetMousePosition()
		{
			return Input.mousePosition;
		}


		private void InputTouchUpdate()
		{
			cpTouchs = null;
			switch (HiSystemInfo.Instance.GetPlatformInfo())
			{
				case RuntimePlatform.Android:
				case RuntimePlatform.IPhonePlayer:
					cpTouchs = Input.touches;
					break;

				//case RuntimePlatform.WindowsEditor:
				//case RuntimePlatform.OSXEditor:
				//	break;

				default:
					if (Input.GetMouseButton(0) == true)
					{
						cpTouchs = new Touch[] { new Touch() };
						cpTouchs[0].position = Input.mousePosition;
					}
					else if (Input.GetMouseButton(1) == true)
					{
						cpTouchs = new Touch[] { new Touch(), new Touch() };
						cpTouchs[0].position = Input.mousePosition;
						cpTouchs[1].position = Vector2.zero;
					}
					else if (Input.GetMouseButton(2) == true)
					{
						cpTouchs = new Touch[] { new Touch(), new Touch() };
						cpTouchs[0].position = Input.mousePosition;
						cpTouchs[1].position = Input.mousePosition;
					}
					else
					{
						//cpTouchs = new Touch[] { new Touch() };
						//cpTouchs[0].position = Input.mousePosition;
						cpTouchs = null;
					}
					break;
			}
		}
	}
}
