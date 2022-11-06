using System;
using System.Collections.Generic;

// Token: 0x020007FB RID: 2043
public class ServerDailyChallengeState
{
	// Token: 0x060036CD RID: 14029 RVA: 0x001222DC File Offset: 0x001204DC
	public ServerDailyChallengeState()
	{
		this.m_incentiveList = new List<ServerDailyChallengeIncentive>();
		this.m_numIncentiveCont = 0;
		this.m_numDailyChalDay = 1;
		this.m_maxDailyChalDay = 1;
		this.m_maxIncentive = 7;
	}

	// Token: 0x04002E0D RID: 11789
	public List<ServerDailyChallengeIncentive> m_incentiveList;

	// Token: 0x04002E0E RID: 11790
	public int m_numIncentiveCont;

	// Token: 0x04002E0F RID: 11791
	public int m_numDailyChalDay;

	// Token: 0x04002E10 RID: 11792
	public int m_maxDailyChalDay;

	// Token: 0x04002E11 RID: 11793
	public int m_maxIncentive;

	// Token: 0x04002E12 RID: 11794
	public DateTime m_chalEndTime;
}
