using System;

namespace Message
{
	// Token: 0x02000589 RID: 1417
	public class MsgAskEquipItemUsed : MessageBase
	{
		// Token: 0x06002AED RID: 10989 RVA: 0x001097D4 File Offset: 0x001079D4
		public MsgAskEquipItemUsed(ItemType itemType) : base(12321)
		{
			this.m_itemType = itemType;
		}

		// Token: 0x04002753 RID: 10067
		public bool m_ok;

		// Token: 0x04002754 RID: 10068
		public ItemType m_itemType;
	}
}
