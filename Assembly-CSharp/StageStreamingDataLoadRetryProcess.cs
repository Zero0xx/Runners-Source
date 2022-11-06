using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
internal class StageStreamingDataLoadRetryProcess : ServerRetryProcess
{
	// Token: 0x060015A5 RID: 5541 RVA: 0x00077D78 File Offset: 0x00075F78
	public StageStreamingDataLoadRetryProcess(GameObject returnObject, GameModeStage gameModeStage) : base(returnObject)
	{
		this.m_gameModeStage = gameModeStage;
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x00077D88 File Offset: 0x00075F88
	public override void Retry()
	{
		this.m_retryCount++;
		if (this.m_gameModeStage != null)
		{
			this.m_gameModeStage.RetryStreamingDataLoad(this.m_retryCount);
		}
	}

	// Token: 0x04001326 RID: 4902
	private GameModeStage m_gameModeStage;

	// Token: 0x04001327 RID: 4903
	private int m_retryCount;
}
