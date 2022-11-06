using System;
using System.Collections.Generic;

// Token: 0x020007FE RID: 2046
public class ServerFreeItemState
{
	// Token: 0x060036D1 RID: 14033 RVA: 0x00122354 File Offset: 0x00120554
	public ServerFreeItemState()
	{
		this.m_itemList = new List<ServerItemState>();
	}

	// Token: 0x170007FF RID: 2047
	// (get) Token: 0x060036D2 RID: 14034 RVA: 0x00122368 File Offset: 0x00120568
	public List<ServerItemState> itemList
	{
		get
		{
			return this.m_itemList;
		}
	}

	// Token: 0x060036D3 RID: 14035 RVA: 0x00122370 File Offset: 0x00120570
	public void AddItem(ServerItemState item)
	{
		this.m_itemList.Add(item);
	}

	// Token: 0x060036D4 RID: 14036 RVA: 0x00122380 File Offset: 0x00120580
	public void SetExpiredFlag(bool flag)
	{
		this.m_isExpired = flag;
	}

	// Token: 0x060036D5 RID: 14037 RVA: 0x0012238C File Offset: 0x0012058C
	public bool IsExpired()
	{
		return this.m_isExpired;
	}

	// Token: 0x060036D6 RID: 14038 RVA: 0x00122394 File Offset: 0x00120594
	public void ClearList()
	{
		this.m_itemList.Clear();
	}

	// Token: 0x060036D7 RID: 14039 RVA: 0x001223A4 File Offset: 0x001205A4
	public void CopyTo(ServerFreeItemState dest)
	{
		foreach (ServerItemState item in this.m_itemList)
		{
			dest.AddItem(item);
		}
		dest.m_isExpired = this.m_isExpired;
	}

	// Token: 0x04002E16 RID: 11798
	private List<ServerItemState> m_itemList;

	// Token: 0x04002E17 RID: 11799
	private bool m_isExpired;
}
