using System;

namespace Message
{
	// Token: 0x02000607 RID: 1543
	public class MsgGetPrizeWheelSpinGeneralSucceed : MessageBase
	{
		// Token: 0x06002B81 RID: 11137 RVA: 0x0010A5A0 File Offset: 0x001087A0
		public MsgGetPrizeWheelSpinGeneralSucceed() : base(61465)
		{
		}

		// Token: 0x04002857 RID: 10327
		public ServerPrizeState prizeState;
	}
}
