using System;
using UnityEngine;

namespace Player
{
	// Token: 0x020009BC RID: 2492
	public class JumpSpringParameter : StateEnteringParameter
	{
		// Token: 0x06004120 RID: 16672 RVA: 0x00152DF0 File Offset: 0x00150FF0
		public override void Reset()
		{
			this.m_outOfControlTime = 0f;
			this.m_firstSpeed = 0f;
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x00152E08 File Offset: 0x00151008
		public void Set(Vector3 pos, Quaternion rot, float speed, float time)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_outOfControlTime = time;
			this.m_firstSpeed = speed;
		}

		// Token: 0x040037CA RID: 14282
		public Vector3 m_position = Vector3.zero;

		// Token: 0x040037CB RID: 14283
		public Quaternion m_rotation = Quaternion.identity;

		// Token: 0x040037CC RID: 14284
		public float m_outOfControlTime;

		// Token: 0x040037CD RID: 14285
		public float m_firstSpeed;
	}
}
