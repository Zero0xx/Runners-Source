using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x020005F6 RID: 1526
	public class MsgQuickModePostGameResultsSucceed : MessageBase
	{
		// Token: 0x06002B70 RID: 11120 RVA: 0x0010A490 File Offset: 0x00108690
		public MsgQuickModePostGameResultsSucceed() : base(61514)
		{
		}

		// Token: 0x04002841 RID: 10305
		public ServerPlayerState m_playerState;

		// Token: 0x04002842 RID: 10306
		public List<ServerItemState> m_dailyIncentive;
	}
}
