using System;
using System.Collections.Generic;

// Token: 0x020006C0 RID: 1728
public class ServerDailyBattlePrizeData
{
	// Token: 0x06002E71 RID: 11889 RVA: 0x00111778 File Offset: 0x0010F978
	public ServerDailyBattlePrizeData()
	{
		this.operatorData = 0;
		this.number = 0;
		this.ItemState = new Dictionary<int, ServerItemState>();
	}

	// Token: 0x17000615 RID: 1557
	// (get) Token: 0x06002E72 RID: 11890 RVA: 0x0011179C File Offset: 0x0010F99C
	// (set) Token: 0x06002E73 RID: 11891 RVA: 0x001117A4 File Offset: 0x0010F9A4
	public Dictionary<int, ServerItemState> ItemState { get; private set; }

	// Token: 0x06002E74 RID: 11892 RVA: 0x001117B0 File Offset: 0x0010F9B0
	public void Dump()
	{
	}

	// Token: 0x06002E75 RID: 11893 RVA: 0x001117B4 File Offset: 0x0010F9B4
	public void AddItemState(ServerItemState itemState)
	{
		if (this.ItemState.ContainsKey(itemState.m_itemId))
		{
			this.ItemState[itemState.m_itemId].m_num += itemState.m_num;
		}
		else
		{
			this.ItemState.Add(itemState.m_itemId, itemState);
		}
	}

	// Token: 0x06002E76 RID: 11894 RVA: 0x00111814 File Offset: 0x0010FA14
	public void CopyTo(ServerDailyBattlePrizeData dest)
	{
		dest.operatorData = this.operatorData;
		dest.number = this.number;
		dest.ItemState.Clear();
		foreach (ServerItemState serverItemState in this.ItemState.Values)
		{
			dest.ItemState.Add(serverItemState.m_itemId, serverItemState);
		}
	}

	// Token: 0x06002E77 RID: 11895 RVA: 0x001118B0 File Offset: 0x0010FAB0
	public void CopyTo(ServerRemainOperator to)
	{
		to.operatorData = this.operatorData;
		to.number = this.number;
		to.ItemState.Clear();
		to.ItemState.Clear();
		foreach (ServerItemState serverItemState in this.ItemState.Values)
		{
			to.ItemState.Add(serverItemState.m_itemId, serverItemState);
		}
	}

	// Token: 0x06002E78 RID: 11896 RVA: 0x00111954 File Offset: 0x0010FB54
	public static List<ServerRemainOperator> ConvertRemainOperatorList(List<ServerDailyBattlePrizeData> prizeList)
	{
		if (prizeList == null || prizeList.Count <= 0)
		{
			return null;
		}
		List<ServerRemainOperator> list = new List<ServerRemainOperator>();
		foreach (ServerDailyBattlePrizeData serverDailyBattlePrizeData in prizeList)
		{
			ServerRemainOperator serverRemainOperator = new ServerRemainOperator();
			serverDailyBattlePrizeData.CopyTo(serverRemainOperator);
			list.Add(serverRemainOperator);
		}
		return list;
	}

	// Token: 0x04002A27 RID: 10791
	public int operatorData;

	// Token: 0x04002A28 RID: 10792
	public int number;
}
