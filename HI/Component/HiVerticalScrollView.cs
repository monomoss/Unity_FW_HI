using UnityEngine;
using HI.ExMethods;
using UnityEngine.UI;
using System.Collections.Generic;
using HI.Abstract;

namespace HI
{
	public class HiVerticalScrollView : MonoBehaviour
	{
		enum EnScrollDirectionType
		{
			None,
			Up,
			Down
		}

		private ScrollRect scrollRect;
		private GameObject itemGameObject;
		private Vector2 itemSize = new Vector2(100, 50);
		private int itemCount = 10;
		private int itemTotalCount = 20;

		private int startIndex = -1;
		private int endIndex = 0;
		private List<GameObject> items = new List<GameObject>();

		private EnScrollDirectionType scrollDirectionType = EnScrollDirectionType.None;
		private int scrollViewUpdateCount = 0;
		private float tempScrollPosY = 0;

		private Vector3 ppp = default;

		void Update()
		{
			//if (Input.GetMouseButtonUp(0))
			//{
			//	scrollRect.vertical = false;
			//} else 
			//if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
			//{
			//	if (ppp == default)
			//	{
			//		ppp = Input.mousePosition;
			//	}
			//	else
			//	{
			//		if (Mathf.Abs(Input.mousePosition.y - ppp.y) > 50)
			//		{
			//			scrollRect.vertical = true;
			//		}
			//	}
			//}
			//if (scrollRect != null)
			//{
			//	scrollRect.vertical = false;
			//}

			ScrollViewUpdate();
		}

		void OnDisable()
		{
			if (scrollRect != null)
			{
				S_ContentsInfo.Instance.LobbyScrollRectPosition = scrollRect.content.localPosition;
			}
		}

		public void ScrollViewBuild(GameObject _item, Vector2 _itemSize, int _viewItemCount, int _totalItemCount)
		{
			if (_viewItemCount > _totalItemCount)
			{
				_viewItemCount = _totalItemCount;
			}

			scrollRect = this.GetComponent<ScrollRect>();
			itemGameObject = _item;
			itemSize = _itemSize;
			itemCount = _viewItemCount;
			itemTotalCount = _totalItemCount;

			GameObject _go = null;
			Vector3 _lp;
			for (int _i = 0; _i < itemCount; _i++)
			{
				_go = GameObject.Instantiate(itemGameObject, scrollRect.content);
				_lp = _go.transform.localPosition;
				_go.transform.localPosition = new Vector3(_lp.x, -itemSize.y * _i, _lp.z);
				items.Add(_go);

				//HiDebug.Log(">>>>>>>>>", items[i].GetComponent<HiScrollViewItem>());
				//
				//items[i].GetComponent<MG_ItemGameRoomInfo>().GO_text.text = i.ToString();
				items[_i].GetComponent<HiScrollViewItem>().ItemUpdate(endIndex);
				//HiDebug.Log(">>>>>>>>>", items[i].GetComponent<HiScrollViewItem>().ItemIndex);

				++endIndex;
			}

			scrollRect.content.GetComponent<RectTransform>().SetRectSize(0, itemSize.y * itemTotalCount);
			scrollRect.onValueChanged.AddListener(delegate { OnValueChanged(); });
			scrollRect.content.localPosition = S_ContentsInfo.Instance.LobbyScrollRectPosition;
			//scrollRect.content.GetComponent<RectTransform>().offsetMax = new Vector2(-50, 0);
			OnValueChanged();

			//scrollDirectionType = EnScrollDirectionType.None;
			//ScrollViewUpdate();
		}


		private void OnValueChanged()
		{
			float _sd = (int)scrollRect.content.localPosition.y - (int)tempScrollPosY;
			if (_sd >= 0)
			{
				scrollDirectionType = EnScrollDirectionType.Down;
			}
			else if (_sd < 0)
			{
				scrollDirectionType = EnScrollDirectionType.Up;
			}
			tempScrollPosY = scrollRect.content.localPosition.y;
			scrollViewUpdateCount = itemCount;
		}

		private void ScrollViewUpdate()
		{
			if (scrollDirectionType == EnScrollDirectionType.None)
			{
				return;
			}

			--scrollViewUpdateCount;
			if (scrollViewUpdateCount <= 0)
			{
				scrollDirectionType = EnScrollDirectionType.None;
				return;
			}

			Vector3 _lp;
			if (scrollDirectionType == EnScrollDirectionType.Up)
			{
				if (startIndex >= 0)
				{
					for (int i = 0; i < itemCount; i++)
					{
						if (items[i].transform.position.y < -4)
						{
							_lp = items[i].transform.localPosition;
							items[i].transform.localPosition = new Vector3(_lp.x, -itemSize.y * startIndex, _lp.z);

							//
							//items[i].GetComponent<MG_ItemGameRoomInfo>().GO_text.text = startIndex.ToString();
							items[i].GetComponent<HiScrollViewItem>().ItemUpdate(startIndex);

							if (startIndex < 0)
							{
								items[i].SetActive(false);
							}
							else
							{
								items[i].SetActive(true);
							}
							--startIndex;
							--endIndex;
							S_ContentsInfo.Instance.LobbyScrollSItemIndex = startIndex;
							S_ContentsInfo.Instance.LobbyScrollEItemIndex = endIndex;
						}
					}
				}
			}
			else if (scrollDirectionType == EnScrollDirectionType.Down)
			{
				if (endIndex < itemTotalCount)
				{
					for (int i = 0; i < itemCount; i++)
					{
						if (items[i].transform.position.y > 4)
						{
							_lp = items[i].transform.localPosition;
							items[i].transform.localPosition = new Vector3(_lp.x, -itemSize.y * endIndex, _lp.z);

							//
							//items[i].GetComponent<MG_ItemGameRoomInfo>().GO_text.text = endIndex.ToString();
							items[i].GetComponent<HiScrollViewItem>().ItemUpdate(endIndex);

							if (endIndex >= itemTotalCount)
							{
								items[i].SetActive(false);
							}
							else
							{
								items[i].SetActive(true);
							}
							++startIndex;
							++endIndex;
							S_ContentsInfo.Instance.LobbyScrollSItemIndex = startIndex;
							S_ContentsInfo.Instance.LobbyScrollEItemIndex = endIndex;
						}
					}
				}
			}
		}
	}
}
