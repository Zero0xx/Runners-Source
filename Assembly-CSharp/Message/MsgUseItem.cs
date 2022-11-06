using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005DD RID: 1501
	public class MsgUseItem : MessageBase
	{
		// Token: 0x06002B55 RID: 11093 RVA: 0x0010A284 File Offset: 0x00108484
		public MsgUseItem(ItemType itemType) : base(12288)
		{
			this.m_itemType = itemType;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x0010A298 File Offset: 0x00108498
		public MsgUseItem(ItemType itemType, float time) : base(12288)
		{
			this.m_itemType = itemType;
			this.m_time = time;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0010A2B4 File Offset: 0x001084B4
		public MsgUseItem(ItemType itemType, GameObject obj) : base(12288)
		{
			this.m_itemType = itemType;
			this.m_gameObject = obj;
		}

		// Token: 0x04002820 RID: 10272
		public readonly ItemType m_itemType;

		// Token: 0x04002821 RID: 10273
		public float m_time;

		// Token: 0x04002822 RID: 10274
		public GameObject m_gameObject;
	}
}
