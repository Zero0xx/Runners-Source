using System;

// Token: 0x02000819 RID: 2073
public class ServerPresentState
{
	// Token: 0x060037AC RID: 14252 RVA: 0x00125BDC File Offset: 0x00123DDC
	public ServerPresentState()
	{
		this.m_itemId = 0;
		this.m_numItem = 0;
		this.m_additionalInfo1 = 0;
		this.m_additionalInfo2 = 0;
	}

	// Token: 0x060037AD RID: 14253 RVA: 0x00125C0C File Offset: 0x00123E0C
	public void CopyTo(ServerPresentState to)
	{
		to.m_itemId = this.m_itemId;
		to.m_numItem = this.m_numItem;
		to.m_additionalInfo1 = this.m_additionalInfo1;
		to.m_additionalInfo2 = this.m_additionalInfo2;
	}

	// Token: 0x04002F0C RID: 12044
	public int m_itemId;

	// Token: 0x04002F0D RID: 12045
	public int m_numItem;

	// Token: 0x04002F0E RID: 12046
	public int m_additionalInfo1;

	// Token: 0x04002F0F RID: 12047
	public int m_additionalInfo2;
}
