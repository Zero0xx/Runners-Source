using System;

namespace Message
{
	// Token: 0x020005AB RID: 1451
	public class MsgDisableInput : MessageBase
	{
		// Token: 0x06002B1A RID: 11034 RVA: 0x00109C38 File Offset: 0x00107E38
		public MsgDisableInput(bool value) : base(12319)
		{
			this.m_disable = value;
		}

		// Token: 0x04002794 RID: 10132
		public bool m_disable;
	}
}
