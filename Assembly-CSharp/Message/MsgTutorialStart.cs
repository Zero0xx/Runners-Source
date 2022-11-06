using System;
using UnityEngine;

namespace Message
{
	// Token: 0x02000647 RID: 1607
	public class MsgTutorialStart : MessageBase
	{
		// Token: 0x06002BC1 RID: 11201 RVA: 0x0010AAB4 File Offset: 0x00108CB4
		public MsgTutorialStart(Vector3 pos) : base(12330)
		{
			this.m_pos = pos;
		}

		// Token: 0x040028A2 RID: 10402
		public readonly Vector3 m_pos;
	}
}
