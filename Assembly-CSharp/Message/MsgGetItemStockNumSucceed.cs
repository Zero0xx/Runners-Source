using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x02000600 RID: 1536
	public class MsgGetItemStockNumSucceed : MessageBase
	{
		// Token: 0x06002B7A RID: 11130 RVA: 0x0010A530 File Offset: 0x00108730
		public MsgGetItemStockNumSucceed() : base(61458)
		{
		}

		// Token: 0x0400284A RID: 10314
		public List<ServerItemState> m_itemStockList;
	}
}
