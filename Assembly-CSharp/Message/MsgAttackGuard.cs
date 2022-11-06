using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005CE RID: 1486
	public class MsgAttackGuard : MessageBase
	{
		// Token: 0x06002B41 RID: 11073 RVA: 0x00109FF0 File Offset: 0x001081F0
		public MsgAttackGuard(GameObject sender) : base(16386)
		{
			this.m_sender = sender;
		}

		// Token: 0x040027F2 RID: 10226
		public readonly GameObject m_sender;
	}
}
