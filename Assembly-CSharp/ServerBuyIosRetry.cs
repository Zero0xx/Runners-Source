using System;
using UnityEngine;

// Token: 0x020007B4 RID: 1972
public class ServerBuyIosRetry : ServerRetryProcess
{
	// Token: 0x06003433 RID: 13363 RVA: 0x0011CC0C File Offset: 0x0011AE0C
	public ServerBuyIosRetry(string receiptData, GameObject callbackObject) : base(callbackObject)
	{
		this.m_receiptData = receiptData;
	}

	// Token: 0x06003434 RID: 13364 RVA: 0x0011CC1C File Offset: 0x0011AE1C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerBuyIos(this.m_receiptData, this.m_callbackObject);
		}
	}

	// Token: 0x04002C0C RID: 11276
	public string m_receiptData;
}
