using System;

namespace Message
{
	// Token: 0x02000609 RID: 1545
	public class MsgCommitChaoWheelSpicSucceed : MessageBase
	{
		// Token: 0x06002B83 RID: 11139 RVA: 0x0010A5C0 File Offset: 0x001087C0
		public MsgCommitChaoWheelSpicSucceed() : base(61467)
		{
		}

		// Token: 0x04002859 RID: 10329
		public ServerPlayerState m_playerState;

		// Token: 0x0400285A RID: 10330
		public ServerChaoWheelOptions m_chaoWheelOptions;

		// Token: 0x0400285B RID: 10331
		public ServerSpinResultGeneral m_resultSpinResultGeneral;
	}
}
