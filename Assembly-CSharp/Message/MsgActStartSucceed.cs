using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x020005F2 RID: 1522
	public class MsgActStartSucceed : MessageBase
	{
		// Token: 0x06002B6C RID: 11116 RVA: 0x0010A450 File Offset: 0x00108650
		public MsgActStartSucceed() : base(61447)
		{
		}

		// Token: 0x0400283A RID: 10298
		public ServerPlayerState m_playerState;

		// Token: 0x0400283B RID: 10299
		public List<ServerDistanceFriendEntry> m_friendDistanceList;
	}
}
