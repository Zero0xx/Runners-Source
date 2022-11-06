using System;
using UnityEngine;

// Token: 0x02000728 RID: 1832
public class ServerActRetryFreeRetry : ServerRetryProcess
{
	// Token: 0x06003113 RID: 12563 RVA: 0x001166A8 File Offset: 0x001148A8
	public ServerActRetryFreeRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003114 RID: 12564 RVA: 0x001166B4 File Offset: 0x001148B4
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerActRetryFree(this.m_callbackObject);
		}
	}
}
