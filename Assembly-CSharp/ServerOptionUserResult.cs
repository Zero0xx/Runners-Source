using System;

// Token: 0x02000813 RID: 2067
public class ServerOptionUserResult
{
	// Token: 0x06003770 RID: 14192 RVA: 0x00124400 File Offset: 0x00122600
	public void CopyTo(ServerOptionUserResult to)
	{
		if (to != null)
		{
			to.m_totalSumHightScore = this.m_totalSumHightScore;
			to.m_quickTotalSumHightScore = this.m_quickTotalSumHightScore;
			to.m_numTakeAllRings = this.m_numTakeAllRings;
			to.m_numTakeAllRedRings = this.m_numTakeAllRedRings;
			to.m_numChaoRoulette = this.m_numChaoRoulette;
			to.m_numItemRoulette = this.m_numItemRoulette;
			to.m_numJackPot = this.m_numJackPot;
			to.m_numMaximumJackPotRings = this.m_numMaximumJackPotRings;
			to.m_numSupport = this.m_numSupport;
		}
	}

	// Token: 0x04002EBE RID: 11966
	public long m_totalSumHightScore;

	// Token: 0x04002EBF RID: 11967
	public long m_quickTotalSumHightScore;

	// Token: 0x04002EC0 RID: 11968
	public long m_numTakeAllRings;

	// Token: 0x04002EC1 RID: 11969
	public long m_numTakeAllRedRings;

	// Token: 0x04002EC2 RID: 11970
	public int m_numChaoRoulette;

	// Token: 0x04002EC3 RID: 11971
	public int m_numItemRoulette;

	// Token: 0x04002EC4 RID: 11972
	public int m_numJackPot;

	// Token: 0x04002EC5 RID: 11973
	public int m_numMaximumJackPotRings;

	// Token: 0x04002EC6 RID: 11974
	public int m_numSupport;
}
