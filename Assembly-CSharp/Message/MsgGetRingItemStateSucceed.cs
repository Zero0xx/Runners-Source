using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x020005FB RID: 1531
	public class MsgGetRingItemStateSucceed : MessageBase
	{
		// Token: 0x06002B75 RID: 11125 RVA: 0x0010A4E0 File Offset: 0x001086E0
		public MsgGetRingItemStateSucceed() : base(61453)
		{
		}

		// Token: 0x04002847 RID: 10311
		public List<ServerRingItemState> m_RingStateList;
	}
}
