using System;
using UnityEngine;

// Token: 0x02000782 RID: 1922
public class ServerGetChaoStateRetry : ServerRetryProcess
{
	// Token: 0x0600332E RID: 13102 RVA: 0x0011B014 File Offset: 0x00119214
	public ServerGetChaoStateRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600332F RID: 13103 RVA: 0x0011B020 File Offset: 0x00119220
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetChaoState(this.m_callbackObject);
		}
	}
}
