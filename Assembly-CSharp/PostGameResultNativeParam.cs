using System;
using System.Runtime.InteropServices;

// Token: 0x0200010E RID: 270
public struct PostGameResultNativeParam
{
	// Token: 0x060007F3 RID: 2035 RVA: 0x0002F07C File Offset: 0x0002D27C
	public void Init(ServerGameResults resultData)
	{
		if (resultData != null)
		{
			this.closed = resultData.m_isSuspended;
			BindingLinkUtility.LongToIntArray(out this.score, resultData.m_score);
			BindingLinkUtility.LongToIntArray(out this.stageMaxScore, resultData.m_maxChapterScore);
			BindingLinkUtility.LongToIntArray(out this.numRings, resultData.m_numRings);
			BindingLinkUtility.LongToIntArray(out this.numFailureRings, resultData.m_numFailureRings);
			BindingLinkUtility.LongToIntArray(out this.numRedStarRings, resultData.m_numRedStarRings);
			BindingLinkUtility.LongToIntArray(out this.distance, resultData.m_distance);
			BindingLinkUtility.LongToIntArray(out this.dailyChallengeValue, resultData.m_dailyMissionValue);
			this.dailyChallengeComplete = resultData.m_dailyMissionComplete;
			BindingLinkUtility.LongToIntArray(out this.numAnimals, resultData.m_numAnimals);
			this.reachPoint = resultData.m_reachPoint;
			this.chapterClear = resultData.m_clearChapter;
			this.numBossAttack = resultData.m_numBossAttack;
			this.getChaoEgg = resultData.m_chaoEggPresent;
			int? num = resultData.m_eventId;
			if (num != null)
			{
				this.eventId = resultData.m_eventId.Value;
				BindingLinkUtility.LongToIntArray(out this.eventValue, resultData.m_eventValue.Value);
			}
			else
			{
				this.eventId = -1;
				BindingLinkUtility.LongToIntArray(out this.eventValue, -1L);
			}
			this.bossDestroyed = resultData.m_isBossDestroyed;
			this.maxComboCount = resultData.m_maxComboCount;
		}
	}

	// Token: 0x0400060D RID: 1549
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] score;

	// Token: 0x0400060E RID: 1550
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] distance;

	// Token: 0x0400060F RID: 1551
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] numRings;

	// Token: 0x04000610 RID: 1552
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] numFailureRings;

	// Token: 0x04000611 RID: 1553
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] numRedStarRings;

	// Token: 0x04000612 RID: 1554
	public bool dailyChallengeComplete;

	// Token: 0x04000613 RID: 1555
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] dailyChallengeValue;

	// Token: 0x04000614 RID: 1556
	public bool closed;

	// Token: 0x04000615 RID: 1557
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] numAnimals;

	// Token: 0x04000616 RID: 1558
	public int reachPoint;

	// Token: 0x04000617 RID: 1559
	public bool chapterClear;

	// Token: 0x04000618 RID: 1560
	public int numBossAttack;

	// Token: 0x04000619 RID: 1561
	public bool getChaoEgg;

	// Token: 0x0400061A RID: 1562
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] stageMaxScore;

	// Token: 0x0400061B RID: 1563
	public int eventId;

	// Token: 0x0400061C RID: 1564
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] eventValue;

	// Token: 0x0400061D RID: 1565
	public bool bossDestroyed;

	// Token: 0x0400061E RID: 1566
	public int maxComboCount;
}
