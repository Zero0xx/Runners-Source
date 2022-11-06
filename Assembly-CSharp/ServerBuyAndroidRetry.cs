using System;
using UnityEngine;

// Token: 0x020007B2 RID: 1970
public class ServerBuyAndroidRetry : ServerRetryProcess
{
	// Token: 0x0600342F RID: 13359 RVA: 0x0011CB74 File Offset: 0x0011AD74
	public ServerBuyAndroidRetry(string receiptData, string signature, GameObject callbackObject) : base(callbackObject)
	{
		this.m_receiptData = receiptData;
		this.m_signature = signature;
	}

	// Token: 0x06003430 RID: 13360 RVA: 0x0011CB8C File Offset: 0x0011AD8C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerBuyAndroid(this.m_receiptData, this.m_signature, this.m_callbackObject);
		}
	}

	// Token: 0x04002C0A RID: 11274
	public string m_receiptData;

	// Token: 0x04002C0B RID: 11275
	public string m_signature;
}
