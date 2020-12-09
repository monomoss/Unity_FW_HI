namespace HI.Abstract
{
	public abstract class HiSingleton<T> where T : class, new()
	{
		private static T instance = null;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new T();
				}
				return instance;
			}
		}

		public HiSingleton()
		{
			if (instance != null)
			{
				throw new System.Exception("Error ▶ Singleton : Duplicate creation");
			}
		}
	}
}
