using System;
using UnityEngine;

// Token: 0x020006A5 RID: 1701
public class ServerGetChaoWheelOptionsRetry : ServerRetryProcess
{
	// Token: 0x06002DC5 RID: 11717 RVA: 0x00110560 File Offset: 0x0010E760
	public ServerGetChaoWheelOptionsRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06002DC6 RID: 11718 RVA: 0x0011056C File Offset: 0x0010E76C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetChaoWheelOptions(this.m_callbackObject);
		}
	}
}
