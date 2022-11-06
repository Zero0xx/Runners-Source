using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x0200041D RID: 1053
public class ItemSetMenu : MonoBehaviour
{
	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x000BC6E8 File Offset: 0x000BA8E8
	public bool IsEndSetup
	{
		get
		{
			return this.m_isEndSetup;
		}
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x000BC6F0 File Offset: 0x000BA8F0
	private void Start()
	{
		base.StartCoroutine(this.OnStart());
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x000BC700 File Offset: 0x000BA900
	private void OnEnable()
	{
		DeckViewWindow.Reset();
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_7_BL");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "Btn_charaset");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(true);
				UIButtonMessage uibuttonMessage = gameObject2.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = gameObject2.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "CharaSetButtonClickedCallback";
				GeneralUtil.SetCharasetBtnIcon(base.gameObject, "Btn_charaset");
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "Btn_cmn_back");
			if (gameObject3 != null)
			{
				gameObject3.SetActive(true);
			}
		}
		bool flag = false;
		if (StageModeManager.Instance != null)
		{
			flag = StageModeManager.Instance.IsQuickMode();
		}
		ItemSetMenu.ButtonType buttonType = ItemSetMenu.ButtonType.NORMAL_STAGE;
		this.m_isRaidMenu = false;
		this.m_isRaidbossTimeOver = false;
		if (!flag)
		{
			if (EventManager.Instance.IsSpecialStage())
			{
				buttonType = ItemSetMenu.ButtonType.SP_STAGE;
			}
			else if (EventManager.Instance.IsRaidBossStage())
			{
				buttonType = ItemSetMenu.ButtonType.RAID_BOSS_ATTACK;
				this.m_currentRaidBossData = RaidBossInfo.currentRaidData;
				this.m_isRaidMenu = true;
				this.m_timeCounter = 0.25f;
				this.m_targetFrameTime = 1f / (float)Application.targetFrameRate;
			}
		}
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_9_BR");
		if (gameObject4 != null)
		{
			for (int i = 0; i < 4; i++)
			{
				string name = "pattern_" + i.ToString();
				GameObject gameObject5 = GameObjectUtil.FindChildGameObject(gameObject4, name);
				if (!(gameObject5 == null))
				{
					gameObject5.SetActive(false);
				}
			}
			string str = "pattern_";
			int num = (int)buttonType;
			string name2 = str + num.ToString();
			GameObject gameObject6 = GameObjectUtil.FindChildGameObject(gameObject4, name2);
			if (gameObject6 != null)
			{
				gameObject6.SetActive(true);
				if (EventManager.Instance.EventStage)
				{
					UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject6, "img_stage_tex");
					if (uitexture != null)
					{
						uitexture.mainTexture = EventUtility.GetBGTexture();
						BoxCollider component = uitexture.gameObject.GetComponent<BoxCollider>();
						if (component != null)
						{
							component.enabled = false;
						}
					}
				}
				else if (flag && EventManager.Instance.Type == EventManager.EventType.QUICK)
				{
					int num2 = 1;
					if (StageModeManager.Instance != null)
					{
						num2 = StageModeManager.Instance.QuickStageIndex;
					}
					UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject6, "img_next_map_cache");
					if (uisprite != null)
					{
						uisprite.gameObject.SetActive(true);
						uisprite.spriteName = "ui_mm_map_thumb_w" + num2.ToString("D2") + "a";
					}
					GameObject gameObject7 = GameObjectUtil.FindChildGameObject(gameObject6, "img_next_map");
					if (gameObject7 != null && gameObject7.activeSelf)
					{
						gameObject7.SetActive(false);
					}
					for (int j = 0; j < 3; j++)
					{
						string name3 = "img_icon_type_" + (j + 1).ToString();
						UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject6, name3);
						if (uisprite2 != null)
						{
							uisprite2.enabled = true;
							uisprite2.gameObject.SetActive(true);
							if (j == 0)
							{
								uisprite2.spriteName = "ui_chao_set_type_icon_power";
							}
							else if (j == 1)
							{
								uisprite2.spriteName = "ui_chao_set_type_icon_fly";
							}
							else if (j == 2)
							{
								uisprite2.spriteName = "ui_chao_set_type_icon_speed";
							}
						}
					}
				}
				else if (buttonType == ItemSetMenu.ButtonType.NORMAL_STAGE)
				{
					GameObject gameObject8 = GameObjectUtil.FindChildGameObject(gameObject6, "img_next_map_cache");
					if (gameObject8 != null && gameObject8.activeSelf)
					{
						gameObject8.SetActive(false);
					}
					int stageIndex = 1;
					if (MileageMapDataManager.Instance != null && StageModeManager.Instance != null)
					{
						if (!flag)
						{
							stageIndex = MileageMapDataManager.Instance.MileageStageIndex;
						}
						else
						{
							stageIndex = StageModeManager.Instance.QuickStageIndex;
						}
					}
					UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject6, "img_next_map");
					if (uisprite3 != null)
					{
						uisprite3.gameObject.SetActive(true);
						uisprite3.spriteName = "ui_mm_map_thumb_w" + stageIndex.ToString("00") + "a";
					}
					CharacterAttribute[] characterAttribute = MileageMapUtility.GetCharacterAttribute(stageIndex);
					if (characterAttribute != null)
					{
						for (int k = 0; k < 3; k++)
						{
							string name4 = "img_icon_type_" + (k + 1).ToString();
							UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject6, name4);
							if (uisprite4 != null)
							{
								if (k < characterAttribute.Length)
								{
									switch (characterAttribute[k])
									{
									case CharacterAttribute.SPEED:
										uisprite4.enabled = true;
										uisprite4.gameObject.SetActive(true);
										uisprite4.spriteName = "ui_chao_set_type_icon_speed";
										break;
									case CharacterAttribute.FLY:
										uisprite4.enabled = true;
										uisprite4.gameObject.SetActive(true);
										uisprite4.spriteName = "ui_chao_set_type_icon_fly";
										break;
									case CharacterAttribute.POWER:
										uisprite4.enabled = true;
										uisprite4.gameObject.SetActive(true);
										uisprite4.spriteName = "ui_chao_set_type_icon_power";
										break;
									default:
										uisprite4.gameObject.SetActive(false);
										uisprite4.enabled = false;
										break;
									}
								}
								else
								{
									uisprite4.gameObject.SetActive(false);
									uisprite4.enabled = false;
								}
							}
						}
					}
				}
			}
			if (this.m_isRaidMenu)
			{
				string str2 = "pattern_";
				int num3 = (int)buttonType;
				string name5 = str2 + num3.ToString();
				GameObject parent = GameObjectUtil.FindChildGameObject(gameObject4, name5);
				this.m_raidChargeCount = 1U;
				this.m_raidChargeCountMax = 1U;
				this.m_ChallengeCountLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_EVchallenge");
				this.m_ChargeText = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_bet_number");
				this.m_AttackRateText = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_strike_power");
				this.m_playBetObject0 = GameObjectUtil.FindChildGameObject(parent, "bet_0");
				if (this.m_playBetObject0 != null)
				{
					this.m_playBetObject0.SetActive(true);
				}
				this.m_playBetObject1 = GameObjectUtil.FindChildGameObject(parent, "bet_1");
				if (this.m_playBetObject1 != null)
				{
					this.m_playBetObject1.SetActive(false);
				}
				this.m_playBetMaxSign = GameObjectUtil.FindChildGameObject(parent, "max_sign");
				if (this.m_playBetMaxSign != null)
				{
					this.m_playBetMaxSign.SetActive(false);
				}
				this.UpdateRaidbossChallangeCountView();
				if (this.m_currentRaidBossData != null)
				{
					UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_boss_name");
					if (uilabel != null)
					{
						uilabel.text = this.m_currentRaidBossData.name;
					}
					UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_boss_name_sh");
					if (uilabel2 != null)
					{
						uilabel2.text = this.m_currentRaidBossData.name;
					}
					UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_boss_lv");
					if (uilabel3 != null)
					{
						uilabel3.text = "Lv." + this.m_currentRaidBossData.lv.ToString();
					}
					GameObject gameObject9 = GameObjectUtil.FindChildGameObject(parent, "img_boss_icon");
					if (gameObject9 != null)
					{
						UISprite component2 = gameObject9.GetComponent<UISprite>();
						if (component2 != null)
						{
							component2.spriteName = "ui_gp_gauge_boss_icon_raid_" + this.m_currentRaidBossData.rarity;
						}
						UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject9, "img_boss_icon_bg");
						if (uisprite5 != null)
						{
							uisprite5.spriteName = "ui_event_raidboss_window_bosslevel_" + this.m_currentRaidBossData.rarity;
						}
					}
					UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_boss_life");
					if (uilabel4 != null)
					{
						uilabel4.text = this.m_currentRaidBossData.hp.ToString() + "/" + this.m_currentRaidBossData.hpMax.ToString();
					}
					this.m_bossTime = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_boss_time");
					if (this.m_bossTime != null)
					{
						this.m_bossTime.text = this.m_currentRaidBossData.GetTimeLimitString(true);
					}
					this.m_bossLifeBar = GameObjectUtil.FindChildGameObjectComponent<UISlider>(parent, "Pgb_BossLife");
					if (this.m_bossLifeBar != null)
					{
						this.m_bossLifeBar.value = this.m_currentRaidBossData.GetHpRate();
						this.m_bossLifeBar.numberOfSteps = 1;
						this.m_bossLifeBar.ForceUpdate();
					}
					UISprite uisprite6 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_boss_bg");
					if (uisprite6 != null)
					{
						if (this.m_currentRaidBossData.IsDiscoverer())
						{
							uisprite6.spriteName = "ui_event_raidboss_window_boss_bar_1";
						}
						else
						{
							uisprite6.spriteName = "ui_event_raidboss_window_boss_bar_0";
						}
					}
				}
			}
			else
			{
				GameObject gameObject10 = GameObjectUtil.FindChildGameObject(gameObject4, "pattern_0");
				if (gameObject10 != null)
				{
					string name6 = "img_word_play";
					GameObject gameObject11 = GameObjectUtil.FindChildGameObject(gameObject10, name6);
					if (gameObject11 != null)
					{
						UISprite component3 = gameObject11.GetComponent<UISprite>();
						if (component3 != null)
						{
							if (MileageMapUtility.IsBossStage() && !flag)
							{
								component3.spriteName = "ui_mm_btn_word_play_boss";
							}
							else
							{
								component3.spriteName = "ui_mm_btn_word_play";
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x000BD064 File Offset: 0x000BB264
	private void UpdateRaidbossChallangeCountView()
	{
		if (this.m_isRaidMenu)
		{
			this.m_raidChargeCountMax = (uint)EventManager.Instance.RaidbossChallengeCount;
			if (this.m_ChallengeCountLabel != null)
			{
				this.m_ChallengeCountLabel.text = "/" + this.m_raidChargeCountMax.ToString();
			}
			if (this.m_raidChargeCountMax > 3U)
			{
				this.m_raidChargeCountMax = 3U;
			}
			if (this.m_ChargeText != null)
			{
				this.m_ChargeText.text = "x" + this.m_raidChargeCount.ToString();
			}
		}
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x000BD104 File Offset: 0x000BB304
	private IEnumerator OnStart()
	{
		if (ServerInterface.FreeItemState != null)
		{
			ServerInterface.FreeItemState.SetExpiredFlag(true);
		}
		yield return null;
		GameObject anchor9 = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_9_BR");
		if (anchor9 != null)
		{
			for (int index = 0; index < 4; index++)
			{
				string buttonParentName = "pattern_" + index.ToString();
				GameObject buttonParentObject = GameObjectUtil.FindChildGameObject(anchor9, buttonParentName);
				if (!(buttonParentObject == null))
				{
					GameObject ui_button = GameObjectUtil.FindChildGameObject(buttonParentObject, "Btn_play");
					if (!(ui_button == null))
					{
						UIButtonMessage button_msg = ui_button.GetComponent<UIButtonMessage>();
						if (button_msg == null)
						{
							ui_button.AddComponent<UIButtonMessage>();
							button_msg = ui_button.GetComponent<UIButtonMessage>();
						}
						if (button_msg != null)
						{
							button_msg.enabled = true;
							button_msg.trigger = UIButtonMessage.Trigger.OnClick;
							button_msg.target = base.gameObject;
							button_msg.functionName = "OnPlayButtonClicked";
						}
						if (index == 3)
						{
							GameObject ui_bet_button = GameObjectUtil.FindChildGameObject(buttonParentObject, "Btn_bet");
							if (ui_bet_button != null)
							{
								UIButtonMessage button_bet_msg = ui_bet_button.GetComponent<UIButtonMessage>();
								if (button_bet_msg == null)
								{
									ui_bet_button.AddComponent<UIButtonMessage>();
									button_bet_msg = ui_bet_button.GetComponent<UIButtonMessage>();
								}
								if (button_bet_msg != null)
								{
									button_bet_msg.enabled = true;
									button_bet_msg.trigger = UIButtonMessage.Trigger.OnClick;
									button_bet_msg.target = base.gameObject;
									button_bet_msg.functionName = "OnBetButtonClicked";
								}
							}
						}
						UIPlayAnimation[] anims = ui_button.GetComponents<UIPlayAnimation>();
						if (anims != null)
						{
							foreach (UIPlayAnimation anim in anims)
							{
								if (!(anim == null))
								{
									anim.target = null;
								}
							}
						}
					}
				}
			}
		}
		this.m_itemSet = GameObjectUtil.FindChildGameObjectComponent<ItemSet>(base.gameObject, "item_set_contents");
		this.m_instantItemSet = GameObjectUtil.FindChildGameObjectComponent<InstantItemSet>(base.gameObject, "item_boost");
		this.m_ringManagement = base.gameObject.GetComponent<ItemSetRingManagement>();
		this.m_hideGameObjects.Add(GameObjectUtil.FindChildGameObject(base.gameObject, "item_boost"));
		this.m_hideGameObjects.Add(GameObjectUtil.FindChildGameObject(base.gameObject, "item_set_contents_maintenance"));
		this.m_hideGameObjects.Add(GameObjectUtil.FindChildGameObject(base.gameObject, "item_info"));
		yield return null;
		foreach (GameObject o in this.m_hideGameObjects)
		{
			if (!(o == null))
			{
				o.SetActive(false);
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x000BD120 File Offset: 0x000BB320
	private void Update()
	{
		switch (this.m_tutorialMode)
		{
		case ItemSetMenu.TutorialMode.AppolloStartWait:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
				this.CreateTutorialWindow(0);
				this.m_tutorialMode = ItemSetMenu.TutorialMode.Window1;
			}
			break;
		case ItemSetMenu.TutorialMode.Window1:
			if (GeneralWindow.IsCreated("ItemTutorial") && GeneralWindow.IsButtonPressed)
			{
				HudMenuUtility.SetConnectAlertSimpleUI(false);
				GeneralWindow.Close();
				TutorialCursor.StartTutorialCursor(TutorialCursor.Type.ITEMSELECT_LASER);
				this.m_tutorialMode = ItemSetMenu.TutorialMode.Laser;
			}
			break;
		case ItemSetMenu.TutorialMode.Laser:
			if (this.IsEndTutorialLaser())
			{
				TutorialCursor.StartTutorialCursor(TutorialCursor.Type.MAINMENU_PLAY);
				this.SetEnablePlayButton(true);
				this.m_tutorialMode = ItemSetMenu.TutorialMode.Play;
			}
			break;
		case ItemSetMenu.TutorialMode.AppolloEndWait:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
				this.m_tutorialMode = ItemSetMenu.TutorialMode.Window2;
				this.CreateTutorialWindow(1);
			}
			break;
		case ItemSetMenu.TutorialMode.Window2:
			if (GeneralWindow.IsCreated("ItemTutorial2") && GeneralWindow.IsButtonPressed)
			{
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.PLAY_ITEM, false);
				GeneralWindow.Close();
				this.m_tutorialMode = ItemSetMenu.TutorialMode.Idle;
			}
			break;
		case ItemSetMenu.TutorialMode.Window3:
			if (GeneralWindow.IsCreated("ItemTutorial3") && GeneralWindow.IsButtonPressed)
			{
				HudMenuUtility.SetConnectAlertSimpleUI(false);
				GeneralWindow.Close();
				TutorialCursor.StartTutorialCursor(TutorialCursor.Type.ITEMSELECT_SUBCHARA);
				this.m_timer = 3f;
				this.m_tutorialMode = ItemSetMenu.TutorialMode.SubChara;
			}
			break;
		case ItemSetMenu.TutorialMode.SubChara:
			this.m_timer -= Time.deltaTime;
			if (TutorialCursor.IsTouchScreen() || this.m_timer < 0f)
			{
				TutorialCursor.DestroyTutorialCursor();
				HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.SUB_CHARA_ITEM_EXPLAINED);
				this.SetEnablePlayButton(true);
				this.m_tutorialMode = ItemSetMenu.TutorialMode.Idle;
			}
			break;
		}
		if (this.m_isRaidMenu)
		{
			if (!this.m_isRaidbossTimeOver)
			{
				this.m_timeCounter -= this.m_targetFrameTime;
				if (this.m_timeCounter <= 0f)
				{
					if (this.m_bossTime != null && this.m_currentRaidBossData != null)
					{
						this.m_bossTime.text = this.m_currentRaidBossData.GetTimeLimitString(true);
					}
					this.m_timeCounter = 0.25f;
				}
			}
			else if (GeneralWindow.IsCreated("RaidbossTimeOver") && GeneralWindow.IsButtonPressed)
			{
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.ITEM_BACK, false);
				GeneralWindow.Close();
			}
		}
		if (GeneralWindow.IsCreated("RingMissing"))
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.RING_TO_SHOP, false);
				this.OnMsgMenuBack();
				GeneralWindow.Close();
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				GeneralWindow.Close();
			}
		}
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x000BD3FC File Offset: 0x000BB5FC
	private void ServerGetRingExchangeList_Succeeded(MsgGetRingExchangeListSucceed msg)
	{
		this.Setup();
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x000BD404 File Offset: 0x000BB604
	private void GetFreeItemList()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		this.m_isUpdateFreeItems = false;
		if (loggedInServerInterface != null)
		{
			ServerFreeItemState freeItemState = ServerInterface.FreeItemState;
			if (freeItemState.IsExpired())
			{
				loggedInServerInterface.RequestServerGetFreeItemList(base.gameObject);
				this.m_isUpdateFreeItems = true;
			}
		}
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x000BD450 File Offset: 0x000BB650
	private void ServerGetFreeItemList_Succeeded(MsgGetFreeItemListSucceed msg)
	{
		if (this.m_instantItemSet != null)
		{
			this.m_instantItemSet.UpdateFreeItemList(msg.m_freeItemState);
			this.m_instantItemSet.SetupBoostedItem();
		}
		if (this.m_itemSet != null)
		{
			this.m_itemSet.UpdateFreeItemList(msg.m_freeItemState);
			this.m_itemSet.SetupEquipItem();
		}
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x000BD4B8 File Offset: 0x000BB6B8
	private void OnMsgMenuItemSetStart(MsgMenuItemSetStart msg)
	{
		this.ServerGetRingExchangeList_Succeeded(null);
		switch (msg.m_setMode)
		{
		case MsgMenuItemSetStart.SetMode.NORMAL:
			this.m_tutorialMode = ItemSetMenu.TutorialMode.Idle;
			this.CheckFreeItemAndEquip();
			break;
		case MsgMenuItemSetStart.SetMode.TUTORIAL:
		{
			BackKeyManager.TutorialFlag = true;
			HudMenuUtility.SetConnectAlertSimpleUI(true);
			this.SetEnablePlayButton(false);
			this.ClearEquipedItemForTutorial();
			string[] value = new string[1];
			SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP4, ref value);
			this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_START, value);
			this.m_tutorialMode = ItemSetMenu.TutorialMode.AppolloStartWait;
			break;
		}
		case MsgMenuItemSetStart.SetMode.TUTORIAL_SUBCHARA:
		{
			uint num = 0U;
			if (SaveDataManager.Instance != null)
			{
				num = SaveDataManager.Instance.PlayerData.ChallengeCount;
			}
			if (num == 0U)
			{
				this.m_tutorialMode = ItemSetMenu.TutorialMode.Idle;
			}
			else
			{
				HudMenuUtility.SetConnectAlertSimpleUI(true);
				this.SetEnablePlayButton(false);
				this.m_tutorialMode = ItemSetMenu.TutorialMode.Window3;
				this.CreateTutorialWindow(2);
			}
			this.CheckFreeItemAndEquip();
			break;
		}
		}
		this.m_msg = msg;
		if (this.m_isRaidMenu)
		{
			this.UpdateRaidbossChallangeCountView();
		}
		this.OnEnable();
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x000BD5B8 File Offset: 0x000BB7B8
	private void CheckFreeItemAndEquip()
	{
		this.GetFreeItemList();
		if (!this.m_isUpdateFreeItems)
		{
			ServerFreeItemState freeItemState = ServerInterface.FreeItemState;
			if (freeItemState != null)
			{
				if (this.m_instantItemSet != null)
				{
					this.m_instantItemSet.UpdateFreeItemList(freeItemState);
				}
				if (this.m_itemSet != null)
				{
					this.m_itemSet.UpdateFreeItemList(freeItemState);
				}
			}
			if (this.m_instantItemSet != null)
			{
				this.m_instantItemSet.SetupBoostedItem();
			}
			if (this.m_itemSet != null)
			{
				this.m_itemSet.SetupEquipItem();
			}
		}
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x000BD654 File Offset: 0x000BB854
	private void ClearEquipedItemForTutorial()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			PlayerData playerData = instance.PlayerData;
			if (playerData != null)
			{
				playerData.BoostedItem[0] = false;
				playerData.BoostedItem[2] = false;
				playerData.BoostedItem[1] = false;
				playerData.EquippedItem[0] = ItemType.UNKNOWN;
				playerData.EquippedItem[1] = ItemType.UNKNOWN;
				playerData.EquippedItem[2] = ItemType.UNKNOWN;
				if (this.m_instantItemSet != null)
				{
					this.m_instantItemSet.SetupBoostedItem();
				}
				if (this.m_itemSet != null)
				{
					this.m_itemSet.SetupEquipItem();
				}
			}
		}
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x000BD6F0 File Offset: 0x000BB8F0
	private void OnMsgMenuBack()
	{
		this.m_instantItemSet.ResetCheckMark();
		this.m_itemSet.ResetCheckMark();
		if (this.m_UpdateCharaSettingFlag)
		{
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			this.m_UpdateCharaSettingFlag = false;
		}
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x000BD720 File Offset: 0x000BB920
	private void OnPlayButtonClicked(GameObject obj)
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (this.m_msg != null && this.m_msg.m_setMode == MsgMenuItemSetStart.SetMode.TUTORIAL)
		{
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.MAINMENU_PLAY);
			this.m_tutorialMode = ItemSetMenu.TutorialMode.AppolloEndWait;
			string[] value = new string[1];
			SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP4, ref value);
			this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_END, value);
		}
		else if (this.m_ringManagement != null)
		{
			if (this.m_ringManagement.GetDisplayRingCount() >= 0)
			{
				if (this.m_isRaidMenu)
				{
					if (this.m_currentRaidBossData.IsLimit())
					{
						GeneralWindow.Create(new GeneralWindow.CInfo
						{
							name = "RaidbossTimeOver",
							buttonType = GeneralWindow.ButtonType.Ok,
							caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_raid_boss_bye"),
							message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_raid_boss_bye2"),
							anchor_path = "Camera/menu_Anim/ItemSet_3_UI/Anchor_5_MC"
						});
						this.m_isRaidbossTimeOver = true;
					}
					else
					{
						uint raidbossChallengeCount = (uint)EventManager.Instance.RaidbossChallengeCount;
						if (this.m_raidChargeCount <= raidbossChallengeCount)
						{
							EventManager.Instance.UseRaidbossChallengeCount = (int)this.m_raidChargeCount;
							HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.PLAY_ITEM, false);
						}
					}
				}
				else
				{
					HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.PLAY_ITEM, false);
				}
			}
			else
			{
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					name = "RingMissing",
					caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_cost_caption").text,
					message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_cost_text").text,
					anchor_path = "Camera/menu_Anim/ItemSet_3_UI/Anchor_5_MC",
					buttonType = GeneralWindow.ButtonType.ShopCancel
				});
			}
		}
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x000BD8DC File Offset: 0x000BBADC
	private void OnBetButtonClicked(GameObject obj)
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		uint raidChargeCount = this.m_raidChargeCount;
		if (this.m_raidChargeCountMax <= 1U)
		{
			this.m_raidChargeCount = 1U;
		}
		else if (this.m_raidChargeCount < this.m_raidChargeCountMax)
		{
			this.m_raidChargeCount += 1U;
		}
		else
		{
			this.m_raidChargeCount = 1U;
		}
		if (raidChargeCount != this.m_raidChargeCount)
		{
			this.m_ChargeText.text = "x" + this.m_raidChargeCount.ToString();
			if (this.m_raidChargeCount == 3U)
			{
				this.m_playBetObject0.SetActive(false);
				this.m_playBetObject1.SetActive(true);
				this.m_playBetMaxSign.SetActive(true);
				this.SetRaidAttackRate();
			}
			else if (this.m_raidChargeCount > 1U)
			{
				this.m_playBetObject0.SetActive(false);
				this.m_playBetObject1.SetActive(true);
				this.m_playBetMaxSign.SetActive(false);
				this.SetRaidAttackRate();
			}
			else
			{
				this.m_playBetObject0.SetActive(true);
				this.m_playBetObject1.SetActive(false);
				this.m_playBetMaxSign.SetActive(false);
			}
		}
	}

	// Token: 0x06001FC7 RID: 8135 RVA: 0x000BDA0C File Offset: 0x000BBC0C
	private void SetRaidAttackRate()
	{
		if (this.m_raidChargeCount > 0U)
		{
			float raidAttackRate = EventManager.Instance.GetRaidAttackRate((int)this.m_raidChargeCount);
			if (this.m_AttackRateText != null)
			{
				this.m_AttackRateText.text = "x" + raidAttackRate.ToString();
			}
		}
	}

	// Token: 0x06001FC8 RID: 8136 RVA: 0x000BDA64 File Offset: 0x000BBC64
	private void CreateTutorialWindow(int index)
	{
		string str = string.Empty;
		if (index > 0)
		{
			str = (index + 1).ToString();
		}
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "ItemTutorial" + str,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", "tutorial_comment_caption" + str).text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", "tutorial_comment_text" + str).text,
			anchor_path = "Camera/menu_Anim/ItemSet_3_UI/Anchor_5_MC",
			buttonType = GeneralWindow.ButtonType.Ok
		});
	}

	// Token: 0x06001FC9 RID: 8137 RVA: 0x000BDB04 File Offset: 0x000BBD04
	private void Setup()
	{
		foreach (GameObject gameObject in this.m_hideGameObjects)
		{
			if (!(gameObject == null))
			{
				gameObject.SetActive(true);
			}
		}
		if (this.m_instantItemSet != null)
		{
			this.m_instantItemSet.Setup();
		}
		if (this.m_itemSet != null)
		{
			this.m_itemSet.Setup();
		}
		this.m_UpdateCharaSettingFlag = false;
		this.m_isEndSetup = true;
	}

	// Token: 0x06001FCA RID: 8138 RVA: 0x000BDBC4 File Offset: 0x000BBDC4
	private bool IsEndTutorialLaser()
	{
		if (this.m_itemSet != null)
		{
			ItemType[] item = this.m_itemSet.GetItem();
			if (item != null)
			{
				foreach (ItemType itemType in item)
				{
					if (itemType == ItemType.LASER)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001FCB RID: 8139 RVA: 0x000BDC18 File Offset: 0x000BBE18
	private void CharaSetButtonClickedCallback()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		DeckViewWindow.Create(base.gameObject);
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x000BDC38 File Offset: 0x000BBE38
	private void OnMsgReset()
	{
		if (StageAbilityManager.Instance != null)
		{
			StageAbilityManager.Instance.RecalcAbilityVaue();
			this.m_UpdateCharaSettingFlag = true;
		}
		if (this.m_itemSet != null)
		{
			this.m_itemSet.UpdateView();
		}
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x000BDC78 File Offset: 0x000BBE78
	private void SetEnablePlayButton(bool enabledFlag)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_7_BL");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "Btn_cmn_back");
			if (gameObject2 != null)
			{
				BoxCollider component = gameObject2.GetComponent<BoxCollider>();
				if (component != null)
				{
					component.isTrigger = !enabledFlag;
				}
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "Btn_charaset");
			if (gameObject3 != null)
			{
				BoxCollider component2 = gameObject3.GetComponent<BoxCollider>();
				if (component2 != null)
				{
					component2.isTrigger = !enabledFlag;
				}
			}
		}
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_9_BR");
		if (gameObject4 != null)
		{
			GameObject x = GameObjectUtil.FindChildGameObject(gameObject4, "pattern_0");
			if (x != null)
			{
				GameObject gameObject5 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_play");
				if (gameObject5 != null)
				{
					BoxCollider component3 = gameObject5.GetComponent<BoxCollider>();
					if (component3 != null)
					{
						component3.isTrigger = !enabledFlag;
					}
				}
			}
		}
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x000BDD8C File Offset: 0x000BBF8C
	public void UpdateBoostButton()
	{
		if (this.m_instantItemSet != null)
		{
			this.m_instantItemSet.ResetCheckMark();
			this.m_instantItemSet.Setup();
			this.m_instantItemSet.SetupBoostedItem();
		}
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x000BDDCC File Offset: 0x000BBFCC
	public static void UpdateBoostItemForCharacterDeck()
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			ItemSetMenu itemSetMenu = GameObjectUtil.FindChildGameObjectComponent<ItemSetMenu>(cameraUIObject, "ItemSet_3_UI");
			if (itemSetMenu != null)
			{
				itemSetMenu.UpdateBoostButton();
			}
		}
	}

	// Token: 0x04001C9E RID: 7326
	private const float UPDATE_TIME = 0.25f;

	// Token: 0x04001C9F RID: 7327
	private const uint MAX_RAID_ATTACK_CHARGE = 3U;

	// Token: 0x04001CA0 RID: 7328
	private ItemSet m_itemSet;

	// Token: 0x04001CA1 RID: 7329
	private InstantItemSet m_instantItemSet;

	// Token: 0x04001CA2 RID: 7330
	private ItemSetRingManagement m_ringManagement;

	// Token: 0x04001CA3 RID: 7331
	private List<GameObject> m_hideGameObjects = new List<GameObject>();

	// Token: 0x04001CA4 RID: 7332
	private MsgMenuItemSetStart m_msg;

	// Token: 0x04001CA5 RID: 7333
	private float m_timer = 3f;

	// Token: 0x04001CA6 RID: 7334
	private ItemSetMenu.TutorialMode m_tutorialMode;

	// Token: 0x04001CA7 RID: 7335
	private float m_targetFrameTime = 0.016666668f;

	// Token: 0x04001CA8 RID: 7336
	private float m_timeCounter = 0.25f;

	// Token: 0x04001CA9 RID: 7337
	private bool m_isRaidMenu;

	// Token: 0x04001CAA RID: 7338
	private uint m_raidChargeCount = 1U;

	// Token: 0x04001CAB RID: 7339
	private uint m_raidChargeCountMax = 3U;

	// Token: 0x04001CAC RID: 7340
	private UILabel m_ChargeText;

	// Token: 0x04001CAD RID: 7341
	private UILabel m_AttackRateText;

	// Token: 0x04001CAE RID: 7342
	private UILabel m_bossTime;

	// Token: 0x04001CAF RID: 7343
	private UILabel m_ChallengeCountLabel;

	// Token: 0x04001CB0 RID: 7344
	private GameObject m_playBetObject0;

	// Token: 0x04001CB1 RID: 7345
	private GameObject m_playBetObject1;

	// Token: 0x04001CB2 RID: 7346
	private GameObject m_playBetMaxSign;

	// Token: 0x04001CB3 RID: 7347
	private UISlider m_bossLifeBar;

	// Token: 0x04001CB4 RID: 7348
	private RaidBossData m_currentRaidBossData;

	// Token: 0x04001CB5 RID: 7349
	private SendApollo m_sendApollo;

	// Token: 0x04001CB6 RID: 7350
	private bool m_isRaidbossTimeOver;

	// Token: 0x04001CB7 RID: 7351
	private bool m_isUpdateFreeItems;

	// Token: 0x04001CB8 RID: 7352
	private bool m_UpdateCharaSettingFlag;

	// Token: 0x04001CB9 RID: 7353
	private bool m_isEndSetup;

	// Token: 0x0200041E RID: 1054
	private enum TutorialMode
	{
		// Token: 0x04001CBB RID: 7355
		Idle,
		// Token: 0x04001CBC RID: 7356
		AppolloStartWait,
		// Token: 0x04001CBD RID: 7357
		Window1,
		// Token: 0x04001CBE RID: 7358
		Laser,
		// Token: 0x04001CBF RID: 7359
		AppolloEndWait,
		// Token: 0x04001CC0 RID: 7360
		Play,
		// Token: 0x04001CC1 RID: 7361
		Window2,
		// Token: 0x04001CC2 RID: 7362
		Window3,
		// Token: 0x04001CC3 RID: 7363
		SubChara
	}

	// Token: 0x0200041F RID: 1055
	private enum ButtonType
	{
		// Token: 0x04001CC5 RID: 7365
		NORMAL_STAGE,
		// Token: 0x04001CC6 RID: 7366
		CHALLENGE_BOSS,
		// Token: 0x04001CC7 RID: 7367
		SP_STAGE,
		// Token: 0x04001CC8 RID: 7368
		RAID_BOSS_ATTACK,
		// Token: 0x04001CC9 RID: 7369
		NUM
	}
}
