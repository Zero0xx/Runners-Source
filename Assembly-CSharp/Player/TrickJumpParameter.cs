using System;
using UnityEngine;

namespace Player
{
	// Token: 0x020009C0 RID: 2496
	public class TrickJumpParameter : StateEnteringParameter
	{
		// Token: 0x06004130 RID: 16688 RVA: 0x001536D0 File Offset: 0x001518D0
		public override void Reset()
		{
			this.m_succeed = false;
			this.m_outOfControlTime = 0f;
			this.m_firstSpeed = 0f;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x001536F0 File Offset: 0x001518F0
		public void Set(Vector3 pos, Quaternion rot, float speed, float time, Quaternion succeedRot, float succeedSpeed, float succeedTime, bool succeed)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_outOfControlTime = time;
			this.m_firstSpeed = speed;
			this.m_succeedRotation = succeedRot;
			this.m_succeedOutOfcontrol = succeedTime;
			this.m_succeedFirstSpeed = succeedSpeed;
			this.m_succeed = succeed;
		}

		// Token: 0x040037E1 RID: 14305
		public bool m_succeed = true;

		// Token: 0x040037E2 RID: 14306
		public Vector3 m_position = Vector3.zero;

		// Token: 0x040037E3 RID: 14307
		public Quaternion m_rotation = Quaternion.identity;

		// Token: 0x040037E4 RID: 14308
		public float m_outOfControlTime;

		// Token: 0x040037E5 RID: 14309
		public float m_firstSpeed;

		// Token: 0x040037E6 RID: 14310
		public Quaternion m_succeedRotation = Quaternion.identity;

		// Token: 0x040037E7 RID: 14311
		public float m_succeedOutOfcontrol;

		// Token: 0x040037E8 RID: 14312
		public float m_succeedFirstSpeed;
	}
}
