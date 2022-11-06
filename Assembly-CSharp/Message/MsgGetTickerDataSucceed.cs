using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x0200061E RID: 1566
	public class MsgGetTickerDataSucceed : MessageBase
	{
		// Token: 0x06002B98 RID: 11160 RVA: 0x0010A7B0 File Offset: 0x001089B0
		public MsgGetTickerDataSucceed() : base(61489)
		{
		}

		// Token: 0x0400287A RID: 10362
		public List<ServerTickerData> m_tickerData;
	}
}
