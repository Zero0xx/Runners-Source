using System;

namespace Message
{
	// Token: 0x020005E3 RID: 1507
	public class MsgUsedItem : MessageBase
	{
		// Token: 0x06002B5D RID: 11101 RVA: 0x0010A348 File Offset: 0x00108548
		public MsgUsedItem(ItemType itemType) : base(12297)
		{
			this.m_itemType = itemType;
		}

		// Token: 0x04002826 RID: 10278
		public readonly ItemType m_itemType;
	}
}
