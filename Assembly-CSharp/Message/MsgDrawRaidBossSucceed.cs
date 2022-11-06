using System;

namespace Message
{
	// Token: 0x02000634 RID: 1588
	public class MsgDrawRaidBossSucceed : MessageBase
	{
		// Token: 0x06002BAE RID: 11182 RVA: 0x0010A910 File Offset: 0x00108B10
		public MsgDrawRaidBossSucceed() : base(61511)
		{
		}

		// Token: 0x04002889 RID: 10377
		public ServerEventRaidBossState m_raidBossState;
	}
}
