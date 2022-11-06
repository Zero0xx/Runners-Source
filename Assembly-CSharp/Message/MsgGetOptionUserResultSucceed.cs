using System;

namespace Message
{
	// Token: 0x0200061C RID: 1564
	public class MsgGetOptionUserResultSucceed : MessageBase
	{
		// Token: 0x06002B96 RID: 11158 RVA: 0x0010A790 File Offset: 0x00108990
		public MsgGetOptionUserResultSucceed() : base(61487)
		{
		}

		// Token: 0x04002878 RID: 10360
		public ServerOptionUserResult m_serverOptionUserResult;
	}
}
