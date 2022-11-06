using System;

namespace Message
{
	// Token: 0x02000630 RID: 1584
	public class MsgGetEventRaidBossUserListSucceed : MessageBase
	{
		// Token: 0x06002BAA RID: 11178 RVA: 0x0010A8D0 File Offset: 0x00108AD0
		public MsgGetEventRaidBossUserListSucceed() : base(61507)
		{
		}

		// Token: 0x04002886 RID: 10374
		public ServerEventRaidBossBonus m_bonus;
	}
}
