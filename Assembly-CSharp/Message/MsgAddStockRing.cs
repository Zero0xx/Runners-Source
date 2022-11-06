using System;

namespace Message
{
	// Token: 0x02000588 RID: 1416
	public class MsgAddStockRing : MessageBase
	{
		// Token: 0x06002AEC RID: 10988 RVA: 0x001097C0 File Offset: 0x001079C0
		public MsgAddStockRing(int num) : base(49154)
		{
			this.m_numAddRings = num;
		}

		// Token: 0x04002752 RID: 10066
		public int m_numAddRings;
	}
}
