using System;

namespace Message
{
	// Token: 0x02000631 RID: 1585
	public class MsgEventActStartSucceed : MessageBase
	{
		// Token: 0x06002BAB RID: 11179 RVA: 0x0010A8E0 File Offset: 0x00108AE0
		public MsgEventActStartSucceed() : base(61508)
		{
		}

		// Token: 0x04002887 RID: 10375
		public ServerPlayerState m_playerState;
	}
}
