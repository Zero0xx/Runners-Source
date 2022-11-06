using System;

namespace Message
{
	// Token: 0x020005DC RID: 1500
	public class MsgPlayerTransferRing
	{
		// Token: 0x06002B54 RID: 11092 RVA: 0x0010A274 File Offset: 0x00108474
		public MsgPlayerTransferRing(bool hud)
		{
			this.m_hud = hud;
		}

		// Token: 0x0400281F RID: 10271
		public bool m_hud;
	}
}
