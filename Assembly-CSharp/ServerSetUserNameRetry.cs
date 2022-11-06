using System;
using UnityEngine;

// Token: 0x02000788 RID: 1928
public class ServerSetUserNameRetry : ServerRetryProcess
{
	// Token: 0x0600333A RID: 13114 RVA: 0x0011B140 File Offset: 0x00119340
	public ServerSetUserNameRetry(string userName, GameObject callbackObject) : base(callbackObject)
	{
		this.m_userName = userName;
	}

	// Token: 0x0600333B RID: 13115 RVA: 0x0011B150 File Offset: 0x00119350
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSetUserName(this.m_userName, this.m_callbackObject);
		}
	}

	// Token: 0x04002BAE RID: 11182
	public string m_userName;
}
