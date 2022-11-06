using System;

namespace Message
{
	// Token: 0x02000592 RID: 1426
	public class MsgBossDistanceEnd : MessageBase
	{
		// Token: 0x06002AF6 RID: 10998 RVA: 0x00109884 File Offset: 0x00107A84
		public MsgBossDistanceEnd(bool end) : base(12324)
		{
			this.m_end = end;
		}

		// Token: 0x04002762 RID: 10082
		public bool m_end;
	}
}
