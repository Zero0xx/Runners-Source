using System;
using UnityEngine;

// Token: 0x0200075A RID: 1882
public class ServerGetCountryRetry : ServerRetryProcess
{
	// Token: 0x0600327D RID: 12925 RVA: 0x00119610 File Offset: 0x00117810
	public ServerGetCountryRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x0600327E RID: 12926 RVA: 0x0011961C File Offset: 0x0011781C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetCountry(this.m_callbackObject);
		}
	}
}
