using System;

namespace Message
{
	// Token: 0x020005C4 RID: 1476
	public class MsgMenuItemSetStart : MessageBase
	{
		// Token: 0x06002B35 RID: 11061 RVA: 0x00109EB8 File Offset: 0x001080B8
		public MsgMenuItemSetStart(MsgMenuItemSetStart.SetMode mode) : base(57344)
		{
			this.m_setMode = mode;
		}

		// Token: 0x040027E2 RID: 10210
		public MsgMenuItemSetStart.SetMode m_setMode;

		// Token: 0x020005C5 RID: 1477
		public enum SetMode
		{
			// Token: 0x040027E4 RID: 10212
			NORMAL,
			// Token: 0x040027E5 RID: 10213
			TUTORIAL,
			// Token: 0x040027E6 RID: 10214
			TUTORIAL_SUBCHARA
		}
	}
}
