using System;

// Token: 0x0200031B RID: 795
public struct StageScoreData
{
	// Token: 0x0600178E RID: 6030 RVA: 0x000871AC File Offset: 0x000853AC
	public StageScoreData(byte scoreType, int scoreValue)
	{
		this.scoreType = scoreType;
		this.scoreValue = scoreValue;
	}

	// Token: 0x04001509 RID: 5385
	public byte scoreType;

	// Token: 0x0400150A RID: 5386
	public int scoreValue;
}
