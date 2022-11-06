using System;
using UnityEngine;

// Token: 0x0200069D RID: 1693
public class ServerAddSpecialEggRetry : ServerRetryProcess
{
	// Token: 0x06002DB5 RID: 11701 RVA: 0x0011034C File Offset: 0x0010E54C
	public ServerAddSpecialEggRetry(int numSpecialEgg, GameObject callbackObject) : base(callbackObject)
	{
		this.m_numSpecialEgg = numSpecialEgg;
	}

	// Token: 0x06002DB6 RID: 11702 RVA: 0x0011035C File Offset: 0x0010E55C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerAddSpecialEgg(this.m_numSpecialEgg, this.m_callbackObject);
		}
	}

	// Token: 0x040029E3 RID: 10723
	public int m_numSpecialEgg;
}
