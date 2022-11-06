using System;
using UnityEngine;

// Token: 0x0200079B RID: 1947
public class ServerCommitWheelSpinRetry : ServerRetryProcess
{
	// Token: 0x060033B5 RID: 13237 RVA: 0x0011C060 File Offset: 0x0011A260
	public ServerCommitWheelSpinRetry(int count, GameObject callbackObject) : base(callbackObject)
	{
		this.m_count = count;
	}

	// Token: 0x060033B6 RID: 13238 RVA: 0x0011C078 File Offset: 0x0011A278
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerCommitWheelSpin(this.m_count, this.m_callbackObject);
		}
	}

	// Token: 0x04002BE2 RID: 11234
	private int m_count = 1;
}
