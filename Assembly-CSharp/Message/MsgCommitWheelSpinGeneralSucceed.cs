using System;

namespace Message
{
	// Token: 0x02000602 RID: 1538
	public class MsgCommitWheelSpinGeneralSucceed : MessageBase
	{
		// Token: 0x06002B7C RID: 11132 RVA: 0x0010A550 File Offset: 0x00108750
		public MsgCommitWheelSpinGeneralSucceed() : base(61460)
		{
		}

		// Token: 0x0400284C RID: 10316
		public ServerPlayerState m_playerState;

		// Token: 0x0400284D RID: 10317
		public ServerWheelOptionsGeneral m_wheelOptionsGeneral;

		// Token: 0x0400284E RID: 10318
		public ServerSpinResultGeneral m_resultSpinResultGeneral;
	}
}
