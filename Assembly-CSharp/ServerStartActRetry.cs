using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200073E RID: 1854
public class ServerStartActRetry : ServerRetryProcess
{
	// Token: 0x0600314C RID: 12620 RVA: 0x00116EB4 File Offset: 0x001150B4
	public ServerStartActRetry(List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, List<string> distanceFriendIdList, bool tutorial, int? eventId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_modifiersItem = modifiersItem;
		this.m_modifiersBoostItem = modifiersBoostItem;
		this.m_distanceFriendIdList = distanceFriendIdList;
		this.m_tutorial = tutorial;
		this.m_eventId = eventId;
	}

	// Token: 0x0600314D RID: 12621 RVA: 0x00116EE4 File Offset: 0x001150E4
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerStartAct(this.m_modifiersItem, this.m_modifiersBoostItem, this.m_distanceFriendIdList, this.m_tutorial, this.m_eventId, this.m_callbackObject);
		}
	}

	// Token: 0x04002B20 RID: 11040
	public List<ItemType> m_modifiersItem;

	// Token: 0x04002B21 RID: 11041
	public List<BoostItemType> m_modifiersBoostItem;

	// Token: 0x04002B22 RID: 11042
	public List<string> m_distanceFriendIdList;

	// Token: 0x04002B23 RID: 11043
	public bool m_tutorial;

	// Token: 0x04002B24 RID: 11044
	private int? m_eventId;
}
