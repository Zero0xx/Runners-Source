using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005C8 RID: 1480
	public class MsgOnAbidePlayer : MessageBase
	{
		// Token: 0x06002B3B RID: 11067 RVA: 0x00109F68 File Offset: 0x00108168
		public MsgOnAbidePlayer(Vector3 pos, Quaternion rot, float height, GameObject obj) : base(24580)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_height = height;
			this.m_abideObject = obj;
			this.m_succeed = false;
		}

		// Token: 0x040027EA RID: 10218
		public Vector3 m_position;

		// Token: 0x040027EB RID: 10219
		public Quaternion m_rotation;

		// Token: 0x040027EC RID: 10220
		public float m_height;

		// Token: 0x040027ED RID: 10221
		public GameObject m_abideObject;

		// Token: 0x040027EE RID: 10222
		public bool m_succeed;
	}
}
