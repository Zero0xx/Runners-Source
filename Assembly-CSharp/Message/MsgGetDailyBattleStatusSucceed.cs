using System;

namespace Message
{
	// Token: 0x0200060D RID: 1549
	public class MsgGetDailyBattleStatusSucceed : MessageBase
	{
		// Token: 0x06002B87 RID: 11143 RVA: 0x0010A600 File Offset: 0x00108800
		public MsgGetDailyBattleStatusSucceed() : base(61471)
		{
			this.battleStatus = new ServerDailyBattleStatus();
			this.endTime = NetBase.GetCurrentTime();
		}

		// Token: 0x0400285F RID: 10335
		public ServerDailyBattleStatus battleStatus;

		// Token: 0x04002860 RID: 10336
		public DateTime endTime;
	}
}
