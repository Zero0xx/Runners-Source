using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006ED RID: 1773
public class ServerEventStartActRetry : ServerRetryProcess
{
	// Token: 0x06002FA4 RID: 12196 RVA: 0x0011379C File Offset: 0x0011199C
	public ServerEventStartActRetry(int eventId, int energyExpend, long raidBossId, List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
		this.m_energyExpend = energyExpend;
		this.m_raidBossId = raidBossId;
		this.m_modifiersItem = modifiersItem;
		this.m_modifiersBoostItem = modifiersBoostItem;
	}

	// Token: 0x06002FA5 RID: 12197 RVA: 0x001137CC File Offset: 0x001119CC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerEventStartAct(this.m_eventId, this.m_energyExpend, this.m_raidBossId, this.m_modifiersItem, this.m_modifiersBoostItem, this.m_callbackObject);
		}
	}

	// Token: 0x04002A98 RID: 10904
	public int m_eventId;

	// Token: 0x04002A99 RID: 10905
	public int m_energyExpend;

	// Token: 0x04002A9A RID: 10906
	public long m_raidBossId;

	// Token: 0x04002A9B RID: 10907
	public List<ItemType> m_modifiersItem;

	// Token: 0x04002A9C RID: 10908
	public List<BoostItemType> m_modifiersBoostItem;
}
