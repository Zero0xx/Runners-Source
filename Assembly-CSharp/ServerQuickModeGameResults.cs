using System;

// Token: 0x0200081C RID: 2076
public class ServerQuickModeGameResults
{
	// Token: 0x060037BE RID: 14270 RVA: 0x001262F8 File Offset: 0x001244F8
	public ServerQuickModeGameResults(bool isSuspended)
	{
		this.Initialize();
		this.m_isSuspended = isSuspended;
		if (!this.m_isSuspended)
		{
			StageScoreManager instance = StageScoreManager.Instance;
			PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
			if (instance != null && playerInformation != null)
			{
				this.m_score = instance.FinalScore;
				this.m_numRings = instance.FinalCountData.ring;
				this.m_numFailureRings = (long)playerInformation.NumLostRings;
				this.m_numRedStarRings = instance.FinalCountData.red_ring;
				this.m_distance = instance.FinalCountData.distance;
				this.m_numAnimals = instance.FinalCountData.animal;
				this.m_maxComboCount = StageComboManager.Instance.MaxComboCount;
			}
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

	// Token: 0x060037BF RID: 14271 RVA: 0x00126408 File Offset: 0x00124608
	private void Initialize()
	{
		this.m_score = 0L;
		this.m_numRings = 0L;
		this.m_numFailureRings = 0L;
		this.m_numRedStarRings = 0L;
		this.m_distance = 0L;
		this.m_dailyMissionValue = 0L;
		this.m_numAnimals = 0L;
		this.m_dailyMissionComplete = false;
		this.m_isSuspended = false;
	}

	// Token: 0x04002F17 RID: 12055
	public long m_score;

	// Token: 0x04002F18 RID: 12056
	public long m_numRings;

	// Token: 0x04002F19 RID: 12057
	public long m_numFailureRings;

	// Token: 0x04002F1A RID: 12058
	public long m_numRedStarRings;

	// Token: 0x04002F1B RID: 12059
	public long m_distance;

	// Token: 0x04002F1C RID: 12060
	public long m_numAnimals;

	// Token: 0x04002F1D RID: 12061
	public int m_maxComboCount;

	// Token: 0x04002F1E RID: 12062
	public long m_dailyMissionValue;

	// Token: 0x04002F1F RID: 12063
	public bool m_dailyMissionComplete;

	// Token: 0x04002F20 RID: 12064
	public bool m_isSuspended;
}
