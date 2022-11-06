using System;
using UnityEngine;

// Token: 0x020006A3 RID: 1699
public class ServerGetChaoRentalStatesRetry : ServerRetryProcess
{
	// Token: 0x06002DC1 RID: 11713 RVA: 0x001104E4 File Offset: 0x0010E6E4
	public ServerGetChaoRentalStatesRetry(string[] friendId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_friendId = friendId;
	}

	// Token: 0x06002DC2 RID: 11714 RVA: 0x001104F4 File Offset: 0x0010E6F4
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetChaoRentalStates(this.m_friendId, this.m_callbackObject);
		}
	}

	// Token: 0x040029E7 RID: 10727
	public string[] m_friendId;
}
