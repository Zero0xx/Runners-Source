using System;
using UnityEngine;

// Token: 0x020007B8 RID: 1976
public class ServerGetRingExchangeListRetry : ServerRetryProcess
{
	// Token: 0x0600343B RID: 13371 RVA: 0x0011CD04 File Offset: 0x0011AF04
	public ServerGetRingExchangeListRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600343C RID: 13372 RVA: 0x0011CD10 File Offset: 0x0011AF10
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetRingExchangeList(this.m_callbackObject);
		}
	}
}
