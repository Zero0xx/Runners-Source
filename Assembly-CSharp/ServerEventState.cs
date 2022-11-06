using System;

// Token: 0x020007E1 RID: 2017
public class ServerEventState
{
	// Token: 0x060035F9 RID: 13817 RVA: 0x00120678 File Offset: 0x0011E878
	public ServerEventState()
	{
		this.m_param = 0L;
		this.m_rewardId = 0;
	}

	// Token: 0x170007A9 RID: 1961
	// (get) Token: 0x060035FA RID: 13818 RVA: 0x00120690 File Offset: 0x0011E890
	// (set) Token: 0x060035FB RID: 13819 RVA: 0x00120698 File Offset: 0x0011E898
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

	// Token: 0x170007AA RID: 1962
	// (get) Token: 0x060035FC RID: 13820 RVA: 0x001206A4 File Offset: 0x0011E8A4
	// (set) Token: 0x060035FD RID: 13821 RVA: 0x001206AC File Offset: 0x0011E8AC
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

	// Token: 0x060035FE RID: 13822 RVA: 0x001206B8 File Offset: 0x0011E8B8
	public void CopyTo(ServerEventState to)
	{
		to.m_param = this.m_param;
		to.m_rewardId = this.m_rewardId;
	}

	// Token: 0x04002D7D RID: 11645
	private long m_param;

	// Token: 0x04002D7E RID: 11646
	private int m_rewardId;
}
