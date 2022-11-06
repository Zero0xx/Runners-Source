using System;
using UnityEngine;

// Token: 0x02000730 RID: 1840
public class ServerGetDailyMissionDataRetry : ServerRetryProcess
{
	// Token: 0x06003123 RID: 12579 RVA: 0x0011686C File Offset: 0x00114A6C
	public ServerGetDailyMissionDataRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003124 RID: 12580 RVA: 0x00116878 File Offset: 0x00114A78
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetDailyMissionData(this.m_callbackObject);
		}
	}
}
