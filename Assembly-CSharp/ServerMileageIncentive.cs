using System;

// Token: 0x020007E8 RID: 2024
public class ServerMileageIncentive
{
	// Token: 0x06003626 RID: 13862 RVA: 0x00120988 File Offset: 0x0011EB88
	public ServerMileageIncentive()
	{
		this.m_type = ServerMileageIncentive.Type.NONE;
		this.m_itemId = 0;
		this.m_num = 0;
		this.m_pointId = 0;
		this.m_friendId = null;
	}

	// Token: 0x06003627 RID: 13863 RVA: 0x001209B4 File Offset: 0x0011EBB4
	public void CopyTo(ServerMileageIncentive to)
	{
		if (to != null)
		{
			to.m_type = this.m_type;
			to.m_itemId = this.m_itemId;
			to.m_num = this.m_num;
			to.m_pointId = this.m_pointId;
			to.m_friendId = this.m_friendId;
		}
	}

	// Token: 0x04002D99 RID: 11673
	public ServerMileageIncentive.Type m_type;

	// Token: 0x04002D9A RID: 11674
	public int m_itemId;

	// Token: 0x04002D9B RID: 11675
	public int m_num;

	// Token: 0x04002D9C RID: 11676
	public int m_pointId;

	// Token: 0x04002D9D RID: 11677
	public string m_friendId;

	// Token: 0x020007E9 RID: 2025
	public enum Type
	{
		// Token: 0x04002D9F RID: 11679
		NONE,
		// Token: 0x04002DA0 RID: 11680
		POINT,
		// Token: 0x04002DA1 RID: 11681
		CHAPTER,
		// Token: 0x04002DA2 RID: 11682
		EPISODE,
		// Token: 0x04002DA3 RID: 11683
		FRIEND
	}
}
