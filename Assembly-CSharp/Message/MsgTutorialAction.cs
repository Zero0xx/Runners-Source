using System;

namespace Message
{
	// Token: 0x02000656 RID: 1622
	public class MsgTutorialAction : MessageBase
	{
		// Token: 0x06002BD0 RID: 11216 RVA: 0x0010AC10 File Offset: 0x00108E10
		public MsgTutorialAction(HudTutorial.Id id) : base(12344)
		{
			this.m_id = id;
		}

		// Token: 0x040028AD RID: 10413
		public HudTutorial.Id m_id;
	}
}
