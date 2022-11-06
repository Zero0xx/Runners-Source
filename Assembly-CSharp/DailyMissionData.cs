using System;

// Token: 0x02000261 RID: 609
public class DailyMissionData
{
	// Token: 0x06001065 RID: 4197 RVA: 0x0005FFC0 File Offset: 0x0005E1C0
	public DailyMissionData()
	{
		this.id = 1;
		this.progress = 0L;
		this.date = 1;
		this.reward_max = 3;
		for (int i = 0; i < 7; i++)
		{
			this.reward_id[i] = 0;
			this.reward_count[i] = 1;
		}
		this.clear_count = 0;
		this.missions_complete = false;
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x0006003C File Offset: 0x0005E23C
	public void CopyTo(DailyMissionData dst)
	{
		dst.id = this.id;
		dst.progress = this.progress;
		dst.date = this.date;
		dst.reward_max = this.reward_max;
		for (int i = 0; i < 7; i++)
		{
			dst.reward_id[i] = this.reward_id[i];
			dst.reward_count[i] = this.reward_count[i];
		}
		dst.clear_count = this.clear_count;
		dst.missions_complete = this.missions_complete;
	}

	// Token: 0x04000EE6 RID: 3814
	public int id;

	// Token: 0x04000EE7 RID: 3815
	public long progress;

	// Token: 0x04000EE8 RID: 3816
	public int date;

	// Token: 0x04000EE9 RID: 3817
	public int[] reward_id = new int[7];

	// Token: 0x04000EEA RID: 3818
	public int[] reward_count = new int[7];

	// Token: 0x04000EEB RID: 3819
	public int reward_max;

	// Token: 0x04000EEC RID: 3820
	public int max;

	// Token: 0x04000EED RID: 3821
	public int clear_count;

	// Token: 0x04000EEE RID: 3822
	public bool missions_complete;

	// Token: 0x02000262 RID: 610
	private enum Reward
	{
		// Token: 0x04000EF0 RID: 3824
		DAYS = 7
	}
}
