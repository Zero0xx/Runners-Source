using System;

namespace Message
{
	// Token: 0x0200063B RID: 1595
	public class MsgUpSpeedLevel : MessageBase
	{
		// Token: 0x06002BB5 RID: 11189 RVA: 0x0010A984 File Offset: 0x00108B84
		public MsgUpSpeedLevel(PlayerSpeed level) : base(12303)
		{
			this.m_level = level;
		}

		// Token: 0x04002893 RID: 10387
		public PlayerSpeed m_level;
	}
}
