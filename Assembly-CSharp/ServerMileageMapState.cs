using System;

// Token: 0x020007EA RID: 2026
public class ServerMileageMapState
{
	// Token: 0x06003628 RID: 13864 RVA: 0x00120A04 File Offset: 0x0011EC04
	public ServerMileageMapState()
	{
		this.m_episode = 1;
		this.m_chapter = 1;
		this.m_point = 0;
		this.m_numBossAttack = 0;
		this.m_stageTotalScore = 0L;
		this.m_stageMaxScore = 0L;
		this.m_chapterStartTime = DateTime.Now;
	}

	// Token: 0x06003629 RID: 13865 RVA: 0x00120A44 File Offset: 0x0011EC44
	public void CopyTo(ServerMileageMapState to)
	{
		if (to != null)
		{
			to.m_episode = this.m_episode;
			to.m_chapter = this.m_chapter;
			to.m_point = this.m_point;
			to.m_numBossAttack = this.m_numBossAttack;
			to.m_stageTotalScore = this.m_stageTotalScore;
			to.m_stageMaxScore = this.m_stageMaxScore;
			to.m_chapterStartTime = this.m_chapterStartTime;
		}
	}

	// Token: 0x04002DA4 RID: 11684
	public int m_episode;

	// Token: 0x04002DA5 RID: 11685
	public int m_chapter;

	// Token: 0x04002DA6 RID: 11686
	public int m_point;

	// Token: 0x04002DA7 RID: 11687
	public int m_numBossAttack;

	// Token: 0x04002DA8 RID: 11688
	public long m_stageTotalScore;

	// Token: 0x04002DA9 RID: 11689
	public long m_stageMaxScore;

	// Token: 0x04002DAA RID: 11690
	public DateTime m_chapterStartTime;
}
