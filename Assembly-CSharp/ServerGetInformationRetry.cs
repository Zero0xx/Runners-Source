using System;
using UnityEngine;

// Token: 0x0200075C RID: 1884
public class ServerGetInformationRetry : ServerRetryProcess
{
	// Token: 0x06003281 RID: 12929 RVA: 0x00119674 File Offset: 0x00117874
	public ServerGetInformationRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003282 RID: 12930 RVA: 0x00119680 File Offset: 0x00117880
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetInformation(this.m_callbackObject);
		}
	}
}
