using System;
using UnityEngine;

// Token: 0x020006EF RID: 1775
public class ServerEventUpdateGameResultsRetry : ServerRetryProcess
{
	// Token: 0x06002FA8 RID: 12200 RVA: 0x0011388C File Offset: 0x00111A8C
	public ServerEventUpdateGameResultsRetry(ServerEventGameResults results, GameObject callbackObject) : base(callbackObject)
	{
		this.m_results = results;
	}

	// Token: 0x06002FA9 RID: 12201 RVA: 0x0011389C File Offset: 0x00111A9C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerEventUpdateGameResults(this.m_results, this.m_callbackObject);
		}
	}

	// Token: 0x04002A9D RID: 10909
	private ServerEventGameResults m_results;
}
