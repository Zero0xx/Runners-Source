using System;
using UnityEngine;

// Token: 0x020006C8 RID: 1736
public class ServerGetPrizeDailyBattleRetry : ServerRetryProcess
{
	// Token: 0x06002E88 RID: 11912 RVA: 0x00111C34 File Offset: 0x0010FE34
	public ServerGetPrizeDailyBattleRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06002E89 RID: 11913 RVA: 0x00111C40 File Offset: 0x0010FE40
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetPrizeDailyBattle(this.m_callbackObject);
		}
	}
}
