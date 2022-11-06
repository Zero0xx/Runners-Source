using System;

namespace Message
{
	// Token: 0x0200060F RID: 1551
	public class MsgPostDailyBattleResultSucceed : MessageBase
	{
		// Token: 0x06002B89 RID: 11145 RVA: 0x0010A658 File Offset: 0x00108858
		public MsgPostDailyBattleResultSucceed() : base(61473)
		{
			this.battleStatus = new ServerDailyBattleStatus();
			this.battleDataPair = new ServerDailyBattleDataPair();
			this.rewardFlag = false;
			this.rewardBattleDataPair = null;
		}

		// Token: 0x04002865 RID: 10341
		public ServerDailyBattleStatus battleStatus;

		// Token: 0x04002866 RID: 10342
		public ServerDailyBattleDataPair battleDataPair;

		// Token: 0x04002867 RID: 10343
		public bool rewardFlag;

		// Token: 0x04002868 RID: 10344
		public ServerDailyBattleDataPair rewardBattleDataPair;
	}
}
