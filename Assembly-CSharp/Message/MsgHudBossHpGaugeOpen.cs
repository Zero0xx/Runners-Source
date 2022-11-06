using System;

namespace Message
{
	// Token: 0x020005B6 RID: 1462
	public class MsgHudBossHpGaugeOpen : MessageBase
	{
		// Token: 0x06002B25 RID: 11045 RVA: 0x00109D3C File Offset: 0x00107F3C
		public MsgHudBossHpGaugeOpen(BossType bossType, int level, int hp, int hpMax) : base(49152)
		{
			this.m_bossType = bossType;
			this.m_level = level;
			this.m_hp = hp;
			this.m_hpMax = hpMax;
		}

		// Token: 0x040027A8 RID: 10152
		public BossType m_bossType;

		// Token: 0x040027A9 RID: 10153
		public int m_level;

		// Token: 0x040027AA RID: 10154
		public int m_hp;

		// Token: 0x040027AB RID: 10155
		public int m_hpMax;
	}
}
