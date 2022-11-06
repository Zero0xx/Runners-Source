using System;
using UnityEngine;

// Token: 0x0200074D RID: 1869
public class ServerGetWeeklyLeaderboardOptionsRetry : ServerRetryProcess
{
	// Token: 0x060031C0 RID: 12736 RVA: 0x00117CA0 File Offset: 0x00115EA0
	public ServerGetWeeklyLeaderboardOptionsRetry(int mode, GameObject callbackObject) : base(callbackObject)
	{
		this.m_mode = mode;
	}

	// Token: 0x060031C1 RID: 12737 RVA: 0x00117CB0 File Offset: 0x00115EB0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetWeeklyLeaderboardOptions(this.m_mode, this.m_callbackObject);
		}
	}

	// Token: 0x04002B46 RID: 11078
	private int m_mode;
}
