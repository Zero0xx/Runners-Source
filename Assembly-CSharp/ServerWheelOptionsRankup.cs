using System;
using System.Collections.Generic;
using Text;

// Token: 0x0200051F RID: 1311
public class ServerWheelOptionsRankup : ServerWheelOptionsOrg
{
	// Token: 0x0600287A RID: 10362 RVA: 0x000FA850 File Offset: 0x000F8A50
	public ServerWheelOptionsRankup(ServerWheelOptions data)
	{
		if (data == null)
		{
			return;
		}
		this.m_category = RouletteCategory.ITEM;
		this.m_init = true;
		this.m_type = RouletteUtility.WheelType.Rankup;
		if (this.m_orgData == null)
		{
			this.m_orgData = new ServerWheelOptions(null);
		}
		data.CopyTo(this.m_orgData);
		this.UpdateItemWeights();
	}

	// Token: 0x1700055D RID: 1373
	// (get) Token: 0x0600287B RID: 10363 RVA: 0x000FA8AC File Offset: 0x000F8AAC
	public override bool isValid
	{
		get
		{
			bool result = false;
			if ((this.m_orgData.m_nextFreeSpin - NetBase.GetCurrentTime()).Ticks > 0L)
			{
				result = true;
			}
			return result;
		}
	}

	// Token: 0x1700055E RID: 1374
	// (get) Token: 0x0600287C RID: 10364 RVA: 0x000FA8E4 File Offset: 0x000F8AE4
	public override bool isRemainingRefresh
	{
		get
		{
			bool result = false;
			if (this.m_orgData != null && this.m_orgData.m_itemWon >= 0 && this.m_orgData.m_items.Length > this.m_orgData.m_itemWon)
			{
				int id = this.m_orgData.m_items[this.m_orgData.m_itemWon];
				ServerItem serverItem = new ServerItem((ServerItem.Id)id);
				if (serverItem.idType == ServerItem.IdType.ITEM_ROULLETE_WIN && this.m_orgData.m_rouletteRank != RouletteUtility.WheelRank.Super)
				{
					result = true;
				}
			}
			return result;
		}
	}

	// Token: 0x1700055F RID: 1375
	// (get) Token: 0x0600287D RID: 10365 RVA: 0x000FA970 File Offset: 0x000F8B70
	public override int itemWon
	{
		get
		{
			int result = -1;
			if (this.m_orgData != null)
			{
				result = this.m_orgData.m_itemWon;
			}
			return result;
		}
	}

	// Token: 0x17000560 RID: 1376
	// (get) Token: 0x0600287E RID: 10366 RVA: 0x000FA998 File Offset: 0x000F8B98
	public override ServerItem itemWonData
	{
		get
		{
			if (this.m_orgData != null && this.m_orgData.m_itemWon >= 0 && this.m_orgData.m_items.Length > this.m_orgData.m_itemWon)
			{
				int id = this.m_orgData.m_items[this.m_orgData.m_itemWon];
				return new ServerItem((ServerItem.Id)id);
			}
			return default(ServerItem);
		}
	}

	// Token: 0x17000561 RID: 1377
	// (get) Token: 0x0600287F RID: 10367 RVA: 0x000FAA08 File Offset: 0x000F8C08
	public override int numJackpotRing
	{
		get
		{
			return this.m_orgData.m_numJackpotRing;
		}
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x000FAA18 File Offset: 0x000F8C18
	public override void Setup(ServerChaoWheelOptions data)
	{
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x000FAA1C File Offset: 0x000F8C1C
	public override void Setup(ServerWheelOptions data)
	{
		if (data == null)
		{
			return;
		}
		this.m_category = RouletteCategory.ITEM;
		this.m_init = true;
		this.m_type = RouletteUtility.WheelType.Rankup;
		if (this.m_orgData == null)
		{
			this.m_orgData = new ServerWheelOptions(null);
		}
		data.CopyTo(this.m_orgData);
		this.UpdateItemWeights();
	}

	// Token: 0x06002882 RID: 10370 RVA: 0x000FAA70 File Offset: 0x000F8C70
	public override void Setup(ServerWheelOptionsGeneral data)
	{
	}

	// Token: 0x06002883 RID: 10371 RVA: 0x000FAA74 File Offset: 0x000F8C74
	public override int GetRouletteBoardPattern()
	{
		int result = 0;
		if (this.m_init)
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x06002884 RID: 10372 RVA: 0x000FAA94 File Offset: 0x000F8C94
	public override string GetRouletteArrowSprite()
	{
		string result = null;
		if (this.m_init)
		{
			switch (this.m_orgData.m_rouletteRank)
			{
			case RouletteUtility.WheelRank.Normal:
				result = "ui_roulette_arrow_sil";
				break;
			case RouletteUtility.WheelRank.Big:
				result = "ui_roulette_arrow_gol";
				break;
			case RouletteUtility.WheelRank.Super:
				result = "ui_roulette_arrow_gol";
				break;
			}
		}
		return result;
	}

	// Token: 0x06002885 RID: 10373 RVA: 0x000FAAF4 File Offset: 0x000F8CF4
	public override string GetRouletteBgSprite()
	{
		return "ui_roulette_tablebg_gre";
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x000FAB0C File Offset: 0x000F8D0C
	public override string GetRouletteBoardSprite()
	{
		string result = null;
		switch (this.m_orgData.m_rouletteRank)
		{
		case RouletteUtility.WheelRank.Normal:
			result = "ui_roulette_table_gre_1";
			break;
		case RouletteUtility.WheelRank.Big:
			result = "ui_roulette_table_sil_1";
			break;
		case RouletteUtility.WheelRank.Super:
			result = "ui_roulette_table_gol_1r";
			break;
		}
		return result;
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x000FAB60 File Offset: 0x000F8D60
	public override string GetRouletteTicketSprite()
	{
		if (this.m_orgData != null)
		{
			return "ui_cmn_icon_item_240000";
		}
		return null;
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x000FAB74 File Offset: 0x000F8D74
	public override RouletteUtility.WheelRank GetRouletteRank()
	{
		RouletteUtility.WheelRank result = RouletteUtility.WheelRank.Normal;
		if (this.m_init && this.m_orgData != null)
		{
			result = this.m_orgData.m_rouletteRank;
		}
		return result;
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x000FABA8 File Offset: 0x000F8DA8
	public override float GetCellWeight(int cellIndex)
	{
		return 1f;
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x000FABBC File Offset: 0x000F8DBC
	public override int GetCellEgg(int cellIndex)
	{
		int num = -1;
		if (this.m_orgData != null && this.m_orgData.m_items.Length > cellIndex)
		{
			num = this.m_orgData.m_items[cellIndex];
			if (num >= 1000)
			{
				if (num >= 300000 && num < 400000)
				{
					num = 100;
				}
				else if (num >= 400000 && num < 500000)
				{
					if (num >= 402000)
					{
						num = 2;
					}
					else if (num >= 401000)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x0600288B RID: 10379 RVA: 0x000FAC60 File Offset: 0x000F8E60
	public override ServerItem GetCellItem(int cellIndex, out int num)
	{
		ServerItem result = default(ServerItem);
		num = 1;
		if (this.m_orgData != null && this.m_orgData.m_items.Length > cellIndex)
		{
			result = new ServerItem((ServerItem.Id)this.m_orgData.m_items[cellIndex]);
			num = this.m_orgData.m_itemQuantities[cellIndex];
		}
		return result;
	}

	// Token: 0x0600288C RID: 10380 RVA: 0x000FACBC File Offset: 0x000F8EBC
	public override void PlayBgm(float delay = 0f)
	{
		Debug.Log("ServerWheelOptionsRankup PlayBgm   no play bgm !");
	}

	// Token: 0x0600288D RID: 10381 RVA: 0x000FACC8 File Offset: 0x000F8EC8
	public override void PlaySe(ServerWheelOptionsData.SE_TYPE seType, float delay = 0f)
	{
		if (this.m_init)
		{
			string text = null;
			switch (seType)
			{
			case ServerWheelOptionsData.SE_TYPE.Open:
				text = "sys_window_open";
				break;
			case ServerWheelOptionsData.SE_TYPE.Close:
				text = "sys_window_close";
				break;
			case ServerWheelOptionsData.SE_TYPE.Click:
				text = "sys_menu_decide";
				break;
			case ServerWheelOptionsData.SE_TYPE.Spin:
				text = "sys_menu_decide";
				break;
			case ServerWheelOptionsData.SE_TYPE.SpinError:
				text = "sys_error";
				break;
			case ServerWheelOptionsData.SE_TYPE.Arrow:
				text = "sys_roulette_arrow";
				break;
			case ServerWheelOptionsData.SE_TYPE.Skip:
				text = "sys_page_skip";
				break;
			case ServerWheelOptionsData.SE_TYPE.GetItem:
				text = "sys_roulette_itemget";
				break;
			case ServerWheelOptionsData.SE_TYPE.GetRare:
				text = "sys_roulette_itemget_rare";
				break;
			case ServerWheelOptionsData.SE_TYPE.GetRankup:
				text = "sys_roulette_levelup";
				break;
			case ServerWheelOptionsData.SE_TYPE.GetJackpot:
				text = "sys_roulette_jackpot";
				break;
			case ServerWheelOptionsData.SE_TYPE.Multi:
				text = "boss_scene_change";
				break;
			case ServerWheelOptionsData.SE_TYPE.Change:
				text = "sys_roulette_change";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (delay <= 0f)
				{
					SoundManager.SePlay(text, "SE");
				}
				else
				{
					RouletteManager.PlaySe(text, delay, "SE");
				}
			}
		}
	}

	// Token: 0x0600288E RID: 10382 RVA: 0x000FADE8 File Offset: 0x000F8FE8
	public override ServerWheelOptionsData.SPIN_BUTTON GetSpinButtonSeting(out int count, out bool btnActive)
	{
		ServerWheelOptionsData.SPIN_BUTTON result = ServerWheelOptionsData.SPIN_BUTTON.NONE;
		count = 0;
		btnActive = false;
		if (this.m_init && SaveDataManager.Instance != null && SaveDataManager.Instance.ItemData != null)
		{
			int ringCount = (int)SaveDataManager.Instance.ItemData.RingCount;
			result = ServerWheelOptionsData.SPIN_BUTTON.TICKET;
			int numRemaining = this.m_orgData.m_numRemaining;
			int numRouletteToken = this.m_orgData.m_numRouletteToken;
			if (numRemaining > numRouletteToken)
			{
				result = ServerWheelOptionsData.SPIN_BUTTON.FREE;
				count = numRemaining - numRouletteToken;
				btnActive = true;
			}
			else
			{
				count = 1;
				if (numRemaining > 0)
				{
					btnActive = true;
				}
			}
		}
		return result;
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x000FAE7C File Offset: 0x000F907C
	public override ServerWheelOptionsData.SPIN_BUTTON GetSpinButtonSeting()
	{
		ServerWheelOptionsData.SPIN_BUTTON result = ServerWheelOptionsData.SPIN_BUTTON.NONE;
		if (this.m_init && SaveDataManager.Instance != null && SaveDataManager.Instance.ItemData != null)
		{
			result = ServerWheelOptionsData.SPIN_BUTTON.TICKET;
			int numRemaining = this.m_orgData.m_numRemaining;
			int numRouletteToken = this.m_orgData.m_numRouletteToken;
			if (numRemaining > numRouletteToken)
			{
				result = ServerWheelOptionsData.SPIN_BUTTON.FREE;
			}
		}
		return result;
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x000FAEE0 File Offset: 0x000F90E0
	public override List<int> GetSpinCostItemIdList()
	{
		List<int> list = new List<int>();
		if (this.m_orgData != null)
		{
			int numRouletteToken = this.m_orgData.m_numRouletteToken;
			if (numRouletteToken >= 0)
			{
				list.Add(240000);
			}
		}
		return list;
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x000FAF20 File Offset: 0x000F9120
	public override int GetSpinCostItemCost(int costItemId)
	{
		int result = 0;
		if (this.m_orgData != null)
		{
			if (costItemId == 0)
			{
				result = 1;
			}
			else if (costItemId != 240000)
			{
				if (costItemId == 900000 || costItemId == 910000)
				{
					result = this.m_orgData.m_spinCost;
				}
			}
			else
			{
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x000FAF88 File Offset: 0x000F9188
	public override int GetSpinCostItemNum(int costItemId)
	{
		int result = 0;
		if (this.m_orgData != null)
		{
			if (costItemId == 0)
			{
				result = this.m_orgData.m_numRemaining - this.m_orgData.m_numRouletteToken;
			}
			else if (costItemId != 240000)
			{
				if (costItemId != 900000)
				{
					if (costItemId == 910000)
					{
						result = (int)SaveDataManager.Instance.ItemData.RingCount;
					}
				}
				else
				{
					result = (int)SaveDataManager.Instance.ItemData.RedRingCount;
				}
			}
			else
			{
				result = this.m_orgData.m_numRouletteToken;
			}
		}
		return result;
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x000FB030 File Offset: 0x000F9230
	public override bool GetEggSeting(out int count)
	{
		bool result = false;
		count = 0;
		return result;
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x000FB044 File Offset: 0x000F9244
	public override ServerWheelOptions GetOrgRankupData()
	{
		return this.m_orgData;
	}

	// Token: 0x06002895 RID: 10389 RVA: 0x000FB04C File Offset: 0x000F924C
	public override ServerChaoWheelOptions GetOrgNormalData()
	{
		return null;
	}

	// Token: 0x06002896 RID: 10390 RVA: 0x000FB050 File Offset: 0x000F9250
	public override ServerWheelOptionsGeneral GetOrgGeneralData()
	{
		return null;
	}

	// Token: 0x06002897 RID: 10391 RVA: 0x000FB054 File Offset: 0x000F9254
	public override Dictionary<long, string[]> UpdateItemWeights()
	{
		if (this.m_itemOdds != null)
		{
			this.m_itemOdds.Clear();
		}
		this.m_itemOdds = new Dictionary<long, string[]>();
		List<long> list = new List<long>();
		Dictionary<long, float> dictionary = new Dictionary<long, float>();
		Dictionary<long, int> dictionary2 = new Dictionary<long, int>();
		float num = 0f;
		if (this.m_orgData != null)
		{
			for (int i = 0; i < this.m_orgData.m_items.Length; i++)
			{
				long num2 = (long)this.m_orgData.m_items[i];
				float num3 = (float)this.m_orgData.m_itemWeight[i];
				int num4 = this.m_orgData.m_itemQuantities[i];
				if (num2 >= 400000L && num2 < 500000L)
				{
					if (num2 >= 402000L)
					{
						num2 = 402000L;
					}
					else if (num2 >= 401000L)
					{
						num2 = 401000L;
					}
					else
					{
						num2 = 400000L;
					}
				}
				num2 = num2 * 100000L + (long)num4;
				num += num3;
				if (!list.Contains(num2))
				{
					list.Add(num2);
				}
				if (!dictionary.ContainsKey(num2))
				{
					dictionary.Add(num2, num3);
				}
				else
				{
					Dictionary<long, float> dictionary4;
					Dictionary<long, float> dictionary3 = dictionary4 = dictionary;
					long key2;
					long key = key2 = num2;
					float num5 = dictionary4[key2];
					dictionary3[key] = num5 + num3;
				}
				if (!dictionary2.ContainsKey(num2))
				{
					dictionary2.Add(num2, num4);
				}
				else
				{
					dictionary2[num2] = num4;
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				List<string> list2 = new List<string>();
				long num6 = list[j];
				float num7 = dictionary[num6] / num * 100f;
				int num8 = dictionary2[num6];
				int id = (int)(num6 / 100000L);
				ServerItem serverItem = new ServerItem((ServerItem.Id)id);
				string str = string.Empty;
				string str2 = string.Empty;
				switch (serverItem.id)
				{
				case ServerItem.Id.BIG:
				case ServerItem.Id.SUPER:
				case ServerItem.Id.JACKPOT:
					str = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "reward_" + serverItem.id.ToString().ToLower()).text;
					break;
				default:
					if (serverItem.idType == ServerItem.IdType.CHAO || serverItem.idType == ServerItem.IdType.CHARA)
					{
						string cellName = "ui_Lbl_rarity_0";
						if (serverItem.id >= ServerItem.Id.CHAO_BEGIN && serverItem.id < (ServerItem.Id)500000)
						{
							cellName = "ui_Lbl_rarity_" + (int)((float)serverItem.id / 1000f) % 10;
						}
						else if (serverItem.id >= ServerItem.Id.CHARA_BEGIN && serverItem.id < ServerItem.Id.CHAO_BEGIN)
						{
							cellName = "ui_Lbl_rarity_100";
						}
						str = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Roulette", cellName).text;
					}
					else
					{
						str = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "RewardType", "reward_type_" + (int)serverItem.rewardType).text;
						str2 = " x " + num8;
					}
					break;
				}
				list2.Add(str + str2);
				string format = "F" + RouletteUtility.OddsDisplayDecimal.ToString();
				string item = string.Empty;
				item = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "odds").text.Replace("{ODDS}", num7.ToString(format));
				list2.Add(item);
				this.m_itemOdds.Add(num6, list2.ToArray());
			}
		}
		return this.m_itemOdds;
	}

	// Token: 0x06002898 RID: 10392 RVA: 0x000FB410 File Offset: 0x000F9610
	public override string ShowSpinErrorWindow()
	{
		string result;
		switch (this.GetSpinButtonSeting())
		{
		case ServerWheelOptionsData.SPIN_BUTTON.FREE:
			result = "SpinRemainingError";
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "SpinRemainingError",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_remain_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_remain_text").text,
				isPlayErrorSe = true
			});
			return result;
		case ServerWheelOptionsData.SPIN_BUTTON.RING:
			result = "SpinCostErrorRing";
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextUtility.GetCommonText("ItemRoulette", "gw_cost_caption"),
				message = TextUtility.GetCommonText("ItemRoulette", "gw_cost_text"),
				buttonType = GeneralWindow.ButtonType.ShopCancel,
				name = "SpinCostErrorRing",
				isPlayErrorSe = true
			});
			return result;
		case ServerWheelOptionsData.SPIN_BUTTON.TICKET:
			result = "SpinTicketError";
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "SpinTicketError",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_remain_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_ticket_text").text,
				isPlayErrorSe = true
			});
			return result;
		}
		result = "SpinDefaultError";
		Debug.Log("ServerWheelOptionsRankup ShowSpinErrorWindow error !!!");
		return result;
	}

	// Token: 0x06002899 RID: 10393 RVA: 0x000FB59C File Offset: 0x000F979C
	public override List<ServerItem> GetAttentionItemList()
	{
		return null;
	}

	// Token: 0x0600289A RID: 10394 RVA: 0x000FB5A0 File Offset: 0x000F97A0
	public override bool IsPrizeDataList()
	{
		return false;
	}

	// Token: 0x0400240C RID: 9228
	private ServerWheelOptions m_orgData;
}
