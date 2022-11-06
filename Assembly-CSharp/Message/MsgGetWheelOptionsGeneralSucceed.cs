using System;

namespace Message
{
	// Token: 0x02000601 RID: 1537
	public class MsgGetWheelOptionsGeneralSucceed : MessageBase
	{
		// Token: 0x06002B7B RID: 11131 RVA: 0x0010A540 File Offset: 0x00108740
		public MsgGetWheelOptionsGeneralSucceed() : base(61459)
		{
		}

		// Token: 0x0400284B RID: 10315
		public ServerWheelOptionsGeneral m_wheelOptionsGeneral;
	}
}
