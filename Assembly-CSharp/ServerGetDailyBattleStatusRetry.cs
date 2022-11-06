using System;
using UnityEngine;

// Token: 0x020006C6 RID: 1734
public class ServerGetDailyBattleStatusRetry : ServerRetryProcess
{
	// Token: 0x06002E84 RID: 11908 RVA: 0x00111BD0 File Offset: 0x0010FDD0
	public ServerGetDailyBattleStatusRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06002E85 RID: 11909 RVA: 0x00111BDC File Offset: 0x0010FDDC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetDailyBattleStatus(this.m_callbackObject);
		}
	}
}
