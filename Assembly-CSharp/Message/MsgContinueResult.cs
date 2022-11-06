using System;

namespace Message
{
	// Token: 0x020005A6 RID: 1446
	public class MsgContinueResult : MessageBase
	{
		// Token: 0x06002B14 RID: 11028 RVA: 0x00109BC0 File Offset: 0x00107DC0
		public MsgContinueResult(bool result) : base(12352)
		{
			this.m_result = result;
		}

		// Token: 0x0400278F RID: 10127
		public readonly bool m_result;
	}
}
