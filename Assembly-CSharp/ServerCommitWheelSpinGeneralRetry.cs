using System;
using UnityEngine;

// Token: 0x0200079D RID: 1949
public class ServerCommitWheelSpinGeneralRetry : ServerRetryProcess
{
	// Token: 0x060033B9 RID: 13241 RVA: 0x0011C0E4 File Offset: 0x0011A2E4
	public ServerCommitWheelSpinGeneralRetry(int eventId, int spinId, int spinCostItemId, int spinNum, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_spinId = spinId;
		this.m_spinCostItemId = spinCostItemId;
		this.m_spinNum = spinNum;
	}

	// Token: 0x060033BA RID: 13242 RVA: 0x0011C10C File Offset: 0x0011A30C
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerCommitWheelSpinGeneral(this.m_eventId, this.m_spinId, this.m_spinCostItemId, this.m_spinNum, this.m_callbackObject);
		}
	}

	// Token: 0x04002BE5 RID: 11237
	private int m_eventId;

	// Token: 0x04002BE6 RID: 11238
	private int m_spinId;

	// Token: 0x04002BE7 RID: 11239
	private int m_spinCostItemId;

	// Token: 0x04002BE8 RID: 11240
	private int m_spinNum;
}
