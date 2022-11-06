using System;

namespace Message
{
	// Token: 0x02000604 RID: 1540
	public class MsgCommitWheelSpinSucceed : MessageBase
	{
		// Token: 0x06002B7E RID: 11134 RVA: 0x0010A570 File Offset: 0x00108770
		public MsgCommitWheelSpinSucceed() : base(61461)
		{
		}

		// Token: 0x04002850 RID: 10320
		public ServerPlayerState m_playerState;

		// Token: 0x04002851 RID: 10321
		public ServerWheelOptions m_wheelOptions;

		// Token: 0x04002852 RID: 10322
		public ServerSpinResultGeneral m_resultSpinResultGeneral;
	}
}
