using System;

namespace Message
{
	// Token: 0x020005F8 RID: 1528
	public class MsgGetCharacterStateSucceed : MessageBase
	{
		// Token: 0x06002B72 RID: 11122 RVA: 0x0010A4B0 File Offset: 0x001086B0
		public MsgGetCharacterStateSucceed() : base(61451)
		{
		}

		// Token: 0x04002844 RID: 10308
		public ServerPlayerState m_playerState;
	}
}
