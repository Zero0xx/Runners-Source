using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x02000622 RID: 1570
	public class MsgGetFriendUserIdListSucceed : MessageBase
	{
		// Token: 0x06002B9C RID: 11164 RVA: 0x0010A7F0 File Offset: 0x001089F0
		public MsgGetFriendUserIdListSucceed() : base(61493)
		{
		}

		// Token: 0x0400287D RID: 10365
		public List<ServerUserTransformData> m_transformDataList;
	}
}
