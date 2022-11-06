using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DataTable;
using Message;
using Mission;
using SaveData;
using Text;
using Tutorial;
using UnityEngine;

// Token: 0x020002F2 RID: 754
[AddComponentMenu("Scripts/Runners/GameMode/Stage")]
public class GameModeStage : MonoBehaviour
{
	// Token: 0x060015A8 RID: 5544 RVA: 0x00077F80 File Offset: 0x00076180
	public string GetStageName()
	{
		return this.m_stageName;
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x00077F88 File Offset: 0x00076188
	public static int ContinueRestCount()
	{
		GameObject gameObject = GameObject.Find("GameModeStage");
		if (gameObject != null)
		{
			GameModeStage component = gameObject.GetComponent<GameModeStage>();
			if (component != null)
			{
				return component.numEnableContinue;
			}
		}
		return 0;
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x060015AA RID: 5546 RVA: 0x00077FC8 File Offset: 0x000761C8
	public int numEnableContinue
	{
		get
		{
			return this.m_numEnableContinue;
		}
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x00077FD0 File Offset: 0x000761D0
	private void Awake()
	{
		Application.targetFrameRate = SystemSettings.TargetFrameRate;
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x00077FDC File Offset: 0x000761DC
	private void Start()
	{
		HudUtility.SetInvalidNGUIMitiTouch();
		base.gameObject.tag = "GameModeStage";
		this.m_pausedObject = new List<GameObject>();
		this.m_rareEnemyTable = new RareEnemyTable();
		this.m_enemyExtendItemTable = new EnemyExtendItemTable();
		this.m_bossTable = new BossTable();
		this.m_bossMap3Table = new BossMap3Table();
		this.m_objectPartTable = new ObjectPartTable();
		this.m_savedFixedTimeStep = Time.fixedDeltaTime;
		this.m_savedMaxFixedTimeStep = Time.maximumDeltaTime;
		this.m_useEquippedItem = new List<ItemType>();
		if (SystemSaveManager.GetSystemSaveData() != null && SystemSaveManager.GetSystemSaveData().lightMode)
		{
			Time.fixedDeltaTime = 0.033333f;
			Time.maximumDeltaTime = 0.333333f;
		}
		if (FontManager.Instance != null && FontManager.Instance.IsNecessaryLoadFont())
		{
			FontManager.Instance.LoadResourceData();
		}
		BackKeyManager.AddEventCallBack(base.gameObject);
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm != null)
		{
			TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
			description.initState = new TinyFsmState(new EventFunction(this.StateInit));
			description.onFixedUpdate = false;
			this.m_fsm.SetUp(description);
		}
		SoundManager.AddStageCommonCueSheet();
		if (ServerInterface.SettingState != null)
		{
			this.m_numEnableContinue = ServerInterface.SettingState.m_onePlayContinueCount;
		}
		this.m_uiRootObj = GameObject.Find("UI Root (2D)");
		if (this.m_uiRootObj != null)
		{
			this.m_connectAlertUI2 = GameObjectUtil.FindChildGameObject(this.m_uiRootObj, "ConnectAlert_2_UI");
			if (this.m_connectAlertUI2 != null)
			{
				this.m_connectAlertUI2.SetActive(true);
				this.m_progressBar = GameObjectUtil.FindChildGameObjectComponent<HudProgressBar>(this.m_connectAlertUI2, "Pgb_loading");
				if (this.m_progressBar != null)
				{
					this.m_progressBar.SetUp(9);
				}
			}
		}
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x000781CC File Offset: 0x000763CC
	private void LateUpdate()
	{
		if (this.m_reqPause)
		{
			this.ChangeState(new TinyFsmState(new EventFunction(this.StatePause)));
		}
		else if (this.m_quickMode && !this.m_quickModeTimeUp && StageTimeManager.Instance != null && StageTimeManager.Instance.IsTimeUp())
		{
			this.OnQuickModeTimeUp(new MsgQuickModeTimeUp());
		}
		if (this.m_levelInformation != null)
		{
			this.m_levelInformation.RequestCharaChange = false;
			this.m_levelInformation.RequestEqitpItem = false;
		}
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x0007826C File Offset: 0x0007646C
	private IEnumerator NotSendPostGameResult()
	{
		yield return null;
		this.DispatchMessage(new MsgPostGameResultsSucceed());
		yield break;
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x00078288 File Offset: 0x00076488
	private IEnumerator NotSendEventUpdateGameResult()
	{
		yield return null;
		this.DispatchMessage(new MsgEventUpdateGameResultsSucceed());
		yield break;
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x000782A4 File Offset: 0x000764A4
	private IEnumerator NotSendEventPostGameResult()
	{
		yield return null;
		this.DispatchMessage(new MsgEventPostGameResultsSucceed());
		yield break;
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x000782C0 File Offset: 0x000764C0
	private void OnDestroy()
	{
		if (this.m_fsm)
		{
			this.m_fsm.ShutDown();
			this.m_fsm = null;
		}
		this.RemoveAllResource();
		Time.fixedDeltaTime = this.m_savedFixedTimeStep;
		Time.maximumDeltaTime = this.m_savedMaxFixedTimeStep;
		this.StopStageEffect();
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x00078314 File Offset: 0x00076514
	private void OnMsgNotifyDead(MsgNotifyDead message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x00078320 File Offset: 0x00076520
	private void OnMsgNotifyStartPause(MsgNotifyStartPause message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x0007832C File Offset: 0x0007652C
	private void OnMsgNotifyEndPause(MsgNotifyEndPause message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x00078338 File Offset: 0x00076538
	private void OnMsgNotifyEndPauseExitStage(MsgNotifyEndPauseExitStage message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x00078344 File Offset: 0x00076544
	private void OnSendToGameModeStage(MessageBase message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x00078350 File Offset: 0x00076550
	private void OnBossEnd(MsgBossEnd message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x0007835C File Offset: 0x0007655C
	private void OnBossClear(MsgBossClear message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x00078368 File Offset: 0x00076568
	private void OnBossTimeUp()
	{
		this.m_bossTimeUp = true;
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x00078374 File Offset: 0x00076574
	private void OnQuickModeTimeUp(MsgQuickModeTimeUp message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015BB RID: 5563 RVA: 0x00078380 File Offset: 0x00076580
	private void OnMsgChangeChara(MsgChangeChara message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x0007838C File Offset: 0x0007658C
	private void OnTransformPhantom(MsgTransformPhantom message)
	{
		this.DispatchMessage(message);
		if (message.m_type == PhantomType.LASER)
		{
			ObjUtil.CreatePrism();
		}
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x000783A8 File Offset: 0x000765A8
	private void OnReturnFromPhantom(MsgReturnFromPhantom message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015BE RID: 5566 RVA: 0x000783B4 File Offset: 0x000765B4
	private void OnMsgInvincible(MsgInvincible message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000783C0 File Offset: 0x000765C0
	private void OnMsgExternalGamePause(MsgExternalGamePause message)
	{
		this.m_reqPauseBackMain = message.m_backMainMenu;
		this.DispatchMessage(message);
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x000783D8 File Offset: 0x000765D8
	private void OnMsgTutorialBackKey(MsgTutorialBackKey message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x000783E4 File Offset: 0x000765E4
	private void OnMsgContinueBackKey(MsgContinueBackKey message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x000783F0 File Offset: 0x000765F0
	private void OnMsgTutorialPlayStart(MsgTutorialPlayStart message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x000783FC File Offset: 0x000765FC
	private void OnMsgTutorialPlayAction(MsgTutorialPlayAction message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x00078408 File Offset: 0x00076608
	private void OnMsgTutorialItemButtonEnd(MsgTutorialEnd message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x00078414 File Offset: 0x00076614
	private void OnMsgTutorialPlayEnd(MsgTutorialPlayEnd message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x00078420 File Offset: 0x00076620
	private void OnMsgTutorialMapBoss(MsgTutorialMapBoss message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C7 RID: 5575 RVA: 0x0007842C File Offset: 0x0007662C
	private void OnMsgTutorialFeverBoss(MsgTutorialFeverBoss message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x00078438 File Offset: 0x00076638
	private void OnMsgTutorialItem(MsgTutorialItem message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x00078444 File Offset: 0x00076644
	private void OnMsgTutorialItemButton(MsgTutorialItemButton message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x00078450 File Offset: 0x00076650
	private void OnMsgTutorialChara(MsgTutorialChara message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x0007845C File Offset: 0x0007665C
	private void OnMsgTutorialAction(MsgTutorialAction message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x00078468 File Offset: 0x00076668
	private void OnMsgTutorialQuickMode(MsgTutorialQuickMode message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x00078474 File Offset: 0x00076674
	private void OnChangeCharaSucceed(MsgChangeCharaSucceed message)
	{
		if (HudTutorial.IsCharaTutorial((CharaType)this.m_playerInformation.SubCharacterID))
		{
			this.m_showCharaTutorial = (int)CharaTypeUtil.GetCharacterTutorialID((CharaType)this.m_playerInformation.SubCharacterID);
			if (this.m_showCharaTutorial != -1)
			{
				GameObjectUtil.SendDelayedMessageToGameObject(base.gameObject, "OnMsgTutorialChara", new MsgTutorialChara((HudTutorial.Id)this.m_showCharaTutorial));
			}
		}
	}

	// Token: 0x060015CE RID: 5582 RVA: 0x000784D4 File Offset: 0x000766D4
	private void OnClickPlatformBackButtonEvent()
	{
		bool backMainMenu = !this.m_firstTutorial;
		this.OnMsgExternalGamePause(new MsgExternalGamePause(backMainMenu, true));
		this.OnMsgTutorialBackKey(new MsgTutorialBackKey());
		this.OnMsgContinueBackKey(new MsgContinueBackKey());
	}

	// Token: 0x060015CF RID: 5583 RVA: 0x00078510 File Offset: 0x00076710
	private void ServerPostGameResults_Succeeded(MsgPostGameResultsSucceed message)
	{
		NetUtil.SyncSaveDataAndDataBase(message.m_playerState);
		this.m_resultMapState = message.m_mileageMapState;
		if (message.m_mileageIncentive != null)
		{
			this.m_mileageIncentive = new List<ServerMileageIncentive>(message.m_mileageIncentive.Count);
			foreach (ServerMileageIncentive item in message.m_mileageIncentive)
			{
				this.m_mileageIncentive.Add(item);
			}
		}
		if (message.m_dailyIncentive != null)
		{
			this.m_dailyIncentive = new List<ServerItemState>(message.m_dailyIncentive.Count);
			foreach (ServerItemState item2 in message.m_dailyIncentive)
			{
				this.m_dailyIncentive.Add(item2);
			}
		}
		this.DispatchMessage(message);
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x00078638 File Offset: 0x00076838
	private void ServerPostGameResults_Failed(MsgServerConnctFailed message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x00078644 File Offset: 0x00076844
	private void ServerStartAct_Succeeded(MsgActStartSucceed message)
	{
		this.m_serverActEnd = true;
		NetUtil.SyncSaveDataAndDataBase(message.m_playerState);
		this.DispatchMessage(message);
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x00078660 File Offset: 0x00076860
	private void ServerStartAct_Failed(MsgServerConnctFailed message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x0007866C File Offset: 0x0007686C
	private void ServerQuickModeStartAct_Succeeded(MsgQuickModeActStartSucceed message)
	{
		this.m_serverActEnd = true;
		NetUtil.SyncSaveDataAndDataBase(message.m_playerState);
		this.DispatchMessage(message);
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x00078688 File Offset: 0x00076888
	private void ServerQuickModePostGameResults_Succeeded(MsgQuickModePostGameResultsSucceed message)
	{
		NetUtil.SyncSaveDataAndDataBase(message.m_playerState);
		if (message.m_dailyIncentive != null)
		{
			this.m_dailyIncentive = new List<ServerItemState>(message.m_dailyIncentive.Count);
			foreach (ServerItemState item in message.m_dailyIncentive)
			{
				this.m_dailyIncentive.Add(item);
			}
		}
		this.DispatchMessage(message);
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x00078728 File Offset: 0x00076928
	private void ServerEventStartAct_Succeeded(MsgEventActStartSucceed message)
	{
		this.m_serverActEnd = true;
		NetUtil.SyncSaveDataAndDataBase(message.m_playerState);
		this.DispatchMessage(message);
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x00078744 File Offset: 0x00076944
	private void ServerEventStartAct_Failed(MsgServerConnctFailed message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x00078750 File Offset: 0x00076950
	private void ServerUpdateGameResults_Succeeded(MsgEventUpdateGameResultsSucceed message)
	{
		if (message.m_bonus != null)
		{
			this.m_raidBossBonus = new ServerEventRaidBossBonus();
			message.m_bonus.CopyTo(this.m_raidBossBonus);
		}
		this.DispatchMessage(message);
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x0007878C File Offset: 0x0007698C
	private void ServerEventPostGameResults_Succeeded(MsgEventPostGameResultsSucceed message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x00078798 File Offset: 0x00076998
	private void ServerDrawRaidBoss_Succeeded(MsgDrawRaidBossSucceed message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x000787A4 File Offset: 0x000769A4
	private void ServerGetEventUserRaidBossState_Succeeded(MsgGetEventUserRaidBossStateSucceed message)
	{
		this.DispatchMessage(message);
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x000787B0 File Offset: 0x000769B0
	private void DailyBattleResultCallBack()
	{
		MessageBase message = new MessageBase(61515);
		this.DispatchMessage(message);
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x000787D0 File Offset: 0x000769D0
	private void DispatchMessage(MessageBase message)
	{
		if (this.m_fsm != null && message != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateMessage(message);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00078808 File Offset: 0x00076A08
	private void ChangeState(TinyFsmState nextState)
	{
		this.m_fsm.ChangeState(nextState);
		this.m_substate = 0;
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x00078820 File Offset: 0x00076A20
	private TinyFsmState StateInit(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(0);
			}
			return TinyFsmState.End();
		case 1:
		{
			TimeProfiler.EndCountTime("MainMenu-GameModeStage");
			TimeProfiler.StartCountTime("GameModeStage:StateInit");
			GC.Collect();
			Resources.UnloadUnusedAssets();
			GC.Collect();
			CameraFade.StartAlphaFade(Color.black, true, 1f, 0f);
			HudLoading.StartScreen(null);
			if (this.m_uiRootObj == null)
			{
				this.m_uiRootObj = GameObject.Find("UI Root (2D)");
			}
			GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject gameObject in array)
			{
				if (gameObject.activeInHierarchy)
				{
					this.m_pausedObject.Add(gameObject);
				}
				gameObject.SetActive(false);
			}
			GameObject[] array3 = GameObject.FindGameObjectsWithTag("Chao");
			foreach (GameObject gameObject2 in array3)
			{
				this.m_pausedObject.Add(gameObject2);
				gameObject2.SetActive(false);
			}
			if (AtlasManager.Instance == null)
			{
				GameObject gameObject3 = new GameObject("AtlasManager");
				gameObject3.AddComponent<AtlasManager>();
			}
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
			if (GameObjectUtil.FindGameObjectComponent<StageAbilityManager>("StageAbilityManager") == null)
			{
				GameObject gameObject4 = new GameObject("StageAbilityManager");
				gameObject4.AddComponent<StageAbilityManager>();
				gameObject4.tag = "Manager";
			}
			if (InformationDataTable.Instance == null)
			{
				InformationDataTable.Create();
				InformationDataTable.Instance.Initialize(base.gameObject);
			}
			this.m_levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
			if (this.m_levelInformation == null)
			{
				this.m_levelInformation = new GameObject("LevelInformation")
				{
					tag = "StageManager"
				}.AddComponent<LevelInformation>();
			}
			this.m_eventManager = EventManager.Instance;
			bool flag = false;
			StageInfo stageInfo = GameObjectUtil.FindGameObjectComponent<StageInfo>("StageInfo");
			if (stageInfo != null)
			{
				if (this.m_levelInformation != null)
				{
					this.m_levelInformation.Missed = false;
					this.m_stageName = stageInfo.SelectedStageName;
					this.m_firstTutorial = stageInfo.FirstTutorial;
					this.m_tutorialStage = stageInfo.TutorialStage;
					this.m_fromTitle = stageInfo.FromTitle;
					this.m_quickMode = stageInfo.QuickMode;
					this.m_bossStage = (!this.m_quickMode && stageInfo.BossStage);
					if (this.m_firstTutorial)
					{
						this.m_quickMode = false;
						this.m_tutorialStage = false;
						this.m_bossStage = false;
					}
					else if (this.m_tutorialStage)
					{
						this.m_quickMode = false;
						this.m_bossStage = false;
					}
					if (!this.m_firstTutorial && !this.m_tutorialStage && HudMenuUtility.IsItemTutorial())
					{
						this.m_equipItemTutorial = true;
					}
					if (StageModeManager.Instance != null)
					{
						StageModeManager.Instance.FirstTutorial = this.m_firstTutorial;
						if (this.m_quickMode)
						{
							StageModeManager.Instance.StageMode = StageModeManager.Mode.QUICK;
						}
						else if (this.m_firstTutorial || this.m_tutorialStage)
						{
							StageModeManager.Instance.StageMode = StageModeManager.Mode.UNKNOWN;
						}
						else
						{
							StageModeManager.Instance.StageMode = StageModeManager.Mode.ENDLESS;
						}
					}
					if (this.m_bossStage)
					{
						this.m_bossType = stageInfo.BossType;
					}
					else
					{
						this.m_bossType = BossType.FEVER;
					}
					this.m_eventStage = stageInfo.EventStage;
					flag = stageInfo.BoostItemValid[2];
					if (TenseEffectManager.Instance != null)
					{
						TenseEffectManager.Type type = (stageInfo.TenseType != TenseType.AFTERNOON) ? TenseEffectManager.Type.TENSE_B : TenseEffectManager.Type.TENSE_A;
						TenseEffectManager.Instance.SetType(type);
						this.m_stageTenseType = stageInfo.TenseType;
						TenseEffectManager.Instance.NotChangeTense = stageInfo.NotChangeTense;
					}
					this.m_postGameResults.m_existBoss = stageInfo.ExistBoss;
					this.m_postGameResults.m_prevMapInfo = new StageInfo.MileageMapInfo();
					this.m_postGameResults.m_prevMapInfo.m_mapState.Set(stageInfo.MileageInfo.m_mapState);
					stageInfo.MileageInfo.m_pointScore.CopyTo(this.m_postGameResults.m_prevMapInfo.m_pointScore, 0);
					this.m_levelInformation.NumBossAttack = stageInfo.NumBossAttack;
					this.m_oldNumBossAttack = this.m_levelInformation.NumBossAttack;
					if (this.m_levelInformation.NumBossAttack > 0)
					{
						this.m_bossNoMissChance = false;
					}
					else
					{
						this.m_bossNoMissChance = true;
					}
					bool flag2 = true;
					if (SystemSaveManager.GetSystemSaveData() != null)
					{
						this.m_levelInformation.LightMode = SystemSaveManager.GetSystemSaveData().lightMode;
						int numRank = ServerInterface.PlayerState.m_numRank;
						if (numRank < 2)
						{
							flag2 = SystemSaveManager.GetSystemSaveData().IsFlagStatus(SystemData.FlagStatus.TUTORIAL_FEVER_BOSS);
						}
					}
					if (this.m_bossStage)
					{
						if (this.m_eventManager != null && this.m_eventManager.IsRaidBossStage() && this.m_eventStage)
						{
							int num = 0;
							if (RaidBossInfo.currentRaidData != null)
							{
								num = RaidBossInfo.currentRaidData.lv;
							}
							if (this.m_levelInformation.NumBossAttack == 0 && (num == 1 || num == 5))
							{
								this.m_showEventBossTutorial = true;
							}
						}
						else if (stageInfo.MileageInfo.m_mapState != null && this.m_levelInformation.NumBossAttack == 0)
						{
							MileageMapState mapState = stageInfo.MileageInfo.m_mapState;
							if (mapState.m_episode == 1)
							{
								this.m_showMapBossTutorial = true;
							}
							if (mapState.m_episode == 2)
							{
								this.m_showMapBossTutorial = true;
							}
							if (mapState.m_episode == 3)
							{
								this.m_showMapBossTutorial = true;
							}
						}
					}
					else
					{
						this.m_showFeverBossTutorial = !flag2;
					}
				}
				if (StageItemManager.Instance != null)
				{
					StageItemManager.Instance.SetEquipItemTutorial(this.m_equipItemTutorial);
					StageItemManager.Instance.SetEquippedItem(stageInfo.EquippedItems);
					this.m_useEquippedItem.Clear();
					if (stageInfo.EquippedItems != null)
					{
						foreach (ItemType item in stageInfo.EquippedItems)
						{
							this.m_useEquippedItem.Add(item);
						}
					}
				}
				for (int l = 0; l < 3; l++)
				{
					bool flag3 = stageInfo.BoostItemValid[l];
					if (flag3)
					{
						BoostItemType item2 = (BoostItemType)l;
						this.m_useBoostItem.Add(item2);
					}
					if (l == 1 && StageItemManager.Instance != null)
					{
						StageItemManager.Instance.SetActiveAltitudeTrampoline(flag3);
					}
				}
				UnityEngine.Object.Destroy(stageInfo.gameObject);
			}
			TerrainXmlData.SetAssetName(this.m_stageName);
			StageScoreManager instance = StageScoreManager.Instance;
			if (instance != null)
			{
				instance.Setup(this.m_bossStage);
			}
			this.m_stagePathManager = GameObjectUtil.FindGameObjectComponent<PathManager>("StagePathManager");
			if (this.m_stagePathManager == null)
			{
				GameObject gameObject5 = new GameObject("StagePathManager");
				this.m_stagePathManager = gameObject5.AddComponent<PathManager>();
			}
			this.m_playerInformation = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
			if (this.m_playerInformation != null)
			{
				if (this.m_playerInformation != null && SaveDataManager.Instance != null)
				{
					PlayerData playerData = SaveDataManager.Instance.PlayerData;
					this.m_mainChara = playerData.MainChara;
					this.m_subChara = ((!flag) ? CharaType.UNKNOWN : playerData.SubChara);
				}
				else
				{
					this.m_mainChara = CharaType.SONIC;
					this.m_subChara = CharaType.UNKNOWN;
				}
				this.m_playerInformation.SetPlayerCharacter((int)this.m_mainChara, (int)this.m_subChara);
			}
			this.m_characterContainer = GameObjectUtil.FindGameObjectComponent<CharacterContainer>("CharacterContainer");
			if (this.m_characterContainer != null)
			{
				this.m_characterContainer.Init();
			}
			this.m_hudCaution = HudCaution.Instance;
			this.m_missionManager = GameObjectUtil.FindGameObjectComponent<StageMissionManager>("StageMissionManager");
			this.m_tutorialManager = GameObjectUtil.FindGameObjectComponent<StageTutorialManager>("StageTutorialManager");
			this.m_friendSignManager = GameObjectUtil.FindGameObjectComponent<FriendSignManager>("FriendSignManager");
			if (this.m_tutorialStage)
			{
				if (this.m_tutorialManager == null)
				{
					GameObject gameObject6 = new GameObject("StageTutorialManager");
					this.m_tutorialManager = gameObject6.AddComponent<StageTutorialManager>();
				}
			}
			else if (this.m_tutorialManager != null)
			{
				UnityEngine.Object.Destroy(this.m_tutorialManager.gameObject);
				this.m_tutorialManager = null;
			}
			if (this.m_uiRootObj != null)
			{
				GameObject gameObject7 = GameObjectUtil.FindChildGameObject(this.m_uiRootObj, "Result");
				if (gameObject7 != null)
				{
					this.m_gameResult = gameObject7.GetComponent<GameResult>();
					gameObject7.SetActive(false);
				}
			}
			if (ServerInterface.PlayerState != null && this.m_levelInformation != null)
			{
				this.m_levelInformation.PlayerRank = ServerInterface.PlayerState.m_numRank;
			}
			this.CreateStageBlockManager();
			if (this.m_eventManager != null && this.m_eventManager.IsRaidBossStage())
			{
				if (this.m_bossType == BossType.EVENT2)
				{
					this.SendPlayerSpeedLevel(PlayerSpeed.LEVEL_2);
				}
				else if (this.m_bossType == BossType.EVENT3)
				{
					this.SendPlayerSpeedLevel(PlayerSpeed.LEVEL_3);
				}
			}
			else if (this.m_quickMode)
			{
				int a = 0;
				if (this.m_stageBlockManager != null)
				{
					a = this.m_stageBlockManager.GetComponent<StageBlockManager>().GetBlockLevel();
				}
				int speedLevel = Mathf.Min(a, 2);
				this.SendPlayerSpeedLevel((PlayerSpeed)speedLevel);
			}
			if (DelayedMessageManager.Instance == null)
			{
				GameObject gameObject8 = new GameObject("DelayedMessageManager");
				gameObject8.AddComponent<DelayedMessageManager>();
			}
			this.m_tutorialKind = HudTutorial.Kind.NONE;
			this.m_showItemTutorial = -1;
			if (this.m_playerInformation != null)
			{
				if (HudTutorial.IsCharaTutorial((CharaType)this.m_playerInformation.MainCharacterID))
				{
					this.m_showCharaTutorial = (int)CharaTypeUtil.GetCharacterTutorialID((CharaType)this.m_playerInformation.MainCharacterID);
				}
				else
				{
					this.m_showCharaTutorial = -1;
				}
			}
			this.m_showActionTutorial = -1;
			if (this.m_quickMode)
			{
				if (HudTutorial.IsQuickModeTutorial(HudTutorial.Id.QUICK_1))
				{
					this.m_showQuickTurorial = 54;
				}
			}
			else
			{
				this.m_showQuickTurorial = -1;
			}
			this.m_saveFlag = false;
			GameObject gameObject9 = GameObject.Find("AllocationStatus");
			if (gameObject9 != null)
			{
				UnityEngine.Object.Destroy(gameObject9);
			}
			TimeProfiler.EndCountTime("GameModeStage:StateInit");
			return TinyFsmState.End();
		}
		case 4:
			if (!Env.useAssetBundle || AssetBundleLoader.Instance.IsEnableDownlad())
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoad)));
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x00079338 File Offset: 0x00077538
	private TinyFsmState StateLoad(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(1);
			}
			return TinyFsmState.End();
		case 1:
		{
			this.m_sceneLoader = new GameObject("SceneLoader");
			ResourceSceneLoader resourceSceneLoader = this.m_sceneLoader.AddComponent<ResourceSceneLoader>();
			TextManager.LoadCommonText(resourceSceneLoader);
			TextManager.LoadEventText(resourceSceneLoader);
			TextManager.LoadChaoText(resourceSceneLoader);
			resourceSceneLoader.AddLoad("TenseEffectTable", true, false);
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_sceneLoader != null && this.m_sceneLoader.GetComponent<ResourceSceneLoader>().Loaded)
			{
				TextManager.SetupCommonText();
				TextManager.SetupEventText();
				TextManager.SetupChaoText();
				UnityEngine.Object.Destroy(this.m_sceneLoader);
				this.m_sceneLoader = null;
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateLoad2)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x0007943C File Offset: 0x0007763C
	private TinyFsmState StateLoad2(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(2);
			}
			return TinyFsmState.End();
		case 1:
			TimeProfiler.StartCountTime("GameModeStage:Load");
			this.m_counter = 0;
			if (AtlasManager.Instance != null)
			{
				AtlasManager.Instance.StartLoadAtlasForStage();
			}
			if (this.m_isLoadResources)
			{
				this.m_sceneLoader = new GameObject("SceneLoader");
				ResourceSceneLoader resourceSceneLoader = this.m_sceneLoader.AddComponent<ResourceSceneLoader>();
				for (int i = 0; i < this.m_loadInfo.Count; i++)
				{
					ResourceSceneLoader.ResourceInfo resourceInfo = this.m_loadInfo[i];
					if (i == 6)
					{
						if (this.m_quickMode && this.m_eventManager != null && this.m_eventManager.Type == EventManager.EventType.QUICK)
						{
							ResourceSceneLoader.ResourceInfo resourceInfo2 = resourceInfo;
							ResourceSceneLoader.ResourceInfo resourceInfo3 = resourceInfo2;
							resourceInfo3.m_scenename += EventManager.GetResourceName();
							resourceSceneLoader.AddLoadAndResourceManager(resourceInfo2);
						}
					}
					else if (i == 7)
					{
						if (this.m_eventManager != null && this.m_eventManager.Type != EventManager.EventType.UNKNOWN)
						{
							ResourceSceneLoader.ResourceInfo resourceInfo4 = resourceInfo;
							ResourceSceneLoader.ResourceInfo resourceInfo5 = resourceInfo4;
							resourceInfo5.m_scenename += EventManager.GetResourceName();
							resourceSceneLoader.AddLoadAndResourceManager(resourceInfo4);
						}
					}
					else
					{
						resourceSceneLoader.AddLoadAndResourceManager(resourceInfo);
					}
				}
				if (this.m_quickMode)
				{
					foreach (ResourceSceneLoader.ResourceInfo resourceInfo6 in this.m_quickModeLoadInfo)
					{
						if (resourceInfo6.m_category == ResourceCategory.EVENT_RESOURCE)
						{
							if (this.m_eventManager != null && this.m_eventStage)
							{
								ResourceSceneLoader.ResourceInfo resourceInfo7 = resourceInfo6;
								ResourceSceneLoader.ResourceInfo resourceInfo8 = resourceInfo7;
								resourceInfo8.m_scenename += EventManager.GetResourceName();
								resourceSceneLoader.AddLoadAndResourceManager(resourceInfo7);
							}
						}
						else
						{
							resourceSceneLoader.AddLoadAndResourceManager(resourceInfo6);
						}
					}
				}
				this.m_terrainDataName = this.m_stageName + "_TerrainData";
				resourceSceneLoader.AddLoad(this.m_terrainDataName, true, false);
				this.m_stageResourceName = this.m_stageName + "_StageResource";
				this.m_stageResourceObjectName = this.m_stageName + "_StageModelResource";
				resourceSceneLoader.AddLoad(this.m_stageResourceName, true, false);
				if (this.m_playerInformation != null)
				{
					string mainCharacterName = this.m_playerInformation.MainCharacterName;
					if (mainCharacterName != null)
					{
						resourceSceneLoader.AddLoad("CharacterModel" + mainCharacterName, true, false);
						resourceSceneLoader.AddLoad("CharacterEffect" + mainCharacterName, true, false);
					}
					string subCharacterName = this.m_playerInformation.SubCharacterName;
					if (subCharacterName != null)
					{
						resourceSceneLoader.AddLoad("CharacterModel" + subCharacterName, true, false);
						resourceSceneLoader.AddLoad("CharacterEffect" + subCharacterName, true, false);
					}
					BossType tutorialBossType = BossType.NONE;
					if (this.m_showMapBossTutorial)
					{
						tutorialBossType = this.m_bossType;
					}
					else if (this.m_showFeverBossTutorial)
					{
						tutorialBossType = this.m_bossType;
					}
					else if (this.m_tutorialStage)
					{
						tutorialBossType = BossType.FEVER;
						this.m_showFeverBossTutorial = true;
					}
					HudTutorial.Load(resourceSceneLoader, this.m_tutorialStage, this.m_bossStage, tutorialBossType, (CharaType)this.m_playerInformation.MainCharacterID, (CharaType)this.m_playerInformation.SubCharacterID);
				}
				SaveDataManager instance = SaveDataManager.Instance;
				if (instance != null)
				{
					bool onAssetBundle = true;
					int mainChaoID = instance.PlayerData.MainChaoID;
					int subChaoID = instance.PlayerData.SubChaoID;
					if (mainChaoID >= 0)
					{
						resourceSceneLoader.AddLoad("chao_" + mainChaoID.ToString("0000"), onAssetBundle, false);
					}
					if (subChaoID >= 0 && subChaoID != mainChaoID)
					{
						resourceSceneLoader.AddLoad("chao_" + subChaoID.ToString("0000"), onAssetBundle, false);
					}
				}
				StageAbilityManager.LoadAbilityDataTable(resourceSceneLoader);
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_sceneLoader != null && AtlasManager.Instance != null)
			{
				if (this.m_sceneLoader.GetComponent<ResourceSceneLoader>().Loaded && AtlasManager.Instance.IsLoadAtlas())
				{
					TimeProfiler.EndCountTime("GameModeStage:Load");
					this.RegisterAllResource();
					UnityEngine.Object.Destroy(this.m_sceneLoader);
					this.m_sceneLoader = null;
					AtlasManager.Instance.ReplaceAtlasForStage();
					this.m_stagePathManager.CreatePathObjectData();
					if (this.m_quickMode)
					{
						StageTimeManager stageTimeManager = GameObjectUtil.FindGameObjectComponent<StageTimeManager>("StageTimeManager");
						if (stageTimeManager != null)
						{
							stageTimeManager.SetTable();
						}
					}
					EventObjectTable.LoadSetup();
					EventSPStageObjectTable.LoadSetup();
					EventBossObjectTable.LoadSetup();
					EventBossParamTable.LoadSetup();
					EventCommonDataTable.LoadSetup();
					TimeProfiler.StartCountTime("GameModeStage:SetupStageBlocks");
					StageBlockManager component = this.m_stageBlockManager.GetComponent<StageBlockManager>();
					if (component != null)
					{
						component.Setup(this.m_bossStage);
						component.PauseTerrainPlacement(this.m_notPlaceTerrain);
					}
					TimeProfiler.EndCountTime("GameModeStage:SetupStageBlocks");
					ResourceManager instance2 = ResourceManager.Instance;
					GameObject gameObject = instance2.GetGameObject(ResourceCategory.TERRAIN_MODEL, TerrainXmlData.DataAssetName);
					if (gameObject != null)
					{
						TerrainXmlData component2 = gameObject.GetComponent<TerrainXmlData>();
						if (component2 != null)
						{
							if (this.m_levelInformation != null)
							{
								this.m_levelInformation.MoveTrapBooRand = component2.MoveTrapBooRand;
							}
							if (StageItemManager.Instance != null)
							{
								ItemTable itemTable = StageItemManager.Instance.GetItemTable();
								if (itemTable != null)
								{
									itemTable.Setup(component2);
								}
							}
							RareEnemyTable rareEnemyTable = this.GetRareEnemyTable();
							if (rareEnemyTable != null)
							{
								rareEnemyTable.Setup(component2);
							}
							EnemyExtendItemTable enemyExtendItemTable = this.GetEnemyExtendItemTable();
							if (enemyExtendItemTable != null)
							{
								enemyExtendItemTable.Setup(component2);
							}
							BossTable bossTable = this.GetBossTable();
							if (bossTable != null)
							{
								bossTable.Setup(component2);
							}
							BossMap3Table bossMap3Table = this.GetBossMap3Table();
							if (bossMap3Table != null)
							{
								bossMap3Table.Setup(component2);
							}
							ObjectPartTable objectPartTable = this.GetObjectPartTable();
							if (objectPartTable != null)
							{
								objectPartTable.Setup(component2);
							}
						}
					}
					if (this.m_characterContainer != null)
					{
						this.m_characterContainer.SetupCharacter();
					}
					StageComboManager instance3 = StageComboManager.Instance;
					if (instance3 != null)
					{
						instance3.Setup();
						instance3.SetComboTime(this.m_quickMode);
					}
					if (this.m_uiRootObj != null)
					{
						this.m_continueWindowObj = GameObjectUtil.FindChildGameObject(this.m_uiRootObj, "ContinueWindow");
						if (this.m_continueWindowObj != null)
						{
							HudContinue component3 = this.m_continueWindowObj.GetComponent<HudContinue>();
							if (component3 != null)
							{
								component3.Setup(this.m_bossStage);
							}
							this.m_continueWindowObj.SetActive(false);
						}
					}
					if (this.m_hudCaution != null)
					{
						this.m_hudCaution.SetBossWord(this.m_bossStage);
					}
					BossType bossType = BossType.NONE;
					if (this.m_bossStage)
					{
						bossType = this.m_bossType;
					}
					bool spCrystal = EventManager.Instance != null && EventManager.Instance.IsSpecialStage();
					bool animal = EventManager.Instance != null && EventManager.Instance.IsGetAnimalStage();
					GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnSetup", new MsgHudCockpitSetup(bossType, spCrystal, animal, !this.m_tutorialStage, this.m_firstTutorial), SendMessageOptions.DontRequireReceiver);
					if (FontManager.Instance != null)
					{
						FontManager.Instance.ReplaceFont();
					}
					if (StageEffectManager.Instance != null)
					{
						StageEffectManager.Instance.StockStageEffect(this.m_bossStage | this.m_tutorialStage);
					}
					if (AnimalResourceManager.Instance != null)
					{
						AnimalResourceManager.Instance.StockAnimalObject(this.m_bossStage | this.m_tutorialStage);
					}
					this.PlayStageEffect();
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateRequestActStart)));
				}
			}
			else
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateRequestActStart)));
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x00079C88 File Offset: 0x00077E88
	private TinyFsmState StateRequestActStart(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(3);
			}
			return TinyFsmState.End();
		case 1:
			this.RequestServerStartAct();
			return TinyFsmState.End();
		case 4:
			if (this.m_serverActEnd)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateSoundConnectIfNotFound)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x00079D1C File Offset: 0x00077F1C
	private TinyFsmState StateSoundConnectIfNotFound(TinyFsmEvent e)
	{
		int signal = e.Signal;
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
			string text = null;
			string text2 = null;
			string cueSheetName = this.GetCueSheetName();
			if (!string.IsNullOrEmpty(cueSheetName))
			{
				text = cueSheetName + ".acb";
				text2 = cueSheetName + "_streamfiles.awb";
			}
			string downloadURL = SoundManager.GetDownloadURL();
			string downloadedDataPath = SoundManager.GetDownloadedDataPath();
			StreamingDataLoader instance = StreamingDataLoader.Instance;
			if (instance != null)
			{
				if (text != null)
				{
					instance.AddFileIfNotDownloaded(downloadURL + text, downloadedDataPath + text);
				}
				if (text2 != null)
				{
					instance.AddFileIfNotDownloaded(downloadURL + text2, downloadedDataPath + text2);
				}
				StageStreamingDataLoadRetryProcess process = new StageStreamingDataLoadRetryProcess(base.gameObject, this);
				NetMonitor.Instance.StartMonitor(process, 0f, HudNetworkConnect.DisplayType.ALL);
				instance.StartDownload(0, base.gameObject);
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			StreamingDataLoader instance2 = StreamingDataLoader.Instance;
			if (instance2 != null)
			{
				if (instance2.Loaded)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateAccessNetworkForStartAct)));
				}
			}
			else
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateAccessNetworkForStartAct)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x00079E90 File Offset: 0x00078090
	private TinyFsmState StateAccessNetworkForStartAct(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			TimeProfiler.EndCountTime("GameModeStage:StateAccessNetworkForStartAct");
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(5);
			}
			return TinyFsmState.End();
		case 1:
			TimeProfiler.StartCountTime("GameModeStage:StateAccessNetworkForStartAct");
			return TinyFsmState.End();
		case 4:
			if (this.m_serverActEnd && this.m_stagePathManager.SetupEnd && this.m_stageBlockManager.GetComponent<StageBlockManager>().IsSetupEnded())
			{
				this.SetMainBGMName();
				SoundManager.AddStageCueSheet(this.GetCueSheetName());
				ObjUtil.CreateSharedMateriaDummyObject(ResourceCategory.OBJECT_RESOURCE, "obj_cmn_ring");
				ObjUtil.CreateSharedMateriaDummyObject(ResourceCategory.OBJECT_RESOURCE, "obj_cmn_movetrap");
				if (EventManager.Instance != null && EventManager.Instance.IsQuickEvent() && this.m_quickMode)
				{
					ObjUtil.CreateSharedMateriaDummyObject(ResourceCategory.EVENT_RESOURCE, "obj_sp_goldcoin");
					ObjUtil.CreateSharedMateriaDummyObject(ResourceCategory.EVENT_RESOURCE, "obj_sp_goldcoin10");
					ObjUtil.CreateSharedMateriaDummyObject(ResourceCategory.EVENT_RESOURCE, "obj_sp_pearl10");
				}
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateSetupPrepareBlock)));
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x00079FD0 File Offset: 0x000781D0
	private TinyFsmState StateSetupPrepareBlock(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(6);
			}
			return TinyFsmState.End();
		case 1:
			if (this.m_stageBlockManager && this.m_playerInformation)
			{
				MsgPrepareStageReplace value = new MsgPrepareStageReplace(this.m_playerInformation.SpeedLevel, this.m_stageName);
				this.m_stageBlockManager.SendMessage("OnMsgPrepareStageReplace", value);
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_stageBlockManager.GetComponent<StageBlockManager>().IsSetupEnded())
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateSetupBlock)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x0007A0B0 File Offset: 0x000782B0
	private TinyFsmState StateSetupBlock(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			foreach (GameObject gameObject in this.m_pausedObject)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(true);
				}
			}
			this.m_pausedObject.Clear();
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(7);
			}
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (this.m_stageBlockManager && this.m_playerInformation)
			{
				TimeProfiler.StartCountTime("GameModeStage:SetupBlock");
				MsgStageReplace value = new MsgStageReplace(this.m_playerInformation.SpeedLevel, this.PlayerResetPosition, this.PlayerResetRotation, this.m_stageName);
				if (this.m_bossStage)
				{
					BossType bossType = this.m_bossType;
					MsgSetStageOnMapBoss value2 = new MsgSetStageOnMapBoss(this.PlayerResetPosition, this.PlayerResetRotation, this.m_stageName, bossType);
					this.m_stageBlockManager.SendMessage("OnMsgSetStageOnMapBoss", value2);
					if (this.m_levelInformation != null)
					{
						this.m_levelInformation.NowBoss = true;
					}
				}
				else if (this.m_tutorialStage)
				{
					this.m_stageBlockManager.GetComponent<StageBlockManager>().SetStageOnTutorial(this.PlayerResetPosition);
				}
				else
				{
					this.m_stageBlockManager.SendMessage("OnMsgStageReplace", value);
				}
				StageFarTerrainManager stageFarTerrainManager = GameObjectUtil.FindGameObjectComponent<StageFarTerrainManager>("StageFarManager");
				if (stageFarTerrainManager != null)
				{
					stageFarTerrainManager.SendMessage("OnMsgStageReplace", value);
				}
				TimeProfiler.EndCountTime("GameModeStage:SetupBlock");
			}
			if (!this.m_tutorialStage && !this.m_firstTutorial && this.m_missionManager != null)
			{
				this.m_missionManager.SetupMissions();
				this.m_missonCompleted = this.m_missionManager.Completed;
			}
			if (this.m_tutorialManager != null)
			{
				this.m_tutorialManager.SetupTutorial();
			}
			TimeProfiler.StartCountTime("GameModeStage:SetupFriendManager");
			if (this.m_friendSignManager != null && !this.m_bossStage)
			{
				this.m_friendSignManager.SetupFriendSignManager();
			}
			TimeProfiler.EndCountTime("GameModeStage:SetupFriendManager");
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateSendApolloStageStart)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x0007A358 File Offset: 0x00078558
	private TinyFsmState StateSendApolloStageStart(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_sendApollo != null)
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
			}
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(8);
			}
			return TinyFsmState.End();
		case 1:
		{
			ApolloTutorialIndex apolloStartTutorialIndex = this.GetApolloStartTutorialIndex();
			if (apolloStartTutorialIndex != ApolloTutorialIndex.NONE)
			{
				string[] value = new string[1];
				SendApollo.GetTutorialValue(apolloStartTutorialIndex, ref value);
				this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_START, value);
			}
			else
			{
				this.m_sendApollo = null;
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			bool flag = true;
			if (this.m_sendApollo != null && this.m_sendApollo.GetState() == SendApollo.State.Succeeded)
			{
				flag = false;
			}
			if (flag)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateFadeIn)));
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x0007A46C File Offset: 0x0007866C
	private TinyFsmState StateFadeIn(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_connectAlertUI2 != null)
			{
				this.m_connectAlertUI2.SetActive(false);
			}
			return TinyFsmState.End();
		case 1:
		{
			GC.Collect();
			Resources.UnloadUnusedAssets();
			GC.Collect();
			SoundManager.BgmPlay(this.m_mainBgmName, "BGM", false);
			this.m_timer = 0.5f;
			this.SetChaoAblityTimeScale();
			this.SetDefaultTimeScale();
			HudLoading.EndScreen(null);
			MsgDisableInput value = new MsgDisableInput(true);
			GameObjectUtil.SendMessageToTagObjects("Player", "OnInputDisable", value, SendMessageOptions.DontRequireReceiver);
			return TinyFsmState.End();
		}
		case 4:
			this.m_timer -= e.GetDeltaTime;
			if (this.m_timer < 0f)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateGameStart)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x0007A568 File Offset: 0x00078768
	private TinyFsmState StateGameStart(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
		{
			MsgDisableInput value = new MsgDisableInput(false);
			GameObjectUtil.SendMessageToTagObjects("Player", "OnInputDisable", value, SendMessageOptions.DontRequireReceiver);
			if (this.m_bossStage || ObjUtil.IsUseTemporarySet())
			{
				ObjUtil.SendStartItemAndChao();
			}
			return TinyFsmState.End();
		}
		case 1:
			if (this.m_bossStage)
			{
				if (this.m_eventManager != null && this.m_eventManager.IsRaidBossStage() && this.m_eventStage)
				{
					this.SendMessageToHudCaution(HudCaution.Type.EVENTBOSS);
				}
				else
				{
					this.SendMessageToHudCaution(HudCaution.Type.BOSS);
				}
				SoundManager.SePlay("sys_boss_warning", "SE");
			}
			else
			{
				this.SendMessageToHudCaution(HudCaution.Type.GO);
				SoundManager.SePlay("sys_go", "SE");
			}
			this.m_timer = 1f;
			return TinyFsmState.End();
		case 4:
			this.m_timer -= e.GetDeltaTime;
			if (this.m_timer < 0f)
			{
				BackKeyManager.StartScene();
				this.HudPlayerChangeCharaButton(true, false);
				if (this.m_chaoEasyTimeScale < 1f)
				{
					ObjUtil.RequestStartAbilityToChao(ChaoAbility.EASY_SPEED, false);
				}
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateNormal)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x0007A6C8 File Offset: 0x000788C8
	private TinyFsmState StateNormal(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
		{
			bool pause = false;
			if (this.m_reqPause || this.m_reqTutorialPause)
			{
				pause = true;
			}
			this.HudPlayerChangeCharaButton(false, pause);
			this.m_reqPause = false;
			if (this.m_levelInformation != null)
			{
				this.m_levelInformation.RequestPause = this.m_reqPause;
			}
			this.EnablePause(false);
			return TinyFsmState.End();
		}
		case 1:
			this.EnablePause(true);
			if (this.m_quickMode && this.m_showQuickTurorial != -1)
			{
				GameObjectUtil.SendDelayedMessageToGameObject(base.gameObject, "OnMsgTutorialQuickMode", new MsgTutorialQuickMode((HudTutorial.Id)this.m_showQuickTurorial));
			}
			else if (this.m_showCharaTutorial != -1)
			{
				GameObjectUtil.SendDelayedMessageToGameObject(base.gameObject, "OnMsgTutorialChara", new MsgTutorialChara((HudTutorial.Id)this.m_showCharaTutorial));
			}
			this.m_reqPause = false;
			if (this.m_levelInformation != null)
			{
				this.m_levelInformation.RequestPause = this.m_reqPause;
			}
			this.m_reqTutorialPause = false;
			if (this.m_IsNowLastChanceHudCautionBoss)
			{
				this.SendMessageToHudCaution(HudCaution.Type.BOSS);
				SoundManager.SePlay("sys_boss_warning", "SE");
				this.m_IsNowLastChanceHudCautionBoss = false;
			}
			return TinyFsmState.End();
		case 4:
			if (this.IsEventTimeup())
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateGameOver)));
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			switch (num)
			{
			case 12339:
				if (this.m_showMapBossTutorial)
				{
					this.m_tutorialKind = HudTutorial.Kind.MAPBOSS;
					this.m_reqTutorialPause = true;
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialPause)));
				}
				else if (this.m_showEventBossTutorial)
				{
					this.m_tutorialKind = HudTutorial.Kind.EVENTBOSS;
					this.m_reqTutorialPause = true;
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialPause)));
				}
				return TinyFsmState.End();
			case 12340:
				if (this.m_showFeverBossTutorial)
				{
					this.m_tutorialKind = HudTutorial.Kind.FEVERBOSS;
					this.m_reqTutorialPause = true;
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialPause)));
				}
				return TinyFsmState.End();
			case 12341:
				if (!this.m_bossStage)
				{
					MsgTutorialItem msgTutorialItem = e.GetMessage as MsgTutorialItem;
					this.m_showItemTutorial = (int)msgTutorialItem.m_id;
					this.m_tutorialKind = HudTutorial.Kind.ITEM;
					this.m_reqTutorialPause = true;
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialPause)));
				}
				return TinyFsmState.End();
			case 12342:
				this.m_reqTutorialPause = true;
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateItemButtonTutorialPause)));
				return TinyFsmState.End();
			case 12343:
				this.m_tutorialKind = HudTutorial.Kind.CHARA;
				this.m_reqTutorialPause = true;
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialPause)));
				return TinyFsmState.End();
			case 12344:
				if (!this.m_bossStage && !this.m_tutorialStage)
				{
					MsgTutorialAction msgTutorialAction = e.GetMessage as MsgTutorialAction;
					this.m_showActionTutorial = (int)msgTutorialAction.m_id;
					this.m_tutorialKind = HudTutorial.Kind.ACTION;
					this.m_reqTutorialPause = true;
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialPause)));
				}
				return TinyFsmState.End();
			case 12345:
				if (this.m_quickMode)
				{
					MsgTutorialQuickMode msgTutorialQuickMode = e.GetMessage as MsgTutorialQuickMode;
					this.m_showQuickTurorial = (int)msgTutorialQuickMode.m_id;
					this.m_tutorialKind = HudTutorial.Kind.QUICK;
					this.m_reqTutorialPause = true;
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialPause)));
				}
				return TinyFsmState.End();
			default:
				switch (num)
				{
				case 12304:
				{
					MsgTransformPhantom msgTransformPhantom = e.GetMessage as MsgTransformPhantom;
					PhantomType type = msgTransformPhantom.m_type;
					string text = null;
					switch (type)
					{
					case PhantomType.LASER:
						text = "bgm_p_laser";
						break;
					case PhantomType.DRILL:
						text = "bgm_p_drill";
						break;
					case PhantomType.ASTEROID:
						text = "bgm_p_asteroid";
						break;
					}
					if (text != null && !this.m_bossStage)
					{
						SoundManager.BgmChange(this.m_mainBgmName, "BGM");
						SoundManager.BgmCrossFadePlay(text, "BGM_jingle", 0f);
					}
					return TinyFsmState.End();
				}
				case 12305:
					if (!this.m_bossStage)
					{
						SoundManager.BgmCrossFadeStop(1f, 1f);
					}
					return TinyFsmState.End();
				case 12306:
					if (this.m_levelInformation != null)
					{
						this.m_levelInformation.NowFeverBoss = true;
					}
					if (StageItemManager.Instance != null)
					{
						MsgPauseItemOnBoss msg = new MsgPauseItemOnBoss();
						StageItemManager.Instance.OnPauseItemOnBoss(msg);
					}
					this.SendBossStartMessageToChao();
					if (!this.m_playerInformation.IsNowLastChance())
					{
						this.SendMessageToHudCaution(HudCaution.Type.BOSS);
						SoundManager.SePlay("sys_boss_warning", "SE");
					}
					else
					{
						this.m_IsNowLastChanceHudCautionBoss = true;
					}
					if (this.m_quickMode && StageTimeManager.Instance != null)
					{
						StageTimeManager.Instance.Pause();
					}
					break;
				case 12307:
				{
					MsgBossEnd msgBossEnd = e.GetMessage as MsgBossEnd;
					if (msgBossEnd != null)
					{
						if (this.m_bossStage)
						{
							if (this.m_levelInformation != null)
							{
								this.m_levelInformation.BossDestroy = msgBossEnd.m_dead;
							}
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePrepareUpdateDatabase)));
							return TinyFsmState.End();
						}
						if (this.m_levelInformation != null)
						{
							this.m_levelInformation.InvalidExtreme = false;
							this.DrawingInvalidExtreme();
						}
						if (StageItemManager.Instance != null)
						{
							MsgPauseItemOnChageLevel msg2 = new MsgPauseItemOnChageLevel();
							StageItemManager.Instance.OnPauseItemOnChangeLevel(msg2);
						}
						GameObjectUtil.SendMessageToTagObjects("Chao", "OnPauseChangeLevel", null, SendMessageOptions.DontRequireReceiver);
						if (this.m_tutorialStage)
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateEndFadeOut)));
						}
						else
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateChangeLevel)));
						}
						return TinyFsmState.End();
					}
					break;
				}
				case 12308:
					if (this.m_bossStage)
					{
						this.m_bossClear = true;
					}
					break;
				default:
					switch (num)
					{
					case 12333:
						if (this.m_tutorialManager != null)
						{
							MsgTutorialPlayStart msgTutorialPlayStart = e.GetMessage as MsgTutorialPlayStart;
							if (msgTutorialPlayStart != null)
							{
								this.m_tutorialMissionID = msgTutorialPlayStart.m_eventID;
								this.m_tutorialKind = HudTutorial.Kind.MISSION;
								this.m_reqTutorialPause = true;
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialPause)));
							}
						}
						return TinyFsmState.End();
					default:
						if (num == 4096)
						{
							this.m_reqPauseBackMain = false;
							this.m_reqPause = true;
							if (this.m_levelInformation != null)
							{
								this.m_levelInformation.RequestPause = this.m_reqPause;
							}
							return TinyFsmState.End();
						}
						if (num == 12329)
						{
							MsgInvincible msgInvincible = e.GetMessage as MsgInvincible;
							if (msgInvincible != null)
							{
								if (msgInvincible.m_mode == MsgInvincible.Mode.Start)
								{
									SoundManager.ItemBgmCrossFadePlay("jingle_invincible", "BGM_jingle", 0f);
								}
								else
								{
									SoundManager.BgmCrossFadeStop(1f, 0.5f);
								}
							}
							return TinyFsmState.End();
						}
						if (num == 12361)
						{
							if (this.m_quickMode)
							{
								this.m_quickModeTimeUp = true;
								if (StageTimeManager.Instance != null)
								{
									StageTimeManager.Instance.Pause();
								}
								if (this.IsEnableContinue())
								{
									this.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckContinue)));
								}
								else
								{
									this.ChangeState(new TinyFsmState(new EventFunction(this.StateQuickModeTimeUp)));
								}
							}
							return TinyFsmState.End();
						}
						if (num == 20480)
						{
							if (!this.m_bossClear)
							{
								if (this.m_quickMode && StageTimeManager.Instance != null)
								{
									StageTimeManager.Instance.Pause();
								}
								if (this.IsEnableContinue())
								{
									this.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckContinue)));
								}
								else if (this.m_firstTutorial)
								{
									this.ChangeState(new TinyFsmState(new EventFunction(this.StateSendApolloStageEnd)));
								}
								else if (this.m_tutorialStage)
								{
									this.ChangeState(new TinyFsmState(new EventFunction(this.StateEndFadeOut)));
								}
								else
								{
									this.ChangeState(new TinyFsmState(new EventFunction(this.StateGameOver)));
								}
							}
							return TinyFsmState.End();
						}
						break;
					case 12335:
					{
						MsgTutorialPlayEnd tutorialEndMsg = e.GetMessage as MsgTutorialPlayEnd;
						this.m_tutorialEndMsg = tutorialEndMsg;
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialMissionEnd)));
						return TinyFsmState.End();
					}
					}
					break;
				case 12313:
					if (this.m_levelInformation != null)
					{
						this.m_levelInformation.RequestCharaChange = true;
					}
					if (this.m_characterContainer != null)
					{
						MsgChangeChara msgChangeChara = e.GetMessage as MsgChangeChara;
						if (msgChangeChara != null)
						{
							this.m_characterContainer.SendMessage("OnMsgChangeChara", e.GetMessage);
						}
					}
					break;
				}
				return TinyFsmState.End();
			case 12350:
				GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnPhantomActStart", e.GetMessage, SendMessageOptions.DontRequireReceiver);
				return TinyFsmState.End();
			case 12351:
				GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnPhantomActEnd", e.GetMessage, SendMessageOptions.DontRequireReceiver);
				return TinyFsmState.End();
			case 12358:
			{
				MsgExternalGamePause msgExternalGamePause = e.GetMessage as MsgExternalGamePause;
				this.m_reqPauseBackMain = msgExternalGamePause.m_backMainMenu;
				this.m_reqPause = true;
				if (this.m_levelInformation != null)
				{
					this.m_levelInformation.RequestPause = this.m_reqPause;
				}
				return TinyFsmState.End();
			}
			}
			break;
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x0007B0C0 File Offset: 0x000792C0
	private TinyFsmState StatePause(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			StageDebugInformation.DestroyActivateButton();
			this.SetDefaultTimeScale();
			return TinyFsmState.End();
		case 1:
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnSetPause", new MsgSetPause(this.m_reqPauseBackMain, false), SendMessageOptions.DontRequireReceiver);
			this.m_reqPauseBackMain = false;
			this.SetTimeScale(0f);
			SoundManager.BgmPause(true);
			SoundManager.SePausePlaying(true);
			SoundManager.SePlay("sys_pause", "SE");
			StageDebugInformation.CreateActivateButton();
			GC.Collect();
			Resources.UnloadUnusedAssets();
			GC.Collect();
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num == 4097)
			{
				SoundManager.BgmPause(false);
				SoundManager.SePausePlaying(false);
				this.HudPlayerChangeCharaButton(true, true);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateNormal)));
				return TinyFsmState.End();
			}
			if (num == 4098)
			{
				this.m_retired = true;
				this.HoldPlayerAndDestroyTerrainOnEnd();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateUpdateDatabase)));
				return TinyFsmState.End();
			}
			if (num != 12358)
			{
				return TinyFsmState.End();
			}
			MsgExternalGamePause msgExternalGamePause = e.GetMessage as MsgExternalGamePause;
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnSetPause", new MsgSetPause(msgExternalGamePause.m_backMainMenu, msgExternalGamePause.m_backKey), SendMessageOptions.DontRequireReceiver);
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x0007B244 File Offset: 0x00079444
	private TinyFsmState StateChangeLevel(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			this.m_onSpeedUp = false;
			this.m_onDestroyRingMode = false;
			return TinyFsmState.End();
		case 1:
			this.m_timer = 0.4f;
			this.SendMessageToHudCaution(HudCaution.Type.STAGE_OUT);
			SoundManager.SePlay("boss_scene_change", "SE");
			this.m_substate = 0;
			return TinyFsmState.End();
		case 4:
			switch (this.m_substate)
			{
			case 0:
				this.m_timer -= e.GetDeltaTime;
				if (this.m_timer <= 0f)
				{
					GC.Collect();
					Resources.UnloadUnusedAssets();
					GC.Collect();
					StageScoreManager instance = StageScoreManager.Instance;
					if (instance != null)
					{
						StageScorePool scorePool = instance.ScorePool;
						if (scorePool != null)
						{
							scorePool.AddScore(ScoreType.distance, (int)this.m_playerInformation.TotalDistance);
							scorePool.CheckHalfWay();
						}
					}
					MsgPLHold value = new MsgPLHold();
					GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgPLHold", value, SendMessageOptions.DontRequireReceiver);
					this.ResetPosStageEffect(true);
					if (TenseEffectManager.Instance)
					{
						TenseEffectManager.Instance.FlipTenseType();
					}
					if (this.m_stageBlockManager != null)
					{
						StageBlockManager component = this.m_stageBlockManager.GetComponent<StageBlockManager>();
						if (component != null)
						{
							component.ReCreateTerrain();
						}
					}
					this.m_timer = 0.6f;
					this.m_counter = 2;
					this.m_substate = 1;
				}
				break;
			case 1:
				this.m_timer -= e.GetDeltaTime;
				this.m_counter--;
				if (this.m_timer <= 0f && this.m_counter < 0)
				{
					PlayerSpeed speedLevel = (PlayerSpeed)Mathf.Min((int)(this.m_playerInformation.SpeedLevel + 1), 2);
					MsgPrepareStageReplace value2 = new MsgPrepareStageReplace(speedLevel, this.m_stageName);
					this.m_stageBlockManager.SendMessage("OnMsgPrepareStageReplace", value2);
					ObjUtil.PauseCombo(MsgPauseComboTimer.State.PAUSE, -1f);
					this.m_substate = 2;
				}
				break;
			case 2:
				if (this.m_playerInformation)
				{
					bool flag = false;
					if (this.m_stageBlockManager != null)
					{
						flag = this.m_stageBlockManager.GetComponent<StageBlockManager>().IsBlockLevelUp();
					}
					switch (this.m_playerInformation.SpeedLevel)
					{
					case PlayerSpeed.LEVEL_1:
						if (flag)
						{
							this.m_onSpeedUp = true;
							this.SendPlayerSpeedLevel(PlayerSpeed.LEVEL_2);
						}
						break;
					case PlayerSpeed.LEVEL_2:
						if (flag)
						{
							this.m_onSpeedUp = true;
							this.SendPlayerSpeedLevel(PlayerSpeed.LEVEL_3);
						}
						break;
					case PlayerSpeed.LEVEL_3:
						if (flag)
						{
							this.m_onDestroyRingMode = true;
							if (this.m_levelInformation != null)
							{
								this.m_levelInformation.Extreme = true;
							}
						}
						break;
					}
				}
				this.m_counter = 3;
				this.m_substate = 3;
				break;
			case 3:
				if (this.m_counter > 0)
				{
					this.m_counter--;
				}
				if (this.m_counter <= 0 && this.m_stageBlockManager.GetComponent<StageBlockManager>().IsSetupEnded())
				{
					this.m_substate = 4;
				}
				break;
			case 4:
			{
				if (this.m_playerInformation)
				{
					PlayerSpeed speedLevel2 = this.m_playerInformation.SpeedLevel;
					MsgStageReplace msgStageReplace = new MsgStageReplace(speedLevel2, this.PlayerResetPosition, this.PlayerResetRotation, this.m_stageName);
					GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgStageReplace", msgStageReplace, SendMessageOptions.RequireReceiver);
					GameObjectUtil.SendMessageToTagObjects("Chao", "OnMsgStageReplace", msgStageReplace, SendMessageOptions.DontRequireReceiver);
					if (this.m_stageBlockManager != null)
					{
						StageBlockManager component2 = this.m_stageBlockManager.GetComponent<StageBlockManager>();
						if (component2 != null)
						{
							component2.OnMsgStageReplace(msgStageReplace);
						}
					}
					StageFarTerrainManager stageFarTerrainManager = GameObjectUtil.FindGameObjectComponent<StageFarTerrainManager>("StageFarManager");
					if (stageFarTerrainManager != null)
					{
						stageFarTerrainManager.SendMessage("OnMsgStageReplace", msgStageReplace, SendMessageOptions.DontRequireReceiver);
					}
				}
				if (this.m_levelInformation != null)
				{
					this.m_levelInformation.NowFeverBoss = false;
				}
				HudCockpit hudCockpit = GameObjectUtil.FindGameObjectComponent<HudCockpit>("HudCockpit");
				if (hudCockpit != null)
				{
					MsgBossEnd value3 = new MsgBossEnd(true);
					hudCockpit.SendMessage("OnBossEnd", value3);
				}
				this.m_counter = 3;
				this.m_substate = 5;
				break;
			}
			case 5:
				if (--this.m_counter < 0)
				{
					MsgStageRestart value4 = new MsgStageRestart();
					GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgStageRestart", value4, SendMessageOptions.RequireReceiver);
					this.SendMessageToHudCaution(HudCaution.Type.STAGE_IN);
					this.m_substate = 6;
					this.m_timer = 0.5f;
				}
				break;
			case 6:
				this.m_timer -= e.GetDeltaTime;
				if (this.m_timer <= 0f)
				{
					this.ResetPosStageEffect(false);
					if (this.m_onSpeedUp || this.m_onDestroyRingMode || this.m_invalidExtremeFlag)
					{
						bool flag2 = false;
						if (this.m_onSpeedUp)
						{
							this.SendMessageToHudCaution(HudCaution.Type.SPEEDUP);
						}
						else
						{
							if (this.m_levelInformation != null && this.m_levelInformation.InvalidExtreme)
							{
								flag2 = true;
								if (this.m_levelInformation != null && this.m_levelInformation.InvalidExtreme)
								{
									ObjUtil.RequestStartAbilityToChao(ChaoAbility.INVALIDI_EXTREME_STAGE, false);
								}
							}
							if (!flag2)
							{
								this.m_invalidExtremeFlag = false;
								this.SendMessageToHudCaution(HudCaution.Type.EXTREMEMODE);
							}
						}
						if (!flag2)
						{
							SoundManager.SePlay("sys_speedup", "SE");
						}
					}
					if (this.m_tutorialStage && this.m_playerInformation.SpeedLevel == PlayerSpeed.LEVEL_2)
					{
						if (StageItemManager.Instance != null)
						{
							MsgUseEquipItem msg = new MsgUseEquipItem();
							StageItemManager.Instance.OnUseEquipItem(msg);
						}
						if (this.m_levelInformation != null)
						{
							this.m_levelInformation.NowTutorial = false;
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateNormal)));
					}
					else
					{
						this.HudPlayerChangeCharaButton(true, false);
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateNormal)));
					}
					return TinyFsmState.End();
				}
				break;
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x0007B87C File Offset: 0x00079A7C
	private void SendPlayerSpeedLevel(PlayerSpeed speedLevel)
	{
		MsgUpSpeedLevel msgUpSpeedLevel = new MsgUpSpeedLevel(speedLevel);
		if (msgUpSpeedLevel != null)
		{
			GameObjectUtil.SendMessageToTagObjects("Player", "OnUpSpeedLevel", msgUpSpeedLevel, SendMessageOptions.DontRequireReceiver);
			GameObjectUtil.SendMessageFindGameObject("PlayerInformation", "OnUpSpeedLevel", msgUpSpeedLevel, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x0007B8BC File Offset: 0x00079ABC
	private HudTutorial.Id GetHudTutorialID(HudTutorial.Kind kind)
	{
		switch (kind)
		{
		case HudTutorial.Kind.MISSION:
			return (HudTutorial.Id)this.m_tutorialMissionID;
		case HudTutorial.Kind.MISSION_END:
			return HudTutorial.Id.MISSION_END;
		case HudTutorial.Kind.FEVERBOSS:
			return HudTutorial.Id.FEVERBOSS;
		case HudTutorial.Kind.MAPBOSS:
			return HudTutorial.Id.MAPBOSS_1 + BossTypeUtil.GetIndexNumber(this.m_bossType);
		case HudTutorial.Kind.EVENTBOSS:
			return HudTutorial.Id.EVENTBOSS_1 + BossTypeUtil.GetIndexNumber(this.m_bossType);
		case HudTutorial.Kind.ITEM:
			return (HudTutorial.Id)this.m_showItemTutorial;
		case HudTutorial.Kind.CHARA:
			return (HudTutorial.Id)this.m_showCharaTutorial;
		case HudTutorial.Kind.ACTION:
			return (HudTutorial.Id)this.m_showActionTutorial;
		case HudTutorial.Kind.QUICK:
			return (HudTutorial.Id)this.m_showQuickTurorial;
		}
		return HudTutorial.Id.NONE;
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x0007B948 File Offset: 0x00079B48
	private void EndTutorial(HudTutorial.Kind kind)
	{
		switch (kind)
		{
		case HudTutorial.Kind.MISSION_END:
			if (StageItemManager.Instance != null)
			{
				MsgUseEquipItem msg = new MsgUseEquipItem();
				StageItemManager.Instance.OnUseEquipItem(msg);
			}
			if (this.m_levelInformation != null)
			{
				this.m_levelInformation.NowTutorial = false;
			}
			break;
		case HudTutorial.Kind.FEVERBOSS:
			this.SetEndBossTutorialFlag(SystemData.FlagStatus.TUTORIAL_FEVER_BOSS);
			this.m_showFeverBossTutorial = false;
			break;
		case HudTutorial.Kind.MAPBOSS:
			switch (this.m_bossType)
			{
			case BossType.MAP1:
				this.SetEndBossTutorialFlag(SystemData.FlagStatus.TUTORIAL_BOSS_MAP_1);
				break;
			case BossType.MAP2:
				this.SetEndBossTutorialFlag(SystemData.FlagStatus.TUTORIAL_BOSS_MAP_2);
				break;
			case BossType.MAP3:
				this.SetEndBossTutorialFlag(SystemData.FlagStatus.TUTORIAL_BOSS_MAP_3);
				break;
			}
			break;
		case HudTutorial.Kind.ITEM:
			if (this.m_showItemTutorial != -1)
			{
				ItemType itemType = ItemType.UNKNOWN;
				for (int i = 0; i < 8; i++)
				{
					HudTutorial.Id itemTutorialID = ItemTypeName.GetItemTutorialID((ItemType)i);
					if (itemTutorialID == (HudTutorial.Id)this.m_showItemTutorial)
					{
						itemType = (ItemType)i;
						break;
					}
				}
				if (itemType != ItemType.UNKNOWN)
				{
					this.SetEndItemTutorialFlag(ItemTypeName.GetItemTutorialStatus(itemType));
				}
				this.m_showItemTutorial = -1;
			}
			break;
		case HudTutorial.Kind.CHARA:
			if (this.m_showCharaTutorial != -1)
			{
				CharaType commonTextCharaName = HudTutorial.GetCommonTextCharaName((HudTutorial.Id)this.m_showCharaTutorial);
				if (commonTextCharaName != CharaType.UNKNOWN)
				{
					this.SetEndCharaTutorialFlag(CharaTypeUtil.GetCharacterSaveDataFlagStatus(commonTextCharaName));
				}
				this.m_showCharaTutorial = -1;
			}
			break;
		case HudTutorial.Kind.ACTION:
			if (this.m_showActionTutorial != -1)
			{
				this.SetEndActionTutorialFlag(HudTutorial.GetActionTutorialSaveFlag((HudTutorial.Id)this.m_showActionTutorial));
				this.m_showActionTutorial = -1;
			}
			break;
		case HudTutorial.Kind.QUICK:
			if (this.m_showQuickTurorial != -1)
			{
				this.SetEndQuickModeTutorialFlag(HudTutorial.GetQuickModeTutorialSaveFlag((HudTutorial.Id)this.m_showQuickTurorial));
				this.m_showQuickTurorial = -1;
			}
			break;
		}
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x0007BB20 File Offset: 0x00079D20
	private void SetEndBossTutorialFlag(SystemData.FlagStatus flagStatus)
	{
		if (flagStatus != SystemData.FlagStatus.NONE)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null && !systemdata.IsFlagStatus(flagStatus))
				{
					systemdata.SetFlagStatus(flagStatus, true);
					this.m_saveFlag = true;
				}
			}
		}
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x0007BB70 File Offset: 0x00079D70
	private void SetEndItemTutorialFlag(SystemData.ItemTutorialFlagStatus flagStatus)
	{
		if (flagStatus != SystemData.ItemTutorialFlagStatus.NONE)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null && !systemdata.IsFlagStatus(flagStatus))
				{
					systemdata.SetFlagStatus(flagStatus, true);
					this.m_saveFlag = true;
				}
			}
		}
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x0007BBC0 File Offset: 0x00079DC0
	private void SetEndCharaTutorialFlag(SystemData.CharaTutorialFlagStatus flagStatus)
	{
		if (flagStatus != SystemData.CharaTutorialFlagStatus.NONE)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null && !systemdata.IsFlagStatus(flagStatus))
				{
					systemdata.SetFlagStatus(flagStatus, true);
					this.m_saveFlag = true;
				}
			}
		}
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x0007BC10 File Offset: 0x00079E10
	private void SetEndActionTutorialFlag(SystemData.ActionTutorialFlagStatus flagStatus)
	{
		if (flagStatus != SystemData.ActionTutorialFlagStatus.NONE)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null && !systemdata.IsFlagStatus(flagStatus))
				{
					systemdata.SetFlagStatus(flagStatus, true);
					this.m_saveFlag = true;
				}
			}
		}
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x0007BC60 File Offset: 0x00079E60
	private void SetEndQuickModeTutorialFlag(SystemData.QuickModeTutorialFlagStatus flagStatus)
	{
		if (flagStatus != SystemData.QuickModeTutorialFlagStatus.NONE)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null && !systemdata.IsFlagStatus(flagStatus))
				{
					systemdata.SetFlagStatus(flagStatus, true);
					this.m_saveFlag = true;
				}
			}
		}
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x0007BCB0 File Offset: 0x00079EB0
	private TinyFsmState StateTutorialPause(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			this.SetDefaultTimeScale();
			SoundManager.SePausePlaying(false);
			return TinyFsmState.End();
		case 1:
		{
			this.SetTimeScale(0f);
			HudTutorial.Id hudTutorialID = this.GetHudTutorialID(this.m_tutorialKind);
			HudTutorial.StartTutorial(hudTutorialID, this.m_bossType);
			SoundManager.SePausePlaying(true);
			SoundManager.SePlay("sys_pause", "SE");
			return TinyFsmState.End();
		}
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num == 12334)
			{
				this.EndTutorial(this.m_tutorialKind);
				this.HudPlayerChangeCharaButton(true, true);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateNormal)));
				return TinyFsmState.End();
			}
			if (num != 12349)
			{
				return TinyFsmState.End();
			}
			HudTutorial.PushBackKey();
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x0007BDB4 File Offset: 0x00079FB4
	private TinyFsmState StateItemButtonTutorialPause(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			this.SetDefaultTimeScale();
			SoundManager.SePausePlaying(false);
			return TinyFsmState.End();
		case 1:
			this.SetTimeScale(0f);
			SoundManager.SePausePlaying(true);
			SoundManager.SePlay("sys_pause", "SE");
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			if (id == 12331)
			{
				this.HudPlayerChangeCharaButton(true, true);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateNormal)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x0007BE78 File Offset: 0x0007A078
	private TinyFsmState StateTutorialMissionEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			this.m_tutorialEndMsg = null;
			if (StageTutorialManager.Instance)
			{
				GameObjectUtil.SendDelayedMessageToGameObject(StageTutorialManager.Instance.gameObject, "OnMsgTutorialNext", new MsgTutorialNext());
			}
			return TinyFsmState.End();
		case 1:
			if (this.m_tutorialEndMsg != null)
			{
				if (this.m_tutorialEndMsg.m_retry)
				{
					HudTutorial.RetryTutorial();
				}
				else
				{
					HudTutorial.SuccessTutorial();
				}
				if (!this.m_tutorialEndMsg.m_complete)
				{
					this.m_timer = 1f;
					this.m_substate = 0;
				}
				else
				{
					this.m_substate = 4;
				}
			}
			return TinyFsmState.End();
		case 4:
			switch (this.m_substate)
			{
			case 0:
				this.m_timer -= e.GetDeltaTime;
				if (this.m_timer <= 0f)
				{
					if (!this.m_tutorialEndMsg.m_complete)
					{
						CameraFade.StartAlphaFade(Color.white, false, 1f);
						this.m_timer = 1f;
					}
					else
					{
						this.m_timer = 0f;
					}
					this.m_substate = 1;
				}
				break;
			case 1:
				this.m_timer -= e.GetDeltaTime;
				if (this.m_timer <= 0f)
				{
					if (this.m_tutorialEndMsg != null)
					{
						if (!this.m_tutorialEndMsg.m_complete)
						{
							MsgWarpPlayer value = new MsgWarpPlayer(this.m_tutorialEndMsg.m_pos, this.PlayerResetRotation, true);
							GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgWarpPlayer", value, SendMessageOptions.DontRequireReceiver);
						}
						if (this.m_tutorialEndMsg.m_retry)
						{
							bool blink = true;
							if (this.m_tutorialEndMsg.m_nextEventID <= Tutorial.EventID.JUMP)
							{
								blink = false;
							}
							MsgTutorialResetForRetry value2 = new MsgTutorialResetForRetry(this.m_tutorialEndMsg.m_pos, this.PlayerResetRotation, blink);
							GameObjectUtil.SendMessageFindGameObject("StageBlockManager", "OnMsgTutorialResetForRetry", value2, SendMessageOptions.DontRequireReceiver);
							GameObjectUtil.SendMessageFindGameObject("StageTutorialManager", "OnMsgTutorialResetForRetry", value2, SendMessageOptions.DontRequireReceiver);
							GameObjectUtil.SendMessageToTagObjects("MainCamera", "OnMsgTutorialResetForRetry", value2, SendMessageOptions.DontRequireReceiver);
						}
					}
					GameObject[] array = GameObject.FindGameObjectsWithTag("Animal");
					foreach (GameObject gameObject in array)
					{
						gameObject.SendMessage("OnDestroyAnimal", SendMessageOptions.DontRequireReceiver);
					}
					ObjAnimalBase.DestroyAnimalEffect();
					ObjUtil.StopCombo();
					HudTutorial.EndTutorial();
					this.m_counter = 4;
					this.m_substate = 2;
				}
				break;
			case 2:
				if (--this.m_counter < 0)
				{
					MsgPLReleaseHold value3 = new MsgPLReleaseHold();
					GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgPLReleaseHold", value3, SendMessageOptions.DontRequireReceiver);
					MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.COME_IN);
					if (!this.m_tutorialEndMsg.m_complete)
					{
						this.m_timer = 1f;
						CameraFade.StartAlphaFade(Color.white, true, 1f);
					}
					else
					{
						this.m_timer = 0f;
					}
					this.m_substate = 3;
				}
				ObjAnimalBase.DestroyAnimalEffect();
				break;
			case 3:
				this.m_timer -= e.GetDeltaTime;
				if (this.m_timer <= 0f)
				{
					this.m_substate = 4;
					return TinyFsmState.End();
				}
				break;
			case 4:
				this.HudPlayerChangeCharaButton(true, false);
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateNormal)));
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num != 12334)
			{
				return TinyFsmState.End();
			}
			if (this.m_substate == 0)
			{
				CameraFade.StartAlphaFade(Color.white, false, 1f);
				this.m_timer = 1f;
				this.m_substate = 1;
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015F7 RID: 5623 RVA: 0x0007C254 File Offset: 0x0007A454
	private TinyFsmState StateGameOver(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_gameResultTimer = 1f;
			return TinyFsmState.End();
		case 4:
			this.m_gameResultTimer -= Time.deltaTime;
			if (this.m_gameResultTimer < 0f)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StatePrepareUpdateDatabase)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x0007C2E4 File Offset: 0x0007A4E4
	private TinyFsmState StateCheckContinue(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			this.SetDefaultTimeScale();
			return TinyFsmState.End();
		case 1:
			this.m_gameResultTimer = 1f;
			this.SetTimeScale(0f);
			ObjUtil.SetHudStockRingEffectOff(true);
			SoundManager.BgmChange(this.m_mainBgmName, "BGM");
			SoundManager.BgmCrossFadePlay("bgm_continue", "BGM_jingle", 0f);
			if (this.m_continueWindowObj != null)
			{
				HudContinue component = this.m_continueWindowObj.GetComponent<HudContinue>();
				if (component != null)
				{
					if (this.m_quickMode)
					{
						component.SetTimeUp(this.m_quickModeTimeUp);
					}
					this.m_continueWindowObj.SetActive(true);
					component.PlayStart();
				}
			}
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			switch (num)
			{
			case 12352:
			{
				MsgContinueResult msgContinueResult = e.GetMessage as MsgContinueResult;
				if (msgContinueResult != null)
				{
					if (!msgContinueResult.m_result)
					{
						SoundManager.BgmStop();
						this.ChangeState(new TinyFsmState(new EventFunction(this.StatePrepareUpdateDatabase)));
						return TinyFsmState.End();
					}
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePrepareContinue)));
				}
				break;
			}
			default:
				if (num == 12329)
				{
					MsgInvincible msgInvincible = e.GetMessage as MsgInvincible;
					if (msgInvincible != null && msgInvincible.m_mode == MsgInvincible.Mode.Start)
					{
						SoundManager.ItemBgmCrossFadePlay("jingle_invincible", "BGM_jingle", 0f);
						this.m_receiveInvincibleMsg = true;
					}
				}
				break;
			case 12354:
			{
				MsgContinueBackKey msgContinueBackKey = e.GetMessage as MsgContinueBackKey;
				if (msgContinueBackKey != null && this.m_continueWindowObj != null)
				{
					HudContinue component2 = this.m_continueWindowObj.GetComponent<HudContinue>();
					if (component2 != null)
					{
						component2.PushBackKey();
					}
				}
				break;
			}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x0007C4F8 File Offset: 0x0007A6F8
	private TinyFsmState StatePrepareContinue(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			ObjUtil.SetHudStockRingEffectOff(false);
			return TinyFsmState.End();
		case 1:
		{
			this.m_numEnableContinue--;
			MsgPrepareContinue value = new MsgPrepareContinue(this.m_bossStage, this.m_quickModeTimeUp);
			GameObjectUtil.SendMessageFindGameObject("CharacterContainer", "OnMsgPrepareContinue", value, SendMessageOptions.DontRequireReceiver);
			GameObjectUtil.SendMessageToTagObjects("Boss", "OnMsgPrepareContinue", value, SendMessageOptions.DontRequireReceiver);
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnMsgPrepareContinue", value, SendMessageOptions.DontRequireReceiver);
			if (this.m_quickMode && StageTimeManager.Instance != null)
			{
				StageTimeManager.Instance.ExtendTime(StageTimeManager.ExtendPattern.CONTINUE);
			}
			if (this.m_continueWindowObj != null)
			{
				this.m_continueWindowObj.SetActive(false);
			}
			if (this.m_bossStage && this.m_levelInformation != null)
			{
				this.m_levelInformation.DistanceToBossOnStart = this.m_levelInformation.DistanceOnStage;
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_quickMode)
			{
				this.m_quickModeTimeUp = false;
				if (StageTimeManager.Instance != null)
				{
					StageTimeManager.Instance.PlayStart();
				}
			}
			if (!this.m_receiveInvincibleMsg)
			{
				SoundManager.BgmCrossFadeStop(0f, 1f);
			}
			this.m_receiveInvincibleMsg = false;
			this.HudPlayerChangeCharaButton(true, true);
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateNormal)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x0007C680 File Offset: 0x0007A880
	private TinyFsmState StateQuickModeTimeUp(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_timer = 0.4f;
			this.SendMessageToHudCaution(HudCaution.Type.STAGE_OUT);
			SoundManager.BgmStop();
			if (this.m_levelInformation != null)
			{
				this.m_levelInformation.RequestPause = this.m_reqPause;
			}
			GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgPLHold", new MsgPLHold(), SendMessageOptions.DontRequireReceiver);
			this.m_substate = 0;
			return TinyFsmState.End();
		case 4:
			this.m_timer -= e.GetDeltaTime;
			if (this.m_timer <= 0f)
			{
				GC.Collect();
				Resources.UnloadUnusedAssets();
				GC.Collect();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StatePrepareUpdateDatabase)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x0007C76C File Offset: 0x0007A96C
	private TinyFsmState StatePrepareUpdateDatabase(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			ObjUtil.SetHudStockRingEffectOff(true);
			ObjUtil.SendMessageScoreCheck(new StageScoreData(10, (int)this.m_playerInformation.TotalDistance));
			ObjUtil.SendMessageFinalScoreBeforeResult();
			GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject gameObject in array)
			{
				gameObject.SetActive(false);
			}
			bool flag = false;
			if (this.m_levelInformation != null)
			{
				flag = this.m_levelInformation.BossDestroy;
			}
			if (this.m_gameResult != null)
			{
				this.m_gameResult.gameObject.SetActive(true);
				SaveDataUtil.ReflctResultsData();
				bool isNoMiss = this.EnableChaoEgg();
				bool isBossTutorialClear = this.IsBossTutorialClear();
				this.m_gameResult.PlayBGStart((!this.m_bossStage) ? GameResult.ResultType.NORMAL : GameResult.ResultType.BOSS, isNoMiss, isBossTutorialClear, flag, this.GetEventResultState());
			}
			SoundManager.BgmStop();
			if (this.m_bossStage && flag)
			{
				SoundManager.BgmPlay("jingle_sys_bossclear", "BGM_jingle", false);
			}
			else
			{
				SoundManager.BgmPlay("jingle_sys_clear", "BGM_jingle", false);
			}
			this.m_timer = 0.5f;
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_timer > 0f)
			{
				this.m_timer -= e.GetDeltaTime;
				if (this.m_timer <= 0f)
				{
					this.HoldPlayerAndDestroyTerrainOnEnd();
					GameObjectUtil.SendDelayedMessageFindGameObject("HudCockpit", "OnMsgExitStage", new MsgExitStage());
					if (EventManager.Instance.IsRaidBossStage())
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateUpdateRaidBossState)));
					}
					else if (this.m_quickMode)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateUpdateQuickModeDatabase)));
					}
					else
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateUpdateDatabase)));
					}
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x0007C988 File Offset: 0x0007AB88
	private TinyFsmState StateUpdateDatabase(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_counter = 0;
			if (this.m_fromTitle || this.m_firstTutorial)
			{
				this.m_retired = true;
			}
			this.m_exitFromResult = !this.m_retired;
			if (this.m_exitFromResult)
			{
				this.SetMissionResult();
			}
			EventResultState eventResultState = this.GetEventResultState();
			EventManager instance = EventManager.Instance;
			bool flag;
			bool flag2;
			if (eventResultState == EventResultState.TIMEUP)
			{
				if (instance.IsCollectEvent())
				{
					flag = true;
					flag2 = false;
				}
				else
				{
					flag = false;
					flag2 = false;
				}
			}
			else
			{
				flag = true;
				flag2 = true;
			}
			if (!flag || this.m_firstTutorial)
			{
				base.StartCoroutine(this.NotSendPostGameResult());
			}
			else
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null && SaveDataManager.Instance != null)
				{
					bool chaoEggPresent = this.EnableChaoEgg();
					int? eventId = null;
					if (flag2 && (instance.EventStage || instance.IsCollectEvent()))
					{
						eventId = new int?(0);
						eventId = new int?(instance.Id);
					}
					long? eventValue = null;
					if (eventId != null)
					{
						StageScoreManager instance2 = StageScoreManager.Instance;
						if (instance2 != null)
						{
							eventValue = new long?(0L);
							if (instance.IsCollectEvent())
							{
								eventValue = new long?(instance2.CollectEventCount);
							}
							else
							{
								eventValue = new long?(instance2.FinalCountData.sp_crystal);
							}
						}
					}
					ServerGameResults serverGameResults = new ServerGameResults(!this.m_exitFromResult, this.m_tutorialStage, chaoEggPresent, this.m_bossStage, this.m_oldNumBossAttack, eventId, eventValue);
					if (serverGameResults != null)
					{
						if (this.m_postGameResults.m_prevMapInfo != null)
						{
							serverGameResults.SetMapProgress(this.m_postGameResults.m_prevMapInfo.m_mapState, ref this.m_postGameResults.m_prevMapInfo.m_pointScore, this.m_postGameResults.m_existBoss);
						}
						loggedInServerInterface.RequestServerPostGameResults(serverGameResults, base.gameObject);
					}
				}
				else
				{
					this.m_counter++;
				}
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_counter > 0 && AchievementManager.IsRequestEnd())
			{
				if (this.m_saveFlag)
				{
					SystemSaveManager instance3 = SystemSaveManager.Instance;
					if (instance3 != null)
					{
						instance3.SaveSystemData();
					}
				}
				if (this.m_retired)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateSendApolloStageEnd)));
				}
				else if (this.IsRaidBossStateUpdate())
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateEventDrawRaidBoss)));
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateResult)));
				}
			}
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num == 61449)
			{
				if (ServerInterface.LoggedInServerInterface != null)
				{
					ServerTickerInfo tickerInfo = ServerInterface.TickerInfo;
					if (tickerInfo != null)
					{
						tickerInfo.ExistNewData = true;
					}
				}
				AchievementManager.RequestUpdate();
				if (this.m_exitFromResult && this.IsBossTutorialLose())
				{
					this.SetBossTutorialPresent();
				}
				this.m_counter++;
				return TinyFsmState.End();
			}
			if (num != 61517)
			{
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x0007CD14 File Offset: 0x0007AF14
	private TinyFsmState StateUpdateQuickModeDatabase(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_counter = 0;
			this.m_exitFromResult = !this.m_retired;
			if (!this.m_exitFromResult)
			{
				base.StartCoroutine(this.NotSendPostGameResult());
			}
			else
			{
				this.SetMissionResult();
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null && SaveDataManager.Instance != null)
				{
					if (StageTimeManager.Instance != null)
					{
						StageTimeManager.Instance.CheckResultTimer();
					}
					ServerQuickModeGameResults serverQuickModeGameResults = new ServerQuickModeGameResults(!this.m_exitFromResult);
					if (serverQuickModeGameResults != null)
					{
						loggedInServerInterface.RequestServerQuickModePostGameResults(serverQuickModeGameResults, base.gameObject);
					}
				}
				else
				{
					this.m_counter++;
				}
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_counter > 0 && AchievementManager.IsRequestEnd())
			{
				if (this.m_saveFlag)
				{
					SystemSaveManager instance = SystemSaveManager.Instance;
					if (instance != null)
					{
						instance.SaveSystemData();
					}
				}
				if (this.m_retired)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateSendApolloStageEnd)));
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateDailyBattleResult)));
				}
			}
			return TinyFsmState.End();
		case 5:
			switch (e.GetMessage.ID)
			{
			case 61514:
				if (ServerInterface.LoggedInServerInterface != null)
				{
					ServerTickerInfo tickerInfo = ServerInterface.TickerInfo;
					if (tickerInfo != null)
					{
						tickerInfo.ExistNewData = true;
					}
				}
				AchievementManager.RequestUpdate();
				this.m_counter++;
				return TinyFsmState.End();
			case 61517:
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x0007CF04 File Offset: 0x0007B104
	private TinyFsmState StateDailyBattleResult(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_counter = 0;
			if (ServerInterface.LoggedInServerInterface != null)
			{
				if (SingletonGameObject<DailyBattleManager>.Instance != null && !this.m_bossStage)
				{
					SingletonGameObject<DailyBattleManager>.Instance.ResultSetup(new DailyBattleManager.CallbackSetup(this.DailyBattleResultCallBack));
				}
				else
				{
					this.m_counter++;
				}
			}
			else
			{
				this.m_counter++;
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_counter > 0)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateResult)));
			}
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num != 61515)
			{
				return TinyFsmState.End();
			}
			this.m_counter++;
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x0007D020 File Offset: 0x0007B220
	private TinyFsmState StateEventDrawRaidBoss(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_counter = 0;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				long score = 0L;
				StageScoreManager instance = StageScoreManager.Instance;
				if (instance != null)
				{
					score = instance.FinalScore;
				}
				loggedInServerInterface.RequestServerDrawRaidBoss(EventManager.Instance.Id, score, base.gameObject);
			}
			else
			{
				this.m_counter++;
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_counter > 0)
			{
				if (EventUtility.CheckRaidbossEntry())
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateEventGetEventUserRaidBoss)));
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateResult)));
				}
			}
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num != 61511)
			{
				return TinyFsmState.End();
			}
			this.m_counter++;
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x0007D154 File Offset: 0x0007B354
	private TinyFsmState StateEventGetEventUserRaidBoss(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_counter = 0;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetEventUserRaidBossState(EventManager.Instance.Id, base.gameObject);
			}
			else
			{
				this.m_counter++;
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_counter > 0)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateResult)));
			}
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num != 61506)
			{
				return TinyFsmState.End();
			}
			this.m_counter++;
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x0007D244 File Offset: 0x0007B444
	private TinyFsmState StateUpdateRaidBossState(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_counter = 0;
			if (this.m_fromTitle)
			{
				this.m_retired = true;
			}
			if (this.GetEventResultState() == EventResultState.TIMEUP)
			{
				base.StartCoroutine(this.NotSendEventUpdateGameResult());
			}
			else
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					long eventValue = 0L;
					long id = RaidBossInfo.currentRaidData.id;
					ServerEventGameResults serverEventGameResults = new ServerEventGameResults(this.m_retired, EventManager.Instance.Id, eventValue, id);
					if (serverEventGameResults != null)
					{
						DailyMissionData dailyMission = SaveDataManager.Instance.PlayerData.DailyMission;
						if (this.m_missionManager != null)
						{
							serverEventGameResults.m_dailyMissionComplete = this.m_missionManager.Completed;
						}
						else
						{
							serverEventGameResults.m_dailyMissionComplete = false;
						}
						serverEventGameResults.m_dailyMissionValue = dailyMission.progress;
						loggedInServerInterface.RequestServerEventUpdateGameResults(serverEventGameResults, base.gameObject);
					}
				}
				else
				{
					this.m_counter++;
				}
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_counter > 0)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateEventPostGameResult)));
			}
			return TinyFsmState.End();
		case 5:
		{
			int id2 = e.GetMessage.ID;
			int num = id2;
			if (num != 61509)
			{
				return TinyFsmState.End();
			}
			this.m_counter++;
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x0007D3D8 File Offset: 0x0007B5D8
	private TinyFsmState StateEventPostGameResult(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_counter = 0;
			if (this.GetEventResultState() == EventResultState.TIMEUP)
			{
				base.StartCoroutine(this.NotSendEventPostGameResult());
			}
			else
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					int numRaidBossRings = 0;
					StageScoreManager instance = StageScoreManager.Instance;
					if (instance != null)
					{
						numRaidBossRings = (int)instance.FinalCountData.raid_boss_ring;
					}
					loggedInServerInterface.RequestServerEventPostGameResults(EventManager.Instance.Id, numRaidBossRings, base.gameObject);
				}
				else
				{
					this.m_counter++;
				}
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_counter > 0)
			{
				EventUtility.SetRaidBossFirstBattle();
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateResult)));
			}
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num != 61510)
			{
				return TinyFsmState.End();
			}
			this.m_counter++;
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x0007D510 File Offset: 0x0007B710
	private TinyFsmState StateResult(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			EventUtility.UpdateCollectObjectCount();
			if (!this.m_fromTitle && this.m_gameResult != null)
			{
				this.m_gameResult.PlayScoreStart();
				if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
				{
					int raidbossBeatBonus = 0;
					if (this.m_raidBossBonus != null)
					{
						raidbossBeatBonus = this.m_raidBossBonus.BeatBonus;
					}
					this.m_gameResult.SetRaidbossBeatBonus(raidbossBeatBonus);
				}
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_gameResult != null && this.m_gameResult.IsEndOutAnimation)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateSendApolloStageEnd)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x0007D604 File Offset: 0x0007B804
	private TinyFsmState StateSendApolloStageEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_sendApollo != null)
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
			}
			return TinyFsmState.End();
		case 1:
		{
			this.SetTimeScale(1f);
			MsgExitStage msgExitStage = new MsgExitStage();
			GameObjectUtil.SendMessageToTagObjects("StageManager", "OnMsgExitStage", msgExitStage, SendMessageOptions.DontRequireReceiver);
			GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgExitStage", msgExitStage, SendMessageOptions.DontRequireReceiver);
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnMsgExitStage", msgExitStage, SendMessageOptions.DontRequireReceiver);
			if (this.m_hudCaution != null)
			{
				this.m_hudCaution.SetMsgExitStage(msgExitStage);
			}
			this.StopStageEffect();
			ApolloTutorialIndex apolloEndTutorialIndex = this.GetApolloEndTutorialIndex();
			if (apolloEndTutorialIndex != ApolloTutorialIndex.NONE)
			{
				string[] value = new string[1];
				SendApollo.GetTutorialValue(apolloEndTutorialIndex, ref value);
				this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_END, value);
			}
			else
			{
				this.m_sendApollo = null;
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			bool flag = true;
			if (this.m_sendApollo != null && !this.m_sendApollo.IsEnd())
			{
				flag = false;
			}
			if (flag)
			{
				if (this.m_equipItemTutorial && this.m_exitFromResult)
				{
					HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.TUTORIAL_EQIP_ITEM_END);
				}
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateEndFadeOut)));
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x0007D784 File Offset: 0x0007B984
	private TinyFsmState StateEndFadeOut(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			BackKeyManager.EndScene();
			this.SetTimeScale(1f);
			CameraFade.StartAlphaFade(Color.white, false, 1f);
			SoundManager.BgmFadeOut(0.5f);
			this.m_timer = 1f;
			return TinyFsmState.End();
		case 4:
			this.m_timer -= e.GetDeltaTime;
			if (this.m_timer < 0f)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x0007D840 File Offset: 0x0007BA40
	private TinyFsmState StateEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.SetTimeScale(1f);
			this.ResetReplaceAtlas();
			this.RemoveAllResource();
			SoundManager.BgmStop();
			AtlasManager.Instance.ClearAllAtlas();
			GC.Collect();
			Resources.UnloadUnusedAssets();
			GC.Collect();
			if (this.m_fromTitle || this.m_firstTutorial)
			{
				if (this.m_firstTutorial)
				{
					GameModeTitle.FirstTutorialReturned = true;
				}
				Application.LoadLevel(TitleDefine.TitleSceneName);
			}
			else
			{
				this.CreateResultInfo();
				Application.LoadLevel("MainMenu");
			}
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x0007D908 File Offset: 0x0007BB08
	private bool IsRaidBossStateUpdate()
	{
		return !this.m_firstTutorial && (EventManager.Instance != null && EventManager.Instance.Type == EventManager.EventType.RAID_BOSS && EventManager.Instance.IsChallengeEvent() && !EventManager.Instance.IsRaidBossStage() && !EventManager.Instance.IsEncounterRaidBoss());
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x0007D974 File Offset: 0x0007BB74
	private void RequestServerStartAct()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface == null)
		{
			this.m_serverActEnd = true;
			return;
		}
		if (this.m_fromTitle || this.m_tutorialStage || this.m_firstTutorial)
		{
			this.m_serverActEnd = true;
			return;
		}
		List<ItemType> list = new List<ItemType>();
		foreach (ItemType itemType in this.m_useEquippedItem)
		{
			if (itemType != ItemType.UNKNOWN)
			{
				list.Add(itemType);
			}
		}
		this.m_serverActEnd = false;
		List<BoostItemType> list2 = new List<BoostItemType>(this.m_useBoostItem);
		if (this.m_quickMode)
		{
			bool tutorial = this.IsTutorialOnActStart();
			if (this.IsTutorialItem())
			{
				list.Clear();
				list2.Clear();
			}
			loggedInServerInterface.RequestServerQuickModeStartAct(list, list2, tutorial, base.gameObject);
		}
		else
		{
			EventManager instance = EventManager.Instance;
			if (instance != null)
			{
				if (instance.IsRaidBossStage())
				{
					loggedInServerInterface.RequestServerEventStartAct(instance.Id, instance.UseRaidbossChallengeCount, RaidBossInfo.currentRaidData.id, list, list2, base.gameObject);
				}
				else
				{
					List<string> list3 = new List<string>();
					SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
					if (socialInterface != null && socialInterface.IsLoggedIn)
					{
						List<SocialUserData> friendList = socialInterface.FriendList;
						if (friendList != null)
						{
							foreach (SocialUserData socialUserData in friendList)
							{
								string gameId = socialUserData.CustomData.GameId;
								list3.Add(gameId);
							}
						}
					}
					if (this.IsTutorialItem())
					{
						list.Clear();
					}
					bool tutorial2 = this.IsTutorialOnActStart();
					int? eventId = null;
					if (instance.EventStage || instance.IsCollectEvent())
					{
						eventId = new int?(0);
						eventId = new int?(instance.Id);
					}
					loggedInServerInterface.RequestServerStartAct(list, list2, list3, tutorial2, eventId, base.gameObject);
				}
			}
		}
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x0007DBD4 File Offset: 0x0007BDD4
	private void RegisterAllResource()
	{
		if (ResourceManager.Instance != null)
		{
			ResourceManager.Instance.AddCategorySceneObjectsAndSetActive(ResourceCategory.TERRAIN_MODEL, this.m_terrainDataName, GameObject.Find(TerrainXmlData.DataAssetName), false);
			ResourceManager.Instance.AddCategorySceneObjectsAndSetActive(ResourceCategory.STAGE_RESOURCE, this.m_stageResourceName, GameObject.Find(this.m_stageResourceObjectName), false);
			ResourceManager.Instance.RemoveNotActiveContainer(ResourceCategory.TERRAIN_MODEL);
			ResourceManager.Instance.RemoveNotActiveContainer(ResourceCategory.STAGE_RESOURCE);
			if (this.m_playerInformation)
			{
				if (this.m_playerInformation.MainCharacterName != null)
				{
					string text = "CharacterModel" + this.m_playerInformation.MainCharacterName;
					string text2 = "CharacterEffect" + this.m_playerInformation.MainCharacterName;
					ResourceManager.Instance.AddCategorySceneObjectsAndSetActive(ResourceCategory.CHARA_MODEL, text, GameObject.Find(text), true);
					ResourceManager.Instance.AddCategorySceneObjectsAndSetActive(ResourceCategory.CHARA_EFFECT, text2, GameObject.Find(text2), true);
				}
				if (this.m_playerInformation.SubCharacterName != null)
				{
					string text3 = "CharacterModel" + this.m_playerInformation.SubCharacterName;
					string text4 = "CharacterEffect" + this.m_playerInformation.SubCharacterName;
					ResourceManager.Instance.AddCategorySceneObjectsAndSetActive(ResourceCategory.CHARA_MODEL, text3, GameObject.Find(text3), true);
					ResourceManager.Instance.AddCategorySceneObjectsAndSetActive(ResourceCategory.CHARA_EFFECT, text4, GameObject.Find(text4), true);
				}
			}
			ResourceManager.Instance.RemoveNotActiveContainer(ResourceCategory.CHARA_MODEL);
			ResourceManager.Instance.RemoveNotActiveContainer(ResourceCategory.CHARA_EFFECT);
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				int mainChaoID = instance.PlayerData.MainChaoID;
				int subChaoID = instance.PlayerData.SubChaoID;
				if (mainChaoID >= 0)
				{
					ResourceManager.Instance.AddCategorySceneObjects(ResourceCategory.CHAO_MODEL, null, GameObject.Find("ChaoModel" + mainChaoID.ToString("0000")), false);
				}
				if (subChaoID >= 0 && subChaoID != mainChaoID)
				{
					ResourceManager.Instance.AddCategorySceneObjects(ResourceCategory.CHAO_MODEL, null, GameObject.Find("ChaoModel" + subChaoID.ToString("0000")), false);
				}
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			StageAbilityManager.SetupAbilityDataTable();
		}
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x0007DDDC File Offset: 0x0007BFDC
	private void ResetReplaceAtlas()
	{
		if (AtlasManager.Instance != null)
		{
			AtlasManager.Instance.ResetReplaceAtlas();
		}
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x0007DDF8 File Offset: 0x0007BFF8
	private void RemoveAllResource()
	{
		if (ResourceManager.Instance != null)
		{
			ResourceManager.Instance.RemoveResourcesOnThisScene();
			ResourceManager.Instance.SetContainerActive(ResourceCategory.TERRAIN_MODEL, this.m_terrainDataName, false);
			ResourceManager.Instance.SetContainerActive(ResourceCategory.STAGE_RESOURCE, this.m_stageResourceName, false);
			if (this.m_playerInformation.MainCharacterName != null)
			{
				ResourceManager.Instance.SetContainerActive(ResourceCategory.CHARA_MODEL, "CharacterModel" + this.m_playerInformation.MainCharacterName, false);
				ResourceManager.Instance.SetContainerActive(ResourceCategory.CHARA_EFFECT, "CharacterEffect" + this.m_playerInformation.MainCharacterName, false);
			}
			if (this.m_playerInformation.SubCharacterName != null)
			{
				ResourceManager.Instance.SetContainerActive(ResourceCategory.CHARA_MODEL, "CharacterModel" + this.m_playerInformation.SubCharacterName, false);
				ResourceManager.Instance.SetContainerActive(ResourceCategory.CHARA_EFFECT, "CharacterEffect" + this.m_playerInformation.SubCharacterName, false);
			}
		}
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x0007DEF4 File Offset: 0x0007C0F4
	private void CreateStageBlockManager()
	{
		if (this.m_stageBlockManager == null)
		{
			this.m_stageBlockManager = GameObject.Find("StageBlockManager");
			StageBlockManager stageBlockManager;
			if (this.m_stageBlockManager == null)
			{
				this.m_stageBlockManager = new GameObject("StageBlockManager");
				stageBlockManager = this.m_stageBlockManager.AddComponent<StageBlockManager>();
			}
			else
			{
				stageBlockManager = this.m_stageBlockManager.GetComponent<StageBlockManager>();
			}
			if (stageBlockManager != null)
			{
				stageBlockManager.Initialize(new StageBlockManager.CreateInfo
				{
					stageName = this.m_stageName,
					isTerrainManager = this.m_isCreateTerrainPlacementManager,
					isSpawnableManager = this.m_isCreatespawnableManager,
					isPathBlockManager = (this.m_stagePathManager != null),
					pathManager = this.m_stagePathManager,
					showInfo = this.m_showBlockInfo,
					randomBlock = this.m_randomBlock,
					bossMode = this.m_bossStage,
					quickMode = this.m_quickMode
				});
			}
		}
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x0007DFF8 File Offset: 0x0007C1F8
	public RareEnemyTable GetRareEnemyTable()
	{
		return this.m_rareEnemyTable;
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x0007E000 File Offset: 0x0007C200
	public EnemyExtendItemTable GetEnemyExtendItemTable()
	{
		return this.m_enemyExtendItemTable;
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x0007E008 File Offset: 0x0007C208
	public BossTable GetBossTable()
	{
		return this.m_bossTable;
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x0007E010 File Offset: 0x0007C210
	public BossMap3Table GetBossMap3Table()
	{
		return this.m_bossMap3Table;
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x0007E018 File Offset: 0x0007C218
	public ObjectPartTable GetObjectPartTable()
	{
		return this.m_objectPartTable;
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x0007E020 File Offset: 0x0007C220
	public void RetryStreamingDataLoad(int retryCount)
	{
		StageStreamingDataLoadRetryProcess process = new StageStreamingDataLoadRetryProcess(base.gameObject, this);
		NetMonitor.Instance.StartMonitor(process, 0f, HudNetworkConnect.DisplayType.ALL);
		StreamingDataLoader.Instance.StartDownload(retryCount, base.gameObject);
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x0007E05C File Offset: 0x0007C25C
	private void SendMessageToHudCaution(HudCaution.Type hudType)
	{
		if (this.m_hudCaution != null)
		{
			MsgCaution caution = new MsgCaution(hudType);
			this.m_hudCaution.SetCaution(caution);
		}
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x0007E090 File Offset: 0x0007C290
	private void SendBossStartMessageToChao()
	{
		GameObjectUtil.SendMessageToTagObjects("Chao", "OnMsgStartBoss", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x0007E0A4 File Offset: 0x0007C2A4
	private string GetChaoBGMName(int chaoId)
	{
		DataTable.ChaoData chaoData = ChaoTable.GetChaoData(chaoId);
		if (chaoData != null)
		{
			return chaoData.bgmName;
		}
		return string.Empty;
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x0007E0CC File Offset: 0x0007C2CC
	private string GetCueSheetName(int chaoId)
	{
		DataTable.ChaoData chaoData = ChaoTable.GetChaoData(chaoId);
		if (chaoData != null)
		{
			return chaoData.cueSheetName;
		}
		return string.Empty;
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x0007E0F4 File Offset: 0x0007C2F4
	private void SetMainBGMName()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			string chaoBGMName = this.GetChaoBGMName(instance.PlayerData.MainChaoID);
			if (!string.IsNullOrEmpty(chaoBGMName))
			{
				this.m_mainBgmName = chaoBGMName;
				return;
			}
		}
		if (EventManager.Instance != null)
		{
			EventStageData stageData = EventManager.Instance.GetStageData();
			if (stageData != null)
			{
				string text = null;
				if (EventManager.Instance.Type == EventManager.EventType.QUICK)
				{
					if (this.m_quickMode)
					{
						text = stageData.quickStageBGM;
					}
				}
				else if (EventManager.Instance.Type == EventManager.EventType.BGM)
				{
					if (this.m_quickMode && stageData.IsQuickModeBGM())
					{
						text = stageData.quickStageBGM;
					}
					else if (this.m_bossStage && stageData.IsEndlessModeBGM())
					{
						text = stageData.bossStagBGM;
					}
					else if (stageData.IsEndlessModeBGM())
					{
						text = stageData.stageBGM;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.m_mainBgmName = text;
					return;
				}
			}
		}
		if (this.m_quickMode)
		{
			this.m_mainBgmName = StageTypeUtil.GetQuickModeBgmName(this.m_stageName);
			return;
		}
		if (this.m_bossStage)
		{
			this.m_mainBgmName = BossTypeUtil.GetBossBgmCueSheetName(this.m_bossType);
			return;
		}
		if (this.m_tutorialStage)
		{
			this.m_mainBgmName = StageTypeUtil.GetBgmName(StageType.W01);
			return;
		}
		string bgmName = StageTypeUtil.GetBgmName(this.m_stageName);
		if (bgmName != string.Empty)
		{
			this.m_mainBgmName = bgmName;
		}
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x0007E274 File Offset: 0x0007C474
	private string GetCueSheetName()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			string cueSheetName = this.GetCueSheetName(instance.PlayerData.MainChaoID);
			if (!string.IsNullOrEmpty(cueSheetName))
			{
				return cueSheetName;
			}
		}
		if (EventManager.Instance != null)
		{
			EventStageData stageData = EventManager.Instance.GetStageData();
			if (stageData != null)
			{
				string text = null;
				if (EventManager.Instance.Type == EventManager.EventType.QUICK)
				{
					if (this.m_quickMode)
					{
						text = stageData.quickStageCueSheetName;
					}
				}
				else if (EventManager.Instance.Type == EventManager.EventType.BGM)
				{
					if (this.m_quickMode && stageData.IsQuickModeBGM())
					{
						text = stageData.quickStageCueSheetName;
					}
					else if (this.m_bossStage && stageData.IsEndlessModeBGM())
					{
						text = stageData.bossStagCueSheetName;
					}
					else if (stageData.IsEndlessModeBGM())
					{
						text = stageData.stageCueSheetName;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
		}
		if (this.m_quickMode)
		{
			return StageTypeUtil.GetQuickModeCueSheetName(this.m_stageName);
		}
		if (this.m_bossStage)
		{
			return BossTypeUtil.GetBossBgmName(this.m_bossType);
		}
		if (this.m_tutorialStage)
		{
			return StageTypeUtil.GetCueSheetName(StageType.W01);
		}
		return StageTypeUtil.GetCueSheetName(this.m_stageName);
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x0007E3BC File Offset: 0x0007C5BC
	private void CreateResultInfo()
	{
		if (this.m_tutorialStage)
		{
			ResultInfo.CreateOptionTutorialResultInfo();
		}
		else
		{
			ResultInfo resultInfo = ResultInfo.CreateResultInfo();
			if (resultInfo != null)
			{
				ResultData resultData = new ResultData();
				if (resultData != null)
				{
					resultData.m_stageName = this.m_stageName;
					resultData.m_validResult = this.m_exitFromResult;
					resultData.m_fromOptionTutorial = this.m_tutorialStage;
					resultData.m_bossStage = this.m_bossStage;
					resultData.m_bossDestroy = (this.m_levelInformation != null && this.m_levelInformation.BossDestroy);
					resultData.m_eventStage = this.m_eventStage;
					resultData.m_quickMode = this.m_quickMode;
					bool flag = false;
					if (this.m_missionManager != null)
					{
						flag = this.m_missionManager.Completed;
					}
					resultData.m_missionComplete = (!this.m_missonCompleted && flag);
					if (this.m_resultMapState != null)
					{
						resultData.m_newMapState = new MileageMapState(this.m_resultMapState);
					}
					if (this.m_postGameResults.m_prevMapInfo != null && this.m_postGameResults.m_prevMapInfo.m_mapState != null)
					{
						resultData.m_oldMapState = new MileageMapState(this.m_postGameResults.m_prevMapInfo.m_mapState);
					}
					if (this.m_mileageIncentive != null)
					{
						resultData.m_mileageIncentiveList = new List<ServerMileageIncentive>(this.m_mileageIncentive.Count);
						foreach (ServerMileageIncentive item in this.m_mileageIncentive)
						{
							resultData.m_mileageIncentiveList.Add(item);
						}
					}
					if (!this.m_eventStage)
					{
						StageScoreManager instance = StageScoreManager.Instance;
						if (instance != null)
						{
							if (resultData.m_validResult)
							{
								RankingUtil.RankingMode rankingMode = RankingUtil.RankingMode.ENDLESS;
								if (this.m_quickMode)
								{
									rankingMode = RankingUtil.RankingMode.QUICK;
								}
								long finalScore = instance.FinalScore;
								long num = 0L;
								long num2 = 0L;
								int num3 = 0;
								bool flag2 = false;
								RankingManager.GetCurrentHighScoreRank(rankingMode, false, ref finalScore, out flag2, out num, out num2, out num3);
								resultData.m_rivalHighScore = flag2;
								if (flag2)
								{
									resultData.m_highScore = finalScore;
								}
								resultData.m_totalScore = finalScore;
							}
							else
							{
								resultData.m_rivalHighScore = false;
								resultData.m_highScore = 0L;
								resultData.m_totalScore = 0L;
							}
						}
					}
					if (this.m_dailyIncentive != null)
					{
						resultData.m_dailyMissionIncentiveList = new List<ServerItemState>(this.m_dailyIncentive.Count);
						foreach (ServerItemState item2 in this.m_dailyIncentive)
						{
							resultData.m_dailyMissionIncentiveList.Add(item2);
						}
					}
					resultInfo.SetInfo(resultData);
				}
			}
		}
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x0007E6A4 File Offset: 0x0007C8A4
	private void EnablePause(bool value)
	{
		MsgEnablePause value2 = new MsgEnablePause(value);
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnEnablePause", value2, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x0007E6CC File Offset: 0x0007C8CC
	private void HoldPlayerAndDestroyTerrainOnEnd()
	{
		MsgPLHold value = new MsgPLHold();
		GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgPLHold", value, SendMessageOptions.DontRequireReceiver);
		if (this.m_stageBlockManager != null)
		{
			StageBlockManager component = this.m_stageBlockManager.GetComponent<StageBlockManager>();
			if (component != null)
			{
				component.DeactivateAll();
			}
		}
		GameObjectUtil.SendDelayedMessageFindGameObject("HudCockpit", "OnMsgExitStage", new MsgExitStage());
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x0007E734 File Offset: 0x0007C934
	private bool IsBossTutorialLose()
	{
		bool flag = this.m_levelInformation != null && this.m_levelInformation.BossDestroy;
		if (this.m_bossStage && !flag && this.m_postGameResults.m_prevMapInfo != null)
		{
			MileageMapState mapState = this.m_postGameResults.m_prevMapInfo.m_mapState;
			if (mapState != null && mapState.m_episode == 1 && mapState.m_chapter == 1)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x0007E7B8 File Offset: 0x0007C9B8
	private bool IsBossTutorialPresent()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				return systemdata.IsFlagStatus(SystemData.FlagStatus.TUTORIAL_BOSS_PRESENT);
			}
		}
		return false;
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x0007E7F0 File Offset: 0x0007C9F0
	private void SetBossTutorialPresent()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null && !systemdata.IsFlagStatus(SystemData.FlagStatus.TUTORIAL_BOSS_PRESENT))
			{
				systemdata.SetFlagStatus(SystemData.FlagStatus.TUTORIAL_BOSS_PRESENT, true);
				instance.SaveSystemData();
			}
		}
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x0007E838 File Offset: 0x0007CA38
	private bool IsBossTutorialClear()
	{
		bool flag = this.m_levelInformation != null && this.m_levelInformation.BossDestroy;
		if (this.m_bossStage && flag && this.m_postGameResults.m_prevMapInfo != null)
		{
			MileageMapState mapState = this.m_postGameResults.m_prevMapInfo.m_mapState;
			if (mapState != null && mapState.m_episode == 1 && mapState.m_chapter == 1)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x0007E8BC File Offset: 0x0007CABC
	private bool IsTutorialOnActStart()
	{
		return this.m_tutorialStage || this.m_firstTutorial || this.m_equipItemTutorial;
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x0007E8F4 File Offset: 0x0007CAF4
	private bool IsTutorialItem()
	{
		return this.m_tutorialStage || this.m_firstTutorial || this.m_equipItemTutorial;
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x0007E92C File Offset: 0x0007CB2C
	private bool EnableChaoEgg()
	{
		if (this.IsBossTutorialClear())
		{
			return true;
		}
		bool flag = false;
		if (this.m_levelInformation != null)
		{
			flag = this.m_levelInformation.BossDestroy;
		}
		return this.m_bossStage && this.m_bossNoMissChance && flag;
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x0007E984 File Offset: 0x0007CB84
	private bool IsEnableContinue()
	{
		return !this.m_firstTutorial && (!this.m_bossStage || !this.m_bossTimeUp) && this.m_numEnableContinue > 0;
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x0007E9C8 File Offset: 0x0007CBC8
	private ApolloTutorialIndex GetApolloStartTutorialIndex()
	{
		if (this.m_firstTutorial)
		{
			return ApolloTutorialIndex.START_STEP1;
		}
		if (this.m_equipItemTutorial)
		{
			return ApolloTutorialIndex.START_STEP5;
		}
		return ApolloTutorialIndex.NONE;
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x0007E9E8 File Offset: 0x0007CBE8
	private ApolloTutorialIndex GetApolloEndTutorialIndex()
	{
		if (this.m_firstTutorial)
		{
			return ApolloTutorialIndex.START_STEP1;
		}
		if (this.m_exitFromResult && this.m_equipItemTutorial)
		{
			return ApolloTutorialIndex.START_STEP5;
		}
		return ApolloTutorialIndex.NONE;
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x0007EA1C File Offset: 0x0007CC1C
	private void OnApplicationPause(bool flag)
	{
		if (flag)
		{
			this.OnMsgExternalGamePause(new MsgExternalGamePause(false, false));
		}
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x0007EA34 File Offset: 0x0007CC34
	private void OnGetMileageMapState(MsgGetMileageMapState msg)
	{
		if (this.m_postGameResults.m_prevMapInfo != null)
		{
			msg.m_mileageMapState = this.m_postGameResults.m_prevMapInfo.m_mapState;
		}
		else
		{
			msg.m_mileageMapState = null;
		}
		msg.m_debugLevel = (uint)this.m_debugBossLevel;
		msg.m_succeed = true;
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x0007EA88 File Offset: 0x0007CC88
	private void SetMissionResult()
	{
		if (this.m_missionManager != null)
		{
			if (!this.m_bossStage)
			{
				StageScoreManager instance = StageScoreManager.Instance;
				long distance = instance.FinalCountData.distance;
				MsgMissionEvent msg = new MsgMissionEvent(Mission.EventID.TOTALDISTANCE, distance);
				ObjUtil.SendMessageMission2(msg);
				long finalScore = instance.FinalScore;
				MsgMissionEvent msg2 = new MsgMissionEvent(Mission.EventID.GET_SCORE, finalScore);
				ObjUtil.SendMessageMission2(msg2);
				long ring = instance.FinalCountData.ring;
				MsgMissionEvent msg3 = new MsgMissionEvent(Mission.EventID.GET_RING, ring);
				ObjUtil.SendMessageMission2(msg3);
				long animal = instance.FinalCountData.animal;
				MsgMissionEvent msg4 = new MsgMissionEvent(Mission.EventID.GET_ANIMALS, animal);
				ObjUtil.SendMessageMission2(msg4);
			}
			this.m_missionManager.SaveMissions();
		}
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x0007EB30 File Offset: 0x0007CD30
	private bool IsEventTimeup()
	{
		if (this.m_eventStage && this.m_eventManager != null && (this.m_eventManager.IsSpecialStage() || this.m_eventManager.IsRaidBossStage()) && !this.m_eventManager.IsPlayEventForStage())
		{
			global::Debug.Log("*****Event Timeup!!!!!*****");
			return true;
		}
		return false;
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x0007EB98 File Offset: 0x0007CD98
	private EventResultState GetEventResultState()
	{
		if (this.m_eventStage && this.m_eventManager != null)
		{
			if (!this.m_eventManager.IsResultEvent())
			{
				return EventResultState.TIMEUP;
			}
			if (!this.m_eventManager.IsPlayEventForStage())
			{
				return EventResultState.TIMEUP_RESULT;
			}
		}
		return EventResultState.NONE;
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x0007EBE8 File Offset: 0x0007CDE8
	private void PlayStageEffect()
	{
		if (this.m_stageEffect == null)
		{
			this.m_stageEffect = StageEffect.CreateStageEffect(this.m_stageName);
		}
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x0007EC18 File Offset: 0x0007CE18
	private void StopStageEffect()
	{
		if (this.m_stageEffect != null)
		{
			UnityEngine.Object.Destroy(this.m_stageEffect.gameObject);
			this.m_stageEffect = null;
		}
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x0007EC50 File Offset: 0x0007CE50
	private void ResetPosStageEffect(bool reset)
	{
		if (this.m_stageEffect != null)
		{
			this.m_stageEffect.ResetPos(reset);
		}
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x0007EC70 File Offset: 0x0007CE70
	private void HudPlayerChangeCharaButton(bool val, bool pause)
	{
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnChangeCharaButton", new MsgChangeCharaButton(val, pause), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x0007EC8C File Offset: 0x0007CE8C
	private void SetChaoAblityTimeScale()
	{
		if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.EASY_SPEED))
		{
			int num = (int)StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.EASY_SPEED);
			int num2 = UnityEngine.Random.Range(0, 100);
			if (num >= num2)
			{
				float chaoAbilityExtraValue = StageAbilityManager.Instance.GetChaoAbilityExtraValue(ChaoAbility.EASY_SPEED);
				this.m_chaoEasyTimeScale = this.m_defaultTimeScale * (1f - chaoAbilityExtraValue * 0.01f);
			}
		}
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x0007ED00 File Offset: 0x0007CF00
	private void SetDefaultTimeScale()
	{
		float num = this.m_defaultTimeScale;
		if (StageAbilityManager.Instance != null)
		{
			num = StageAbilityManager.Instance.GetTeamAbliltyTimeScale(num);
		}
		if (this.m_chaoEasyTimeScale < num)
		{
			num = this.m_chaoEasyTimeScale;
		}
		this.SetTimeScale(num);
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x0007ED4C File Offset: 0x0007CF4C
	private void SetTimeScale(float timeScale)
	{
		Time.timeScale = timeScale;
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x0007ED54 File Offset: 0x0007CF54
	private void DrawingInvalidExtreme()
	{
		if (this.m_levelInformation != null && this.m_levelInformation.Extreme)
		{
			StageAbilityManager instance = StageAbilityManager.Instance;
			if (instance != null && instance.HasChaoAbility(ChaoAbility.INVALIDI_EXTREME_STAGE))
			{
				int num = (int)instance.GetChaoAbilityExtraValue(ChaoAbility.INVALIDI_EXTREME_STAGE);
				if (this.m_invalidExtremeCount < num)
				{
					float chaoAbilityValue = instance.GetChaoAbilityValue(ChaoAbility.INVALIDI_EXTREME_STAGE);
					float num2 = UnityEngine.Random.Range(0f, 99.9f);
					if (chaoAbilityValue >= num2)
					{
						this.m_levelInformation.InvalidExtreme = true;
						this.m_invalidExtremeCount++;
						this.m_invalidExtremeFlag = true;
					}
				}
			}
		}
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x0007EDF8 File Offset: 0x0007CFF8
	private void StreamingDataLoad_Succeed()
	{
		NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseSucceed(null, null), null, null);
		NetMonitor.Instance.EndMonitorBackward();
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x0007EE18 File Offset: 0x0007D018
	private void StreamingDataLoad_Failed()
	{
		NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseFailedMonitor(), null, null);
		NetMonitor.Instance.EndMonitorBackward();
	}

	// Token: 0x04001328 RID: 4904
	private const float LightModeFixedTimeStep = 0.033333f;

	// Token: 0x04001329 RID: 4905
	private const float LightModeMaxFixedTimeStep = 0.333333f;

	// Token: 0x0400132A RID: 4906
	private const string pre_characterModelResourceName = "CharacterModel";

	// Token: 0x0400132B RID: 4907
	private const string pre_characterEffectName = "CharacterEffect";

	// Token: 0x0400132C RID: 4908
	private const string PathManagerName = "StagePathManager";

	// Token: 0x0400132D RID: 4909
	private bool m_isLoadResources = true;

	// Token: 0x0400132E RID: 4910
	public bool m_isCreatespawnableManager;

	// Token: 0x0400132F RID: 4911
	public bool m_isCreateTerrainPlacementManager;

	// Token: 0x04001330 RID: 4912
	public bool m_notPlaceTerrain;

	// Token: 0x04001331 RID: 4913
	public bool m_showBlockInfo;

	// Token: 0x04001332 RID: 4914
	public bool m_randomBlock;

	// Token: 0x04001333 RID: 4915
	public bool m_useTemporarySet;

	// Token: 0x04001334 RID: 4916
	public int m_blockNumOfNotPlaceTerrain = -1;

	// Token: 0x04001335 RID: 4917
	public bool m_bossStage;

	// Token: 0x04001336 RID: 4918
	public int m_debugBossLevel;

	// Token: 0x04001337 RID: 4919
	public BossType m_bossType = BossType.MAP1;

	// Token: 0x04001338 RID: 4920
	public bool m_useCharaInInspector;

	// Token: 0x04001339 RID: 4921
	public bool m_noStartHud;

	// Token: 0x0400133A RID: 4922
	private bool m_exitFromResult;

	// Token: 0x0400133B RID: 4923
	private bool m_bossClear;

	// Token: 0x0400133C RID: 4924
	private bool m_bossTimeUp;

	// Token: 0x0400133D RID: 4925
	private bool m_quickModeTimeUp;

	// Token: 0x0400133E RID: 4926
	[SerializeField]
	private bool m_tutorialStage;

	// Token: 0x0400133F RID: 4927
	[SerializeField]
	private bool m_eventStage;

	// Token: 0x04001340 RID: 4928
	private bool m_showMapBossTutorial;

	// Token: 0x04001341 RID: 4929
	private bool m_showFeverBossTutorial;

	// Token: 0x04001342 RID: 4930
	private bool m_showEventBossTutorial;

	// Token: 0x04001343 RID: 4931
	private int m_showItemTutorial = -1;

	// Token: 0x04001344 RID: 4932
	private int m_showCharaTutorial = -1;

	// Token: 0x04001345 RID: 4933
	private int m_showActionTutorial = -1;

	// Token: 0x04001346 RID: 4934
	private int m_showQuickTurorial = -1;

	// Token: 0x04001347 RID: 4935
	private bool m_fromTitle;

	// Token: 0x04001348 RID: 4936
	private bool m_serverActEnd;

	// Token: 0x04001349 RID: 4937
	private bool m_bossNoMissChance;

	// Token: 0x0400134A RID: 4938
	private bool m_saveFlag;

	// Token: 0x0400134B RID: 4939
	private bool m_firstTutorial;

	// Token: 0x0400134C RID: 4940
	private bool m_equipItemTutorial;

	// Token: 0x0400134D RID: 4941
	private bool m_missonCompleted;

	// Token: 0x0400134E RID: 4942
	private int m_oldNumBossAttack;

	// Token: 0x0400134F RID: 4943
	private bool m_retired;

	// Token: 0x04001350 RID: 4944
	private TinyFsmBehavior m_fsm;

	// Token: 0x04001351 RID: 4945
	[SerializeField]
	private string m_stageName = "w01";

	// Token: 0x04001352 RID: 4946
	[SerializeField]
	private TenseType m_stageTenseType = TenseType.NONE;

	// Token: 0x04001353 RID: 4947
	[SerializeField]
	private CharaType m_mainChara;

	// Token: 0x04001354 RID: 4948
	[SerializeField]
	private CharaType m_subChara = CharaType.UNKNOWN;

	// Token: 0x04001355 RID: 4949
	private int m_substate;

	// Token: 0x04001356 RID: 4950
	private List<ItemType> m_useEquippedItem = new List<ItemType>();

	// Token: 0x04001357 RID: 4951
	private List<BoostItemType> m_useBoostItem = new List<BoostItemType>();

	// Token: 0x04001358 RID: 4952
	private GameObject m_sceneLoader;

	// Token: 0x04001359 RID: 4953
	private GameObject m_stageBlockManager;

	// Token: 0x0400135A RID: 4954
	private PathManager m_stagePathManager;

	// Token: 0x0400135B RID: 4955
	private List<GameObject> m_pausedObject;

	// Token: 0x0400135C RID: 4956
	private PlayerInformation m_playerInformation;

	// Token: 0x0400135D RID: 4957
	private LevelInformation m_levelInformation;

	// Token: 0x0400135E RID: 4958
	private HudCaution m_hudCaution;

	// Token: 0x0400135F RID: 4959
	private CharacterContainer m_characterContainer;

	// Token: 0x04001360 RID: 4960
	private StageMissionManager m_missionManager;

	// Token: 0x04001361 RID: 4961
	private StageTutorialManager m_tutorialManager;

	// Token: 0x04001362 RID: 4962
	private FriendSignManager m_friendSignManager;

	// Token: 0x04001363 RID: 4963
	private EventManager m_eventManager;

	// Token: 0x04001364 RID: 4964
	private string m_terrainDataName;

	// Token: 0x04001365 RID: 4965
	private string m_stageResourceName;

	// Token: 0x04001366 RID: 4966
	private string m_stageResourceObjectName;

	// Token: 0x04001367 RID: 4967
	private bool m_onSpeedUp;

	// Token: 0x04001368 RID: 4968
	private bool m_onDestroyRingMode;

	// Token: 0x04001369 RID: 4969
	[SerializeField]
	private int m_numEnableContinue = 2;

	// Token: 0x0400136A RID: 4970
	private int m_invalidExtremeCount;

	// Token: 0x0400136B RID: 4971
	private bool m_invalidExtremeFlag;

	// Token: 0x0400136C RID: 4972
	private float m_timer;

	// Token: 0x0400136D RID: 4973
	private int m_counter;

	// Token: 0x0400136E RID: 4974
	private bool m_reqPause;

	// Token: 0x0400136F RID: 4975
	private bool m_reqPauseBackMain;

	// Token: 0x04001370 RID: 4976
	private bool m_reqTutorialPause;

	// Token: 0x04001371 RID: 4977
	private bool m_IsNowLastChanceHudCautionBoss;

	// Token: 0x04001372 RID: 4978
	private bool m_receiveInvincibleMsg;

	// Token: 0x04001373 RID: 4979
	[SerializeField]
	private float m_defaultTimeScale = 1f;

	// Token: 0x04001374 RID: 4980
	private float m_chaoEasyTimeScale = 1f;

	// Token: 0x04001375 RID: 4981
	private float m_gameResultTimer;

	// Token: 0x04001376 RID: 4982
	private GameResult m_gameResult;

	// Token: 0x04001377 RID: 4983
	private MsgTutorialPlayEnd m_tutorialEndMsg;

	// Token: 0x04001378 RID: 4984
	private Tutorial.EventID m_tutorialMissionID;

	// Token: 0x04001379 RID: 4985
	private HudTutorial.Kind m_tutorialKind;

	// Token: 0x0400137A RID: 4986
	private string m_mainBgmName = "bgm_z_w01";

	// Token: 0x0400137B RID: 4987
	private RareEnemyTable m_rareEnemyTable;

	// Token: 0x0400137C RID: 4988
	private EnemyExtendItemTable m_enemyExtendItemTable;

	// Token: 0x0400137D RID: 4989
	private BossTable m_bossTable;

	// Token: 0x0400137E RID: 4990
	private BossMap3Table m_bossMap3Table;

	// Token: 0x0400137F RID: 4991
	private ObjectPartTable m_objectPartTable;

	// Token: 0x04001380 RID: 4992
	private float m_savedFixedTimeStep;

	// Token: 0x04001381 RID: 4993
	private float m_savedMaxFixedTimeStep;

	// Token: 0x04001382 RID: 4994
	private GameModeStage.PostGameResultInfo m_postGameResults = default(GameModeStage.PostGameResultInfo);

	// Token: 0x04001383 RID: 4995
	private ServerMileageMapState m_resultMapState;

	// Token: 0x04001384 RID: 4996
	private List<ServerMileageIncentive> m_mileageIncentive;

	// Token: 0x04001385 RID: 4997
	private List<ServerItemState> m_dailyIncentive;

	// Token: 0x04001386 RID: 4998
	private SendApollo m_sendApollo;

	// Token: 0x04001387 RID: 4999
	private StageEffect m_stageEffect;

	// Token: 0x04001388 RID: 5000
	private List<ResourceSceneLoader.ResourceInfo> m_loadInfo = new List<ResourceSceneLoader.ResourceInfo>
	{
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.OBJECT_RESOURCE, "CommonObjectResource", true, false, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.OBJECT_PREFAB, "CommonObjectPrefabs", false, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ENEMY_RESOURCE, "CommonEnemyResource", true, false, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ENEMY_PREFAB, "CommonEnemyPrefabs", false, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.COMMON_EFFECT, "ResourcesCommonEffect", true, false, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.PLAYER_COMMON, "CharacterCommonResource", true, false, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.EVENT_RESOURCE, "EventResourceStage", true, false, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.EVENT_RESOURCE, "EventResourceCommon", true, false, false, null, false)
	};

	// Token: 0x04001389 RID: 5001
	private List<ResourceSceneLoader.ResourceInfo> m_quickModeLoadInfo = new List<ResourceSceneLoader.ResourceInfo>
	{
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.QUICK_MODE, "StageTimeTable", true, false, true, null, false)
	};

	// Token: 0x0400138A RID: 5002
	private GameObject m_uiRootObj;

	// Token: 0x0400138B RID: 5003
	private GameObject m_continueWindowObj;

	// Token: 0x0400138C RID: 5004
	private ServerEventRaidBossBonus m_raidBossBonus;

	// Token: 0x0400138D RID: 5005
	private HudProgressBar m_progressBar;

	// Token: 0x0400138E RID: 5006
	private GameObject m_connectAlertUI2;

	// Token: 0x0400138F RID: 5007
	private bool m_quickMode;

	// Token: 0x04001390 RID: 5008
	private readonly Vector3 PlayerResetPosition = Vector3.zero;

	// Token: 0x04001391 RID: 5009
	private readonly Quaternion PlayerResetRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));

	// Token: 0x020002F3 RID: 755
	private struct PostGameResultInfo
	{
		// Token: 0x04001392 RID: 5010
		public bool m_existBoss;

		// Token: 0x04001393 RID: 5011
		public StageInfo.MileageMapInfo m_prevMapInfo;
	}

	// Token: 0x020002F4 RID: 756
	private enum LoadType
	{
		// Token: 0x04001395 RID: 5013
		COMMON_OBJECT_RESOURCE,
		// Token: 0x04001396 RID: 5014
		COMMON_OBJECT_PREFABS,
		// Token: 0x04001397 RID: 5015
		COMMON_ENEMY_RESOURCE,
		// Token: 0x04001398 RID: 5016
		COMMON_ENEMY_PREFABS,
		// Token: 0x04001399 RID: 5017
		RESOURCES_COMMON_EFFECT,
		// Token: 0x0400139A RID: 5018
		CHARACTER_COMMON_RESOURCE,
		// Token: 0x0400139B RID: 5019
		EVENT_RESOURCE_STAGE,
		// Token: 0x0400139C RID: 5020
		EVENT_RESOURCE_COMMON,
		// Token: 0x0400139D RID: 5021
		NUM
	}

	// Token: 0x020002F5 RID: 757
	public enum ProgressBarLeaveState
	{
		// Token: 0x0400139F RID: 5023
		IDLE = -1,
		// Token: 0x040013A0 RID: 5024
		StateInit,
		// Token: 0x040013A1 RID: 5025
		StateLoad,
		// Token: 0x040013A2 RID: 5026
		StateLoad2,
		// Token: 0x040013A3 RID: 5027
		StateRequestStartAct,
		// Token: 0x040013A4 RID: 5028
		StateSoundConnectIfNotFound,
		// Token: 0x040013A5 RID: 5029
		StateAccessNetworkForStartAct,
		// Token: 0x040013A6 RID: 5030
		StateSetupPrepareBlock,
		// Token: 0x040013A7 RID: 5031
		StateSetupBlock,
		// Token: 0x040013A8 RID: 5032
		StateSendApolloStageStart,
		// Token: 0x040013A9 RID: 5033
		NUM
	}

	// Token: 0x020002F6 RID: 758
	private enum ChangeLevelSubState
	{
		// Token: 0x040013AB RID: 5035
		FADEOUT,
		// Token: 0x040013AC RID: 5036
		FADEOUT_STOPCHARACTER,
		// Token: 0x040013AD RID: 5037
		SETUP_SPEEDLEVEL,
		// Token: 0x040013AE RID: 5038
		WAITPREPARE_STAGE,
		// Token: 0x040013AF RID: 5039
		SETUP_STAGE,
		// Token: 0x040013B0 RID: 5040
		WAIT,
		// Token: 0x040013B1 RID: 5041
		FADEIN
	}

	// Token: 0x020002F7 RID: 759
	private enum TutorialMissionEndSubState
	{
		// Token: 0x040013B3 RID: 5043
		SHOWRESULT,
		// Token: 0x040013B4 RID: 5044
		FADEOUT,
		// Token: 0x040013B5 RID: 5045
		WAIT,
		// Token: 0x040013B6 RID: 5046
		FADEIN,
		// Token: 0x040013B7 RID: 5047
		END
	}
}
