using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000993 RID: 2451
	public class CannonReachParameter : StateEnteringParameter
	{
		// Token: 0x06004067 RID: 16487 RVA: 0x0014DE70 File Offset: 0x0014C070
		public override void Reset()
		{
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x0014DE74 File Offset: 0x0014C074
		public void Set(Vector3 pos, Quaternion rot, float height, GameObject catchedObject)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_height = height;
			this.m_catchedObject = catchedObject;
		}

		// Token: 0x04003717 RID: 14103
		public Vector3 m_position = Vector3.zero;

		// Token: 0x04003718 RID: 14104
		public Quaternion m_rotation = Quaternion.identity;

		// Token: 0x04003719 RID: 14105
		public float m_height;

		// Token: 0x0400371A RID: 14106
		public GameObject m_catchedObject;
	}
}
