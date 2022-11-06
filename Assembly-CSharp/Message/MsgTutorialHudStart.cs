using System;

namespace Message
{
	// Token: 0x0200065A RID: 1626
	public class MsgTutorialHudStart : MessageBase
	{
		// Token: 0x06002BD5 RID: 11221 RVA: 0x0010AC94 File Offset: 0x00108E94
		public MsgTutorialHudStart(HudTutorial.Id id, BossType bossType) : base(12348)
		{
			this.m_id = id;
			this.m_bossType = bossType;
		}

		// Token: 0x040028B4 RID: 10420
		public HudTutorial.Id m_id;

		// Token: 0x040028B5 RID: 10421
		public BossType m_bossType;
	}
}
