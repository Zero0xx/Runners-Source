using System;
using UnityEngine;

// Token: 0x0200073A RID: 1850
public class ServerGetRingItemListRetry : ServerRetryProcess
{
	// Token: 0x06003144 RID: 12612 RVA: 0x00116DD4 File Offset: 0x00114FD4
	public ServerGetRingItemListRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003145 RID: 12613 RVA: 0x00116DE0 File Offset: 0x00114FE0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetRingItemList(this.m_callbackObject);
		}
	}
}
