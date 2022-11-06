using System;
using UnityEngine;

// Token: 0x020007B6 RID: 1974
public class ServerGetRedStarExchangeListRetry : ServerRetryProcess
{
	// Token: 0x06003437 RID: 13367 RVA: 0x0011CC88 File Offset: 0x0011AE88
	public ServerGetRedStarExchangeListRetry(int itemType, GameObject callbackObject) : base(callbackObject)
	{
		this.m_itemType = itemType;
	}

	// Token: 0x06003438 RID: 13368 RVA: 0x0011CC98 File Offset: 0x0011AE98
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetRedStarExchangeList(this.m_itemType, this.m_callbackObject);
		}
	}

	// Token: 0x04002C0D RID: 11277
	public int m_itemType;
}
