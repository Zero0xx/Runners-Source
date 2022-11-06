using System;

namespace Message
{
	// Token: 0x0200060E RID: 1550
	public class MsgUpdateDailyBattleStatusSucceed : MessageBase
	{
		// Token: 0x06002B88 RID: 11144 RVA: 0x0010A624 File Offset: 0x00108824
		public MsgUpdateDailyBattleStatusSucceed() : base(61472)
		{
			this.battleStatus = new ServerDailyBattleStatus();
			this.endTime = NetBase.GetCurrentTime();
			this.rewardFlag = false;
			this.rewardBattleDataPair = null;
		}

		// Token: 0x04002861 RID: 10337
		public ServerDailyBattleStatus battleStatus;

		// Token: 0x04002862 RID: 10338
		public DateTime endTime;

		// Token: 0x04002863 RID: 10339
		public bool rewardFlag;

		// Token: 0x04002864 RID: 10340
		public ServerDailyBattleDataPair rewardBattleDataPair;
	}
}
