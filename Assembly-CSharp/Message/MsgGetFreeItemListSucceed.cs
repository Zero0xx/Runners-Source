using System;

namespace Message
{
	// Token: 0x020005FE RID: 1534
	public class MsgGetFreeItemListSucceed : MessageBase
	{
		// Token: 0x06002B78 RID: 11128 RVA: 0x0010A510 File Offset: 0x00108710
		public MsgGetFreeItemListSucceed() : base(61456)
		{
		}

		// Token: 0x04002849 RID: 10313
		public ServerFreeItemState m_freeItemState;
	}
}
