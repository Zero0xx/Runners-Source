using System;

// Token: 0x02000801 RID: 2049
public class ServerGameResults
{
	// Token: 0x060036DB RID: 14043 RVA: 0x001224A0 File Offset: 0x001206A0
	public ServerGameResults(bool isSuspended, bool tutorialStage, bool chaoEggPresent, bool bossStage, int oldNumBossAttack, int? eventId, long? eventValue)
	{
		this.Initialize(oldNumBossAttack);
		this.m_isSuspended = isSuspended;
		this.m_eventId = eventId;
		this.m_eventValue = eventValue;
		LevelInformation levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
		if (!this.m_isSuspended)
		{
			StageScoreManager instance = StageScoreManager.Instance;
			PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
			if (instance != null && playerInformation != null && levelInformation != null)
			{
				this.m_score = ((!bossStage) ? instance.FinalScore : 0L);
				this.m_numRings = instance.FinalCountData.ring;
				this.m_numFailureRings = (long)playerInformation.NumLostRings;
				this.m_numRedStarRings = instance.FinalCountData.red_ring;
				this.m_distance = ((!bossStage) ? instance.FinalCountData.distance : 0L);
				this.m_numAnimals = instance.FinalCountData.animal;
				this.m_reachPoint = 0;
				this.m_clearChapter = false;
				if (levelInformation.NowBoss)
				{
					this.m_numBossAttack = ((!levelInformation.BossDestroy) ? levelInformation.NumBossAttack : 0);
				}
				this.m_maxComboCount = StageComboManager.Instance.MaxComboCount;
			}
			this.m_chaoEggPresent = chaoEggPresent;
		}
		if (levelInformation != null)
		{
			this.m_isBossDestroyed = levelInformation.BossDestroy;
		}
		StageMissionManager stageMissionManager = GameObjectUtil.FindGameObjectComponent<StageMissionManager>("StageMissionManager");
		if (stageMissionManager != null)
		{
			this.m_dailyMissionComplete = stageMissionManager.Completed;
			if (SaveDataManager.Instance != null)
			{
				DailyMissionData dailyMission = SaveDataManager.Instance.PlayerData.DailyMission;
				this.m_dailyMissionValue = dailyMission.progress;
			}
		}
	}

	// Token: 0x060036DC RID: 14044 RVA: 0x0012264C File Offset: 0x0012084C
	public void SetMapProgress(MileageMapState prevMapInfo, ref long[] pointScore, bool existBossInChapter)
	{
		if (EventManager.Instance != null && EventManager.Instance.IsSpecialStage())
		{
			this.m_clearChapter = false;
			this.m_maxChapterScore = 0L;
			this.m_reachPoint = 0;
			return;
		}
		this.m_maxChapterScore = pointScore[5];
		if (!this.m_isSuspended)
		{
			LevelInformation levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
			if (prevMapInfo != null)
			{
				long score = prevMapInfo.m_score;
				long num = score + this.m_score;
				for (int i = 5; i >= 0; i--)
				{
					if (num >= pointScore[i])
					{
						this.m_reachPoint = i;
						break;
					}
				}
				if (this.m_reachPoint >= 5 && !existBossInChapter)
				{
					this.m_clearChapter = true;
				}
			}
			if (levelInformation != null && levelInformation.NowBoss && levelInformation.BossDestroy)
			{
				this.m_clearChapter = true;
			}
		}
	}

	// Token: 0x060036DD RID: 14045 RVA: 0x00122730 File Offset: 0x00120930
	private void Initialize(int oldNumBossAttack)
	{
		this.m_score = 0L;
		this.m_numRings = 0L;
		this.m_numFailureRings = 0L;
		this.m_numRedStarRings = 0L;
		this.m_distance = 0L;
		this.m_dailyMissionValue = 0L;
		this.m_dailyMissionComplete = false;
		this.m_numAnimals = 0L;
		this.m_reachPoint = 0;
		this.m_clearChapter = false;
		this.m_numBossAttack = oldNumBossAttack;
		this.m_chaoEggPresent = false;
		this.m_isSuspended = false;
	}

	// Token: 0x04002E21 RID: 11809
	public long m_score;

	// Token: 0x04002E22 RID: 11810
	public long m_numRings;

	// Token: 0x04002E23 RID: 11811
	public long m_numFailureRings;

	// Token: 0x04002E24 RID: 11812
	public long m_numRedStarRings;

	// Token: 0x04002E25 RID: 11813
	public long m_distance;

	// Token: 0x04002E26 RID: 11814
	public long m_dailyMissionValue;

	// Token: 0x04002E27 RID: 11815
	public bool m_dailyMissionComplete;

	// Token: 0x04002E28 RID: 11816
	public bool m_isSuspended;

	// Token: 0x04002E29 RID: 11817
	public long m_numAnimals;

	// Token: 0x04002E2A RID: 11818
	public int m_reachPoint;

	// Token: 0x04002E2B RID: 11819
	public bool m_clearChapter;

	// Token: 0x04002E2C RID: 11820
	public int m_numBossAttack;

	// Token: 0x04002E2D RID: 11821
	public long m_maxChapterScore;

	// Token: 0x04002E2E RID: 11822
	public bool m_chaoEggPresent;

	// Token: 0x04002E2F RID: 11823
	public bool m_isBossDestroyed;

	// Token: 0x04002E30 RID: 11824
	public int m_maxComboCount;

	// Token: 0x04002E31 RID: 11825
	public int? m_eventId;

	// Token: 0x04002E32 RID: 11826
	public long? m_eventValue;
}
