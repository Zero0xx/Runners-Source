using System;

namespace Message
{
	// Token: 0x020005D5 RID: 1493
	public class MsgInvalidateItem : MessageBase
	{
		// Token: 0x06002B4B RID: 11083 RVA: 0x0010A130 File Offset: 0x00108330
		public MsgInvalidateItem(ItemType itemType) : base(12289)
		{
			this.m_itemType = itemType;
		}

		// Token: 0x04002807 RID: 10247
		public readonly ItemType m_itemType;
	}
}
