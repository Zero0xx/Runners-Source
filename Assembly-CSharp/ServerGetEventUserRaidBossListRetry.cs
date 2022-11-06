using System;
using UnityEngine;

// Token: 0x020006FB RID: 1787
public class ServerGetEventUserRaidBossListRetry : ServerRetryProcess
{
	// Token: 0x06002FC0 RID: 12224 RVA: 0x00113BB0 File Offset: 0x00111DB0
	public ServerGetEventUserRaidBossListRetry(int eventId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
	}

	// Token: 0x06002FC1 RID: 12225 RVA: 0x00113BC0 File Offset: 0x00111DC0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventUserRaidBossList(this.m_eventId, this.m_callbackObject);
		}
	}

	// Token: 0x04002AA7 RID: 10919
	private int m_eventId;
}
