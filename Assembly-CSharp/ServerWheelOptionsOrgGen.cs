using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x0200051D RID: 1309
public class ServerWheelOptionsOrgGen : ServerWheelOptionsOrg
{
	// Token: 0x0600282E RID: 10286 RVA: 0x000F85B0 File Offset: 0x000F67B0
	public ServerWheelOptionsOrgGen(ServerWheelOptionsGeneral data)
	{
		if (data == null)
		{
			return;
		}
		this.m_category = RouletteUtility.GetRouletteCategory(data);
		this.m_init = true;
		this.m_type = RouletteUtility.WheelType.Rankup;
		if (this.m_orgData == null)
		{
			this.m_orgData = new ServerWheelOptionsGeneral();
		}
		data.CopyTo(this.m_orgData);
		this.UpdateItemWeights();
	}

	// Token: 0x17000551 RID: 1361
	// (get) Token: 0x0600282F RID: 10287 RVA: 0x000F8610 File Offset: 0x000F6810
	public override bool isValid
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000552 RID: 1362
	// (get) Token: 0x06002830 RID: 10288 RVA: 0x000F8620 File Offset: 0x000F6820
	public override bool isRemainingRefresh
	{
		get
		{
			bool result = false;
			if (this.m_orgData != null && this.m_orgData.type == RouletteUtility.WheelType.Rankup && RouletteManager.Instance != null)
			{
				result = RouletteManager.Instance.currentRankup;
			}
			return result;
		}
	}

	// Token: 0x17000553 RID: 1363
	// (get) Token: 0x06002831 RID: 10289 RVA: 0x000F8668 File Offset: 0x000F6868
	public override int itemWon
	{
		get
		{
			return -1;
		}
	}

	// Token: 0x17000554 RID: 1364
	// (get) Token: 0x06002832 RID: 10290 RVA: 0x000F8678 File Offset: 0x000F6878
	public override ServerItem itemWonData
	{
		get
		{
			return default(ServerItem);
		}
	}

	// Token: 0x17000555 RID: 1365
	// (get) Token: 0x06002833 RID: 10291 RVA: 0x000F8690 File Offset: 0x000F6890
	public override int rouletteId
	{
		get
		{
			return this.m_orgData.rouletteId;
		}
	}

	// Token: 0x17000556 RID: 1366
	// (get) Token: 0x06002834 RID: 10292 RVA: 0x000F86A0 File Offset: 0x000F68A0
	public override int multi
	{
		get
		{
			return this.m_orgData.multi;
		}
	}

	// Token: 0x17000557 RID: 1367
	// (get) Token: 0x06002835 RID: 10293 RVA: 0x000F86B0 File Offset: 0x000F68B0
	public override int numJackpotRing
	{
		get
		{
			return this.m_orgData.jackpotRing;
		}
	}

	// Token: 0x06002836 RID: 10294 RVA: 0x000F86C0 File Offset: 0x000F68C0
	public override void Setup(ServerChaoWheelOptions data)
	{
	}

	// Token: 0x06002837 RID: 10295 RVA: 0x000F86C4 File Offset: 0x000F68C4
	public override void Setup(ServerWheelOptions data)
	{
	}

	// Token: 0x06002838 RID: 10296 RVA: 0x000F86C8 File Offset: 0x000F68C8
	public override void Setup(ServerWheelOptionsGeneral data)
	{
		if (data == null)
		{
			return;
		}
		this.m_category = RouletteUtility.GetRouletteCategory(data);
		this.m_init = true;
		this.m_type = RouletteUtility.WheelType.Rankup;
		int selectIndex = 0;
		int multi = 1;
		if (this.m_orgData == null)
		{
			this.m_orgData = new ServerWheelOptionsGeneral();
		}
		else
		{
			selectIndex = this.m_orgData.currentCostSelect;
			multi = this.m_orgData.multi;
		}
		data.CopyTo(this.m_orgData);
		data.ChangeCostItem(selectIndex);
		if (!data.ChangeMulti(multi) || data.rank != RouletteUtility.WheelRank.Normal)
		{
			data.ChangeMulti(1);
		}
		this.UpdateItemWeights();
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x000F8768 File Offset: 0x000F6968
	public override bool ChangeMulti(int multi)
	{
		return this.m_orgData != null && this.m_orgData.ChangeMulti(multi);
	}

	// Token: 0x0600283A RID: 10298 RVA: 0x000F8784 File Offset: 0x000F6984
	public override bool IsMulti(int multi)
	{
		return this.m_orgData != null && this.m_orgData.IsMulti(multi);
	}

	// Token: 0x0600283B RID: 10299 RVA: 0x000F87A0 File Offset: 0x000F69A0
	public override int GetRouletteBoardPattern()
	{
		int result = 0;
		if (this.m_init)
		{
			result = this.m_orgData.patternType;
		}
		return result;
	}

	// Token: 0x0600283C RID: 10300 RVA: 0x000F87C8 File Offset: 0x000F69C8
	public override string GetRouletteArrowSprite()
	{
		if (this.m_orgData != null)
		{
			return this.m_orgData.spriteNameArrow;
		}
		return null;
	}

	// Token: 0x0600283D RID: 10301 RVA: 0x000F87E4 File Offset: 0x000F69E4
	public override string GetRouletteBgSprite()
	{
		if (this.m_orgData != null)
		{
			return this.m_orgData.spriteNameBg;
		}
		return null;
	}

	// Token: 0x0600283E RID: 10302 RVA: 0x000F8800 File Offset: 0x000F6A00
	public override string GetRouletteBoardSprite()
	{
		if (this.m_orgData != null)
		{
			return this.m_orgData.spriteNameBoard;
		}
		return null;
	}

	// Token: 0x0600283F RID: 10303 RVA: 0x000F881C File Offset: 0x000F6A1C
	public override string GetRouletteTicketSprite()
	{
		if (this.m_orgData != null)
		{
			return this.m_orgData.spriteNameCostItem;
		}
		return null;
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x000F8838 File Offset: 0x000F6A38
	public override RouletteUtility.WheelRank GetRouletteRank()
	{
		RouletteUtility.WheelRank result = RouletteUtility.WheelRank.Normal;
		if (this.m_init && this.m_orgData != null)
		{
			result = this.m_orgData.rank;
		}
		return result;
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x000F886C File Offset: 0x000F6A6C
	public override float GetCellWeight(int cellIndex)
	{
		float result = 0f;
		if (this.m_orgData != null && this.m_orgData.itemLenght > cellIndex)
		{
			result = this.m_orgData.GetCellWeight(cellIndex);
		}
		return result;
	}

	// Token: 0x06002842 RID: 10306 RVA: 0x000F88AC File Offset: 0x000F6AAC
	public override int GetCellEgg(int cellIndex)
	{
		int result = -1;
		if (this.m_orgData != null && this.m_orgData.itemLenght > cellIndex)
		{
			int id = 0;
			int num = 0;
			float num2 = 0f;
			this.m_orgData.GetCell(cellIndex, out id, out num, out num2);
			ServerItem serverItem = new ServerItem((ServerItem.Id)id);
			if (serverItem.idType == ServerItem.IdType.CHAO)
			{
				result = (int)(serverItem.id / (ServerItem.Id)1000 % (ServerItem.Id)10);
			}
		}
		return result;
	}

	// Token: 0x06002843 RID: 10307 RVA: 0x000F891C File Offset: 0x000F6B1C
	public override ServerItem GetCellItem(int cellIndex, out int num)
	{
		ServerItem result = default(ServerItem);
		num = 1;
		if (this.m_orgData != null && this.m_orgData.itemLenght > cellIndex)
		{
			int id = 0;
			float num2 = 0f;
			this.m_orgData.GetCell(cellIndex, out id, out num, out num2);
			result = new ServerItem((ServerItem.Id)id);
		}
		else
		{
			num = -1;
		}
		return result;
	}

	// Token: 0x06002844 RID: 10308 RVA: 0x000F897C File Offset: 0x000F6B7C
	public override void PlayBgm(float delay = 0f)
	{
		if (this.m_init && EventManager.Instance != null)
		{
			EventManager.EventType type = EventManager.Instance.Type;
			string text = null;
			string cueSheetName = "BGM";
			if ((!RouletteUtility.isTutorial || this.m_category != RouletteCategory.PREMIUM) && type != EventManager.EventType.NUM && type != EventManager.EventType.UNKNOWN && EventManager.Instance.IsInEvent() && EventCommonDataTable.Instance != null)
			{
				string data;
				switch (base.category)
				{
				case RouletteCategory.SPECIAL:
					data = EventCommonDataTable.Instance.GetData(EventCommonDataItem.RouletteS_BgmName);
					goto IL_CB;
				}
				data = EventCommonDataTable.Instance.GetData(EventCommonDataItem.Roulette_BgmName);
				IL_CB:
				if (!string.IsNullOrEmpty(data))
				{
					cueSheetName = "BGM_" + EventManager.GetEventTypeName(EventManager.Instance.Type);
					text = data;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				switch (base.category)
				{
				case RouletteCategory.SPECIAL:
					text = "bgm_sys_s_roulette";
					goto IL_149;
				}
				text = "bgm_sys_roulette";
			}
			IL_149:
			if (!string.IsNullOrEmpty(text))
			{
				RouletteManager.PlayBgm(text, delay, cueSheetName, false);
			}
		}
	}

	// Token: 0x06002845 RID: 10309 RVA: 0x000F8AE8 File Offset: 0x000F6CE8
	public override void PlaySe(ServerWheelOptionsData.SE_TYPE seType, float delay = 0f)
	{
		if (this.m_init && EventManager.Instance != null)
		{
			string text = null;
			string cueSheetName = "SE";
			EventManager.EventType type = EventManager.Instance.Type;
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
				if (!RouletteUtility.isTutorial || this.m_category != RouletteCategory.PREMIUM)
				{
					switch (base.category)
					{
					case RouletteCategory.PREMIUM:
					case RouletteCategory.SPECIAL:
						if ((base.category == RouletteCategory.PREMIUM || base.category == RouletteCategory.SPECIAL) && type != EventManager.EventType.NUM && type != EventManager.EventType.UNKNOWN && EventManager.Instance.IsInEvent())
						{
							string data = EventCommonDataTable.Instance.GetData(EventCommonDataItem.RouletteDecide_SeCueName);
							if (!string.IsNullOrEmpty(data))
							{
								cueSheetName = "SE_" + EventManager.GetEventTypeName(EventManager.Instance.Type);
								text = data;
							}
						}
						goto IL_158;
					}
					text = "sys_menu_decide";
				}
				IL_158:
				if (string.IsNullOrEmpty(text))
				{
					text = "sys_menu_decide";
				}
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
				if (!RouletteUtility.isTutorial || this.m_category != RouletteCategory.PREMIUM)
				{
					switch (base.category)
					{
					case RouletteCategory.PREMIUM:
					case RouletteCategory.SPECIAL:
						if ((base.category == RouletteCategory.PREMIUM || base.category == RouletteCategory.SPECIAL) && type != EventManager.EventType.NUM && type != EventManager.EventType.UNKNOWN && EventManager.Instance.IsInEvent())
						{
							string data2 = EventCommonDataTable.Instance.GetData(EventCommonDataItem.RouletteChange_SeCueName);
							if (!string.IsNullOrEmpty(data2))
							{
								cueSheetName = "SE_" + EventManager.GetEventTypeName(EventManager.Instance.Type);
								text = data2;
							}
						}
						goto IL_28D;
					}
					text = "sys_roulette_change";
				}
				IL_28D:
				if (string.IsNullOrEmpty(text))
				{
					text = "sys_roulette_change";
				}
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (delay <= 0f)
				{
					SoundManager.SePlay(text, cueSheetName);
				}
				else
				{
					RouletteManager.PlaySe(text, delay, cueSheetName);
				}
			}
		}
	}

	// Token: 0x06002846 RID: 10310 RVA: 0x000F8DC4 File Offset: 0x000F6FC4
	public override ServerWheelOptionsData.SPIN_BUTTON GetSpinButtonSeting(out int count, out bool btnActive)
	{
		ServerWheelOptionsData.SPIN_BUTTON spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.NONE;
		count = 0;
		btnActive = false;
		if (this.m_init && SaveDataManager.Instance != null && SaveDataManager.Instance.ItemData != null && this.m_orgData != null)
		{
			if (RouletteUtility.isTutorial && this.m_category == RouletteCategory.PREMIUM)
			{
				count = -1;
				spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.FREE;
				btnActive = true;
			}
			else
			{
				int currentCostItemId = this.m_orgData.GetCurrentCostItemId();
				int multi = this.m_orgData.multi;
				int costItemNum = this.m_orgData.GetCostItemNum(currentCostItemId);
				count = this.m_orgData.GetCostItemCost(currentCostItemId) * multi;
				spin_BUTTON = this.m_orgData.GetSpinButton();
				if (costItemNum >= count)
				{
					btnActive = true;
				}
				if (spin_BUTTON == ServerWheelOptionsData.SPIN_BUTTON.FREE)
				{
					btnActive = true;
					count = this.m_orgData.remainingFree;
				}
			}
		}
		return spin_BUTTON;
	}

	// Token: 0x06002847 RID: 10311 RVA: 0x000F8E94 File Offset: 0x000F7094
	public override ServerWheelOptionsData.SPIN_BUTTON GetSpinButtonSeting()
	{
		ServerWheelOptionsData.SPIN_BUTTON result = ServerWheelOptionsData.SPIN_BUTTON.NONE;
		if (this.m_init && SaveDataManager.Instance != null && SaveDataManager.Instance.ItemData != null && this.m_orgData != null)
		{
			if (RouletteUtility.isTutorial && this.m_category == RouletteCategory.PREMIUM)
			{
				result = ServerWheelOptionsData.SPIN_BUTTON.FREE;
			}
			else
			{
				result = this.m_orgData.GetSpinButton();
			}
		}
		return result;
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x000F8F04 File Offset: 0x000F7104
	public override List<int> GetSpinCostItemIdList()
	{
		List<int> result = null;
		if (this.m_orgData != null)
		{
			result = this.m_orgData.GetCostItemList();
		}
		return result;
	}

	// Token: 0x06002849 RID: 10313 RVA: 0x000F8F2C File Offset: 0x000F712C
	public override int GetSpinCostItemCost(int costItemId)
	{
		int result = 0;
		if (this.m_orgData != null)
		{
			if (costItemId <= 0)
			{
				result = 1;
			}
			else
			{
				result = this.m_orgData.GetCostItemCost(costItemId);
			}
		}
		return result;
	}

	// Token: 0x0600284A RID: 10314 RVA: 0x000F8F64 File Offset: 0x000F7164
	public override int GetSpinCostItemNum(int costItemId)
	{
		int result = 0;
		if (this.m_orgData != null)
		{
			if (costItemId <= 0)
			{
				result = this.m_orgData.remainingFree;
			}
			else
			{
				result = this.m_orgData.GetCostItemNum(costItemId);
			}
		}
		return result;
	}

	// Token: 0x0600284B RID: 10315 RVA: 0x000F8FA4 File Offset: 0x000F71A4
	public override bool ChangeSpinCost(int selectIndex)
	{
		bool result = false;
		if (this.IsChangeSpinCost())
		{
			result = this.m_orgData.ChangeCostItem(selectIndex);
		}
		return result;
	}

	// Token: 0x0600284C RID: 10316 RVA: 0x000F8FCC File Offset: 0x000F71CC
	public override bool IsChangeSpinCost()
	{
		bool result = false;
		if (this.m_orgData.GetSpinButton() != ServerWheelOptionsData.SPIN_BUTTON.FREE)
		{
			List<int> spinCostItemIdList = this.GetSpinCostItemIdList();
			if (spinCostItemIdList.Count > 1)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600284D RID: 10317 RVA: 0x000F9004 File Offset: 0x000F7204
	public override int GetSpinCostCurrentIndex()
	{
		return this.m_orgData.currentCostSelect;
	}

	// Token: 0x0600284E RID: 10318 RVA: 0x000F9014 File Offset: 0x000F7214
	public override bool GetEggSeting(out int count)
	{
		bool result = false;
		count = RouletteManager.Instance.specialEgg;
		if (this.m_init && this.m_orgData != null && base.category != RouletteCategory.SPECIAL && base.category != RouletteCategory.RAID)
		{
			result = true;
			count = RouletteManager.Instance.specialEgg;
		}
		return result;
	}

	// Token: 0x0600284F RID: 10319 RVA: 0x000F906C File Offset: 0x000F726C
	public override ServerWheelOptions GetOrgRankupData()
	{
		return null;
	}

	// Token: 0x06002850 RID: 10320 RVA: 0x000F9070 File Offset: 0x000F7270
	public override ServerChaoWheelOptions GetOrgNormalData()
	{
		return null;
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x000F9074 File Offset: 0x000F7274
	public override ServerWheelOptionsGeneral GetOrgGeneralData()
	{
		return this.m_orgData;
	}

	// Token: 0x06002852 RID: 10322 RVA: 0x000F907C File Offset: 0x000F727C
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
		Dictionary<long, float> dictionary3 = new Dictionary<long, float>();
		bool flag = false;
		ServerCampaignState serverCampaignState = null;
		if (this.m_orgData != null)
		{
			int num = 0;
			if (base.IsCampaign(Constants.Campaign.emType.PremiumRouletteOdds) && base.category == RouletteCategory.PREMIUM)
			{
				flag = true;
				serverCampaignState = ServerInterface.CampaignState;
				for (int i = 0; i < this.m_orgData.itemLenght; i++)
				{
					ServerCampaignData campaignInSession = serverCampaignState.GetCampaignInSession(Constants.Campaign.emType.PremiumRouletteOdds, i);
					if (campaignInSession != null)
					{
						num += campaignInSession.iContent;
					}
				}
			}
			for (int j = 0; j < this.m_orgData.itemLenght; j++)
			{
				int num2;
				int num3;
				float num4;
				this.m_orgData.GetCell(j, out num2, out num3, out num4);
				long num5 = (long)num2;
				float num6 = num4;
				float num7 = num4;
				int num8 = num3;
				num5 = num5 * 100000L + (long)num8;
				if (flag && flag && serverCampaignState != null)
				{
					float num9 = -1f;
					ServerCampaignData campaignInSession2 = serverCampaignState.GetCampaignInSession(Constants.Campaign.emType.PremiumRouletteOdds, j);
					if (campaignInSession2 != null)
					{
						num9 = (float)campaignInSession2.iContent;
					}
					if (num9 >= 0f)
					{
						num7 = (float)Mathf.RoundToInt(num9 / (float)num * 10000f) / 100f;
					}
				}
				if (!list.Contains(num5))
				{
					list.Add(num5);
				}
				if (!dictionary.ContainsKey(num5))
				{
					dictionary.Add(num5, num7);
				}
				else
				{
					Dictionary<long, float> dictionary5;
					Dictionary<long, float> dictionary4 = dictionary5 = dictionary;
					long key2;
					long key = key2 = num5;
					float num10 = dictionary5[key2];
					dictionary4[key] = num10 + num7;
				}
				if (!dictionary3.ContainsKey(num5))
				{
					dictionary3.Add(num5, num6);
				}
				else
				{
					Dictionary<long, float> dictionary7;
					Dictionary<long, float> dictionary6 = dictionary7 = dictionary3;
					long key2;
					long key3 = key2 = num5;
					float num10 = dictionary7[key2];
					dictionary6[key3] = num10 + num6;
				}
				if (!dictionary2.ContainsKey(num5))
				{
					dictionary2.Add(num5, num8);
				}
				else
				{
					dictionary2[num5] = num8;
				}
			}
			list.Sort();
			for (int k = 0; k < list.Count; k++)
			{
				List<string> list2 = new List<string>();
				long num11 = list[k];
				float num12 = dictionary3[num11];
				float num13 = dictionary[num11];
				int num14 = dictionary2[num11];
				int id = (int)(num11 / 100000L);
				ServerItem serverItem = new ServerItem((ServerItem.Id)id);
				string str = string.Empty;
				string str2 = string.Empty;
				if (serverItem.idType == ServerItem.IdType.CHARA)
				{
					str = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Roulette", "ui_Lbl_rarity_100").text;
				}
				else if (serverItem.idType == ServerItem.IdType.CHAO)
				{
					int id2 = (int)serverItem.id;
					int num15 = id2 / 1000 % 10;
					str = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Roulette", "ui_Lbl_rarity_" + num15).text;
				}
				else
				{
					switch (serverItem.id)
					{
					case ServerItem.Id.BIG:
					case ServerItem.Id.SUPER:
					case ServerItem.Id.JACKPOT:
						str = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "reward_" + serverItem.id.ToString().ToLower()).text;
						break;
					default:
						str = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "RewardType", "reward_type_" + (int)serverItem.rewardType).text;
						str2 = " x " + num14;
						break;
					}
				}
				list2.Add(str + str2);
				string format = "F" + RouletteUtility.OddsDisplayDecimal.ToString();
				string text = string.Empty;
				if (num13 != num12)
				{
					text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "odds").text.Replace("{ODDS}", num13.ToString(format));
					float num16 = num13 - num12;
					string text2 = num16.ToString(format);
					string cellName = string.Empty;
					if (num16 > 0f)
					{
						text2 = "+" + text2;
						cellName = "campaign_odds_up";
					}
					else
					{
						cellName = "campaign_odds_down";
					}
					string str3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", cellName).text.Replace("{ODDS}", text2);
					text += str3;
				}
				else
				{
					text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "odds").text.Replace("{ODDS}", num13.ToString(format));
				}
				list2.Add(text);
				this.m_itemOdds.Add(num11, list2.ToArray());
			}
		}
		return this.m_itemOdds;
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x000F954C File Offset: 0x000F774C
	public override string ShowSpinErrorWindow()
	{
		ServerWheelOptionsData.SPIN_BUTTON spinButtonSeting = this.GetSpinButtonSeting();
		string result = null;
		switch (spinButtonSeting)
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
		case ServerWheelOptionsData.SPIN_BUTTON.RSRING:
		{
			result = "SpinCostErrorRSRing";
			bool flag = ServerInterface.IsRSREnable();
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextUtility.GetCommonText("ChaoRoulette", "gw_cost_caption"),
				message = ((!flag) ? TextUtility.GetCommonText("ChaoRoulette", "gw_cost_caption_text_2") : TextUtility.GetCommonText("ChaoRoulette", "gw_cost_caption_text")),
				buttonType = ((!flag) ? GeneralWindow.ButtonType.Ok : GeneralWindow.ButtonType.ShopCancel),
				name = "SpinCostErrorRSRing",
				isPlayErrorSe = true
			});
			return result;
		}
		case ServerWheelOptionsData.SPIN_BUTTON.RAID:
			result = "SpinCostErrorRaidRing";
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextUtility.GetCommonText("Roulette", "gw_raid_cost_caption"),
				message = TextUtility.GetCommonText("Roulette", "gw_raid_cost_caption_text"),
				buttonType = GeneralWindow.ButtonType.Ok,
				name = "SpinCostErrorRaidRing",
				isPlayErrorSe = true
			});
			return result;
		}
		global::Debug.Log("ServerWheelOptionsRankup ShowSpinErrorWindow error !!!");
		return result;
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x000F9758 File Offset: 0x000F7958
	public override List<ServerItem> GetAttentionItemList()
	{
		List<ServerItem> result = null;
		if (base.category == RouletteCategory.RAID && RouletteManager.Instance != null)
		{
			ServerPrizeState prizeList = RouletteManager.Instance.GetPrizeList(base.category);
			if (prizeList != null)
			{
				result = prizeList.GetAttentionList();
			}
		}
		return result;
	}

	// Token: 0x06002855 RID: 10325 RVA: 0x000F97A4 File Offset: 0x000F79A4
	public override bool IsPrizeDataList()
	{
		return true;
	}

	// Token: 0x04002409 RID: 9225
	private ServerWheelOptionsGeneral m_orgData;
}
