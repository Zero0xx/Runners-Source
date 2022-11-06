using System;
using DataTable;
using Message;
using SaveData;
using UnityEngine;

// Token: 0x02000410 RID: 1040
public class HudMenuUtility
{
	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000B8D78 File Offset: 0x000B6F78
	public static HudMenuUtility.ITEM_SELECT_MODE itemSelectMode
	{
		get
		{
			return HudMenuUtility.s_itemSelectMode;
		}
	}

	// Token: 0x06001F0E RID: 7950 RVA: 0x000B8D80 File Offset: 0x000B6F80
	public static GameObject GetMenuAnimUIObject()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			Transform transform = gameObject.transform.FindChild("Camera/menu_Anim");
			if (transform != null)
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	// Token: 0x06001F0F RID: 7951 RVA: 0x000B8DCC File Offset: 0x000B6FCC
	public static GameObject GetCameraUIObject()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			Transform transform = gameObject.transform.FindChild("Camera");
			if (transform != null)
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x000B8E18 File Offset: 0x000B7018
	public static GameObject GetMainMenuUIObject()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			Transform transform = gameObject.transform.FindChild("Camera/menu_Anim/MainMenuUI4");
			if (transform != null)
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x000B8E64 File Offset: 0x000B7064
	public static GameObject GetMainMenuCmnUIObject()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			Transform transform = gameObject.transform.FindChild("Camera/menu_Anim/MainMenuCmnUI");
			if (transform != null)
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x000B8EB0 File Offset: 0x000B70B0
	public static GameObject GetMainMenuGeneralAnchor()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			Transform transform = gameObject.transform.FindChild("Camera/Anchor_5_MC");
			if (transform != null)
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x000B8EFC File Offset: 0x000B70FC
	public static void SetTagHudSaveItem(GameObject obj)
	{
		if (obj != null)
		{
			obj.tag = "HudSaveItem";
		}
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x000B8F18 File Offset: 0x000B7118
	public static void SetTagHudMileageMap(GameObject obj)
	{
		if (obj != null)
		{
			obj.tag = "HudMileageMap";
		}
	}

	// Token: 0x06001F15 RID: 7957 RVA: 0x000B8F34 File Offset: 0x000B7134
	public static void SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType sequenece_type)
	{
		MsgMenuSequence msgMenuSequence = new MsgMenuSequence(sequenece_type);
		if (msgMenuSequence != null)
		{
			GameObjectUtil.SendMessageFindGameObject("MainMenu", "OnMsgReceive", msgMenuSequence, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06001F16 RID: 7958 RVA: 0x000B8F60 File Offset: 0x000B7160
	public static void SendMsgUpdateSaveDataDisplay()
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnUpdateSaveDataDisplay", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F17 RID: 7959 RVA: 0x000B8F74 File Offset: 0x000B7174
	public static void SendMsgInformationDisplay()
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnUpdateInformationDisplay", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x000B8F88 File Offset: 0x000B7188
	public static void SendMsgTickerUpdate()
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnUpdateTickerDisplay", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F19 RID: 7961 RVA: 0x000B8F9C File Offset: 0x000B719C
	public static void SendMsgTickerReset()
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnTickerReset", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x000B8FB0 File Offset: 0x000B71B0
	public static void SendMsgInitMainMenuUI()
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnInitDisplay", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x000B8FC4 File Offset: 0x000B71C4
	public static void SendMsgSetEnableSkipButton(bool enableFlag)
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnSetEnableSkipButton", enableFlag, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x000B8FE0 File Offset: 0x000B71E0
	public static void SendMsgStartRankingProduction()
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnStartRankingProduction", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F1D RID: 7965 RVA: 0x000B8FF4 File Offset: 0x000B71F4
	public static void SendMsgStartLoginRanking(bool fromInformaion = false)
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnStartLoginRankingDisplay", fromInformaion, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x000B9010 File Offset: 0x000B7210
	public static void SendMsgStartTutorialDisplay(bool autoBtn = false)
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnStartTutorialDisplay", autoBtn, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x000B902C File Offset: 0x000B722C
	public static void SendMsgEndTutorialDisplayToHud()
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnEndTutorialDisplay", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x000B9040 File Offset: 0x000B7240
	public static void SendMsgStartNormal()
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuUI4", "OnStartNormalDisplay", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F21 RID: 7969 RVA: 0x000B9054 File Offset: 0x000B7254
	public static void SendMsgUpdateMileageMapDisplayToMileage()
	{
		if (ui_mm_mileage_page.Instance != null)
		{
			ui_mm_mileage_page.Instance.OnUpdateMileageMapDisplay();
		}
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x000B9070 File Offset: 0x000B7270
	public static void SendMsgPrepareMileageMapProduction(ResultData result)
	{
		if (ui_mm_mileage_page.Instance != null)
		{
			ui_mm_mileage_page.Instance.OnPrepareMileageMapProduction(result);
		}
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x000B9090 File Offset: 0x000B7290
	public static GameObject GetGameObjectRanking()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			return GameObjectUtil.FindChildGameObject(gameObject, "ui_mm_ranking_page(Clone)");
		}
		return null;
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x000B90C4 File Offset: 0x000B72C4
	public static void SendMsgUpdateRanking()
	{
		GameObject gameObjectRanking = HudMenuUtility.GetGameObjectRanking();
		if (gameObjectRanking != null)
		{
			gameObjectRanking.SendMessage("OnUpdateRanking", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x000B90F0 File Offset: 0x000B72F0
	public static void SendMsgNextButtonRanking()
	{
		GameObject gameObjectRanking = HudMenuUtility.GetGameObjectRanking();
		if (gameObjectRanking != null)
		{
			gameObjectRanking.SendMessage("OnClickNextButton", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x000B911C File Offset: 0x000B731C
	public static void SetUpdateRankingFlag()
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			SingletonGameObject<RankingManager>.Instance.Init(null, null);
		}
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "OnUpdateRankingFlag", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x000B9158 File Offset: 0x000B7358
	public static void SendMsgUpdateChallengeDisply()
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnUpdateChallengeCountDisply", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x000B916C File Offset: 0x000B736C
	public static void SendStartMainMenuDlsplay()
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "OnStartMainMenu", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F29 RID: 7977 RVA: 0x000B9180 File Offset: 0x000B7380
	public static void SendStartPlayerChaoPage()
	{
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			MainMenuUI component = mainMenuUIObject.GetComponent<MainMenuUI>();
			if (component != null)
			{
			}
		}
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "PageChangeMessage", new MsgMenuButtonEvent(ButtonInfoTable.ButtonType.ITEM_BACK)
		{
			m_clearHistories = true
		}, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x000B91D4 File Offset: 0x000B73D4
	public static void SendStartInformaionDlsplay()
	{
		GameObject gameObject = GameObject.Find("InformationUI");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "news_set");
			if (gameObject2 != null)
			{
				gameObject2.SendMessage("OnStartInformation", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x000B9220 File Offset: 0x000B7420
	public static void SendMenuButtonClicked(ButtonInfoTable.ButtonType type, bool clearHistories = false)
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "PageChangeMessage", new MsgMenuButtonEvent(type)
		{
			m_clearHistories = clearHistories
		}, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x000B9250 File Offset: 0x000B7450
	public static void SendItemRouletteButtonClicked()
	{
		MsgMenuButtonEvent value = new MsgMenuButtonEvent(ButtonInfoTable.ButtonType.ITEM_ROULETTE);
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "PageChangeMessage", value, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x000B9278 File Offset: 0x000B7478
	public static void SendChaoRouletteButtonClicked()
	{
		MsgMenuButtonEvent value = new MsgMenuButtonEvent(ButtonInfoTable.ButtonType.CHAO_ROULETTE);
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "PageChangeMessage", value, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x000B92A0 File Offset: 0x000B74A0
	public static void SendVirtualNewItemSelectClicked(HudMenuUtility.ITEM_SELECT_MODE mode = HudMenuUtility.ITEM_SELECT_MODE.NORMAL)
	{
		HudMenuUtility.s_itemSelectMode = mode;
		MsgMenuButtonEvent value = new MsgMenuButtonEvent(ButtonInfoTable.ButtonType.VIRTUAL_NEW_ITEM);
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "PageChangeMessage", value, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x000B92CC File Offset: 0x000B74CC
	public static void SendUIPageStart()
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "OnPageStart", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x000B92E0 File Offset: 0x000B74E0
	public static void SendUIPageEnd()
	{
		MsgMenuButtonEvent value = new MsgMenuButtonEvent(ButtonInfoTable.ButtonType.ITEM_ROULETTE);
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "OnPageEnd", value, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x000B9308 File Offset: 0x000B7508
	public static void SendChangeHeaderText(string cellName)
	{
		MsgMenuHeaderText msgMenuHeaderText = new MsgMenuHeaderText(cellName);
		GameObject gameObject = GameObject.Find("HudSaveItem");
		if (gameObject == null)
		{
			return;
		}
		HudHeaderPageName hudHeaderPageName = GameObjectUtil.FindChildGameObjectComponent<HudHeaderPageName>(gameObject, "HudHeaderPageName");
		if (hudHeaderPageName == null)
		{
			return;
		}
		string headerText = HudHeaderPageName.CalcHeaderTextByCellName(cellName);
		hudHeaderPageName.ChangeHeaderText(headerText);
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x000B935C File Offset: 0x000B755C
	public static void SendChangeMainPageHeaderText()
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnSendChangeMainPageHeaderText", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x000B9370 File Offset: 0x000B7570
	public static void SendEnableShopButton(bool enableFlag)
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnEnableShopButton", enableFlag, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x000B9388 File Offset: 0x000B7588
	public static void OnForceDisableShopButton(bool disableFlag)
	{
		GameObjectUtil.SendMessageToTagObjects("HudSaveItem", "OnForceDisableShopButton", disableFlag, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x000B93A0 File Offset: 0x000B75A0
	public static bool IsSale(Constants.Campaign.emType type)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerCampaignState campaignState = ServerInterface.CampaignState;
			if (campaignState != null)
			{
				return campaignState.InAnyIdSession(type);
			}
		}
		return false;
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x000B93D4 File Offset: 0x000B75D4
	public static void GoToTitleScene()
	{
		GameModeTitle.Logined = false;
		HudMenuUtility.CleanUpAllResources();
		ServerSessionWatcher serverSessionWatcher = GameObjectUtil.FindGameObjectComponent<ServerSessionWatcher>("NetMonitor");
		if (serverSessionWatcher != null)
		{
			serverSessionWatcher.InvalidateSession();
		}
		if (Application.loadedLevelName == TitleDefine.TitleSceneName)
		{
			GameObject gameObject = GameObject.Find("GameModeTitle");
			if (gameObject != null)
			{
				gameObject.SendMessage("OnMsgGotoHead", SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			Application.LoadLevel(TitleDefine.TitleSceneName);
		}
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x000B9450 File Offset: 0x000B7650
	public static void CleanUpAllResources()
	{
		ResourceManager instance = ResourceManager.Instance;
		if (instance != null)
		{
			instance.RemoveAllResources();
		}
		if (AssetBundleLoader.Instance != null)
		{
			AssetBundleLoader.Instance.ClearDownloadList();
			UnityEngine.Object.Destroy(AssetBundleLoader.Instance);
		}
		if (AssetBundleManager.Instance != null)
		{
			UnityEngine.Object.Destroy(AssetBundleManager.Instance);
		}
		ChaoTextureManager instance2 = ChaoTextureManager.Instance;
		if (instance2 != null)
		{
			UnityEngine.Object.Destroy(instance2.gameObject);
		}
		AchievementManager instance3 = AchievementManager.Instance;
		if (instance3 != null)
		{
			UnityEngine.Object.Destroy(instance3.gameObject);
		}
		StageAbilityManager instance4 = StageAbilityManager.Instance;
		if (instance4 != null)
		{
			UnityEngine.Object.Destroy(instance4.gameObject);
		}
		CharacterDataNameInfo instance5 = CharacterDataNameInfo.Instance;
		if (instance5 != null)
		{
			UnityEngine.Object.Destroy(instance5.gameObject);
		}
		MissionTable instance6 = MissionTable.Instance;
		if (instance6 != null)
		{
			UnityEngine.Object.Destroy(instance6.gameObject);
		}
		InformationDataTable instance7 = InformationDataTable.Instance;
		if (instance7 != null)
		{
			UnityEngine.Object.Destroy(instance7.gameObject);
		}
		MileageMapDataManager instance8 = MileageMapDataManager.Instance;
		if (instance8 != null)
		{
			UnityEngine.Object.Destroy(instance8.gameObject);
		}
		AtlasManager instance9 = AtlasManager.Instance;
		if (instance9 != null)
		{
			UnityEngine.Object.Destroy(instance9.gameObject);
		}
		EventManager instance10 = EventManager.Instance;
		if (instance10 != null)
		{
			UnityEngine.Object.Destroy(instance10.gameObject);
		}
		GameObject gameObject = GameObject.Find("TextManager");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x000B95EC File Offset: 0x000B77EC
	public static GameObject GetLoadMenuChildObject(string name, bool active)
	{
		GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
		if (menuAnimUIObject != null)
		{
			Transform transform = menuAnimUIObject.transform.FindChild(name);
			if (transform != null)
			{
				GameObject gameObject = transform.gameObject;
				if (gameObject != null)
				{
					if (active)
					{
						gameObject.SetActive(true);
					}
					return gameObject;
				}
			}
			Transform transform2 = menuAnimUIObject.transform.FindChild("OptionWindows");
			if (transform2 != null)
			{
				Transform transform3 = transform2.FindChild(name);
				if (transform3 != null)
				{
					GameObject gameObject2 = transform3.gameObject;
					if (active)
					{
						gameObject2.SetActive(true);
					}
					return gameObject2;
				}
			}
		}
		GameObject mainMenuGeneralAnchor = HudMenuUtility.GetMainMenuGeneralAnchor();
		if (mainMenuGeneralAnchor != null)
		{
			Transform transform4 = mainMenuGeneralAnchor.transform.FindChild(name);
			if (transform4 != null)
			{
				GameObject gameObject3 = transform4.gameObject;
				if (gameObject3 != null)
				{
					if (active)
					{
						gameObject3.SetActive(true);
					}
					return gameObject3;
				}
			}
		}
		return null;
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x000B96EC File Offset: 0x000B78EC
	public static UIDraggablePanel GetMainMenuDraggablePanel()
	{
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			Transform transform = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_contents");
			if (transform != null)
			{
				GameObject gameObject = transform.gameObject;
				if (gameObject != null)
				{
					return gameObject.GetComponent<UIDraggablePanel>();
				}
			}
		}
		return null;
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x000B9744 File Offset: 0x000B7944
	public static bool IsTutorial_2_1_0()
	{
		ServerMileageMapState mileageMapState = ServerInterface.MileageMapState;
		return mileageMapState != null && mileageMapState.m_episode == 2 && mileageMapState.m_chapter == 1 && mileageMapState.m_stageTotalScore == 0L;
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x000B9784 File Offset: 0x000B7984
	public static bool IsItemTutorial()
	{
		return ServerInterface.PlayerState.m_numPlaying == 0 && !HudMenuUtility.IsSystemDataFlagStatus(SystemData.FlagStatus.TUTORIAL_EQIP_ITEM_END);
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x000B97A4 File Offset: 0x000B79A4
	public static bool IsNumPlayingRouletteTutorial()
	{
		return ServerInterface.PlayerState.m_numPlaying == HudMenuUtility.NumPlayingRouletteTutorial;
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x000B97C0 File Offset: 0x000B79C0
	public static bool IsTutorialCharaLevelUp()
	{
		return ServerInterface.PlayerState.m_numPlaying == 9 && HudMenuUtility.IsTutorial_CharaLevelUp();
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x000B97E0 File Offset: 0x000B79E0
	public static bool IsRouletteTutorial()
	{
		return HudMenuUtility.IsNumPlayingRouletteTutorial() && RouletteUtility.isTutorial;
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x000B97F4 File Offset: 0x000B79F4
	public static bool IsRecommendReviewTutorial()
	{
		return ServerInterface.PlayerState.m_numPlaying == 10 && !HudMenuUtility.IsSystemDataFlagStatus(SystemData.FlagStatus.RECOMMEND_REVIEW_END);
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x000B9818 File Offset: 0x000B7A18
	public static bool IsTutorial_11()
	{
		if (!HudMenuUtility.IsSystemDataFlagStatus(SystemData.FlagStatus.ANOTHER_CHARA_EXPLAINED))
		{
			ServerMileageMapState mileageMapState = ServerInterface.MileageMapState;
			if (mileageMapState != null && mileageMapState.m_episode == 11)
			{
				ServerPlayerState playerState = ServerInterface.PlayerState;
				if (playerState != null)
				{
					ServerCharacterState serverCharacterState = playerState.CharacterState(CharaType.TAILS);
					if (serverCharacterState != null)
					{
						return serverCharacterState.Status != ServerCharacterState.CharacterStatus.Locked;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x000B9874 File Offset: 0x000B7A74
	public static bool IsTutorial_SubCharaItem()
	{
		if (!HudMenuUtility.IsSystemDataFlagStatus(SystemData.FlagStatus.SUB_CHARA_ITEM_EXPLAINED))
		{
			CharaType subChara = SaveDataManager.Instance.PlayerData.SubChara;
			if (subChara != CharaType.UNKNOWN && subChara < CharaType.NUM)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x000B98B0 File Offset: 0x000B7AB0
	public static bool IsTutorial_CharaLevelUp()
	{
		if (!HudMenuUtility.IsSystemDataFlagStatus(SystemData.FlagStatus.CHARA_LEVEL_UP_EXPLAINED) && ServerInterface.PlayerState.m_numPlaying <= 9)
		{
			uint num = 0U;
			if (SaveDataManager.Instance != null)
			{
				CharaType mainChara = SaveDataManager.Instance.PlayerData.MainChara;
				num = SaveDataUtil.GetCharaLevel(mainChara);
			}
			if (num < 70U)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x000B9910 File Offset: 0x000B7B10
	public static bool IsSystemDataFlagStatus(SystemData.FlagStatus flag)
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				return systemdata.IsFlagStatus(flag);
			}
		}
		return false;
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x000B9948 File Offset: 0x000B7B48
	public static void SaveSystemDataFlagStatus(SystemData.FlagStatus flag)
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				systemdata.SetFlagStatus(flag, true);
			}
			instance.SaveSystemData();
		}
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x000B9984 File Offset: 0x000B7B84
	public static void SetConnectAlertSimpleUI(bool on)
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "ConnectAlertSimpleUI");
			if (gameObject != null)
			{
				ConnectAlertSimpleUI component = gameObject.GetComponent<ConnectAlertSimpleUI>();
				if (component != null)
				{
					if (on)
					{
						component.StartCollider();
					}
					else
					{
						component.EndCollider();
					}
				}
			}
		}
	}

	// Token: 0x06001F46 RID: 8006 RVA: 0x000B99E8 File Offset: 0x000B7BE8
	public static void SetConnectAlertMenuButtonUI(bool on)
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "ConnectAlertMenuButtonUI");
			if (gameObject != null)
			{
				ConnectAlertSimpleUI component = gameObject.GetComponent<ConnectAlertSimpleUI>();
				if (component != null)
				{
					if (on)
					{
						component.StartCollider();
					}
					else
					{
						component.EndCollider();
					}
				}
			}
		}
	}

	// Token: 0x06001F47 RID: 8007 RVA: 0x000B9A4C File Offset: 0x000B7C4C
	public static void CheckCurrentTextures()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			UITexture[] componentsInChildren = gameObject.GetComponentsInChildren<UITexture>();
			if (componentsInChildren != null && componentsInChildren.Length > 0)
			{
				foreach (UITexture uitexture in componentsInChildren)
				{
					global::Debug.Log("UITexture object:" + uitexture.gameObject);
				}
			}
		}
	}

	// Token: 0x06001F48 RID: 8008 RVA: 0x000B9ABC File Offset: 0x000B7CBC
	public static void RemoveCurrentTextures()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			UITexture[] componentsInChildren = gameObject.GetComponentsInChildren<UITexture>();
			if (componentsInChildren != null && componentsInChildren.Length > 0)
			{
				foreach (UITexture uitexture in componentsInChildren)
				{
					UnityEngine.Object.DestroyImmediate(uitexture.mainTexture, true);
				}
			}
		}
	}

	// Token: 0x06001F49 RID: 8009 RVA: 0x000B9B24 File Offset: 0x000B7D24
	public static void StartMainMenuBGM()
	{
		SoundManager.BgmPlay("bgm_sys_menu_v2", "BGM_menu_v2", false);
	}

	// Token: 0x06001F4A RID: 8010 RVA: 0x000B9B38 File Offset: 0x000B7D38
	public static void ChangeMainMenuBGM()
	{
		SoundManager.BgmChange("bgm_sys_menu_v2", "BGM_menu_v2");
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x000B9B4C File Offset: 0x000B7D4C
	public static void ChangeEventBGM()
	{
		string data = EventCommonDataTable.Instance.GetData(EventCommonDataItem.EventTop_BgmName);
		if (!string.IsNullOrEmpty(data))
		{
			string cueSheetName = "BGM_" + EventManager.GetEventTypeName(EventManager.Instance.Type);
			SoundManager.BgmChange(data, cueSheetName);
		}
		else
		{
			HudMenuUtility.ChangeMainMenuBGM();
		}
	}

	// Token: 0x04001C5D RID: 7261
	private static HudMenuUtility.ITEM_SELECT_MODE s_itemSelectMode;

	// Token: 0x04001C5E RID: 7262
	public static readonly int NumPlayingRouletteTutorial = 7;

	// Token: 0x02000411 RID: 1041
	public enum ITEM_SELECT_MODE
	{
		// Token: 0x04001C60 RID: 7264
		NORMAL,
		// Token: 0x04001C61 RID: 7265
		EVENT_STAGE,
		// Token: 0x04001C62 RID: 7266
		EVENT_BOSS,
		// Token: 0x04001C63 RID: 7267
		EVENT_ETC
	}

	// Token: 0x02000412 RID: 1042
	public enum EffectPriority
	{
		// Token: 0x04001C65 RID: 7269
		None = -1,
		// Token: 0x04001C66 RID: 7270
		Menu,
		// Token: 0x04001C67 RID: 7271
		UniqueWindow,
		// Token: 0x04001C68 RID: 7272
		GeneralWindow,
		// Token: 0x04001C69 RID: 7273
		NetworkErrorWindow,
		// Token: 0x04001C6A RID: 7274
		Num
	}
}
