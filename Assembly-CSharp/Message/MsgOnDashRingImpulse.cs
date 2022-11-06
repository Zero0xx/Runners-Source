using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005D3 RID: 1491
	public class MsgOnDashRingImpulse : MessageBase
	{
		// Token: 0x06002B47 RID: 11079 RVA: 0x0010A0B8 File Offset: 0x001082B8
		public MsgOnDashRingImpulse(Vector3 pos, Quaternion rot, float firstSpeed, float outOfControl) : base(24577)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_firstSpeed = firstSpeed;
			this.m_outOfControl = outOfControl;
			this.m_succeed = false;
		}

		// Token: 0x04002800 RID: 10240
		public Vector3 m_position;

		// Token: 0x04002801 RID: 10241
		public Quaternion m_rotation;

		// Token: 0x04002802 RID: 10242
		public float m_firstSpeed;

		// Token: 0x04002803 RID: 10243
		public float m_outOfControl;

		// Token: 0x04002804 RID: 10244
		public bool m_succeed;
	}
}
