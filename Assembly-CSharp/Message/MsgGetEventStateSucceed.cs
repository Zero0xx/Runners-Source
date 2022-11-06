using System;

namespace Message
{
	// Token: 0x0200062D RID: 1581
	public class MsgGetEventStateSucceed : MessageBase
	{
		// Token: 0x06002BA7 RID: 11175 RVA: 0x0010A8A0 File Offset: 0x00108AA0
		public MsgGetEventStateSucceed() : base(61504)
		{
		}

		// Token: 0x04002885 RID: 10373
		public ServerEventState m_eventState;
	}
}
