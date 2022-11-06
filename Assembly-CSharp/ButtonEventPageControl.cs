using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000436 RID: 1078
public class ButtonEventPageControl : MonoBehaviour
{
	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x060020C3 RID: 8387 RVA: 0x000C4580 File Offset: 0x000C2780
	public bool IsTransform
	{
		get
		{
			return this.m_transform;
		}
	}

	// Token: 0x060020C4 RID: 8388 RVA: 0x000C4588 File Offset: 0x000C2788
	public void Initialize(ButtonEventPageControl.ResourceLoadedCallback callback)
	{
		this.m_menu_anim_obj = HudMenuUtility.GetMenuAnimUIObject();
		GameObject parent = GameObjectUtil.FindChildGameObject(this.m_menu_anim_obj, "MainMenuUI4");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(parent, "page_1");
		this.m_resLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
		HudMenuUtility.SendMsgInitMainMenuUI();
		this.m_pageHistory = new ButtonEventPageHistory();
		this.m_pageHistory.Push(ButtonInfoTable.PageType.MAIN);
		this.m_hud_display = new HudDisplay();
		this.m_animation = base.gameObject.AddComponent<ButtonEventAnimation>();
		this.m_animation.Initialize();
		this.ChangeHeaderText(ButtonInfoTable.PageType.MAIN);
		this.m_resourceLoadedCallback = callback;
	}

	// Token: 0x060020C5 RID: 8389 RVA: 0x000C4620 File Offset: 0x000C2820
	public void PageBack()
	{
		if (this.m_transform)
		{
			return;
		}
		ButtonInfoTable.ButtonType buttonType = this.m_info_table.m_platformBackButtonType[(int)this.m_currentPageType];
		this.PageChange(buttonType, false, true);
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x000C4658 File Offset: 0x000C2858
	public void PageChange(ButtonInfoTable.ButtonType buttonType, bool clearHistory, bool buttonPressed)
	{
		if (this.m_transform)
		{
			return;
		}
		if (this.CheckEventTopRewardListRoutletteButtonClick(buttonType))
		{
			this.SendMsgEventWindow(buttonType);
		}
		else if (this.CheckEventTopShopButtonClick(buttonType))
		{
			this.SendMsgEventWindow(buttonType);
			this.SetClickedEvent(buttonType, clearHistory);
		}
		else if (this.m_currentPageType == ButtonInfoTable.PageType.EVENT)
		{
			this.SendMsgEventWindow(buttonType);
			this.SetClickedEvent(buttonType, clearHistory);
		}
		else
		{
			this.SetClickedEvent(buttonType, clearHistory);
		}
		if (buttonPressed)
		{
			this.m_info_table.PlaySE(buttonType);
		}
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x000C46E4 File Offset: 0x000C28E4
	private void SetClickedEvent(ButtonInfoTable.ButtonType button_type, bool clearHistory)
	{
		global::Debug.Log("SetClicedEvent " + button_type.ToString());
		MsgMenuSequence.SequeneceType sequeneceType = this.m_info_table.GetSequeneceType(button_type);
		if (sequeneceType == MsgMenuSequence.SequeneceType.MAIN)
		{
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		}
		bool flag = sequeneceType == MsgMenuSequence.SequeneceType.BACK;
		ButtonInfoTable.PageType pageType;
		ButtonInfoTable.PageType currentPageType;
		if (flag)
		{
			pageType = this.m_pageHistory.Pop();
			currentPageType = this.m_currentPageType;
		}
		else
		{
			pageType = this.m_info_table.GetPageType(button_type);
			currentPageType = this.m_currentPageType;
			if (clearHistory)
			{
				this.m_pageHistory.Clear();
			}
			else
			{
				bool flag2 = button_type != ButtonInfoTable.ButtonType.VIRTUAL_NEW_ITEM;
				if (flag2 && pageType != ButtonInfoTable.PageType.NON)
				{
					this.m_pageHistory.Push(currentPageType);
				}
			}
		}
		this.m_current_sequence_type = this.m_info_table.GetSequeneceType(pageType);
		this.m_currentPageType = pageType;
		bool flag3 = pageType == ButtonInfoTable.PageType.NON && !flag;
		if (flag3)
		{
			HudMenuUtility.SendMsgMenuSequenceToMainMenu(sequeneceType);
		}
		else
		{
			bool flag4 = currentPageType == ButtonInfoTable.PageType.MAIN && flag;
			if (flag4)
			{
				HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.BACK);
			}
			else
			{
				this.m_transform = true;
				HudMenuUtility.SetConnectAlertSimpleUI(true);
				this.ChangeHeaderText(pageType);
				ButtonInfoTable.PageType pageType2 = pageType;
				switch (pageType2)
				{
				case ButtonInfoTable.PageType.EPISODE_RANKING:
					RankingUtil.SetCurrentRankingMode(RankingUtil.RankingMode.ENDLESS);
					break;
				default:
					if (pageType2 == ButtonInfoTable.PageType.ROULETTE)
					{
						this.SetRoulletePage(button_type);
					}
					break;
				case ButtonInfoTable.PageType.QUICK_RANKING:
					RankingUtil.SetCurrentRankingMode(RankingUtil.RankingMode.QUICK);
					break;
				}
				this.SendMessageEndPage(currentPageType);
				this.m_animation.PageOutAnimation(currentPageType, pageType, new ButtonEventAnimation.AnimationEndCallback(this.OnCurrentPageAnimEndCallback));
			}
		}
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x000C486C File Offset: 0x000C2A6C
	private bool CheckEventTopShopButtonClick(ButtonInfoTable.ButtonType btnType)
	{
		return (btnType == ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP || btnType == ButtonInfoTable.ButtonType.RING_TO_SHOP || btnType == ButtonInfoTable.ButtonType.CHALLENGE_TO_SHOP || btnType == ButtonInfoTable.ButtonType.RAIDENERGY_TO_SHOP) && this.m_currentPageType == ButtonInfoTable.PageType.EVENT;
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x000C489C File Offset: 0x000C2A9C
	private bool CheckEventTopRewardListRoutletteButtonClick(ButtonInfoTable.ButtonType btnType)
	{
		return btnType == ButtonInfoTable.ButtonType.REWARDLIST_TO_CHAO_ROULETTE && this.m_currentPageType == ButtonInfoTable.PageType.EVENT;
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x000C48B4 File Offset: 0x000C2AB4
	public void SetRoulletePage(ButtonInfoTable.ButtonType button_type)
	{
		if (RouletteUtility.rouletteDefault != RouletteCategory.RAID)
		{
			if (button_type == ButtonInfoTable.ButtonType.ITEM_ROULETTE)
			{
				RouletteUtility.rouletteDefault = RouletteCategory.ITEM;
			}
			else
			{
				RouletteUtility.rouletteDefault = RouletteCategory.PREMIUM;
			}
		}
	}

	// Token: 0x060020CB RID: 8395 RVA: 0x000C48E8 File Offset: 0x000C2AE8
	private void ChangeHeaderText(ButtonInfoTable.PageType pageType)
	{
		string[] array = new string[]
		{
			"ui_Header_main_menu",
			"ui_Header_ChaoSet",
			string.Empty,
			"ui_Header_Information",
			"ui_Header_Item",
			string.Empty,
			"ui_Header_Option",
			"ui_Header_PlayerSet",
			"ui_Header_PlayerSet",
			"ui_Header_PresentBox",
			"ui_Header_Information",
			"ui_Header_daily_battle",
			"ui_Header_Roulette_top",
			"ui_Header_Shop",
			"ui_Header_Shop",
			"ui_Header_Shop",
			"ui_Header_Shop",
			"ui_Header_MainPage2",
			"ui_Header_Item",
			"ui_Header_episodemode_score_ranking",
			"ui_Header_Item",
			"ui_Header_quickmode_score_ranking",
			"ui_Header_Item"
		};
		HudMenuUtility.SendChangeHeaderText(array[(int)pageType]);
	}

	// Token: 0x060020CC RID: 8396 RVA: 0x000C49CC File Offset: 0x000C2BCC
	private void OnCurrentPageAnimEndCallback()
	{
		base.StartCoroutine(this.OnCurrentPageAnimationEndCallbackCoroutine());
		switch (this.m_currentPageType)
		{
		case ButtonInfoTable.PageType.DAILY_BATTLE:
		case ButtonInfoTable.PageType.QUICK:
		case ButtonInfoTable.PageType.QUICK_RANKING:
			SoundManager.BgmChange("bgm_sys_menu", "BGM");
			break;
		case ButtonInfoTable.PageType.ROULETTE:
			break;
		case ButtonInfoTable.PageType.SHOP_RSR:
		case ButtonInfoTable.PageType.SHOP_RING:
		case ButtonInfoTable.PageType.SHOP_ENERGY:
		case ButtonInfoTable.PageType.SHOP_EVENT:
			break;
		case ButtonInfoTable.PageType.EPISODE:
		case ButtonInfoTable.PageType.EPISODE_PLAY:
		case ButtonInfoTable.PageType.EPISODE_RANKING:
		case ButtonInfoTable.PageType.PLAY_AT_EPISODE_PAGE:
			SoundManager.BgmChange("bgm_sys_menu", "BGM");
			break;
		default:
			SoundManager.BgmChange("bgm_sys_menu_v2", "BGM_menu_v2");
			break;
		}
	}

	// Token: 0x060020CD RID: 8397 RVA: 0x000C4A74 File Offset: 0x000C2C74
	private IEnumerator OnCurrentPageAnimationEndCallbackCoroutine()
	{
		this.m_hud_display.SetAllDisableDisplay();
		yield return base.StartCoroutine(this.m_resLoader.LoadAtlasResourceIfNotLoaded());
		yield return base.StartCoroutine(this.m_resLoader.LoadPageResourceIfNotLoadedSync(this.m_currentPageType, delegate
		{
			this.m_resourceLoadedCallback();
		}));
		this.m_hud_display = new HudDisplay();
		HudDisplay.ObjType obj_type = HudDisplay.CalcObjTypeFromSequenceType(this.m_current_sequence_type);
		this.m_hud_display.SetDisplayHudObject(obj_type);
		HudMenuUtility.SendMsgMenuSequenceToMainMenu(this.m_current_sequence_type);
		this.SendMessageNextPage(this.m_currentPageType);
		this.m_animation.PageInAnimation(this.m_currentPageType, new ButtonEventAnimation.AnimationEndCallback(this.OnNextPageAnimEndCallback));
		yield break;
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x000C4A90 File Offset: 0x000C2C90
	private void OnNextPageAnimEndCallback()
	{
		this.m_transform = false;
		HudMenuUtility.SetConnectAlertSimpleUI(false);
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x000C4AA0 File Offset: 0x000C2CA0
	private void SendMsgEventWindow(ButtonInfoTable.ButtonType button_type)
	{
		if (EventManager.Instance != null)
		{
			GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
			if (cameraUIObject != null)
			{
				if (EventManager.Instance.Type == EventManager.EventType.SPECIAL_STAGE)
				{
					GameObjectUtil.SendMessageFindGameObject("SpecialStageWindowUI", "OnClickEndButton", button_type, SendMessageOptions.RequireReceiver);
				}
				else if (EventManager.Instance.Type == EventManager.EventType.RAID_BOSS)
				{
					GameObjectUtil.SendMessageFindGameObject("RaidBossWindowUI", "OnClickEndButton", button_type, SendMessageOptions.RequireReceiver);
				}
			}
		}
	}

	// Token: 0x060020D0 RID: 8400 RVA: 0x000C4B24 File Offset: 0x000C2D24
	private void SendMessageEndPage(ButtonInfoTable.PageType endPage)
	{
		if (endPage == ButtonInfoTable.PageType.ITEM || endPage == ButtonInfoTable.PageType.QUICK || endPage == ButtonInfoTable.PageType.EPISODE_PLAY || endPage == ButtonInfoTable.PageType.PLAY_AT_EPISODE_PAGE)
		{
			if (this.m_currentPageType != ButtonInfoTable.PageType.STAGE)
			{
				GameObjectUtil.SendMessageFindGameObject("ItemSet_3_UI", "OnMsgMenuBack", null, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			ButtonInfoTable.MessageInfo pageMessageInfo = this.m_info_table.GetPageMessageInfo(endPage, false);
			this.SendMessage(pageMessageInfo, false);
		}
	}

	// Token: 0x060020D1 RID: 8401 RVA: 0x000C4B8C File Offset: 0x000C2D8C
	private void SendMessageNextPage(ButtonInfoTable.PageType nextPage)
	{
		if (nextPage == ButtonInfoTable.PageType.ITEM || nextPage == ButtonInfoTable.PageType.QUICK || nextPage == ButtonInfoTable.PageType.EPISODE_PLAY || nextPage == ButtonInfoTable.PageType.PLAY_AT_EPISODE_PAGE)
		{
			MsgMenuItemSetStart.SetMode msgMenuItemSetStartMode = ItemSetUtility.GetMsgMenuItemSetStartMode();
			MsgMenuItemSetStart value = new MsgMenuItemSetStart(msgMenuItemSetStartMode);
			GameObjectUtil.SendMessageFindGameObject("ItemSet_3_UI", "OnMsgMenuItemSetStart", value, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			ButtonInfoTable.MessageInfo pageMessageInfo = this.m_info_table.GetPageMessageInfo(nextPage, true);
			this.SendMessage(pageMessageInfo, true);
		}
	}

	// Token: 0x060020D2 RID: 8402 RVA: 0x000C4BF4 File Offset: 0x000C2DF4
	private void SendMessage(ButtonInfoTable.MessageInfo msgInfo, bool waitFlag = false)
	{
		if (msgInfo != null)
		{
			if (!msgInfo.uiFlag)
			{
				GameObjectUtil.SendMessageFindGameObject(msgInfo.targetName, msgInfo.methodName, null, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				int waitCount = (!waitFlag) ? 0 : 30;
				GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
				if (cameraUIObject != null)
				{
					GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, msgInfo.targetName);
					if (gameObject != null)
					{
						Component component = gameObject.GetComponent(msgInfo.componentName);
						if (component != null)
						{
							this.WaitSendMessage(component, waitCount, msgInfo.methodName, null);
						}
					}
				}
			}
		}
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x000C4C90 File Offset: 0x000C2E90
	public void WaitSendMessage(Component component, int waitCount, string methodName, object value)
	{
		base.StartCoroutine(this.WaitSendMessageCoroutine(new ButtonEventPageControl.WaitSendMessageParam
		{
			m_component = component,
			m_waitCount = waitCount,
			m_methodName = methodName,
			m_value = value
		}));
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x000C4CD0 File Offset: 0x000C2ED0
	private IEnumerator WaitSendMessageCoroutine(ButtonEventPageControl.WaitSendMessageParam param)
	{
		for (int i = 0; i < param.m_waitCount; i++)
		{
			if (param.m_component.gameObject.activeInHierarchy)
			{
				break;
			}
			yield return null;
		}
		param.m_component.SendMessage(param.m_methodName, param.m_value, SendMessageOptions.DontRequireReceiver);
		yield break;
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x000C4CF4 File Offset: 0x000C2EF4
	private void Start()
	{
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x000C4CF8 File Offset: 0x000C2EF8
	private void Update()
	{
	}

	// Token: 0x04001D45 RID: 7493
	private const int WAIT_SEND_MESSAGE_WAIT = 30;

	// Token: 0x04001D46 RID: 7494
	private ButtonInfoTable.PageType m_currentPageType;

	// Token: 0x04001D47 RID: 7495
	private MsgMenuSequence.SequeneceType m_current_sequence_type;

	// Token: 0x04001D48 RID: 7496
	private ButtonInfoTable m_info_table = new ButtonInfoTable();

	// Token: 0x04001D49 RID: 7497
	private GameObject m_menu_anim_obj;

	// Token: 0x04001D4A RID: 7498
	private ButtonEventResourceLoader m_resLoader;

	// Token: 0x04001D4B RID: 7499
	private ButtonEventBackButton m_backButton;

	// Token: 0x04001D4C RID: 7500
	private ButtonEventPageHistory m_pageHistory;

	// Token: 0x04001D4D RID: 7501
	private HudDisplay m_hud_display;

	// Token: 0x04001D4E RID: 7502
	private ButtonEventAnimation m_animation;

	// Token: 0x04001D4F RID: 7503
	private bool m_transform;

	// Token: 0x04001D50 RID: 7504
	private ButtonEventPageControl.ResourceLoadedCallback m_resourceLoadedCallback;

	// Token: 0x02000437 RID: 1079
	private class WaitSendMessageParam
	{
		// Token: 0x04001D51 RID: 7505
		public Component m_component;

		// Token: 0x04001D52 RID: 7506
		public int m_waitCount;

		// Token: 0x04001D53 RID: 7507
		public string m_methodName;

		// Token: 0x04001D54 RID: 7508
		public object m_value;
	}

	// Token: 0x02000A90 RID: 2704
	// (Invoke) Token: 0x0600487A RID: 18554
	public delegate void ResourceLoadedCallback();
}
