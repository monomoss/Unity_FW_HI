using HI.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace HI.Utility
{
	public class HiUtile : HiSingleton<HiUtile>
	{
		private Stopwatch stopwatch = new Stopwatch();

		public HiUtile()
		{
			stopwatch.Start();
		}
		
		//플레이 타임
		public long GetPlayTime()
		{
			//stopwatch.ElapsedMilliseconds > 10 * 1000
			return stopwatch.ElapsedMilliseconds;
		}

		//디바이스ID
		public string GetDeviceID()
		{
			return SystemInfo.deviceUniqueIdentifier.ToUpper();
			//return "null";
			//return "440d8b6a92e2654e13812fc7f2378fba".ToUpper();
		}


		//오브젝트 멤버 포함여부 확인
		public bool IsInclude(object item, object[] list)
		{
			foreach (object element in list)
			{
				if(element == item)
				{
					return true;
				}
			}
			return false;
		}
		public bool IsInclude(object item, List<object> list)
		{
			foreach (object element in list)
			{
				if (element == item)
				{
					return true;
				}
			}
			return false;
		}


		//컨퍼넌트 바인딩
		public T GetComponentEx<T>(GameObject go) where T : Component
		{
			T cp = go.GetComponent<T>();
			if (cp == null)
			{
				cp = go.GetComponent<T>();
			}
			if (cp == null)
			{
				cp = go.AddComponent<T>();
			}
			return cp;
		}
		public T GetComponentEx<T>(GameObject go, ref T cp) where T : Component
		{
			if (cp == null)
			{
				cp = go.GetComponent<T>();
			}
			if (cp == null)
			{
				cp = go.AddComponent<T>();
			}
			return cp;
		}


		//오브젝트 동적 생성
		public object ClassNameToObjet(string _className)
		{
			string className = _className;
			Assembly assembly = Assembly.GetExecutingAssembly();
			Type t = assembly.GetType(className);
			return Activator.CreateInstance(t);
		}
		//메소드 동적 호출
		public void MethodInvoke(object targer, string methodName, params object[] arg)
		{
			Type type = null;
			MethodInfo methodInfo = null;

			type = targer.GetType();
			if (type == null)
			{
				return;
			}

			methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (methodInfo == null)
			{
				return;
			}

			methodInfo.Invoke(targer, arg);
		}
		//필드 동적 호출
		public object GetFieldValue(Type _t, string _fieldName)
		{
			object _result = null;
			FieldInfo _fi = _t.GetField(_fieldName);
			if (_fi != null)
			{
				_result = _fi.GetValue(null);
			}
			return _result;
		}


		//string <-> object 치환
		public byte[] StringToBytes(string stringToConvert)
		{
			return Encoding.UTF8.GetBytes(stringToConvert);
		}
		public string BytesToString(byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes);
		}
		public byte[] ObjectToBytes(object _obj)
		{
			byte[] bt;
			BinaryFormatter formatter = new BinaryFormatter();
			MemoryStream memStream = new MemoryStream();
			formatter.Serialize(memStream, _obj);
			bt = memStream.ToArray();
			//HiDebug.Log("ObjectToBytes 001", bytes);
			return bt;
		}
		public object BytesToObject(byte[] _bytes)
		{
			object obj;
			BinaryFormatter formatter1 = new BinaryFormatter();
			MemoryStream memStream1 = new MemoryStream(_bytes);
			obj = formatter1.Deserialize(memStream1);
			//HiDebug.Log("BytesToObject 002", obj);
			return obj;
		}


		//숫자 -> 문자열 (002)
		public string NumberToString(int _num, int _digit = 0)
		{
			return NumberToString(_num.ToString(), _digit);
		}
		public string NumberToString(long _num, int _digit = 0)
		{
			return NumberToString(_num.ToString(), _digit);
		}
		public string NumberToString(string _num, int _digit = 0)
		{
			string _str = _num;
			while(_str.Length < _digit)
			{
				_str = "0" + _str;
			}
			return _str;
		}


		//숫자 -> 문자열 (123,456,789)
		//string.Format("{0:n0}", won);
		//string.Format("{0}", won.ToString("n0"));
		//string.Format("{0:#,##0}", won);
		//string.Format("{0}", won.ToString("#,##0"));
		public string NumberAmountUnit(int _num)
		{
			return string.Format("{0:n0}", _num);
		}
		public string NumberAmountUnit(long _num)
		{
			return string.Format("{0:n0}", _num);
		}


		//반올림
		public int NumberRound(float _value)
		{
			int _result = (int)_value;
			if(_value - _result > 0.5)
			{
				_result += 1;
			}
			return _result;
		}


		//딜레이 메소드 호출
		public void MethodCall(Action _methodName, float _delayTime=0)
		{
			HiMain.Instance.StartCoroutine(MethodCallEnumerator(_methodName, _delayTime));
		}
		private IEnumerator MethodCallEnumerator(Action _methodName, float _delayTime)
		{
			yield return new WaitForSeconds(_delayTime);
			_methodName();
		}


















		//public void DestroyAllGameObject()
		//{
		//	GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();
		//	for (int i = 0; i < temp.Length; i++)
		//	{
		//		GameObject.Destroy(temp[i].gameObject);
		//	}
		//}


		//public void SetActiveAllGameObject(bool value)
		//{
		//	GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();

		//	for (int i = 0; i < temp.Length; i++)
		//	{
		//		temp[i].SetActive(value);
		//	}
		//}

		//public void DestroyAllGameObject()
		//{
		//	GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();
		//	for (int i = 0; i < temp.Length; i++)
		//	{
		//		GameObject.Destroy(temp[i]);
		//	}
		//}

		//public void GameObjectChildsSetActive(GameObject target, bool value)
		//{
		//	Transform temp1 = target.transform;
		//	Transform temp2 = null;

		//	target.gameObject.SetActive(value);
		//	for (int i = 0; i < temp1.childCount; i++)
		//	{
		//		temp2 = temp1.GetChild(i);
		//		temp2.gameObject.SetActive(value);
		//	}
		//}

		//public void GameObjectChildsSetActiveAll(GameObject target, bool value)
		//{
		//	Transform temp1 = target.transform;
		//	Transform temp2 = null;

		//	target.gameObject.SetActive(value);
		//	for (int i = 0; i < temp1.childCount; i++)
		//	{
		//		temp2 = temp1.GetChild(i);
		//		temp2.gameObject.SetActive(value);
		//		if (temp2.childCount > 0)
		//		{
		//			GameObjectChildsSetActiveAll(temp2.gameObject, value);
		//		}
		//	}
		//}

	}
}
