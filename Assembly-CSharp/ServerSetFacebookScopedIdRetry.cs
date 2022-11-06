using System;
using UnityEngine;

// Token: 0x0200070C RID: 1804
public class ServerSetFacebookScopedIdRetry : ServerRetryProcess
{
	// Token: 0x06003017 RID: 12311 RVA: 0x001143DC File Offset: 0x001125DC
	public ServerSetFacebookScopedIdRetry(string userId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_userId = userId;
	}

	// Token: 0x06003018 RID: 12312 RVA: 0x001143EC File Offset: 0x001125EC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSetFacebookScopedId(this.m_userId, this.m_callbackObject);
		}
	}

	// Token: 0x04002AC5 RID: 10949
	public string m_userId;
}
