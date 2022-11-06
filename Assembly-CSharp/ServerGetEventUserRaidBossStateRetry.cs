using System;
using UnityEngine;

// Token: 0x020006FD RID: 1789
public class ServerGetEventUserRaidBossStateRetry : ServerRetryProcess
{
	// Token: 0x06002FC4 RID: 12228 RVA: 0x00113C2C File Offset: 0x00111E2C
	public ServerGetEventUserRaidBossStateRetry(int eventId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
	}

	// Token: 0x06002FC5 RID: 12229 RVA: 0x00113C3C File Offset: 0x00111E3C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventUserRaidBossState(this.m_eventId, this.m_callbackObject);
		}
	}

	// Token: 0x04002AAA RID: 10922
	private int m_eventId;
}
