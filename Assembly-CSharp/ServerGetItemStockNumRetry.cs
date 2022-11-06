using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200079F RID: 1951
public class ServerGetItemStockNumRetry : ServerRetryProcess
{
	// Token: 0x060033BD RID: 13245 RVA: 0x0011C1B4 File Offset: 0x0011A3B4
	public ServerGetItemStockNumRetry(int eventId, List<int> itemId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_itemId = itemId;
	}

	// Token: 0x060033BE RID: 13246 RVA: 0x0011C1CC File Offset: 0x0011A3CC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetItemStockNum(this.m_eventId, this.m_itemId, this.m_callbackObject);
		}
	}

	// Token: 0x04002BEB RID: 11243
	private int m_eventId;

	// Token: 0x04002BEC RID: 11244
	private List<int> m_itemId;
}
