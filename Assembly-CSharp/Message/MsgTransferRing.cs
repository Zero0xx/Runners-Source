using System;

namespace Message
{
	// Token: 0x020005DB RID: 1499
	public class MsgTransferRing
	{
		// Token: 0x06002B52 RID: 11090 RVA: 0x0010A254 File Offset: 0x00108454
		public MsgTransferRing()
		{
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x0010A25C File Offset: 0x0010845C
		public MsgTransferRing(int ring, bool isContinue)
		{
			this.m_ring = ring;
			this.m_isContinue = isContinue;
		}

		// Token: 0x0400281D RID: 10269
		public int m_ring;

		// Token: 0x0400281E RID: 10270
		public bool m_isContinue;
	}
}
