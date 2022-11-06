using System;
using UnityEngine;

// Token: 0x02000760 RID: 1888
public class ServerGetTickerRetry : ServerRetryProcess
{
	// Token: 0x0600328A RID: 12938 RVA: 0x001197C4 File Offset: 0x001179C4
	public ServerGetTickerRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600328B RID: 12939 RVA: 0x001197D0 File Offset: 0x001179D0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetTicker(this.m_callbackObject);
		}
	}
}
