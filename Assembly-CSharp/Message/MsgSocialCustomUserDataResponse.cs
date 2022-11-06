using System;

namespace Message
{
	// Token: 0x0200063A RID: 1594
	public class MsgSocialCustomUserDataResponse : MessageBase
	{
		// Token: 0x06002BB4 RID: 11188 RVA: 0x0010A974 File Offset: 0x00108B74
		public MsgSocialCustomUserDataResponse() : base(63491)
		{
		}

		// Token: 0x04002891 RID: 10385
		public bool m_isCreated;

		// Token: 0x04002892 RID: 10386
		public SocialUserData m_userData;
	}
}
