using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005D8 RID: 1496
	public class MsgOnJumpBoardJump : MessageBase
	{
		// Token: 0x06002B4E RID: 11086 RVA: 0x0010A174 File Offset: 0x00108374
		public MsgOnJumpBoardJump(Vector3 pos, Quaternion rot1, Quaternion rot2, float spd1, float spd2, float ooc1, float ooc2) : base(24585)
		{
			this.m_position = pos;
			this.m_succeedRotation = rot1;
			this.m_missRotation = rot2;
			this.m_succeedFirstSpeed = spd1;
			this.m_missFirstSpeed = spd2;
			this.m_succeedOutOfcontrol = ooc1;
			this.m_missOutOfcontrol = ooc2;
			this.m_succeed = false;
		}

		// Token: 0x0400280B RID: 10251
		public Vector3 m_position;

		// Token: 0x0400280C RID: 10252
		public Quaternion m_succeedRotation;

		// Token: 0x0400280D RID: 10253
		public Quaternion m_missRotation;

		// Token: 0x0400280E RID: 10254
		public float m_succeedFirstSpeed;

		// Token: 0x0400280F RID: 10255
		public float m_missFirstSpeed;

		// Token: 0x04002810 RID: 10256
		public float m_succeedOutOfcontrol;

		// Token: 0x04002811 RID: 10257
		public float m_missOutOfcontrol;

		// Token: 0x04002812 RID: 10258
		public bool m_succeed;
	}
}
