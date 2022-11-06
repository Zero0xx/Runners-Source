using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x020005F4 RID: 1524
	public class MsgPostGameResultsSucceed : MessageBase
	{
		// Token: 0x06002B6E RID: 11118 RVA: 0x0010A470 File Offset: 0x00108670
		public MsgPostGameResultsSucceed() : base(61449)
		{
		}

		// Token: 0x0400283C RID: 10300
		public ServerPlayerState m_playerState;

		// Token: 0x0400283D RID: 10301
		public ServerMileageMapState m_mileageMapState;

		// Token: 0x0400283E RID: 10302
		public List<ServerMileageIncentive> m_mileageIncentive;

		// Token: 0x0400283F RID: 10303
		public List<ServerItemState> m_dailyIncentive;
	}
}
