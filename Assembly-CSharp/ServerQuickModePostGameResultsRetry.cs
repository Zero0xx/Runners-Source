using System;
using UnityEngine;

// Token: 0x02000722 RID: 1826
public class ServerQuickModePostGameResultsRetry : ServerRetryProcess
{
	// Token: 0x06003107 RID: 12551 RVA: 0x00116514 File Offset: 0x00114714
	public ServerQuickModePostGameResultsRetry(ServerQuickModeGameResults results, GameObject callbackObject) : base(callbackObject)
	{
		this.m_results = results;
	}

	// Token: 0x06003108 RID: 12552 RVA: 0x00116524 File Offset: 0x00114724
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerQuickModePostGameResults(this.m_results, this.m_callbackObject);
		}
	}

	// Token: 0x04002B15 RID: 11029
	private ServerQuickModeGameResults m_results;
}
