using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x02000616 RID: 1558
	public class MsgGetFriendChaoStateSucceed : MessageBase
	{
		// Token: 0x06002B90 RID: 11152 RVA: 0x0010A730 File Offset: 0x00108930
		public MsgGetFriendChaoStateSucceed() : base(61480)
		{
		}

		// Token: 0x04002870 RID: 10352
		public List<ServerChaoRentalState> m_chaoRentalStates;
	}
}
