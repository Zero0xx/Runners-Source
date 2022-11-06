using System;
using UnityEngine;

// Token: 0x0200073C RID: 1852
public class ServerPostGameResultsRetry : ServerRetryProcess
{
	// Token: 0x06003148 RID: 12616 RVA: 0x00116E38 File Offset: 0x00115038
	public ServerPostGameResultsRetry(ServerGameResults results, GameObject callbackObject) : base(callbackObject)
	{
		this.m_results = results;
	}

	// Token: 0x06003149 RID: 12617 RVA: 0x00116E48 File Offset: 0x00115048
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerPostGameResults(this.m_results, this.m_callbackObject);
		}
	}

	// Token: 0x04002B1F RID: 11039
	private ServerGameResults m_results;
}
