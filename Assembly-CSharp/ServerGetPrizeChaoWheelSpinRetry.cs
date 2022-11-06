using System;
using UnityEngine;

// Token: 0x020006A9 RID: 1705
public class ServerGetPrizeChaoWheelSpinRetry : ServerRetryProcess
{
	// Token: 0x06002DCD RID: 11725 RVA: 0x00110628 File Offset: 0x0010E828
	public ServerGetPrizeChaoWheelSpinRetry(int chaoWheelSpinType, GameObject callbackObject) : base(callbackObject)
	{
		this.m_chaoWheelSpinType = chaoWheelSpinType;
	}

	// Token: 0x06002DCE RID: 11726 RVA: 0x00110638 File Offset: 0x0010E838
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetPrizeChaoWheelSpin(this.m_chaoWheelSpinType, this.m_callbackObject);
		}
	}

	// Token: 0x040029EA RID: 10730
	private int m_chaoWheelSpinType;
}
