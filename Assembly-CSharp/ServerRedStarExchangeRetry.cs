using System;
using UnityEngine;

// Token: 0x020007BC RID: 1980
public class ServerRedStarExchangeRetry : ServerRetryProcess
{
	// Token: 0x06003443 RID: 13379 RVA: 0x0011CDD8 File Offset: 0x0011AFD8
	public ServerRedStarExchangeRetry(int storeItemId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_storeItemId = storeItemId;
	}

	// Token: 0x06003444 RID: 13380 RVA: 0x0011CDE8 File Offset: 0x0011AFE8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerRedStarExchange(this.m_storeItemId, this.m_callbackObject);
		}
	}

	// Token: 0x04002C0E RID: 11278
	public int m_storeItemId;
}
