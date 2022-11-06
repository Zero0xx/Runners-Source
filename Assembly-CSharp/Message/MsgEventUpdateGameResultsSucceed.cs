using System;

namespace Message
{
	// Token: 0x02000632 RID: 1586
	public class MsgEventUpdateGameResultsSucceed : MessageBase
	{
		// Token: 0x06002BAC RID: 11180 RVA: 0x0010A8F0 File Offset: 0x00108AF0
		public MsgEventUpdateGameResultsSucceed() : base(61509)
		{
		}

		// Token: 0x04002888 RID: 10376
		public ServerEventRaidBossBonus m_bonus;
	}
}
