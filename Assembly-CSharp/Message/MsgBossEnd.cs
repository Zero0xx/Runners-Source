using System;

namespace Message
{
	// Token: 0x0200058D RID: 1421
	public class MsgBossEnd : MessageBase
	{
		// Token: 0x06002AF1 RID: 10993 RVA: 0x00109830 File Offset: 0x00107A30
		public MsgBossEnd(bool dead) : base(12307)
		{
			this.m_dead = dead;
		}

		// Token: 0x04002759 RID: 10073
		public bool m_dead;
	}
}
