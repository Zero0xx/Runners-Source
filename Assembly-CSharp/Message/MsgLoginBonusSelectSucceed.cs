using System;

namespace Message
{
	// Token: 0x020005F1 RID: 1521
	public class MsgLoginBonusSelectSucceed : MessageBase
	{
		// Token: 0x06002B6B RID: 11115 RVA: 0x0010A440 File Offset: 0x00108640
		public MsgLoginBonusSelectSucceed() : base(61446)
		{
		}

		// Token: 0x04002838 RID: 10296
		public ServerLoginBonusReward m_loginBonusReward;

		// Token: 0x04002839 RID: 10297
		public ServerLoginBonusReward m_firstLoginBonusReward;
	}
}
