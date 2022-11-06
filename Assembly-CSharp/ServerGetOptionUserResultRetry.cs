using System;
using UnityEngine;

// Token: 0x0200077C RID: 1916
public class ServerGetOptionUserResultRetry : ServerRetryProcess
{
	// Token: 0x0600330B RID: 13067 RVA: 0x0011ADA0 File Offset: 0x00118FA0
	public ServerGetOptionUserResultRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600330C RID: 13068 RVA: 0x0011ADAC File Offset: 0x00118FAC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerOptionUserResult(this.m_callbackObject);
		}
	}
}
