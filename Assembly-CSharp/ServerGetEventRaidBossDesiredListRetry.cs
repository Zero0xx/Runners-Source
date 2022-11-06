using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006F3 RID: 1779
public class ServerGetEventRaidBossDesiredListRetry : ServerRetryProcess
{
	// Token: 0x06002FB0 RID: 12208 RVA: 0x0011396C File Offset: 0x00111B6C
	public ServerGetEventRaidBossDesiredListRetry(int eventId, long raidBossId, List<string> friendIdList, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_raidBossId = raidBossId;
		this.m_friendIdList = friendIdList;
	}

	// Token: 0x06002FB1 RID: 12209 RVA: 0x0011398C File Offset: 0x00111B8C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventRaidBossDesiredList(this.m_eventId, this.m_raidBossId, this.m_friendIdList, this.m_callbackObject);
		}
	}

	// Token: 0x04002A9E RID: 10910
	public int m_eventId;

	// Token: 0x04002A9F RID: 10911
	public long m_raidBossId;

	// Token: 0x04002AA0 RID: 10912
	public List<string> m_friendIdList;
}
