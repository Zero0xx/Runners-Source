using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x0200062C RID: 1580
	public class MsgGetEventRewardSucceed : MessageBase
	{
		// Token: 0x06002BA6 RID: 11174 RVA: 0x0010A890 File Offset: 0x00108A90
		public MsgGetEventRewardSucceed() : base(61503)
		{
		}

		// Token: 0x04002884 RID: 10372
		public List<ServerEventReward> m_eventRewardList;
	}
}
