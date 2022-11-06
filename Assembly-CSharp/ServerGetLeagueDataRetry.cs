using System;
using UnityEngine;

// Token: 0x02000749 RID: 1865
public class ServerGetLeagueDataRetry : ServerRetryProcess
{
	// Token: 0x060031B8 RID: 12728 RVA: 0x00117BA8 File Offset: 0x00115DA8
	public ServerGetLeagueDataRetry(int mode, GameObject callbackObject) : base(callbackObject)
	{
		this.m_mode = mode;
	}

	// Token: 0x060031B9 RID: 12729 RVA: 0x00117BB8 File Offset: 0x00115DB8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetLeagueData(this.m_mode, this.m_callbackObject);
		}
	}

	// Token: 0x04002B44 RID: 11076
	public int m_mode;
}
