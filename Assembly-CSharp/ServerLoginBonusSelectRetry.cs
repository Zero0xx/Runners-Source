using System;
using UnityEngine;

// Token: 0x0200076A RID: 1898
public class ServerLoginBonusSelectRetry : ServerRetryProcess
{
	// Token: 0x0600329F RID: 12959 RVA: 0x00119A10 File Offset: 0x00117C10
	public ServerLoginBonusSelectRetry(int rewardId, int rewardDays, int rewardSelect, int firstRewardDays, int firstRewardSelect, GameObject callbackObject) : base(callbackObject)
	{
		this.m_rewardId = rewardId;
		this.m_rewardDays = rewardDays;
		this.m_rewardSelect = rewardSelect;
		this.m_firstRewardDays = firstRewardDays;
		this.m_firstRewardSelect = firstRewardSelect;
	}

	// Token: 0x060032A0 RID: 12960 RVA: 0x00119A40 File Offset: 0x00117C40
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerLoginBonusSelect(this.m_rewardId, this.m_rewardDays, this.m_rewardSelect, this.m_firstRewardDays, this.m_firstRewardSelect, this.m_callbackObject);
		}
	}

	// Token: 0x04002B84 RID: 11140
	private int m_rewardId;

	// Token: 0x04002B85 RID: 11141
	private int m_rewardDays;

	// Token: 0x04002B86 RID: 11142
	private int m_rewardSelect;

	// Token: 0x04002B87 RID: 11143
	private int m_firstRewardDays;

	// Token: 0x04002B88 RID: 11144
	private int m_firstRewardSelect;
}
