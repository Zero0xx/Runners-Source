using System;

namespace Message
{
	// Token: 0x02000637 RID: 1591
	public class MsgSocialNormalResponse : MessageBase
	{
		// Token: 0x06002BB1 RID: 11185 RVA: 0x0010A944 File Offset: 0x00108B44
		public MsgSocialNormalResponse() : base(63488)
		{
		}

		// Token: 0x0400288C RID: 10380
		public SocialResult m_result;
	}
}
