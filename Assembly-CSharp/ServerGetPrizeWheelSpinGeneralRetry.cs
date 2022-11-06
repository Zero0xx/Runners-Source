using System;
using UnityEngine;

// Token: 0x020007A1 RID: 1953
public class ServerGetPrizeWheelSpinGeneralRetry : ServerRetryProcess
{
	// Token: 0x060033C1 RID: 13249 RVA: 0x0011C24C File Offset: 0x0011A44C
	public ServerGetPrizeWheelSpinGeneralRetry(int eventId, int spinType, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_spinType = spinType;
	}

	// Token: 0x060033C2 RID: 13250 RVA: 0x0011C264 File Offset: 0x0011A464
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetPrizeWheelSpinGeneral(this.m_eventId, this.m_spinType, this.m_callbackObject);
		}
	}

	// Token: 0x04002BED RID: 11245
	private int m_spinType;

	// Token: 0x04002BEE RID: 11246
	private int m_eventId;
}
