using System;

namespace Message
{
	// Token: 0x020005D6 RID: 1494
	public class MsgTerminateItem : MessageBase
	{
		// Token: 0x06002B4C RID: 11084 RVA: 0x0010A144 File Offset: 0x00108344
		public MsgTerminateItem(ItemType itemType) : base(12290)
		{
			this.m_itemType = itemType;
		}

		// Token: 0x04002808 RID: 10248
		public readonly ItemType m_itemType;
	}
}
