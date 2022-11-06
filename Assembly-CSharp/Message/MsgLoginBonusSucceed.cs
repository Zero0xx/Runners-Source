using System;

namespace Message
{
	// Token: 0x020005F0 RID: 1520
	public class MsgLoginBonusSucceed : MessageBase
	{
		// Token: 0x06002B6A RID: 11114 RVA: 0x0010A430 File Offset: 0x00108630
		public MsgLoginBonusSucceed() : base(61445)
		{
		}

		// Token: 0x04002837 RID: 10295
		public ServerLoginBonusData m_loginBonusData;
	}
}
