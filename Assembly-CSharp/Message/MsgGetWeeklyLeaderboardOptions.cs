using System;

namespace Message
{
	// Token: 0x02000629 RID: 1577
	public class MsgGetWeeklyLeaderboardOptions : MessageBase
	{
		// Token: 0x06002BA3 RID: 11171 RVA: 0x0010A860 File Offset: 0x00108A60
		public MsgGetWeeklyLeaderboardOptions() : base(61500)
		{
		}

		// Token: 0x04002881 RID: 10369
		public ServerWeeklyLeaderboardOptions m_weeklyLeaderboardOptions;
	}
}
