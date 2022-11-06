using System;

namespace Message
{
	// Token: 0x02000593 RID: 1427
	public class MsgBossBoostLevel : MessageBase
	{
		// Token: 0x06002AF7 RID: 10999 RVA: 0x00109898 File Offset: 0x00107A98
		public MsgBossBoostLevel(WispBoostLevel wispBoostLevel, string effect) : base(12325)
		{
			this.m_wispBoostLevel = wispBoostLevel;
			this.m_wispBoostEffect = effect;
		}

		// Token: 0x04002763 RID: 10083
		public WispBoostLevel m_wispBoostLevel;

		// Token: 0x04002764 RID: 10084
		public string m_wispBoostEffect;
	}
}
