using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000779 RID: 1913
public class ServerUpdateMessageRetry : ServerRetryProcess
{
	// Token: 0x060032FD RID: 13053 RVA: 0x0011A78C File Offset: 0x0011898C
	public ServerUpdateMessageRetry(List<int> messageIdList, List<int> operatorMessageIdList, GameObject callbackObject) : base(callbackObject)
	{
		this.m_messageIdList = messageIdList;
		this.m_operatorMessageIdList = operatorMessageIdList;
	}

	// Token: 0x060032FE RID: 13054 RVA: 0x0011A7A4 File Offset: 0x001189A4
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerUpdateMessage(this.m_messageIdList, this.m_operatorMessageIdList, this.m_callbackObject);
		}
	}

	// Token: 0x04002BA2 RID: 11170
	public List<int> m_messageIdList;

	// Token: 0x04002BA3 RID: 11171
	public List<int> m_operatorMessageIdList;
}
