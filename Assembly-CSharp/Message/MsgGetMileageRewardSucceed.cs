using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x020005FA RID: 1530
	public class MsgGetMileageRewardSucceed : MessageBase
	{
		// Token: 0x06002B74 RID: 11124 RVA: 0x0010A4D0 File Offset: 0x001086D0
		public MsgGetMileageRewardSucceed() : base(61501)
		{
		}

		// Token: 0x04002846 RID: 10310
		public List<ServerMileageReward> m_mileageRewardList;
	}
}
