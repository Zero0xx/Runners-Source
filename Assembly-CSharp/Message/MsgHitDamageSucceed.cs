using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005D1 RID: 1489
	public class MsgHitDamageSucceed : MessageBase
	{
		// Token: 0x06002B44 RID: 11076 RVA: 0x0010A048 File Offset: 0x00108248
		public MsgHitDamageSucceed(GameObject sender, int score) : base(16385)
		{
			this.m_sender = sender;
			this.m_score = score;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0010A064 File Offset: 0x00108264
		public MsgHitDamageSucceed(GameObject sender, int score, Vector3 position, Quaternion rotation) : base(16385)
		{
			this.m_sender = sender;
			this.m_score = score;
			this.m_position = position;
			this.m_rotation = rotation;
		}

		// Token: 0x040027F9 RID: 10233
		public readonly GameObject m_sender;

		// Token: 0x040027FA RID: 10234
		public readonly int m_score;

		// Token: 0x040027FB RID: 10235
		public readonly Vector3 m_position;

		// Token: 0x040027FC RID: 10236
		public readonly Quaternion m_rotation;
	}
}
