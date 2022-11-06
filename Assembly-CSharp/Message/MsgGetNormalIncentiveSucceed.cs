using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x0200061F RID: 1567
	public class MsgGetNormalIncentiveSucceed : MessageBase
	{
		// Token: 0x06002B99 RID: 11161 RVA: 0x0010A7C0 File Offset: 0x001089C0
		public MsgGetNormalIncentiveSucceed() : base(61490)
		{
		}

		// Token: 0x0400287B RID: 10363
		public ServerPlayerState m_playerState;

		// Token: 0x0400287C RID: 10364
		public List<ServerPresentState> m_incentive;
	}
}
