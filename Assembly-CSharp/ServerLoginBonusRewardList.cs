using System;
using System.Collections.Generic;

// Token: 0x02000809 RID: 2057
public class ServerLoginBonusRewardList
{
	// Token: 0x06003720 RID: 14112 RVA: 0x0012347C File Offset: 0x0012167C
	public ServerLoginBonusRewardList()
	{
		this.m_selectRewardList = new List<ServerLoginBonusReward>();
	}

	// Token: 0x06003721 RID: 14113 RVA: 0x00123490 File Offset: 0x00121690
	public void CopyTo(ServerLoginBonusRewardList dest)
	{
		foreach (ServerLoginBonusReward item in this.m_selectRewardList)
		{
			dest.m_selectRewardList.Add(item);
		}
	}

	// Token: 0x04002E6A RID: 11882
	public List<ServerLoginBonusReward> m_selectRewardList;
}
