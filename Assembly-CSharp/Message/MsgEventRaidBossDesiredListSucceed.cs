using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x02000635 RID: 1589
	public class MsgEventRaidBossDesiredListSucceed : MessageBase
	{
		// Token: 0x06002BAF RID: 11183 RVA: 0x0010A920 File Offset: 0x00108B20
		public MsgEventRaidBossDesiredListSucceed() : base(61511)
		{
		}

		// Token: 0x0400288A RID: 10378
		public List<ServerEventRaidBossDesiredState> m_desiredList;
	}
}
