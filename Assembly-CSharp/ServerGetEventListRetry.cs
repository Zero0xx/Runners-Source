using System;
using UnityEngine;

// Token: 0x020006F1 RID: 1777
public class ServerGetEventListRetry : ServerRetryProcess
{
	// Token: 0x06002FAC RID: 12204 RVA: 0x00113908 File Offset: 0x00111B08
	public ServerGetEventListRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06002FAD RID: 12205 RVA: 0x00113914 File Offset: 0x00111B14
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventList(this.m_callbackObject);
		}
	}
}
