using System;

namespace Message
{
	// Token: 0x020005F5 RID: 1525
	public class MsgQuickModeActStartSucceed : MessageBase
	{
		// Token: 0x06002B6F RID: 11119 RVA: 0x0010A480 File Offset: 0x00108680
		public MsgQuickModeActStartSucceed() : base(61513)
		{
		}

		// Token: 0x04002840 RID: 10304
		public ServerPlayerState m_playerState;
	}
}
