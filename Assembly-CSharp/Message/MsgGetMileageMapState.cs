using System;

namespace Message
{
	// Token: 0x02000594 RID: 1428
	public class MsgGetMileageMapState : MessageBase
	{
		// Token: 0x06002AF8 RID: 11000 RVA: 0x001098B4 File Offset: 0x00107AB4
		public MsgGetMileageMapState() : base(12327)
		{
		}

		// Token: 0x04002765 RID: 10085
		public MileageMapState m_mileageMapState;

		// Token: 0x04002766 RID: 10086
		public uint m_debugLevel;

		// Token: 0x04002767 RID: 10087
		public bool m_succeed;
	}
}
