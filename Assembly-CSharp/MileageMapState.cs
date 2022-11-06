using System;

// Token: 0x020002E1 RID: 737
public class MileageMapState
{
	// Token: 0x0600155C RID: 5468 RVA: 0x000765CC File Offset: 0x000747CC
	public MileageMapState()
	{
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x000765E4 File Offset: 0x000747E4
	public MileageMapState(ServerMileageMapState src)
	{
		this.Set(src);
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x00076604 File Offset: 0x00074804
	public MileageMapState(MileageMapState src)
	{
		this.Set(src);
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x00076624 File Offset: 0x00074824
	public void Set(ServerMileageMapState src)
	{
		if (src != null)
		{
			this.m_episode = src.m_episode;
			this.m_chapter = src.m_chapter;
			this.m_point = src.m_point;
			this.m_score = src.m_stageTotalScore;
		}
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x00076668 File Offset: 0x00074868
	public void Set(MileageMapState src)
	{
		if (src != null)
		{
			this.m_episode = src.m_episode;
			this.m_chapter = src.m_chapter;
			this.m_point = src.m_point;
			this.m_score = src.m_score;
		}
	}

	// Token: 0x040012C1 RID: 4801
	public int m_episode = 1;

	// Token: 0x040012C2 RID: 4802
	public int m_chapter = 1;

	// Token: 0x040012C3 RID: 4803
	public int m_point;

	// Token: 0x040012C4 RID: 4804
	public long m_score;
}
