using System;

namespace Message
{
	// Token: 0x020005A7 RID: 1447
	public class MsgPrepareContinue : MessageBase
	{
		// Token: 0x06002B15 RID: 11029 RVA: 0x00109BD4 File Offset: 0x00107DD4
		public MsgPrepareContinue(bool bossStage, bool timeUp) : base(12353)
		{
			this.m_bossStage = bossStage;
			this.m_timeUp = timeUp;
		}

		// Token: 0x04002790 RID: 10128
		public bool m_bossStage;

		// Token: 0x04002791 RID: 10129
		public bool m_timeUp;
	}
}
