using System;
using UnityEngine;

namespace Message
{
	// Token: 0x0200058F RID: 1423
	public class MsgBossInfo : MessageBase
	{
		// Token: 0x06002AF3 RID: 10995 RVA: 0x00109854 File Offset: 0x00107A54
		public MsgBossInfo() : base(12322)
		{
		}

		// Token: 0x0400275A RID: 10074
		public GameObject m_boss;

		// Token: 0x0400275B RID: 10075
		public Vector3 m_position;

		// Token: 0x0400275C RID: 10076
		public Quaternion m_rotation;

		// Token: 0x0400275D RID: 10077
		public bool m_succeed;
	}
}
