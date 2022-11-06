using System;
using UnityEngine;

// Token: 0x020007DA RID: 2010
public class ServerEventGameResults
{
	// Token: 0x06003587 RID: 13703 RVA: 0x0011FCC4 File Offset: 0x0011DEC4
	public ServerEventGameResults(bool isSuspended, int eventId, long eventValue, long raidBossId)
	{
		this.m_isSuspended = isSuspended;
		this.m_numRings = 0L;
		this.m_numFailureRings = 0L;
		this.m_numRedStarRings = 0L;
		this.m_dailyMissionValue = 0L;
		this.m_dailyMissionComplete = false;
		this.m_eventId = eventId;
		this.m_eventValue = eventValue;
		this.m_raidBossId = raidBossId;
		this.m_raidBossDamage = 0;
		this.m_isRaidBossBeat = false;
		if (!this.m_isSuspended)
		{
			StageScoreManager instance = StageScoreManager.Instance;
			if (instance != null)
			{
				this.m_numRedStarRings = instance.FinalCountData.red_ring;
			}
			LevelInformation levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
			if (levelInformation != null)
			{
				int num = 0;
				RaidBossData currentRaidData = RaidBossInfo.currentRaidData;
				if (currentRaidData != null)
				{
					num = (int)(currentRaidData.hpMax - currentRaidData.hp);
				}
				this.m_raidBossDamage = Mathf.Max(levelInformation.NumBossAttack - num, 0);
				this.m_isRaidBossBeat = levelInformation.BossDestroy;
			}
		}
	}

	// Token: 0x04002D39 RID: 11577
	public long m_numRings;

	// Token: 0x04002D3A RID: 11578
	public long m_numFailureRings;

	// Token: 0x04002D3B RID: 11579
	public long m_numRedStarRings;

	// Token: 0x04002D3C RID: 11580
	public bool m_isSuspended;

	// Token: 0x04002D3D RID: 11581
	public long m_dailyMissionValue;

	// Token: 0x04002D3E RID: 11582
	public bool m_dailyMissionComplete;

	// Token: 0x04002D3F RID: 11583
	public int m_eventId;

	// Token: 0x04002D40 RID: 11584
	public long m_eventValue;

	// Token: 0x04002D41 RID: 11585
	public long m_raidBossId;

	// Token: 0x04002D42 RID: 11586
	public int m_raidBossDamage;

	// Token: 0x04002D43 RID: 11587
	public bool m_isRaidBossBeat;
}
