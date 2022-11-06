using System;
using UnityEngine;

namespace Message
{
	// Token: 0x0200063F RID: 1599
	public class MsgSetStageOnMapBoss : MessageBase
	{
		// Token: 0x06002BB9 RID: 11193 RVA: 0x0010A9FC File Offset: 0x00108BFC
		public MsgSetStageOnMapBoss(Vector3 pos, Quaternion rot, string stagename, BossType bossType) : base(12312)
		{
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_stageName = stagename;
			this.m_bossType = bossType;
		}

		// Token: 0x0400289A RID: 10394
		public Vector3 m_position;

		// Token: 0x0400289B RID: 10395
		public Quaternion m_rotation;

		// Token: 0x0400289C RID: 10396
		public string m_stageName;

		// Token: 0x0400289D RID: 10397
		public BossType m_bossType;
	}
}
