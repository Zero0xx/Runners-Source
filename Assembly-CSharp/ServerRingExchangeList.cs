using System;

// Token: 0x0200081F RID: 2079
public class ServerRingExchangeList
{
	// Token: 0x060037CD RID: 14285 RVA: 0x001266C0 File Offset: 0x001248C0
	public ServerRingExchangeList()
	{
		this.m_itemId = 0;
		this.m_itemNum = 0;
		this.m_price = 0;
	}

	// Token: 0x060037CE RID: 14286 RVA: 0x001266E0 File Offset: 0x001248E0
	public void Dump()
	{
		Debug.Log(string.Format("itemId={0}, itemNum={1}, price={2}", this.m_itemId, this.m_itemNum, this.m_price));
	}

	// Token: 0x060037CF RID: 14287 RVA: 0x00126720 File Offset: 0x00124920
	public void CopyTo(ServerRingExchangeList dest)
	{
		dest.m_ringItemId = this.m_ringItemId;
		dest.m_itemId = this.m_itemId;
		dest.m_itemNum = this.m_itemNum;
		dest.m_price = this.m_price;
	}

	// Token: 0x04002F2A RID: 12074
	public int m_ringItemId;

	// Token: 0x04002F2B RID: 12075
	public int m_itemId;

	// Token: 0x04002F2C RID: 12076
	public int m_itemNum;

	// Token: 0x04002F2D RID: 12077
	public int m_price;
}
