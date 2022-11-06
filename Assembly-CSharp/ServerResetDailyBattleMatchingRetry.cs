using System;
using UnityEngine;

// Token: 0x020006CC RID: 1740
public class ServerResetDailyBattleMatchingRetry : ServerRetryProcess
{
	// Token: 0x06002E90 RID: 11920 RVA: 0x00111CFC File Offset: 0x0010FEFC
	public ServerResetDailyBattleMatchingRetry(int type, GameObject callbackObject) : base(callbackObject)
	{
		this.m_type = type;
	}

	// Token: 0x06002E91 RID: 11921 RVA: 0x00111D0C File Offset: 0x0010FF0C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerResetDailyBattleMatching(this.m_type, this.m_callbackObject);
		}
	}

	// Token: 0x04002A3B RID: 10811
	private int m_type;
}
