using System;

namespace Message
{
	// Token: 0x02000606 RID: 1542
	public class MsgGetPrizeChaoWheelSpinSucceed : MessageBase
	{
		// Token: 0x06002B80 RID: 11136 RVA: 0x0010A590 File Offset: 0x00108790
		public MsgGetPrizeChaoWheelSpinSucceed() : base(61464)
		{
		}

		// Token: 0x04002855 RID: 10325
		public ServerPrizeState m_prizeState;

		// Token: 0x04002856 RID: 10326
		public int m_type;
	}
}
