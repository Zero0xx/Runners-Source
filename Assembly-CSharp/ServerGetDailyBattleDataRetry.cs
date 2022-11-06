using System;
using UnityEngine;

// Token: 0x020006C2 RID: 1730
public class ServerGetDailyBattleDataRetry : ServerRetryProcess
{
	// Token: 0x06002E7C RID: 11900 RVA: 0x00111AF0 File Offset: 0x0010FCF0
	public ServerGetDailyBattleDataRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06002E7D RID: 11901 RVA: 0x00111AFC File Offset: 0x0010FCFC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetDailyBattleData(this.m_callbackObject);
		}
	}
}
