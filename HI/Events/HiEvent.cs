using HI.Abstract;
using System;
using System.Collections.Generic;

namespace HI
{
	public class HiEvent : HiSingleton<HiEvent>
	{
		private List<EventDispatcher> dispatcherList = new List<EventDispatcher>();

		/*
		public static void AddEventListener<T>(string eventName, DG_DDD<T> method)
		{
			EventDispatcher listener = Instance.GetListener(eventName);
			if (listener == null)
			{
				listener = new EventDispatcher(eventName);
				Instance.dispatcherList.Add(listener);
			}
			if (method != null)
			{
				listener.AddEvent(method);
				//listener.AddValue(value);
			}
		}

		public static void AddEventListener(string eventName, DG_Normal method)
		{
			EventDispatcher listener = Instance.GetListener(eventName);
			if (listener == null)
			{
				listener = new EventDispatcher(eventName);
				Instance.dispatcherList.Add(listener);
			}
			if (method != null)
			{
				listener.AddEvent(method);
			}
		}
		*/


		//=============================================================
		public static void AddEventListener<T>(HiEventID eventID, T method)
		{
			Instance._AddEventListener<T>(eventID.ToString(), method);
		}
		public static void EventDispatch(HiEventID eventID, params object[] args)
		{
			Instance._EventDispatch(eventID.ToString(), args);
		}
		public static void RemoveEventListener<T>(HiEventID eventID, T method)
		{
			Instance._RemoveEventListener<T>(eventID.ToString(), method);
		}
		public static void RemoveEventListener(HiEventID eventID)
		{
			Instance._RemoveEventListener(eventID.ToString());
		}
		
		public static void AddEventListener<T>(HiEventBtnID eventID, T method)
		{
			Instance._AddEventListener<T>(eventID.ToString(), method);
		}
		public static void EventDispatch(HiEventBtnID eventID, params object[] args)
		{
			Instance._EventDispatch(eventID.ToString(), args);
		}
		public static void RemoveEventListener<T>(HiEventBtnID eventID, T method)
		{
			Instance._RemoveEventListener<T>(eventID.ToString(), method);
		}
		public static void RemoveEventListener(HiEventBtnID eventID)
		{
			Instance._RemoveEventListener(eventID.ToString());
		}
		//=============================================================


		private void _AddEventListener<T>(string eventName, T method)
		{
			EventDispatcher listener = Instance.GetListener(eventName);
			if (listener == null)
			{
				listener = new EventDispatcher(eventName);
				Instance.dispatcherList.Add(listener);
			}
			if (method != null)
			{
				listener.AddEvent<T>(method);
			}
		}

		private void _EventDispatch(string eventName, params object[] args)
		{
			EventDispatcher listener1 = Instance.GetListener(eventName);
			List<DG_Objects> methodList1 = null;
			if (listener1 != null)
			{
				methodList1 = listener1.GetList_Objects();
			}
			if (methodList1 != null && methodList1.Count > 0)
			{
				Instance.MethodCall1(methodList1, args);
			}
			//
			EventDispatcher listener2 = Instance.GetListener(eventName);
			List<DG_Normal> methodList2 = null;
			if (listener2 != null)
			{
				methodList2 = listener2.GetList_Normal();
			}
			if (methodList2 != null && methodList2.Count > 0)
			{
				Instance.MethodCall2(methodList2);
			}
		}

		private void _RemoveEventListener<T>(string eventName, T method)
		{
			EventDispatcher listener = Instance.GetListener(eventName);
			if (listener == null)
			{
				return;
			}
			listener.RemoveEvent<T>(method);
		}

		private void _RemoveEventListener(string eventName)
		{
			EventDispatcher listener = Instance.GetListener(eventName);
			if (listener == null)
			{
				return;
			}
			listener.RemoveAllEvent();
			Instance.dispatcherList.Remove(listener);
		}


		private EventDispatcher GetListener(string eventName)
		{
			EventDispatcher listener = null;
			for (int i = 0; i < dispatcherList.Count; i++)
			{
				if (eventName == dispatcherList[i].GetName())
				{
					listener = dispatcherList[i];
					break;
				}
			}
			return listener;
		}

		private void MethodCall1(List<DG_Objects> list, params object[] args)
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i](args);
			}
		}

		private void MethodCall2(List<DG_Normal> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i]();
			}
		}



		/// <summary>
		/// EventDispatcher is InnerClass
		/// </summary>
		private class EventDispatcher
		{
			private string cpEventName = "";
			private List<DG_Objects> cpMethodList1 = new List<DG_Objects>();
			private List<DG_Normal> cpMethodList2 = new List<DG_Normal>();
			
			//private List<Object> cpValueList = new List<Object>();

			public EventDispatcher(string eventName)
			{
				if (cpEventName == eventName)
				{
					return;
				}
				cpEventName = eventName;
			}

			/*
			public void AddEvent(DG_Objects dg)
			{
				cpMethodList1.Add(dg);
			}

			public void AddEvent(DG_Normal dg)
			{
				cpMethodList2.Add(dg);
			}
			*/
			public void AddEvent<T>(T dg)
			{
				if (typeof(T) == typeof(DG_Objects))
				{
					cpMethodList1.Add(dg as DG_Objects);
					return;
				}
				if (typeof(T) == typeof(DG_Normal))
				{
					cpMethodList2.Add(dg as DG_Normal);
					return;
				}
			}

			public void RemoveEvent<T>(T dg)
			{
				if (typeof(T) == typeof(DG_Objects))
				{
					cpMethodList1.Remove(dg as DG_Objects);
					return;
				}
				if (typeof(T) == typeof(DG_Normal))
				{
					cpMethodList2.Remove(dg as DG_Normal);
					return;
				}
			}

			public void RemoveAllEvent()
			{
				cpMethodList1 = null;
				cpMethodList1 = null;
			}

			//public void AddValue(Object value)
			//{
			//	cpValueList.Add(value);
			//}

			public string GetName()
			{
				return cpEventName;
			}

			public List<DG_Objects> GetList_Objects()
			{
				MethodListNullCheck1();
				return cpMethodList1;
			}

			public List<DG_Normal> GetList_Normal()
			{
				MethodListNullCheck2();
				return cpMethodList2;
			}

			private void MethodListNullCheck1()
			{
				for (int i = 0; i < cpMethodList1.Count; i++)
				{
					if (i >= cpMethodList1.Count)
					{
						break;
					}
					if (cpMethodList1[i].Target == null ||
						cpMethodList1[i].Target.ToString() == "null")
					{
						cpMethodList1.RemoveAt(i);
						--i;
					}
				}
			}

			private void MethodListNullCheck2()
			{
				for (int i = 0; i < cpMethodList2.Count; i++)
				{
					if (i >= cpMethodList2.Count)
					{
						break;
					}
					if (cpMethodList2[i].Target == null ||
						cpMethodList2[i].Target.ToString() == "null")
					{
						cpMethodList2.RemoveAt(i);
						--i;
					}
				}
			}
		}
	}
}
