using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x0200061A RID: 1562
	public class MsgUpdateMesseageSucceed : MessageBase
	{
		// Token: 0x06002B94 RID: 11156 RVA: 0x0010A770 File Offset: 0x00108970
		public MsgUpdateMesseageSucceed() : base(61485)
		{
		}

		// Token: 0x04002874 RID: 10356
		public List<ServerPresentState> m_presentStateList;

		// Token: 0x04002875 RID: 10357
		public List<int> m_notRecvMessageList;

		// Token: 0x04002876 RID: 10358
		public List<int> m_notRecvOperatorMessageList;
	}
}
