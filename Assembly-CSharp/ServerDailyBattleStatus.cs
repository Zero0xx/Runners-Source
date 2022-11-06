using System;

// Token: 0x020006C1 RID: 1729
public class ServerDailyBattleStatus
{
	// Token: 0x06002E79 RID: 11897 RVA: 0x001119E0 File Offset: 0x0010FBE0
	public ServerDailyBattleStatus()
	{
		this.numWin = 0;
		this.numLose = 0;
		this.numDraw = 0;
		this.numLoseByDefault = 0;
		this.goOnWin = 0;
		this.goOnLose = 0;
	}

	// Token: 0x06002E7A RID: 11898 RVA: 0x00111A20 File Offset: 0x0010FC20
	public void Dump()
	{
		Debug.Log(string.Format("ServerDailyBattleStatus  numWin:{0} numLose:{1} numDraw:{2} numLoseByDefault:{3} goOnWin:{4} goOnLose:{5}", new object[]
		{
			this.numWin,
			this.numLose,
			this.numDraw,
			this.numLoseByDefault,
			this.goOnWin,
			this.goOnLose
		}));
	}

	// Token: 0x06002E7B RID: 11899 RVA: 0x00111A98 File Offset: 0x0010FC98
	public void CopyTo(ServerDailyBattleStatus dest)
	{
		dest.numWin = this.numWin;
		dest.numLose = this.numLose;
		dest.numDraw = this.numDraw;
		dest.numLoseByDefault = this.numLoseByDefault;
		dest.goOnWin = this.goOnWin;
		dest.goOnLose = this.goOnLose;
	}

	// Token: 0x04002A2A RID: 10794
	public int numWin;

	// Token: 0x04002A2B RID: 10795
	public int numLose;

	// Token: 0x04002A2C RID: 10796
	public int numDraw;

	// Token: 0x04002A2D RID: 10797
	public int numLoseByDefault;

	// Token: 0x04002A2E RID: 10798
	public int goOnWin;

	// Token: 0x04002A2F RID: 10799
	public int goOnLose;
}
