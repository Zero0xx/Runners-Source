using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x0200062A RID: 1578
	public class MsgGetLeagueOperatorDataSucceed : MessageBase
	{
		// Token: 0x06002BA4 RID: 11172 RVA: 0x0010A870 File Offset: 0x00108A70
		public MsgGetLeagueOperatorDataSucceed() : base(61499)
		{
		}

		// Token: 0x04002882 RID: 10370
		public List<ServerLeagueOperatorData> m_leagueOperatorData;
	}
}
