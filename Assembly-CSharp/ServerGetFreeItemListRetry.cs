using System;
using UnityEngine;

// Token: 0x02000732 RID: 1842
public class ServerGetFreeItemListRetry : ServerRetryProcess
{
	// Token: 0x06003127 RID: 12583 RVA: 0x001168D0 File Offset: 0x00114AD0
	public ServerGetFreeItemListRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003128 RID: 12584 RVA: 0x001168DC File Offset: 0x00114ADC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetFreeItemList(this.m_callbackObject);
		}
	}
}
