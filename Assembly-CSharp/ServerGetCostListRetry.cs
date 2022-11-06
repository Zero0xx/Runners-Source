using System;
using UnityEngine;

// Token: 0x0200072E RID: 1838
public class ServerGetCostListRetry : ServerRetryProcess
{
	// Token: 0x0600311F RID: 12575 RVA: 0x00116808 File Offset: 0x00114A08
	public ServerGetCostListRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003120 RID: 12576 RVA: 0x00116814 File Offset: 0x00114A14
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetCostList(this.m_callbackObject);
		}
	}
}
