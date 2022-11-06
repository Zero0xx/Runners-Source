using System;
using UnityEngine;

// Token: 0x020006A7 RID: 1703
public class ServerGetFirstLaunchChaoRetry : ServerRetryProcess
{
	// Token: 0x06002DC9 RID: 11721 RVA: 0x001105C4 File Offset: 0x0010E7C4
	public ServerGetFirstLaunchChaoRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06002DCA RID: 11722 RVA: 0x001105D0 File Offset: 0x0010E7D0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetFirstLaunchChao(this.m_callbackObject);
		}
	}
}
