using System;
using System.Collections.Generic;

// Token: 0x02000808 RID: 2056
public class ServerLoginBonusReward
{
	// Token: 0x0600371E RID: 14110 RVA: 0x001233FC File Offset: 0x001215FC
	public ServerLoginBonusReward()
	{
		this.m_itemList = new List<ServerItemState>();
	}

	// Token: 0x0600371F RID: 14111 RVA: 0x00123410 File Offset: 0x00121610
	public void CopyTo(ServerLoginBonusReward dest)
	{
		foreach (ServerItemState item in this.m_itemList)
		{
			dest.m_itemList.Add(item);
		}
	}

	// Token: 0x04002E69 RID: 11881
	public List<ServerItemState> m_itemList;
}
