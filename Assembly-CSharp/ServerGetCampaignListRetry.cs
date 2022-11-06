using System;
using UnityEngine;

// Token: 0x0200072C RID: 1836
public class ServerGetCampaignListRetry : ServerRetryProcess
{
	// Token: 0x0600311B RID: 12571 RVA: 0x001167A4 File Offset: 0x001149A4
	public ServerGetCampaignListRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600311C RID: 12572 RVA: 0x001167B0 File Offset: 0x001149B0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetCampaignList(this.m_callbackObject);
		}
	}
}
