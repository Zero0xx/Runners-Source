using System;
using UnityEngine;

// Token: 0x020007A7 RID: 1959
public class ServerGetWheelSpinInfoRetry : ServerRetryProcess
{
	// Token: 0x060033CD RID: 13261 RVA: 0x0011C3E0 File Offset: 0x0011A5E0
	public ServerGetWheelSpinInfoRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x060033CE RID: 13262 RVA: 0x0011C3EC File Offset: 0x0011A5EC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetWheelSpinInfo(this.m_callbackObject);
		}
	}
}
