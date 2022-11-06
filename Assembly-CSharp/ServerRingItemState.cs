using System;

// Token: 0x02000820 RID: 2080
public class ServerRingItemState
{
	// Token: 0x060037D0 RID: 14288 RVA: 0x00126760 File Offset: 0x00124960
	public ServerRingItemState()
	{
		this.m_itemId = 0;
		this.m_cost = 0;
	}

	// Token: 0x060037D1 RID: 14289 RVA: 0x00126778 File Offset: 0x00124978
	public void Dump()
	{
		Debug.Log(string.Format("itemId={0}, cost={1}", this.m_itemId, this.m_cost));
	}

	// Token: 0x04002F2E RID: 12078
	public int m_itemId;

	// Token: 0x04002F2F RID: 12079
	public int m_cost;
}
