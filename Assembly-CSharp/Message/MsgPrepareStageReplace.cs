using System;

namespace Message
{
	// Token: 0x0200063D RID: 1597
	public class MsgPrepareStageReplace : MessageBase
	{
		// Token: 0x06002BB7 RID: 11191 RVA: 0x0010A9D0 File Offset: 0x00108BD0
		public MsgPrepareStageReplace(PlayerSpeed speedLevel, string stagename) : base(12310)
		{
			this.m_speedLevel = speedLevel;
			this.m_stageName = stagename;
		}

		// Token: 0x04002898 RID: 10392
		public PlayerSpeed m_speedLevel;

		// Token: 0x04002899 RID: 10393
		public string m_stageName;
	}
}
