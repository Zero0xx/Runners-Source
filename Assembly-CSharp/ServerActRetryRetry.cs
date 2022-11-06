using System;
using UnityEngine;

// Token: 0x02000726 RID: 1830
public class ServerActRetryRetry : ServerRetryProcess
{
	// Token: 0x0600310F RID: 12559 RVA: 0x00116644 File Offset: 0x00114844
	public ServerActRetryRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003110 RID: 12560 RVA: 0x00116650 File Offset: 0x00114850
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerActRetry(this.m_callbackObject);
		}
	}
}
