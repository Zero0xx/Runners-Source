using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x020005FC RID: 1532
	public class MsgGetCostListSucceed : MessageBase
	{
		// Token: 0x06002B76 RID: 11126 RVA: 0x0010A4F0 File Offset: 0x001086F0
		public MsgGetCostListSucceed() : base(61454)
		{
		}

		// Token: 0x04002848 RID: 10312
		public List<ServerConsumedCostData> m_costList;
	}
}
