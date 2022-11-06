using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000829 RID: 2089
public class ServerWheelOptionsGeneral
{
	// Token: 0x06003818 RID: 14360 RVA: 0x001282CC File Offset: 0x001264CC
	public ServerWheelOptionsGeneral()
	{
		this.m_itemId = new List<int>();
		this.m_itemNum = new List<int>();
		this.m_itemWeight = new List<int>();
		this.m_costItem = new Dictionary<int, long>();
		for (int i = 0; i < 8; i++)
		{
			if (i == 0)
			{
				this.m_itemId.Add(200000);
			}
			else
			{
				this.m_itemId.Add(120000 + i - 1);
			}
			this.m_itemNum.Add(1);
			this.m_itemWeight.Add(1);
		}
		this.m_remaining = 0;
		this.m_jackpotRing = 0;
		this.m_rank = 0;
		this.m_multi = 1;
		this.m_nextFreeSpin = DateTime.Now;
		this.m_nextFreeSpin = this.m_nextFreeSpin.AddDays(999.0);
	}

	// Token: 0x1700085C RID: 2140
	// (get) Token: 0x06003819 RID: 14361 RVA: 0x001283AC File Offset: 0x001265AC
	// (set) Token: 0x0600381A RID: 14362 RVA: 0x001283B4 File Offset: 0x001265B4
	public int currentCostSelect
	{
		get
		{
			return this.m_currentCostSelect;
		}
		private set
		{
			this.m_currentCostSelect = value;
		}
	}

	// Token: 0x1700085D RID: 2141
	// (get) Token: 0x0600381B RID: 14363 RVA: 0x001283C0 File Offset: 0x001265C0
	// (set) Token: 0x0600381C RID: 14364 RVA: 0x001283C8 File Offset: 0x001265C8
	public int multi
	{
		get
		{
			return this.m_multi;
		}
		private set
		{
			this.m_multi = value;
		}
	}

	// Token: 0x1700085E RID: 2142
	// (get) Token: 0x0600381D RID: 14365 RVA: 0x001283D4 File Offset: 0x001265D4
	public int itemLenght
	{
		get
		{
			if (this.m_itemId == null)
			{
				return 0;
			}
			return this.m_itemId.Count;
		}
	}

	// Token: 0x1700085F RID: 2143
	// (get) Token: 0x0600381E RID: 14366 RVA: 0x001283F0 File Offset: 0x001265F0
	public int rouletteId
	{
		get
		{
			return this.m_rouletteId;
		}
	}

	// Token: 0x17000860 RID: 2144
	// (get) Token: 0x0600381F RID: 14367 RVA: 0x001283F8 File Offset: 0x001265F8
	public int remainingTicketTotal
	{
		get
		{
			return this.GetRemainingTicket();
		}
	}

	// Token: 0x17000861 RID: 2145
	// (get) Token: 0x06003820 RID: 14368 RVA: 0x00128400 File Offset: 0x00126600
	public int remainingFree
	{
		get
		{
			return this.m_remaining - this.GetRemainingTicket();
		}
	}

	// Token: 0x17000862 RID: 2146
	// (get) Token: 0x06003821 RID: 14369 RVA: 0x00128410 File Offset: 0x00126610
	public int jackpotRing
	{
		get
		{
			return this.m_jackpotRing;
		}
	}

	// Token: 0x17000863 RID: 2147
	// (get) Token: 0x06003822 RID: 14370 RVA: 0x00128418 File Offset: 0x00126618
	// (set) Token: 0x06003823 RID: 14371 RVA: 0x00128420 File Offset: 0x00126620
	public int spEgg
	{
		get
		{
			return this.m_spEgg;
		}
		set
		{
			this.m_spEgg = value;
		}
	}

	// Token: 0x17000864 RID: 2148
	// (get) Token: 0x06003824 RID: 14372 RVA: 0x0012842C File Offset: 0x0012662C
	public DateTime nextFreeSpin
	{
		get
		{
			return this.m_nextFreeSpin;
		}
	}

	// Token: 0x17000865 RID: 2149
	// (get) Token: 0x06003825 RID: 14373 RVA: 0x00128434 File Offset: 0x00126634
	public RouletteUtility.WheelType type
	{
		get
		{
			return (!this.isRankup) ? RouletteUtility.WheelType.Normal : RouletteUtility.WheelType.Rankup;
		}
	}

	// Token: 0x17000866 RID: 2150
	// (get) Token: 0x06003826 RID: 14374 RVA: 0x00128448 File Offset: 0x00126648
	public RouletteUtility.WheelRank rank
	{
		get
		{
			return RouletteUtility.GetRouletteRank(this.m_rank);
		}
	}

	// Token: 0x17000867 RID: 2151
	// (get) Token: 0x06003827 RID: 14375 RVA: 0x00128458 File Offset: 0x00126658
	public int patternType
	{
		get
		{
			if (this.m_patternType < 0)
			{
				return this.GetRoulettePatternType();
			}
			return this.m_patternType;
		}
	}

	// Token: 0x17000868 RID: 2152
	// (get) Token: 0x06003828 RID: 14376 RVA: 0x00128474 File Offset: 0x00126674
	public string spriteNameBg
	{
		get
		{
			return RouletteUtility.GetRouletteBgSpriteName(this);
		}
	}

	// Token: 0x17000869 RID: 2153
	// (get) Token: 0x06003829 RID: 14377 RVA: 0x0012847C File Offset: 0x0012667C
	public string spriteNameBoard
	{
		get
		{
			return RouletteUtility.GetRouletteBoardSpriteName(this);
		}
	}

	// Token: 0x1700086A RID: 2154
	// (get) Token: 0x0600382A RID: 14378 RVA: 0x00128484 File Offset: 0x00126684
	public string spriteNameArrow
	{
		get
		{
			return RouletteUtility.GetRouletteArrowSpriteName(this);
		}
	}

	// Token: 0x1700086B RID: 2155
	// (get) Token: 0x0600382B RID: 14379 RVA: 0x0012848C File Offset: 0x0012668C
	public string spriteNameCostItem
	{
		get
		{
			return RouletteUtility.GetRouletteCostItemName(this.GetCurrentCostItemId());
		}
	}

	// Token: 0x1700086C RID: 2156
	// (get) Token: 0x0600382C RID: 14380 RVA: 0x0012849C File Offset: 0x0012669C
	public bool isRankup
	{
		get
		{
			bool result = false;
			if (this.m_rank > 0)
			{
				result = true;
			}
			else
			{
				foreach (int id in this.m_itemId)
				{
					ServerItem serverItem = new ServerItem((ServerItem.Id)id);
					if (serverItem.idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}
	}

	// Token: 0x0600382D RID: 14381 RVA: 0x00128530 File Offset: 0x00126730
	private int GetRoulettePatternType()
	{
		int num = 0;
		int num2 = 9999999;
		foreach (int num3 in this.m_itemWeight)
		{
			if (num < num3)
			{
				num = num3;
			}
			if (num2 > num3)
			{
				num2 = num3;
			}
		}
		int num4;
		if ((float)num2 / (float)num < 0.35f)
		{
			num4 = 0;
		}
		else
		{
			num4 = 1;
		}
		this.m_patternType = num4;
		return num4;
	}

	// Token: 0x0600382E RID: 14382 RVA: 0x001285D0 File Offset: 0x001267D0
	public ServerWheelOptionsData.SPIN_BUTTON GetSpinButton()
	{
		ServerWheelOptionsData.SPIN_BUTTON spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.NONE;
		if (this.remainingFree > 0)
		{
			spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.FREE;
		}
		else if (spin_BUTTON == ServerWheelOptionsData.SPIN_BUTTON.NONE)
		{
			int currentCostItemId = this.GetCurrentCostItemId();
			if (currentCostItemId != 900000)
			{
				if (currentCostItemId != 910000)
				{
					if (currentCostItemId != 960000)
					{
						spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.TICKET;
					}
					else
					{
						spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.RAID;
					}
				}
				else
				{
					spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.RING;
				}
			}
			else
			{
				spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.RSRING;
			}
		}
		return spin_BUTTON;
	}

	// Token: 0x0600382F RID: 14383 RVA: 0x00128644 File Offset: 0x00126844
	public RouletteUtility.CellType GetCell(int index)
	{
		RouletteUtility.CellType result = RouletteUtility.CellType.Item;
		if (index >= 0 && index < this.itemLenght)
		{
			int num = this.m_itemId[index];
			if (num >= 0)
			{
				ServerItem serverItem = new ServerItem((ServerItem.Id)num);
				ServerItem.IdType idType = serverItem.idType;
				if (idType != ServerItem.IdType.ITEM_ROULLETE_WIN)
				{
					if (idType != ServerItem.IdType.CHARA && idType != ServerItem.IdType.CHAO)
					{
						result = RouletteUtility.CellType.Item;
					}
					else
					{
						result = RouletteUtility.CellType.Egg;
					}
				}
				else
				{
					result = RouletteUtility.CellType.Rankup;
				}
			}
		}
		return result;
	}

	// Token: 0x06003830 RID: 14384 RVA: 0x001286C0 File Offset: 0x001268C0
	public float GetCellWeight(int index)
	{
		float result = 0f;
		if (index >= 0 && index < this.itemLenght)
		{
			result = (float)this.m_itemWeight[index];
		}
		return result;
	}

	// Token: 0x06003831 RID: 14385 RVA: 0x001286F8 File Offset: 0x001268F8
	public RouletteUtility.CellType GetCell(int index, out int itemId, out int itemNum, out float itemRate)
	{
		RouletteUtility.CellType result = RouletteUtility.CellType.Item;
		itemId = 0;
		itemNum = 0;
		itemRate = 0f;
		if (index >= 0 && index < this.itemLenght)
		{
			int num = this.m_itemId[index];
			int num2 = this.m_itemNum[index];
			if (num >= 0)
			{
				itemId = num;
				itemNum = num2;
				itemRate = this.GetItemRate(index);
				ServerItem serverItem = new ServerItem((ServerItem.Id)num);
				ServerItem.IdType idType = serverItem.idType;
				if (idType != ServerItem.IdType.ITEM_ROULLETE_WIN)
				{
					if (idType != ServerItem.IdType.CHARA && idType != ServerItem.IdType.CHAO)
					{
						result = RouletteUtility.CellType.Item;
					}
					else
					{
						result = RouletteUtility.CellType.Egg;
						itemNum = 0;
					}
				}
				else
				{
					result = RouletteUtility.CellType.Rankup;
					itemNum = 0;
				}
			}
		}
		return result;
	}

	// Token: 0x06003832 RID: 14386 RVA: 0x001287A8 File Offset: 0x001269A8
	private float GetItemRate(int index)
	{
		float result = 0f;
		if (index >= 0 && index < this.itemLenght)
		{
			int itemMaxWeightIndex = this.GetItemMaxWeightIndex();
			int itemTotalWeight = this.GetItemTotalWeight();
			int num = this.m_itemWeight[index];
			if (itemTotalWeight > 0 && num > 0)
			{
				if (itemMaxWeightIndex < 0 || index != itemMaxWeightIndex)
				{
					result = Mathf.Round((float)num / (float)itemTotalWeight * 10000f) / 100f;
				}
				else
				{
					float num2 = 0f;
					for (int i = 0; i < this.m_itemWeight.Count; i++)
					{
						if (i != itemMaxWeightIndex)
						{
							num2 += Mathf.Round((float)num / (float)itemTotalWeight * 10000f) / 100f;
						}
					}
					result = 100f - num2;
				}
			}
		}
		return result;
	}

	// Token: 0x06003833 RID: 14387 RVA: 0x00128878 File Offset: 0x00126A78
	private int GetItemTotalWeight()
	{
		int num = 0;
		for (int i = 0; i < this.m_itemWeight.Count; i++)
		{
			num += this.m_itemWeight[i];
		}
		return num;
	}

	// Token: 0x06003834 RID: 14388 RVA: 0x001288B4 File Offset: 0x00126AB4
	private int GetItemMaxWeightIndex()
	{
		int result = -1;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.m_itemWeight.Count; i++)
		{
			int num3 = num;
			if (this.m_itemWeight[i] > num)
			{
				num = this.m_itemWeight[i];
				result = i;
			}
			if (num3 != num)
			{
				num2++;
			}
		}
		if (num2 <= 1)
		{
			result = -1;
		}
		return result;
	}

	// Token: 0x06003835 RID: 14389 RVA: 0x00128920 File Offset: 0x00126B20
	public void SetupItem(int index, int itemId, int weight, int num = 0)
	{
		if (this.m_itemId != null && this.m_itemNum != null && this.m_itemWeight != null && index >= 0)
		{
			if (index < this.m_itemId.Count)
			{
				this.m_itemId[index] = itemId;
				this.m_itemNum[index] = num;
				this.m_itemWeight[index] = weight;
			}
			else
			{
				int num2 = index - this.m_itemId.Count + 1;
				for (int i = 0; i < num2; i++)
				{
					this.m_itemId.Add(itemId);
					this.m_itemNum.Add(num);
					this.m_itemWeight.Add(weight);
				}
			}
		}
	}

	// Token: 0x06003836 RID: 14390 RVA: 0x001289DC File Offset: 0x00126BDC
	public void ResetupCostItem()
	{
		if (this.m_costItem != null)
		{
			this.m_costItem.Clear();
		}
		this.m_costItem = new Dictionary<int, long>();
	}

	// Token: 0x06003837 RID: 14391 RVA: 0x00128A00 File Offset: 0x00126C00
	public void AddCostItem(int itemId, int itemNum, int oneCost = 1)
	{
		if (this.m_costItem == null)
		{
			this.m_costItem = new Dictionary<int, long>();
		}
		global::Debug.Log(string.Concat(new object[]
		{
			"ServerWheelOptionsGeneral AddCostItem  itemId:",
			itemId,
			"  itemNum:",
			itemNum,
			"  oneCost:",
			oneCost
		}));
		if (this.m_costItem.ContainsKey(itemId))
		{
			Dictionary<int, long> costItem;
			Dictionary<int, long> dictionary = costItem = this.m_costItem;
			long num = costItem[itemId];
			dictionary[itemId] = num + (long)itemNum;
		}
		else
		{
			this.m_costItem.Add(itemId, (long)itemNum + 10000000L * (long)oneCost);
		}
		GeneralUtil.SetItemCount((ServerItem.Id)itemId, this.m_costItem[itemId] % 10000000L);
	}

	// Token: 0x06003838 RID: 14392 RVA: 0x00128ACC File Offset: 0x00126CCC
	public void CopyToCostItem(Dictionary<int, long> items)
	{
		if (this.m_costItem == null)
		{
			this.m_costItem = new Dictionary<int, long>();
		}
		else
		{
			this.m_costItem.Clear();
			this.m_costItem = new Dictionary<int, long>();
		}
		if (items != null && items.Count > 0)
		{
			Dictionary<int, long>.KeyCollection keys = items.Keys;
			foreach (int key in keys)
			{
				this.m_costItem.Add(key, items[key]);
			}
		}
	}

	// Token: 0x06003839 RID: 14393 RVA: 0x00128B84 File Offset: 0x00126D84
	public List<int> GetCostItemList()
	{
		List<int> list = null;
		if (this.m_costItem != null && this.m_costItem.Count > 0)
		{
			list = new List<int>();
			Dictionary<int, long>.KeyCollection keys = this.m_costItem.Keys;
			foreach (int item in keys)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x0600383A RID: 14394 RVA: 0x00128C18 File Offset: 0x00126E18
	public int GetCostItemNum(int costItemId)
	{
		int result = -1;
		if (this.m_costItem != null && this.m_costItem.Count > 0 && this.m_costItem.ContainsKey(costItemId))
		{
			result = (int)GeneralUtil.GetItemCount((ServerItem.Id)costItemId);
		}
		return result;
	}

	// Token: 0x0600383B RID: 14395 RVA: 0x00128C60 File Offset: 0x00126E60
	public int GetCostItemCost(int costItemId)
	{
		int result = -1;
		if (this.m_costItem != null && this.m_costItem.Count > 0 && this.m_costItem.ContainsKey(costItemId))
		{
			result = (int)(this.m_costItem[costItemId] / 10000000L);
		}
		return result;
	}

	// Token: 0x0600383C RID: 14396 RVA: 0x00128CB4 File Offset: 0x00126EB4
	public int GetDefultCostItemId()
	{
		int result = -1;
		List<int> costItemList = this.GetCostItemList();
		if (costItemList != null && costItemList.Count > 0)
		{
			result = costItemList[0];
		}
		return result;
	}

	// Token: 0x0600383D RID: 14397 RVA: 0x00128CE8 File Offset: 0x00126EE8
	public int GetCurrentCostItemId()
	{
		int num = -1;
		List<int> costItemList = this.GetCostItemList();
		if (costItemList != null && costItemList.Count > 0)
		{
			if (this.m_currentCostSelect <= 0)
			{
				for (int i = 0; i < costItemList.Count; i++)
				{
					int costItemNum = this.GetCostItemNum(costItemList[i]);
					int costItemCost = this.GetCostItemCost(costItemList[i]);
					if (costItemNum >= costItemCost)
					{
						num = costItemList[i];
						break;
					}
				}
			}
			else if (this.m_currentCostSelect <= costItemList.Count)
			{
				for (int j = 0; j < costItemList.Count; j++)
				{
					int num2 = (this.m_currentCostSelect + j - 1) % costItemList.Count;
					if (num2 < costItemList.Count)
					{
						int costItemNum = this.GetCostItemNum(costItemList[num2]);
						int costItemCost = this.GetCostItemCost(costItemList[num2]);
						if (costItemNum >= costItemCost)
						{
							num = costItemList[num2];
							this.m_currentCostSelect = num2 + 1;
							break;
						}
					}
				}
			}
			if (num == -1)
			{
				num = costItemList[0];
			}
		}
		return num;
	}

	// Token: 0x0600383E RID: 14398 RVA: 0x00128E10 File Offset: 0x00127010
	public int GetCurrentCostItemNum()
	{
		int result = 0;
		int currentCostItemId = this.GetCurrentCostItemId();
		if (currentCostItemId > 0)
		{
			int costItemNum = this.GetCostItemNum(currentCostItemId);
			int costItemCost = this.GetCostItemCost(currentCostItemId);
			if (costItemNum >= costItemCost)
			{
				result = costItemNum / costItemCost;
			}
		}
		return result;
	}

	// Token: 0x0600383F RID: 14399 RVA: 0x00128E4C File Offset: 0x0012704C
	public bool ChangeCostItem(int selectIndex)
	{
		bool result = false;
		if (this.currentCostSelect == selectIndex)
		{
			return false;
		}
		if (selectIndex <= 0)
		{
			this.currentCostSelect = 0;
			return true;
		}
		List<int> costItemList = this.GetCostItemList();
		if (costItemList != null && costItemList.Count > 1)
		{
			if (costItemList.Count > selectIndex - 1)
			{
				int costItemId = costItemList[selectIndex - 1];
				int costItemCost = this.GetCostItemCost(costItemId);
				int costItemNum = this.GetCostItemNum(costItemId);
				if (costItemNum >= costItemCost)
				{
					this.currentCostSelect = selectIndex;
				}
				else
				{
					this.currentCostSelect = 0;
				}
			}
			else
			{
				this.currentCostSelect = 99;
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06003840 RID: 14400 RVA: 0x00128EE8 File Offset: 0x001270E8
	public bool ChangeMulti(int multi)
	{
		bool flag = this.IsMulti(multi);
		if (flag)
		{
			this.m_multi = multi;
			if (this.m_multi < 1)
			{
				this.m_multi = 1;
			}
		}
		else
		{
			this.m_multi = 1;
		}
		return flag;
	}

	// Token: 0x06003841 RID: 14401 RVA: 0x00128F2C File Offset: 0x0012712C
	public bool IsMulti(int multi)
	{
		bool result = false;
		if (multi <= 1)
		{
			result = true;
		}
		else
		{
			int currentCostItemId = this.GetCurrentCostItemId();
			ServerWheelOptionsData.SPIN_BUTTON spinButton = this.GetSpinButton();
			int costItemCost = this.GetCostItemCost(currentCostItemId);
			int num = 0;
			bool flag = true;
			switch (spinButton)
			{
			case ServerWheelOptionsData.SPIN_BUTTON.RING:
				num = (int)SaveDataManager.Instance.ItemData.RingCount;
				break;
			case ServerWheelOptionsData.SPIN_BUTTON.RSRING:
				num = (int)SaveDataManager.Instance.ItemData.RedRingCount;
				break;
			case ServerWheelOptionsData.SPIN_BUTTON.TICKET:
			case ServerWheelOptionsData.SPIN_BUTTON.RAID:
				num = this.GetCostItemNum(currentCostItemId);
				break;
			default:
				flag = false;
				break;
			}
			if (flag && num >= costItemCost * multi)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06003842 RID: 14402 RVA: 0x00128FDC File Offset: 0x001271DC
	public void SetupParam(int rouletteId, int remaining, int jackpotRing, int rank, int spEggNum, DateTime nextFree)
	{
		this.m_rouletteId = rouletteId;
		this.m_remaining = remaining;
		this.m_jackpotRing = jackpotRing;
		this.m_rank = rank;
		this.m_spEgg = spEggNum;
		this.m_nextFreeSpin = nextFree;
	}

	// Token: 0x06003843 RID: 14403 RVA: 0x0012900C File Offset: 0x0012720C
	public void SetupParam(int rouletteId, int remaining, int jackpotRing, int rank, int spEggNum)
	{
		this.m_rouletteId = rouletteId;
		this.m_remaining = remaining;
		this.m_jackpotRing = jackpotRing;
		this.m_rank = rank;
		this.m_spEgg = spEggNum;
	}

	// Token: 0x06003844 RID: 14404 RVA: 0x00129034 File Offset: 0x00127234
	public void CopyTo(ServerWheelOptionsGeneral to)
	{
		for (int i = 0; i < this.itemLenght; i++)
		{
			to.SetupItem(i, this.m_itemId[i], this.m_itemWeight[i], this.m_itemNum[i]);
		}
		to.SetupParam(this.m_rouletteId, this.m_remaining, this.m_jackpotRing, this.m_rank, this.m_spEgg, this.m_nextFreeSpin);
		to.CopyToCostItem(this.m_costItem);
	}

	// Token: 0x06003845 RID: 14405 RVA: 0x001290BC File Offset: 0x001272BC
	private int GetRemainingTicket()
	{
		int num = 0;
		if (this.m_costItem != null && this.m_costItem.Count > 0)
		{
			Dictionary<int, long>.KeyCollection keys = this.m_costItem.Keys;
			foreach (int num2 in keys)
			{
				if (num2 >= 240000 && num2 < 250000)
				{
					int num3 = (int)(this.m_costItem[num2] / 10000000L);
					int num4 = (int)(this.m_costItem[num2] % 10000000L);
					if (num4 >= num3)
					{
						num += num4 / num3;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x04002F75 RID: 12149
	private const long COST_ITEM_REQ_OFFSET = 10000000L;

	// Token: 0x04002F76 RID: 12150
	private int m_currentCostSelect;

	// Token: 0x04002F77 RID: 12151
	private List<int> m_itemId;

	// Token: 0x04002F78 RID: 12152
	private List<int> m_itemNum;

	// Token: 0x04002F79 RID: 12153
	private List<int> m_itemWeight;

	// Token: 0x04002F7A RID: 12154
	private int m_rouletteId;

	// Token: 0x04002F7B RID: 12155
	private int m_rank;

	// Token: 0x04002F7C RID: 12156
	private int m_jackpotRing;

	// Token: 0x04002F7D RID: 12157
	private int m_remaining;

	// Token: 0x04002F7E RID: 12158
	private int m_spEgg;

	// Token: 0x04002F7F RID: 12159
	private int m_multi;

	// Token: 0x04002F80 RID: 12160
	private Dictionary<int, long> m_costItem;

	// Token: 0x04002F81 RID: 12161
	private DateTime m_nextFreeSpin;

	// Token: 0x04002F82 RID: 12162
	private int m_patternType = -1;
}
