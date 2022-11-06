using System;

// Token: 0x020007E0 RID: 2016
public class ServerEventReward : ServerItemState
{
	// Token: 0x060035F3 RID: 13811 RVA: 0x00120614 File Offset: 0x0011E814
	public ServerEventReward()
	{
		this.m_rewardId = 0;
		this.m_param = 0L;
	}

	// Token: 0x170007A7 RID: 1959
	// (get) Token: 0x060035F4 RID: 13812 RVA: 0x0012062C File Offset: 0x0011E82C
	// (set) Token: 0x060035F5 RID: 13813 RVA: 0x00120634 File Offset: 0x0011E834
	public int RewardId
	{
		get
		{
			return this.m_rewardId;
		}
		set
		{
			this.m_rewardId = value;
		}
	}

	// Token: 0x170007A8 RID: 1960
	// (get) Token: 0x060035F6 RID: 13814 RVA: 0x00120640 File Offset: 0x0011E840
	// (set) Token: 0x060035F7 RID: 13815 RVA: 0x00120648 File Offset: 0x0011E848
	public long Param
	{
		get
		{
			return this.m_param;
		}
		set
		{
			this.m_param = value;
		}
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x00120654 File Offset: 0x0011E854
	public void CopyTo(ServerEventReward to)
	{
		base.CopyTo(to);
		to.m_rewardId = this.m_rewardId;
		to.m_param = this.m_param;
	}

	// Token: 0x04002D7B RID: 11643
	private int m_rewardId;

	// Token: 0x04002D7C RID: 11644
	private long m_param;
}
