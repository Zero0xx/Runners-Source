using System;

namespace Message
{
	// Token: 0x02000657 RID: 1623
	public class MsgTutorialQuickMode : MessageBase
	{
		// Token: 0x06002BD1 RID: 11217 RVA: 0x0010AC24 File Offset: 0x00108E24
		public MsgTutorialQuickMode(HudTutorial.Id id) : base(12345)
		{
			this.m_id = id;
		}

		// Token: 0x040028AE RID: 10414
		public HudTutorial.Id m_id;
	}
}
