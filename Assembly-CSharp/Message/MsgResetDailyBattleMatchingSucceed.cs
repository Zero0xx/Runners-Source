using System;

namespace Message
{
	// Token: 0x02000613 RID: 1555
	public class MsgResetDailyBattleMatchingSucceed : MessageBase
	{
		// Token: 0x06002B8D RID: 11149 RVA: 0x0010A6D4 File Offset: 0x001088D4
		public MsgResetDailyBattleMatchingSucceed() : base(61477)
		{
			this.playerState = new ServerPlayerState();
			this.battleDataPair = new ServerDailyBattleDataPair();
			this.endTime = NetBase.GetCurrentTime();
		}

		// Token: 0x0400286C RID: 10348
		public ServerPlayerState playerState;

		// Token: 0x0400286D RID: 10349
		public ServerDailyBattleDataPair battleDataPair;

		// Token: 0x0400286E RID: 10350
		public DateTime endTime;
	}
}
