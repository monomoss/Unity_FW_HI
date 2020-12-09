
namespace HI
{
	//기본 이벤트
	public enum HiEventID
	{
		None = -1,
		//
		GoLobbyScreen = 0,
		GoGameScreen = 1,
		//
		ChangeScreen,
		ChangeTouchAction,
		TransformReset,
		CubeRotation,
		CubeInhalator,
		ScoreUpdate,
		HeartUpdate,

		AD_Load_Banner,
		//AD_Show_Banner,
		AD_Load_Interstitial,
		AD_Show_Interstitial,
		//AD_Load_Native,
		//AD_Show_Native,

		Dim_Hide,
		Dim_Show_0,
		Dim_Show_5,

		Loading_Hide,
		Loading_Show,

		Popup_Hide,
		Popup_Show,
		//Update_GameRoomItems,

		Window_Hide,
		Window_Show,

		GameClock_Start,
		GameClock_Stop,
		GuideScreen_Start,
		GuideScreen_End,
		GuideScreen_Next,

		CubeSelectFailure,
		Max
	}


	//버튼 이벤트
	public enum HiEventBtnID
	{
		None = -1,
		BTN_BackSpace,
		BTN_Setting,
		BTN_Shop,
		BTN_Pause,
		BTN_Guide,
		Max
	}


	//창 구별 이벤트
	public enum HiWindowID
	{
		None = -1,
		Setting,
		Result,
		Guide,
		Stage41,
		Shop,
		Max
	}
}
