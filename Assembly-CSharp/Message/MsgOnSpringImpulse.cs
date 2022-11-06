using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005DA RID: 1498
	public class MsgOnSpringImpulse : MessageBase
	{
		// Token: 0x06002B51 RID: 11089 RVA: 0x0010A220 File Offset: 0x00108420
		public MsgOnSpringImpulse(Vector3 pos, Quaternion rot, float firstSpeed, float outOfControl) : base(24576)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_firstSpeed = firstSpeed;
			this.m_outOfControl = outOfControl;
			this.m_succeed = false;
		}

		// Token: 0x04002818 RID: 10264
		public Vector3 m_position;

		// Token: 0x04002819 RID: 10265
		public Quaternion m_rotation;

		// Token: 0x0400281A RID: 10266
		public float m_firstSpeed;

		// Token: 0x0400281B RID: 10267
		public float m_outOfControl;

		// Token: 0x0400281C RID: 10268
		public bool m_succeed;
	}
}
