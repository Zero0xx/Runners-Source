using System;

namespace Message
{
	// Token: 0x02000636 RID: 1590
	public class MsgServerConnctFailed : MessageBase
	{
		// Token: 0x06002BB0 RID: 11184 RVA: 0x0010A930 File Offset: 0x00108B30
		public MsgServerConnctFailed(ServerInterface.StatusCode status) : base(61517)
		{
			this.m_status = status;
		}

		// Token: 0x0400288B RID: 10379
		public ServerInterface.StatusCode m_status;
	}
}
