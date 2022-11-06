using System;

namespace Message
{
	// Token: 0x0200061B RID: 1563
	public class MsgGetDailyMissionDataSucceed : MessageBase
	{
		// Token: 0x06002B95 RID: 11157 RVA: 0x0010A780 File Offset: 0x00108980
		public MsgGetDailyMissionDataSucceed() : base(61486)
		{
		}

		// Token: 0x04002877 RID: 10359
		public ServerDailyChallengeState m_dailyChallengeState;
	}
}
