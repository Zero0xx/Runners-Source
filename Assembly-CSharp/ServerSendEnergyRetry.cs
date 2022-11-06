using System;
using UnityEngine;

// Token: 0x02000777 RID: 1911
public class ServerSendEnergyRetry : ServerRetryProcess
{
	// Token: 0x060032F9 RID: 13049 RVA: 0x0011A710 File Offset: 0x00118910
	public ServerSendEnergyRetry(string friendId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_friendId = friendId;
	}

	// Token: 0x060032FA RID: 13050 RVA: 0x0011A720 File Offset: 0x00118920
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSendEnergy(this.m_friendId, this.m_callbackObject);
		}
	}

	// Token: 0x04002B9F RID: 11167
	public string m_friendId;
}
