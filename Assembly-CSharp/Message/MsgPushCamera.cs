using System;
using UnityEngine;

namespace Message
{
	// Token: 0x02000597 RID: 1431
	public class MsgPushCamera : MessageBase
	{
		// Token: 0x06002AFB RID: 11003 RVA: 0x001098E8 File Offset: 0x00107AE8
		public MsgPushCamera(CameraType type, float interpolateTime, UnityEngine.Object parameter = null) : base(32768)
		{
			this.m_type = type;
			this.m_parameter = parameter;
			this.m_interpolateTime = interpolateTime;
		}

		// Token: 0x04002769 RID: 10089
		public CameraType m_type;

		// Token: 0x0400276A RID: 10090
		public UnityEngine.Object m_parameter;

		// Token: 0x0400276B RID: 10091
		public float m_interpolateTime;
	}
}
