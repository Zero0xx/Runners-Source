using System;

namespace Message
{
	// Token: 0x020005CB RID: 1483
	public class MsgAddCount
	{
		// Token: 0x06002B3E RID: 11070 RVA: 0x00109FBC File Offset: 0x001081BC
		public MsgAddCount(int addCount)
		{
			this.m_addCount = addCount;
		}

		// Token: 0x040027EF RID: 10223
		public readonly int m_addCount;
	}
}
