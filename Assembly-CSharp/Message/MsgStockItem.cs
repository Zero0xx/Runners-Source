using System;

namespace Message
{
	// Token: 0x020005E1 RID: 1505
	public class MsgStockItem : MessageBase
	{
		// Token: 0x06002B5B RID: 11099 RVA: 0x0010A300 File Offset: 0x00108500
		public MsgStockItem(ItemType itemType) : base(12295)
		{
			this.m_itemType = itemType;
		}

		// Token: 0x04002823 RID: 10275
		public ItemType m_itemType;
	}
}
