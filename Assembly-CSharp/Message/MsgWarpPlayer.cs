using System;
using UnityEngine;

namespace Message
{
	// Token: 0x0200065E RID: 1630
	public class MsgWarpPlayer : MessageBase
	{
		// Token: 0x06002BD9 RID: 11225 RVA: 0x0010ACDC File Offset: 0x00108EDC
		public MsgWarpPlayer(Vector3 pos, Quaternion rot, bool hold) : base(20485)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_hold = hold;
		}

		// Token: 0x040028BA RID: 10426
		public Vector3 m_position;

		// Token: 0x040028BB RID: 10427
		public Quaternion m_rotation;

		// Token: 0x040028BC RID: 10428
		public bool m_hold;
	}
}
