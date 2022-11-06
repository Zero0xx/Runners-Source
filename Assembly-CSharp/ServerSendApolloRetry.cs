using System;
using UnityEngine;

// Token: 0x02000790 RID: 1936
public class ServerSendApolloRetry : ServerRetryProcess
{
	// Token: 0x06003361 RID: 13153 RVA: 0x0011B630 File Offset: 0x00119830
	public ServerSendApolloRetry(int type, string[] value, GameObject callbackObject) : base(callbackObject)
	{
		this.m_type = type;
		this.m_value = value;
	}

	// Token: 0x06003362 RID: 13154 RVA: 0x0011B648 File Offset: 0x00119848
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSendApollo(this.m_type, this.m_value, this.m_callbackObject);
		}
	}

	// Token: 0x04002BC5 RID: 11205
	public int m_type;

	// Token: 0x04002BC6 RID: 11206
	public string[] m_value;
}
