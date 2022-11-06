using System;
using UnityEngine;

// Token: 0x0200072A RID: 1834
public class ServerDrawRaidBossRetry : ServerRetryProcess
{
	// Token: 0x06003117 RID: 12567 RVA: 0x0011670C File Offset: 0x0011490C
	public ServerDrawRaidBossRetry(int eventId, long score, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_score = score;
	}

	// Token: 0x06003118 RID: 12568 RVA: 0x00116724 File Offset: 0x00114924
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerDrawRaidBoss(this.m_eventId, this.m_score, this.m_callbackObject);
		}
	}

	// Token: 0x04002B1A RID: 11034
	private int m_eventId;

	// Token: 0x04002B1B RID: 11035
	private long m_score;
}
