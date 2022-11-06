using System;
using UnityEngine;

// Token: 0x020006CA RID: 1738
public class ServerPostDailyBattleResultRetry : ServerRetryProcess
{
	// Token: 0x06002E8C RID: 11916 RVA: 0x00111C98 File Offset: 0x0010FE98
	public ServerPostDailyBattleResultRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06002E8D RID: 11917 RVA: 0x00111CA4 File Offset: 0x0010FEA4
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerPostDailyBattleResult(this.m_callbackObject);
		}
	}
}
