using System;
using UnityEngine;

// Token: 0x0200076E RID: 1902
public class ServerReLoginRetry : ServerRetryProcess
{
	// Token: 0x060032A7 RID: 12967 RVA: 0x00119B9C File Offset: 0x00117D9C
	public ServerReLoginRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x060032A8 RID: 12968 RVA: 0x00119BA8 File Offset: 0x00117DA8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerReLogin(this.m_callbackObject);
		}
	}
}
