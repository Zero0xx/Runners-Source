using System;

namespace Message
{
	// Token: 0x02000658 RID: 1624
	public class MsgTutorialGetRingNum : MessageBase
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x0010AC38 File Offset: 0x00108E38
		public MsgTutorialGetRingNum() : base(12346)
		{
		}

		// Token: 0x040028AF RID: 10415
		public int m_ring;
	}
}
