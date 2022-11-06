using System;
using UnityEngine;

// Token: 0x020007D5 RID: 2005
public abstract class ServerRetryProcess
{
	// Token: 0x0600356A RID: 13674 RVA: 0x0011F708 File Offset: 0x0011D908
	public ServerRetryProcess(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x0600356B RID: 13675
	public abstract void Retry();

	// Token: 0x04002D24 RID: 11556
	protected GameObject m_callbackObject;
}
