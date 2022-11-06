using System;
using UnityEngine;

// Token: 0x020007A3 RID: 1955
public class ServerGetWheelOptionsRetry : ServerRetryProcess
{
	// Token: 0x060033C5 RID: 13253 RVA: 0x0011C2E4 File Offset: 0x0011A4E4
	public ServerGetWheelOptionsRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x060033C6 RID: 13254 RVA: 0x0011C2F0 File Offset: 0x0011A4F0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetWheelOptions(this.m_callbackObject);
		}
	}
}
