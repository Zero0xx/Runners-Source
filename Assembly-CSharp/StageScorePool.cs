using System;

// Token: 0x0200031C RID: 796
public class StageScorePool
{
	// Token: 0x1700039D RID: 925
	// (get) Token: 0x06001791 RID: 6033 RVA: 0x000871E0 File Offset: 0x000853E0
	// (set) Token: 0x06001792 RID: 6034 RVA: 0x000871E8 File Offset: 0x000853E8
	public int StoredCount
	{
		get
		{
			return this.m_objectIndex;
		}
		private set
		{
		}
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x000871EC File Offset: 0x000853EC
	public void CheckHalfWay()
	{
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null && this.m_objectIndex != 0)
		{
			if (StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode())
			{
				ServerQuickModeGameResults gameResult = new ServerQuickModeGameResults(false);
				instance.CheckNativeHalfWayQuickModeResultScore(gameResult);
			}
			else
			{
				ServerGameResults gameResult2 = new ServerGameResults(false, false, false, false, 0, null, null);
				instance.CheckNativeHalfWayResultScore(gameResult2);
			}
		}
		this.m_objectIndex = 0;
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x00087274 File Offset: 0x00085474
	public void AddScore(StageScoreData scoreData)
	{
		this.AddScore((ScoreType)scoreData.scoreType, scoreData.scoreValue);
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x0008728C File Offset: 0x0008548C
	public void AddScore(ScoreType type, int score)
	{
		if (this.m_objectIndex >= StageScorePool.ArrayCount)
		{
			Debug.Log("StageScorePool arraySize over");
			return;
		}
		this.scoreDatas[this.m_objectIndex].scoreType = (byte)type;
		if (type == ScoreType.distance)
		{
			int scoreValue = score - this.m_lastDistance;
			this.scoreDatas[this.m_objectIndex].scoreValue = scoreValue;
			this.m_lastDistance = score;
		}
		else
		{
			this.scoreDatas[this.m_objectIndex].scoreValue = score;
		}
		this.m_objectIndex++;
	}

	// Token: 0x06001796 RID: 6038 RVA: 0x00087324 File Offset: 0x00085524
	public void DebugLog()
	{
		Debug.Log("StageScorePool.CurrentDataSize = " + this.m_objectIndex.ToString());
	}

	// Token: 0x0400150B RID: 5387
	public static readonly int ArrayCount = 30000;

	// Token: 0x0400150C RID: 5388
	public StageScoreData[] scoreDatas = new StageScoreData[StageScorePool.ArrayCount];

	// Token: 0x0400150D RID: 5389
	private int m_objectIndex;

	// Token: 0x0400150E RID: 5390
	private int m_lastDistance;
}
