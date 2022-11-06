using System;
using UnityEngine;

// Token: 0x020007A5 RID: 1957
public class ServerGetWheelOptionsGeneralRetry : ServerRetryProcess
{
	// Token: 0x060033C9 RID: 13257 RVA: 0x0011C348 File Offset: 0x0011A548
	public ServerGetWheelOptionsGeneralRetry(int eventId, int spinId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_spinId = spinId;
	}

	// Token: 0x060033CA RID: 13258 RVA: 0x0011C360 File Offset: 0x0011A560
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetWheelOptionsGeneral(this.m_eventId, this.m_spinId, this.m_callbackObject);
		}
	}

	// Token: 0x04002BF1 RID: 11249
	private int m_spinId;

	// Token: 0x04002BF2 RID: 11250
	private int m_eventId;
}
