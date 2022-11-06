using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005D2 RID: 1490
	public class MsgHitDamage : MessageBase
	{
		// Token: 0x06002B46 RID: 11078 RVA: 0x0010A09C File Offset: 0x0010829C
		public MsgHitDamage(GameObject sender, AttackPower attack) : base(16384)
		{
			this.m_sender = sender;
			this.m_attackPower = (int)attack;
		}

		// Token: 0x040027FD RID: 10237
		public readonly GameObject m_sender;

		// Token: 0x040027FE RID: 10238
		public int m_attackPower;

		// Token: 0x040027FF RID: 10239
		public uint m_attackAttribute;
	}
}
