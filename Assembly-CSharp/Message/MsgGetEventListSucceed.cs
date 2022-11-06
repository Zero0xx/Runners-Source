using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x0200062B RID: 1579
	public class MsgGetEventListSucceed : MessageBase
	{
		// Token: 0x06002BA5 RID: 11173 RVA: 0x0010A880 File Offset: 0x00108A80
		public MsgGetEventListSucceed() : base(61502)
		{
		}

		// Token: 0x04002883 RID: 10371
		public List<ServerEventEntry> m_eventList;
	}
}
