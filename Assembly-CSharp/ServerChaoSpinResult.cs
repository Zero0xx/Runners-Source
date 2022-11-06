using System;
using System.Collections.Generic;

// Token: 0x020007F1 RID: 2033
public class ServerChaoSpinResult
{
	// Token: 0x0600365E RID: 13918 RVA: 0x00121288 File Offset: 0x0011F488
	public ServerChaoSpinResult()
	{
		this.AcquiredChaoData = null;
		this.IsRequiredChao = true;
		this.NumRequiredSpEggs = 0;
		this.ItemState = new Dictionary<int, ServerItemState>();
		this.ItemWon = 0;
		this.IsGotAlreadyChaoLevelMax = false;
	}

	// Token: 0x170007CB RID: 1995
	// (get) Token: 0x0600365F RID: 13919 RVA: 0x001212CC File Offset: 0x0011F4CC
	// (set) Token: 0x06003660 RID: 13920 RVA: 0x001212D4 File Offset: 0x0011F4D4
	public ServerChaoData AcquiredChaoData { get; set; }

	// Token: 0x170007CC RID: 1996
	// (get) Token: 0x06003661 RID: 13921 RVA: 0x001212E0 File Offset: 0x0011F4E0
	// (set) Token: 0x06003662 RID: 13922 RVA: 0x001212E8 File Offset: 0x0011F4E8
	public bool IsRequiredChao { get; set; }

	// Token: 0x170007CD RID: 1997
	// (get) Token: 0x06003663 RID: 13923 RVA: 0x001212F4 File Offset: 0x0011F4F4
	// (set) Token: 0x06003664 RID: 13924 RVA: 0x001212FC File Offset: 0x0011F4FC
	public int NumRequiredSpEggs { get; set; }

	// Token: 0x170007CE RID: 1998
	// (get) Token: 0x06003665 RID: 13925 RVA: 0x00121308 File Offset: 0x0011F508
	public bool IsRequiredSpEgg
	{
		get
		{
			return 0 < this.NumRequiredSpEggs;
		}
	}

	// Token: 0x170007CF RID: 1999
	// (get) Token: 0x06003666 RID: 13926 RVA: 0x00121314 File Offset: 0x0011F514
	// (set) Token: 0x06003667 RID: 13927 RVA: 0x0012131C File Offset: 0x0011F51C
	public Dictionary<int, ServerItemState> ItemState { get; private set; }

	// Token: 0x170007D0 RID: 2000
	// (get) Token: 0x06003668 RID: 13928 RVA: 0x00121328 File Offset: 0x0011F528
	// (set) Token: 0x06003669 RID: 13929 RVA: 0x00121330 File Offset: 0x0011F530
	public int ItemWon { get; set; }

	// Token: 0x170007D1 RID: 2001
	// (get) Token: 0x0600366A RID: 13930 RVA: 0x0012133C File Offset: 0x0011F53C
	// (set) Token: 0x0600366B RID: 13931 RVA: 0x00121344 File Offset: 0x0011F544
	public bool IsGotAlreadyChaoLevelMax { get; set; }

	// Token: 0x0600366C RID: 13932 RVA: 0x00121350 File Offset: 0x0011F550
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

	// Token: 0x0600366D RID: 13933 RVA: 0x001213B0 File Offset: 0x0011F5B0
	public void CopyTo(ServerChaoSpinResult to)
	{
		to.AcquiredChaoData = this.AcquiredChaoData;
		to.IsRequiredChao = this.IsRequiredChao;
		to.NumRequiredSpEggs = this.NumRequiredSpEggs;
		to.ItemState.Clear();
		foreach (ServerItemState serverItemState in this.ItemState.Values)
		{
			to.ItemState.Add(serverItemState.m_itemId, serverItemState);
		}
		to.ItemWon = this.ItemWon;
		to.IsGotAlreadyChaoLevelMax = this.IsGotAlreadyChaoLevelMax;
	}

	// Token: 0x0600366E RID: 13934 RVA: 0x00121470 File Offset: 0x0011F670
	public void Dump()
	{
	}
}
