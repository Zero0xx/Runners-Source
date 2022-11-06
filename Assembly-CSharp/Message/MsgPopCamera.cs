using System;

namespace Message
{
	// Token: 0x02000598 RID: 1432
	public class MsgPopCamera : MessageBase
	{
		// Token: 0x06002AFC RID: 11004 RVA: 0x00109918 File Offset: 0x00107B18
		public MsgPopCamera(CameraType type, float interpolateTime) : base(32769)
		{
			this.m_type = type;
			this.m_interpolateTime = interpolateTime;
		}

		// Token: 0x0400276C RID: 10092
		public CameraType m_type;

		// Token: 0x0400276D RID: 10093
		public float m_interpolateTime;
	}
}
