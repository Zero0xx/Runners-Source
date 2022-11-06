using System;

namespace Message
{
	// Token: 0x02000617 RID: 1559
	public class MsgRequestEnergySucceed : MessageBase
	{
		// Token: 0x06002B91 RID: 11153 RVA: 0x0010A740 File Offset: 0x00108940
		public MsgRequestEnergySucceed() : base(61481)
		{
		}

		// Token: 0x04002871 RID: 10353
		public ServerPlayerState m_playerState;

		// Token: 0x04002872 RID: 10354
		public long m_resultExpireTime;
	}
}
