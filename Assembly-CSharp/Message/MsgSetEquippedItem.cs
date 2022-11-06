using System;

namespace Message
{
	// Token: 0x020005E2 RID: 1506
	public class MsgSetEquippedItem : MessageBase
	{
		// Token: 0x06002B5C RID: 11100 RVA: 0x0010A314 File Offset: 0x00108514
		public MsgSetEquippedItem(ItemType[] itemType) : base(12296)
		{
			this.m_itemType = new ItemType[itemType.Length];
			itemType.CopyTo(this.m_itemType, 0);
		}

		// Token: 0x04002824 RID: 10276
		public readonly ItemType[] m_itemType;

		// Token: 0x04002825 RID: 10277
		public bool m_enabled;
	}
}
