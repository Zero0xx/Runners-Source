using System;
using UnityEngine;

// Token: 0x0200070A RID: 1802
public class ServerRequestEnergyRetry : ServerRetryProcess
{
	// Token: 0x06003013 RID: 12307 RVA: 0x00114360 File Offset: 0x00112560
	public ServerRequestEnergyRetry(string friendId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_friendId = friendId;
	}

	// Token: 0x06003014 RID: 12308 RVA: 0x00114370 File Offset: 0x00112570
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerRequestEnergy(this.m_friendId, this.m_callbackObject);
		}
	}

	// Token: 0x04002AC4 RID: 10948
	public string m_friendId;
}
