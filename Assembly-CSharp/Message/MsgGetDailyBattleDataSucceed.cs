using System;

namespace Message
{
	// Token: 0x02000610 RID: 1552
	public class MsgGetDailyBattleDataSucceed : MessageBase
	{
		// Token: 0x06002B8A RID: 11146 RVA: 0x0010A68C File Offset: 0x0010888C
		public MsgGetDailyBattleDataSucceed() : base(61474)
		{
			this.battleDataPair = new ServerDailyBattleDataPair();
		}

		// Token: 0x04002869 RID: 10345
		public ServerDailyBattleDataPair battleDataPair;
	}
}
