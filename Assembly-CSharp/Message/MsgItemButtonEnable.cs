using System;

namespace Message
{
	// Token: 0x020005E4 RID: 1508
	public class MsgItemButtonEnable : MessageBase
	{
		// Token: 0x06002B5E RID: 11102 RVA: 0x0010A35C File Offset: 0x0010855C
		public MsgItemButtonEnable(bool enable) : base(12298)
		{
			this.m_enable = enable;
		}

		// Token: 0x04002827 RID: 10279
		public bool m_enable;
	}
}
