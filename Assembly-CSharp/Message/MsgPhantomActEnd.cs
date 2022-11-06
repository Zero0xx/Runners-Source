using System;

namespace Message
{
	// Token: 0x02000646 RID: 1606
	public class MsgPhantomActEnd : MessageBase
	{
		// Token: 0x06002BC0 RID: 11200 RVA: 0x0010AAA0 File Offset: 0x00108CA0
		public MsgPhantomActEnd(PhantomType type) : base(12351)
		{
			this.m_type = type;
		}

		// Token: 0x040028A1 RID: 10401
		public readonly PhantomType m_type;
	}
}
