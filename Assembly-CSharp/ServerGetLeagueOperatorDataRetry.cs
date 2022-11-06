using System;
using UnityEngine;

// Token: 0x0200074B RID: 1867
public class ServerGetLeagueOperatorDataRetry : ServerRetryProcess
{
	// Token: 0x060031BC RID: 12732 RVA: 0x00117C24 File Offset: 0x00115E24
	public ServerGetLeagueOperatorDataRetry(int mode, GameObject callbackObject) : base(callbackObject)
	{
		this.m_mode = mode;
	}

	// Token: 0x060031BD RID: 12733 RVA: 0x00117C34 File Offset: 0x00115E34
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetLeagueOperatorData(this.m_mode, this.m_callbackObject);
		}
	}

	// Token: 0x04002B45 RID: 11077
	public int m_mode;
}
