using HI.Abstract;
using UnityEngine;

namespace HI
{
	public class HiSystemInfo : HiSingleton<HiSystemInfo>
	{
		public enum RuntimePlayerType
		{
			None = -1,
			UNITY_EDITOR,
			UNITY_ANDROID,
			UNITY_IPHONE
		}

		public enum LanguageType
		{
			None = -1,
			KO,
			EN,
		}

		//플랫폼 타입
		public RuntimePlatform GetPlatformInfo()
		{
			return Application.platform;
		}

		//플레이어 타입
		public RuntimePlayerType GetPlayerInfo()
		{
			RuntimePlayerType pt = RuntimePlayerType.None;
#if UNITY_EDITOR
			pt = RuntimePlayerType.UNITY_EDITOR;
#elif UNITY_ANDROID
			pt = RuntimePlayerType.UNITY_ANDROID;
#elif UNITY_IOS || UNITY_IPHONE
			pt = RuntimePlayerType.UNITY_IPHONE;
#endif
			return pt;
		}

		//언어 타입
		private LanguageType languageType = LanguageType.None;

		public LanguageType GetLanguageType()
		{
			return languageType;
		}

		public void SetLanguageType(LanguageType type=LanguageType.None)
		{
			if (type != LanguageType.None)
			{
				languageType = type;
			}
			else
			{
				if (Application.systemLanguage == SystemLanguage.Korean)
				{
					languageType = LanguageType.KO;
				}
				else
				{
					languageType = LanguageType.EN;
				}
			}
		}
	}
}
