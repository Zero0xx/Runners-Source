using System;

namespace Message
{
	// Token: 0x02000653 RID: 1619
	public class MsgTutorialItem : MessageBase
	{
		// Token: 0x06002BCD RID: 11213 RVA: 0x0010ABD8 File Offset: 0x00108DD8
		public MsgTutorialItem(HudTutorial.Id id) : base(12341)
		{
			this.m_id = id;
		}

		// Token: 0x040028AB RID: 10411
		public HudTutorial.Id m_id;
	}
}
