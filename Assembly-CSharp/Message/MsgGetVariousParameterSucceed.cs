using System;

namespace Message
{
	// Token: 0x020005EF RID: 1519
	public class MsgGetVariousParameterSucceed : MessageBase
	{
		// Token: 0x06002B69 RID: 11113 RVA: 0x0010A420 File Offset: 0x00108620
		public MsgGetVariousParameterSucceed() : base(61444)
		{
		}

		// Token: 0x04002831 RID: 10289
		public int m_energyRefreshTime;

		// Token: 0x04002832 RID: 10290
		public int m_energyRecoveryMax;

		// Token: 0x04002833 RID: 10291
		public int m_onePlayCmCount;

		// Token: 0x04002834 RID: 10292
		public int m_onePlayContinueCount;

		// Token: 0x04002835 RID: 10293
		public int m_cmSkipCount;

		// Token: 0x04002836 RID: 10294
		public bool m_isPurchased;
	}
}
