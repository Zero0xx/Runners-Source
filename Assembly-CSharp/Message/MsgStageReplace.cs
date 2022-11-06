using System;
using UnityEngine;

namespace Message
{
	// Token: 0x0200063C RID: 1596
	public class MsgStageReplace : MessageBase
	{
		// Token: 0x06002BB6 RID: 11190 RVA: 0x0010A998 File Offset: 0x00108B98
		public MsgStageReplace(PlayerSpeed speedLevel, Vector3 pos, Quaternion rot, string stagename) : base(12309)
		{
			this.m_speedLevel = speedLevel;
			this.m_position = pos;
			this.m_rotation = rot;
			this.m_stageName = stagename;
		}

		// Token: 0x04002894 RID: 10388
		public PlayerSpeed m_speedLevel;

		// Token: 0x04002895 RID: 10389
		public Vector3 m_position;

		// Token: 0x04002896 RID: 10390
		public Quaternion m_rotation;

		// Token: 0x04002897 RID: 10391
		public string m_stageName;
	}
}
