using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x0200060C RID: 1548
	public class MsgGetRingExchangeListSucceed : MessageBase
	{
		// Token: 0x06002B86 RID: 11142 RVA: 0x0010A5F0 File Offset: 0x001087F0
		public MsgGetRingExchangeListSucceed() : base(61470)
		{
		}

		// Token: 0x0400285E RID: 10334
		public List<ServerRingExchangeList> m_exchangeList;
	}
}
