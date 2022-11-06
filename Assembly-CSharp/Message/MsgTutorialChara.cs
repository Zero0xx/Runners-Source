using System;

namespace Message
{
	// Token: 0x02000655 RID: 1621
	public class MsgTutorialChara : MessageBase
	{
		// Token: 0x06002BCF RID: 11215 RVA: 0x0010ABFC File Offset: 0x00108DFC
		public MsgTutorialChara(HudTutorial.Id id) : base(12343)
		{
			this.m_id = id;
		}

		// Token: 0x040028AC RID: 10412
		public HudTutorial.Id m_id;
	}
}
