using System;

namespace Message
{
	// Token: 0x02000638 RID: 1592
	public class MsgSocialMyProfileResponse : MessageBase
	{
		// Token: 0x06002BB2 RID: 11186 RVA: 0x0010A954 File Offset: 0x00108B54
		public MsgSocialMyProfileResponse() : base(63489)
		{
		}

		// Token: 0x0400288D RID: 10381
		public SocialResult m_result;

		// Token: 0x0400288E RID: 10382
		public SocialUserData m_profile;
	}
}
