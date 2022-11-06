using System;
using UnityEngine;

// Token: 0x020006F9 RID: 1785
public class ServerGetEventStateRetry : ServerRetryProcess
{
	// Token: 0x06002FBC RID: 12220 RVA: 0x00113B34 File Offset: 0x00111D34
	public ServerGetEventStateRetry(int eventId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
	}

	// Token: 0x06002FBD RID: 12221 RVA: 0x00113B44 File Offset: 0x00111D44
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventState(this.m_eventId, this.m_callbackObject);
		}
	}

	// Token: 0x04002AA6 RID: 10918
	private int m_eventId;
}
