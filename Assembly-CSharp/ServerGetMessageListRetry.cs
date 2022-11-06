using System;
using UnityEngine;

// Token: 0x02000775 RID: 1909
public class ServerGetMessageListRetry : ServerRetryProcess
{
	// Token: 0x060032F5 RID: 13045 RVA: 0x0011A6AC File Offset: 0x001188AC
	public ServerGetMessageListRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x060032F6 RID: 13046 RVA: 0x0011A6B8 File Offset: 0x001188B8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetMessageList(this.m_callbackObject);
		}
	}
}
