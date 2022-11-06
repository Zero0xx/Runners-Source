using System;
using UnityEngine;

// Token: 0x020006C4 RID: 1732
public class ServerGetDailyBattleDataHistoryRetry : ServerRetryProcess
{
	// Token: 0x06002E80 RID: 11904 RVA: 0x00111B54 File Offset: 0x0010FD54
	public ServerGetDailyBattleDataHistoryRetry(int count, GameObject callbackObject) : base(callbackObject)
	{
		this.m_count = count;
	}

	// Token: 0x06002E81 RID: 11905 RVA: 0x00111B64 File Offset: 0x0010FD64
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetDailyBattleDataHistory(this.m_count, this.m_callbackObject);
		}
	}

	// Token: 0x04002A32 RID: 10802
	private int m_count;
}
