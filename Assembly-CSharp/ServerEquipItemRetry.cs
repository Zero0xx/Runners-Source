using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000741 RID: 1857
public class ServerEquipItemRetry : ServerRetryProcess
{
	// Token: 0x0600315B RID: 12635 RVA: 0x001170C8 File Offset: 0x001152C8
	public ServerEquipItemRetry(List<ItemType> items, GameObject callbackObject) : base(callbackObject)
	{
		this.m_items = items;
	}

	// Token: 0x0600315C RID: 12636 RVA: 0x001170D8 File Offset: 0x001152D8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerEquipItem(this.m_items, this.m_callbackObject);
		}
	}

	// Token: 0x04002B27 RID: 11047
	public List<ItemType> m_items;
}
