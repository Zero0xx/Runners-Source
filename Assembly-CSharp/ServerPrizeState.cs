using System;
using System.Collections.Generic;
using Text;

// Token: 0x0200081B RID: 2075
public class ServerPrizeState
{
	// Token: 0x060037B3 RID: 14259 RVA: 0x00125E28 File Offset: 0x00124028
	public ServerPrizeState()
	{
		this.m_prizeList = new List<ServerPrizeData>();
		this.m_prizeText = null;
	}

	// Token: 0x060037B4 RID: 14260 RVA: 0x00125E44 File Offset: 0x00124044
	public ServerPrizeState(ServerWheelOptionsData data)
	{
		this.m_prizeList = null;
		this.m_prizeText = null;
		if (data.dataType == ServerWheelOptionsData.DATA_TYPE.RANKUP)
		{
			ServerWheelOptions orgRankupData = data.GetOrgRankupData();
			if (orgRankupData != null)
			{
				int num = orgRankupData.m_items.Length;
				for (int i = 0; i < num; i++)
				{
					this.AddPrize(new ServerPrizeData
					{
						itemId = orgRankupData.m_items[i],
						num = orgRankupData.m_itemQuantities[i],
						weight = orgRankupData.m_itemWeight[i]
					});
				}
			}
		}
	}

	// Token: 0x1700083E RID: 2110
	// (get) Token: 0x060037B5 RID: 14261 RVA: 0x00125ED4 File Offset: 0x001240D4
	public List<ServerPrizeData> prizeList
	{
		get
		{
			return this.m_prizeList;
		}
	}

	// Token: 0x060037B6 RID: 14262 RVA: 0x00125EDC File Offset: 0x001240DC
	public bool AddPrize(ServerPrizeData data)
	{
		if (this.m_prizeList == null)
		{
			this.m_prizeList = new List<ServerPrizeData>();
		}
		this.m_prizeList.Add(data);
		return true;
	}

	// Token: 0x060037B7 RID: 14263 RVA: 0x00125F04 File Offset: 0x00124104
	public void ResetPrizeList()
	{
		if (this.m_prizeList != null)
		{
			this.m_prizeList.Clear();
		}
		this.m_prizeList = new List<ServerPrizeData>();
	}

	// Token: 0x060037B8 RID: 14264 RVA: 0x00125F28 File Offset: 0x00124128
	public bool IsExpired()
	{
		return false;
	}

	// Token: 0x060037B9 RID: 14265 RVA: 0x00125F2C File Offset: 0x0012412C
	public bool IsData()
	{
		bool result = false;
		if (this.m_prizeList != null && this.m_prizeList.Count > 0)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060037BA RID: 14266 RVA: 0x00125F5C File Offset: 0x0012415C
	public List<string[]> GetItemOdds(ServerWheelOptionsData data)
	{
		return data.GetItemOdds();
	}

	// Token: 0x060037BB RID: 14267 RVA: 0x00125F64 File Offset: 0x00124164
	public string GetPrizeText(ServerWheelOptionsData data)
	{
		string result = null;
		RouletteCategory category = data.category;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL && category != RouletteCategory.GENERAL)
		{
			this.m_prizeText = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Roulette", "note_" + RouletteUtility.GetRouletteCategoryName(category)).text;
			string prizeList = RouletteUtility.GetPrizeList(this);
			result = this.m_prizeText.Replace("{PARAM}", prizeList);
		}
		return result;
	}

	// Token: 0x060037BC RID: 14268 RVA: 0x00125FD0 File Offset: 0x001241D0
	public List<ServerItem> GetAttentionList()
	{
		List<ServerItem> list = null;
		if (this.m_prizeList != null && this.m_prizeList.Count > 0)
		{
			List<ServerItem> list2 = new List<ServerItem>();
			List<ServerItem> list3 = new List<ServerItem>();
			foreach (ServerPrizeData serverPrizeData in this.m_prizeList)
			{
				ServerItem item = new ServerItem((ServerItem.Id)serverPrizeData.itemId);
				if (item.idType == ServerItem.IdType.CHARA)
				{
					bool flag = true;
					if (list2.Count > 0)
					{
						foreach (ServerItem serverItem in list2)
						{
							if (serverItem.id == item.id)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						list2.Add(item);
					}
				}
				else if (item.idType == ServerItem.IdType.CHAO && serverPrizeData.itemId >= 402000)
				{
					bool flag2 = true;
					if (list3.Count > 0)
					{
						foreach (ServerItem serverItem2 in list3)
						{
							if (serverItem2.id == item.id)
							{
								flag2 = false;
								break;
							}
						}
					}
					if (flag2)
					{
						list3.Add(item);
					}
				}
			}
			if (list2.Count > 0 || list3.Count > 0)
			{
				list = new List<ServerItem>();
				int num = 4;
				int num2 = list2.Count;
				int num3 = list3.Count;
				if (num2 > 2)
				{
					num2 = 2;
				}
				if (num3 > num - num2)
				{
					num3 = num - num2;
				}
				GeneralUtil.RandomList<ServerItem>(ref list2);
				GeneralUtil.RandomList<ServerItem>(ref list3);
				if (num2 > 0)
				{
					for (int i = 0; i < num2; i++)
					{
						list.Add(list2[i]);
					}
				}
				if (num3 > 0)
				{
					for (int j = 0; j < num3; j++)
					{
						list.Add(list3[j]);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060037BD RID: 14269 RVA: 0x0012625C File Offset: 0x0012445C
	public void CopyTo(ServerPrizeState to)
	{
		if (to == null || this.prizeList == null)
		{
			return;
		}
		if (this.prizeList.Count <= 0)
		{
			return;
		}
		to.ResetPrizeList();
		foreach (ServerPrizeData serverPrizeData in this.prizeList)
		{
			if (serverPrizeData != null)
			{
				to.AddPrize(serverPrizeData);
			}
		}
	}

	// Token: 0x04002F15 RID: 12053
	private List<ServerPrizeData> m_prizeList;

	// Token: 0x04002F16 RID: 12054
	private string m_prizeText;
}
