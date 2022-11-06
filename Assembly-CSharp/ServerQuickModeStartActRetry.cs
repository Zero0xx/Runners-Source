using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000724 RID: 1828
public class ServerQuickModeStartActRetry : ServerRetryProcess
{
	// Token: 0x0600310B RID: 12555 RVA: 0x00116590 File Offset: 0x00114790
	public ServerQuickModeStartActRetry(List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, bool tutorial, GameObject callbackObject) : base(callbackObject)
	{
		this.m_modifiersItem = modifiersItem;
		this.m_modifiersBoostItem = modifiersBoostItem;
		this.m_tutorial = tutorial;
	}

	// Token: 0x0600310C RID: 12556 RVA: 0x001165B0 File Offset: 0x001147B0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerQuickModeStartAct(this.m_modifiersItem, this.m_modifiersBoostItem, this.m_tutorial, this.m_callbackObject);
		}
	}

	// Token: 0x04002B16 RID: 11030
	public List<ItemType> m_modifiersItem;

	// Token: 0x04002B17 RID: 11031
	public List<BoostItemType> m_modifiersBoostItem;

	// Token: 0x04002B18 RID: 11032
	public List<string> m_distanceFriendIdList;

	// Token: 0x04002B19 RID: 11033
	public bool m_tutorial;
}
