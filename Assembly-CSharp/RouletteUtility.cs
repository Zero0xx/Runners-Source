using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x02000A51 RID: 2641
public class RouletteUtility
{
	// Token: 0x17000996 RID: 2454
	// (get) Token: 0x0600472B RID: 18219 RVA: 0x00175E74 File Offset: 0x00174074
	// (set) Token: 0x0600472C RID: 18220 RVA: 0x00175E7C File Offset: 0x0017407C
	public static RouletteCategory rouletteDefault
	{
		get
		{
			return RouletteUtility.s_rouletteDefault;
		}
		set
		{
			RouletteUtility.s_rouletteDefault = value;
		}
	}

	// Token: 0x17000997 RID: 2455
	// (get) Token: 0x0600472D RID: 18221 RVA: 0x00175E84 File Offset: 0x00174084
	// (set) Token: 0x0600472E RID: 18222 RVA: 0x00175E8C File Offset: 0x0017408C
	public static bool loginRoulette
	{
		get
		{
			return RouletteUtility.s_loginRoulette;
		}
		set
		{
			RouletteUtility.s_loginRoulette = value;
		}
	}

	// Token: 0x17000998 RID: 2456
	// (get) Token: 0x0600472F RID: 18223 RVA: 0x00175E94 File Offset: 0x00174094
	public static string jackpotFeedText
	{
		get
		{
			return RouletteUtility.s_jackpotFeedText;
		}
	}

	// Token: 0x17000999 RID: 2457
	// (get) Token: 0x06004730 RID: 18224 RVA: 0x00175E9C File Offset: 0x0017409C
	// (set) Token: 0x06004731 RID: 18225 RVA: 0x00175EA4 File Offset: 0x001740A4
	public static bool rouletteTurtorialEnd
	{
		get
		{
			return RouletteUtility.s_rouletteTurtorialEnd;
		}
		set
		{
			RouletteUtility.s_rouletteTurtorial = false;
			RouletteUtility.s_rouletteTurtorialEnd = value;
			if (!RouletteUtility.s_rouletteTurtorialEnd)
			{
				RouletteUtility.s_rouletteTurtorialLock = true;
			}
		}
	}

	// Token: 0x1700099A RID: 2458
	// (get) Token: 0x06004732 RID: 18226 RVA: 0x00175EC4 File Offset: 0x001740C4
	public static bool isTutorial
	{
		get
		{
			bool flag = false;
			if (RouletteUtility.s_rouletteTurtorialLock)
			{
				return false;
			}
			if (RouletteUtility.s_rouletteTurtorial)
			{
				flag = true;
			}
			else if (ServerInterface.ChaoWheelOptions != null && !RouletteUtility.s_rouletteTurtorialEnd)
			{
				flag = ServerInterface.ChaoWheelOptions.IsTutorial;
				if (flag)
				{
					RouletteUtility.s_rouletteTurtorial = true;
				}
			}
			return flag;
		}
	}

	// Token: 0x06004733 RID: 18227 RVA: 0x00175F1C File Offset: 0x0017411C
	public static RouletteTicketCategory GetRouletteTicketCategory(int itemId)
	{
		RouletteTicketCategory result = RouletteTicketCategory.NONE;
		if (itemId > 229999 && itemId <= 299999)
		{
			if (itemId >= 230000 && itemId < 240000)
			{
				result = RouletteTicketCategory.PREMIUM;
			}
			else if (itemId >= 240000 && itemId < 250000)
			{
				result = RouletteTicketCategory.ITEM;
			}
			else if (itemId >= 250000 && itemId < 260000)
			{
				result = RouletteTicketCategory.RAID;
			}
			else if (itemId >= 260000 && itemId < 270000)
			{
				result = RouletteTicketCategory.EVENT;
			}
		}
		return result;
	}

	// Token: 0x06004734 RID: 18228 RVA: 0x00175FB4 File Offset: 0x001741B4
	public static bool SetItemRouletteUseRing(bool use)
	{
		return false;
	}

	// Token: 0x06004735 RID: 18229 RVA: 0x00175FC4 File Offset: 0x001741C4
	public static string GetRouletteCostItemName(int costItemId)
	{
		string text = "ui_cmn_icon_item_{ID}";
		return text.Replace("{ID}", costItemId.ToString());
	}

	// Token: 0x06004736 RID: 18230 RVA: 0x00175FEC File Offset: 0x001741EC
	public static string GetRouletteColorName(RouletteUtility.RouletteColor rcolor)
	{
		string result = null;
		switch (rcolor)
		{
		case RouletteUtility.RouletteColor.Blue:
			result = "blu";
			break;
		case RouletteUtility.RouletteColor.Purple:
			result = "pur";
			break;
		case RouletteUtility.RouletteColor.Green:
			result = "gre";
			break;
		case RouletteUtility.RouletteColor.Silver:
			result = "sil";
			break;
		case RouletteUtility.RouletteColor.Gold:
			result = "gol";
			break;
		}
		return result;
	}

	// Token: 0x06004737 RID: 18231 RVA: 0x00176058 File Offset: 0x00174258
	public static RouletteUtility.WheelRank GetRouletteRank(int rank)
	{
		RouletteUtility.WheelRank result = RouletteUtility.WheelRank.Normal;
		switch (rank % 100)
		{
		case 0:
			result = RouletteUtility.WheelRank.Normal;
			break;
		case 1:
			result = RouletteUtility.WheelRank.Big;
			break;
		case 2:
			result = RouletteUtility.WheelRank.Super;
			break;
		}
		return result;
	}

	// Token: 0x06004738 RID: 18232 RVA: 0x0017609C File Offset: 0x0017429C
	public static string GetRouletteChangeIconSpriteName(RouletteCategory category)
	{
		string text = "ui_roulette_pager_icon_{CATEGORY}";
		int num = (int)category;
		return text.Replace("{CATEGORY}", num.ToString());
	}

	// Token: 0x06004739 RID: 18233 RVA: 0x001760C8 File Offset: 0x001742C8
	public static string GetRouletteBgSpriteName(ServerWheelOptionsGeneral wheel)
	{
		string text = "ui_roulette_tablebg_{COLOR}";
		string rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Green);
		RouletteUtility.WheelType type = wheel.type;
		if (type != RouletteUtility.WheelType.Normal)
		{
			if (type == RouletteUtility.WheelType.Rankup)
			{
				rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Green);
			}
		}
		else
		{
			rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Blue);
		}
		return text.Replace("{COLOR}", rouletteColorName);
	}

	// Token: 0x0600473A RID: 18234 RVA: 0x00176124 File Offset: 0x00174324
	public static string GetRouletteBoardSpriteName(ServerWheelOptionsGeneral wheel)
	{
		RouletteCategory rouletteCategory = RouletteUtility.GetRouletteCategory(wheel);
		string text = "ui_roulette_table_{COLOR}_{TYPE}";
		string rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Green);
		int patternType = wheel.patternType;
		string str = string.Empty;
		RouletteUtility.WheelRank rank = wheel.rank;
		RouletteUtility.WheelType type = wheel.type;
		if (type != RouletteUtility.WheelType.Normal)
		{
			if (type == RouletteUtility.WheelType.Rankup)
			{
				switch (rank)
				{
				case RouletteUtility.WheelRank.Normal:
					rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Green);
					break;
				case RouletteUtility.WheelRank.Big:
					rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Silver);
					break;
				case RouletteUtility.WheelRank.Super:
					rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Gold);
					str = "r";
					break;
				}
			}
		}
		else if (rouletteCategory != RouletteCategory.SPECIAL)
		{
			rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Silver);
		}
		else
		{
			rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Gold);
		}
		text = text.Replace("{COLOR}", rouletteColorName);
		text = text.Replace("{TYPE}", patternType.ToString());
		return text + str;
	}

	// Token: 0x0600473B RID: 18235 RVA: 0x00176210 File Offset: 0x00174410
	public static string GetRouletteArrowSpriteName(ServerWheelOptionsGeneral wheel)
	{
		string text = "ui_roulette_arrow_{COLOR}";
		string rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Silver);
		RouletteUtility.WheelType type = wheel.type;
		if (type != RouletteUtility.WheelType.Normal)
		{
			if (type == RouletteUtility.WheelType.Rankup)
			{
				if (wheel.rank == RouletteUtility.WheelRank.Super)
				{
					rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Gold);
				}
				else
				{
					rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Silver);
				}
			}
		}
		else if (wheel.rank == RouletteUtility.WheelRank.Normal)
		{
			rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Silver);
		}
		else
		{
			rouletteColorName = RouletteUtility.GetRouletteColorName(RouletteUtility.RouletteColor.Gold);
		}
		return text.Replace("{COLOR}", rouletteColorName);
	}

	// Token: 0x0600473C RID: 18236 RVA: 0x0017629C File Offset: 0x0017449C
	public static RouletteCategory GetRouletteCategory(ServerWheelOptionsGeneral wheel)
	{
		RouletteCategory result = RouletteCategory.NONE;
		if (wheel != null && wheel.rouletteId >= 0)
		{
			result = RouletteCategory.RAID;
		}
		return result;
	}

	// Token: 0x0600473D RID: 18237 RVA: 0x001762C0 File Offset: 0x001744C0
	public static bool ChangeRouletteHeader(RouletteCategory category)
	{
		bool result = false;
		if (category != RouletteCategory.ALL)
		{
			string rouletteCategoryName = RouletteUtility.GetRouletteCategoryName(category);
			if (!string.IsNullOrEmpty(rouletteCategoryName))
			{
				string text = "ui_Header_{TYPE}_Roulette";
				text = text.Replace("{TYPE}", rouletteCategoryName);
				HudMenuUtility.SendChangeHeaderText(text);
				result = true;
			}
		}
		else
		{
			string cellName = "ui_Header_Roulette_top";
			HudMenuUtility.SendChangeHeaderText(cellName);
			result = true;
		}
		return result;
	}

	// Token: 0x0600473E RID: 18238 RVA: 0x00176318 File Offset: 0x00174518
	public static string GetRouletteCategoryHeaderText(RouletteCategory category)
	{
		string result = string.Empty;
		string text = string.Empty;
		if (category != RouletteCategory.ALL)
		{
			string rouletteCategoryName = RouletteUtility.GetRouletteCategoryName(category);
			if (!string.IsNullOrEmpty(rouletteCategoryName))
			{
				text = "ui_Header_{TYPE}_Roulette";
				text = text.Replace("{TYPE}", rouletteCategoryName);
			}
		}
		else
		{
			text = "ui_Header_Roulette_top";
		}
		TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", text);
		if (text2 != null)
		{
			result = text2.text;
		}
		return result;
	}

	// Token: 0x0600473F RID: 18239 RVA: 0x00176384 File Offset: 0x00174584
	public static string GetRouletteCategoryName(RouletteCategory category)
	{
		string result = null;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL)
		{
			switch (category)
			{
			case RouletteCategory.PREMIUM:
				result = "Premium";
				break;
			case RouletteCategory.ITEM:
				result = "Item";
				break;
			case RouletteCategory.RAID:
				result = "Raidboss";
				break;
			case RouletteCategory.EVENT:
				result = "Event";
				break;
			case RouletteCategory.SPECIAL:
				result = "Special";
				break;
			}
		}
		return result;
	}

	// Token: 0x06004740 RID: 18240 RVA: 0x00176408 File Offset: 0x00174608
	private static void ShowItem(int id, int num)
	{
		ServerItem serverItem = new ServerItem((ServerItem.Id)id);
		if (serverItem.idType != ServerItem.IdType.ITEM_ROULLETE_WIN)
		{
			ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
			if (itemGetWindow != null)
			{
				itemGetWindow.Create(new ItemGetWindow.CInfo
				{
					name = "ItemGet",
					caption = RouletteUtility.GetText("gw_item_caption", null),
					serverItemId = id,
					imageCount = RouletteUtility.GetText("gw_item_text", "{COUNT}", HudUtility.GetFormatNumString<int>(num))
				});
			}
		}
		else
		{
			RouletteUtility.ShowJackpot(num);
		}
	}

	// Token: 0x06004741 RID: 18241 RVA: 0x00176494 File Offset: 0x00174694
	private static void ShowJackpot(int jackpotRing)
	{
		RouletteManager.isShowGetWindow = true;
		int serverItemId = 910000;
		ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
		if (itemGetWindow != null)
		{
			itemGetWindow.Create(new ItemGetWindow.CInfo
			{
				name = "Jackpot",
				buttonType = ItemGetWindow.ButtonType.TweetCancel,
				caption = RouletteUtility.GetText("gw_jackpot_caption", null),
				serverItemId = serverItemId,
				imageCount = RouletteUtility.GetText("gw_jackpot_text", "{COUNT}", HudUtility.GetFormatNumString<int>(RouletteManager.numJackpotRing))
			});
			RouletteUtility.s_jackpotFeedText = RouletteUtility.GetText("feed_jackpot_text", "{COUNT}", HudUtility.GetFormatNumString<int>(RouletteManager.numJackpotRing));
			RouletteManager.numJackpotRing = jackpotRing;
		}
	}

	// Token: 0x06004742 RID: 18242 RVA: 0x0017653C File Offset: 0x0017473C
	public static void ShowGetWindow(ServerWheelOptions data)
	{
		GameObject x = GameObject.Find("UI Root (2D)");
		if (x != null)
		{
			RouletteManager.isShowGetWindow = true;
			BackKeyManager.InvalidFlag = false;
			int id = data.m_items[data.m_itemWon];
			int num = data.m_itemQuantities[data.m_itemWon];
			ServerItem serverItem = new ServerItem((ServerItem.Id)id);
			if (serverItem.idType == ServerItem.IdType.ITEM_ROULLETE_WIN && data.m_rouletteRank == RouletteUtility.WheelRank.Super)
			{
				RouletteUtility.ShowJackpot(data.m_numJackpotRing);
			}
			else if (serverItem.idType == ServerItem.IdType.CHAO || serverItem.idType == ServerItem.IdType.CHARA)
			{
				ServerChaoState chao = data.GetChao();
				if (chao != null)
				{
					RouletteUtility.ShowOtomo(chao, !data.IsItemList(), data.m_itemList, data.NumRequiredSpEggs, false);
				}
			}
			else
			{
				RouletteUtility.ShowItem(id, num);
			}
		}
	}

	// Token: 0x06004743 RID: 18243 RVA: 0x00176610 File Offset: 0x00174810
	public static void ShowGetWindow(ServerSpinResultGeneral data)
	{
		RouletteManager.isShowGetWindow = false;
		RouletteUtility.s_spinResult = null;
		RouletteUtility.s_spinResultCount = -1;
		GameObject x = GameObject.Find("UI Root (2D)");
		if (x != null)
		{
			global::Debug.Log("ShowGetWindow ItemWon:" + data.ItemWon + " !!!!!!!!");
			if (data.ItemWon >= 0)
			{
				if (data.AcquiredChaoData.Count > 0)
				{
					Dictionary<int, ServerChaoData>.KeyCollection keys = data.AcquiredChaoData.Keys;
					using (Dictionary<int, ServerChaoData>.KeyCollection.Enumerator enumerator = keys.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							int key = enumerator.Current;
							RouletteUtility.ShowOtomo(data.AcquiredChaoData[key], data.IsRequiredChao[key], data.ItemState, data.NumRequiredSpEggs, false);
						}
					}
				}
				else if (data.ItemState.Count > 0)
				{
					Dictionary<int, ServerItemState>.KeyCollection keys2 = data.ItemState.Keys;
					using (Dictionary<int, ServerItemState>.KeyCollection.Enumerator enumerator2 = keys2.GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							int key2 = enumerator2.Current;
							RouletteUtility.ShowItem(data.ItemState[key2].m_itemId, data.ItemState[key2].m_num);
						}
					}
				}
				else
				{
					global::Debug.Log("RouletteUtility ShowGetWindow G  single error?");
				}
			}
			else
			{
				RouletteUtility.s_spinResult = data;
				RouletteUtility.s_spinResultCount = data.GetOtomoAndCharaMax() - 1;
				RouletteManager.isShowGetWindow = false;
				global::Debug.Log("ShowGetWindow ResultCount:" + RouletteUtility.s_spinResultCount + " !!!!!!!!");
				if (RouletteUtility.s_spinResultCount >= 0)
				{
					RouletteManager.isMultiGetWindow = true;
					RouletteUtility.ShowGetAllOtomoAndChara();
				}
				else
				{
					RouletteManager.isMultiGetWindow = false;
					string acquiredListText = data.AcquiredListText;
					if (!string.IsNullOrEmpty(acquiredListText))
					{
						GeneralWindow.Create(new GeneralWindow.CInfo
						{
							name = "RouletteGetAllList",
							buttonType = GeneralWindow.ButtonType.Ok,
							caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "GeneralWindow", "ui_Lbl_get_list").text,
							message = acquiredListText
						});
					}
				}
			}
		}
	}

	// Token: 0x06004744 RID: 18244 RVA: 0x00176874 File Offset: 0x00174A74
	public static bool IsGetOtomoOrCharaWindow()
	{
		bool flag = false;
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			ChaoGetWindow chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(gameObject, "ro_PlayerGetWindowUI");
			if (chaoGetWindow != null && chaoGetWindow.gameObject.activeSelf)
			{
				flag = true;
			}
			if (!flag)
			{
				chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(gameObject, "chao_get_Window");
				if (chaoGetWindow != null && chaoGetWindow.gameObject.activeSelf)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(gameObject, "chao_rare_get_Window");
				if (chaoGetWindow != null && chaoGetWindow.gameObject.activeSelf)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				ChaoMergeWindow chaoMergeWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoMergeWindow>(gameObject, "chao_merge_Window");
				if (chaoMergeWindow != null && chaoMergeWindow.gameObject.activeSelf)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				PlayerMergeWindow playerMergeWindow = GameObjectUtil.FindChildGameObjectComponent<PlayerMergeWindow>(gameObject, "player_merge_Window");
				if (playerMergeWindow != null && playerMergeWindow.gameObject.activeSelf)
				{
					flag = true;
				}
			}
		}
		return flag;
	}

	// Token: 0x06004745 RID: 18245 RVA: 0x00176988 File Offset: 0x00174B88
	public static bool ShowGetAllOtomoAndChara()
	{
		bool result = false;
		if (RouletteUtility.s_spinResult != null && RouletteUtility.s_spinResultCount >= 0)
		{
			ServerChaoData showData = RouletteUtility.s_spinResult.GetShowData(RouletteUtility.s_spinResultCount);
			if (showData != null)
			{
				RouletteUtility.ShowOtomo(showData, true, null, 0, true);
				result = true;
			}
			RouletteUtility.s_spinResultCount--;
		}
		return result;
	}

	// Token: 0x06004746 RID: 18246 RVA: 0x001769DC File Offset: 0x00174BDC
	public static void ShowGetAllListEnd()
	{
		string name = "RouletteGetAllListEnd";
		string text = string.Empty;
		string text2 = string.Empty;
		GeneralWindow.ButtonType buttonType = GeneralWindow.ButtonType.YesNo;
		if (RouletteUtility.s_spinResult != null)
		{
			text2 = RouletteUtility.s_spinResult.AcquiredListText;
			bool flag = RouletteUtility.s_spinResult.CheckGetChara();
			if (!string.IsNullOrEmpty(text2))
			{
				string text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Roulette", "get_item_list_text").text;
				if (!string.IsNullOrEmpty(text3))
				{
					text = text3.Replace("{PARAN}", text2);
					if (flag)
					{
						name = "RouletteGetAllListEndChara";
						string text4 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_Header_PlayerSet").text;
						text = text.Replace("{PAGE}", text4);
					}
					else
					{
						name = "RouletteGetAllListEndChao";
						string text5 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_Header_ChaoSet").text;
						text = text.Replace("{PAGE}", text5);
					}
				}
				else
				{
					text = text2;
					buttonType = GeneralWindow.ButtonType.Ok;
				}
			}
		}
		RouletteManager.isShowGetWindow = true;
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = name,
			buttonType = buttonType,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "GeneralWindow", "ui_Lbl_get_list").text,
			message = text
		});
	}

	// Token: 0x06004747 RID: 18247 RVA: 0x00176B14 File Offset: 0x00174D14
	private static void ShowOtomo(ServerChaoData data, bool required, Dictionary<int, ServerItemState> itemState, int numRequiredSpEggs, bool multi)
	{
		ServerItem serverItem = new ServerItem((ServerItem.Id)data.Id);
		GameObject uiRoot = GameObject.Find("UI Root (2D)");
		if (data.Rarity == 100 || serverItem.idType == ServerItem.IdType.CHARA)
		{
			RouletteUtility.ShowGetWindowChara(data, uiRoot, itemState, multi);
		}
		else if (data.Level == 0)
		{
			RouletteUtility.ShowGetWindowOtomo(data, uiRoot, multi);
		}
		else if (RouletteUtility.IsLevelMaxChao(data.Id) && !required)
		{
			if (numRequiredSpEggs > 0)
			{
				RouletteUtility.ShowGetWindowOtomoMax(data, uiRoot, numRequiredSpEggs);
			}
			else
			{
				RouletteUtility.ShowGetWindowOtomoLvup(data, uiRoot, multi);
			}
		}
		else if (!multi && !required)
		{
			RouletteUtility.ShowGetWindowOtomoMax(data, uiRoot, numRequiredSpEggs);
		}
		else
		{
			RouletteUtility.ShowGetWindowOtomoLvup(data, uiRoot, multi);
		}
	}

	// Token: 0x06004748 RID: 18248 RVA: 0x00176BD8 File Offset: 0x00174DD8
	public static void ShowGetWindow(ServerChaoSpinResult data)
	{
		RouletteManager.isShowGetWindow = false;
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			ServerItem serverItem = new ServerItem((ServerItem.Id)data.AcquiredChaoData.Id);
			if (data.AcquiredChaoData.Rarity == 100 || serverItem.idType == ServerItem.IdType.CHARA)
			{
				RouletteUtility.ShowGetWindowChara(data.AcquiredChaoData, gameObject, data.ItemState, false);
			}
			else if (data.AcquiredChaoData.Level == 0)
			{
				RouletteUtility.ShowGetWindowOtomo(data.AcquiredChaoData, gameObject, false);
			}
			else if (RouletteUtility.IsLevelMaxChao(data.AcquiredChaoData.Id) && !data.IsRequiredChao)
			{
				RouletteUtility.ShowGetWindowOtomoMax(data.AcquiredChaoData, gameObject, data.NumRequiredSpEggs);
			}
			else
			{
				RouletteUtility.ShowGetWindowOtomoLvup(data.AcquiredChaoData, gameObject, false);
			}
		}
	}

	// Token: 0x06004749 RID: 18249 RVA: 0x00176CB4 File Offset: 0x00174EB4
	private static void ShowGetWindowChara(ServerChaoData data, GameObject uiRoot, Dictionary<int, ServerItemState> itemState, bool multi)
	{
		int id = data.Id;
		int rarity = data.Rarity;
		int level = data.Level;
		RouletteUtility.AchievementType achievement = RouletteUtility.AchievementType.PlayerGet;
		ServerPlayerState playerState = ServerInterface.PlayerState;
		CharacterDataNameInfo.Info dataByServerID = CharacterDataNameInfo.Instance.GetDataByServerID(id);
		ServerCharacterState serverCharacterState = playerState.CharacterState(dataByServerID.m_ID);
		if ((itemState == null || (itemState != null && itemState.Count == 0)) && serverCharacterState.star > 0)
		{
			PlayerMergeWindow playerMergeWindow = GameObjectUtil.FindChildGameObjectComponent<PlayerMergeWindow>(uiRoot, "player_merge_Window");
			if (playerMergeWindow != null)
			{
				playerMergeWindow.PlayStart(id, achievement);
			}
		}
		else
		{
			ChaoGetWindow chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(uiRoot, "ro_PlayerGetWindowUI");
			if (chaoGetWindow != null)
			{
				if (multi)
				{
					achievement = RouletteUtility.AchievementType.Multi;
				}
				ChaoGetPartsBase chaoGetParts;
				if (itemState != null && itemState.Count > 0)
				{
					PlayerGetPartsOverlap playerGetPartsOverlap = chaoGetWindow.gameObject.GetComponent<PlayerGetPartsOverlap>();
					if (playerGetPartsOverlap == null)
					{
						playerGetPartsOverlap = chaoGetWindow.gameObject.AddComponent<PlayerGetPartsOverlap>();
					}
					playerGetPartsOverlap.Init(id, rarity, level, itemState, PlayerGetPartsOverlap.IntroType.NORMAL);
					chaoGetParts = playerGetPartsOverlap;
				}
				else
				{
					if (RouletteUtility.isTutorial && RouletteTop.Instance != null && RouletteTop.Instance.category == RouletteCategory.PREMIUM)
					{
						TutorialCursor.StartTutorialCursor(TutorialCursor.Type.ROULETTE_OK);
					}
					PlayerGetPartsOverlap playerGetPartsOverlap2 = chaoGetWindow.gameObject.GetComponent<PlayerGetPartsOverlap>();
					if (playerGetPartsOverlap2 == null)
					{
						playerGetPartsOverlap2 = chaoGetWindow.gameObject.AddComponent<PlayerGetPartsOverlap>();
					}
					playerGetPartsOverlap2.Init(id, rarity, level, null, PlayerGetPartsOverlap.IntroType.NORMAL);
					chaoGetParts = playerGetPartsOverlap2;
				}
				if (multi)
				{
					chaoGetWindow.isSetuped = false;
				}
				chaoGetWindow.PlayStart(chaoGetParts, RouletteUtility.isTutorial, false, achievement);
			}
		}
	}

	// Token: 0x0600474A RID: 18250 RVA: 0x00176E48 File Offset: 0x00175048
	private static void ShowGetWindowOtomo(ServerChaoData data, GameObject uiRoot, bool multi)
	{
		ChaoGetPartsBase chaoGetParts = null;
		int rarity = data.Rarity;
		ChaoGetWindow chaoGetWindow;
		if (rarity == 0 || rarity == 1)
		{
			chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(uiRoot, "chao_get_Window");
			if (chaoGetWindow != null)
			{
				ChaoGetPartsNormal chaoGetPartsNormal = chaoGetWindow.GetComponent<ChaoGetPartsNormal>();
				if (chaoGetPartsNormal == null)
				{
					chaoGetPartsNormal = chaoGetWindow.gameObject.AddComponent<ChaoGetPartsNormal>();
				}
				chaoGetPartsNormal.Init(data.Id, rarity);
				chaoGetParts = chaoGetPartsNormal;
			}
		}
		else
		{
			chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(uiRoot, "chao_rare_get_Window");
			if (chaoGetWindow != null)
			{
				ChaoGetPartsRare chaoGetPartsRare = chaoGetWindow.gameObject.GetComponent<ChaoGetPartsRare>();
				if (chaoGetPartsRare == null)
				{
					chaoGetPartsRare = chaoGetWindow.gameObject.AddComponent<ChaoGetPartsRare>();
				}
				chaoGetPartsRare.Init(data.Id, rarity);
				chaoGetParts = chaoGetPartsRare;
			}
		}
		if (chaoGetWindow != null)
		{
			if (multi)
			{
				chaoGetWindow.isSetuped = false;
				chaoGetWindow.PlayStart(chaoGetParts, RouletteUtility.isTutorial, false, RouletteUtility.AchievementType.Multi);
			}
			else
			{
				chaoGetWindow.PlayStart(chaoGetParts, RouletteUtility.isTutorial, false, RouletteUtility.AchievementType.ChaoGet);
			}
		}
	}

	// Token: 0x0600474B RID: 18251 RVA: 0x00176F44 File Offset: 0x00175144
	private static void ShowGetWindowOtomoLvup(ServerChaoData data, GameObject uiRoot, bool multi)
	{
		ChaoMergeWindow chaoMergeWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoMergeWindow>(uiRoot, "chao_merge_Window");
		if (chaoMergeWindow != null)
		{
			if (multi)
			{
				chaoMergeWindow.isSetuped = false;
				chaoMergeWindow.PlayStart(data.Id, data.Level, data.Rarity, RouletteUtility.AchievementType.Multi);
			}
			else
			{
				chaoMergeWindow.PlayStart(data.Id, data.Level, data.Rarity, RouletteUtility.AchievementType.LevelUp);
			}
		}
	}

	// Token: 0x0600474C RID: 18252 RVA: 0x00176FB0 File Offset: 0x001751B0
	private static void ShowGetWindowOtomoMax(ServerChaoData data, GameObject uiRoot, int numRequiredSpEggs)
	{
		int rarity = data.Rarity;
		SpEggGetWindow spEggGetWindow;
		SpEggGetPartsBase spEggGetParts;
		if (rarity == 0)
		{
			spEggGetWindow = GameObjectUtil.FindChildGameObjectComponent<SpEggGetWindow>(uiRoot, "chao_egg_transform_Window");
			spEggGetParts = new SpEggGetPartsNormal(data.Id, numRequiredSpEggs);
		}
		else
		{
			spEggGetWindow = GameObjectUtil.FindChildGameObjectComponent<SpEggGetWindow>(uiRoot, "chao_rare_egg_transform_Window");
			spEggGetParts = new SpEggGetPartsRare(data.Id, rarity, numRequiredSpEggs);
		}
		if (spEggGetWindow != null)
		{
			spEggGetWindow.PlayStart(spEggGetParts, RouletteUtility.AchievementType.LevelMax);
		}
	}

	// Token: 0x0600474D RID: 18253 RVA: 0x0017701C File Offset: 0x0017521C
	public static void ShowLoginBounsInfoWindow(string param = "")
	{
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "today_roulette_caption").text;
		string message;
		if (string.IsNullOrEmpty(param))
		{
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "today_roulette_text").text;
		}
		else
		{
			message = param;
		}
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "LoginBouns",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = text,
			message = message
		});
	}

	// Token: 0x0600474E RID: 18254 RVA: 0x0017709C File Offset: 0x0017529C
	private static string GetText(string cellName, Dictionary<string, string> dicReplaces = null)
	{
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", cellName).text;
		if (dicReplaces != null)
		{
			text = TextUtility.Replaces(text, dicReplaces);
		}
		return text;
	}

	// Token: 0x0600474F RID: 18255 RVA: 0x001770CC File Offset: 0x001752CC
	private static string GetText(string cellName, string srcText, string dstText)
	{
		return RouletteUtility.GetText(cellName, new Dictionary<string, string>
		{
			{
				srcText,
				dstText
			}
		});
	}

	// Token: 0x06004750 RID: 18256 RVA: 0x001770F0 File Offset: 0x001752F0
	public static string GetPrizeList(ServerPrizeState prizeState)
	{
		string text = string.Empty;
		int num = -1;
		Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
		List<int> list = new List<int>();
		foreach (ServerPrizeData serverPrizeData in prizeState.prizeList)
		{
			if (serverPrizeData.priority >= 0)
			{
				if (!list.Contains(serverPrizeData.priority))
				{
					list.Add(serverPrizeData.priority);
				}
				if (dictionary.ContainsKey(serverPrizeData.priority))
				{
					dictionary[serverPrizeData.priority].Add(serverPrizeData.GetItemName());
				}
				else
				{
					List<string> list2 = new List<string>();
					list2.Add(serverPrizeData.GetItemName());
					dictionary.Add(serverPrizeData.priority, list2);
				}
			}
		}
		list.Sort();
		for (int i = 0; i < list.Count; i++)
		{
			int num2 = list[i];
			List<string> list3 = new List<string>();
			int num3 = 0;
			foreach (string text2 in dictionary[num2])
			{
				if (!list3.Contains(text2))
				{
					if (num != num2)
					{
						num = num2;
						if (!string.IsNullOrEmpty(text))
						{
							if (num3 != 0)
							{
								text += Environment.NewLine;
							}
							text += Environment.NewLine;
						}
						num3 = 0;
						string cellName = "ui_Lbl_rarity_" + num2.ToString();
						text += "[00ff00]";
						text += TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Roulette", cellName).text;
						text += "[-]";
						text += Environment.NewLine;
					}
					else if (num3 > 0)
					{
						text += ", ";
					}
					text += text2;
					list3.Add(text2);
					num3++;
					if (num3 >= 3)
					{
						text += Environment.NewLine;
						num3 = 0;
					}
				}
			}
		}
		return text;
	}

	// Token: 0x06004751 RID: 18257 RVA: 0x00177354 File Offset: 0x00175554
	public static List<Constants.Campaign.emType> GetCampaign(RouletteCategory category)
	{
		List<Constants.Campaign.emType> list = null;
		if (RouletteUtility.isTutorial && category == RouletteCategory.PREMIUM)
		{
			return null;
		}
		ServerCampaignState campaignState = ServerInterface.CampaignState;
		if (campaignState != null)
		{
			if (category != RouletteCategory.PREMIUM)
			{
				if (category == RouletteCategory.ITEM)
				{
					if (campaignState.InSession(Constants.Campaign.emType.FreeWheelSpinCount))
					{
						if (list == null)
						{
							list = new List<Constants.Campaign.emType>();
						}
						list.Add(Constants.Campaign.emType.FreeWheelSpinCount);
					}
					if (campaignState.InSession(Constants.Campaign.emType.JackPotValueBonus))
					{
						if (list == null)
						{
							list = new List<Constants.Campaign.emType>();
						}
						list.Add(Constants.Campaign.emType.JackPotValueBonus);
					}
				}
			}
			else
			{
				if (campaignState.InSession(Constants.Campaign.emType.PremiumRouletteOdds))
				{
					if (list == null)
					{
						list = new List<Constants.Campaign.emType>();
					}
					list.Add(Constants.Campaign.emType.PremiumRouletteOdds);
				}
				if (campaignState.InSession(Constants.Campaign.emType.ChaoRouletteCost))
				{
					if (list == null)
					{
						list = new List<Constants.Campaign.emType>();
					}
					list.Add(Constants.Campaign.emType.ChaoRouletteCost);
				}
			}
		}
		return list;
	}

	// Token: 0x06004752 RID: 18258 RVA: 0x00177424 File Offset: 0x00175624
	public static string GetChaoGroupName(int chaoId)
	{
		ServerItem serverItem = new ServerItem((ServerItem.Id)chaoId);
		if (serverItem.idType == ServerItem.IdType.CHARA)
		{
			return "CharaName";
		}
		return "Chao";
	}

	// Token: 0x06004753 RID: 18259 RVA: 0x00177454 File Offset: 0x00175654
	public static string GetChaoCellName(int chaoId)
	{
		string result = string.Empty;
		ServerItem serverItem = new ServerItem((ServerItem.Id)chaoId);
		if (serverItem.idType == ServerItem.IdType.CHARA)
		{
			ServerItem serverItem2 = new ServerItem((ServerItem.Id)chaoId);
			int charaType = (int)serverItem2.charaType;
			result = CharaName.Name[charaType];
		}
		else
		{
			int num = chaoId - 400000;
			result = string.Format("name{0:D4}", num);
		}
		return result;
	}

	// Token: 0x06004754 RID: 18260 RVA: 0x001774B8 File Offset: 0x001756B8
	public static ServerChaoState.ChaoStatus GetChaoStatus(int chaoId)
	{
		ServerChaoState.ChaoStatus result = ServerChaoState.ChaoStatus.NotOwned;
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState == null)
		{
			return result;
		}
		ServerChaoState serverChaoState = playerState.ChaoStateByItemID(chaoId);
		if (serverChaoState == null)
		{
			return result;
		}
		return serverChaoState.Status;
	}

	// Token: 0x06004755 RID: 18261 RVA: 0x001774F0 File Offset: 0x001756F0
	public static bool IsLevelMaxChao(int chaoId)
	{
		ServerChaoState.ChaoStatus chaoStatus = RouletteUtility.GetChaoStatus(chaoId);
		return chaoStatus == ServerChaoState.ChaoStatus.MaxLevel;
	}

	// Token: 0x04003B32 RID: 15154
	public const bool ROULETTE_PARTS_DSTROY = false;

	// Token: 0x04003B33 RID: 15155
	public const bool ITEM_ROULETTE_USE_RING = false;

	// Token: 0x04003B34 RID: 15156
	public const bool ROULETTE_CHANGE_EFFECT = false;

	// Token: 0x04003B35 RID: 15157
	public const float ROULETTE_BASIC_RELOAD_SPAN = 5f;

	// Token: 0x04003B36 RID: 15158
	public const float ROULETTE_MULTI_GET_EFFECT_TIME = 5f;

	// Token: 0x04003B37 RID: 15159
	public const float ROULETTE_SPIN_WAIT_LIMIT_TIME = 10f;

	// Token: 0x04003B38 RID: 15160
	public const int ROULETTE_MULTI_NUM_0 = 1;

	// Token: 0x04003B39 RID: 15161
	public const int ROULETTE_MULTI_NUM_1 = 3;

	// Token: 0x04003B3A RID: 15162
	public const int ROULETTE_MULTI_NUM_2 = 5;

	// Token: 0x04003B3B RID: 15163
	public const int ROULETTE_TUTORIAL_ADD_SP_EGG = 10;

	// Token: 0x04003B3C RID: 15164
	private const string ROULETTE_CHANGE_ICON_SPRITE_NAME = "ui_roulette_pager_icon_{CATEGORY}";

	// Token: 0x04003B3D RID: 15165
	private const string ROULETTE_BG_SPRITE_NAME = "ui_roulette_tablebg_{COLOR}";

	// Token: 0x04003B3E RID: 15166
	private const string ROULETTE_BOARD_SPRITE_NAME = "ui_roulette_table_{COLOR}_{TYPE}";

	// Token: 0x04003B3F RID: 15167
	private const string ROULETTE_ARROW_SPRITE_NAME = "ui_roulette_arrow_{COLOR}";

	// Token: 0x04003B40 RID: 15168
	private const string ROULETTE_COST_ITEM_SPRITE_NAME = "ui_cmn_icon_item_{ID}";

	// Token: 0x04003B41 RID: 15169
	private const string ROULETTE_HEADER_NAME = "ui_Header_{TYPE}_Roulette";

	// Token: 0x04003B42 RID: 15170
	public static readonly int OddsDisplayDecimal = 2;

	// Token: 0x04003B43 RID: 15171
	private static bool s_itemRouletteUse;

	// Token: 0x04003B44 RID: 15172
	private static bool s_loginRoulette;

	// Token: 0x04003B45 RID: 15173
	private static RouletteCategory s_rouletteDefault = RouletteCategory.ITEM;

	// Token: 0x04003B46 RID: 15174
	public static ServerSpinResultGeneral s_spinResult;

	// Token: 0x04003B47 RID: 15175
	public static int s_spinResultCount;

	// Token: 0x04003B48 RID: 15176
	private static bool s_rouletteTurtorialEnd;

	// Token: 0x04003B49 RID: 15177
	private static bool s_rouletteTurtorial;

	// Token: 0x04003B4A RID: 15178
	private static bool s_rouletteTurtorialLock;

	// Token: 0x04003B4B RID: 15179
	private static string s_jackpotFeedText = string.Empty;

	// Token: 0x02000A52 RID: 2642
	public enum AchievementType
	{
		// Token: 0x04003B4D RID: 15181
		NONE,
		// Token: 0x04003B4E RID: 15182
		PlayerGet,
		// Token: 0x04003B4F RID: 15183
		ChaoGet,
		// Token: 0x04003B50 RID: 15184
		LevelUp,
		// Token: 0x04003B51 RID: 15185
		LevelMax,
		// Token: 0x04003B52 RID: 15186
		Multi
	}

	// Token: 0x02000A53 RID: 2643
	public enum NextType
	{
		// Token: 0x04003B54 RID: 15188
		NONE,
		// Token: 0x04003B55 RID: 15189
		EQUIP,
		// Token: 0x04003B56 RID: 15190
		CHARA_EQUIP
	}

	// Token: 0x02000A54 RID: 2644
	public enum CellType
	{
		// Token: 0x04003B58 RID: 15192
		Item,
		// Token: 0x04003B59 RID: 15193
		Egg,
		// Token: 0x04003B5A RID: 15194
		Rankup
	}

	// Token: 0x02000A55 RID: 2645
	public enum WheelRank
	{
		// Token: 0x04003B5C RID: 15196
		Normal,
		// Token: 0x04003B5D RID: 15197
		Big,
		// Token: 0x04003B5E RID: 15198
		Super,
		// Token: 0x04003B5F RID: 15199
		MAX
	}

	// Token: 0x02000A56 RID: 2646
	public enum WheelType
	{
		// Token: 0x04003B61 RID: 15201
		NONE,
		// Token: 0x04003B62 RID: 15202
		Normal,
		// Token: 0x04003B63 RID: 15203
		Rankup
	}

	// Token: 0x02000A57 RID: 2647
	public enum RouletteColor
	{
		// Token: 0x04003B65 RID: 15205
		NONE,
		// Token: 0x04003B66 RID: 15206
		Blue,
		// Token: 0x04003B67 RID: 15207
		Purple,
		// Token: 0x04003B68 RID: 15208
		Green,
		// Token: 0x04003B69 RID: 15209
		Silver,
		// Token: 0x04003B6A RID: 15210
		Gold
	}
}
