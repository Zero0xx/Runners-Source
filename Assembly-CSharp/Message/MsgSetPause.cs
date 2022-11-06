using System;

namespace Message
{
	// Token: 0x020005AE RID: 1454
	public class MsgSetPause : MessageBase
	{
		// Token: 0x06002B1D RID: 11037 RVA: 0x00109C7C File Offset: 0x00107E7C
		public MsgSetPause(bool backMainMenu, bool backKey) : base(12359)
		{
			this.m_backMainMenu = backMainMenu;
			this.m_backKey = backKey;
		}

		// Token: 0x04002798 RID: 10136
		public bool m_backMainMenu;

		// Token: 0x04002799 RID: 10137
		public bool m_backKey;
	}
}
