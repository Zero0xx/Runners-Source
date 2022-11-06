using System;

namespace Message
{
	// Token: 0x02000603 RID: 1539
	public class MsgGetWheelOptionsSucceed : MessageBase
	{
		// Token: 0x06002B7D RID: 11133 RVA: 0x0010A560 File Offset: 0x00108760
		public MsgGetWheelOptionsSucceed() : base(61461)
		{
		}

		// Token: 0x0400284F RID: 10319
		public ServerWheelOptions m_wheelOptions;
	}
}
