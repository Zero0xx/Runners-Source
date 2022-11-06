using System;
using UnityEngine;

// Token: 0x02000762 RID: 1890
public class ServerGetVariousParameterRetry : ServerRetryProcess
{
	// Token: 0x0600328E RID: 12942 RVA: 0x00119828 File Offset: 0x00117A28
	public ServerGetVariousParameterRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600328F RID: 12943 RVA: 0x00119834 File Offset: 0x00117A34
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetVariousParameter(this.m_callbackObject);
		}
	}
}
