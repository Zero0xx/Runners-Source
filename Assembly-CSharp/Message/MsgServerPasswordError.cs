using System;

namespace Message
{
	// Token: 0x02000624 RID: 1572
	public class MsgServerPasswordError : MessageBase
	{
		// Token: 0x06002B9E RID: 11166 RVA: 0x0010A810 File Offset: 0x00108A10
		public MsgServerPasswordError() : base(61516)
		{
		}

		// Token: 0x0400287E RID: 10366
		public string m_key;

		// Token: 0x0400287F RID: 10367
		public string m_userId;

		// Token: 0x04002880 RID: 10368
		public string m_password;
	}
}
