using System;
using System.Collections.Generic;

// Token: 0x0200051C RID: 1308
public abstract class ServerWheelOptionsOrg
{
	// Token: 0x17000548 RID: 1352
	// (get) Token: 0x06002800 RID: 10240 RVA: 0x000F82BC File Offset: 0x000F64BC
	public RouletteUtility.WheelType wheelType
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x17000549 RID: 1353
	// (get) Token: 0x06002801 RID: 10241 RVA: 0x000F82C4 File Offset: 0x000F64C4
	public RouletteCategory category
	{
		get
		{
			return this.m_category;
		}
	}

	// Token: 0x1700054A RID: 1354
	// (get) Token: 0x06002802 RID: 10242 RVA: 0x000F82CC File Offset: 0x000F64CC
	public virtual bool isValid
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700054B RID: 1355
	// (get) Token: 0x06002803 RID: 10243 RVA: 0x000F82D0 File Offset: 0x000F64D0
	public virtual bool isRemainingRefresh
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700054C RID: 1356
	// (get) Token: 0x06002804 RID: 10244 RVA: 0x000F82D4 File Offset: 0x000F64D4
	public virtual int itemWon
	{
		get
		{
			return -1;
		}
	}

	// Token: 0x1700054D RID: 1357
	// (get) Token: 0x06002805 RID: 10245 RVA: 0x000F82D8 File Offset: 0x000F64D8
	public virtual ServerItem itemWonData
	{
		get
		{
			return default(ServerItem);
		}
	}

	// Token: 0x1700054E RID: 1358
	// (get) Token: 0x06002806 RID: 10246 RVA: 0x000F82F0 File Offset: 0x000F64F0
	public virtual int rouletteId
	{
		get
		{
			return -1;
		}
	}

	// Token: 0x1700054F RID: 1359
	// (get) Token: 0x06002807 RID: 10247 RVA: 0x000F82F4 File Offset: 0x000F64F4
	public virtual int multi
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000550 RID: 1360
	// (get) Token: 0x06002808 RID: 10248 RVA: 0x000F82F8 File Offset: 0x000F64F8
	public virtual int numJackpotRing
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x06002809 RID: 10249
	public abstract void Setup(ServerChaoWheelOptions data);

	// Token: 0x0600280A RID: 10250
	public abstract void Setup(ServerWheelOptions data);

	// Token: 0x0600280B RID: 10251
	public abstract void Setup(ServerWheelOptionsGeneral data);

	// Token: 0x0600280C RID: 10252 RVA: 0x000F82FC File Offset: 0x000F64FC
	public virtual bool ChangeMulti(int multi)
	{
		return false;
	}

	// Token: 0x0600280D RID: 10253 RVA: 0x000F8300 File Offset: 0x000F6500
	public virtual bool IsMulti(int multi)
	{
		return multi == 1;
	}

	// Token: 0x0600280E RID: 10254
	public abstract int GetRouletteBoardPattern();

	// Token: 0x0600280F RID: 10255
	public abstract string GetRouletteArrowSprite();

	// Token: 0x06002810 RID: 10256
	public abstract string GetRouletteBgSprite();

	// Token: 0x06002811 RID: 10257
	public abstract string GetRouletteBoardSprite();

	// Token: 0x06002812 RID: 10258
	public abstract string GetRouletteTicketSprite();

	// Token: 0x06002813 RID: 10259
	public abstract RouletteUtility.WheelRank GetRouletteRank();

	// Token: 0x06002814 RID: 10260
	public abstract float GetCellWeight(int cellIndex);

	// Token: 0x06002815 RID: 10261
	public abstract int GetCellEgg(int cellIndex);

	// Token: 0x06002816 RID: 10262
	public abstract ServerItem GetCellItem(int cellIndex, out int num);

	// Token: 0x06002817 RID: 10263
	public abstract void PlayBgm(float delay = 0f);

	// Token: 0x06002818 RID: 10264
	public abstract void PlaySe(ServerWheelOptionsData.SE_TYPE seType, float delay = 0f);

	// Token: 0x06002819 RID: 10265
	public abstract ServerWheelOptionsData.SPIN_BUTTON GetSpinButtonSeting(out int count, out bool btnActive);

	// Token: 0x0600281A RID: 10266
	public abstract ServerWheelOptionsData.SPIN_BUTTON GetSpinButtonSeting();

	// Token: 0x0600281B RID: 10267 RVA: 0x000F830C File Offset: 0x000F650C
	public int GetSpinCostItemId()
	{
		int result = -1;
		switch (this.GetSpinButtonSeting())
		{
		case ServerWheelOptionsData.SPIN_BUTTON.FREE:
			result = 0;
			break;
		case ServerWheelOptionsData.SPIN_BUTTON.RING:
			result = 910000;
			break;
		case ServerWheelOptionsData.SPIN_BUTTON.RSRING:
			result = 900000;
			break;
		case ServerWheelOptionsData.SPIN_BUTTON.TICKET:
		{
			ServerWheelOptionsGeneral orgGeneralData = this.GetOrgGeneralData();
			if (orgGeneralData != null)
			{
				int currentCostItemId = orgGeneralData.GetCurrentCostItemId();
				if (currentCostItemId > 0)
				{
					int costItemNum = orgGeneralData.GetCostItemNum(currentCostItemId);
					int costItemCost = orgGeneralData.GetCostItemCost(currentCostItemId);
					if (costItemNum >= costItemCost)
					{
						result = currentCostItemId;
					}
				}
			}
			else
			{
				ServerWheelOptions orgRankupData = this.GetOrgRankupData();
				ServerChaoWheelOptions orgNormalData = this.GetOrgNormalData();
				if (orgRankupData != null)
				{
					if (orgRankupData.m_numRouletteToken > 0)
					{
						result = 240000;
					}
				}
				else if (orgNormalData != null)
				{
					result = 230000;
				}
			}
			break;
		}
		case ServerWheelOptionsData.SPIN_BUTTON.RAID:
			result = 960000;
			break;
		}
		return result;
	}

	// Token: 0x0600281C RID: 10268
	public abstract List<int> GetSpinCostItemIdList();

	// Token: 0x0600281D RID: 10269
	public abstract int GetSpinCostItemCost(int costItemId);

	// Token: 0x0600281E RID: 10270
	public abstract int GetSpinCostItemNum(int costItemId);

	// Token: 0x0600281F RID: 10271 RVA: 0x000F83E8 File Offset: 0x000F65E8
	public virtual int GetSpinCostCurrentIndex()
	{
		return 0;
	}

	// Token: 0x06002820 RID: 10272 RVA: 0x000F83EC File Offset: 0x000F65EC
	public virtual bool ChangeSpinCost(int selectIndex)
	{
		return false;
	}

	// Token: 0x06002821 RID: 10273 RVA: 0x000F83F0 File Offset: 0x000F65F0
	public virtual bool IsChangeSpinCost()
	{
		return false;
	}

	// Token: 0x06002822 RID: 10274
	public abstract bool GetEggSeting(out int count);

	// Token: 0x06002823 RID: 10275
	public abstract ServerWheelOptions GetOrgRankupData();

	// Token: 0x06002824 RID: 10276
	public abstract ServerChaoWheelOptions GetOrgNormalData();

	// Token: 0x06002825 RID: 10277
	public abstract ServerWheelOptionsGeneral GetOrgGeneralData();

	// Token: 0x06002826 RID: 10278 RVA: 0x000F83F4 File Offset: 0x000F65F4
	public List<Constants.Campaign.emType> GetCampaign()
	{
		return RouletteUtility.GetCampaign(this.category);
	}

	// Token: 0x06002827 RID: 10279 RVA: 0x000F8404 File Offset: 0x000F6604
	public bool IsCampaign()
	{
		bool result = false;
		List<Constants.Campaign.emType> campaign = this.GetCampaign();
		if (campaign != null)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06002828 RID: 10280 RVA: 0x000F8424 File Offset: 0x000F6624
	public bool IsCampaign(Constants.Campaign.emType campaign)
	{
		bool result = false;
		List<Constants.Campaign.emType> campaign2 = this.GetCampaign();
		if (campaign2 != null)
		{
			result = campaign2.Contains(campaign);
		}
		return result;
	}

	// Token: 0x06002829 RID: 10281
	public abstract Dictionary<long, string[]> UpdateItemWeights();

	// Token: 0x0600282A RID: 10282 RVA: 0x000F844C File Offset: 0x000F664C
	public List<string[]> GetItemOdds()
	{
		if (this.m_itemOdds == null)
		{
			this.UpdateItemWeights();
		}
		List<string[]> list = new List<string[]>();
		Dictionary<long, string[]>.KeyCollection keys = this.m_itemOdds.Keys;
		foreach (long key in keys)
		{
			list.Add(this.m_itemOdds[key]);
		}
		return list;
	}

	// Token: 0x0600282B RID: 10283
	public abstract string ShowSpinErrorWindow();

	// Token: 0x0600282C RID: 10284 RVA: 0x000F84E0 File Offset: 0x000F66E0
	public virtual List<ServerItem> GetAttentionItemList()
	{
		List<ServerItem> list = null;
		EventManager instance = EventManager.Instance;
		if (instance != null)
		{
			EyeCatcherChaoData[] eyeCatcherChaoDatas = instance.GetEyeCatcherChaoDatas();
			EyeCatcherCharaData[] eyeCatcherCharaDatas = instance.GetEyeCatcherCharaDatas();
			if (eyeCatcherCharaDatas != null)
			{
				foreach (EyeCatcherCharaData eyeCatcherCharaData in eyeCatcherCharaDatas)
				{
					ServerItem item = new ServerItem((ServerItem.Id)eyeCatcherCharaData.id);
					if (list == null)
					{
						list = new List<ServerItem>();
					}
					list.Add(item);
				}
			}
			if (eyeCatcherChaoDatas != null)
			{
				foreach (EyeCatcherChaoData eyeCatcherChaoData in eyeCatcherChaoDatas)
				{
					ServerItem item2 = new ServerItem(eyeCatcherChaoData.chao_id + ServerItem.Id.CHAO_BEGIN);
					if (list == null)
					{
						list = new List<ServerItem>();
					}
					list.Add(item2);
				}
			}
		}
		return list;
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x000F85AC File Offset: 0x000F67AC
	public virtual bool IsPrizeDataList()
	{
		return false;
	}

	// Token: 0x04002405 RID: 9221
	protected bool m_init;

	// Token: 0x04002406 RID: 9222
	protected RouletteUtility.WheelType m_type;

	// Token: 0x04002407 RID: 9223
	protected RouletteCategory m_category;

	// Token: 0x04002408 RID: 9224
	protected Dictionary<long, string[]> m_itemOdds;
}
