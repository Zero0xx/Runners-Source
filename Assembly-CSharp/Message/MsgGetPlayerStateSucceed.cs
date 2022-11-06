using System;

namespace Message
{
	// Token: 0x020005F7 RID: 1527
	public class MsgGetPlayerStateSucceed : MessageBase
	{
		// Token: 0x06002B71 RID: 11121 RVA: 0x0010A4A0 File Offset: 0x001086A0
		public MsgGetPlayerStateSucceed() : base(61450)
		{
		}

		// Token: 0x04002843 RID: 10307
		public ServerPlayerState m_playerState;
	}
}
