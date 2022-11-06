using System;
using UnityEngine;

// Token: 0x02000768 RID: 1896
public class ServerLoginBonusRetry : ServerRetryProcess
{
	// Token: 0x0600329B RID: 12955 RVA: 0x001199AC File Offset: 0x00117BAC
	public ServerLoginBonusRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600329C RID: 12956 RVA: 0x001199B8 File Offset: 0x00117BB8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerLoginBonus(this.m_callbackObject);
		}
	}
}
