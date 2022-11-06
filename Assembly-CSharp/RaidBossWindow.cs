using System;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000228 RID: 552
public class RaidBossWindow : EventWindowBase
{
	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00055868 File Offset: 0x00053A68
	public bool isLoading
	{
		get
		{
			return this.m_isLoading;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00055870 File Offset: 0x00053A70
	public float time
	{
		get
		{
			return this.m_time;
		}
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x00055878 File Offset: 0x00053A78
	private void Update()
	{
		this.m_time += Time.deltaTime;
		if (this.m_time >= 3.4028235E+38f)
		{
			this.m_time = 1000f;
		}
		this.CheckReloadBtn(this.m_time);
		if (GeneralWindow.IsCreated("RaidbossChallengeMissing"))
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				GeneralWindow.Close();
				this.OnClickEvChallenge();
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				GeneralWindow.Close();
			}
		}
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x000558F8 File Offset: 0x00053AF8
	private void CheckReloadBtn(float time)
	{
		if (time <= 5f && this.m_btnReload != null && this.m_btnReload.Length > 0)
		{
			foreach (UIButton uibutton in this.m_btnReload)
			{
				uibutton.isEnabled = false;
			}
		}
		else if (time > 5f && this.m_btnReload != null && this.m_btnReload.Length > 0)
		{
			foreach (UIButton uibutton2 in this.m_btnReload)
			{
				uibutton2.isEnabled = true;
			}
		}
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x000559A8 File Offset: 0x00053BA8
	private void UpdateList()
	{
		this.m_time = 0f;
		this.m_isMyBoss = false;
		if (this.m_energy != null)
		{
			this.m_energy.Init();
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_energy.gameObject, "img_sale_icon_challenge");
		if (gameObject != null)
		{
			bool flag = true;
			if (EventManager.Instance != null)
			{
				flag = EventManager.Instance.IsChallengeEvent();
			}
			if (HudMenuUtility.IsSale(Constants.Campaign.emType.PurchaseAddRaidEnergys) && flag)
			{
				gameObject.SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
		this.m_rbInfoData = null;
		if (EventManager.Instance != null)
		{
			this.m_rbInfoData = EventManager.Instance.RaidBossInfo;
		}
		if (this.m_rbInfoData != null)
		{
			if (this.m_crushCount != null)
			{
				this.m_crushCount.text = HudUtility.GetFormatNumString<long>(this.m_rbInfoData.totalDestroyCount);
			}
			if (this.m_raidRingCount != null)
			{
				this.m_raidRingCount.text = HudUtility.GetFormatNumString<long>(GeneralUtil.GetItemCount(ServerItem.Id.RAIDRING));
			}
			if (this.m_listPanel != null)
			{
				List<RaidBossData> raidData = this.m_rbInfoData.raidData;
				List<RaidBossData> list = new List<RaidBossData>();
				List<RaidBossData> list2 = new List<RaidBossData>();
				List<RaidBossData> list3 = new List<RaidBossData>();
				List<RaidBossData> list4 = new List<RaidBossData>();
				List<RaidBossData> list5 = new List<RaidBossData>();
				if (raidData != null)
				{
					for (int i = 0; i < raidData.Count; i++)
					{
						if (raidData[i] != null)
						{
							if (raidData[i].end && !raidData[i].IsDiscoverer())
							{
								if (!raidData[i].participation)
								{
									list5.Add(raidData[i]);
								}
								else if (raidData[i].clear)
								{
									list3.Add(raidData[i]);
								}
								else
								{
									list4.Add(raidData[i]);
								}
							}
							else if (raidData[i].IsDiscoverer())
							{
								list.Add(raidData[i]);
								this.m_isMyBoss = true;
							}
							else
							{
								list2.Add(raidData[i]);
							}
						}
					}
				}
				if (list2.Count > 0)
				{
					foreach (RaidBossData item in list2)
					{
						list.Add(item);
					}
				}
				if (list3.Count > 0)
				{
					foreach (RaidBossData item2 in list3)
					{
						list.Add(item2);
					}
				}
				if (list4.Count > 0)
				{
					foreach (RaidBossData item3 in list4)
					{
						list.Add(item3);
					}
				}
				if (list5.Count > 0)
				{
					ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
					int id = EventManager.Instance.Id;
					if (loggedInServerInterface != null)
					{
						foreach (RaidBossData raidBossData in list5)
						{
							loggedInServerInterface.RequestServerGetEventRaidBossUserList(id, raidBossData.id, base.gameObject);
						}
					}
				}
				this.m_storage = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(this.m_listPanel.gameObject, "slot");
				if (this.m_storage != null)
				{
					this.m_storage.maxRows = list.Count;
					this.m_storage.Restart();
					if (list.Count > 0)
					{
						List<ui_event_raid_scroll> list6 = GameObjectUtil.FindChildGameObjectsComponents<ui_event_raid_scroll>(this.m_listPanel.gameObject, "ui_event_raid_scroll(Clone)");
						if (list6 != null && list6.Count > 0)
						{
							for (int j = 0; j < list6.Count; j++)
							{
								if (j >= list.Count)
								{
									break;
								}
								list6[j].UpdateView(list[j], this, true);
							}
						}
						this.m_listPanel.Scroll(1f);
						this.m_listPanel.ResetPosition();
					}
				}
				if (this.eventEndObject != null)
				{
					GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_charge_challenge");
					GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_roulette");
					if (!EventManager.Instance.IsChallengeEvent() && list.Count <= 0)
					{
						this.eventEndObject.SetActive(true);
						UIButtonMessage componentInChildren = this.eventEndObject.GetComponentInChildren<UIButtonMessage>();
						if (componentInChildren != null)
						{
							componentInChildren.target = base.gameObject;
							componentInChildren.functionName = "OnClickRouletteRaid";
						}
						UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.eventEndObject, "Lbl_expdate");
						if (uilabel != null)
						{
							DateTime eventCloseTime = EventManager.Instance.EventCloseTime;
							string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "event_finished_guidance_2").text;
							if (!string.IsNullOrEmpty(text))
							{
								uilabel.text = text.Replace("{DATE}", eventCloseTime.ToString());
							}
						}
						if (gameObject2 != null)
						{
							gameObject2.SetActive(false);
						}
						if (gameObject3 != null)
						{
							gameObject3.SetActive(false);
						}
					}
					else
					{
						this.eventEndObject.SetActive(false);
						if (gameObject2 != null)
						{
							gameObject2.SetActive(true);
						}
						if (gameObject3 != null)
						{
							gameObject3.SetActive(true);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x00055FF8 File Offset: 0x000541F8
	public void Setup(RaidBossInfo info, RaidBossWindow.WINDOW_OPEN_MODE mode)
	{
		RaidBossInfo.currentRaidData = null;
		this.m_isLoading = false;
		this.m_isBossAttention = false;
		this.m_isMyBoss = false;
		this.m_opened = false;
		this.m_openMode = mode;
		this.mainPanel.alpha = 1f;
		this.SetObject();
		this.SetAlertSimpleUI(true);
		HudMenuUtility.SendEnableShopButton(true);
		if (this.m_animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_eventmenu_intro", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			SoundManager.SePlay("sys_window_open", "SE");
		}
		Texture bgtexture = EventUtility.GetBGTexture();
		if (bgtexture != null && this.m_bgTexture != null)
		{
			this.m_bgTexture.mainTexture = bgtexture;
		}
		base.enabledAnchorObjects = true;
		info.callback = new RaidBossInfo.CallbackRaidBossInfoUpdate(this.CallbackInfoUpdate);
		this.UpdateList();
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.NONE;
		this.m_close = false;
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x00056104 File Offset: 0x00054304
	protected override void SetObject()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_reload");
		if (gameObject != null)
		{
			this.m_btnReload = gameObject.GetComponents<UIButton>();
		}
		this.ShowBossAdvent(this.m_openMode != RaidBossWindow.WINDOW_OPEN_MODE.NONE);
		GeneralUtil.SetRouletteBtnIcon(base.gameObject, "Btn_roulette");
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x00056160 File Offset: 0x00054360
	private void ShowBossAdvent(bool adventActive)
	{
		if (this.m_advent != null)
		{
			this.m_advent.SetActive(adventActive);
			if (adventActive)
			{
				RaidBossInfo raidBossInfo = EventManager.Instance.RaidBossInfo;
				RaidBossData raidBossData = null;
				if (raidBossInfo != null)
				{
					List<RaidBossData> raidData = raidBossInfo.raidData;
					if (raidData != null && raidData.Count > 0)
					{
						foreach (RaidBossData raidBossData2 in raidData)
						{
							if (raidBossData2.IsDiscoverer())
							{
								raidBossData = raidBossData2;
								break;
							}
						}
					}
				}
				this.m_bossAnim = GameObjectUtil.FindChildGameObjectComponent<Animation>(this.m_advent.gameObject, "bit_Anim");
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_advent.gameObject, "Lbl_boss_level");
				List<UISprite> list = null;
				for (int i = 0; i < 5; i++)
				{
					UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_advent.gameObject, "boss_icon" + i);
					if (!(uisprite != null))
					{
						break;
					}
					if (list == null)
					{
						list = new List<UISprite>();
					}
					list.Add(uisprite);
				}
				if (raidBossData != null)
				{
					if (uilabel != null)
					{
						string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_LevelNumber").text;
						uilabel.text = text.Replace("{PARAM}", raidBossData.lv.ToString());
					}
					if (list != null)
					{
						foreach (UISprite uisprite2 in list)
						{
							uisprite2.spriteName = "ui_event_raidboss_icon_silhouette_" + raidBossData.rarity;
							uisprite2.color = new Color(1f, 1f, 1f, uisprite2.alpha);
						}
					}
				}
				else
				{
					this.m_advent.SetActive(false);
				}
			}
		}
		if (this.m_openMode == RaidBossWindow.WINDOW_OPEN_MODE.NONE && adventActive)
		{
			this.StartRaidbossAttentionAnim();
		}
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x000563C0 File Offset: 0x000545C0
	private void StartRaidbossAttentionAnim()
	{
		ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_bossAnim, "ui_EventResult_raidboss_attention_intro_Anim", Direction.Forward);
		EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.BossAnimationIntroCallback), true);
		SoundManager.SePlay("sys_boss_warning", "SE");
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x00056408 File Offset: 0x00054608
	public void OnClickEvChallenge()
	{
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.CHALLENGE;
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_cmn_back");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
		HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.RAIDENERGY_TO_SHOP, false);
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00056480 File Offset: 0x00054680
	public void OnClickNo()
	{
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.CLOSE;
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_cmn_back");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x000564F0 File Offset: 0x000546F0
	public void OnClickNoBg()
	{
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x000564F4 File Offset: 0x000546F4
	public void OnClickReload()
	{
		if (GeneralUtil.IsNetwork())
		{
			if (this.m_time > 5f)
			{
				RaidBossInfo.currentRaidData = null;
				this.RequestServerGetEventUserRaidBossList();
				SoundManager.SePlay("sys_menu_decide", "SE");
			}
		}
		else
		{
			this.ShowNoCommunication();
		}
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00056544 File Offset: 0x00054744
	public void RequestServerGetEventUserRaidBossList()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			int eventId = 0;
			if (EventManager.Instance != null)
			{
				eventId = EventManager.Instance.Id;
			}
			this.m_isLoading = true;
			this.SetAlertSimpleUI(true);
			loggedInServerInterface.RequestServerGetEventUserRaidBossList(eventId, base.gameObject);
		}
		else
		{
			this.ServerGetEventUserRaidBossList_Succeeded(null);
		}
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x000565A8 File Offset: 0x000547A8
	private void ServerGetEventUserRaidBossList_Succeeded(MsgGetEventUserRaidBossListSucceed msg)
	{
		this.m_isLoading = false;
		if (RaidBossInfo.currentRaidData == null)
		{
			this.SetAlertSimpleUI(false);
			this.listReload();
		}
		else if (RaidBossInfo.currentRaidData.end)
		{
			this.SetAlertSimpleUI(false);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "BossEnd",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_raid_boss_bye"),
				message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_raid_boss_bye2")
			});
			this.listReload();
		}
		else
		{
			this.SetAlertSimpleUI(true);
			this.m_btnAct = RaidBossWindow.BUTTON_ACT.BOSS_PLAY;
			this.m_close = true;
			SoundManager.SePlay("sys_window_close", "SE");
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_cmn_back");
			UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
			if (component != null)
			{
				EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
				component.Play(true);
			}
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x000566B4 File Offset: 0x000548B4
	private void ServerGetEventUserRaidBossList_Failed(MsgServerConnctFailed msg)
	{
		this.m_isLoading = false;
		this.SetAlertSimpleUI(false);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x000566C4 File Offset: 0x000548C4
	public void listReload()
	{
		this.m_time = 0f;
		this.UpdateList();
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x000566D8 File Offset: 0x000548D8
	private void CallbackInfoUpdate(RaidBossInfo info)
	{
		this.UpdateList();
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x000566E0 File Offset: 0x000548E0
	public void OnClickBossPlayButton(RaidBossData bossData)
	{
		if (this.m_energy != null && this.m_energy.energyCount <= 0U)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RaidbossChallengeMissing",
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "no_challenge_raid_count").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "no_challenge_raid_count_info").text,
				anchor_path = "Camera/RaidBossWindowUI/Anchor_5_MC",
				buttonType = GeneralWindow.ButtonType.ShopCancel
			});
			return;
		}
		this.SetAlertSimpleUI(true);
		RaidBossInfo.currentRaidData = bossData;
		TimeSpan timeLimit = bossData.GetTimeLimit();
		if (this.time >= 300f || timeLimit.Ticks <= 0L)
		{
			this.RequestServerGetEventUserRaidBossList();
		}
		this.m_energy.ReflectChallengeCount();
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.BOSS_PLAY;
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_cmn_back");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0005681C File Offset: 0x00054A1C
	public void OnClickBossInfoButton(RaidBossData bossData)
	{
		RaidBossInfo.currentRaidData = bossData;
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.BOSS_INFO;
		SoundManager.SePlay("sys_menu_decide", "SE");
		RaidBossDamageRewardWindow.Create(bossData, this);
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x00056844 File Offset: 0x00054A44
	public void OnClickBossRewardButton(RaidBossData bossData)
	{
		RaidBossInfo.currentRaidData = bossData;
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.BOSS_REWARD;
		SoundManager.SePlay("sys_menu_decide", "SE");
		RaidBossDamageRewardWindow.Create(bossData, this);
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0005686C File Offset: 0x00054A6C
	public void OnClickBossInfoBackButton(RaidBossData bossData)
	{
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.NONE;
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x00056878 File Offset: 0x00054A78
	public void OnClickReward()
	{
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.INFO;
		if (EventManager.Instance != null)
		{
			EventRewardWindow.Create(EventManager.Instance.RaidBossInfo);
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x000568A4 File Offset: 0x00054AA4
	public void OnClickRoulette()
	{
		this.SetAlertSimpleUI(true);
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.ROULETTE;
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_roulette");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0005691C File Offset: 0x00054B1C
	public void OnClickRouletteRaid()
	{
		if (EventManager.Instance != null && EventManager.Instance.TypeInTime != EventManager.EventType.RAID_BOSS)
		{
			GeneralUtil.ShowEventEnd("ShowEventEnd");
			return;
		}
		this.SetAlertSimpleUI(true);
		RouletteUtility.rouletteDefault = RouletteCategory.RAID;
		this.m_btnAct = RaidBossWindow.BUTTON_ACT.ROULETTE;
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_raid_roulette");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x000569C4 File Offset: 0x00054BC4
	public void OnClickAdventBg()
	{
		if (this.m_advent != null)
		{
			this.m_advent.SetActive(false);
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x000569F4 File Offset: 0x00054BF4
	public void OnClickEndButton(ButtonInfoTable.ButtonType btnType)
	{
		this.SetAlertSimpleUI(true);
		switch (btnType)
		{
		case ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP:
			this.m_btnAct = RaidBossWindow.BUTTON_ACT.SHOP_RSRING;
			break;
		case ButtonInfoTable.ButtonType.RING_TO_SHOP:
			this.m_btnAct = RaidBossWindow.BUTTON_ACT.SHOP_RING;
			break;
		case ButtonInfoTable.ButtonType.CHALLENGE_TO_SHOP:
			this.m_btnAct = RaidBossWindow.BUTTON_ACT.SHOP_CHALLENGE;
			break;
		default:
			if (btnType == ButtonInfoTable.ButtonType.REWARDLIST_TO_CHAO_ROULETTE)
			{
				this.m_btnAct = RaidBossWindow.BUTTON_ACT.ROULETTE;
			}
			break;
		case ButtonInfoTable.ButtonType.EVENT_BACK:
			this.m_btnAct = RaidBossWindow.BUTTON_ACT.CLOSE;
			break;
		}
		this.m_close = true;
		if (this.m_animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_eventmenu_outro", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
		}
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x00056AC0 File Offset: 0x00054CC0
	public void OnClickBossAttention()
	{
		if (this.m_isBossAttention)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_bossAnim, "ui_EventResult_raidboss_attention_outro_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.BossAnimationFinishCallback), true);
			this.m_isBossAttention = false;
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x00056B1C File Offset: 0x00054D1C
	private void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null && this.m_alertCollider)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x00056B38 File Offset: 0x00054D38
	private void BossAnimationIntroCallback()
	{
		this.m_isBossAttention = true;
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x00056B44 File Offset: 0x00054D44
	private void BossAnimationFinishCallback()
	{
		this.m_advent.SetActive(false);
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x00056B54 File Offset: 0x00054D54
	private bool IsActiveAdventData()
	{
		return this.m_advent != null && this.m_advent.activeSelf;
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x00056B74 File Offset: 0x00054D74
	private void WindowAnimationFinishCallback()
	{
		if (this.m_close)
		{
			switch (this.m_btnAct)
			{
			case RaidBossWindow.BUTTON_ACT.BOSS_PLAY:
				HudMenuUtility.SendVirtualNewItemSelectClicked(HudMenuUtility.ITEM_SELECT_MODE.EVENT_BOSS);
				base.enabledAnchorObjects = false;
				HudMenuUtility.SendEnableShopButton(true);
				break;
			case RaidBossWindow.BUTTON_ACT.BOSS_INFO:
				break;
			case RaidBossWindow.BUTTON_ACT.BOSS_REWARD:
				break;
			case RaidBossWindow.BUTTON_ACT.INFO:
				break;
			case RaidBossWindow.BUTTON_ACT.ROULETTE:
				base.enabledAnchorObjects = false;
				HudMenuUtility.SendEnableShopButton(true);
				HudMenuUtility.SendChaoRouletteButtonClicked();
				break;
			case RaidBossWindow.BUTTON_ACT.CHALLENGE:
				base.enabledAnchorObjects = false;
				break;
			case RaidBossWindow.BUTTON_ACT.SHOP_RSRING:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP, false);
				base.enabledAnchorObjects = false;
				break;
			case RaidBossWindow.BUTTON_ACT.SHOP_RING:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.RING_TO_SHOP, false);
				base.enabledAnchorObjects = false;
				break;
			case RaidBossWindow.BUTTON_ACT.SHOP_CHALLENGE:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHALLENGE_TO_SHOP, false);
				base.enabledAnchorObjects = false;
				break;
			default:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.EVENT_BACK, false);
				base.enabledAnchorObjects = false;
				HudMenuUtility.SendEnableShopButton(true);
				break;
			}
			this.SetAlertSimpleUI(false);
			BackKeyManager.RemoveWindowCallBack(base.gameObject);
			this.m_opened = false;
			this.m_close = false;
		}
		else
		{
			if (this.m_openMode != RaidBossWindow.WINDOW_OPEN_MODE.NONE)
			{
				this.StartRaidbossAttentionAnim();
			}
			this.m_opened = true;
			this.SetAlertSimpleUI(false);
		}
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x00056CA8 File Offset: 0x00054EA8
	private void SetAlertSimpleUI(bool flag)
	{
		if (this.m_alertCollider)
		{
			if (!flag)
			{
				HudMenuUtility.SetConnectAlertSimpleUI(false);
				this.m_alertCollider = false;
			}
		}
		else if (flag)
		{
			HudMenuUtility.SetConnectAlertSimpleUI(true);
			this.m_alertCollider = true;
		}
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x00056CEC File Offset: 0x00054EEC
	public static bool IsEnabled()
	{
		bool result = false;
		if (RaidBossWindow.s_instance != null)
		{
			result = RaidBossWindow.s_instance.enabledAnchorObjects;
		}
		return result;
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x00056D18 File Offset: 0x00054F18
	public static bool IsDataReload()
	{
		bool result = true;
		if (RaidBossWindow.s_instance != null)
		{
			result = RaidBossWindow.s_instance.IsReload();
		}
		return result;
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x00056D44 File Offset: 0x00054F44
	private bool IsReload()
	{
		bool result = false;
		if (this.m_rbInfoData == null || this.m_time > 5f)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x00056D74 File Offset: 0x00054F74
	public static bool IsOpend()
	{
		return RaidBossWindow.s_instance != null && RaidBossWindow.s_instance.m_opened;
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x00056D94 File Offset: 0x00054F94
	public static bool IsOpenAdvent()
	{
		return RaidBossWindow.s_instance != null && RaidBossWindow.s_instance.IsActiveAdventData();
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x00056DB4 File Offset: 0x00054FB4
	public static RaidBossWindow Create(RaidBossInfo info, RaidBossWindow.WINDOW_OPEN_MODE mode)
	{
		if (!(RaidBossWindow.s_instance != null))
		{
			return null;
		}
		if (RaidBossWindow.s_instance.gameObject.transform.parent != null && RaidBossWindow.s_instance.gameObject.transform.parent.name != "Camera")
		{
			return null;
		}
		RaidBossWindow.s_instance.gameObject.SetActive(true);
		RaidBossWindow.s_instance.Setup(info, mode);
		return RaidBossWindow.s_instance;
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x00056E40 File Offset: 0x00055040
	private static void RaidbossDiscoverSaveDataUpdate()
	{
		if (EventManager.Instance == null)
		{
			return;
		}
		if (EventManager.Instance.Type != EventManager.EventType.RAID_BOSS)
		{
			return;
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null && EventManager.Instance.RaidBossInfo != null)
			{
				List<ServerEventRaidBossState> userRaidBossList = EventManager.Instance.UserRaidBossList;
				if (userRaidBossList != null)
				{
					bool flag = false;
					foreach (ServerEventRaidBossState serverEventRaidBossState in userRaidBossList)
					{
						if (serverEventRaidBossState.Encounter && systemdata.currentRaidDrawIndex != serverEventRaidBossState.Id)
						{
							systemdata.currentRaidDrawIndex = serverEventRaidBossState.Id;
							systemdata.raidEntryFlag = false;
							flag = true;
							break;
						}
					}
					if (flag)
					{
						instance.SaveSystemData();
					}
				}
			}
		}
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x00056F48 File Offset: 0x00055148
	public void ShowNoCommunication()
	{
		GeneralUtil.ShowNoCommunication("ShowNoCommunication");
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00056F54 File Offset: 0x00055154
	private static RaidBossWindow Instance
	{
		get
		{
			return RaidBossWindow.s_instance;
		}
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x00056F5C File Offset: 0x0005515C
	private void Awake()
	{
		this.SetInstance();
		base.enabledAnchorObjects = false;
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00056F6C File Offset: 0x0005516C
	private void OnDestroy()
	{
		if (RaidBossWindow.s_instance == this)
		{
			RaidBossWindow.s_instance = null;
		}
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x00056F84 File Offset: 0x00055184
	private void SetInstance()
	{
		if (RaidBossWindow.s_instance == null)
		{
			RaidBossWindow.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000C9F RID: 3231
	public const float RELOAD_TIME = 5f;

	// Token: 0x04000CA0 RID: 3232
	public const float WAIT_LIMIT_TIME = 300f;

	// Token: 0x04000CA1 RID: 3233
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04000CA2 RID: 3234
	[SerializeField]
	private Animation m_animation;

	// Token: 0x04000CA3 RID: 3235
	[SerializeField]
	private UIDraggablePanel m_listPanel;

	// Token: 0x04000CA4 RID: 3236
	[SerializeField]
	private UILabel m_crushCount;

	// Token: 0x04000CA5 RID: 3237
	[SerializeField]
	private UILabel m_raidRingCount;

	// Token: 0x04000CA6 RID: 3238
	[SerializeField]
	private RaidEnergyStorage m_energy;

	// Token: 0x04000CA7 RID: 3239
	[SerializeField]
	private GameObject m_advent;

	// Token: 0x04000CA8 RID: 3240
	[SerializeField]
	private UITexture m_bgTexture;

	// Token: 0x04000CA9 RID: 3241
	[SerializeField]
	private GameObject eventEndObject;

	// Token: 0x04000CAA RID: 3242
	private RaidBossInfo m_rbInfoData;

	// Token: 0x04000CAB RID: 3243
	private bool m_isLoading;

	// Token: 0x04000CAC RID: 3244
	private float m_time;

	// Token: 0x04000CAD RID: 3245
	private bool m_opened;

	// Token: 0x04000CAE RID: 3246
	private bool m_close;

	// Token: 0x04000CAF RID: 3247
	private bool m_alertCollider;

	// Token: 0x04000CB0 RID: 3248
	private RaidBossWindow.BUTTON_ACT m_btnAct = RaidBossWindow.BUTTON_ACT.NONE;

	// Token: 0x04000CB1 RID: 3249
	private UIRectItemStorage m_storage;

	// Token: 0x04000CB2 RID: 3250
	private UIButton[] m_btnReload;

	// Token: 0x04000CB3 RID: 3251
	private bool m_isBossAttention;

	// Token: 0x04000CB4 RID: 3252
	private bool m_isMyBoss;

	// Token: 0x04000CB5 RID: 3253
	private RaidBossWindow.WINDOW_OPEN_MODE m_openMode;

	// Token: 0x04000CB6 RID: 3254
	private Animation m_bossAnim;

	// Token: 0x04000CB7 RID: 3255
	private static RaidBossWindow s_instance;

	// Token: 0x02000229 RID: 553
	private enum BUTTON_ACT
	{
		// Token: 0x04000CB9 RID: 3257
		CLOSE,
		// Token: 0x04000CBA RID: 3258
		BOSS_PLAY,
		// Token: 0x04000CBB RID: 3259
		BOSS_INFO,
		// Token: 0x04000CBC RID: 3260
		BOSS_REWARD,
		// Token: 0x04000CBD RID: 3261
		INFO,
		// Token: 0x04000CBE RID: 3262
		ROULETTE,
		// Token: 0x04000CBF RID: 3263
		CHALLENGE,
		// Token: 0x04000CC0 RID: 3264
		SHOP_RSRING,
		// Token: 0x04000CC1 RID: 3265
		SHOP_RING,
		// Token: 0x04000CC2 RID: 3266
		SHOP_CHALLENGE,
		// Token: 0x04000CC3 RID: 3267
		NONE
	}

	// Token: 0x0200022A RID: 554
	public enum WINDOW_OPEN_MODE
	{
		// Token: 0x04000CC5 RID: 3269
		NONE,
		// Token: 0x04000CC6 RID: 3270
		ADVENT_BOSS_NORMAL,
		// Token: 0x04000CC7 RID: 3271
		ADVENT_BOSS_RARE,
		// Token: 0x04000CC8 RID: 3272
		ADVENT_BOSS_S_RARE
	}
}
