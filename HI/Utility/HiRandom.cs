using System;
using System.Collections;
using System.Collections.Generic;

namespace HI.Utility
{
	public class HiRandom<T> //where T : struct
	{
		private List<T> randomList = null;


		//public HiRandom(IEnumerable<T> collection)
		//{
		//	randomList = new List<T>(collection);
		//}

		public HiRandom(IEnumerable<T> collection)
		{
			randomList = new List<T>();
			randomList.AddRange(collection);
			HiDebug.Log("BBB0", randomList.Count);
		}

		public HiRandom(int min, int max)
		{
			int length = max - min;
			int[] list = new int[length];
			for (int i = 0; i < list.Length; i++)
			{
				list[i] = i + min;
			}
			randomList = new List<T>(list as T[]);
		}
		

		public T Range()
		{
			if (randomList == null ||
				randomList.Count == 0)
			{
				//HiDebug.Log("BBB1 : ", randomList);
				//HiDebug.Log("BBB2 : ", randomList.Count);
				throw new Exception("Error ▶ HiRandom : Initialization Failed");
			}

			int index = UnityEngine.Random.Range(0, randomList.Count);
			T num = (T)randomList[index];
			randomList.RemoveAt(index);

			return num;
		}




		//public UnityEngine.Random UnityRandom()
		//{
		//	UnityEngine.Random _ran = new UnityEngine.Random();
		//	return _ran;
		//}

		//public Random SystemRandom()
		//{
		//	Random _ran = new Random();
		//	return _ran;
		//}


		//public IEnumerator<T> GetEnumerator()
		//{
		//	throw new NotImplementedException();
		//}


		//public List<int> GetRandomList()
		//{
		//	string[] txt_data = new string[] { "unity", "unreal",....}; // 배열 선언
		//	List<string> list = new List<string>(txt_data); // 리스트 안에 위에 선언한 배열을 넣음

		//	for (int i = 0; i < txt_data.Length; i++)
		//	{
		//		int tagetIndex = Random.Range(0, list.Count); // 랜덤함수로 인덱스 값을 얻음
		//		string a = list[tagetIndex]; // 리스트안에 그 인덱스값 번째에 해당하는 데이터를 뽑음
		//		list.Remove(list[tagetIndex]); // 그 데이터를 삭제
		//	}
		//}
	}
}
