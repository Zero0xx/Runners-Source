using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005D7 RID: 1495
	public class MsgOnJumpBoardHit : MessageBase
	{
		// Token: 0x06002B4D RID: 11085 RVA: 0x0010A158 File Offset: 0x00108358
		public MsgOnJumpBoardHit(Vector3 pos, Quaternion rot) : base(24584)
		{
			this.m_position = pos;
			this.m_rotation = rot;
		}

		// Token: 0x04002809 RID: 10249
		public Vector3 m_position;

		// Token: 0x0400280A RID: 10250
		public Quaternion m_rotation;
	}
}
