using System;
using UnityEngine;

// Token: 0x020007BE RID: 1982
public class ServerRingExchangeRetry : ServerRetryProcess
{
	// Token: 0x06003447 RID: 13383 RVA: 0x0011CE54 File Offset: 0x0011B054
	public ServerRingExchangeRetry(int itemId, int itemNum, GameObject callbackObject) : base(callbackObject)
	{
		this.m_itemId = itemId;
		this.m_itemNum = itemNum;
	}

	// Token: 0x06003448 RID: 13384 RVA: 0x0011CE6C File Offset: 0x0011B06C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerRingExchange(this.m_itemId, this.m_itemNum, this.m_callbackObject);
		}
	}

	// Token: 0x04002C0F RID: 11279
	public int m_itemId;

	// Token: 0x04002C10 RID: 11280
	public int m_itemNum;
}
