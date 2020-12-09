using HI.Abstract;
using HI.Utility;

namespace HI
{
	public class HiMessage : HiSingleton<HiMessage>
	{
		//--------------------------------------------------------------------
		public string GetMessage_SignInFailed()
		{
			return MsgLoginFailed[Index];
		}
		public string GetMessage_AdsOnLoad()
		{
			return MsgAdsOnLoad[Index];
		}
		public string GetMessage_SigningIn()
		{
			return MsgSigningIn[Index];
		}
		public string GetMessage_WritingFailed()
		{
			return MsgWritingFailed[Index];
		}
		public string GetMessage_ReadingFailed()
		{
			return MsgReadingFailed[Index];
		}
		public string GetMessage_WritingFailedLB()
		{
			return MsgWritingFailed_LB[Index];
		}
		public string GetMessage_GameExit()
		{
			return MsgGameExit[Index];
		}
		public string GetMessage_AppExit()
		{
			return MsgAppExit[Index];
		}
		public string GetMessage_GamePause()
		{
			return MsgGamePause[Index];
		}
		public string GetMessage_GameFail()
		{
			return MsgGameFail[Index];
		}
		public string GetMsgPreparation()
		{
			return MsgPreparation[Index];
		}
		public string GetMsgStage41()
		{
			return MsgStage41[Index];
		}
		public string GetMsgNetworkFailed()
		{
			return MsgNetworkFailed[Index];
		}
		public string GetMsgLoginStateError()
		{
			return MsgLoginStateError[Index];
		}
		public string GetMsgAppUpdates()
		{
			return MsgAppUpdates[Index];
		}
		public string GetMsgAppReStart()
		{
			return MsgAppReStart[Index];
		}
		public string GetMsgCloseTutorial()
		{
			return MsgCloseTutorial[Index];
		}
		//
		public string GetText_Resume()
		{
			return Resume[Index];
		}
		public string GetText_Retry()
		{
			return Retry[Index];
		}
		public string GetText_Yes()
		{
			return Yes[Index];
		}
		public string GetText_No()
		{
			return No[Index];
		}
		public string GetText_Confirm()
		{
			return Confirm[Index];
		}


		//--------------------------------------------------------------------
		private readonly string[] MsgLoginFailed =
		{
			"게임 서비스 로그인에 실패했습니다. 인터넷 상태 및 Google Play 게임 로그인 상태를 확인하십시오.",
			"Game service login failed. Please check your internet status and Google Play game login status."
		};
		private readonly string[] MsgAdsOnLoad =
		{
			"화면 전환 중에 광고가 노출 될 수 있습니다.",
			"Ads may be exposed during screen transitions."
		};
		private readonly string[] MsgSigningIn =
		{
			"로그인 단계가 진행 중입니다.",
			"The sign-in phase is in progress."
		};
		private readonly string[] MsgWritingFailed =
		{
			"게임 정보를 저장하지 못했습니다.",
			"Failed to save game information."
		};
		private readonly string[] MsgReadingFailed =
		{
			"게임 정보를 읽을 수 없습니다.",
			"Game information could not be read."
		};
		private readonly string[] MsgWritingFailed_LB =
		{
			"리더보드 정보를 저장하지 못했습니다.",
			"Failed to save leaderboard information."
		};
		private readonly string[] MsgGameExit =
		{
			"게임이 진행 중입니다. 게임을 종료 하시겠습니까?",
			"The game is in progress. Are you sure you want to quit the game?"
		};
		private readonly string[] MsgAppExit =
		{
			"앱을 종료 하시겠습니까?",
			"Are you sure you want to quit the app?"
		};
		private readonly string[] MsgGamePause =
		{
			"현재 게임을 일시 중지했습니다. 아래 버튼을 클릭하여 다시 시작하십시오.",
			"You have suspended the current game. Click the button below to restart."
		};
		private readonly string[] MsgGameFail =
		{
			"임무 실패",
			"Mission failure"
		};
		private readonly string[] MsgPreparation = 
		{
			"준비 중입니다",
			"In preparation"
		};
		private readonly string[] MsgStage41 =
		{
			"당신은 당신의 능력을 입증했습니다. 이제부터는 특별한 미네랄을 수집해야합니다. 이 미네랄은 잔류 레벨을 방출하는 미네랄입니다. 따라서 미네랄을 수집하더라도 주위 큐브의 농도 값은 변경되지 않습니다. 행운을 빕니다.",
			"You have proven your ability. From now on, you need to collect special minerals. These minerals are minerals that release residual levels. Therefore, even if you collect minerals, the concentration value of the surrounding cube will not change. good luck."
		};
		private readonly string[] MsgNetworkFailed =
		{
			"인터넷의 상태를 확인하십시오. 게임 버전 및 데이터 관리에 필요합니다.",
			"Please check the status of the Internet. Needed for game version and data management."
		};
		private readonly string[] MsgLoginStateError =
		{
			"Google Play 게임 서비스에 로그인하지 않으면 게임 데이터가 손실 될 수 있습니다.",
			"If you do not sign in to Google Play Game Services, your game data may be lost."
		};
		private readonly string[] MsgAppUpdates =
		{
			"앱 업데이트가 필요합니다.",
			"App updates are required."
		};
		private readonly string[] MsgAppReStart =
		{
			"변경 사항을 적용하려면 앱을 다시 시작해야합니다.",
			"You must restart the app for the changes to take effect."
		};
		private readonly string[] MsgCloseTutorial =
		{
			"튜토리얼을 종료했습니다. 튜토리얼을 다시 열려면 설정 창을 확인하십시오.",
			"You have exited the tutorial. Check the settings window to reopen the tutorial."
		};
		//
		private readonly string[] Resume =
		{
			"다시 시작",
			"Resume"
		};
		private readonly string[] Retry =
		{
			"다시 시도",
			"Retry"
		};
		private readonly string[] Yes =
		{
			"예",
			"Yes"
		};
		private readonly string[] No =
		{
			"아니",
			"No"
		};
		private readonly string[] Confirm =
		{
			"확인",
			"Confirm"
		};


		//--------------------------------------------------------------------
		private int Index
		{
			get
			{
				return (int)HiSystemInfo.Instance.GetLanguageType();
			}
		}
	}
}
