using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005D0 RID: 1488
	public class MsgOnCannonImpulse : MessageBase
	{
		// Token: 0x06002B43 RID: 11075 RVA: 0x0010A00C File Offset: 0x0010820C
		public MsgOnCannonImpulse(Vector3 pos, Quaternion rot, float firstSpeed, float outOfControl, bool roulette) : base(24578)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_firstSpeed = firstSpeed;
			this.m_outOfControl = outOfControl;
			this.m_roulette = roulette;
			this.m_succeed = false;
		}

		// Token: 0x040027F3 RID: 10227
		public Vector3 m_position;

		// Token: 0x040027F4 RID: 10228
		public Quaternion m_rotation;

		// Token: 0x040027F5 RID: 10229
		public float m_firstSpeed;

		// Token: 0x040027F6 RID: 10230
		public float m_outOfControl;

		// Token: 0x040027F7 RID: 10231
		public bool m_roulette;

		// Token: 0x040027F8 RID: 10232
		public bool m_succeed;
	}
}
