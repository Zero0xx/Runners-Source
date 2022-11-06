using System;

namespace Message
{
	// Token: 0x02000615 RID: 1557
	public class MsgGetLeaderboardEntriesSucceed : MessageBase
	{
		// Token: 0x06002B8F RID: 11151 RVA: 0x0010A720 File Offset: 0x00108920
		public MsgGetLeaderboardEntriesSucceed() : base(61479)
		{
		}

		// Token: 0x0400286F RID: 10351
		public ServerLeaderboardEntries m_leaderboardEntries;
	}
}
