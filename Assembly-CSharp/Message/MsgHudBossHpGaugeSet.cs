using System;

namespace Message
{
	// Token: 0x020005B7 RID: 1463
	public class MsgHudBossHpGaugeSet : MessageBase
	{
		// Token: 0x06002B26 RID: 11046 RVA: 0x00109D74 File Offset: 0x00107F74
		public MsgHudBossHpGaugeSet(int hp, int hpMax) : base(49153)
		{
			this.m_hp = hp;
			this.m_hpMax = hpMax;
		}

		// Token: 0x040027AC RID: 10156
		public int m_hp;

		// Token: 0x040027AD RID: 10157
		public int m_hpMax;
	}
}
