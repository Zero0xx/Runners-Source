using System;
using UnityEngine;

// Token: 0x020006CE RID: 1742
public class ServerUpdateDailyBattleStatusRetry : ServerRetryProcess
{
	// Token: 0x06002E94 RID: 11924 RVA: 0x00111D78 File Offset: 0x0010FF78
	public ServerUpdateDailyBattleStatusRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06002E95 RID: 11925 RVA: 0x00111D84 File Offset: 0x0010FF84
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerUpdateDailyBattleStatus(this.m_callbackObject);
		}
	}
}
