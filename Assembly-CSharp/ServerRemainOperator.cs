using System;
using System.Collections.Generic;

// Token: 0x0200081E RID: 2078
public class ServerRemainOperator
{
	// Token: 0x060037C3 RID: 14275 RVA: 0x00126558 File Offset: 0x00124758
	public ServerRemainOperator()
	{
		this.operatorData = 0;
		this.number = 0;
		this.ItemState = new Dictionary<int, ServerItemState>();
	}

	// Token: 0x1700083F RID: 2111
	// (get) Token: 0x060037C4 RID: 14276 RVA: 0x00126584 File Offset: 0x00124784
	// (set) Token: 0x060037C5 RID: 14277 RVA: 0x0012658C File Offset: 0x0012478C
	public int operatorData { get; set; }

	// Token: 0x17000840 RID: 2112
	// (get) Token: 0x060037C6 RID: 14278 RVA: 0x00126598 File Offset: 0x00124798
	// (set) Token: 0x060037C7 RID: 14279 RVA: 0x001265A0 File Offset: 0x001247A0
	public int number { get; set; }

	// Token: 0x17000841 RID: 2113
	// (get) Token: 0x060037C8 RID: 14280 RVA: 0x001265AC File Offset: 0x001247AC
	// (set) Token: 0x060037C9 RID: 14281 RVA: 0x001265B4 File Offset: 0x001247B4
	public Dictionary<int, ServerItemState> ItemState { get; private set; }

	// Token: 0x060037CA RID: 14282 RVA: 0x001265C0 File Offset: 0x001247C0
	public void CopyTo(ServerRemainOperator to)
	{
		to.operatorData = this.operatorData;
		to.number = this.number;
		to.ItemState.Clear();
		foreach (ServerItemState serverItemState in this.ItemState.Values)
		{
			to.ItemState.Add(serverItemState.m_itemId, serverItemState);
		}
	}

	// Token: 0x060037CB RID: 14283 RVA: 0x0012665C File Offset: 0x0012485C
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

	// Token: 0x060037CC RID: 14284 RVA: 0x001266BC File Offset: 0x001248BC
	public void Dump()
	{
	}
}
