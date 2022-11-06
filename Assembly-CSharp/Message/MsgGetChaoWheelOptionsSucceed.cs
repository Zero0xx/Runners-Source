using System;

namespace Message
{
	// Token: 0x02000605 RID: 1541
	public class MsgGetChaoWheelOptionsSucceed : MessageBase
	{
		// Token: 0x06002B7F RID: 11135 RVA: 0x0010A580 File Offset: 0x00108780
		public MsgGetChaoWheelOptionsSucceed() : base(61463)
		{
		}

		// Token: 0x04002853 RID: 10323
		public ServerPlayerState m_playerState;

		// Token: 0x04002854 RID: 10324
		public ServerChaoWheelOptions m_options;
	}
}
