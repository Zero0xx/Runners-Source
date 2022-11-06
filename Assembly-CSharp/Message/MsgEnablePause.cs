using System;

namespace Message
{
	// Token: 0x020005AC RID: 1452
	public class MsgEnablePause : MessageBase
	{
		// Token: 0x06002B1B RID: 11035 RVA: 0x00109C4C File Offset: 0x00107E4C
		public MsgEnablePause(bool value) : base(4099)
		{
			this.m_enable = value;
		}

		// Token: 0x04002795 RID: 10133
		public bool m_enable;
	}
}
