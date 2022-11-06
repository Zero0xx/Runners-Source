using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x02000611 RID: 1553
	public class MsgGetDailyBattleDataHistorySucceed : MessageBase
	{
		// Token: 0x06002B8B RID: 11147 RVA: 0x0010A6A4 File Offset: 0x001088A4
		public MsgGetDailyBattleDataHistorySucceed() : base(61475)
		{
			this.battleDataPairList = new List<ServerDailyBattleDataPair>();
		}

		// Token: 0x0400286A RID: 10346
		public List<ServerDailyBattleDataPair> battleDataPairList;
	}
}
