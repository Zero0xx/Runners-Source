using System;

namespace Message
{
	// Token: 0x0200059A RID: 1434
	public class MsgChangeChara : MessageBase
	{
		// Token: 0x06002B05 RID: 11013 RVA: 0x00109A48 File Offset: 0x00107C48
		public MsgChangeChara() : base(12313)
		{
		}

		// Token: 0x04002776 RID: 10102
		public bool m_changeByBtn;

		// Token: 0x04002777 RID: 10103
		public bool m_changeByMiss;

		// Token: 0x04002778 RID: 10104
		public bool m_succeed;
	}
}
