using System;
using UnityEngine;

namespace Message
{
	// Token: 0x02000659 RID: 1625
	public class MsgTutorialResetForRetry : MessageBase
	{
		// Token: 0x06002BD3 RID: 11219 RVA: 0x0010AC48 File Offset: 0x00108E48
		public MsgTutorialResetForRetry(Vector3 position, Quaternion rotation, bool blink) : base(12347)
		{
			this.m_position = position;
			this.m_rotation = rotation;
			this.m_blink = blink;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x0010AC78 File Offset: 0x00108E78
		public MsgTutorialResetForRetry(int ring, bool blink) : base(12347)
		{
			this.m_ring = ring;
			this.m_blink = blink;
		}

		// Token: 0x040028B0 RID: 10416
		public Vector3 m_position;

		// Token: 0x040028B1 RID: 10417
		public Quaternion m_rotation;

		// Token: 0x040028B2 RID: 10418
		public int m_ring;

		// Token: 0x040028B3 RID: 10419
		public bool m_blink;
	}
}
