using System;
using Message;
using UnityEngine;

// Token: 0x0200043B RID: 1083
public class ButtonInfoTable
{
	// Token: 0x060020F3 RID: 8435 RVA: 0x000C5424 File Offset: 0x000C3624
	public ButtonInfoTable()
	{
		ButtonInfoTable.MessageInfo[] array = new ButtonInfoTable.MessageInfo[23];
		array[1] = new ButtonInfoTable.MessageInfo("ChaoSetUI", "OnStartChaoSet", "ChaoSetUI", true);
		array[3] = new ButtonInfoTable.MessageInfo("InformationUI", "OnStartInformation", "InformationUI", true);
		array[6] = new ButtonInfoTable.MessageInfo("OptionUI", "OnStartOptionUI", "OptionUI", true);
		array[7] = new ButtonInfoTable.MessageInfo("PlayerSet_3_UI", "Setup", "PlayerCharaList", true);
		array[8] = new ButtonInfoTable.MessageInfo("PlayerSet_2_UI", "StartSubCharacter", "MenuPlayerSet", true);
		array[9] = new ButtonInfoTable.MessageInfo("PresentBoxUI", "OnStartPresentBox", "PresentBoxUI", true);
		array[11] = new ButtonInfoTable.MessageInfo("DailyInfoUI", "Setup", "DailyInfo", true);
		array[12] = new ButtonInfoTable.MessageInfo("RouletteTopUI", "OnRouletteOpenDefault", "RouletteTop", true);
		array[13] = new ButtonInfoTable.MessageInfo("ShopUI2", "OnStartShopRedStarRing", "ShopUI", true);
		array[14] = new ButtonInfoTable.MessageInfo("ShopUI2", "OnStartShopRing", "ShopUI", true);
		array[15] = new ButtonInfoTable.MessageInfo("ShopUI2", "OnStartShopChallenge", "ShopUI", true);
		array[16] = new ButtonInfoTable.MessageInfo("ShopUI2", "OnStartShopEvent", "ShopUI", true);
		array[17] = new ButtonInfoTable.MessageInfo("ui_mm_mileage2_page", "OnStartMileage", "ui_mm_mileage_page", true);
		array[19] = new ButtonInfoTable.MessageInfo("ui_mm_ranking_page", "SetDisplayEndlessModeOn", "RankingUI", true);
		array[21] = new ButtonInfoTable.MessageInfo("ui_mm_ranking_page", "SetDisplayQuickModeOn", "RankingUI", true);
		this.m_msgInfosForPages = array;
		ButtonInfoTable.MessageInfo[] array2 = new ButtonInfoTable.MessageInfo[23];
		array2[1] = new ButtonInfoTable.MessageInfo("ChaoSetUI", "OnMsgMenuBack", "ChaoSetUI", true);
		array2[3] = new ButtonInfoTable.MessageInfo("InformationUI", "OnEndInformation", "InformationUI", true);
		array2[4] = new ButtonInfoTable.MessageInfo("ItemSet_3_UI", "OnMsgMenuBack", "ItemSetMenu", true);
		array2[6] = new ButtonInfoTable.MessageInfo("MainMenuButtonEvent", "OnOptionBackButtonClicked", string.Empty, false);
		array2[9] = new ButtonInfoTable.MessageInfo("PresentBoxUI", "OnEndPresentBox", "PresentBoxUI", true);
		array2[11] = new ButtonInfoTable.MessageInfo("DailyInfoUI", "OnClickBackButton", "DailyInfo", true);
		array2[12] = new ButtonInfoTable.MessageInfo("RouletteTopUI", "OnRouletteEnd", "RouletteTop", true);
		array2[13] = new ButtonInfoTable.MessageInfo("ShopUI2", "OnShopBackButtonClicked", "ShopUI", true);
		array2[14] = new ButtonInfoTable.MessageInfo("ShopUI2", "OnShopBackButtonClicked", "ShopUI", true);
		array2[15] = new ButtonInfoTable.MessageInfo("ShopUI2", "OnShopBackButtonClicked", "ShopUI", true);
		array2[16] = new ButtonInfoTable.MessageInfo("ShopUI2", "OnShopBackButtonClicked", "ShopUI", true);
		array2[17] = new ButtonInfoTable.MessageInfo("ui_mm_mileage2_page", "OnEndMileage", "ui_mm_mileage_page", true);
		array2[19] = new ButtonInfoTable.MessageInfo("ui_mm_ranking_page", "SetDisplayEndlessModeOff", "RankingUI", true);
		array2[21] = new ButtonInfoTable.MessageInfo("ui_mm_ranking_page", "SetDisplayQuickModeOff", "RankingUI", true);
		this.m_msgInfosForEndPages = array2;
		this.m_button_info = new ButtonInfoTable.ButtonInfo[]
		{
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.PRESENT_BOX, ButtonInfoTable.PageType.PRESENT_BOX, "MainMenuUI4/Anchor_7_BL/Btn_2_presentbox", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.DAILY_CHALLENGE, ButtonInfoTable.PageType.DAILY_CHALLENGE, "MainMenuUI4/Anchor_9_BR/Btn_1_challenge", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.DAILY_BATTLE, ButtonInfoTable.PageType.DAILY_BATTLE, "MainMenuUI4/Anchor_5_MC/1_Quick/Btn_2_battle", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.CHARA_MAIN, ButtonInfoTable.PageType.PLAYER_MAIN, "MainMenuUI4/Anchor_5_MC/2_Character/Btn_2_player", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.CHARA_MAIN, ButtonInfoTable.PageType.PLAYER_SUB, "MainMenuUI4/Anchor_5_MC/mainmenu_contents/grid/page_3/slot/ui_mm_main2_page(Clone)/player_set/Btn_player_sub", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.CHAO, ButtonInfoTable.PageType.CHAO, "MainMenuUI4/Anchor_5_MC/2_Character/Btn_1_chao", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.PLAY_ITEM, ButtonInfoTable.PageType.ITEM),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.STAGE_CHECK, ButtonInfoTable.PageType.NON),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.OPTION, ButtonInfoTable.PageType.OPTION, "MainMenuUI4/Anchor_7_BL/Btn_1_Option", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.INFOMATION, ButtonInfoTable.PageType.INFOMATION, "MainMenuUI4/Anchor_7_BL/Btn_0_info", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.ROULETTE, ButtonInfoTable.PageType.ROULETTE, "MainMenuUI4/Anchor_8_BC/Btn_roulette", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.CHAO_ROULETTE, ButtonInfoTable.PageType.ROULETTE),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.CHAO_ROULETTE, ButtonInfoTable.PageType.ROULETTE),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.ITEM_ROULETTE, ButtonInfoTable.PageType.ROULETTE),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.CHAO_ROULETTE, ButtonInfoTable.PageType.ROULETTE, "ChaoSetUIPage/ChaoSetUI/Anchor_7_BL/mainmenu_btn_1c/Btn_roulette", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.EPISODE, ButtonInfoTable.PageType.EPISODE, "MainMenuUI4/Anchor_5_MC/0_Endless/Btn_2_mileage", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.MAIN_PLAY_BUTTON, ButtonInfoTable.PageType.EPISODE_PLAY, "MainMenuUI4/Anchor_5_MC/0_Endless/Btn_3_play", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.EPISODE_RANKING, ButtonInfoTable.PageType.EPISODE_RANKING, "MainMenuUI4/Anchor_5_MC/0_Endless/Btn_1_ranking", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.QUICK, ButtonInfoTable.PageType.QUICK, "MainMenuUI4/Anchor_5_MC/1_Quick/Btn_3_play", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.QUICK_RANKING, ButtonInfoTable.PageType.QUICK_RANKING, "MainMenuUI4/Anchor_5_MC/1_Quick/Btn_1_ranking", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.PLAY_AT_EPISODE_PAGE, ButtonInfoTable.PageType.PLAY_AT_EPISODE_PAGE, "ui_mm_mileage2_page/Anchor_9_BR/Btn_play", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.EVENT_TOP, ButtonInfoTable.PageType.EVENT, string.Empty, "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "PresentBoxUI/Anchor_7_BL/mainmenu_btn_1b/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, string.Empty, "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "DailyInfoUI/Anchor_7_BL/mainmenu_btn_1b/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "PlayerSet_3_UI/Anchor_7_BL/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ItemSet_3_UI/Anchor_7_BL/mainmenu_btn_1b/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ChaoSetUIPage/ChaoSetUI/Anchor_7_BL/mainmenu_btn_1c/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ShopPage/ShopUI2/Anchor_7_BL/mainmenu_btn_1b/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ShopPage/ShopUI2/Anchor_7_BL/mainmenu_btn_1b/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ui_mm_mileage2_page/Anchor_7_BL/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ui_mm_ranking_page/Anchor_7_BL/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ShopPage/ShopUI2/Anchor_7_BL/mainmenu_btn_1b/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ui_mm_ranking_page/Anchor_7_BL/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "ShopPage/ShopUI2/Anchor_7_BL/mainmenu_btn_1b/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "InformationUI/Anchor_7_BL/mainmenu_btn_1b/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "RouletteUI/Anchor_7_BL/roulette_btn_2/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, "OptionUI/Anchor_7_BL/option_btn/Btn_cmn_back", "sys_menu_decide"),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.SHOP, ButtonInfoTable.PageType.SHOP_RSR, "MainMenuCmnUI/Anchor_3_TR/mainmenu_info_quantum/Btn_shop/Btn_charge_rsring", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.SHOP, ButtonInfoTable.PageType.SHOP_RING, "MainMenuCmnUI/Anchor_3_TR/mainmenu_info_quantum/Btn_shop/Btn_charge_stock", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.SHOP, ButtonInfoTable.PageType.SHOP_ENERGY, "MainMenuCmnUI/Anchor_3_TR/mainmenu_info_quantum/Btn_shop/Btn_charge_challenge", "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.SHOP, ButtonInfoTable.PageType.SHOP_EVENT, string.Empty, "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.EVENT_RAID, ButtonInfoTable.PageType.EVENT, string.Empty, "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.EVENT_SPECIAL, ButtonInfoTable.PageType.EVENT, string.Empty, "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.EVENT_COLLECT, ButtonInfoTable.PageType.EVENT, string.Empty, "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, ButtonInfoTable.PageType.NON, string.Empty, "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.MAIN, ButtonInfoTable.PageType.MAIN, string.Empty, "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.BACK, ButtonInfoTable.PageType.NON, string.Empty, "sys_menu_decide", null),
			new ButtonInfoTable.ButtonInfo(MsgMenuSequence.SequeneceType.STAGE, ButtonInfoTable.PageType.STAGE, string.Empty, "sys_menu_decide", null)
		};
		base..ctor();
	}

	// Token: 0x060020F5 RID: 8437 RVA: 0x000C5DA0 File Offset: 0x000C3FA0
	public void PlaySE(ButtonInfoTable.ButtonType button_type)
	{
		if (button_type < ButtonInfoTable.ButtonType.NUM && !string.IsNullOrEmpty(this.m_button_info[(int)button_type].seName))
		{
			SoundManager.SePlay(this.m_button_info[(int)button_type].seName, "SE");
		}
	}

	// Token: 0x060020F6 RID: 8438 RVA: 0x000C5DDC File Offset: 0x000C3FDC
	public ButtonInfoTable.PageType GetPageType(ButtonInfoTable.ButtonType button_type)
	{
		if (button_type < ButtonInfoTable.ButtonType.NUM)
		{
			return this.m_button_info[(int)button_type].nextPageType;
		}
		return ButtonInfoTable.PageType.NON;
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x000C5DF8 File Offset: 0x000C3FF8
	public MsgMenuSequence.SequeneceType GetSequeneceType(ButtonInfoTable.ButtonType button_type)
	{
		if (button_type < ButtonInfoTable.ButtonType.NUM)
		{
			return this.m_button_info[(int)button_type].nextMenuId;
		}
		return MsgMenuSequence.SequeneceType.NON;
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x000C5E14 File Offset: 0x000C4014
	public MsgMenuSequence.SequeneceType GetSequeneceType(ButtonInfoTable.PageType pageType)
	{
		if (pageType != ButtonInfoTable.PageType.NON && pageType < ButtonInfoTable.PageType.NUM)
		{
			return this.m_sequences[(int)pageType];
		}
		return MsgMenuSequence.SequeneceType.NON;
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x000C5E30 File Offset: 0x000C4030
	public ButtonInfoTable.AnimInfo GetPageAnimInfo(ButtonInfoTable.PageType pageType)
	{
		if (pageType != ButtonInfoTable.PageType.NON && pageType < ButtonInfoTable.PageType.NUM)
		{
			return ButtonInfoTable.m_animInfos[(int)pageType];
		}
		return null;
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x000C5E4C File Offset: 0x000C404C
	public ButtonInfoTable.MessageInfo GetPageMessageInfo(ButtonInfoTable.PageType page, bool start)
	{
		if (page == ButtonInfoTable.PageType.NON || page >= ButtonInfoTable.PageType.NUM)
		{
			return null;
		}
		if (start)
		{
			return this.m_msgInfosForPages[(int)page];
		}
		return this.m_msgInfosForEndPages[(int)page];
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x000C5E84 File Offset: 0x000C4084
	public GameObject GetDisplayObj(ButtonInfoTable.PageType nextPageType)
	{
		if (nextPageType != ButtonInfoTable.PageType.NON && nextPageType < ButtonInfoTable.PageType.NUM && ButtonInfoTable.m_animInfos[(int)nextPageType] != null)
		{
			GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
			if (cameraUIObject != null)
			{
				return GameObjectUtil.FindChildGameObject(cameraUIObject, ButtonInfoTable.m_animInfos[(int)nextPageType].targetName);
			}
		}
		return null;
	}

	// Token: 0x04001D5A RID: 7514
	public readonly MsgMenuSequence.SequeneceType[] m_sequences = new MsgMenuSequence.SequeneceType[]
	{
		MsgMenuSequence.SequeneceType.MAIN,
		MsgMenuSequence.SequeneceType.CHAO,
		MsgMenuSequence.SequeneceType.EVENT_TOP,
		MsgMenuSequence.SequeneceType.INFOMATION,
		MsgMenuSequence.SequeneceType.PLAY_ITEM,
		MsgMenuSequence.SequeneceType.NON,
		MsgMenuSequence.SequeneceType.OPTION,
		MsgMenuSequence.SequeneceType.CHARA_MAIN,
		MsgMenuSequence.SequeneceType.CHARA_MAIN,
		MsgMenuSequence.SequeneceType.PRESENT_BOX,
		MsgMenuSequence.SequeneceType.DAILY_CHALLENGE,
		MsgMenuSequence.SequeneceType.DAILY_BATTLE,
		MsgMenuSequence.SequeneceType.ROULETTE,
		MsgMenuSequence.SequeneceType.SHOP,
		MsgMenuSequence.SequeneceType.SHOP,
		MsgMenuSequence.SequeneceType.SHOP,
		MsgMenuSequence.SequeneceType.SHOP,
		MsgMenuSequence.SequeneceType.EPISODE,
		MsgMenuSequence.SequeneceType.EPISODE_PLAY,
		MsgMenuSequence.SequeneceType.EPISODE_RANKING,
		MsgMenuSequence.SequeneceType.QUICK,
		MsgMenuSequence.SequeneceType.QUICK_RANKING,
		MsgMenuSequence.SequeneceType.PLAY_AT_EPISODE_PAGE
	};

	// Token: 0x04001D5B RID: 7515
	public readonly ButtonInfoTable.ButtonType[] m_platformBackButtonType = new ButtonInfoTable.ButtonType[]
	{
		ButtonInfoTable.ButtonType.TITLE_BACK,
		ButtonInfoTable.ButtonType.CHAO_BACK,
		ButtonInfoTable.ButtonType.EVENT_BACK,
		ButtonInfoTable.ButtonType.INFOMATION_BACK,
		ButtonInfoTable.ButtonType.ITEM_BACK,
		ButtonInfoTable.ButtonType.ITEM_BACK,
		ButtonInfoTable.ButtonType.OPTION_BACK,
		ButtonInfoTable.ButtonType.CHARA_BACK,
		ButtonInfoTable.ButtonType.CHARA_BACK,
		ButtonInfoTable.ButtonType.PRESENT_BOX_BACK,
		ButtonInfoTable.ButtonType.DAILY_CHALLENGE_BACK,
		ButtonInfoTable.ButtonType.DAILY_BATTLE_BACK,
		ButtonInfoTable.ButtonType.ROULETTE_BACK,
		ButtonInfoTable.ButtonType.SHOP_BACK,
		ButtonInfoTable.ButtonType.SHOP_BACK,
		ButtonInfoTable.ButtonType.SHOP_BACK,
		ButtonInfoTable.ButtonType.SHOP_BACK,
		ButtonInfoTable.ButtonType.EPISODE_BACK,
		ButtonInfoTable.ButtonType.EPISODE_PLAY_BACK,
		ButtonInfoTable.ButtonType.EPISODE_RANKING_BACK,
		ButtonInfoTable.ButtonType.QUICK_BACK,
		ButtonInfoTable.ButtonType.QUICK_RANKING_BACK,
		ButtonInfoTable.ButtonType.PLAY_AT_EPISODE_PAGE_BACK
	};

	// Token: 0x04001D5C RID: 7516
	public static readonly ButtonInfoTable.AnimInfo[] m_animInfos = new ButtonInfoTable.AnimInfo[]
	{
		new ButtonInfoTable.AnimInfo("MainMenuUI4", "ui_mm_Anim"),
		new ButtonInfoTable.AnimInfo("ChaoSetUI", "ui_mm_chao_Anim"),
		null,
		new ButtonInfoTable.AnimInfo("InformationUI", "ui_daily_challenge_infomation_intro_Anim"),
		new ButtonInfoTable.AnimInfo("ItemSet_3_UI", "ui_itemset_3_intro_Anim"),
		null,
		new ButtonInfoTable.AnimInfo("OptionUI", "ui_menu_option_intro_Anim"),
		new ButtonInfoTable.AnimInfo("PlayerSet_3_UI", "ui_mm_player_set_2_intro_Anim"),
		new ButtonInfoTable.AnimInfo("PlayerSet_2_UI", "ui_mm_player_set_2_intro_Anim"),
		new ButtonInfoTable.AnimInfo("PresentBoxUI", "ui_menu_presentbox_intro_Anim"),
		null,
		new ButtonInfoTable.AnimInfo("DailyInfoUI", "ui_daily_challenge_infomation_intro_Anim"),
		new ButtonInfoTable.AnimInfo("RouletteTopUI", null),
		new ButtonInfoTable.AnimInfo("ShopUI2", "ui_mm_shop_intro_Anim"),
		new ButtonInfoTable.AnimInfo("ShopUI2", "ui_mm_shop_intro_Anim"),
		new ButtonInfoTable.AnimInfo("ShopUI2", "ui_mm_shop_intro_Anim"),
		new ButtonInfoTable.AnimInfo("ShopUI2", "ui_mm_shop_intro_Anim"),
		null,
		new ButtonInfoTable.AnimInfo("ItemSet_3_UI", "ui_itemset_3_intro_Anim"),
		null,
		new ButtonInfoTable.AnimInfo("ItemSet_3_UI", "ui_itemset_3_intro_Anim"),
		null,
		new ButtonInfoTable.AnimInfo("ItemSet_3_UI", "ui_itemset_3_intro_Anim")
	};

	// Token: 0x04001D5D RID: 7517
	public readonly ButtonInfoTable.MessageInfo[] m_msgInfosForPages;

	// Token: 0x04001D5E RID: 7518
	public readonly ButtonInfoTable.MessageInfo[] m_msgInfosForEndPages;

	// Token: 0x04001D5F RID: 7519
	public readonly ButtonInfoTable.ButtonInfo[] m_button_info;

	// Token: 0x0200043C RID: 1084
	public enum PageType
	{
		// Token: 0x04001D61 RID: 7521
		MAIN,
		// Token: 0x04001D62 RID: 7522
		CHAO,
		// Token: 0x04001D63 RID: 7523
		EVENT,
		// Token: 0x04001D64 RID: 7524
		INFOMATION,
		// Token: 0x04001D65 RID: 7525
		ITEM,
		// Token: 0x04001D66 RID: 7526
		STAGE,
		// Token: 0x04001D67 RID: 7527
		OPTION,
		// Token: 0x04001D68 RID: 7528
		PLAYER_MAIN,
		// Token: 0x04001D69 RID: 7529
		PLAYER_SUB,
		// Token: 0x04001D6A RID: 7530
		PRESENT_BOX,
		// Token: 0x04001D6B RID: 7531
		DAILY_CHALLENGE,
		// Token: 0x04001D6C RID: 7532
		DAILY_BATTLE,
		// Token: 0x04001D6D RID: 7533
		ROULETTE,
		// Token: 0x04001D6E RID: 7534
		SHOP_RSR,
		// Token: 0x04001D6F RID: 7535
		SHOP_RING,
		// Token: 0x04001D70 RID: 7536
		SHOP_ENERGY,
		// Token: 0x04001D71 RID: 7537
		SHOP_EVENT,
		// Token: 0x04001D72 RID: 7538
		EPISODE,
		// Token: 0x04001D73 RID: 7539
		EPISODE_PLAY,
		// Token: 0x04001D74 RID: 7540
		EPISODE_RANKING,
		// Token: 0x04001D75 RID: 7541
		QUICK,
		// Token: 0x04001D76 RID: 7542
		QUICK_RANKING,
		// Token: 0x04001D77 RID: 7543
		PLAY_AT_EPISODE_PAGE,
		// Token: 0x04001D78 RID: 7544
		NUM,
		// Token: 0x04001D79 RID: 7545
		NON = -1
	}

	// Token: 0x0200043D RID: 1085
	public class AnimInfo
	{
		// Token: 0x060020FC RID: 8444 RVA: 0x000C5ED4 File Offset: 0x000C40D4
		public AnimInfo(string targetName, string animName)
		{
			this.targetName = targetName;
			this.animName = animName;
		}

		// Token: 0x04001D7A RID: 7546
		public string animName;

		// Token: 0x04001D7B RID: 7547
		public string targetName;
	}

	// Token: 0x0200043E RID: 1086
	public class MessageInfo
	{
		// Token: 0x060020FD RID: 8445 RVA: 0x000C5EEC File Offset: 0x000C40EC
		public MessageInfo(string target, string method, string component, bool ui = true)
		{
			this.targetName = target;
			this.methodName = method;
			this.componentName = component;
			this.uiFlag = ui;
		}

		// Token: 0x04001D7C RID: 7548
		public string targetName;

		// Token: 0x04001D7D RID: 7549
		public string methodName;

		// Token: 0x04001D7E RID: 7550
		public string componentName;

		// Token: 0x04001D7F RID: 7551
		public bool uiFlag;
	}

	// Token: 0x0200043F RID: 1087
	public class ButtonInfo
	{
		// Token: 0x060020FE RID: 8446 RVA: 0x000C5F14 File Offset: 0x000C4114
		public ButtonInfo(MsgMenuSequence.SequeneceType menuId, ButtonInfoTable.PageType pageType, string path, string se, ButtonInfoTable.MessageInfo info = null)
		{
			this.nextMenuId = menuId;
			this.nextPageType = pageType;
			this.btnMsgInfo = info;
			this.clickButtonPath = path;
			this.seName = se;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000C5F70 File Offset: 0x000C4170
		public ButtonInfo(MsgMenuSequence.SequeneceType menuId, string path, string se)
		{
			this.nextMenuId = menuId;
			this.nextPageType = ButtonInfoTable.PageType.NON;
			this.btnMsgInfo = null;
			this.clickButtonPath = path;
			this.seName = se;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000C5FCC File Offset: 0x000C41CC
		public ButtonInfo(MsgMenuSequence.SequeneceType menuId, ButtonInfoTable.PageType pageType)
		{
			this.nextMenuId = menuId;
			this.nextPageType = pageType;
			this.btnMsgInfo = null;
			this.clickButtonPath = string.Empty;
			this.seName = string.Empty;
		}

		// Token: 0x04001D80 RID: 7552
		public MsgMenuSequence.SequeneceType nextMenuId = MsgMenuSequence.SequeneceType.NON;

		// Token: 0x04001D81 RID: 7553
		public ButtonInfoTable.PageType nextPageType = ButtonInfoTable.PageType.NON;

		// Token: 0x04001D82 RID: 7554
		public ButtonInfoTable.MessageInfo btnMsgInfo;

		// Token: 0x04001D83 RID: 7555
		public string clickButtonPath = string.Empty;

		// Token: 0x04001D84 RID: 7556
		public string seName = string.Empty;
	}

	// Token: 0x02000440 RID: 1088
	public enum ButtonType
	{
		// Token: 0x04001D86 RID: 7558
		PRESENT_BOX,
		// Token: 0x04001D87 RID: 7559
		DAILY_CHALLENGE,
		// Token: 0x04001D88 RID: 7560
		DAILY_BATTLE,
		// Token: 0x04001D89 RID: 7561
		CHARA_MAIN,
		// Token: 0x04001D8A RID: 7562
		CHARA_SUB,
		// Token: 0x04001D8B RID: 7563
		CHAO,
		// Token: 0x04001D8C RID: 7564
		VIRTUAL_NEW_ITEM,
		// Token: 0x04001D8D RID: 7565
		PLAY_ITEM,
		// Token: 0x04001D8E RID: 7566
		OPTION,
		// Token: 0x04001D8F RID: 7567
		INFOMATION,
		// Token: 0x04001D90 RID: 7568
		ROULETTE,
		// Token: 0x04001D91 RID: 7569
		CHAO_ROULETTE,
		// Token: 0x04001D92 RID: 7570
		REWARDLIST_TO_CHAO_ROULETTE,
		// Token: 0x04001D93 RID: 7571
		ITEM_ROULETTE,
		// Token: 0x04001D94 RID: 7572
		CHAO_TO_ROULETTE,
		// Token: 0x04001D95 RID: 7573
		EPISODE,
		// Token: 0x04001D96 RID: 7574
		EPISODE_PLAY,
		// Token: 0x04001D97 RID: 7575
		EPISODE_RANKING,
		// Token: 0x04001D98 RID: 7576
		QUICK,
		// Token: 0x04001D99 RID: 7577
		QUICK_RANKING,
		// Token: 0x04001D9A RID: 7578
		PLAY_AT_EPISODE_PAGE,
		// Token: 0x04001D9B RID: 7579
		PLAY_EVENT,
		// Token: 0x04001D9C RID: 7580
		PRESENT_BOX_BACK,
		// Token: 0x04001D9D RID: 7581
		DAILY_CHALLENGE_BACK,
		// Token: 0x04001D9E RID: 7582
		DAILY_BATTLE_BACK,
		// Token: 0x04001D9F RID: 7583
		CHARA_BACK,
		// Token: 0x04001DA0 RID: 7584
		ITEM_BACK,
		// Token: 0x04001DA1 RID: 7585
		CHAO_BACK,
		// Token: 0x04001DA2 RID: 7586
		SHOP_BACK,
		// Token: 0x04001DA3 RID: 7587
		EPISODE_BACK,
		// Token: 0x04001DA4 RID: 7588
		EPISODE_PLAY_BACK,
		// Token: 0x04001DA5 RID: 7589
		EPISODE_RANKING_BACK,
		// Token: 0x04001DA6 RID: 7590
		QUICK_BACK,
		// Token: 0x04001DA7 RID: 7591
		QUICK_RANKING_BACK,
		// Token: 0x04001DA8 RID: 7592
		PLAY_AT_EPISODE_PAGE_BACK,
		// Token: 0x04001DA9 RID: 7593
		INFOMATION_BACK,
		// Token: 0x04001DAA RID: 7594
		ROULETTE_BACK,
		// Token: 0x04001DAB RID: 7595
		OPTION_BACK,
		// Token: 0x04001DAC RID: 7596
		REDSTAR_TO_SHOP,
		// Token: 0x04001DAD RID: 7597
		RING_TO_SHOP,
		// Token: 0x04001DAE RID: 7598
		CHALLENGE_TO_SHOP,
		// Token: 0x04001DAF RID: 7599
		RAIDENERGY_TO_SHOP,
		// Token: 0x04001DB0 RID: 7600
		EVENT_RAID,
		// Token: 0x04001DB1 RID: 7601
		EVENT_SPECIAL,
		// Token: 0x04001DB2 RID: 7602
		EVENT_COLLECT,
		// Token: 0x04001DB3 RID: 7603
		EVENT_BACK,
		// Token: 0x04001DB4 RID: 7604
		FORCE_MAIN_BACK,
		// Token: 0x04001DB5 RID: 7605
		TITLE_BACK,
		// Token: 0x04001DB6 RID: 7606
		GO_STAGE,
		// Token: 0x04001DB7 RID: 7607
		NUM,
		// Token: 0x04001DB8 RID: 7608
		UNKNOWN = -1
	}
}
