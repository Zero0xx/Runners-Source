using System;
using UnityEngine;

// Token: 0x0200069F RID: 1695
public class ServerCommitChaoWheelSpinRetry : ServerRetryProcess
{
	// Token: 0x06002DB9 RID: 11705 RVA: 0x001103C8 File Offset: 0x0010E5C8
	public ServerCommitChaoWheelSpinRetry(int count, GameObject callbackObject) : base(callbackObject)
	{
		this.m_count = count;
	}

	// Token: 0x06002DBA RID: 11706 RVA: 0x001103E0 File Offset: 0x0010E5E0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerCommitChaoWheelSpin(this.m_count, this.m_callbackObject);
		}
	}

	// Token: 0x040029E4 RID: 10724
	private int m_count = 1;
}
