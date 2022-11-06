using System;
using System.Collections.Generic;
using App;
using App.Utility;
using DataTable;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class MainMenu : MonoBehaviour
{
	// Token: 0x06001484 RID: 5252 RVA: 0x0006DABC File Offset: 0x0006BCBC
	public MainMenu()
	{
		MainMenu.CollisionType[] array = new MainMenu.CollisionType[73];
		array[0] = MainMenu.CollisionType.NON;
		array[1] = MainMenu.CollisionType.NON;
		array[2] = MainMenu.CollisionType.NON;
		array[3] = MainMenu.CollisionType.NON;
		array[4] = MainMenu.CollisionType.NON;
		array[5] = MainMenu.CollisionType.NON;
		array[6] = MainMenu.CollisionType.NON;
		array[7] = MainMenu.CollisionType.NON;
		array[8] = MainMenu.CollisionType.NON;
		array[9] = MainMenu.CollisionType.NON;
		array[11] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[23] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[29] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[37] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[40] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[41] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[42] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[43] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[44] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[45] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[46] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[47] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[48] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[49] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		array[51] = MainMenu.CollisionType.ALERT_BUTTON_OFF;
		this.COLLISION_TYPE_TABLE = array;
		this.m_rankingResultList = new List<NetNoticeItem>();
		this.m_loadInfo = new List<ResourceSceneLoader.ResourceInfo>
		{
			new ResourceSceneLoader.ResourceInfo(ResourceCategory.EVENT_RESOURCE, "EventResourceCommon", true, false, true, "EventResourceCommon", false),
			new ResourceSceneLoader.ResourceInfo(ResourceCategory.EVENT_RESOURCE, "EventResourceMenu", true, false, false, "EventResourceMenu", false),
			new ResourceSceneLoader.ResourceInfo(ResourceCategory.UI, string.Empty, true, false, false, null, false)
		};
		this.m_atomCampain = string.Empty;
		this.m_atomSerial = string.Empty;
		this.m_atomInvalidTextId = string.Empty;
		this.m_request_face_list = new List<int>();
		this.m_request_bg_list = new List<int>();
		this.m_mileage_map_state = new ServerMileageMapState();
		this.m_prev_mileage_map_state = new ServerMileageMapState();
		this.m_eventResourceId = -1;
		this.m_pressedButtonType = MainMenu.PressedButtonType.NONE;
		base..ctor();
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x0006DC14 File Offset: 0x0006BE14
	private void Awake()
	{
		Application.targetFrameRate = SystemSettings.TargetFrameRate;
		float fadeDuration = 0.3f;
		float fadeDelay = 0f;
		bool isFadeIn = true;
		CameraFade.StartAlphaFade(Color.black, isFadeIn, fadeDuration, fadeDelay, new Action(this.OnFinishedFadeOutCallback));
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x0006DC54 File Offset: 0x0006BE54
	private void Start()
	{
		TimeProfiler.EndCountTime("Title-NextScene");
		HudUtility.SetInvalidNGUIMitiTouch();
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "ConnectAlertMaskUI");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(true);
			}
		}
		ConnectAlertMaskUI.StartScreen();
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				SoundManager.BgmVolume = (float)systemdata.bgmVolume / 100f;
				SoundManager.SeVolume = (float)systemdata.seVolume / 100f;
			}
		}
		MenuPlayerSetUtil.ResetMarkCharaPage();
		SoundManager.AddMainMenuCommonCueSheet();
		HudMenuUtility.StartMainMenuBGM();
		GC.Collect();
		this.m_flags.Reset();
		this.m_flags.Set(25, true);
		this.m_flags.Set(26, true);
		this.m_flags.Set(27, true);
		GameObject gameObject3 = GameObject.Find("AllocationStatus");
		if (gameObject3 != null)
		{
			gameObject3.SetActive(false);
			UnityEngine.Object.Destroy(gameObject3);
		}
		this.m_stage_info_obj = GameObject.Find("StageInfo");
		if (this.m_stage_info_obj == null)
		{
			this.m_stage_info_obj = new GameObject();
			if (this.m_stage_info_obj != null)
			{
				this.m_stage_info_obj.name = "StageInfo";
				UnityEngine.Object.DontDestroyOnLoad(this.m_stage_info_obj);
				this.m_stage_info_obj.AddComponent("StageInfo");
			}
		}
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		GameObject mainMenuCmnUIObject = HudMenuUtility.GetMainMenuCmnUIObject();
		if (mainMenuUIObject != null)
		{
			mainMenuUIObject.SetActive(false);
		}
		if (mainMenuCmnUIObject != null)
		{
			mainMenuCmnUIObject.SetActive(false);
		}
		GameObject gameObject4 = GameObject.Find("MainMenuWindow");
		if (gameObject4 != null)
		{
			this.m_main_menu_window = gameObject4.GetComponent<MainMenuWindow>();
		}
		GameObject gameObject5 = GameObject.Find("MainMenuButtonEvent");
		if (gameObject5 != null)
		{
			this.m_buttonEvent = gameObject5.GetComponent<ButtonEvent>();
		}
		if (EventManager.Instance != null && EventManager.Instance.IsStandby())
		{
			this.m_flags.Set(22, true);
		}
		BackKeyManager.AddTutorialEventCallBack(base.gameObject);
		this.m_fsm_behavior = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm_behavior != null)
		{
			TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
			description.initState = new TinyFsmState(new EventFunction(this.StateInit));
			this.m_fsm_behavior.SetUp(description);
		}
		GameObject gameObject6 = GameObject.Find("ConnectAlertMaskUI");
		if (gameObject6 != null)
		{
			this.m_progressBar = GameObjectUtil.FindChildGameObjectComponent<HudProgressBar>(gameObject6, "Pgb_loading");
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetUp(11);
			}
		}
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x0006DF34 File Offset: 0x0006C134
	private void OnDestroy()
	{
		if (this.m_fsm_behavior)
		{
			this.m_fsm_behavior.ShutDown();
			this.m_fsm_behavior = null;
		}
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x0006DF64 File Offset: 0x0006C164
	private void OnApplicationPause(bool pause)
	{
		this.m_flags.Set(21, true);
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x0006DF78 File Offset: 0x0006C178
	private void ChangeState(TinyFsmState nextState, MainMenu.SequenceState sequenceState)
	{
		bool flag = this.m_fsm_behavior.ChangeState(nextState);
		if (flag)
		{
			this.SetCollisionState(this.COLLISION_TYPE_TABLE[(int)sequenceState]);
		}
		this.DebugInfoDraw("MainMenu SequenceState = " + sequenceState.ToString());
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x0006DFC4 File Offset: 0x0006C1C4
	private void OnClickPlatformBackButtonTutorialEvent()
	{
		if (this.m_fsm_behavior != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(102);
			this.m_fsm_behavior.Dispatch(signal);
		}
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x0006DFF8 File Offset: 0x0006C1F8
	private void OnMsgReceive(MsgMenuSequence message)
	{
		this.DebugInfoDraw("MainMenu OnMsgReceive " + message.Sequenece.ToString());
		if (this.m_fsm_behavior != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateMessage(message);
			this.m_fsm_behavior.Dispatch(signal);
		}
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x0006E04C File Offset: 0x0006C24C
	private void SetEventStage(bool flag)
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.EventStage = flag;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x0600148E RID: 5262 RVA: 0x0006E06C File Offset: 0x0006C26C
	// (set) Token: 0x0600148F RID: 5263 RVA: 0x0006E074 File Offset: 0x0006C274
	public bool BossChallenge
	{
		get
		{
			return this.m_bossChallenge;
		}
		set
		{
			this.m_bossChallenge = value;
		}
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x0006E080 File Offset: 0x0006C280
	private void DebugInfoDraw(string msg)
	{
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x0006E084 File Offset: 0x0006C284
	private void ServerEquipChao_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		NetUtil.SyncSaveDataAndDataBase(msg.m_playerState);
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x0006E098 File Offset: 0x0006C298
	private void ServerEquipChao_Failed(MsgServerConnctFailed msg)
	{
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x0006E09C File Offset: 0x0006C29C
	private void SetCollisionState(MainMenu.CollisionType enterCollisionType)
	{
		if (enterCollisionType != MainMenu.CollisionType.ALERT_BUTTON_ON)
		{
			if (enterCollisionType == MainMenu.CollisionType.ALERT_BUTTON_OFF)
			{
				this.SetConnectAlertButtonCollision(false);
			}
		}
		else
		{
			this.SetConnectAlertButtonCollision(true);
		}
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x0006E0DC File Offset: 0x0006C2DC
	private void SetConnectAlertButtonCollision(bool on)
	{
		if (on)
		{
			if (!this.m_alertBtnFlag)
			{
				HudMenuUtility.SetConnectAlertMenuButtonUI(true);
				BackKeyManager.MenuSequenceTransitionFlag = true;
				this.m_alertBtnFlag = true;
			}
		}
		else if (this.m_alertBtnFlag)
		{
			HudMenuUtility.SetConnectAlertMenuButtonUI(false);
			BackKeyManager.MenuSequenceTransitionFlag = false;
			this.m_alertBtnFlag = false;
		}
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x0006E130 File Offset: 0x0006C330
	private MainMenu.CautionType GetCautionType()
	{
		if (StageModeManager.Instance != null && EventManager.Instance != null)
		{
			if (StageModeManager.Instance.IsQuickMode())
			{
				if (EventManager.Instance.IsStandby())
				{
					if (EventManager.Instance.IsInEvent())
					{
						return MainMenu.CautionType.NEW_EVENT;
					}
				}
				else if (EventManager.Instance.Type != EventManager.EventType.UNKNOWN && !EventManager.Instance.IsInEvent())
				{
					return MainMenu.CautionType.END_EVENT;
				}
			}
			else if (EventManager.Instance.IsStandby())
			{
				if ((EventManager.Instance.StandbyType == EventManager.EventType.COLLECT_OBJECT || EventManager.Instance.StandbyType == EventManager.EventType.BGM) && EventManager.Instance.IsInEvent())
				{
					return MainMenu.CautionType.NEW_EVENT;
				}
			}
			else if ((EventManager.Instance.Type == EventManager.EventType.COLLECT_OBJECT || EventManager.Instance.Type == EventManager.EventType.BGM) && !EventManager.Instance.IsInEvent())
			{
				return MainMenu.CautionType.END_EVENT;
			}
		}
		if (SaveDataManager.Instance != null && SaveDataManager.Instance.PlayerData.ChallengeCount == 0U)
		{
			return MainMenu.CautionType.CHALLENGE_COUNT;
		}
		return MainMenu.CautionType.NON;
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x0006E254 File Offset: 0x0006C454
	private void CreateStageCautionWindow()
	{
		this.m_pressedButtonType = MainMenu.PressedButtonType.NONE;
		switch (this.m_cautionType)
		{
		case MainMenu.CautionType.CHALLENGE_COUNT:
			this.m_main_menu_window.CreateWindow(MainMenuWindow.WindowType.ChallengeGoShop, delegate(bool yesButtonClicked)
			{
				this.m_pressedButtonType = ((!yesButtonClicked) ? MainMenu.PressedButtonType.CANCEL : MainMenu.PressedButtonType.GOTO_SHOP);
			});
			break;
		case MainMenu.CautionType.NEW_EVENT:
			this.m_main_menu_window.CreateWindow(MainMenuWindow.WindowType.EventStart, delegate(bool yesButtonClicked)
			{
				this.m_pressedButtonType = MainMenu.PressedButtonType.BACK;
			});
			break;
		case MainMenu.CautionType.END_EVENT:
			this.m_main_menu_window.CreateWindow(MainMenuWindow.WindowType.EventOutOfTime, delegate(bool yesButtonClicked)
			{
				this.m_pressedButtonType = MainMenu.PressedButtonType.BACK;
			});
			break;
		case MainMenu.CautionType.EVENT_LAST_TIME:
			this.m_main_menu_window.CreateWindow(MainMenuWindow.WindowType.EventLastPlay, delegate(bool yesButtonClicked)
			{
				this.m_pressedButtonType = MainMenu.PressedButtonType.NEXT_STATE;
			});
			break;
		}
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x0006E300 File Offset: 0x0006C500
	private void CreateTitleBackWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "BackTitle",
			buttonType = GeneralWindow.ButtonType.YesNo,
			caption = TextUtility.GetCommonText("MainMenu", "back_title_caption"),
			message = TextUtility.GetCommonText("MainMenu", "back_title_text")
		});
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x0006E35C File Offset: 0x0006C55C
	private void CheckTutoralWindow()
	{
		if (GeneralWindow.IsCreated("BackTitle") && GeneralWindow.IsButtonPressed)
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateTitle)), MainMenu.SequenceState.FadeOut);
			}
			GeneralWindow.Close();
		}
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x0006E3AC File Offset: 0x0006C5AC
	private TinyFsmState MenuStateLoadEventResource(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
			this.CeateSceneLoader();
			if (EventManager.Instance != null)
			{
				if (EventManager.Instance.Type == EventManager.EventType.QUICK || EventManager.Instance.Type == EventManager.EventType.BGM)
				{
					this.SetLoadEventResource();
					if (this.m_flags.Test(22) && AtlasManager.Instance != null)
					{
						AtlasManager.Instance.StartLoadAtlasForEventMenu();
					}
				}
				else if (EventManager.Instance.Type == EventManager.EventType.UNKNOWN)
				{
					this.SetLoadTopMenuTexture();
				}
			}
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		case 4:
		{
			bool flag = true;
			if (this.m_buttonEventResourceLoader != null)
			{
				flag = this.m_buttonEventResourceLoader.IsLoaded;
			}
			if (flag && this.CheckSceneLoad())
			{
				this.DestroySceneLoader();
				this.SetEventResources();
				if (EventManager.Instance != null)
				{
					EventManager.EventType type = EventManager.Instance.Type;
					if (type != EventManager.EventType.QUICK)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.MenuStateLoadEventTextureResource)), MainMenu.SequenceState.LoadEventTextureResource);
					}
					else if (this.m_flags.Test(22))
					{
						this.m_flags.Set(22, false);
						StageModeManager.Instance.DrawQuickStageIndex();
						this.ChangeState(new TinyFsmState(new EventFunction(this.MenuStateLoadEventTextureResource)), MainMenu.SequenceState.LoadEventTextureResource);
					}
					else
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
					}
				}
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x0006E590 File Offset: 0x0006C790
	private TinyFsmState MenuStateLoadEventTextureResource(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CeateSceneLoader();
			this.SetLoadTopMenuTexture();
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		case 4:
			if (this.CheckSceneLoad())
			{
				this.DestroySceneLoader();
				this.ChangeState(new TinyFsmState(new EventFunction(this.MenuStateEventDisplayProduction)), MainMenu.SequenceState.EventDisplayProduction);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x0006E62C File Offset: 0x0006C82C
	private TinyFsmState MenuStateEventDisplayProduction(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			bool isFadeIn = false;
			float fadeDuration = 1f;
			float fadeDelay = 0f;
			this.m_flags.Set(1, false);
			CameraFade.StartAlphaFade(Color.black, isFadeIn, fadeDuration, fadeDelay, new Action(this.OnFinishedFadeOutCallback));
			return TinyFsmState.End();
		}
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_flags.Test(1))
			{
				this.m_flags.Set(1, false);
				if (EventManager.Instance != null)
				{
					EventManager.EventType type = EventManager.Instance.Type;
					if (type != EventManager.EventType.QUICK)
					{
						GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnUpdateQuickModeData", null, SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnUpdateQuickModeData", null, SendMessageOptions.DontRequireReceiver);
					}
				}
				float fadeDuration2 = 2f;
				float fadeDelay2 = 0f;
				bool isFadeIn2 = true;
				CameraFade.StartAlphaFade(Color.black, isFadeIn2, fadeDuration2, fadeDelay2, null);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x0006E778 File Offset: 0x0006C978
	private void ServerGetEventReward_Succeeded(MsgGetEventRewardSucceed msg)
	{
		if (this.m_fsm_behavior != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
			this.m_fsm_behavior.Dispatch(signal);
		}
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x0006E7AC File Offset: 0x0006C9AC
	private void ServerGetEventState_Succeeded(MsgGetEventStateSucceed msg)
	{
		if (this.m_fsm_behavior != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(101);
			this.m_fsm_behavior.Dispatch(signal);
		}
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x0006E7E0 File Offset: 0x0006C9E0
	private TinyFsmState StateBestRecordCheckEnableFeed(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
		{
			bool flag = false;
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					flag = systemdata.IsFacebookWindow();
				}
			}
			if (flag)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateBestRecordAskFeed)), MainMenu.SequenceState.BestRecordAskFeed);
			}
			else
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x0006E89C File Offset: 0x0006CA9C
	private TinyFsmState StateBestRecordAskFeed(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				buttonType = GeneralWindow.ButtonType.TweetCancel,
				caption = MileageMapUtility.GetText("gw_highscore_caption", null),
				message = MileageMapUtility.GetText("gw_highscore_text", null)
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsButtonPressed)
			{
				if (GeneralWindow.IsYesButtonPressed)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateBestRecordFeed)), MainMenu.SequenceState.BestRecordFeed);
				}
				else
				{
					SystemSaveManager instance = SystemSaveManager.Instance;
					if (instance != null)
					{
						SystemData systemdata = instance.GetSystemdata();
						if (systemdata != null)
						{
							systemdata.SetFacebookWindow(false);
							instance.SaveSystemData();
						}
					}
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				GeneralWindow.Close();
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x0006E9AC File Offset: 0x0006CBAC
	private TinyFsmState StateBestRecordFeed(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			long highScore = this.m_stageResultData.m_highScore;
			this.m_easySnsFeed = new EasySnsFeed(base.gameObject, "Camera/menu_Anim/MainMenuUI4/Anchor_5_MC", MileageMapUtility.GetText("feed_highscore_caption", null), MileageMapUtility.GetText("feed_highscore_text", new Dictionary<string, string>
			{
				{
					"{HIGHSCORE}",
					highScore.ToString()
				}
			}), null);
			return TinyFsmState.End();
		}
		case 4:
		{
			EasySnsFeed.Result result = this.m_easySnsFeed.Update();
			if (result == EasySnsFeed.Result.COMPLETED || result == EasySnsFeed.Result.FAILED)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x0006EA90 File Offset: 0x0006CC90
	private TinyFsmState StateChaoSelect(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.CHAO_ROULETTE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.ROULETTE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x0006EB74 File Offset: 0x0006CD74
	private TinyFsmState StateMainCharaSelect(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.CHAO_ROULETTE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.ROULETTE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.CHAO)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateChaoSelect)), MainMenu.SequenceState.ChaoSelect);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x0006EC84 File Offset: 0x0006CE84
	private TinyFsmState StateDailyBattle(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null && msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x0006ED10 File Offset: 0x0006CF10
	private TinyFsmState StateDailyBattleRewardWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("DailybattleRewardWindowUI", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			return TinyFsmState.End();
		case 4:
			if (this.m_buttonEventResourceLoader.IsLoaded)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateDailyBattleRewardWindowDisplay)), MainMenu.SequenceState.DailyBattleRewardWindowDisplay);
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x0006EDC4 File Offset: 0x0006CFC4
	private TinyFsmState StateDailyBattleRewardWindowDisplay(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			this.m_dailyBattleRewardWindow = null;
			return TinyFsmState.End();
		case 1:
			if (SingletonGameObject<DailyBattleManager>.Instance != null)
			{
				this.m_dailyBattleRewardWindow = DailyBattleRewardWindow.Open(SingletonGameObject<DailyBattleManager>.Instance.GetRewardDataPair(false));
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_dailyBattleRewardWindow != null && this.m_dailyBattleRewardWindow.IsEnd)
			{
				DailyBattleManager instance = SingletonGameObject<DailyBattleManager>.Instance;
				if (instance != null)
				{
					instance.RestReward();
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x0006EE90 File Offset: 0x0006D090
	private TinyFsmState StateInfomation(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.ROULETTE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.CHAO_ROULETTE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.SHOP)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateShop)), MainMenu.SequenceState.Shop);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x0006EFA0 File Offset: 0x0006D1A0
	private TinyFsmState StateDailyMissionWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_flags.Set(16, false);
			return TinyFsmState.End();
		case 4:
			if (this.m_dailyWindowUI == null)
			{
				GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
				if (menuAnimUIObject != null)
				{
					this.m_dailyWindowUI = GameObjectUtil.FindChildGameObjectComponent<DailyWindowUI>(menuAnimUIObject, "DailyWindowUI");
					if (this.m_dailyWindowUI != null)
					{
						this.m_dailyWindowUI.gameObject.SetActive(true);
						this.m_dailyWindowUI.PlayStart();
					}
				}
			}
			else if (this.m_dailyWindowUI.IsEnd)
			{
				this.m_dailyWindowUI = null;
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.DAILY_CHALLENGE_BACK, false);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x0006F0A4 File Offset: 0x0006D2A4
	private TinyFsmState StateEnd(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.ResetRingCountOffset();
			this.SetEventManagerParam();
			ChaoTextureManager.Instance.RemoveChaoTextureForMainMenuEnd();
			AtlasManager.Instance.ClearAllAtlas();
			this.CeateSceneLoader();
			if (StageModeManager.Instance.StageMode == StageModeManager.Mode.ENDLESS)
			{
				this.LoadMileageText();
				this.m_request_face_list.Clear();
				this.m_request_bg_list.Clear();
				this.EntryMileageTexturesList();
				this.LoadMileageTextures();
			}
			return TinyFsmState.End();
		case 4:
		{
			CPlusPlusLink instance = CPlusPlusLink.Instance;
			if (instance != null)
			{
				instance.BeforeGameCheatCheck();
			}
			if (this.CheckSceneLoad())
			{
				this.SetupMileageText();
				this.TransTextureObj();
				if (this.m_flags.Test(4))
				{
					this.DestroySceneLoader();
					EventUtility.SetDontDestroyLoadingFaceTexture();
					this.EndSpecialStageProcessing();
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)), MainMenu.SequenceState.End);
				}
				else if (this.m_flags.Test(5))
				{
					this.DestroySceneLoader();
					EventUtility.SetDontDestroyLoadingFaceTexture();
					this.EndRaidBossProcessing();
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)), MainMenu.SequenceState.End);
				}
				else if (this.m_flags.Test(3))
				{
					this.EndStageProcessing();
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)), MainMenu.SequenceState.End);
				}
				else
				{
					this.EndTitleProcessing();
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)), MainMenu.SequenceState.End);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x0006F250 File Offset: 0x0006D450
	private TinyFsmState StateIdle(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x0006F29C File Offset: 0x0006D49C
	private void EndStageProcessing()
	{
		if (this.m_flags.Test(15))
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				instance.PlayerData.MainChaoID = -1;
				instance.PlayerData.SubChaoID = -1;
				instance.PlayerData.MainChara = CharaType.SONIC;
			}
			this.SetTutorialStageInfo();
			this.SetTutorialLoadingInfo();
		}
		else
		{
			this.SetStageInfo();
			this.SetLoadingInfo();
			this.DestroyMileageInfo();
		}
		this.PrepareForSceneMove();
		TimeProfiler.StartCountTime("MainMenu-GameModeStage");
		Application.LoadLevel("s_playingstage");
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x0006F330 File Offset: 0x0006D530
	private void EndSpecialStageProcessing()
	{
		this.SetSpecialStageInfo();
		this.SetEventLoadingInfo();
		this.PrepareForSceneMove();
		TimeProfiler.StartCountTime("MainMenu-GameModeStage");
		Application.LoadLevel("s_playingstage");
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x0006F364 File Offset: 0x0006D564
	private void EndRaidBossProcessing()
	{
		this.SetRaidBossInfo();
		this.SetEventLoadingInfo();
		this.PrepareForSceneMove();
		TimeProfiler.StartCountTime("MainMenu-GameModeStage");
		Application.LoadLevel("s_playingstage");
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x0006F398 File Offset: 0x0006D598
	private void EndTitleProcessing()
	{
		if (this.m_stage_info_obj != null)
		{
			UnityEngine.Object.Destroy(this.m_stage_info_obj);
		}
		if (MileageMapDataManager.Instance != null)
		{
			UnityEngine.Object.Destroy(MileageMapDataManager.Instance.gameObject);
		}
		this.PrepareForSceneMove();
		HudMenuUtility.GoToTitleScene();
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x0006F3EC File Offset: 0x0006D5EC
	private StageInfo.MileageMapInfo CreateMileageInfo()
	{
		StageInfo.MileageMapInfo mileageMapInfo = new StageInfo.MileageMapInfo();
		if (this.m_mileage_map_state != null)
		{
			mileageMapInfo.m_mapState.m_episode = this.m_mileage_map_state.m_episode;
			mileageMapInfo.m_mapState.m_chapter = this.m_mileage_map_state.m_chapter;
			mileageMapInfo.m_mapState.m_point = this.m_mileage_map_state.m_point;
			mileageMapInfo.m_mapState.m_score = this.m_mileage_map_state.m_stageTotalScore;
		}
		else
		{
			mileageMapInfo.m_mapState.m_episode = 1;
			mileageMapInfo.m_mapState.m_chapter = 1;
			mileageMapInfo.m_mapState.m_point = 0;
			mileageMapInfo.m_mapState.m_score = 0L;
		}
		if (mileageMapInfo.m_pointScore != null)
		{
			int num = mileageMapInfo.m_pointScore.Length;
			long num2 = (long)MileageMapUtility.GetPointInterval();
			for (int i = 0; i < num; i++)
			{
				mileageMapInfo.m_pointScore[i] = num2 * (long)i;
			}
		}
		return mileageMapInfo;
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x0006F4D4 File Offset: 0x0006D6D4
	private void SetBoostItemValidFlag(StageInfo stageInfo)
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance != null)
		{
			bool[] boostItemValidFlag = instance.BoostItemValidFlag;
			if (boostItemValidFlag != null)
			{
				for (int i = 0; i < 3; i++)
				{
					boostItemValidFlag[i] = stageInfo.BoostItemValid[i];
				}
			}
		}
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x0006F520 File Offset: 0x0006D720
	private void ResetRingCountOffset()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			instance.ItemData.RingCountOffset = 0;
		}
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x0006F54C File Offset: 0x0006D74C
	private void PrepareForSceneMove()
	{
		if (InformationImageManager.Instance != null)
		{
			InformationImageManager.Instance.ResetImage();
		}
		AtlasManager.Instance.ResetReplaceAtlas();
		MileageMapText.DestroyPreEPisodeText();
		ResourceManager.Instance.RemoveResourcesOnThisScene();
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x0006F598 File Offset: 0x0006D798
	private void SetStageInfo()
	{
		if (this.m_stage_info_obj != null)
		{
			StageInfo component = this.m_stage_info_obj.GetComponent<StageInfo>();
			if (component != null)
			{
				component.MileageInfo = this.CreateMileageInfo();
				int point_type = 0;
				int numBossAttack = 0;
				if (this.m_mileage_map_state != null)
				{
					point_type = this.m_mileage_map_state.m_point;
					numBossAttack = this.m_mileage_map_state.m_numBossAttack;
				}
				if (StageModeManager.Instance.IsQuickMode())
				{
					if (EventManager.Instance != null && EventManager.Instance.Type == EventManager.EventType.QUICK)
					{
						int index = 1;
						TenseType tenseType = TenseType.AFTERNOON;
						EventStageData stageData = EventManager.Instance.GetStageData();
						if (stageData != null)
						{
							index = MileageMapUtility.GetStageIndex(stageData.stage_key);
							tenseType = MileageMapUtility.GetTenseType(stageData.stage_key);
						}
						component.SelectedStageName = StageInfo.GetStageNameByIndex(index);
						component.TenseType = tenseType;
						component.NotChangeTense = true;
					}
					else
					{
						switch (StageModeManager.Instance.QuickStageCharaAttribute)
						{
						case CharacterAttribute.SPEED:
							component.SelectedStageName = StageInfo.GetStageNameByIndex(1);
							break;
						case CharacterAttribute.FLY:
							component.SelectedStageName = StageInfo.GetStageNameByIndex(2);
							break;
						case CharacterAttribute.POWER:
							component.SelectedStageName = StageInfo.GetStageNameByIndex(3);
							break;
						default:
							component.SelectedStageName = StageInfo.GetStageNameByIndex(1);
							break;
						}
						component.TenseType = TenseType.AFTERNOON;
						component.NotChangeTense = true;
					}
					component.ExistBoss = false;
					component.BossStage = false;
					component.QuickMode = true;
					component.FromTitle = false;
					component.BossType = BossType.NONE;
					component.NumBossAttack = 0;
				}
				else
				{
					component.SelectedStageName = MileageMapUtility.GetMileageStageName();
					component.TenseType = MileageMapUtility.GetTenseType((PointType)point_type);
					component.NotChangeTense = !MileageMapUtility.GetChangeTense((PointType)point_type);
					component.ExistBoss = MileageMapUtility.IsExistBoss();
					component.BossStage = this.BossChallenge;
					component.QuickMode = false;
					component.FromTitle = false;
					component.BossType = MileageMapUtility.GetBossType();
					component.NumBossAttack = numBossAttack;
				}
				if (this.m_flags.Test(15))
				{
					component.TutorialStage = true;
				}
				if (!component.TutorialStage && EventManager.Instance != null)
				{
					if (EventManager.Instance.Type == EventManager.EventType.COLLECT_OBJECT && EventManager.Instance.IsInEvent())
					{
						this.SetEventStage(true);
					}
					component.EventStage = EventManager.Instance.EventStage;
				}
				this.SetBoostItemValidFlag(component);
			}
		}
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x0006F7FC File Offset: 0x0006D9FC
	private void SetSpecialStageInfo()
	{
		if (this.m_stage_info_obj != null)
		{
			StageInfo component = this.m_stage_info_obj.GetComponent<StageInfo>();
			if (component != null)
			{
				EventStageData stageData = EventManager.Instance.GetStageData();
				if (stageData != null)
				{
					string stage_key = stageData.stage_key;
					component.SelectedStageName = MileageMapUtility.GetEventStageName(stage_key);
					component.TenseType = MileageMapUtility.GetTenseType(stage_key);
					component.NotChangeTense = !MileageMapUtility.GetChangeTense(stage_key);
					component.ExistBoss = false;
					component.BossStage = false;
					component.TutorialStage = false;
				}
				if (EventManager.Instance != null)
				{
					component.EventStage = EventManager.Instance.EventStage;
				}
				component.MileageInfo = this.CreateMileageInfo();
				this.SetBoostItemValidFlag(component);
				component.FromTitle = false;
			}
		}
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x0006F8C0 File Offset: 0x0006DAC0
	private void SetRaidBossInfo()
	{
		if (this.m_stage_info_obj != null)
		{
			StageInfo component = this.m_stage_info_obj.GetComponent<StageInfo>();
			if (component != null)
			{
				EventStageData stageData = EventManager.Instance.GetStageData();
				if (stageData != null)
				{
					string stage_key = stageData.stage_key;
					component.SelectedStageName = MileageMapUtility.GetEventStageName(stage_key);
					component.TenseType = MileageMapUtility.GetTenseType(stage_key);
					component.NotChangeTense = !MileageMapUtility.GetChangeTense(stage_key);
					component.ExistBoss = true;
					component.BossStage = true;
					component.TutorialStage = false;
					if (RaidBossInfo.currentRaidData != null)
					{
						component.NumBossAttack = (int)(RaidBossInfo.currentRaidData.hpMax - RaidBossInfo.currentRaidData.hp);
						switch (RaidBossInfo.currentRaidData.rarity)
						{
						case 0:
							component.BossType = BossType.EVENT1;
							break;
						case 1:
							component.BossType = BossType.EVENT2;
							break;
						case 2:
							component.BossType = BossType.EVENT3;
							break;
						default:
							component.BossType = BossType.EVENT1;
							break;
						}
					}
					else
					{
						component.NumBossAttack = 0;
						component.BossType = BossType.EVENT1;
					}
				}
				if (EventManager.Instance != null)
				{
					component.EventStage = EventManager.Instance.EventStage;
				}
				component.MileageInfo = this.CreateMileageInfo();
				this.SetBoostItemValidFlag(component);
				component.FromTitle = false;
			}
		}
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x0006FA10 File Offset: 0x0006DC10
	private void DestroyMileageInfo()
	{
		if (MileageMapDataManager.Instance != null)
		{
			MileageMapDataManager.Instance.DestroyData();
		}
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x0006FA2C File Offset: 0x0006DC2C
	private void SetLoadingInfo()
	{
		LoadingInfo loadingInfo = LoadingInfo.CreateLoadingInfo();
		if (loadingInfo != null)
		{
			if (StageModeManager.Instance.IsQuickMode())
			{
				LoadingInfo.LoadingData info = loadingInfo.GetInfo();
				if (info != null)
				{
					UnityEngine.Random.seed = NetUtil.GetCurrentUnixTime();
					int num = UnityEngine.Random.Range(1, 13);
					info.m_titleText = TextUtility.GetCommonText("quick", "loading_title");
					if (num == 1)
					{
						info.m_mainText = TextUtility.GetCommonText("quick", "loading_text");
					}
					else
					{
						info.m_mainText = TextUtility.GetCommonText("quick", "loading_text" + num.ToString());
					}
				}
			}
			else if (MileageMapDataManager.Instance != null)
			{
				int episode = 1;
				int chapter = 1;
				if (this.m_mileage_map_state != null)
				{
					episode = this.m_mileage_map_state.m_episode;
					chapter = this.m_mileage_map_state.m_chapter;
				}
				MileageMapData mileageMapData = MileageMapDataManager.Instance.GetMileageMapData(episode, chapter);
				if (mileageMapData != null)
				{
					LoadingInfo.LoadingData info2 = loadingInfo.GetInfo();
					if (info2 != null)
					{
						int num2 = (!this.IsBossLoading()) ? mileageMapData.loading.window_id : mileageMapData.loading.boss_window_id;
						if (num2 < mileageMapData.window_data.Length)
						{
							info2.m_titleText = MileageMapText.GetText(mileageMapData.scenario.episode, mileageMapData.scenario.title_cell_id);
							info2.m_mainText = MileageMapText.GetText(mileageMapData.scenario.episode, mileageMapData.window_data[num2].body[0].text_cell_id);
							int face_id = mileageMapData.window_data[num2].body[0].product[0].face_id;
							info2.m_texture = MileageMapUtility.GetFaceTexture(face_id);
						}
					}
				}
			}
		}
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x0006FBF8 File Offset: 0x0006DDF8
	private void SetEventLoadingInfo()
	{
		LoadingInfo loadingInfo = LoadingInfo.CreateLoadingInfo();
		if (loadingInfo != null)
		{
			LoadingInfo.LoadingData info = loadingInfo.GetInfo();
			if (info != null && EventManager.Instance != null)
			{
				WindowEventData loadingEventData = EventUtility.GetLoadingEventData();
				if (loadingEventData != null)
				{
					TextManager.TextType type = TextManager.TextType.TEXTTYPE_EVENT_SPECIFIC;
					info.m_titleText = TextUtility.GetText(type, "Production", loadingEventData.title_cell_id);
					info.m_mainText = TextUtility.GetText(type, "Production", loadingEventData.body[0].text_cell_id);
					info.m_texture = EventUtility.GetLoadingFaceTexture();
				}
			}
		}
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x0006FC84 File Offset: 0x0006DE84
	private bool IsBossLoading()
	{
		return this.m_mileage_map_state != null && MileageMapUtility.IsExistBoss() && this.BossChallenge;
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x0006FCA4 File Offset: 0x0006DEA4
	private void SetTutorialStageInfo()
	{
		if (this.m_stage_info_obj != null)
		{
			StageInfo component = this.m_stage_info_obj.GetComponent<StageInfo>();
			if (component != null)
			{
				StageInfo.MileageMapInfo mileageMapInfo = new StageInfo.MileageMapInfo();
				mileageMapInfo.m_mapState.m_episode = 1;
				mileageMapInfo.m_mapState.m_chapter = 1;
				mileageMapInfo.m_mapState.m_point = 0;
				mileageMapInfo.m_mapState.m_score = 0L;
				component.SelectedStageName = StageInfo.GetStageNameByIndex(1);
				component.TenseType = TenseType.AFTERNOON;
				component.ExistBoss = false;
				component.BossStage = false;
				component.TutorialStage = true;
				StageAbilityManager instance = StageAbilityManager.Instance;
				if (instance != null)
				{
					bool[] boostItemValidFlag = instance.BoostItemValidFlag;
					if (boostItemValidFlag != null)
					{
						for (int i = 0; i < 3; i++)
						{
							boostItemValidFlag[i] = false;
						}
					}
				}
				component.FromTitle = false;
				component.MileageInfo = mileageMapInfo;
			}
		}
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x0006FD84 File Offset: 0x0006DF84
	private void SetTutorialLoadingInfo()
	{
		LoadingInfo loadingInfo = LoadingInfo.CreateLoadingInfo();
		if (loadingInfo != null)
		{
			LoadingInfo.LoadingData info = loadingInfo.GetInfo();
			if (info != null)
			{
				string cellID = CharaName.Name[0];
				string commonText = TextUtility.GetCommonText("CharaName", cellID);
				info.m_titleText = TextUtility.GetCommonText("Option", "chara_operation_method", "{CHARA_NAME}", commonText);
				info.m_mainText = TextUtility.GetCommonText("Option", "sonic_operation_comment");
				info.m_optionTutorial = true;
				int face_id = 1;
				info.m_texture = MileageMapUtility.GetFaceTexture(face_id);
			}
		}
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x0006FE0C File Offset: 0x0006E00C
	private void SetEventManagerParam()
	{
		if (EventManager.Instance != null)
		{
			if (this.m_flags.Test(4) || this.m_flags.Test(5))
			{
				EventManager.Instance.EventStage = true;
				EventManager.Instance.ReCalcEndPlayTime();
			}
			else
			{
				EventManager.Instance.EventStage = false;
			}
		}
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x0006FE70 File Offset: 0x0006E070
	private TinyFsmState StateLoadMileageXml(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (!this.m_episodeLoaded)
			{
				this.CeateSceneLoader();
				if (!this.IsExistMileageMapData(this.m_mileage_map_state))
				{
					this.AddSceneLoader(this.GetMileageMapDataScenaName(this.m_mileage_map_state));
				}
				if (GameObject.Find("MileageDataTable") == null)
				{
					this.AddSceneLoader("MileageDataTable");
				}
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_episodeLoaded)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMileageReward)), MainMenu.SequenceState.MileageReward);
			}
			else if (this.CheckSceneLoad())
			{
				this.m_episodeLoaded = true;
				this.DestroySceneLoader();
				this.SetupMileageDataTable();
				if (this.m_flags.Test(6))
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadNextMileageXml)), MainMenu.SequenceState.LoadNextMileageXml);
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadMileageTexture)), MainMenu.SequenceState.LoadMileageTexture);
				}
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x0006FFAC File Offset: 0x0006E1AC
	private TinyFsmState StateLoadNextMileageXml(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CeateSceneLoader();
			if (!this.IsExistMileageMapData(this.m_prev_mileage_map_state))
			{
				this.AddSceneLoader(this.GetMileageMapDataScenaName(this.m_prev_mileage_map_state));
			}
			return TinyFsmState.End();
		case 4:
			if (this.CheckSceneLoad())
			{
				this.DestroySceneLoader();
				MileageMapDataManager instance = MileageMapDataManager.Instance;
				if (instance != null)
				{
					instance.SetCurrentData(this.m_prev_mileage_map_state.m_episode, this.m_prev_mileage_map_state.m_chapter);
				}
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadMileageTexture)), MainMenu.SequenceState.LoadMileageTexture);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x00070084 File Offset: 0x0006E284
	private TinyFsmState StateLoadMileageTexture(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			MileageMapDataManager instance = MileageMapDataManager.Instance;
			if (instance != null)
			{
				this.m_request_face_list.Clear();
				this.m_request_bg_list.Clear();
				this.EntryMileageTexturesList();
				this.CeateSceneLoader();
				this.LoadMileageText();
				this.LoadMileageTextures();
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.CheckSceneLoad())
			{
				this.SetIncentive();
				this.SetupMileageText();
				this.TransTextureObj();
				this.DestroySceneLoader();
				Resources.UnloadUnusedAssets();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMileageReward)), MainMenu.SequenceState.MileageReward);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x0007015C File Offset: 0x0006E35C
	private TinyFsmState StateMileageReward(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (ServerInterface.LoggedInServerInterface != null)
			{
				ServerMileageMapState mileageMapState = ServerInterface.MileageMapState;
				List<ServerMileageReward> mileageRewardList = ServerInterface.MileageRewardList;
				foreach (ServerMileageReward serverMileageReward in mileageRewardList)
				{
					if (serverMileageReward.m_episode == mileageMapState.m_episode && serverMileageReward.m_episode == mileageMapState.m_chapter)
					{
						this.ServerGetMileageReward_Succeeded(null);
						break;
					}
				}
				if (!this.m_flags.Test(12))
				{
					ServerInterface.LoggedInServerInterface.RequestServerGetMileageReward(mileageMapState.m_episode, mileageMapState.m_chapter, base.gameObject);
				}
			}
			else
			{
				this.ServerGetMileageReward_Succeeded(null);
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_flags.Test(12))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitFadeIfNotEndFade)), MainMenu.SequenceState.WaitFadeIfNotEndFade);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000702B0 File Offset: 0x0006E4B0
	private TinyFsmState StateWaitFadeIfNotEndFade(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (!this.m_flags.Test(0))
			{
				ConnectAlertMaskUI.EndScreen(new Action(this.OnFinishedFadeInCallback));
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_flags.Test(0))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateEpisode)), MainMenu.SequenceState.Episode);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x00070354 File Offset: 0x0006E554
	private TinyFsmState StateEpisode(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_flags.Set(28, true);
			if (this.m_flags.Test(7))
			{
				this.m_flags.Set(7, false);
				HudMenuUtility.SendMsgPrepareMileageMapProduction(this.m_stageResultData);
			}
			else
			{
				HudMenuUtility.SendMsgUpdateMileageMapDisplayToMileage();
			}
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.PLAY_AT_EPISODE_PAGE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.MenuStatePlayButton)), MainMenu.SequenceState.PlayButton);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.SHOP)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateShop)), MainMenu.SequenceState.Shop);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x0007047C File Offset: 0x0006E67C
	private void TransTextureObj()
	{
		if (MileageMapDataManager.Instance != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(MileageMapDataManager.Instance.gameObject, "MileageMapBG");
			if (gameObject != null && MileageMapBGDataTable.Instance != null)
			{
				foreach (int id in this.m_request_bg_list)
				{
					GameObject gameObject2 = GameObject.Find(MileageMapBGDataTable.Instance.GetTextureName(id));
					if (gameObject2 != null)
					{
						gameObject2.transform.parent = gameObject.transform;
					}
				}
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(MileageMapDataManager.Instance.gameObject, "MileageMapFace");
			if (gameObject3 != null)
			{
				foreach (int face_id in this.m_request_face_list)
				{
					GameObject gameObject4 = GameObject.Find(MileageMapUtility.GetFaceTextureName(face_id));
					if (gameObject4 != null)
					{
						gameObject4.transform.parent = gameObject3.transform;
					}
				}
			}
		}
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000705EC File Offset: 0x0006E7EC
	private void EntryMileageTexturesList()
	{
		MileageMapDataManager instance = MileageMapDataManager.Instance;
		MileageMapData mileageMapData = null;
		MileageMapData mileageMapData2 = null;
		if (instance != null)
		{
			mileageMapData2 = instance.GetMileageMapData(this.m_mileage_map_state.m_episode, this.m_mileage_map_state.m_chapter);
			if (this.m_flags.Test(6))
			{
				mileageMapData = instance.GetMileageMapData(this.m_prev_mileage_map_state.m_episode, this.m_prev_mileage_map_state.m_chapter);
			}
		}
		if (mileageMapData2 != null)
		{
			bool keep = true;
			bool keep2 = false;
			int bg_id = mileageMapData2.map_data.bg_id;
			this.AddIDList(ref this.m_request_bg_list, bg_id, "bg", keep);
			if (mileageMapData != null)
			{
				bg_id = mileageMapData.map_data.bg_id;
				this.AddIDList(ref this.m_request_bg_list, bg_id, "bg", keep2);
			}
			List<int> list = new List<int>();
			if (this.m_mileage_map_state.m_point == 5 && mileageMapData2.event_data.IsBossEvent())
			{
				int boss_window_id = mileageMapData2.loading.boss_window_id;
				this.SetLoadingFaceTexture(ref this.m_request_face_list, ref list, mileageMapData2, boss_window_id);
			}
			int window_id = mileageMapData2.loading.window_id;
			this.SetLoadingFaceTexture(ref this.m_request_face_list, ref list, mileageMapData2, window_id);
			int num = 1;
			if (!this.m_request_face_list.Contains(num))
			{
				this.AddIDList(ref this.m_request_face_list, num, "face", keep);
				list.Add(num);
			}
			instance.SetLoadingFaceId(list);
			if (this.m_flags.Test(6))
			{
				int num2 = mileageMapData.event_data.point.Length;
				for (int i = 0; i < num2; i++)
				{
					if (this.m_prev_mileage_map_state.m_point <= i)
					{
						if (i != 5 || !mileageMapData.event_data.IsBossEvent())
						{
							int balloon_face_id = mileageMapData.event_data.point[i].balloon_face_id;
							int balloon_on_arrival_face_id = mileageMapData.event_data.point[i].balloon_on_arrival_face_id;
							this.AddIDList(ref this.m_request_face_list, balloon_face_id, "face", keep2);
							this.AddIDList(ref this.m_request_face_list, balloon_on_arrival_face_id, "face", keep2);
						}
						else
						{
							BossEvent bossEvent = mileageMapData.event_data.GetBossEvent();
							this.AddIDList(ref this.m_request_face_list, bossEvent.balloon_on_arrival_face_id, "face", keep2);
							this.AddIDList(ref this.m_request_face_list, bossEvent.balloon_clear_face_id, "face", keep2);
						}
					}
				}
				num2 = mileageMapData2.event_data.point.Length;
				for (int j = 0; j < num2; j++)
				{
					if (this.m_mileage_map_state.m_point <= j)
					{
						if (j != 5 || !mileageMapData2.event_data.IsBossEvent())
						{
							int balloon_face_id2 = mileageMapData2.event_data.point[j].balloon_face_id;
							this.AddIDList(ref this.m_request_face_list, balloon_face_id2, "face", keep);
						}
						else
						{
							BossEvent bossEvent2 = mileageMapData2.event_data.GetBossEvent();
							this.AddIDList(ref this.m_request_face_list, bossEvent2.balloon_init_face_id, "face", keep);
						}
					}
				}
				this.SetLoadWindowFaceTexture(ref this.m_request_face_list, mileageMapData, this.m_prev_mileage_map_state);
				this.SetLoadWindowFaceTexture(ref this.m_request_face_list, mileageMapData2, this.m_mileage_map_state);
			}
			else
			{
				int num3 = mileageMapData2.event_data.point.Length;
				for (int k = 0; k < num3; k++)
				{
					if (this.m_prev_mileage_map_state.m_point <= k)
					{
						if (k != 5 || !mileageMapData2.event_data.IsBossEvent())
						{
							int balloon_face_id3 = mileageMapData2.event_data.point[k].balloon_face_id;
							int balloon_on_arrival_face_id2 = mileageMapData2.event_data.point[k].balloon_on_arrival_face_id;
							this.AddIDList(ref this.m_request_face_list, balloon_face_id3, "face", keep);
							this.AddIDList(ref this.m_request_face_list, balloon_on_arrival_face_id2, "face", keep2);
						}
						else
						{
							BossEvent bossEvent3 = mileageMapData2.event_data.GetBossEvent();
							this.AddIDList(ref this.m_request_face_list, bossEvent3.balloon_init_face_id, "face", keep);
							this.AddIDList(ref this.m_request_face_list, bossEvent3.balloon_on_arrival_face_id, "face", keep);
							this.AddIDList(ref this.m_request_face_list, bossEvent3.balloon_clear_face_id, "face", keep2);
						}
					}
				}
				if (this.m_mileage_map_state.m_episode == 1 || this.m_flags.Test(7))
				{
					this.SetLoadWindowFaceTexture(ref this.m_request_face_list, mileageMapData2, null);
				}
			}
		}
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x00070A48 File Offset: 0x0006EC48
	private void LoadEventProductTexture()
	{
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x00070A4C File Offset: 0x0006EC4C
	private void LoadMileageTextures()
	{
		if (MileageMapDataManager.Instance != null)
		{
			foreach (int id in this.m_request_bg_list)
			{
				string textureName = MileageMapBGDataTable.Instance.GetTextureName(id);
				if (GameObjectUtil.FindChildGameObject(MileageMapDataManager.Instance.gameObject, textureName) == null)
				{
					this.AddSceneLoader(textureName);
				}
			}
			foreach (int face_id in this.m_request_face_list)
			{
				string faceTextureName = MileageMapUtility.GetFaceTextureName(face_id);
				if (GameObjectUtil.FindChildGameObject(MileageMapDataManager.Instance.gameObject, faceTextureName) == null)
				{
					this.AddSceneLoader(faceTextureName);
				}
			}
		}
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x00070B68 File Offset: 0x0006ED68
	private void LoadMileageText()
	{
		int episode = 1;
		int pre_episode = -1;
		this.GetMileageTextParam(ref episode, ref pre_episode);
		if (this.m_scene_loader_obj != null)
		{
			MileageMapText.Load(this.m_scene_loader_obj.GetComponent<ResourceSceneLoader>(), episode, pre_episode);
		}
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x00070BA8 File Offset: 0x0006EDA8
	private void GetMileageTextParam(ref int episode, ref int pre_episode)
	{
		if (this.m_mileage_map_state != null)
		{
			episode = this.m_mileage_map_state.m_episode;
		}
		if (this.m_stageResultData != null && this.m_stageResultData.m_oldMapState != null)
		{
			pre_episode = this.m_stageResultData.m_oldMapState.m_episode;
		}
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x00070BFC File Offset: 0x0006EDFC
	public void SetupMileageText()
	{
		MileageMapText.Setup();
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x00070C04 File Offset: 0x0006EE04
	public void SetIncentive()
	{
		if (this.m_stageResultData != null && this.m_stageResultData.m_mileageIncentiveList != null)
		{
			MileageMapDataManager instance = MileageMapDataManager.Instance;
			if (instance != null)
			{
				int episode = this.m_mileage_map_state.m_episode;
				int chapter = this.m_mileage_map_state.m_chapter;
				int num = 0;
				int num2 = 0;
				foreach (ServerMileageIncentive serverMileageIncentive in this.m_stageResultData.m_mileageIncentiveList)
				{
					RewardData src_reward = new RewardData(serverMileageIncentive.m_itemId, serverMileageIncentive.m_num);
					if (serverMileageIncentive.m_type == ServerMileageIncentive.Type.POINT)
					{
						if (this.m_stageResultData != null && this.m_stageResultData.m_oldMapState != null && serverMileageIncentive.m_pointId > this.m_stageResultData.m_oldMapState.m_point)
						{
							episode = this.m_stageResultData.m_oldMapState.m_episode;
							chapter = this.m_stageResultData.m_oldMapState.m_chapter;
						}
						instance.SetPointIncentiveData(episode, chapter, serverMileageIncentive.m_pointId, src_reward);
					}
					else if (serverMileageIncentive.m_type == ServerMileageIncentive.Type.CHAPTER)
					{
						if (this.m_stageResultData != null && this.m_stageResultData.m_oldMapState != null)
						{
							episode = this.m_stageResultData.m_oldMapState.m_episode;
							chapter = this.m_stageResultData.m_oldMapState.m_chapter;
						}
						instance.SetChapterIncentiveData(episode, chapter, num, src_reward);
						num++;
					}
					else if (serverMileageIncentive.m_type == ServerMileageIncentive.Type.EPISODE)
					{
						if (this.m_stageResultData != null && this.m_stageResultData.m_oldMapState != null)
						{
							episode = this.m_stageResultData.m_oldMapState.m_episode;
							chapter = this.m_stageResultData.m_oldMapState.m_chapter;
						}
						instance.SetEpisodeIncentiveData(episode, chapter, num2, src_reward);
						num2++;
					}
				}
			}
		}
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x00070E00 File Offset: 0x0006F000
	private void SetupMileageDataTable()
	{
		if (GameObject.Find("MileageDataTable") == null)
		{
			GameObject gameObject = new GameObject("MileageDataTable");
			GameObject gameObject2 = GameObject.Find("BGDataTable");
			if (gameObject2 != null)
			{
				gameObject2.transform.parent = gameObject.gameObject.transform;
			}
			GameObject gameObject3 = GameObject.Find("PointDataTable");
			if (gameObject3 != null)
			{
				gameObject3.transform.parent = gameObject.gameObject.transform;
			}
			GameObject gameObject4 = GameObject.Find("RouteDataTable");
			if (gameObject4 != null)
			{
				gameObject4.transform.parent = gameObject.gameObject.transform;
			}
			GameObject gameObject5 = GameObject.Find("StageSuggestedDataTable");
			if (gameObject5 != null)
			{
				gameObject5.transform.parent = gameObject.gameObject.transform;
			}
			GameObject gameObject6 = GameObject.Find("MileageMapDataManager");
			if (gameObject6 != null)
			{
				gameObject.transform.parent = gameObject6.transform;
			}
		}
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x00070F10 File Offset: 0x0006F110
	private bool IsExistMileageMapData(ServerMileageMapState state)
	{
		MileageMapDataManager instance = MileageMapDataManager.Instance;
		return instance != null && instance.IsExist(state.m_episode, state.m_chapter);
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x00070F44 File Offset: 0x0006F144
	private TinyFsmState StateFadeIn(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.StartScene();
			return TinyFsmState.End();
		case 1:
		{
			TimeProfiler.StartCountTime("StateFadeIn");
			if (this.m_flags.Test(7))
			{
				MileageMapUtility.SetDisplayOffset_FromResultData(this.m_stageResultData);
			}
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			HudMenuUtility.SendStartMainMenuDlsplay();
			HudMenuUtility.SendMsgTickerUpdate();
			this.m_ButtonOfNextMenu = ButtonInfoTable.ButtonType.UNKNOWN;
			bool stageResultData = this.m_stageResultData != null;
			if (stageResultData && !this.m_stageResultData.m_quickMode)
			{
				if (this.m_stageResultData.m_fromOptionTutorial)
				{
					this.m_ButtonOfNextMenu = ButtonInfoTable.ButtonType.OPTION;
				}
				else
				{
					this.m_ButtonOfNextMenu = ButtonInfoTable.ButtonType.EPISODE;
				}
			}
			if (this.m_ButtonOfNextMenu == ButtonInfoTable.ButtonType.UNKNOWN)
			{
				ConnectAlertMaskUI.EndScreen(new Action(this.OnFinishedFadeInCallback));
			}
			else
			{
				this.OnFinishedFadeInCallback();
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_flags.Test(0))
			{
				TimeProfiler.EndCountTime("StateFadeIn");
				if (this.m_ButtonOfNextMenu == ButtonInfoTable.ButtonType.UNKNOWN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				else
				{
					HudMenuUtility.SendMenuButtonClicked(this.m_ButtonOfNextMenu, false);
					this.m_flags.Set(0, false);
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenu)), MainMenu.SequenceState.Main);
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x000710C0 File Offset: 0x0006F2C0
	private void OnFinishedFadeInCallback()
	{
		this.m_flags.Set(0, true);
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x000710D0 File Offset: 0x0006F2D0
	private TinyFsmState StateFadeOut(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			BackKeyManager.EndScene();
			bool isFadeIn = false;
			float fadeDuration = 1f;
			float fadeDelay = 0f;
			this.m_flags.Set(1, false);
			CameraFade.StartAlphaFade(Color.black, isFadeIn, fadeDuration, fadeDelay, new Action(this.OnFinishedFadeOutCallback));
			SoundManager.BgmFadeOut(0.5f);
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_flags.Test(1))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)), MainMenu.SequenceState.End);
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x0007118C File Offset: 0x0006F38C
	private void OnFinishedFadeOutCallback()
	{
		this.m_flags.Set(1, true);
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x0007119C File Offset: 0x0006F39C
	private TinyFsmState StateInformationWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync(ButtonInfoTable.PageType.INFOMATION, new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			return TinyFsmState.End();
		case 4:
			if (this.m_buttonEventResourceLoader.IsLoaded)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateInformationWindowCreate)), MainMenu.SequenceState.InformationWindowCreate);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x00071258 File Offset: 0x0006F458
	private TinyFsmState StateInformationWindowCreate(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.SetRankingResult();
			this.m_eventRankingResultInfo = this.GetEventRankingResultInformation();
			this.CreateInformationWindow();
			return TinyFsmState.End();
		case 4:
			if (this.m_eventRankingResultInfo != null)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateEventRankingResultWindow)), MainMenu.SequenceState.EventRankingResultWindow);
			}
			else if (this.m_rankingResultList.Count > 0)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateRankingResultLeagueWindow)), MainMenu.SequenceState.RankingResultLeagueWindow);
			}
			else if (this.m_serverInformationWindow != null)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateDisplayInformaon)), MainMenu.SequenceState.DisplayInformaon);
			}
			else
			{
				this.ChangeNextStateForInformation();
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x00071354 File Offset: 0x0006F554
	private TinyFsmState StateEventRankingResultWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_eventRankingResult != null)
			{
				this.m_eventRankingResult = null;
			}
			return TinyFsmState.End();
		case 1:
			this.m_eventRankingResult = this.CreateWorldRankingWindow(this.m_eventRankingResultInfo);
			return TinyFsmState.End();
		case 4:
			if (this.m_eventRankingResult != null && this.m_eventRankingResult.IsEnd)
			{
				this.ServerNoticeInfoUpdateChecked(this.m_eventRankingResultInfo);
				this.ServerNoticeInfoSave();
				if (this.m_rankingResultList.Count > 0)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRankingResultLeagueWindow)), MainMenu.SequenceState.RankingResultLeagueWindow);
				}
				else if (this.m_serverInformationWindow != null)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateDisplayInformaon)), MainMenu.SequenceState.DisplayInformaon);
				}
				else
				{
					this.ChangeNextStateForInformation();
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x00071464 File Offset: 0x0006F664
	private TinyFsmState StateRankingResultLeagueWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_rankingResultLeagueWindow != null)
			{
				this.m_rankingResultLeagueWindow = null;
			}
			return TinyFsmState.End();
		case 1:
			this.m_rankingResultLeagueWindow = null;
			using (List<NetNoticeItem>.Enumerator enumerator = this.m_rankingResultList.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					NetNoticeItem netNoticeItem = enumerator.Current;
					this.m_rankingResultLeagueWindow = RankingResultLeague.Create(netNoticeItem);
					this.m_currentResultInfo = netNoticeItem;
				}
			}
			return TinyFsmState.End();
		case 4:
		{
			bool flag = this.m_rankingResultLeagueWindow == null;
			if (this.m_rankingResultLeagueWindow != null && this.m_rankingResultLeagueWindow.IsEnd())
			{
				this.ServerNoticeInfoUpdateChecked(this.m_currentResultInfo);
				this.m_rankingResultList.Remove(this.m_currentResultInfo);
				this.m_rankingResultLeagueWindow = null;
				this.m_currentResultInfo = null;
				using (List<NetNoticeItem>.Enumerator enumerator2 = this.m_rankingResultList.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						NetNoticeItem netNoticeItem2 = enumerator2.Current;
						this.m_rankingResultLeagueWindow = RankingResultLeague.Create(netNoticeItem2);
						this.m_currentResultInfo = netNoticeItem2;
					}
				}
			}
			if (flag)
			{
				if (this.m_serverInformationWindow != null)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateDisplayInformaon)), MainMenu.SequenceState.DisplayInformaon);
				}
				else
				{
					this.ChangeNextStateForInformation();
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x00071638 File Offset: 0x0006F838
	private TinyFsmState StateDisplayInformaon(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_serverInformationWindow != null)
			{
				this.m_serverInformationWindow = null;
			}
			return TinyFsmState.End();
		case 1:
			if (this.m_serverInformationWindow != null)
			{
				this.m_serverInformationWindow.SetSaveFlag();
				this.m_serverInformationWindow.PlayStart();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_serverInformationWindow != null && this.m_serverInformationWindow.IsEnd())
			{
				this.ChangeNextStateForInformation();
				if (InformationImageManager.Instance != null)
				{
					InformationImageManager.Instance.ClearWinowImage();
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x00071704 File Offset: 0x0006F904
	private void CreateInformationWindow()
	{
		if (this.m_serverInformationWindow == null)
		{
			GameObject gameObject = new GameObject();
			if (gameObject != null)
			{
				gameObject.name = "ServerInformationWindow";
				gameObject.AddComponent<ServerInformationWindow>();
				this.m_serverInformationWindow = gameObject.GetComponent<ServerInformationWindow>();
			}
		}
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x00071754 File Offset: 0x0006F954
	private RankingResultWorldRanking CreateWorldRankingWindow(NetNoticeItem item)
	{
		if (item != null)
		{
			RankingResultWorldRanking resultWorldRanking = RankingResultWorldRanking.GetResultWorldRanking();
			if (resultWorldRanking != null)
			{
				resultWorldRanking.Setup(item);
				resultWorldRanking.PlayStart();
				return resultWorldRanking;
			}
		}
		return null;
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x0007178C File Offset: 0x0006F98C
	private void SetRankingResult()
	{
		this.m_eventRankingResultInfo = this.GetInformation(NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID);
		NetNoticeItem information = this.GetInformation(NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID);
		NetNoticeItem information2 = this.GetInformation(NetNoticeItem.OPERATORINFO_QUICKRANKINGRESULT_ID);
		if (information != null && information2 != null)
		{
			if (information.Priority < information2.Priority)
			{
				this.m_rankingResultList.Add(information);
				this.m_rankingResultList.Add(information2);
			}
			else
			{
				this.m_rankingResultList.Add(information2);
				this.m_rankingResultList.Add(information);
			}
		}
		else
		{
			if (information != null)
			{
				this.m_rankingResultList.Add(information);
			}
			if (information2 != null)
			{
				this.m_rankingResultList.Add(information2);
			}
		}
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x00071840 File Offset: 0x0006FA40
	private NetNoticeItem GetRankingResultInformation()
	{
		return this.GetInformation(NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID);
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x00071850 File Offset: 0x0006FA50
	private NetNoticeItem GetEventRankingResultInformation()
	{
		return this.GetInformation(NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID);
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x00071860 File Offset: 0x0006FA60
	private NetNoticeItem GetInformation(int id)
	{
		if (ServerInterface.NoticeInfo != null)
		{
			List<NetNoticeItem> noticeItems = ServerInterface.NoticeInfo.m_noticeItems;
			if (noticeItems != null)
			{
				foreach (NetNoticeItem netNoticeItem in noticeItems)
				{
					if (netNoticeItem != null && netNoticeItem.Id == (long)id && !ServerInterface.NoticeInfo.IsChecked(netNoticeItem))
					{
						return netNoticeItem;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x00071904 File Offset: 0x0006FB04
	private void ServerNoticeInfoUpdateChecked(NetNoticeItem item)
	{
		if (ServerInterface.NoticeInfo != null)
		{
			ServerInterface.NoticeInfo.UpdateChecked(item);
		}
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x0007191C File Offset: 0x0006FB1C
	private void ServerNoticeInfoSave()
	{
		if (ServerInterface.NoticeInfo != null)
		{
			ServerInterface.NoticeInfo.m_isShowedNoticeInfo = true;
			ServerInterface.NoticeInfo.SaveInformation();
		}
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x00071940 File Offset: 0x0006FB40
	private void ChangeNextStateForInformation()
	{
		HudMenuUtility.SendMsgInformationDisplay();
		this.ServerNoticeInfoSave();
		HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.INFOMATION, false);
		this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenu)), MainMenu.SequenceState.Main);
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x0007197C File Offset: 0x0006FB7C
	private void ResourceLoadEndCallback()
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "OnPageResourceLoadedCallback", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x00071990 File Offset: 0x0006FB90
	private TinyFsmState StateInit(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			TimeProfiler.StartCountTime("StateInit");
			if (Env.useAssetBundle)
			{
				if (AssetBundleLoader.Instance == null)
				{
					AssetBundleLoader.Create();
				}
				if (!AssetBundleLoader.Instance.IsEnableDownlad())
				{
					AssetBundleLoader.Instance.Initialize();
				}
			}
			StageAbilityManager instance = StageAbilityManager.Instance;
			if (instance != null)
			{
				for (int i = 0; i < 3; i++)
				{
					instance.BoostItemValidFlag[i] = false;
				}
			}
			NativeObserver.Instance.CheckCurrentTransaction();
			return TinyFsmState.End();
		}
		case 4:
			if (!Env.useAssetBundle || AssetBundleLoader.Instance.IsEnableDownlad())
			{
				TimeProfiler.EndCountTime("StateInit");
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateRequestDayCrossWatcher)), MainMenu.SequenceState.RequestDayCrossWatcher);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x00071A9C File Offset: 0x0006FC9C
	private TinyFsmState StateRequestDayCrossWatcher(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			this.m_callBackFlag.Reset();
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(1);
			}
			return TinyFsmState.End();
		case 1:
		{
			TimeProfiler.StartCountTime("StateRequestDayCrossWatcher");
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_callBackFlag.Reset();
				if (ServerDayCrossWatcher.Instance != null)
				{
					ServerDayCrossWatcher.Instance.UpdateClientInfosByDayCross(new ServerDayCrossWatcher.UpdateInfoCallback(this.DataCrossCallBack));
				}
			}
			else
			{
				this.m_callBackFlag.Set(0, true);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_callBackFlag.Test(0))
			{
				TimeProfiler.EndCountTime("StateRequestDayCrossWatcher");
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateRequestDailyBattle)), MainMenu.SequenceState.RequestDailyBattle);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x00071BB0 File Offset: 0x0006FDB0
	private TinyFsmState StateRequestDailyBattle(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(2);
			}
			return TinyFsmState.End();
		case 1:
			TimeProfiler.StartCountTime("StateRequestDailyBattle");
			if (this.IsRequestDailyBattle())
			{
				if (SingletonGameObject<DailyBattleManager>.Instance != null)
				{
					SingletonGameObject<DailyBattleManager>.Instance.FirstSetup(new DailyBattleManager.CallbackSetup(this.DailyBattleManagerCallBack));
				}
				else
				{
					this.m_connected = true;
				}
			}
			else
			{
				this.m_connected = true;
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_connected)
			{
				TimeProfiler.EndCountTime("StateRequestDailyBattle");
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateRequestChaoWheelOption)), MainMenu.SequenceState.RequestChaoWheelOption);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x00071CA4 File Offset: 0x0006FEA4
	private TinyFsmState StateRequestChaoWheelOption(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(3);
			}
			return TinyFsmState.End();
		case 1:
			TimeProfiler.StartCountTime("StateRequestChaoWheelOption");
			if (this.IsRequestChoaWheelOption())
			{
				if (RouletteManager.Instance != null)
				{
					RouletteManager.CallbackRouletteInit callback = new RouletteManager.CallbackRouletteInit(this.CallbackRouletteInit);
					RouletteManager.Instance.InitRouletteRequest(callback);
				}
				else
				{
					this.m_connected = true;
				}
			}
			else
			{
				this.m_connected = true;
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_connected)
			{
				this.SetStageResultData();
				this.CheckEventEnd();
				TimeProfiler.EndCountTime("StateRequestChaoWheelOption");
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateRequestMsgList)), MainMenu.SequenceState.RequestMsgList);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x00071DA8 File Offset: 0x0006FFA8
	private TinyFsmState StateRequestMsgList(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(4);
			}
			return TinyFsmState.End();
		case 1:
		{
			TimeProfiler.StartCountTime("StateRequestMsgList");
			this.m_flags.Set(19, false);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetMessageList(base.gameObject);
				this.m_flags.Set(19, true);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (!this.m_flags.Test(19))
			{
				TimeProfiler.EndCountTime("StateRequestMsgList");
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateRequestNoticeInfo)), MainMenu.SequenceState.RequestNoticeInfo);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x00071E98 File Offset: 0x00070098
	private TinyFsmState StateRequestNoticeInfo(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(5);
			}
			return TinyFsmState.End();
		case 1:
		{
			TimeProfiler.StartCountTime("StateRequestNoticeInfo");
			this.m_is_end_notice_connect = false;
			bool flag = true;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				ServerNoticeInfo noticeInfo = ServerInterface.NoticeInfo;
				if (noticeInfo != null && noticeInfo.IsNeedUpdateInfo())
				{
					loggedInServerInterface.RequestServerGetInformation(base.gameObject);
					flag = false;
				}
			}
			if (flag)
			{
				this.ServerGetInformation_Succeeded(null);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_is_end_notice_connect)
			{
				RouletteInformationManager instance = RouletteInformationManager.Instance;
				if (instance != null)
				{
					instance.SetUp();
				}
				TimeProfiler.EndCountTime("StateRequestNoticeInfo");
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoad)), MainMenu.SequenceState.Load);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x00071FAC File Offset: 0x000701AC
	private void SetStageResultData()
	{
		this.m_flags.Set(6, false);
		this.m_flags.Set(7, false);
		this.m_flags.Set(8, false);
		ServerMileageMapState mileageMapState = ServerInterface.MileageMapState;
		if (mileageMapState != null)
		{
			mileageMapState.CopyTo(this.m_mileage_map_state);
			mileageMapState.CopyTo(this.m_prev_mileage_map_state);
		}
		GameObject gameObject = GameObject.Find("ResultInfo");
		bool flag = gameObject != null;
		bool flag2 = !flag;
		if (flag)
		{
			ResultInfo component = gameObject.GetComponent<ResultInfo>();
			if (component != null)
			{
				this.m_stageResultData = component.GetInfo();
				UnityEngine.Object.Destroy(gameObject);
				bool quickMode = this.m_stageResultData.m_quickMode;
				if (quickMode && this.m_stageResultData.m_missionComplete)
				{
					this.m_flags.Set(16, true);
				}
				if (this.m_stageResultData.m_validResult)
				{
					this.ReflectMileageProductionResultData();
					this.m_flags.Set(7, true);
				}
				else if (this.m_stageResultData.m_fromOptionTutorial && SaveDataManager.Instance != null)
				{
					PlayerData playerData = SaveDataManager.Instance.PlayerData;
					ServerPlayerState playerState = ServerInterface.PlayerState;
					if (playerState != null && playerData != null)
					{
						PlayerData playerData2 = playerData;
						ServerItem serverItem = new ServerItem((ServerItem.Id)playerState.m_mainCharaId);
						playerData2.MainChara = serverItem.charaType;
						PlayerData playerData3 = playerData;
						ServerItem serverItem2 = new ServerItem((ServerItem.Id)playerState.m_mainChaoId);
						playerData3.MainChaoID = serverItem2.chaoId;
						PlayerData playerData4 = playerData;
						ServerItem serverItem3 = new ServerItem((ServerItem.Id)playerState.m_subChaoId);
						playerData4.SubChaoID = serverItem3.chaoId;
					}
				}
			}
		}
		else if (flag2)
		{
			this.m_flags.Set(8, false);
			bool flag3 = !HudMenuUtility.IsTutorial_11() && !HudMenuUtility.IsRouletteTutorial() && !HudMenuUtility.IsTutorialCharaLevelUp();
			if (flag3 && SaveDataManager.Instance != null && !SaveDataManager.Instance.PlayerData.DailyMission.missions_complete)
			{
				this.m_flags.Set(16, true);
			}
		}
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000721B8 File Offset: 0x000703B8
	private bool HaveNoticeInfo()
	{
		return ServerInterface.NoticeInfo != null && !ServerInterface.NoticeInfo.IsAllChecked();
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000721D4 File Offset: 0x000703D4
	private void ReflectMileageProductionResultData()
	{
		if (this.m_stageResultData.m_oldMapState == null)
		{
			this.m_stageResultData.m_oldMapState = new MileageMapState();
		}
		if (this.m_stageResultData.m_newMapState == null)
		{
			this.m_stageResultData.m_newMapState = this.m_stageResultData.m_oldMapState;
		}
		if (ServerInterface.MileageMapState != null)
		{
			ServerInterface.MileageMapState.CopyTo(this.m_mileage_map_state);
		}
		this.SetMapState(ref this.m_mileage_map_state, this.m_stageResultData.m_newMapState);
		this.SetMapState(ref this.m_prev_mileage_map_state, this.m_stageResultData.m_oldMapState);
		if (this.CheckNextMap())
		{
			this.m_flags.Set(6, true);
		}
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x00072288 File Offset: 0x00070488
	private void SetMapState(ref ServerMileageMapState map_state, MileageMapState result_map_state)
	{
		if (map_state != null && result_map_state != null)
		{
			map_state.m_episode = result_map_state.m_episode;
			map_state.m_chapter = result_map_state.m_chapter;
			map_state.m_point = result_map_state.m_point;
			map_state.m_stageTotalScore = result_map_state.m_score;
			if (map_state.m_episode == 0)
			{
				map_state.m_episode = 1;
			}
			if (map_state.m_chapter == 0)
			{
				map_state.m_chapter = 1;
			}
		}
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x00072300 File Offset: 0x00070500
	private bool IsRequestChoaWheelOption()
	{
		ServerMileageMapState mileageMapState = ServerInterface.MileageMapState;
		return mileageMapState != null && !ServerInterface.ChaoWheelOptions.IsConnected;
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x0007232C File Offset: 0x0007052C
	private bool IsRequestDailyBattle()
	{
		if (ServerInterface.LoggedInServerInterface != null)
		{
			GameObject x = GameObject.Find("ResultInfo");
			if (x == null)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x00072364 File Offset: 0x00070564
	private bool CheckNextMap()
	{
		if (this.m_stageResultData != null && this.m_stageResultData.m_newMapState != null && this.m_stageResultData.m_oldMapState != null)
		{
			if (this.m_stageResultData.m_oldMapState.m_episode != this.m_stageResultData.m_newMapState.m_episode)
			{
				return true;
			}
			if (this.m_stageResultData.m_oldMapState.m_chapter != this.m_stageResultData.m_newMapState.m_chapter)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x000723EC File Offset: 0x000705EC
	private bool CheckEventStateRequest()
	{
		return EventManager.Instance != null && EventManager.Instance.Type == EventManager.EventType.COLLECT_OBJECT && !EventManager.Instance.IsSetEventStateInfo && EventManager.Instance.IsInEvent();
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x00072434 File Offset: 0x00070634
	private void CheckEventEnd()
	{
		if (EventManager.Instance != null && EventManager.Instance.Type != EventManager.EventType.UNKNOWN && !EventManager.Instance.IsInEvent())
		{
			EventManager.Instance.CheckEvent();
			if (ResourceManager.Instance != null)
			{
				ResourceManager.Instance.RemoveResources(ResourceCategory.EVENT_RESOURCE);
			}
		}
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x00072498 File Offset: 0x00070698
	private void ServerGetChaoWheelOptions_Succeeded(MsgGetChaoWheelOptionsSucceed msg)
	{
		this.m_connected = true;
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000724A4 File Offset: 0x000706A4
	private void CallbackRouletteInit(int specialEggNum)
	{
		this.m_connected = true;
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000724B0 File Offset: 0x000706B0
	private void ServerGetInformation_Succeeded(MsgGetInformationSucceed msg)
	{
		this.m_is_end_notice_connect = true;
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000724BC File Offset: 0x000706BC
	private void DailyBattleManagerCallBack()
	{
		this.m_connected = true;
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000724C8 File Offset: 0x000706C8
	private TinyFsmState StateInviteFriend(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_fristLaunchInviteFriend != null)
			{
				UnityEngine.Object.Destroy(this.m_fristLaunchInviteFriend.gameObject);
			}
			return TinyFsmState.End();
		case 1:
			this.CreateFirstLaunchInviteFriend();
			return TinyFsmState.End();
		case 4:
			if (this.m_fristLaunchInviteFriend != null)
			{
				if (this.m_startLauncherInviteFriendFlag)
				{
					if (this.m_fristLaunchInviteFriend.IsEndPlay)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
					}
				}
				else
				{
					this.m_fristLaunchInviteFriend.Setup("Camera/menu_Anim/MainMenuUI4/Anchor_5_MC");
					this.m_fristLaunchInviteFriend.PlayStart();
					this.m_startLauncherInviteFriendFlag = true;
				}
			}
			else
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000725D0 File Offset: 0x000707D0
	private void CreateFirstLaunchInviteFriend()
	{
		if (this.m_fristLaunchInviteFriend == null)
		{
			GameObject gameObject = new GameObject();
			if (gameObject != null)
			{
				gameObject.name = "FirstLaunchInviteFriend";
				gameObject.AddComponent<FirstLaunchInviteFriend>();
				this.m_fristLaunchInviteFriend = gameObject.GetComponent<FirstLaunchInviteFriend>();
			}
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x00072620 File Offset: 0x00070820
	private TinyFsmState StatePlayItem(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.SHOP)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateShop)), MainMenu.SequenceState.Shop);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.STAGE_CHECK)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckStage)), MainMenu.SequenceState.CheckStage);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.EPISODE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadMileageXml)), MainMenu.SequenceState.LoadMileageXml);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x0007272C File Offset: 0x0007092C
	private TinyFsmState StateLoad(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(6);
			}
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
		{
			TimeProfiler.StartCountTime("StateLoad");
			this.CeateSceneLoader();
			if (this.m_scene_loader_obj != null)
			{
				ResourceSceneLoader component = this.m_scene_loader_obj.GetComponent<ResourceSceneLoader>();
				TextManager.LoadCommonText(component);
				TextManager.LoadChaoText(component);
				TextManager.LoadEventText(component);
			}
			this.CeateSceneLoader();
			this.SetLoadEventResource();
			if (GameObject.Find("MissionTable") == null)
			{
				this.AddSceneLoader("MissionTable");
			}
			if (GameObject.Find("CharacterDataNameInfo") == null)
			{
				this.AddSceneLoader("CharacterDataNameInfo");
			}
			if (!this.IsExistMileageMapData(this.m_mileage_map_state))
			{
				this.AddSceneLoader(this.GetMileageMapDataScenaName(this.m_mileage_map_state));
			}
			if (GameObject.Find("MileageDataTable") == null)
			{
				this.AddSceneLoader("MileageDataTable");
			}
			string suffixe = TextUtility.GetSuffixe();
			if (GameObject.Find("ui_tex_ranking_" + suffixe) == null)
			{
				this.AddSceneLoader("ui_tex_ranking_" + suffixe);
			}
			if (InformationDataTable.Instance == null)
			{
				InformationDataTable.Create();
				InformationDataTable.Instance.Initialize(base.gameObject);
			}
			if (this.m_scene_loader_obj != null)
			{
				StageAbilityManager.LoadAbilityDataTable(this.m_scene_loader_obj.GetComponent<ResourceSceneLoader>());
			}
			this.AddSceneLoader("MainMenuPages");
			return TinyFsmState.End();
		}
		case 4:
		{
			bool flag = true;
			if (this.m_buttonEventResourceLoader != null)
			{
				flag = this.m_buttonEventResourceLoader.IsLoaded;
			}
			if (flag && this.CheckSceneLoad())
			{
				TextManager.SetupCommonText();
				TextManager.SetupChaoText();
				TextManager.SetupEventText();
				if (this.m_eventSpecficText)
				{
					TextManager.SetupEventProductionText();
				}
				this.DestroySceneLoader();
				this.SetMainMenuPages();
				this.SetEventResources();
				StageAbilityManager.SetupAbilityDataTable();
				TimeProfiler.EndCountTime("StateLoad");
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadAtlas)), MainMenu.SequenceState.LoadAtlas);
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x00072984 File Offset: 0x00070B84
	private TinyFsmState StateLoadAtlas(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(7);
			}
			return TinyFsmState.End();
		case 1:
			TimeProfiler.StartCountTime("StateLoadAtlas");
			this.CeateSceneLoader();
			this.StartLoadAtlas();
			return TinyFsmState.End();
		case 4:
			TimeProfiler.EndCountTime("StateLoadAtlas");
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateStartMessage)), MainMenu.SequenceState.StartMessage);
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x00072A30 File Offset: 0x00070C30
	private bool CheckSceneLoad()
	{
		if (this.m_scene_loader_obj != null)
		{
			ResourceSceneLoader component = this.m_scene_loader_obj.GetComponent<ResourceSceneLoader>();
			if (component != null)
			{
				return component.Loaded;
			}
		}
		return true;
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x00072A70 File Offset: 0x00070C70
	private string GetMileageMapDataScenaName(ServerMileageMapState state)
	{
		if (state != null)
		{
			return "MileageMapData" + state.m_episode.ToString("000") + "_" + state.m_chapter.ToString("00");
		}
		return null;
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x00072AB4 File Offset: 0x00070CB4
	private void CeateSceneLoader()
	{
		if (this.m_scene_loader_obj == null)
		{
			this.m_scene_loader_obj = new GameObject("SceneLoader");
			if (this.m_scene_loader_obj != null)
			{
				this.m_scene_loader_obj.AddComponent<ResourceSceneLoader>();
			}
		}
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x00072B00 File Offset: 0x00070D00
	private void DestroySceneLoader()
	{
		UnityEngine.Object.Destroy(this.m_scene_loader_obj);
		this.m_scene_loader_obj = null;
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x00072B14 File Offset: 0x00070D14
	private void AddSceneLoader(string scene_name)
	{
		if (scene_name != null && this.m_scene_loader_obj != null)
		{
			ResourceSceneLoader component = this.m_scene_loader_obj.GetComponent<ResourceSceneLoader>();
			if (component != null)
			{
				bool onAssetBundle = true;
				component.AddLoad(scene_name, onAssetBundle, false);
			}
		}
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x00072B5C File Offset: 0x00070D5C
	private void AddSceneLoaderAndResourceManager(ResourceSceneLoader.ResourceInfo resInfo)
	{
		if (this.m_scene_loader_obj != null)
		{
			ResourceSceneLoader component = this.m_scene_loader_obj.GetComponent<ResourceSceneLoader>();
			if (component != null)
			{
				component.AddLoadAndResourceManager(resInfo);
			}
		}
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x00072B9C File Offset: 0x00070D9C
	private void AddIDList(ref List<int> request_list, int id, string type, bool keep)
	{
		if (request_list != null && id != -1 && id != 0)
		{
			MileageMapDataManager instance = MileageMapDataManager.Instance;
			if (instance != null)
			{
				string text = string.Empty;
				if (type == "bg")
				{
					text = MileageMapBGDataTable.Instance.GetTextureName(id);
				}
				else if (type == "face")
				{
					text = MileageMapUtility.GetFaceTextureName(id);
				}
				if (text != string.Empty && keep)
				{
					instance.AddKeepList(text);
				}
			}
			foreach (int num in request_list)
			{
				if (num == id)
				{
					return;
				}
			}
			request_list.Add(id);
		}
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x00072C90 File Offset: 0x00070E90
	private void SetLoadingFaceTexture(ref List<int> requestFaceList, ref List<int> loadingList, MileageMapData mileageMapData, int windowId)
	{
		if (mileageMapData != null && windowId < mileageMapData.window_data.Length)
		{
			int face_id = mileageMapData.window_data[windowId].body[0].product[0].face_id;
			this.AddIDList(ref requestFaceList, face_id, "face", true);
			loadingList.Add(face_id);
		}
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x00072CE8 File Offset: 0x00070EE8
	private void SetLoadWindowFaceTexture(ref List<int> requestFaceList, MileageMapData mileageMapData, ServerMileageMapState state = null)
	{
		if (mileageMapData == null)
		{
			return;
		}
		List<int> list = new List<int>();
		int num = mileageMapData.event_data.point.Length;
		if (state != null)
		{
			for (int i = 0; i < num; i++)
			{
				if (state.m_point <= i)
				{
					if (i != 5 || !mileageMapData.event_data.IsBossEvent())
					{
						list.Add(mileageMapData.event_data.point[i].window_id);
					}
					else
					{
						BossEvent bossEvent = mileageMapData.event_data.GetBossEvent();
						list.Add(bossEvent.after_window_id);
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < num; j++)
			{
				if (this.m_prev_mileage_map_state.m_point <= j && this.m_mileage_map_state.m_point >= j)
				{
					if (j != 5 || !mileageMapData.event_data.IsBossEvent())
					{
						list.Add(mileageMapData.event_data.point[j].window_id);
					}
					else
					{
						list.Add(mileageMapData.event_data.point[j].boss.before_window_id);
					}
				}
			}
		}
		int num2 = mileageMapData.window_data.Length;
		for (int k = 0; k < num2; k++)
		{
			int num3 = mileageMapData.window_data[k].body.Length;
			for (int l = 0; l < num3; l++)
			{
				if (mileageMapData.window_data[k].body[l].product == null)
				{
					mileageMapData.window_data[k].body[l].product = new WindowProductData[0];
				}
				if (list.Contains(mileageMapData.window_data[k].id))
				{
					int num4 = mileageMapData.window_data[k].body[l].product.Length;
					for (int m = 0; m < num4; m++)
					{
						this.AddIDList(ref requestFaceList, mileageMapData.window_data[k].body[l].product[m].face_id, "face", false);
					}
				}
			}
		}
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x00072F0C File Offset: 0x0007110C
	public void SetMainMenuPages()
	{
		GameObject gameObject = GameObject.Find("MainMenuPages");
		if (gameObject != null)
		{
			GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
			GameObject mainMenuGeneralAnchor = HudMenuUtility.GetMainMenuGeneralAnchor();
			if (menuAnimUIObject != null && mainMenuGeneralAnchor != null)
			{
				Transform transform = gameObject.transform;
				int childCount = transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					Transform child = transform.GetChild(0);
					Vector3 localPosition = child.localPosition;
					Vector3 localScale = child.localScale;
					if (child.name == "item_get_Window" || child.name == "RankingWindowUI")
					{
						child.parent = mainMenuGeneralAnchor.transform;
					}
					else
					{
						child.parent = menuAnimUIObject.transform;
					}
					child.localPosition = localPosition;
					child.localScale = localScale;
					child.gameObject.SetActive(false);
				}
			}
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x00073008 File Offset: 0x00071208
	private void SetLoadEventResource()
	{
		if (EventManager.Instance != null)
		{
			if (EventManager.Instance.IsQuickEvent())
			{
				string resourceName = EventManager.GetResourceName();
				ResourceSceneLoader.ResourceInfo resourceInfo = this.m_loadInfo[0];
				resourceInfo.m_scenename += resourceName;
				this.AddSceneLoaderAndResourceManager(resourceInfo);
				ResourceSceneLoader.ResourceInfo resourceInfo2 = this.m_loadInfo[1];
				resourceInfo2.m_scenename += resourceName;
				this.AddSceneLoaderAndResourceManager(resourceInfo2);
				this.m_eventResourceId = EventManager.Instance.Id;
				this.LoadNewsWindow();
			}
			else if (EventManager.Instance.IsBGMEvent())
			{
				string resourceName2 = EventManager.GetResourceName();
				ResourceSceneLoader.ResourceInfo resourceInfo3 = this.m_loadInfo[0];
				resourceInfo3.m_scenename += resourceName2;
				this.AddSceneLoaderAndResourceManager(resourceInfo3);
				this.m_eventResourceId = EventManager.Instance.Id;
				this.LoadNewsWindow();
			}
		}
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000730F8 File Offset: 0x000712F8
	private void LoadNewsWindow()
	{
		this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
		this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("NewsWindow", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x00073134 File Offset: 0x00071334
	private void SetLoadTopMenuTexture()
	{
		ResourceSceneLoader.ResourceInfo resourceInfo = this.m_loadInfo[2];
		int num = 1;
		if (StageModeManager.Instance != null)
		{
			num = StageModeManager.Instance.QuickStageIndex;
		}
		resourceInfo.m_scenename = "ui_tex_mile_w" + num.ToString("D2") + "A";
		this.AddSceneLoaderAndResourceManager(resourceInfo);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x00073194 File Offset: 0x00071394
	private void SetEventResources()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsQuickEvent())
		{
			EventCommonDataTable.LoadSetup();
		}
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x000731C8 File Offset: 0x000713C8
	private void StartLoadAtlas()
	{
		if (FontManager.Instance != null)
		{
			FontManager.Instance.LoadResourceData();
		}
		if (AtlasManager.Instance != null)
		{
			AtlasManager.Instance.StartLoadAtlasForMenu();
		}
		if (TextureAsyncLoadManager.Instance != null)
		{
			PlayerData playerData = SaveDataManager.Instance.PlayerData;
			TextureRequestChara request = new TextureRequestChara(playerData.MainChara, null);
			TextureRequestChara request2 = new TextureRequestChara(playerData.SubChara, null);
			TextureAsyncLoadManager.Instance.Request(request);
			TextureAsyncLoadManager.Instance.Request(request2);
			UnityEngine.Random.seed = NetUtil.GetCurrentUnixTime();
			int textureIndex = UnityEngine.Random.Range(0, TextureRequestEpisodeBanner.BannerCount);
			TextureRequestEpisodeBanner request3 = new TextureRequestEpisodeBanner(textureIndex, null);
			TextureAsyncLoadManager.Instance.Request(request3);
			StageModeManager instance = StageModeManager.Instance;
			if (instance != null)
			{
				instance.DrawQuickStageIndex();
				TextureRequestStagePicture request4 = new TextureRequestStagePicture(instance.QuickStageIndex, null);
				TextureAsyncLoadManager.Instance.Request(request4);
			}
		}
		if (ChaoTextureManager.Instance != null)
		{
			ChaoTextureManager.Instance.RequestLoadingPageChaoTexture();
		}
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x000732D0 File Offset: 0x000714D0
	private bool IsLoadedAtlas()
	{
		return !(AtlasManager.Instance != null) || AtlasManager.Instance.IsLoadAtlas();
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x00073300 File Offset: 0x00071500
	private void SetReplaceAtlas()
	{
		if (FontManager.Instance != null)
		{
			FontManager.Instance.ReplaceFont();
		}
		if (AtlasManager.Instance != null)
		{
			AtlasManager.Instance.ReplaceAtlasForMenu(true);
		}
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x00073344 File Offset: 0x00071544
	private TinyFsmState StateLoginBonusWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("LoginWindowUI", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			this.m_flags.Set(18, false);
			return TinyFsmState.End();
		case 4:
			if (this.m_buttonEventResourceLoader.IsLoaded)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoginBonusWindowDisplay)), MainMenu.SequenceState.LoginBonusWindowDisplay);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x00073414 File Offset: 0x00071614
	private TinyFsmState StateLoginBonusWindowDisplay(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
			if (menuAnimUIObject != null)
			{
				this.m_LoginWindowUI = GameObjectUtil.FindChildGameObjectComponent<LoginBonusWindowUI>(menuAnimUIObject, "LoginWindowUI");
				if (this.m_LoginWindowUI != null)
				{
					this.m_LoginWindowUI.gameObject.SetActive(true);
					this.m_LoginWindowUI.PlayStart(LoginBonusWindowUI.LoginBonusType.NORMAL);
				}
			}
			this.m_flags.Set(17, false);
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_LoginWindowUI != null && this.m_LoginWindowUI.IsEnd)
			{
				this.m_LoginWindowUI = null;
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x0007350C File Offset: 0x0007170C
	private TinyFsmState StateFirstLoginBonusWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("StartDashWindowUI", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			this.m_flags.Set(18, false);
			return TinyFsmState.End();
		case 4:
			if (this.m_buttonEventResourceLoader.IsLoaded)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateFirstLoginBonusWindowDisplay)), MainMenu.SequenceState.FirstLoginBonusWindowDisplay);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000735DC File Offset: 0x000717DC
	private TinyFsmState StateFirstLoginBonusWindowDisplay(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
			if (menuAnimUIObject != null)
			{
				this.m_LoginWindowUI = GameObjectUtil.FindChildGameObjectComponent<LoginBonusWindowUI>(menuAnimUIObject, "StartDashWindowUI");
				if (this.m_LoginWindowUI != null)
				{
					this.m_LoginWindowUI.gameObject.SetActive(true);
					this.m_LoginWindowUI.PlayStart(LoginBonusWindowUI.LoginBonusType.FIRST);
				}
			}
			this.m_flags.Set(18, false);
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_LoginWindowUI != null && this.m_LoginWindowUI.IsEnd)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				this.m_LoginWindowUI = null;
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x000736D4 File Offset: 0x000718D4
	private TinyFsmState StateLoginRanking(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_flags.Set(8, false);
			return TinyFsmState.End();
		case 4:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null && msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.RANKING_END)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x0007378C File Offset: 0x0007198C
	private TinyFsmState StateMainMenu(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.BossChallenge = false;
			this.m_flags.Set(4, false);
			this.m_flags.Set(5, false);
			this.m_flags.Set(28, false);
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			HudMenuUtility.OnForceDisableShopButton(false);
			GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "UpdateView", null, SendMessageOptions.DontRequireReceiver);
			return TinyFsmState.End();
		default:
			if (signal != 102)
			{
				return TinyFsmState.End();
			}
			this.CreateTitleBackWindow();
			return TinyFsmState.End();
		case 4:
			this.CheckTutoralWindow();
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null && this.JudgeMainMenuReceiveEvent(msgMenuSequence))
			{
				this.m_communicateFlag.Reset();
			}
			return TinyFsmState.End();
		}
		}
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x00073878 File Offset: 0x00071A78
	private TinyFsmState StateMainMenuConnect(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CheckEventParameter();
			this.m_communicateFlag.Set(0, this.IsTickerCommunicate());
			this.m_communicateFlag.Set(2, this.IsMsgBoxCommunicate());
			this.m_communicateFlag.Set(4, this.IsSchemeCommunicate());
			this.m_communicateFlag.Set(6, this.IsChangeDataVersion());
			if (!this.m_communicateFlag.Test(8))
			{
				this.m_communicateFlag.Set(7, true);
			}
			return TinyFsmState.End();
		case 4:
		{
			if (this.m_buttonEvent != null && !this.m_buttonEvent.IsTransform && this.m_flags.Test(21))
			{
				this.m_communicateFlag.Set(4, this.IsSchemeCommunicate());
				this.m_flags.Set(21, false);
			}
			bool stageResultData = this.m_stageResultData != null;
			bool flag = this.m_stageResultData != null && this.m_stageResultData.m_quickMode;
			bool flag2 = this.m_stageResultData != null && this.m_stageResultData.m_rivalHighScore;
			bool flag3 = false;
			if (SingletonGameObject<RankingManager>.Instance != null)
			{
				RankingUtil.RankChange rankingRankChange = SingletonGameObject<RankingManager>.Instance.GetRankingRankChange(RankingUtil.RankingMode.QUICK, RankingUtil.RankingScoreType.HIGH_SCORE, RankingUtil.RankingRankerType.RIVAL);
				flag3 = (rankingRankChange == RankingUtil.RankChange.UP);
			}
			if (this.m_flags.Test(25) && stageResultData && flag && flag2)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateBestRecordCheckEnableFeed)), MainMenu.SequenceState.BestRecordCheckEnableFeed);
				this.m_flags.Set(25, false);
			}
			else if (this.m_flags.Test(26) && stageResultData && flag && flag2 && flag3)
			{
				MainMenu.m_debug++;
				this.m_flags.Set(26, false);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateQuickModeRankUp)), MainMenu.SequenceState.QuickModeRankUp);
			}
			else if (this.m_communicateFlag.Test(6))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateVersionChangeWindow)), MainMenu.SequenceState.VersionChangeWindow);
			}
			else if (this.m_flags.Test(13))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateEventResourceChangeWindow)), MainMenu.SequenceState.EventResourceChangeWindow);
			}
			else if (FirstLaunchUserName.IsFirstLaunch)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameSetting)), MainMenu.SequenceState.UserNameSetting);
			}
			else if (!AgeVerification.IsAgeVerificated)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateAgeVerification)), MainMenu.SequenceState.AgeVerification);
			}
			else if (this.m_communicateFlag.Test(0) && !this.m_communicateFlag.Test(1))
			{
				HudMenuUtility.SendMsgTickerReset();
				this.m_communicateFlag.Set(1, true);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateTickerCommunicate)), MainMenu.SequenceState.TickerCommunicate);
			}
			else if (this.m_communicateFlag.Test(4))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateSchemeCommunicate)), MainMenu.SequenceState.SchemeCommunicate);
			}
			else if (this.m_communicateFlag.Test(2) && !this.m_communicateFlag.Test(3))
			{
				this.m_communicateFlag.Set(3, true);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMsgBoxCommunicate)), MainMenu.SequenceState.MsgBoxCommunicate);
			}
			else if (this.m_communicateFlag.Test(7))
			{
				this.m_communicateFlag.Set(8, true);
				this.m_communicateFlag.Set(7, false);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateDayCrossCommunicate)), MainMenu.SequenceState.DayCrossCommunicate);
			}
			else if (this.m_communicateFlag.Test(9))
			{
				this.m_communicateFlag.Set(9, false);
				this.ChangeState(new TinyFsmState(new EventFunction(this.MenuStateLoadEventResource)), MainMenu.SequenceState.LoadEventResource);
			}
			else
			{
				this.m_communicateFlag.Set(7, false);
				this.m_communicateFlag.Set(8, false);
				this.m_flags.Set(20, true);
				if (this.m_flags.Test(18))
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateFirstLoginBonusWindow)), MainMenu.SequenceState.FirstLoginBonusWindow);
				}
				else if (this.m_flags.Test(17))
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoginBonusWindow)), MainMenu.SequenceState.LoginBonusWindow);
				}
				else if (this.m_flags.Test(16))
				{
					HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.DAILY_CHALLENGE, false);
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateDailyMissionWindow)), MainMenu.SequenceState.DailyMissionWindow);
				}
				else if (SingletonGameObject<DailyBattleManager>.Instance != null && SingletonGameObject<DailyBattleManager>.Instance.currentRewardFlag)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateDailyBattleRewardWindow)), MainMenu.SequenceState.DailyBattleRewardWindow);
				}
				else if (this.HaveNoticeInfo())
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateInformationWindow)), MainMenu.SequenceState.InformationWindow);
				}
				else if (HudMenuUtility.IsTutorialCharaLevelUp())
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialCharaLevelUpMenuStart)), MainMenu.SequenceState.TutorialCharaLevelUpMenuStart);
				}
				else if (HudMenuUtility.IsRouletteTutorial())
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialMenuRoulette)), MainMenu.SequenceState.TutorialMenuRoulette);
				}
				else if (HudMenuUtility.IsRecommendReviewTutorial())
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRecommendReview)), MainMenu.SequenceState.RecommendReview);
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenu)), MainMenu.SequenceState.Main);
				}
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x00073E80 File Offset: 0x00072080
	private TinyFsmState StateTickerCommunicate(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetTicker(base.gameObject);
			}
			else
			{
				this.m_communicateFlag.Set(0, false);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (!this.m_communicateFlag.Test(0))
			{
				HudMenuUtility.SendMsgTickerUpdate();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x00073F38 File Offset: 0x00072138
	private TinyFsmState StateMsgBoxCommunicate(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetMessageList(base.gameObject);
			}
			else
			{
				this.m_communicateFlag.Set(2, false);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (!this.m_communicateFlag.Test(2))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x00073FEC File Offset: 0x000721EC
	private TinyFsmState StateSchemeCommunicate(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			this.ClearUrlScheme();
			return TinyFsmState.End();
		case 1:
			if (ServerAtomSerial.GetSerialFromScheme(this.GetUrlSchemeStr(), ref this.m_atomCampain, ref this.m_atomSerial))
			{
				this.CreateSchemeCheckWinow();
			}
			else
			{
				this.m_communicateFlag.Set(4, false);
			}
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("SchemeCheck") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				bool new_user = true;
				SystemSaveManager instance = SystemSaveManager.Instance;
				if (instance != null)
				{
					SystemData systemdata = instance.GetSystemdata();
					if (systemdata != null)
					{
						new_user = systemdata.IsNewUser();
					}
				}
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				loggedInServerInterface.RequestServerAtomSerial(this.m_atomCampain, this.m_atomSerial, new_user, base.gameObject);
			}
			if (!this.m_communicateFlag.Test(4))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateResultSchemeCommunicate)), MainMenu.SequenceState.ResultSchemeCommunicate);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x00074110 File Offset: 0x00072310
	private TinyFsmState StateResultSchemeCommunicate(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			this.ClearUrlScheme();
			return TinyFsmState.End();
		case 1:
			this.CreateSchemeResultWinow();
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("SchemeResult") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x000741AC File Offset: 0x000723AC
	private TinyFsmState StateDayCrossCommunicate(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_callBackFlag.Reset();
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null && ServerDayCrossWatcher.Instance != null)
			{
				ServerDayCrossWatcher.Instance.UpdateClientInfosByDayCross(new ServerDayCrossWatcher.UpdateInfoCallback(this.DataCrossCallBack));
				ServerDayCrossWatcher.Instance.UpdateDailyMissionForOneDay(new ServerDayCrossWatcher.UpdateInfoCallback(this.DailyMissionForOneDataCallBack));
				ServerDayCrossWatcher.Instance.UpdateDailyMissionInfoByChallengeEnd(new ServerDayCrossWatcher.UpdateInfoCallback(this.DailyMissionChallengeEndCallBack));
				ServerDayCrossWatcher.Instance.UpdateLoginBonusEnd(new ServerDayCrossWatcher.UpdateInfoCallback(this.LoginBonusUpdateCallBack));
			}
			else
			{
				this.m_callBackFlag.Set(0, true);
				this.m_callBackFlag.Set(2, true);
				this.m_callBackFlag.Set(4, true);
				this.m_callBackFlag.Set(7, true);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_callBackFlag.Test(0) && this.m_callBackFlag.Test(2) && this.m_callBackFlag.Test(4) && this.m_callBackFlag.Test(7))
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x00074324 File Offset: 0x00072524
	private void CreateSchemeCheckWinow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "SchemeCheck",
			buttonType = GeneralWindow.ButtonType.Ok,
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "atom_check"),
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "atom_check_caption")
		});
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x00074384 File Offset: 0x00072584
	private void CreateSchemeResultWinow()
	{
		string message = string.Empty;
		string caption = string.Empty;
		if (this.m_communicateFlag.Test(5))
		{
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", this.m_atomInvalidTextId);
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "atom_failure_caption");
		}
		else
		{
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "atom_present_get");
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "atom_success_caption");
		}
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "SchemeResult",
			buttonType = GeneralWindow.ButtonType.Ok,
			message = message,
			caption = caption
		});
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x0007442C File Offset: 0x0007262C
	private bool JudgeMainMenuReceiveEvent(MsgMenuSequence msg_sequence)
	{
		switch (msg_sequence.Sequenece)
		{
		case MsgMenuSequence.SequeneceType.TITLE:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateTitle)), MainMenu.SequenceState.Title);
			return true;
		case MsgMenuSequence.SequeneceType.STAGE:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateStage)), MainMenu.SequenceState.Stage);
			return true;
		case MsgMenuSequence.SequeneceType.PRESENT_BOX:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StatePresentBox)), MainMenu.SequenceState.PresentBox);
			return true;
		case MsgMenuSequence.SequeneceType.DAILY_CHALLENGE:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateDailyMissionWindow)), MainMenu.SequenceState.DailyMissionWindow);
			return true;
		case MsgMenuSequence.SequeneceType.DAILY_BATTLE:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateDailyBattle)), MainMenu.SequenceState.DailyBattle);
			return true;
		case MsgMenuSequence.SequeneceType.CHARA_MAIN:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainCharaSelect)), MainMenu.SequenceState.CharaSelect);
			return true;
		case MsgMenuSequence.SequeneceType.CHAO:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateChaoSelect)), MainMenu.SequenceState.ChaoSelect);
			return true;
		case MsgMenuSequence.SequeneceType.OPTION:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateOption)), MainMenu.SequenceState.Option);
			return true;
		case MsgMenuSequence.SequeneceType.INFOMATION:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateInfomation)), MainMenu.SequenceState.Infomation);
			return true;
		case MsgMenuSequence.SequeneceType.ROULETTE:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
			return true;
		case MsgMenuSequence.SequeneceType.SHOP:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateShop)), MainMenu.SequenceState.Shop);
			return true;
		case MsgMenuSequence.SequeneceType.EPISODE:
		{
			StageModeManager instance = StageModeManager.Instance;
			if (instance != null)
			{
				instance.StageMode = StageModeManager.Mode.ENDLESS;
			}
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadMileageXml)), MainMenu.SequenceState.LoadMileageXml);
			return true;
		}
		case MsgMenuSequence.SequeneceType.EPISODE_PLAY:
		case MsgMenuSequence.SequeneceType.MAIN_PLAY_BUTTON:
		{
			StageModeManager instance2 = StageModeManager.Instance;
			if (instance2 != null)
			{
				instance2.StageMode = StageModeManager.Mode.ENDLESS;
			}
			this.ChangeState(new TinyFsmState(new EventFunction(this.MenuStatePlayButton)), MainMenu.SequenceState.PlayButton);
			return true;
		}
		case MsgMenuSequence.SequeneceType.EPISODE_RANKING:
		case MsgMenuSequence.SequeneceType.QUICK_RANKING:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateRanking)), MainMenu.SequenceState.Ranking);
			return true;
		case MsgMenuSequence.SequeneceType.QUICK:
		{
			StageModeManager instance3 = StageModeManager.Instance;
			if (instance3 != null)
			{
				instance3.StageMode = StageModeManager.Mode.QUICK;
			}
			this.ChangeState(new TinyFsmState(new EventFunction(this.MenuStatePlayButton)), MainMenu.SequenceState.PlayButton);
			return true;
		}
		case MsgMenuSequence.SequeneceType.BACK:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckBackTitle)), MainMenu.SequenceState.CheckBackTitle);
			return true;
		}
		return false;
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x000746D0 File Offset: 0x000728D0
	private void ServerGetTicker_Succeeded(MsgGetTickerDataSucceed msg)
	{
		this.SetTickerCommunicate();
		this.m_communicateFlag.Set(0, false);
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x000746E8 File Offset: 0x000728E8
	private void ServerGetTicker_Failed(MsgServerConnctFailed msg)
	{
		this.m_communicateFlag.Set(0, false);
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x000746F8 File Offset: 0x000728F8
	private void ServerGetMessageList_Succeeded(MsgGetMessageListSucceed msg)
	{
		if (this.m_communicateFlag.Test(2))
		{
			this.SetMsgBoxCommunicate(false);
			this.m_communicateFlag.Set(2, false);
		}
		this.m_flags.Set(19, false);
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x0007473C File Offset: 0x0007293C
	private void ServerGetMessageList_Failed(MsgServerConnctFailed msg)
	{
		this.m_communicateFlag.Set(2, false);
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x0007474C File Offset: 0x0007294C
	private void ServerAtomSerial_Succeeded(MsgSendAtomSerialSucceed msg)
	{
		this.m_communicateFlag.Set(4, false);
		this.m_communicateFlag.Set(5, false);
		this.SetMsgBoxCommunicate(true);
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x00074774 File Offset: 0x00072974
	private void ServerAtomSerial_Failed(MsgServerConnctFailed msg)
	{
		this.m_communicateFlag.Set(4, false);
		this.m_communicateFlag.Set(5, true);
		this.m_atomInvalidTextId = "atom_invalid_serial";
		if (msg != null && msg.m_status == ServerInterface.StatusCode.UsedSerialCode)
		{
			this.m_atomInvalidTextId = "atom_used_serial";
		}
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x000747CC File Offset: 0x000729CC
	private void DataCrossCallBack(ServerDayCrossWatcher.MsgDayCross msg)
	{
		if (msg != null && msg.ServerConnect)
		{
			this.m_callBackFlag.Set(1, true);
		}
		this.m_callBackFlag.Set(0, true);
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000747FC File Offset: 0x000729FC
	private void DailyMissionForOneDataCallBack(ServerDayCrossWatcher.MsgDayCross msg)
	{
		if (msg != null && msg.ServerConnect)
		{
			this.m_callBackFlag.Set(3, true);
		}
		this.m_callBackFlag.Set(2, true);
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x0007482C File Offset: 0x00072A2C
	private void DailyMissionChallengeEndCallBack(ServerDayCrossWatcher.MsgDayCross msg)
	{
		if (msg != null && msg.ServerConnect)
		{
			this.m_callBackFlag.Set(5, true);
		}
		this.m_callBackFlag.Set(4, true);
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x0007485C File Offset: 0x00072A5C
	private void LoginBonusUpdateCallBack(ServerDayCrossWatcher.MsgDayCross msg)
	{
		if (msg != null)
		{
			if (msg.ServerConnect)
			{
				this.m_callBackFlag.Set(6, true);
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					ServerLoginBonusData loginBonusData = ServerInterface.LoginBonusData;
					if (loginBonusData != null)
					{
						if (!loginBonusData.isGetLoginBonusToday())
						{
							loggedInServerInterface.RequestServerLoginBonusSelect(loginBonusData.m_rewardId, loginBonusData.m_rewardDays, 0, loginBonusData.m_firstRewardDays, 0, base.gameObject);
						}
						else
						{
							this.m_callBackFlag.Set(7, true);
						}
					}
				}
			}
			else
			{
				this.m_callBackFlag.Set(7, true);
			}
		}
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x000748F8 File Offset: 0x00072AF8
	private void ServerLoginBonusSelect_Succeeded(MsgLoginBonusSelectSucceed msg)
	{
		bool flag = false;
		if (msg.m_loginBonusReward != null)
		{
			this.m_flags.Set(17, true);
			flag = true;
		}
		if (msg.m_firstLoginBonusReward != null)
		{
			this.m_flags.Set(18, true);
			flag = true;
		}
		this.m_callBackFlag.Set(7, true);
		if (flag)
		{
			this.SetMsgBoxCommunicate(true);
		}
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x0007495C File Offset: 0x00072B5C
	private bool IsTickerCommunicate()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerTickerInfo tickerInfo = ServerInterface.TickerInfo;
			if (tickerInfo != null && tickerInfo.ExistNewData)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x00074998 File Offset: 0x00072B98
	private bool IsMsgBoxCommunicate()
	{
		return SaveDataManager.Instance != null && SaveDataManager.Instance.ConnectData != null && SaveDataManager.Instance.ConnectData.ReplaceMessageBox;
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x000749D8 File Offset: 0x00072BD8
	private bool IsSchemeCommunicate()
	{
		if (Binding.Instance != null)
		{
			string urlSchemeStr = Binding.Instance.GetUrlSchemeStr();
			return !string.IsNullOrEmpty(urlSchemeStr);
		}
		return false;
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x00074A08 File Offset: 0x00072C08
	private string GetUrlSchemeStr()
	{
		if (Binding.Instance != null)
		{
			return Binding.Instance.GetUrlSchemeStr();
		}
		return string.Empty;
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x00074A24 File Offset: 0x00072C24
	private void ClearUrlScheme()
	{
		if (Binding.Instance != null)
		{
			Binding.Instance.ClearUrlSchemeStr();
		}
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x00074A3C File Offset: 0x00072C3C
	private bool IsChangeDataVersion()
	{
		if (ServerInterface.LoginState != null)
		{
			if (ServerInterface.LoginState.IsChangeDataVersion)
			{
				return true;
			}
			if (ServerInterface.LoginState.IsChangeAssetsVersion)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x00074A78 File Offset: 0x00072C78
	private void SetTickerCommunicate()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerTickerInfo tickerInfo = ServerInterface.TickerInfo;
			if (tickerInfo != null)
			{
				tickerInfo.ExistNewData = false;
			}
		}
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x00074AAC File Offset: 0x00072CAC
	private void SetMsgBoxCommunicate(bool flag)
	{
		if (SaveDataManager.Instance != null && SaveDataManager.Instance.ConnectData != null)
		{
			SaveDataManager.Instance.ConnectData.ReplaceMessageBox = flag;
		}
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x00074AE8 File Offset: 0x00072CE8
	private void CheckLimitEvent()
	{
		if (EventManager.Instance == null)
		{
			return;
		}
		if (EventManager.Instance.IsStandby())
		{
			this.m_flags.Set(22, true);
			if (EventManager.Instance.IsInEvent())
			{
				EventManager.Instance.CheckEvent();
				if (this.m_eventResourceId > 0 && this.m_eventResourceId != EventManager.Instance.Id)
				{
					this.m_flags.Set(13, true);
				}
				else
				{
					this.m_communicateFlag.Set(9, true);
				}
			}
		}
		else if (EventManager.Instance.Type != EventManager.EventType.UNKNOWN && !EventManager.Instance.IsInEvent())
		{
			EventManager.EventType type = EventManager.Instance.Type;
			EventManager.Instance.CheckEvent();
			if (EventManager.Instance.Type != EventManager.EventType.UNKNOWN && EventManager.Instance.IsInEvent())
			{
				this.m_flags.Set(13, true);
			}
			else
			{
				if (type == EventManager.EventType.QUICK)
				{
					StageModeManager.Instance.DrawQuickStageIndex();
				}
				this.m_communicateFlag.Set(9, true);
				if (AtlasManager.Instance != null)
				{
					AtlasManager.Instance.ResetEventRelaceAtlas();
				}
				if (ResourceManager.Instance != null)
				{
					ResourceManager.Instance.RemoveResources(ResourceCategory.EVENT_RESOURCE);
				}
			}
		}
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x00074C44 File Offset: 0x00072E44
	private void CheckEventParameter()
	{
		this.SetEventStage(false);
		this.CheckLimitEvent();
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x00074C54 File Offset: 0x00072E54
	private void SetMilageStageIndex()
	{
		int episode = 1;
		int chapter = 1;
		int type = 0;
		if (this.m_mileage_map_state != null)
		{
			episode = this.m_mileage_map_state.m_episode;
			chapter = this.m_mileage_map_state.m_chapter;
			type = this.m_mileage_map_state.m_point;
		}
		MileageMapUtility.SetMileageStageIndex(episode, chapter, (PointType)type);
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x00074CA0 File Offset: 0x00072EA0
	private void ServerGetMileageReward_Succeeded(MsgGetMileageRewardSucceed msg)
	{
		MileageMapDataManager instance = MileageMapDataManager.Instance;
		if (instance != null)
		{
			if (msg != null)
			{
				instance.SetRewardData(msg.m_mileageRewardList);
			}
			else
			{
				instance.SetRewardData(ServerInterface.MileageRewardList);
			}
		}
		this.m_flags.Set(12, true);
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x00074CF0 File Offset: 0x00072EF0
	private void ServerGetMileageReward_Failed()
	{
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x00074CF4 File Offset: 0x00072EF4
	private TinyFsmState StateOption(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (!this.m_flags.Test(0))
			{
				ConnectAlertMaskUI.EndScreen(new Action(this.OnFinishedFadeInCallback));
			}
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.STAGE)
				{
					this.m_flags.Set(15, true);
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateStage)), MainMenu.SequenceState.Stage);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.TITLE)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTitle)), MainMenu.SequenceState.Title);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x00074E08 File Offset: 0x00073008
	private TinyFsmState MenuStatePlayButton(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_cautionType = this.GetCautionType();
			return TinyFsmState.End();
		case 4:
			if (this.m_cautionType != MainMenu.CautionType.CHALLENGE_COUNT)
			{
				this.m_bossChallenge = MileageMapUtility.IsBossStage();
				HudMenuUtility.SendVirtualNewItemSelectClicked(HudMenuUtility.ITEM_SELECT_MODE.NORMAL);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StatePlayItem)), MainMenu.SequenceState.PlayItem);
			}
			else
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateChallengeDisplyWindow)), MainMenu.SequenceState.ChallengeDisplyWindow);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x00074EC0 File Offset: 0x000730C0
	private TinyFsmState StateChallengeDisplyWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CreateStageCautionWindow();
			return TinyFsmState.End();
		case 4:
		{
			MainMenu.PressedButtonType pressedButtonType = this.m_pressedButtonType;
			switch (pressedButtonType + 1)
			{
			case MainMenu.PressedButtonType.BACK:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHALLENGE_TO_SHOP, false);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateShop)), MainMenu.SequenceState.Shop);
				break;
			case MainMenu.PressedButtonType.CANCEL:
			case MainMenu.PressedButtonType.NUM:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.ITEM_BACK, false);
				if (this.m_flags.Test(28))
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadMileageXml)), MainMenu.SequenceState.LoadMileageXml);
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				break;
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x00074FC8 File Offset: 0x000731C8
	private TinyFsmState StatePresentBox(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.CHARA_MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainCharaSelect)), MainMenu.SequenceState.CharaSelect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.CHAO)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateChaoSelect)), MainMenu.SequenceState.ChaoSelect);
				}
				else if (msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.SHOP)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateShop)), MainMenu.SequenceState.Shop);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000750D4 File Offset: 0x000732D4
	private TinyFsmState StateUserNameSetting(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("window_name_setting", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 4:
			if (this.m_buttonEventResourceLoader.IsLoaded)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameSettingDisplay)), MainMenu.SequenceState.UserNameSettingDisplay);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x00075198 File Offset: 0x00073398
	private TinyFsmState StateUserNameSettingDisplay(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = false;
			return TinyFsmState.End();
		case 1:
			if (this.m_userNameSetting == null)
			{
				GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
				if (menuAnimUIObject != null)
				{
					this.m_userNameSetting = GameObjectUtil.FindChildGameObjectComponent<FirstLaunchUserName>(menuAnimUIObject, "window_name_setting");
				}
			}
			if (this.m_userNameSetting != null)
			{
				this.m_userNameSetting.Setup(string.Empty);
				this.m_userNameSetting.PlayStart();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_userNameSetting != null && this.m_userNameSetting.IsEndPlay)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x00075290 File Offset: 0x00073490
	private TinyFsmState StateAgeVerification(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("AgeVerificationWindow", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			this.m_flags.Set(18, false);
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 4:
			if (this.m_buttonEventResourceLoader.IsLoaded)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateAgeVerificationDisplay)), MainMenu.SequenceState.AgeVerificationDisplay);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x00075364 File Offset: 0x00073564
	private TinyFsmState StateAgeVerificationDisplay(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = false;
			return TinyFsmState.End();
		case 1:
			if (this.m_ageVerification == null)
			{
				GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
				if (menuAnimUIObject != null)
				{
					this.m_ageVerification = GameObjectUtil.FindChildGameObjectComponent<AgeVerification>(menuAnimUIObject, "AgeVerificationWindow");
				}
			}
			if (this.m_ageVerification != null)
			{
				this.m_ageVerification.Setup(string.Empty);
				this.m_ageVerification.PlayStart();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_ageVerification != null && this.m_ageVerification.IsEnd)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x0007545C File Offset: 0x0007365C
	private TinyFsmState StateQuickModeRankUp(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
			this.m_buttonEventResourceLoader = null;
			return TinyFsmState.End();
		case 1:
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("RankingResultBitWindow", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			return TinyFsmState.End();
		case 4:
			if (this.m_buttonEventResourceLoader.IsLoaded)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateQuickModeRankUpDisplay)), MainMenu.SequenceState.QuickModeRankUpDisplay);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x0007551C File Offset: 0x0007371C
	private TinyFsmState StateQuickModeRankUpDisplay(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			RankingUtil.ShowRankingChangeWindow(RankingUtil.RankingMode.QUICK);
			return TinyFsmState.End();
		case 4:
			if (RankingUtil.IsEndRankingChangeWindow())
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x0007559C File Offset: 0x0007379C
	private TinyFsmState StateRanking(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null && msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.MAIN)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x00075628 File Offset: 0x00073828
	private TinyFsmState StateRecommendReview(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_fristLaunchRecommendReview != null)
			{
				UnityEngine.Object.Destroy(this.m_fristLaunchRecommendReview.gameObject);
			}
			return TinyFsmState.End();
		case 1:
			this.CreateFirstLaunchRecommendReview();
			return TinyFsmState.End();
		case 4:
			if (this.m_fristLaunchRecommendReview != null)
			{
				if (this.m_startLauncherRecommendReviewFlag)
				{
					if (this.m_fristLaunchRecommendReview.IsEndPlay)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
					}
				}
				else
				{
					this.m_fristLaunchRecommendReview.Setup("Camera/menu_Anim/MainMenuUI4/Anchor_5_MC");
					this.m_fristLaunchRecommendReview.PlayStart();
					this.m_startLauncherRecommendReviewFlag = true;
				}
			}
			else
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x00075730 File Offset: 0x00073930
	private void CreateFirstLaunchRecommendReview()
	{
		if (this.m_fristLaunchRecommendReview == null)
		{
			GameObject gameObject = new GameObject();
			if (gameObject != null)
			{
				gameObject.name = "FirstLaunchRecommendReview";
				gameObject.AddComponent<FirstLaunchRecommendReview>();
				this.m_fristLaunchRecommendReview = gameObject.GetComponent<FirstLaunchRecommendReview>();
			}
		}
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x00075780 File Offset: 0x00073980
	private TinyFsmState StateRoulette(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				MsgMenuSequence.SequeneceType sequenece = msgMenuSequence.Sequenece;
				if (sequenece == MsgMenuSequence.SequeneceType.MAIN)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x00075818 File Offset: 0x00073A18
	private TinyFsmState StateShop(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null)
			{
				MsgMenuSequence.SequeneceType sequenece = msgMenuSequence.Sequenece;
				switch (sequenece)
				{
				case MsgMenuSequence.SequeneceType.PRESENT_BOX:
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePresentBox)), MainMenu.SequenceState.PresentBox);
					break;
				default:
					if (sequenece == MsgMenuSequence.SequeneceType.MAIN)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
					}
					break;
				case MsgMenuSequence.SequeneceType.CHARA_MAIN:
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainCharaSelect)), MainMenu.SequenceState.CharaSelect);
					break;
				case MsgMenuSequence.SequeneceType.CHAO:
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateChaoSelect)), MainMenu.SequenceState.ChaoSelect);
					break;
				case MsgMenuSequence.SequeneceType.PLAY_ITEM:
				case MsgMenuSequence.SequeneceType.EPISODE_PLAY:
				case MsgMenuSequence.SequeneceType.QUICK:
				case MsgMenuSequence.SequeneceType.PLAY_AT_EPISODE_PAGE:
					this.ChangeState(new TinyFsmState(new EventFunction(this.MenuStatePlayButton)), MainMenu.SequenceState.PlayButton);
					break;
				case MsgMenuSequence.SequeneceType.OPTION:
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateOption)), MainMenu.SequenceState.Option);
					break;
				case MsgMenuSequence.SequeneceType.INFOMATION:
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateInfomation)), MainMenu.SequenceState.Infomation);
					break;
				case MsgMenuSequence.SequeneceType.ROULETTE:
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
					break;
				case MsgMenuSequence.SequeneceType.EPISODE:
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadMileageXml)), MainMenu.SequenceState.LoadMileageXml);
					break;
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000759FC File Offset: 0x00073BFC
	private TinyFsmState StateCheckStage(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_cautionType = this.GetCautionType();
			return TinyFsmState.End();
		case 4:
			if (this.m_cautionType == MainMenu.CautionType.NON)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateStage)), MainMenu.SequenceState.Stage);
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.GO_STAGE, false);
			}
			else
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateStageCautionWindow)), MainMenu.SequenceState.CautionStage);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x00075AA8 File Offset: 0x00073CA8
	private TinyFsmState StateStageCautionWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CreateStageCautionWindow();
			return TinyFsmState.End();
		case 4:
		{
			MainMenu.PressedButtonType pressedButtonType = this.m_pressedButtonType;
			switch (pressedButtonType + 1)
			{
			case MainMenu.PressedButtonType.GOTO_SHOP:
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateStage)), MainMenu.SequenceState.Stage);
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.GO_STAGE, false);
				break;
			case MainMenu.PressedButtonType.BACK:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHALLENGE_TO_SHOP, false);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateShop)), MainMenu.SequenceState.Shop);
				break;
			case MainMenu.PressedButtonType.CANCEL:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.FORCE_MAIN_BACK, true);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenuConnect)), MainMenu.SequenceState.MainConnect);
				break;
			case MainMenu.PressedButtonType.NUM:
				this.ChangeState(new TinyFsmState(new EventFunction(this.StatePlayItem)), MainMenu.SequenceState.PlayItem);
				break;
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x00075BC4 File Offset: 0x00073DC4
	private TinyFsmState StateStage(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_flags.Set(3, true);
			return TinyFsmState.End();
		case 4:
			this.LoadEventFaceTexture();
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateFadeOut)), MainMenu.SequenceState.FadeOut);
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x00075C48 File Offset: 0x00073E48
	private void LoadEventFaceTexture()
	{
		if (this.m_flags.Test(4) || this.m_flags.Test(5))
		{
			string eventResourceLoadingTextureName = EventUtility.GetEventResourceLoadingTextureName();
			if (eventResourceLoadingTextureName != null)
			{
				this.CeateSceneLoader();
				this.AddSceneLoader(eventResourceLoadingTextureName);
			}
		}
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x00075C90 File Offset: 0x00073E90
	private TinyFsmState StateStartMessage(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(8);
			}
			return TinyFsmState.End();
		case 1:
			TimeProfiler.StartCountTime("StateStartMessage");
			RouletteUtility.rouletteDefault = RouletteCategory.PREMIUM;
			this.SetMilageStageIndex();
			HudMenuUtility.OnForceDisableShopButton(true);
			return TinyFsmState.End();
		case 4:
			TimeProfiler.EndCountTime("StateStartMessage");
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateRankingWait)), MainMenu.SequenceState.RankingWait);
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x00075D44 File Offset: 0x00073F44
	private TinyFsmState StateRankingWait(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(9);
			}
			return TinyFsmState.End();
		case 1:
		{
			TimeProfiler.StartCountTime("StateRankingWait");
			this.m_rankingCallBack = false;
			this.m_eventRankingCallBack = false;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null && SingletonGameObject<RankingManager>.Instance != null)
			{
				SingletonGameObject<RankingManager>.Instance.Init(new RankingManager.CallbackRankingData(this.NormalRankingDataCallback), new RankingManager.CallbackRankingData(this.EventDataCallback));
			}
			else
			{
				this.m_rankingCallBack = true;
				this.m_eventRankingCallBack = true;
			}
			GeneralUtil.SetDailyBattleBtnIcon(null, "Btn_2_battle");
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_rankingCallBack && this.m_eventRankingCallBack && this.IsLoadedAtlas() && this.CheckSceneLoad())
			{
				this.DestroySceneLoader();
				this.SetReplaceAtlas();
				TimeProfiler.EndCountTime("StateRankingWait");
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateFadeIn)), MainMenu.SequenceState.FadeIn);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x00075E90 File Offset: 0x00074090
	private void NormalRankingDataCallback(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData)
	{
		this.m_rankingCallBack = true;
		RankingUI.Setup();
		bool flag = false;
		if (EventManager.Instance != null && EventManager.Instance.IsInEvent() && EventManager.Instance.Type == EventManager.EventType.SPECIAL_STAGE)
		{
			flag = true;
		}
		if (!flag)
		{
			this.m_eventRankingCallBack = true;
		}
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x00075EEC File Offset: 0x000740EC
	private void EventDataCallback(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData)
	{
		this.m_eventRankingCallBack = true;
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x00075EF8 File Offset: 0x000740F8
	private TinyFsmState StateTitle(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_flags.Set(3, false);
			this.m_flags.Set(4, false);
			return TinyFsmState.End();
		case 4:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateFadeOut)), MainMenu.SequenceState.FadeOut);
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x00075F84 File Offset: 0x00074184
	private TinyFsmState StateCheckBackTitle(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CreateTitleBackWindow();
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("BackTitle") && GeneralWindow.IsButtonPressed)
			{
				if (GeneralWindow.IsYesButtonPressed)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTitle)), MainMenu.SequenceState.Title);
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainMenu)), MainMenu.SequenceState.Main);
				}
				GeneralWindow.Close();
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x00076040 File Offset: 0x00074240
	private TinyFsmState StateTutorialCharaLevelUpMenuStart(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CreateCharaLevelUpWinow();
			BackKeyManager.TutorialFlag = true;
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("chara_level_up") && GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialCharaLevelUpMenuMoveChara)), MainMenu.SequenceState.TutorialCharaLevelUpMenuMoveChara);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x000760DC File Offset: 0x000742DC
	private TinyFsmState StateTutorialCharaLevelUpMenuMoveChara(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.TutorialFlag = false;
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.CHAOSELECT_MAIN);
			return TinyFsmState.End();
		case 1:
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.CHAOSELECT_MAIN);
			return TinyFsmState.End();
		default:
			if (signal != 102)
			{
				return TinyFsmState.End();
			}
			this.CreateTitleBackWindow();
			return TinyFsmState.End();
		case 4:
			this.CheckTutoralWindow();
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null && msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.CHARA_MAIN)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateMainCharaSelect)), MainMenu.SequenceState.CharaSelect);
			}
			return TinyFsmState.End();
		}
		}
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x00076198 File Offset: 0x00074398
	private void CreateCharaLevelUpWinow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "chara_level_up",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextUtility.GetCommonText("MainMenu", "chara_level_up_move_explan_caption"),
			message = TextUtility.GetCommonText("MainMenu", "chara_level_up_move_explan")
		});
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x000761F4 File Offset: 0x000743F4
	private TinyFsmState StateTutorialMenuRoulette(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.MAINMENU_ROULETTE);
			BackKeyManager.InvalidFlag = false;
			return TinyFsmState.End();
		case 1:
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.MAINMENU_ROULETTE);
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		default:
			if (signal != 102)
			{
				return TinyFsmState.End();
			}
			this.CreateTitleBackWindow();
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			MsgMenuSequence msgMenuSequence = fsm_event.GetMessage as MsgMenuSequence;
			if (msgMenuSequence != null && msgMenuSequence.Sequenece == MsgMenuSequence.SequeneceType.ROULETTE)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateRoulette)), MainMenu.SequenceState.Roulette);
			}
			return TinyFsmState.End();
		}
		}
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x000762B0 File Offset: 0x000744B0
	private bool CheckTutorialRoulette()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		return loggedInServerInterface != null && RouletteUtility.isTutorial;
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x000762D8 File Offset: 0x000744D8
	private void SetTutorialFlagToMainMenuUI()
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnSetTutorial", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x000762EC File Offset: 0x000744EC
	private TinyFsmState StateVersionChangeWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CreateVersionChangeWindow(false);
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("VersionChange") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateTitle)), MainMenu.SequenceState.Title);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x00076384 File Offset: 0x00074584
	private TinyFsmState StateEventResourceChangeWindow(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.CreateVersionChangeWindow(true);
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("VersionChange") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateTitle)), MainMenu.SequenceState.Title);
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x0007641C File Offset: 0x0007461C
	private void CreateVersionChangeWindow(bool eventFlag)
	{
		string name = "VersionChange";
		if (!GeneralWindow.IsCreated(name))
		{
			if (eventFlag)
			{
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					name = name,
					buttonType = GeneralWindow.ButtonType.Ok,
					caption = TextUtility.GetCommonText("Option", "take_over_attention"),
					message = TextUtility.GetCommonText("MainMenu", "title_back_message")
				});
			}
			else
			{
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					name = name,
					buttonType = GeneralWindow.ButtonType.Ok,
					caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_reboot_app_caption"),
					message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_reboot_app")
				});
			}
		}
	}

	// Token: 0x040011E2 RID: 4578
	private const string STAGE_MODE_INFO = "StageInfo";

	// Token: 0x040011E3 RID: 4579
	public bool m_debugInfo;

	// Token: 0x040011E4 RID: 4580
	private TinyFsmBehavior m_fsm_behavior;

	// Token: 0x040011E5 RID: 4581
	private GameObject m_stage_info_obj;

	// Token: 0x040011E6 RID: 4582
	private MainMenuWindow m_main_menu_window;

	// Token: 0x040011E7 RID: 4583
	private GameObject m_scene_loader_obj;

	// Token: 0x040011E8 RID: 4584
	private List<int> m_request_face_list;

	// Token: 0x040011E9 RID: 4585
	private List<int> m_request_bg_list;

	// Token: 0x040011EA RID: 4586
	private ButtonEventResourceLoader m_buttonEventResourceLoader;

	// Token: 0x040011EB RID: 4587
	private ServerMileageMapState m_mileage_map_state;

	// Token: 0x040011EC RID: 4588
	private ServerMileageMapState m_prev_mileage_map_state;

	// Token: 0x040011ED RID: 4589
	private ResultData m_stageResultData;

	// Token: 0x040011EE RID: 4590
	private int m_eventResourceId;

	// Token: 0x040011EF RID: 4591
	private SendApollo m_sendApollo;

	// Token: 0x040011F0 RID: 4592
	private ButtonEvent m_buttonEvent;

	// Token: 0x040011F1 RID: 4593
	private bool m_bossChallenge;

	// Token: 0x040011F2 RID: 4594
	private Bitset32 m_flags;

	// Token: 0x040011F3 RID: 4595
	private HudProgressBar m_progressBar;

	// Token: 0x040011F4 RID: 4596
	private MainMenu.CautionType m_cautionType;

	// Token: 0x040011F5 RID: 4597
	private MainMenu.PressedButtonType m_pressedButtonType;

	// Token: 0x040011F6 RID: 4598
	private readonly MainMenu.CollisionType[] COLLISION_TYPE_TABLE;

	// Token: 0x040011F7 RID: 4599
	private bool m_alertBtnFlag;

	// Token: 0x040011F8 RID: 4600
	private bool m_eventConnectSkip;

	// Token: 0x040011F9 RID: 4601
	private EasySnsFeed m_easySnsFeed;

	// Token: 0x040011FA RID: 4602
	private DailyBattleRewardWindow m_dailyBattleRewardWindow;

	// Token: 0x040011FB RID: 4603
	private DailyWindowUI m_dailyWindowUI;

	// Token: 0x040011FC RID: 4604
	private bool m_episodeLoaded;

	// Token: 0x040011FD RID: 4605
	private ButtonInfoTable.ButtonType m_ButtonOfNextMenu;

	// Token: 0x040011FE RID: 4606
	private RankingResultLeague m_rankingResultLeagueWindow;

	// Token: 0x040011FF RID: 4607
	private RankingResultWorldRanking m_eventRankingResult;

	// Token: 0x04001200 RID: 4608
	private NetNoticeItem m_currentResultInfo;

	// Token: 0x04001201 RID: 4609
	private NetNoticeItem m_eventRankingResultInfo;

	// Token: 0x04001202 RID: 4610
	private List<NetNoticeItem> m_rankingResultList;

	// Token: 0x04001203 RID: 4611
	private ServerInformationWindow m_serverInformationWindow;

	// Token: 0x04001204 RID: 4612
	private bool m_is_end_notice_connect;

	// Token: 0x04001205 RID: 4613
	private bool m_connected;

	// Token: 0x04001206 RID: 4614
	private FirstLaunchInviteFriend m_fristLaunchInviteFriend;

	// Token: 0x04001207 RID: 4615
	private bool m_startLauncherInviteFriendFlag;

	// Token: 0x04001208 RID: 4616
	private bool m_eventSpecficText;

	// Token: 0x04001209 RID: 4617
	private List<ResourceSceneLoader.ResourceInfo> m_loadInfo;

	// Token: 0x0400120A RID: 4618
	private LoginBonusWindowUI m_LoginWindowUI;

	// Token: 0x0400120B RID: 4619
	private Bitset32 m_communicateFlag;

	// Token: 0x0400120C RID: 4620
	private Bitset32 m_callBackFlag;

	// Token: 0x0400120D RID: 4621
	private string m_atomCampain;

	// Token: 0x0400120E RID: 4622
	private string m_atomSerial;

	// Token: 0x0400120F RID: 4623
	private string m_atomInvalidTextId;

	// Token: 0x04001210 RID: 4624
	private static int m_debug;

	// Token: 0x04001211 RID: 4625
	private FirstLaunchUserName m_userNameSetting;

	// Token: 0x04001212 RID: 4626
	private AgeVerification m_ageVerification;

	// Token: 0x04001213 RID: 4627
	private FirstLaunchRecommendReview m_fristLaunchRecommendReview;

	// Token: 0x04001214 RID: 4628
	private bool m_startLauncherRecommendReviewFlag;

	// Token: 0x04001215 RID: 4629
	private bool m_rankingCallBack;

	// Token: 0x04001216 RID: 4630
	private bool m_eventRankingCallBack;

	// Token: 0x020002D5 RID: 725
	public enum SequenceState
	{
		// Token: 0x04001218 RID: 4632
		Init,
		// Token: 0x04001219 RID: 4633
		RequestDailyBattle,
		// Token: 0x0400121A RID: 4634
		RequestChaoWheelOption,
		// Token: 0x0400121B RID: 4635
		RequestDayCrossWatcher,
		// Token: 0x0400121C RID: 4636
		RequestMsgList,
		// Token: 0x0400121D RID: 4637
		RequestNoticeInfo,
		// Token: 0x0400121E RID: 4638
		Load,
		// Token: 0x0400121F RID: 4639
		LoadAtlas,
		// Token: 0x04001220 RID: 4640
		StartMessage,
		// Token: 0x04001221 RID: 4641
		RankingWait,
		// Token: 0x04001222 RID: 4642
		FadeIn,
		// Token: 0x04001223 RID: 4643
		Main,
		// Token: 0x04001224 RID: 4644
		MainConnect,
		// Token: 0x04001225 RID: 4645
		TickerCommunicate,
		// Token: 0x04001226 RID: 4646
		MsgBoxCommunicate,
		// Token: 0x04001227 RID: 4647
		SchemeCommunicate,
		// Token: 0x04001228 RID: 4648
		ResultSchemeCommunicate,
		// Token: 0x04001229 RID: 4649
		DayCrossCommunicate,
		// Token: 0x0400122A RID: 4650
		LoadMileageXml,
		// Token: 0x0400122B RID: 4651
		LoadNextMileageXml,
		// Token: 0x0400122C RID: 4652
		LoadMileageTexture,
		// Token: 0x0400122D RID: 4653
		MileageReward,
		// Token: 0x0400122E RID: 4654
		WaitFadeIfNotEndFade,
		// Token: 0x0400122F RID: 4655
		Episode,
		// Token: 0x04001230 RID: 4656
		DailyMissionWindow,
		// Token: 0x04001231 RID: 4657
		LoginBonusWindow,
		// Token: 0x04001232 RID: 4658
		LoginBonusWindowDisplay,
		// Token: 0x04001233 RID: 4659
		FirstLoginBonusWindow,
		// Token: 0x04001234 RID: 4660
		FirstLoginBonusWindowDisplay,
		// Token: 0x04001235 RID: 4661
		PlayButton,
		// Token: 0x04001236 RID: 4662
		ChallengeDisplyWindow,
		// Token: 0x04001237 RID: 4663
		RecommendReview,
		// Token: 0x04001238 RID: 4664
		InformationWindow,
		// Token: 0x04001239 RID: 4665
		InformationWindowCreate,
		// Token: 0x0400123A RID: 4666
		EventRankingResultWindow,
		// Token: 0x0400123B RID: 4667
		RankingResultLeagueWindow,
		// Token: 0x0400123C RID: 4668
		DisplayInformaon,
		// Token: 0x0400123D RID: 4669
		DailyBattle,
		// Token: 0x0400123E RID: 4670
		DailyBattleRewardWindow,
		// Token: 0x0400123F RID: 4671
		DailyBattleRewardWindowDisplay,
		// Token: 0x04001240 RID: 4672
		CharaSelect,
		// Token: 0x04001241 RID: 4673
		ChaoSelect,
		// Token: 0x04001242 RID: 4674
		Option,
		// Token: 0x04001243 RID: 4675
		Ranking,
		// Token: 0x04001244 RID: 4676
		Infomation,
		// Token: 0x04001245 RID: 4677
		Roulette,
		// Token: 0x04001246 RID: 4678
		Shop,
		// Token: 0x04001247 RID: 4679
		PresentBox,
		// Token: 0x04001248 RID: 4680
		PlayItem,
		// Token: 0x04001249 RID: 4681
		TutorialMenuRoulette,
		// Token: 0x0400124A RID: 4682
		TutorialCharaLevelUpMenuStart,
		// Token: 0x0400124B RID: 4683
		TutorialCharaLevelUpMenuMoveChara,
		// Token: 0x0400124C RID: 4684
		BestRecordCheckEnableFeed,
		// Token: 0x0400124D RID: 4685
		BestRecordAskFeed,
		// Token: 0x0400124E RID: 4686
		BestRecordFeed,
		// Token: 0x0400124F RID: 4687
		QuickModeRankUp,
		// Token: 0x04001250 RID: 4688
		QuickModeRankUpDisplay,
		// Token: 0x04001251 RID: 4689
		LoadEventResource,
		// Token: 0x04001252 RID: 4690
		LoadEventTextureResource,
		// Token: 0x04001253 RID: 4691
		EventDisplayProduction,
		// Token: 0x04001254 RID: 4692
		UserNameSetting,
		// Token: 0x04001255 RID: 4693
		UserNameSettingDisplay,
		// Token: 0x04001256 RID: 4694
		AgeVerification,
		// Token: 0x04001257 RID: 4695
		AgeVerificationDisplay,
		// Token: 0x04001258 RID: 4696
		CheckStage,
		// Token: 0x04001259 RID: 4697
		CautionStage,
		// Token: 0x0400125A RID: 4698
		Stage,
		// Token: 0x0400125B RID: 4699
		VersionChangeWindow,
		// Token: 0x0400125C RID: 4700
		EventResourceChangeWindow,
		// Token: 0x0400125D RID: 4701
		Title,
		// Token: 0x0400125E RID: 4702
		CheckBackTitle,
		// Token: 0x0400125F RID: 4703
		FadeOut,
		// Token: 0x04001260 RID: 4704
		End,
		// Token: 0x04001261 RID: 4705
		NUM
	}

	// Token: 0x020002D6 RID: 726
	private enum EventSignal
	{
		// Token: 0x04001263 RID: 4707
		SERVER_GET_EVENT_REWARD_END = 100,
		// Token: 0x04001264 RID: 4708
		SERVER_GET_EVENT_STATE_END,
		// Token: 0x04001265 RID: 4709
		TITLE_BACK
	}

	// Token: 0x020002D7 RID: 727
	private enum Flags
	{
		// Token: 0x04001267 RID: 4711
		FadeIn,
		// Token: 0x04001268 RID: 4712
		FadeOut,
		// Token: 0x04001269 RID: 4713
		ForceBackMainMenu,
		// Token: 0x0400126A RID: 4714
		GoStage,
		// Token: 0x0400126B RID: 4715
		GoSpecialStage,
		// Token: 0x0400126C RID: 4716
		GoRaidBoss,
		// Token: 0x0400126D RID: 4717
		MileageNextMapLoad,
		// Token: 0x0400126E RID: 4718
		MileageProduction,
		// Token: 0x0400126F RID: 4719
		LoginRanking,
		// Token: 0x04001270 RID: 4720
		EndMileageMapProduction,
		// Token: 0x04001271 RID: 4721
		ReceiveMileageState,
		// Token: 0x04001272 RID: 4722
		RecieveDailyChallengeInfo,
		// Token: 0x04001273 RID: 4723
		MileageReward,
		// Token: 0x04001274 RID: 4724
		EventLoadAgain,
		// Token: 0x04001275 RID: 4725
		OptionResourceLoaded,
		// Token: 0x04001276 RID: 4726
		OptionTutorialStage,
		// Token: 0x04001277 RID: 4727
		DailyChallenge,
		// Token: 0x04001278 RID: 4728
		LoginBonus,
		// Token: 0x04001279 RID: 4729
		FirstLoginBonus,
		// Token: 0x0400127A RID: 4730
		MsgBox,
		// Token: 0x0400127B RID: 4731
		EndMainConnect,
		// Token: 0x0400127C RID: 4732
		REQUEST_CHECK_SCHEME,
		// Token: 0x0400127D RID: 4733
		EventWait,
		// Token: 0x0400127E RID: 4734
		EventConnetctBeforeLoadMenu,
		// Token: 0x0400127F RID: 4735
		TuorialWindow,
		// Token: 0x04001280 RID: 4736
		QuickRankingResult,
		// Token: 0x04001281 RID: 4737
		QuickRankingUpProduction,
		// Token: 0x04001282 RID: 4738
		BestRecordFeed,
		// Token: 0x04001283 RID: 4739
		FromMileage,
		// Token: 0x04001284 RID: 4740
		NUM
	}

	// Token: 0x020002D8 RID: 728
	public enum ProgressBarLeaveState
	{
		// Token: 0x04001286 RID: 4742
		IDLE = -1,
		// Token: 0x04001287 RID: 4743
		StateInit,
		// Token: 0x04001288 RID: 4744
		StateRequestDayCrossWatcher,
		// Token: 0x04001289 RID: 4745
		StateRequestDailyBattle,
		// Token: 0x0400128A RID: 4746
		StateRequestChaoWheelOption,
		// Token: 0x0400128B RID: 4747
		StateRequestMsgList,
		// Token: 0x0400128C RID: 4748
		StateRequestNoticeInfo,
		// Token: 0x0400128D RID: 4749
		StateLoad,
		// Token: 0x0400128E RID: 4750
		StateLoadAtlas,
		// Token: 0x0400128F RID: 4751
		StateStartMessage,
		// Token: 0x04001290 RID: 4752
		StateRankingWait,
		// Token: 0x04001291 RID: 4753
		StateEventRankingWait,
		// Token: 0x04001292 RID: 4754
		NUM
	}

	// Token: 0x020002D9 RID: 729
	private enum CautionType
	{
		// Token: 0x04001294 RID: 4756
		NON,
		// Token: 0x04001295 RID: 4757
		CHALLENGE_COUNT,
		// Token: 0x04001296 RID: 4758
		NEW_EVENT,
		// Token: 0x04001297 RID: 4759
		END_EVENT,
		// Token: 0x04001298 RID: 4760
		EVENT_LAST_TIME
	}

	// Token: 0x020002DA RID: 730
	private enum PressedButtonType
	{
		// Token: 0x0400129A RID: 4762
		NONE = -1,
		// Token: 0x0400129B RID: 4763
		NEXT_STATE,
		// Token: 0x0400129C RID: 4764
		GOTO_SHOP,
		// Token: 0x0400129D RID: 4765
		BACK,
		// Token: 0x0400129E RID: 4766
		CANCEL,
		// Token: 0x0400129F RID: 4767
		NUM
	}

	// Token: 0x020002DB RID: 731
	private enum CollisionType
	{
		// Token: 0x040012A1 RID: 4769
		ALERT_BUTTON_ON,
		// Token: 0x040012A2 RID: 4770
		ALERT_BUTTON_OFF,
		// Token: 0x040012A3 RID: 4771
		NON
	}

	// Token: 0x020002DC RID: 732
	private enum ResType
	{
		// Token: 0x040012A5 RID: 4773
		EVENT_COMMON,
		// Token: 0x040012A6 RID: 4774
		EVENT_MENU,
		// Token: 0x040012A7 RID: 4775
		MENU_TOP_TEXTURE,
		// Token: 0x040012A8 RID: 4776
		NUM
	}

	// Token: 0x020002DD RID: 733
	private enum Communicate
	{
		// Token: 0x040012AA RID: 4778
		TICKER,
		// Token: 0x040012AB RID: 4779
		TICKER_END,
		// Token: 0x040012AC RID: 4780
		MSGBOX,
		// Token: 0x040012AD RID: 4781
		MSGBOX_END,
		// Token: 0x040012AE RID: 4782
		SCHEME,
		// Token: 0x040012AF RID: 4783
		SCHEME_FAILD,
		// Token: 0x040012B0 RID: 4784
		VERSION,
		// Token: 0x040012B1 RID: 4785
		DAY_CROSS,
		// Token: 0x040012B2 RID: 4786
		DAY_CROSS_END,
		// Token: 0x040012B3 RID: 4787
		LOAD_EVENT_RESOURCE
	}

	// Token: 0x020002DE RID: 734
	private enum CallBack
	{
		// Token: 0x040012B5 RID: 4789
		DAY_CROSS,
		// Token: 0x040012B6 RID: 4790
		DAY_CROSS_SERVER_CONNECT,
		// Token: 0x040012B7 RID: 4791
		DAILY_MISSION_CHALLENGE_END,
		// Token: 0x040012B8 RID: 4792
		DAILY_MISSION_CHALLENGE_END_SERVER_CONNECT,
		// Token: 0x040012B9 RID: 4793
		DAILY_MISSION_CHALLENGE_INFO_END,
		// Token: 0x040012BA RID: 4794
		DAILY_MISSION_CHALLENGE_INFO_END_SERVER_CONNECT,
		// Token: 0x040012BB RID: 4795
		LOGINBONUS_UPDATE_SERVER_CONNECT,
		// Token: 0x040012BC RID: 4796
		LOGINBONUS_UPDATE_END
	}
}
