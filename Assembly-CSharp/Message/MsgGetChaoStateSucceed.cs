using System;

namespace Message
{
	// Token: 0x020005F9 RID: 1529
	public class MsgGetChaoStateSucceed : MessageBase
	{
		// Token: 0x06002B73 RID: 11123 RVA: 0x0010A4C0 File Offset: 0x001086C0
		public MsgGetChaoStateSucceed() : base(61452)
		{
		}

		// Token: 0x04002845 RID: 10309
		public ServerPlayerState m_playerState;
	}
}
