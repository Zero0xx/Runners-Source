using System;
using System.Collections.Generic;
using System.Diagnostics;
using AnimationOrTween;
using App.Utility;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class HudCockpit : MonoBehaviour
{
	// Token: 0x060019BC RID: 6588 RVA: 0x00096010 File Offset: 0x00094210
	private void Start()
	{
		this.Initialize();
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x00096018 File Offset: 0x00094218
	private void Initialize()
	{
		if (this.m_init)
		{
			return;
		}
		this.m_init = true;
		if (StageModeManager.Instance != null)
		{
			this.m_quickModeFlag = StageModeManager.Instance.IsQuickMode();
		}
		this.m_playerInfo = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
		this.m_levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
		this.m_stageScoreManager = StageScoreManager.Instance;
		this.m_charaChangeBtn = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_change");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "HUD_btn_item");
		if (gameObject != null)
		{
			this.m_itemBtn = GameObjectUtil.FindChildGameObject(gameObject, "Btn_item");
			if (this.m_itemBtn != null)
			{
				this.m_itemBtn.SetActive(false);
			}
			for (int i = 0; i < this.m_itemSptires.Length; i++)
			{
				this.m_itemSptires[i] = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "item_" + i);
				if (this.m_itemSptires[i] != null)
				{
					this.m_itemSptires[i].gameObject.SetActive(false);
				}
			}
		}
		GameObject parent = GameObjectUtil.FindChildGameObject(base.gameObject, "HUD_indicator");
		this.m_distanceLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_distance");
		this.m_distanceSlider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(parent, "Pgb_distance");
		this.m_speedLSlider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(parent, "Pgb_speed_L");
		this.m_speedRSlider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(parent, "Pgb_speed_R");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(parent, "pattern_0_default");
		if (gameObject2 != null)
		{
			this.m_endlessObj = GameObjectUtil.FindChildGameObject(gameObject2, "mode_endless");
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(parent, "pattern_2_boss");
		if (gameObject3 != null)
		{
			this.m_endlessBossObj = GameObjectUtil.FindChildGameObject(gameObject3, "mode_endless");
		}
		this.m_quickModeObj = GameObjectUtil.FindChildGameObject(base.gameObject, "mode_quick_time");
		if (this.m_quickModeObj != null)
		{
			this.m_quickModeObj.SetActive(this.m_quickModeFlag);
		}
		if (this.m_quickModeFlag && this.m_quickModeObj != null)
		{
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_quickModeObj, "Lbl_time1");
			if (gameObject4 != null)
			{
				this.m_timer1Label = gameObject4.GetComponent<UILabel>();
				this.m_timer1TweenColor = gameObject4.GetComponent<TweenColor>();
			}
			GameObject gameObject5 = GameObjectUtil.FindChildGameObject(this.m_quickModeObj, "Lbl_time2");
			if (gameObject5 != null)
			{
				this.m_timer2Label = gameObject5.GetComponent<UILabel>();
				this.m_timer2TweenColor = gameObject5.GetComponent<TweenColor>();
			}
		}
		this.SetupBossParam(BossType.FEVER, 0, 0);
		GameObject parent2 = GameObjectUtil.FindChildGameObject(base.gameObject, "HUD_btn_pause");
		this.m_pauseButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent2, "Btn_pause");
		if (this.m_pauseButton != null)
		{
			this.m_pauseButtonAnim = this.m_pauseButton.GetComponent<UIPlayAnimation>();
			this.m_pauseButtonAnim.enabled = false;
			this.m_pauseButton.isEnabled = this.m_enablePause;
		}
		this.m_colorPowerEffect = GameObjectUtil.FindChildGameObject(base.gameObject, "HUD_ColorPower_effect");
		this.m_backTitle = false;
		this.ItemButton_SetEnabled(false);
		this.UpdateItemView();
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x00096348 File Offset: 0x00094548
	private void SetupBossParam(BossType bossType, int hp, int hpMax)
	{
		GameObject parent = GameObjectUtil.FindChildGameObject(base.gameObject, "HUD_indicator");
		GameObject gameObject = null;
		string name;
		switch (bossType)
		{
		case BossType.FEVER:
			name = "pattern_2_boss";
			break;
		case BossType.MAP1:
		case BossType.MAP2:
		case BossType.MAP3:
			gameObject = GameObjectUtil.FindChildGameObject(parent, "pattern_0_default");
			name = "pattern_1_boss";
			break;
		default:
			gameObject = GameObjectUtil.FindChildGameObject(parent, "pattern_0_default");
			name = "pattern_3_raid";
			break;
		}
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		this.m_bossGauge = GameObjectUtil.FindChildGameObject(parent, name);
		if (this.m_bossGauge != null)
		{
			if (bossType != BossType.FEVER)
			{
				this.m_bossLifeSlider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(this.m_bossGauge, "Pgb_boss_life");
				this.m_bossLifeLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_bossGauge, "Lbl_boss_life");
			}
			this.m_deathDistance = GameObjectUtil.FindChildGameObjectComponent<UISlider>(this.m_bossGauge, "Pgb_death_distance");
			this.m_deathDistanceTweenColor = GameObjectUtil.FindChildGameObjectComponent<TweenColor>(this.m_deathDistance.gameObject, "img_gauge_distance");
		}
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x00096458 File Offset: 0x00094658
	private void Update()
	{
		if (this.m_createWindow && GeneralWindow.IsCreated("BackMainMenuCheckWindow"))
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				GeneralWindow.Close();
				GameObjectUtil.SendMessageFindGameObject("pause_window", "OnBackMainMenuAnimation", null, SendMessageOptions.DontRequireReceiver);
				GameObjectUtil.SendMessageToTagObjects("GameModeStage", "OnMsgNotifyEndPauseExitStage", new MsgNotifyEndPauseExitStage(), SendMessageOptions.DontRequireReceiver);
				this.m_backTitle = true;
				this.m_createWindow = false;
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				ObjUtil.SetHudStockRingEffectOff(false);
				GeneralWindow.Close();
				this.m_createWindow = false;
			}
		}
		if (this.m_pauseContinueCount > 0)
		{
			this.m_pauseContinueTimer -= RealTime.deltaTime;
			if (this.m_pauseContinueTimer <= 0f)
			{
				this.m_pauseContinueCount--;
				if (this.m_pauseContinueCount > 0)
				{
					SoundManager.SePlay("sys_pause", "SE");
					this.m_pauseContinueTimer = 1f;
				}
				else
				{
					SoundManager.SePlay("sys_go", "SE");
					this.m_pauseContinueTimer = 0f;
				}
			}
		}
		if (this.m_playerInfo != null)
		{
			if (this.m_ringLabel != null)
			{
				int numRings = this.m_playerInfo.NumRings;
				if (this.m_ringCount != numRings)
				{
					this.m_ringCount = numRings;
					this.m_ringLabel.text = HudUtility.GetFormatNumString<int>(this.m_ringCount);
				}
				if (this.m_ringTweenColor != null)
				{
					if (this.m_ringTweenColor.enabled)
					{
						if (numRings > 0)
						{
							this.m_ringTweenColor.enabled = false;
							this.m_ringLabel.color = this.m_ringDefaultColor;
						}
					}
					else if (numRings == 0)
					{
						this.m_ringTweenColor.enabled = true;
					}
				}
			}
			if (this.m_stockRingLabel != null && this.m_stageScoreManager != null)
			{
				int num = (int)this.m_stageScoreManager.Ring;
				if (this.m_stockRingCount != num)
				{
					this.m_stockRingCount = num;
					this.m_stockRingLabel.text = HudUtility.GetFormatNumString<int>(this.m_stockRingCount);
				}
			}
			if (this.m_quickModeFlag)
			{
				this.UpdateTimer();
			}
			if (this.m_distanceLabel != null)
			{
				int num2 = Mathf.FloorToInt(this.m_playerInfo.TotalDistance);
				if (this.m_distance != num2)
				{
					this.m_distance = num2;
					this.m_distanceLabel.text = HudUtility.GetFormatNumString<int>(this.m_distance);
				}
			}
			if (this.m_distanceSlider != null && this.m_levelInformation != null)
			{
				float num3 = (this.m_levelInformation.DistanceToBossOnStart != 0f) ? (this.m_levelInformation.DistanceOnStage / this.m_levelInformation.DistanceToBossOnStart) : 1f;
				if (num3 != this.m_distanceSlider.value)
				{
					this.m_distanceSlider.value = num3;
				}
			}
			float num4 = (1f + (float)this.m_playerInfo.SpeedLevel) / 3f;
			if (this.m_speedLSlider != null && this.m_speedLSlider.value != num4)
			{
				this.m_speedLSlider.value = num4;
			}
			if (this.m_speedRSlider != null && this.m_speedRSlider.value != num4)
			{
				this.m_speedRSlider.value = num4;
			}
		}
		if (this.m_stageScoreManager != null)
		{
			if (this.m_scoreLabel != null)
			{
				long realtimeScore = this.m_stageScoreManager.GetRealtimeScore();
				if (this.m_realTimeScore != realtimeScore)
				{
					this.m_realTimeScore = realtimeScore;
					this.m_scoreLabel.text = HudUtility.GetFormatNumString<long>(this.m_realTimeScore);
					if (this.m_nextScoreRank < HudCockpit.ScoreRank.NUM && (long)this.m_scoreColors[(int)this.m_nextScoreRank].score < this.m_realTimeScore)
					{
						this.m_scoreColor.r = this.m_scoreColors[(int)this.m_nextScoreRank].Red;
						this.m_scoreColor.g = this.m_scoreColors[(int)this.m_nextScoreRank].Green;
						this.m_scoreColor.b = this.m_scoreColors[(int)this.m_nextScoreRank].Blue;
						this.m_scoreLabel.color = this.m_scoreColor;
						this.m_nextScoreRank++;
						ActiveAnimation.Play(this.m_scoreAnim, "ui_gp_bit_score_Anim", Direction.Forward);
					}
				}
			}
			if (this.m_spCrystalLabel != null)
			{
				int num5 = (int)this.m_stageScoreManager.GetRealtimeEventScore();
				if (this.m_crystalCount != num5)
				{
					this.m_crystalCount = num5;
					this.m_spCrystalLabel.text = HudUtility.GetFormatNumString<int>(this.m_crystalCount);
				}
			}
			if (this.m_animalLabel != null)
			{
				int num6 = (int)this.m_stageScoreManager.GetRealtimeEventAnimal();
				if (this.m_animalCount != num6)
				{
					this.m_animalCount = num6;
					this.m_animalLabel.text = HudUtility.GetFormatNumString<int>(this.m_animalCount);
				}
			}
		}
		if (this.m_deathDistance != null && this.m_bossGauge != null && this.m_bossGauge.activeSelf && this.m_playerInfo != null && this.m_levelInformation != null)
		{
			if (!this.m_bBossStart)
			{
				if (this.m_deathDistance.value != 0f)
				{
					this.m_deathDistance.value = 0f;
					this.m_bossTime = this.m_levelInformation.BossEndTime;
				}
			}
			else
			{
				float num7 = 0f;
				this.m_bossTime -= Time.deltaTime;
				if (this.m_bossTime < 0f)
				{
					this.m_bossTime = 0f;
				}
				if (this.m_bossTime > 0f && this.m_levelInformation.BossEndTime > 0f)
				{
					num7 = this.m_bossTime / this.m_levelInformation.BossEndTime;
				}
				this.m_deathDistance.value = 1f - num7;
			}
			if (this.m_deathDistance.value > 0.8f && this.m_deathDistanceTweenColor != null && !this.m_deathDistanceTweenColor.enabled)
			{
				this.m_deathDistanceTweenColor.enabled = true;
			}
			if (this.m_deathDistance.value == 1f && this.m_bBossBattleDistance)
			{
				MsgBossDistanceEnd value = new MsgBossDistanceEnd(true);
				GameObjectUtil.SendMessageToTagObjects("Boss", "OnMsgBossDistanceEnd", value, SendMessageOptions.DontRequireReceiver);
				this.m_bBossBattleDistance = false;
				GameObjectUtil.SendMessageToTagObjects("GameModeStage", "OnBossTimeUp", null, SendMessageOptions.DontRequireReceiver);
			}
			if (this.m_bossTime < 3f && !this.m_bBossBattleDistanceArea)
			{
				MsgBossDistanceEnd value2 = new MsgBossDistanceEnd(false);
				GameObjectUtil.SendMessageToTagObjects("Boss", "OnMsgBossDistanceEnd", value2, SendMessageOptions.DontRequireReceiver);
				this.m_bBossBattleDistanceArea = true;
			}
		}
		if (this.m_charaChaneDisableTime > 0f)
		{
			if (this.m_charaChaneDisableTime < Time.deltaTime)
			{
				this.m_charaChaneDisableTime = 0f;
			}
			else
			{
				this.m_charaChaneDisableTime -= Time.deltaTime;
			}
			if (this.m_charaChaneDisableTime == 0f)
			{
				this.OnChangeCharaButton(new MsgChangeCharaButton(true, true));
			}
		}
	}

	// Token: 0x060019C0 RID: 6592 RVA: 0x00096BAC File Offset: 0x00094DAC
	private void UpdateItemView()
	{
		string arg = (!this.m_enableItem) ? "ui_cmn_icon_item_g_" : "ui_cmn_icon_item_";
		for (int i = 0; i < this.m_itemSptires.Length; i++)
		{
			ItemType itemType = (i >= this.m_displayItems.Count) ? ItemType.UNKNOWN : this.m_displayItems[i];
			if (this.m_itemSptires[i] != null)
			{
				if (itemType == ItemType.UNKNOWN)
				{
					this.m_itemSptires[i].gameObject.SetActive(false);
				}
				else
				{
					this.m_itemSptires[i].gameObject.SetActive(true);
					this.m_itemSptires[i].spriteName = arg + (int)itemType;
				}
			}
		}
		if (this.m_itemBtn != null)
		{
			this.m_itemBtn.SetActive(this.m_displayItems.Count > 0);
		}
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x00096C9C File Offset: 0x00094E9C
	private void UpdateTimer()
	{
		if (StageTimeManager.Instance != null)
		{
			float time = StageTimeManager.Instance.Time;
			int num = (int)time;
			int decimalNumber = (int)((time - (float)num) * 100f);
			if (this.m_timer1Label != null)
			{
				this.m_timer1Label.text = num.ToString("D2") + " .";
			}
			if (this.m_timer2Label != null)
			{
				this.m_timer2Label.text = decimalNumber.ToString("D2");
			}
			if (this.m_countDownFlag)
			{
				if (time > 10f)
				{
					this.m_countDownFlag = false;
					if (this.m_timer1TweenColor != null)
					{
						this.m_timer1TweenColor.enabled = false;
						if (this.m_timer1Label != null)
						{
							this.m_timer1Label.color = this.m_timer1TweenColor.from;
						}
					}
					if (this.m_timer2TweenColor != null)
					{
						this.m_timer2TweenColor.enabled = false;
						if (this.m_timer2Label != null)
						{
							this.m_timer2Label.color = this.m_timer2TweenColor.from;
						}
					}
				}
			}
			else if (time < 10f)
			{
				this.m_countDownFlag = true;
				this.m_countDownSEflags.Reset();
				if (this.m_timer1TweenColor != null)
				{
					this.m_timer1TweenColor.enabled = true;
				}
				if (this.m_timer2TweenColor != null)
				{
					this.m_timer2TweenColor.enabled = true;
				}
			}
			this.UpdateCountDownSE(num, decimalNumber);
		}
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x00096E38 File Offset: 0x00095038
	private void UpdateCountDownSE(int integerNumber, int decimalNumber)
	{
		if (this.m_countDownFlag)
		{
			if (integerNumber == 0 && decimalNumber == 0)
			{
				if (!this.m_countDownSEflags.Test(0))
				{
					ObjUtil.PlaySE("sys_quickmode_count_zero", "SE");
					this.m_countDownSEflags.Set(0);
				}
			}
			else
			{
				if (this.m_countDownSEflags.Test(0))
				{
					this.m_countDownSEflags.Reset(0);
				}
				for (int i = 0; i < 10; i++)
				{
					if (i < integerNumber)
					{
						if (this.m_countDownSEflags.Test(i + 1))
						{
							this.m_countDownSEflags.Reset(i + 1);
						}
					}
					else if (i == integerNumber && !this.m_countDownSEflags.Test(i + 1))
					{
						this.m_countDownSEflags.Set(i + 1);
						ObjUtil.PlaySE("sys_quickmode_count", "SE");
						break;
					}
				}
			}
		}
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x00096F2C File Offset: 0x0009512C
	private void OnClickStartPause()
	{
		GameObjectUtil.SendMessageToTagObjects("GameModeStage", "OnMsgNotifyStartPause", new MsgNotifyStartPause(), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060019C4 RID: 6596 RVA: 0x00096F44 File Offset: 0x00095144
	private void OnClickEndPause()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		GC.Collect();
		Resources.UnloadUnusedAssets();
		GC.Collect();
		this.m_pauseContinueCount = 3;
		this.m_pauseContinueTimer = 1f;
	}

	// Token: 0x060019C5 RID: 6597 RVA: 0x00096F84 File Offset: 0x00095184
	private void OnFinishedContinueAnimation()
	{
		if (this.m_pauseFlag)
		{
			GameObjectUtil.SendMessageToTagObjects("GameModeStage", "OnMsgNotifyEndPause", new MsgNotifyEndPause(), SendMessageOptions.DontRequireReceiver);
			this.m_pauseFlag = false;
		}
	}

	// Token: 0x060019C6 RID: 6598 RVA: 0x00096FB0 File Offset: 0x000951B0
	private void OnClickEndPauseExitStage()
	{
		if (this.m_pauseContinueCount == 0 && this.m_pauseContinueTimer == 0f)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.CreateBackMainMenuCheckWindow();
		}
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x00096FE4 File Offset: 0x000951E4
	private void OnEnablePause(MsgEnablePause msg)
	{
		this.m_enablePause = msg.m_enable;
		if (this.m_pauseButton != null)
		{
			this.m_pauseButton.isEnabled = this.m_enablePause;
		}
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x00097020 File Offset: 0x00095220
	private void OnClickChange()
	{
		SoundManager.SePlay("act_chara_change", "SE");
		if (this.m_charaChaneDisableTime == 0f)
		{
			GameObjectUtil.SendMessageToTagObjects("GameModeStage", "OnMsgChangeChara", new MsgChangeChara
			{
				m_changeByBtn = true
			}, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x0009706C File Offset: 0x0009526C
	private void OnChangeCharaSucceed(MsgChangeCharaSucceed msg)
	{
		this.m_charaChaneDisableTime = 5f;
		if (msg.m_disabled)
		{
			this.ChangeButton_SetActive(false);
		}
		else
		{
			this.m_changeFlag = true;
			this.ChangeButton_SetEnabled(false);
		}
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x000970AC File Offset: 0x000952AC
	private void OnChangeCharaEnable(MsgChangeCharaEnable msg)
	{
		this.Initialize();
		this.ChangeButton_SetActive(msg.value);
		this.ChangeButton_SetEnabled(false);
	}

	// Token: 0x060019CB RID: 6603 RVA: 0x000970C8 File Offset: 0x000952C8
	private void OnChangeCharaButton(MsgChangeCharaButton msg)
	{
		if (!msg.value)
		{
			if (!msg.pause)
			{
				this.m_changeFlag = false;
			}
			this.ChangeButton_SetEnabled(false);
		}
		else
		{
			if (!msg.pause)
			{
				this.m_changeFlag = true;
			}
			if (this.m_charaChaneDisableTime == 0f && this.m_changeFlag)
			{
				this.ChangeButton_SetEnabled(true);
			}
		}
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x00097134 File Offset: 0x00095334
	private void ChangeButton_SetActive(bool isActive)
	{
		if (this.m_charaChangeBtn != null)
		{
			this.m_charaChangeBtn.SetActive(isActive);
		}
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x00097154 File Offset: 0x00095354
	private void ChangeButton_SetEnabled(bool isEnabled)
	{
		if (this.m_charaChangeBtn != null)
		{
			UIImageButton component = this.m_charaChangeBtn.GetComponent<UIImageButton>();
			if (component != null)
			{
				component.isEnabled = isEnabled;
			}
		}
	}

	// Token: 0x060019CE RID: 6606 RVA: 0x00097194 File Offset: 0x00095394
	private void ItemButton_SetActive(bool isActive)
	{
		if (this.m_itemBtn != null)
		{
			this.m_itemBtn.SetActive(isActive);
		}
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x000971B4 File Offset: 0x000953B4
	private void ItemButton_SetEnabled(bool isEnabled)
	{
		this.m_enableItem = isEnabled;
		if (this.m_itemBtn != null)
		{
			UIImageButton component = this.m_itemBtn.GetComponent<UIImageButton>();
			if (component != null)
			{
				component.isEnabled = isEnabled;
			}
		}
		this.UpdateItemView();
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x00097200 File Offset: 0x00095400
	private void OnItemEnable(MsgItemButtonEnable msg)
	{
		if (msg.m_enable && !this.m_itemPause)
		{
			this.ItemButton_SetEnabled(true);
		}
		else
		{
			this.ItemButton_SetEnabled(false);
		}
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x0009722C File Offset: 0x0009542C
	private void OnStartTutorial()
	{
		this.m_itemTutorial = true;
		HudTutorial.StartTutorial(HudTutorial.Id.ITEM_BUTTON_1, BossType.NONE);
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x00097240 File Offset: 0x00095440
	private void OnNextTutorial()
	{
		if (this.m_itemTutorial)
		{
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.STAGE_ITEM);
		}
	}

	// Token: 0x060019D3 RID: 6611 RVA: 0x00097254 File Offset: 0x00095454
	private void OnSetEquippedItem(MsgSetEquippedItem msg)
	{
		foreach (ItemType itemType2 in msg.m_itemType)
		{
			if (itemType2 != ItemType.UNKNOWN)
			{
				this.m_displayItems.Add(itemType2);
			}
		}
		this.UpdateItemView();
	}

	// Token: 0x060019D4 RID: 6612 RVA: 0x0009729C File Offset: 0x0009549C
	private void OnSetPresentEquippedItem(MsgSetEquippedItem msg)
	{
		bool flag = this.m_displayItems.Count > 0;
		this.m_displayItems.Clear();
		foreach (ItemType itemType2 in msg.m_itemType)
		{
			if (itemType2 != ItemType.UNKNOWN)
			{
				this.m_displayItems.Add(itemType2);
			}
		}
		this.UpdateItemView();
		if (!flag && msg.m_enabled && !this.m_itemPause)
		{
			this.ItemButton_SetEnabled(true);
		}
	}

	// Token: 0x060019D5 RID: 6613 RVA: 0x00097320 File Offset: 0x00095520
	private void OnChangeItem(MsgSetEquippedItem msg)
	{
		bool flag = this.m_displayItems.Count > 0;
		this.m_displayItems.Clear();
		foreach (ItemType itemType2 in msg.m_itemType)
		{
			if (itemType2 != ItemType.UNKNOWN)
			{
				this.m_displayItems.Add(itemType2);
			}
		}
		this.UpdateItemView();
	}

	// Token: 0x060019D6 RID: 6614 RVA: 0x00097380 File Offset: 0x00095580
	private void OnUsedItem(MsgUsedItem msg)
	{
		this.m_displayItems.Remove(msg.m_itemType);
		this.UpdateItemView();
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x0009739C File Offset: 0x0009559C
	private void OnSetPause(MsgSetPause msg)
	{
		if (GeneralWindow.IsCreated("BackMainMenuCheckWindow") || this.m_backTitle)
		{
			return;
		}
		if (this.m_pauseFlag && msg.m_backKey)
		{
			if (this.m_pauseContinueCount == 0 && this.m_pauseContinueTimer == 0f)
			{
				GameObjectUtil.SendMessageFindGameObject("pause_window", "OnContinueAnimation", null, SendMessageOptions.DontRequireReceiver);
				this.OnClickEndPause();
				if (this.m_pauseButton != null)
				{
					NGUITools.SetActive(this.m_pauseButton.gameObject, true);
				}
			}
			return;
		}
		if (this.m_pauseButton != null && this.m_pauseButtonAnim != null)
		{
			Animation target = this.m_pauseButtonAnim.target;
			if (target != null)
			{
				this.m_pauseFlag = true;
				GameObjectUtil.SendMessageFindGameObject("pause_window", "OnMsgNotifyStartPause", null, SendMessageOptions.DontRequireReceiver);
				this.m_pauseButton.gameObject.SetActive(false);
				this.m_pauseContinueCount = 0;
				this.m_pauseContinueTimer = 0f;
				target.gameObject.SetActive(true);
				target.Rewind(pause_window.INANIM_NAME);
				ActiveAnimation.Play(target, pause_window.INANIM_NAME, Direction.Forward, true);
				if (msg.m_backMainMenu)
				{
					this.CreateBackMainMenuCheckWindow();
				}
			}
		}
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x000974DC File Offset: 0x000956DC
	private void HudBossHpGaugeOpen(MsgHudBossHpGaugeOpen msg)
	{
		this.SetupBossParam(msg.m_bossType, msg.m_hp, msg.m_hpMax);
		if (this.m_bossGauge != null)
		{
			this.m_bossGauge.SetActive(true);
		}
		this.SetBossHp(msg.m_hp, msg.m_hpMax);
		this.m_bBossBattleDistance = true;
		if (this.m_deathDistanceTweenColor != null)
		{
			this.m_deathDistanceTweenColor.enabled = false;
		}
	}

	// Token: 0x060019D9 RID: 6617 RVA: 0x00097554 File Offset: 0x00095754
	private void HudBossGaugeStart(MsgHudBossGaugeStart msg)
	{
		this.m_bBossStart = true;
		this.m_bBossBattleDistanceArea = false;
	}

	// Token: 0x060019DA RID: 6618 RVA: 0x00097564 File Offset: 0x00095764
	private void OnBossEnd(MsgBossEnd msg)
	{
		if (this.m_bossGauge != null)
		{
			this.m_bossGauge.SetActive(false);
		}
		this.m_bBossStart = false;
		this.m_bBossBattleDistanceArea = false;
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x00097594 File Offset: 0x00095794
	private void HudBossHpGaugeSet(MsgHudBossHpGaugeSet msg)
	{
		this.SetBossHp(msg.m_hp, msg.m_hpMax);
	}

	// Token: 0x060019DC RID: 6620 RVA: 0x000975A8 File Offset: 0x000957A8
	private void SetBossHp(int hp, int hpMax)
	{
		float value = 0f;
		if (hp > 0)
		{
			value = (float)hp / (float)hpMax;
		}
		if (this.m_bossLifeSlider != null)
		{
			this.m_bossLifeSlider.value = value;
		}
		if (this.m_bossLifeLabel != null)
		{
			this.m_bossLifeLabel.text = hp.ToString() + "/" + hpMax.ToString();
		}
	}

	// Token: 0x060019DD RID: 6621 RVA: 0x0009761C File Offset: 0x0009581C
	private void OnMsgPrepareContinue(MsgPrepareContinue msg)
	{
		if (msg.m_bossStage && this.m_deathDistance != null && this.m_levelInformation != null)
		{
			this.m_deathDistance.value = 0f;
			this.m_bossTime = this.m_levelInformation.BossEndTime;
		}
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x00097678 File Offset: 0x00095878
	private void OnPhantomActStart(MsgPhantomActStart msg)
	{
		if (this.m_colorPowerEffect != null)
		{
			this.m_colorPowerEffect.SetActive(true);
		}
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x00097698 File Offset: 0x00095898
	private void OnPhantomActEnd(MsgPhantomActEnd msg)
	{
		if (this.m_colorPowerEffect != null)
		{
			this.m_colorPowerEffect.SetActive(false);
		}
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x000976B8 File Offset: 0x000958B8
	private void OnAddStockRing(MsgAddStockRing msg)
	{
		if (this.m_addStockRing != null && msg.m_numAddRings > 0)
		{
			this.m_addStockRing.SetActive(false);
			this.m_addStockRing.SetActive(true);
			SoundManager.SePlay("act_ring_collect", "SE");
		}
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x0009770C File Offset: 0x0009590C
	private void OnSetup(MsgHudCockpitSetup msg)
	{
		this.m_backMainMenuCheck = msg.m_backMainMenuCheck;
		this.m_firtsTutorial = msg.m_firstTutorial;
		GameObject[] array = new GameObject[4];
		GameObject gameObject = base.gameObject;
		if (gameObject != null)
		{
			for (int i = 0; i < 4; i++)
			{
				Transform transform = gameObject.transform.FindChild("Anchor_2_TC/" + this.Anchor2TC_tbl[i]);
				if (transform != null)
				{
					array[i] = transform.gameObject;
					if (array[i] != null)
					{
						array[i].SetActive(false);
					}
				}
			}
		}
		GameObject gameObject2 = GameObject.Find("UI Root (2D)/Camera/Anchor_5_MC");
		if (gameObject2 != null)
		{
			for (int j = 0; j < this.PauseWindowSetupLbl.Length; j++)
			{
				Transform transform2 = gameObject2.transform.FindChild(this.PauseWindowSetupLbl[j]);
				if (transform2 != null)
				{
					HudUtility.SetupUILabelText(transform2.gameObject);
				}
			}
		}
		if (msg.m_bossType == BossType.MAP1 || msg.m_bossType == BossType.MAP2 || msg.m_bossType == BossType.MAP3)
		{
			GameObject gameObject3 = array[1];
			if (gameObject3 != null)
			{
				gameObject3.SetActive(true);
			}
			this.SetupRingObj(gameObject3);
		}
		else if (msg.m_bossType != BossType.NONE && msg.m_bossType != BossType.FEVER)
		{
			GameObject gameObject4 = array[2];
			if (gameObject4 != null)
			{
				gameObject4.SetActive(true);
			}
			this.SetupRingObj(gameObject4);
		}
		else if (msg.m_spCrystal)
		{
			GameObject gameObject5 = array[3];
			if (gameObject5 != null)
			{
				gameObject5.SetActive(true);
			}
			this.SetupRingObj(gameObject5);
			this.SetupScoreObj(gameObject5);
			this.m_spCrystalLabel = this.SetupEventObj(gameObject5, "HUD_event", "Lbl_event", "img_event_object", "ui_event_object_icon");
		}
		else if (msg.m_animal)
		{
			GameObject gameObject6 = array[3];
			if (gameObject6 != null)
			{
				gameObject6.SetActive(true);
			}
			this.SetupRingObj(gameObject6);
			this.SetupScoreObj(gameObject6);
			this.m_animalLabel = this.SetupEventObj(gameObject6, "HUD_event", "Lbl_event", "img_event_object", "ui_event_object_icon");
		}
		else
		{
			GameObject gameObject7 = array[0];
			if (gameObject7 != null)
			{
				gameObject7.SetActive(true);
			}
			this.SetupRingObj(gameObject7);
			this.SetupScoreObj(gameObject7);
		}
		if (this.m_stageScoreManager != null)
		{
			this.m_stageScoreManager.SetupScoreRate();
		}
		if (this.m_firtsTutorial)
		{
			GameObjectUtil.SendMessageFindGameObject("pause_window", "OnSetFirstTutorial", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x000979B8 File Offset: 0x00095BB8
	private void SetupScoreObj(GameObject patternObj)
	{
		if (patternObj != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(patternObj, "HUD_score");
			if (gameObject != null)
			{
				this.m_scoreLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_score");
				this.m_scoreAnim = GameObjectUtil.FindChildGameObjectComponent<Animation>(gameObject, "Anim_score");
			}
		}
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x00097A0C File Offset: 0x00095C0C
	private UILabel SetupEventObj(GameObject patternObj, string objName, string lblName, string imgName, string texName)
	{
		UILabel result = null;
		if (patternObj != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(patternObj, objName);
			if (gameObject != null)
			{
				result = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, lblName);
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, imgName);
				if (uisprite != null)
				{
					uisprite.spriteName = texName;
				}
			}
		}
		return result;
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x00097A64 File Offset: 0x00095C64
	private void SetupRingObj(GameObject patternObj)
	{
		if (patternObj != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(patternObj, "HUD_ring");
			if (gameObject != null)
			{
				this.m_stockRingLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_stock_ring");
				this.m_ringLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_ring");
				if (this.m_ringLabel != null)
				{
					this.m_ringDefaultColor = this.m_ringLabel.color;
				}
				this.m_ringTweenColor = GameObjectUtil.FindChildGameObjectComponent<TweenColor>(gameObject, "Lbl_ring");
				this.m_addStockRing = GameObjectUtil.FindChildGameObject(gameObject, "add");
				if (this.m_addStockRing != null)
				{
					this.m_addStockRingEff = GameObjectUtil.FindChildGameObject(this.m_addStockRing, "eff_switch");
					this.m_addStockRing.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x00097B30 File Offset: 0x00095D30
	private void CreateBackMainMenuCheckWindow()
	{
		string cellName = (!this.m_backMainMenuCheck) ? "ui_back_menu_text_option" : "ui_back_menu_text";
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "BackMainMenuCheckWindow",
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "PauseWindow", "ui_back_menu_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "PauseWindow", cellName).text,
			buttonType = GeneralWindow.ButtonType.YesNo
		});
		ObjUtil.SetHudStockRingEffectOff(true);
		this.m_createWindow = true;
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x00097BBC File Offset: 0x00095DBC
	private void OnPauseItemOnBoss()
	{
		this.m_itemPause = true;
		this.OnItemEnable(new MsgItemButtonEnable(false));
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x00097BD4 File Offset: 0x00095DD4
	private void OnResumeItemOnBoss(bool phatomFlag)
	{
		this.m_itemPause = false;
		if (!phatomFlag)
		{
			this.OnItemEnable(new MsgItemButtonEnable(true));
		}
	}

	// Token: 0x060019E8 RID: 6632 RVA: 0x00097BF0 File Offset: 0x00095DF0
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
		if (this.m_addStockRing != null)
		{
			this.m_addStockRing.SetActive(false);
		}
	}

	// Token: 0x060019E9 RID: 6633 RVA: 0x00097C24 File Offset: 0x00095E24
	private void OnMsgStockRingEffect(MsgHudStockRingEffect msg)
	{
		if (this.m_addStockRingEff != null)
		{
			if (msg.m_off)
			{
				this.m_addStockRingEff.transform.localPosition = new Vector3(1000f, 1000f, 0f);
			}
			else
			{
				this.m_addStockRingEff.transform.localPosition = Vector3.zero;
			}
		}
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x00097C8C File Offset: 0x00095E8C
	private void OnClickItemButton()
	{
		if (this.m_levelInformation != null)
		{
			this.m_levelInformation.RequestEqitpItem = true;
		}
		if (this.m_itemTutorial)
		{
			this.m_itemTutorial = false;
			TutorialCursor.DestroyTutorialCursor();
			MsgTutorialEnd value = new MsgTutorialEnd();
			GameObjectUtil.SendMessageToTagObjects("GameModeStage", "OnMsgTutorialItemButtonEnd", value, SendMessageOptions.DontRequireReceiver);
		}
		if (StageItemManager.Instance != null)
		{
			for (int i = 0; i < this.m_displayItems.Count; i++)
			{
				if (this.m_displayItems[i] != ItemType.UNKNOWN)
				{
					StageItemManager.Instance.OnRequestItemUse(new MsgAskEquipItemUsed(this.m_displayItems[i]));
					return;
				}
			}
		}
	}

	// Token: 0x060019EB RID: 6635 RVA: 0x00097D40 File Offset: 0x00095F40
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x060019EC RID: 6636 RVA: 0x00097D54 File Offset: 0x00095F54
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x04001701 RID: 5889
	private const float CHARA_CHANGE_DISABLE_TIME = 5f;

	// Token: 0x04001702 RID: 5890
	private PlayerInformation m_playerInfo;

	// Token: 0x04001703 RID: 5891
	private LevelInformation m_levelInformation;

	// Token: 0x04001704 RID: 5892
	private StageScoreManager m_stageScoreManager;

	// Token: 0x04001705 RID: 5893
	private UILabel m_stockRingLabel;

	// Token: 0x04001706 RID: 5894
	private GameObject m_addStockRing;

	// Token: 0x04001707 RID: 5895
	private GameObject m_addStockRingEff;

	// Token: 0x04001708 RID: 5896
	private UILabel m_ringLabel;

	// Token: 0x04001709 RID: 5897
	private TweenColor m_ringTweenColor;

	// Token: 0x0400170A RID: 5898
	private Color m_ringDefaultColor;

	// Token: 0x0400170B RID: 5899
	private UISprite[] m_itemSptires = new UISprite[3];

	// Token: 0x0400170C RID: 5900
	private UILabel m_scoreLabel;

	// Token: 0x0400170D RID: 5901
	private UILabel m_spCrystalLabel;

	// Token: 0x0400170E RID: 5902
	private UILabel m_animalLabel;

	// Token: 0x0400170F RID: 5903
	private long m_realTimeScore = -1L;

	// Token: 0x04001710 RID: 5904
	private int m_ringCount = -1;

	// Token: 0x04001711 RID: 5905
	private int m_stockRingCount = -1;

	// Token: 0x04001712 RID: 5906
	private int m_animalCount = -1;

	// Token: 0x04001713 RID: 5907
	private int m_crystalCount = -1;

	// Token: 0x04001714 RID: 5908
	private int m_distance = -1;

	// Token: 0x04001715 RID: 5909
	private GameObject m_charaChangeBtn;

	// Token: 0x04001716 RID: 5910
	private GameObject m_itemBtn;

	// Token: 0x04001717 RID: 5911
	private GameObject m_quickModeObj;

	// Token: 0x04001718 RID: 5912
	private GameObject m_endlessObj;

	// Token: 0x04001719 RID: 5913
	private GameObject m_endlessBossObj;

	// Token: 0x0400171A RID: 5914
	private UILabel m_distanceLabel;

	// Token: 0x0400171B RID: 5915
	private UILabel m_timer1Label;

	// Token: 0x0400171C RID: 5916
	private UILabel m_timer2Label;

	// Token: 0x0400171D RID: 5917
	private TweenColor m_timer1TweenColor;

	// Token: 0x0400171E RID: 5918
	private TweenColor m_timer2TweenColor;

	// Token: 0x0400171F RID: 5919
	private UISlider m_distanceSlider;

	// Token: 0x04001720 RID: 5920
	private UISlider m_speedLSlider;

	// Token: 0x04001721 RID: 5921
	private UISlider m_speedRSlider;

	// Token: 0x04001722 RID: 5922
	private GameObject m_bossGauge;

	// Token: 0x04001723 RID: 5923
	private UISlider m_bossLifeSlider;

	// Token: 0x04001724 RID: 5924
	private UILabel m_bossLifeLabel;

	// Token: 0x04001725 RID: 5925
	private UISlider m_deathDistance;

	// Token: 0x04001726 RID: 5926
	private TweenColor m_deathDistanceTweenColor;

	// Token: 0x04001727 RID: 5927
	private UIImageButton m_pauseButton;

	// Token: 0x04001728 RID: 5928
	private UIPlayAnimation m_pauseButtonAnim;

	// Token: 0x04001729 RID: 5929
	private GameObject m_colorPowerEffect;

	// Token: 0x0400172A RID: 5930
	private Animation m_scoreAnim;

	// Token: 0x0400172B RID: 5931
	private List<ItemType> m_displayItems = new List<ItemType>();

	// Token: 0x0400172C RID: 5932
	private float m_charaChaneDisableTime;

	// Token: 0x0400172D RID: 5933
	private bool m_enablePause;

	// Token: 0x0400172E RID: 5934
	private bool m_enableItem;

	// Token: 0x0400172F RID: 5935
	private bool m_itemPause;

	// Token: 0x04001730 RID: 5936
	private bool m_bBossBattleDistance;

	// Token: 0x04001731 RID: 5937
	private bool m_bBossBattleDistanceArea;

	// Token: 0x04001732 RID: 5938
	private bool m_bBossStart;

	// Token: 0x04001733 RID: 5939
	private float m_bossTime;

	// Token: 0x04001734 RID: 5940
	private bool m_quickModeFlag;

	// Token: 0x04001735 RID: 5941
	private bool m_countDownFlag;

	// Token: 0x04001736 RID: 5942
	private bool m_changeFlag;

	// Token: 0x04001737 RID: 5943
	private bool m_pauseFlag;

	// Token: 0x04001738 RID: 5944
	private int m_pauseContinueCount;

	// Token: 0x04001739 RID: 5945
	private float m_pauseContinueTimer;

	// Token: 0x0400173A RID: 5946
	private bool m_backTitle;

	// Token: 0x0400173B RID: 5947
	private bool m_backMainMenuCheck;

	// Token: 0x0400173C RID: 5948
	private bool m_createWindow;

	// Token: 0x0400173D RID: 5949
	private bool m_itemTutorial;

	// Token: 0x0400173E RID: 5950
	private bool m_firtsTutorial;

	// Token: 0x0400173F RID: 5951
	private bool m_init;

	// Token: 0x04001740 RID: 5952
	private Bitset32 m_countDownSEflags;

	// Token: 0x04001741 RID: 5953
	private readonly string[] Anchor2TC_tbl = new string[]
	{
		"pattern_0_default",
		"pattern_1_boss",
		"pattern_3_raid",
		"pattern_3_ev1"
	};

	// Token: 0x04001742 RID: 5954
	private readonly string[] PauseWindowSetupLbl = new string[]
	{
		"pause_window/pause_Anim/window/Btn_back_mainmenu/Lbl_back",
		"pause_window/pause_Anim/window/Btn_back_mainmenu/Lbl_back/Lbl_back_sh",
		"pause_window/pause_Anim/window/Btn_continue/Lbl_continue",
		"pause_window/pause_Anim/window/Btn_continue/Lbl_continue/Lbl_continue_sh"
	};

	// Token: 0x04001743 RID: 5955
	private HudCockpit.ScoreRank m_nextScoreRank;

	// Token: 0x04001744 RID: 5956
	[SerializeField]
	private HudCockpit.ScoreColor[] m_scoreColors = new HudCockpit.ScoreColor[8];

	// Token: 0x04001745 RID: 5957
	private Color m_scoreColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x02000368 RID: 872
	private enum Anchor2TC
	{
		// Token: 0x04001747 RID: 5959
		DEFAULT,
		// Token: 0x04001748 RID: 5960
		BOSS,
		// Token: 0x04001749 RID: 5961
		EVENTBOSS,
		// Token: 0x0400174A RID: 5962
		SPSTAGE,
		// Token: 0x0400174B RID: 5963
		NUM
	}

	// Token: 0x02000369 RID: 873
	private enum ScoreRank
	{
		// Token: 0x0400174D RID: 5965
		RANK_01,
		// Token: 0x0400174E RID: 5966
		RANK_02,
		// Token: 0x0400174F RID: 5967
		RANK_03,
		// Token: 0x04001750 RID: 5968
		RANK_04,
		// Token: 0x04001751 RID: 5969
		RANK_05,
		// Token: 0x04001752 RID: 5970
		RANK_06,
		// Token: 0x04001753 RID: 5971
		RANK_07,
		// Token: 0x04001754 RID: 5972
		RANK_08,
		// Token: 0x04001755 RID: 5973
		NUM
	}

	// Token: 0x0200036A RID: 874
	[Serializable]
	private class ScoreColor
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x00097D70 File Offset: 0x00095F70
		public float Red
		{
			get
			{
				return (float)this.red / 255f;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00097D80 File Offset: 0x00095F80
		public float Green
		{
			get
			{
				return (float)this.green / 255f;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00097D90 File Offset: 0x00095F90
		public float Blue
		{
			get
			{
				return (float)this.blue / 255f;
			}
		}

		// Token: 0x04001756 RID: 5974
		public int red;

		// Token: 0x04001757 RID: 5975
		public int green;

		// Token: 0x04001758 RID: 5976
		public int blue;

		// Token: 0x04001759 RID: 5977
		public int score;
	}
}
