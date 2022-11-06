using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x02000612 RID: 1554
	public class MsgGetPrizeDailyBattleSucceed : MessageBase
	{
		// Token: 0x06002B8C RID: 11148 RVA: 0x0010A6BC File Offset: 0x001088BC
		public MsgGetPrizeDailyBattleSucceed() : base(61476)
		{
			this.battlePrizeDataList = new List<ServerDailyBattlePrizeData>();
		}

		// Token: 0x0400286B RID: 10347
		public List<ServerDailyBattlePrizeData> battlePrizeDataList;
	}
}
