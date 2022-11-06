using System;
using UnityEngine;

// Token: 0x02000710 RID: 1808
public class ServerSetInviteHistoryRetry : ServerRetryProcess
{
	// Token: 0x0600301F RID: 12319 RVA: 0x001144D4 File Offset: 0x001126D4
	public ServerSetInviteHistoryRetry(string facebookIdHash, GameObject callbackObject) : base(callbackObject)
	{
		this.m_facebookIdHash = facebookIdHash;
	}

	// Token: 0x06003020 RID: 12320 RVA: 0x001144E4 File Offset: 0x001126E4
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSetInviteHistory(this.m_facebookIdHash, this.m_callbackObject);
		}
	}

	// Token: 0x04002AC7 RID: 10951
	private string m_facebookIdHash;
}
