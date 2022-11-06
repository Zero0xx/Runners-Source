using System;

namespace Message
{
	// Token: 0x020005AD RID: 1453
	public class MsgExternalGamePause : MessageBase
	{
		// Token: 0x06002B1C RID: 11036 RVA: 0x00109C60 File Offset: 0x00107E60
		public MsgExternalGamePause(bool backMainMenu, bool backKey) : base(12358)
		{
			this.m_backMainMenu = backMainMenu;
			this.m_backKey = backKey;
		}

		// Token: 0x04002796 RID: 10134
		public bool m_backMainMenu;

		// Token: 0x04002797 RID: 10135
		public bool m_backKey;
	}
}
