using System;
using UnityEngine;

// Token: 0x02000734 RID: 1844
public class ServerGetMenuDataRetry : ServerRetryProcess
{
	// Token: 0x0600312B RID: 12587 RVA: 0x00116934 File Offset: 0x00114B34
	public ServerGetMenuDataRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600312C RID: 12588 RVA: 0x00116940 File Offset: 0x00114B40
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetMenuData(this.m_callbackObject);
		}
	}
}
