using System;
using UnityEngine;

// Token: 0x02000786 RID: 1926
public class ServerRetrievePlayerStateRetry : ServerRetryProcess
{
	// Token: 0x06003336 RID: 13110 RVA: 0x0011B0DC File Offset: 0x001192DC
	public ServerRetrievePlayerStateRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003337 RID: 13111 RVA: 0x0011B0E8 File Offset: 0x001192E8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerRetrievePlayerState(this.m_callbackObject);
		}
	}
}
