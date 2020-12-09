namespace HI.Abstract
{
	public abstract class HiSingletonMono<T> : UnityEngine.MonoBehaviour where T : UnityEngine.MonoBehaviour
	{
		private static T instance = null;

		public HiSingletonMono()
		{
			if (instance != null)
			{
				throw new System.Exception("Error ▶ Singleton : Duplicate creation");
			}
		}

		public static T Instance
		{
			get
			{
				//if (instance == null)
				//{
				//	throw new System.Exception("Error ▶ Singleton : MonoBehaviour instance is null");
				//}
				return instance;
			}
		}				

		virtual protected void Awake()
		{
			instance = this as T;
		}
	}
}
