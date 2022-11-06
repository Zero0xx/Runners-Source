using System;

namespace Message
{
	// Token: 0x020005EB RID: 1515
	public class MsgLoginSucceed : MessageBase
	{
		// Token: 0x06002B65 RID: 11109 RVA: 0x0010A3D8 File Offset: 0x001085D8
		public MsgLoginSucceed() : base(61440)
		{
		}

		// Token: 0x0400282B RID: 10283
		public string m_userId;

		// Token: 0x0400282C RID: 10284
		public string m_password;

		// Token: 0x0400282D RID: 10285
		public string m_countryCode = string.Empty;
	}
}
