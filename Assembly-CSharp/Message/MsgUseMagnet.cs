using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005BD RID: 1469
	public class MsgUseMagnet : MessageBase
	{
		// Token: 0x06002B2C RID: 11052 RVA: 0x00109DE4 File Offset: 0x00107FE4
		public MsgUseMagnet(GameObject obj, float time) : base(12360)
		{
			this.m_obj = obj;
			this.m_target = null;
			this.m_time = time;
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x00109E14 File Offset: 0x00108014
		public MsgUseMagnet(GameObject obj, GameObject target, float time) : base(12360)
		{
			this.m_obj = obj;
			this.m_target = target;
			this.m_time = time;
		}

		// Token: 0x040027B4 RID: 10164
		public readonly GameObject m_obj;

		// Token: 0x040027B5 RID: 10165
		public readonly GameObject m_target;

		// Token: 0x040027B6 RID: 10166
		public readonly float m_time;
	}
}
