using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x0200060B RID: 1547
	public class MsgGetRedStarExchangeListSucceed : MessageBase
	{
		// Token: 0x06002B85 RID: 11141 RVA: 0x0010A5E0 File Offset: 0x001087E0
		public MsgGetRedStarExchangeListSucceed() : base(61469)
		{
		}

		// Token: 0x0400285C RID: 10332
		public List<ServerRedStarItemState> m_itemList;

		// Token: 0x0400285D RID: 10333
		public int m_totalItems;
	}
}
