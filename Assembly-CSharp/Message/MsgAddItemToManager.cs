using System;

namespace Message
{
	// Token: 0x020005CC RID: 1484
	public class MsgAddItemToManager : MessageBase
	{
		// Token: 0x06002B3F RID: 11071 RVA: 0x00109FCC File Offset: 0x001081CC
		public MsgAddItemToManager(ItemType itemType) : base(12291)
		{
			this.m_itemType = itemType;
		}

		// Token: 0x040027F0 RID: 10224
		public readonly ItemType m_itemType;
	}
}
