using System;

namespace Message
{
	// Token: 0x020005B9 RID: 1465
	public class MsgInvincible : MessageBase
	{
		// Token: 0x06002B28 RID: 11048 RVA: 0x00109DA4 File Offset: 0x00107FA4
		public MsgInvincible(MsgInvincible.Mode mode) : base(12329)
		{
			this.m_mode = mode;
		}

		// Token: 0x040027AF RID: 10159
		public MsgInvincible.Mode m_mode;

		// Token: 0x020005BA RID: 1466
		public enum Mode
		{
			// Token: 0x040027B1 RID: 10161
			Start,
			// Token: 0x040027B2 RID: 10162
			End
		}
	}
}
