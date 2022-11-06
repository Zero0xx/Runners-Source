using System;
using UnityEngine;

// Token: 0x02000784 RID: 1924
public class ServerGetCharacterStateRetry : ServerRetryProcess
{
	// Token: 0x06003332 RID: 13106 RVA: 0x0011B078 File Offset: 0x00119278
	public ServerGetCharacterStateRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003333 RID: 13107 RVA: 0x0011B084 File Offset: 0x00119284
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetCharacterState(this.m_callbackObject);
		}
	}
}
