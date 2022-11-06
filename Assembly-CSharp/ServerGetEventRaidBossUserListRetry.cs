using System;
using UnityEngine;

// Token: 0x020006F5 RID: 1781
public class ServerGetEventRaidBossUserListRetry : ServerRetryProcess
{
	// Token: 0x06002FB4 RID: 12212 RVA: 0x00113A20 File Offset: 0x00111C20
	public ServerGetEventRaidBossUserListRetry(int eventId, long raidBossId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_raidBossId = raidBossId;
	}

	// Token: 0x06002FB5 RID: 12213 RVA: 0x00113A38 File Offset: 0x00111C38
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventRaidBossUserList(this.m_eventId, this.m_raidBossId, this.m_callbackObject);
		}
	}

	// Token: 0x04002AA1 RID: 10913
	private int m_eventId;

	// Token: 0x04002AA2 RID: 10914
	private long m_raidBossId;
}
