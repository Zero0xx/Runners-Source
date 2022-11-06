using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000994 RID: 2452
	public class CannonLaunchParameter : StateEnteringParameter
	{
		// Token: 0x0600406A RID: 16490 RVA: 0x0014DEB4 File Offset: 0x0014C0B4
		public override void Reset()
		{
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x0014DEB8 File Offset: 0x0014C0B8
		public void Set(Vector3 pos, Quaternion rot, float firstSpeed, float height, float outOfcontrol)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_firstSpeed = firstSpeed;
			this.m_height = height;
			this.m_outOfControlTime = outOfcontrol;
		}

		// Token: 0x0400371B RID: 14107
		public Vector3 m_position = Vector3.zero;

		// Token: 0x0400371C RID: 14108
		public Quaternion m_rotation = Quaternion.identity;

		// Token: 0x0400371D RID: 14109
		public float m_firstSpeed;

		// Token: 0x0400371E RID: 14110
		public float m_height;

		// Token: 0x0400371F RID: 14111
		public float m_outOfControlTime;
	}
}
