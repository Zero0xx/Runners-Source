using System;

// Token: 0x02000826 RID: 2086
public class ServerWeeklyLeaderboardOptions
{
	// Token: 0x060037FF RID: 14335 RVA: 0x00127B98 File Offset: 0x00125D98
	public ServerWeeklyLeaderboardOptions()
	{
		this.mode = 0;
		this.type = 0;
		this.param = 0;
		this.startTime = 0;
		this.endTime = 0;
	}

	// Token: 0x17000854 RID: 2132
	// (get) Token: 0x06003800 RID: 14336 RVA: 0x00127BD0 File Offset: 0x00125DD0
	// (set) Token: 0x06003801 RID: 14337 RVA: 0x00127BD8 File Offset: 0x00125DD8
	public int mode { get; set; }

	// Token: 0x17000855 RID: 2133
	// (get) Token: 0x06003802 RID: 14338 RVA: 0x00127BE4 File Offset: 0x00125DE4
	// (set) Token: 0x06003803 RID: 14339 RVA: 0x00127BEC File Offset: 0x00125DEC
	public int type { get; set; }

	// Token: 0x17000856 RID: 2134
	// (get) Token: 0x06003804 RID: 14340 RVA: 0x00127BF8 File Offset: 0x00125DF8
	// (set) Token: 0x06003805 RID: 14341 RVA: 0x00127C00 File Offset: 0x00125E00
	public int param { get; set; }

	// Token: 0x17000857 RID: 2135
	// (get) Token: 0x06003806 RID: 14342 RVA: 0x00127C0C File Offset: 0x00125E0C
	// (set) Token: 0x06003807 RID: 14343 RVA: 0x00127C14 File Offset: 0x00125E14
	public int startTime { get; set; }

	// Token: 0x17000858 RID: 2136
	// (get) Token: 0x06003808 RID: 14344 RVA: 0x00127C20 File Offset: 0x00125E20
	// (set) Token: 0x06003809 RID: 14345 RVA: 0x00127C28 File Offset: 0x00125E28
	public int endTime { get; set; }

	// Token: 0x17000859 RID: 2137
	// (get) Token: 0x0600380A RID: 14346 RVA: 0x00127C34 File Offset: 0x00125E34
	public RankingUtil.RankingScoreType rankingScoreType
	{
		get
		{
			RankingUtil.RankingScoreType result = RankingUtil.RankingScoreType.HIGH_SCORE;
			if (this.type != 0)
			{
				result = RankingUtil.RankingScoreType.TOTAL_SCORE;
			}
			return result;
		}
	}

	// Token: 0x0600380B RID: 14347 RVA: 0x00127C54 File Offset: 0x00125E54
	public void CopyTo(ServerWeeklyLeaderboardOptions to)
	{
		to.mode = this.mode;
		to.type = this.type;
		to.param = this.param;
		to.startTime = this.startTime;
		to.endTime = this.endTime;
	}
}
