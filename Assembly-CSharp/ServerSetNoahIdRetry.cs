using System;
using UnityEngine;

// Token: 0x02000792 RID: 1938
public class ServerSetNoahIdRetry : ServerRetryProcess
{
	// Token: 0x06003365 RID: 13157 RVA: 0x0011B6C8 File Offset: 0x001198C8
	public ServerSetNoahIdRetry(string noahId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_noahId = noahId;
	}

	// Token: 0x06003366 RID: 13158 RVA: 0x0011B6D8 File Offset: 0x001198D8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSetNoahId(this.m_noahId, this.m_callbackObject);
		}
	}

	// Token: 0x04002BC7 RID: 11207
	public string m_noahId;
}
