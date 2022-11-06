using System;

// Token: 0x0200081D RID: 2077
public class ServerRedStarItemState
{
	// Token: 0x060037C0 RID: 14272 RVA: 0x0012645C File Offset: 0x0012465C
	public ServerRedStarItemState()
	{
		this.m_storeItemId = 0;
		this.m_itemId = 0;
		this.m_numItem = 0;
		this.m_price = 0;
		this.m_priceDisp = string.Empty;
		this.m_productId = string.Empty;
	}

	// Token: 0x060037C1 RID: 14273 RVA: 0x001264A4 File Offset: 0x001246A4
	public void CopyTo(ServerRedStarItemState dest)
	{
		dest.m_storeItemId = this.m_storeItemId;
		dest.m_itemId = this.m_itemId;
		dest.m_numItem = this.m_numItem;
		dest.m_price = this.m_price;
		dest.m_priceDisp = this.m_priceDisp;
		dest.m_productId = this.m_productId;
	}

	// Token: 0x060037C2 RID: 14274 RVA: 0x001264FC File Offset: 0x001246FC
	public void Dump()
	{
		Debug.Log(string.Format("storeItemId={0}, itemId={1}, numItem={2}, price={3}", new object[]
		{
			this.m_storeItemId,
			this.m_itemId,
			this.m_numItem,
			this.m_price
		}));
	}

	// Token: 0x04002F21 RID: 12065
	public int m_storeItemId;

	// Token: 0x04002F22 RID: 12066
	public int m_itemId;

	// Token: 0x04002F23 RID: 12067
	public int m_numItem;

	// Token: 0x04002F24 RID: 12068
	public int m_price;

	// Token: 0x04002F25 RID: 12069
	public string m_priceDisp;

	// Token: 0x04002F26 RID: 12070
	public string m_productId;
}
