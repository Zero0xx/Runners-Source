using System;

namespace Message
{
	// Token: 0x02000595 RID: 1429
	public class MsgBossPlayerDamage : MessageBase
	{
		// Token: 0x06002AF9 RID: 11001 RVA: 0x001098C4 File Offset: 0x00107AC4
		public MsgBossPlayerDamage(bool dead) : base(12326)
		{
			this.m_dead = dead;
		}

		// Token: 0x04002768 RID: 10088
		public bool m_dead;
	}
}
