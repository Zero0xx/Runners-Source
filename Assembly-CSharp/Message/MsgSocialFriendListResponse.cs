using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x02000639 RID: 1593
	public class MsgSocialFriendListResponse : MessageBase
	{
		// Token: 0x06002BB3 RID: 11187 RVA: 0x0010A964 File Offset: 0x00108B64
		public MsgSocialFriendListResponse() : base(63490)
		{
		}

		// Token: 0x0400288F RID: 10383
		public SocialResult m_result;

		// Token: 0x04002890 RID: 10384
		public List<SocialUserData> m_friends;
	}
}
