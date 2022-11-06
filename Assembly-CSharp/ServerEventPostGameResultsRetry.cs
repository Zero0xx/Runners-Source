using System;
using UnityEngine;

// Token: 0x020006EB RID: 1771
public class ServerEventPostGameResultsRetry : ServerRetryProcess
{
	// Token: 0x06002FA0 RID: 12192 RVA: 0x00113704 File Offset: 0x00111904
	public ServerEventPostGameResultsRetry(int eventId, int numRaidbossRings, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_numRaidbossRings = numRaidbossRings;
	}

	// Token: 0x06002FA1 RID: 12193 RVA: 0x0011371C File Offset: 0x0011191C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerEventPostGameResults(this.m_eventId, this.m_numRaidbossRings, this.m_callbackObject);
		}
	}

	// Token: 0x04002A96 RID: 10902
	public int m_eventId;

	// Token: 0x04002A97 RID: 10903
	public int m_numRaidbossRings;
}
