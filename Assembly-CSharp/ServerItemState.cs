using System;

// Token: 0x02000802 RID: 2050
public class ServerItemState
{
	// Token: 0x060036DE RID: 14046 RVA: 0x001227A0 File Offset: 0x001209A0
	public ServerItemState()
	{
		this.m_itemId = 0;
		this.m_num = 0;
	}

	// Token: 0x060036DF RID: 14047 RVA: 0x001227B8 File Offset: 0x001209B8
	public void CopyTo(ServerItemState to)
	{
		to.m_itemId = this.m_itemId;
		to.m_num = this.m_num;
	}

	// Token: 0x060036E0 RID: 14048 RVA: 0x001227D4 File Offset: 0x001209D4
	public ServerItem GetItem()
	{
		if (this.m_itemId >= 0)
		{
			return new ServerItem((ServerItem.Id)this.m_itemId);
		}
		return default(ServerItem);
	}

	// Token: 0x04002E33 RID: 11827
	public int m_itemId;

	// Token: 0x04002E34 RID: 11828
	public int m_num;
}
