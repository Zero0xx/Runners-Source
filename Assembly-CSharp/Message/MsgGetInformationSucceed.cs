using System;

namespace Message
{
	// Token: 0x0200061D RID: 1565
	public class MsgGetInformationSucceed : MessageBase
	{
		// Token: 0x06002B97 RID: 11159 RVA: 0x0010A7A0 File Offset: 0x001089A0
		public MsgGetInformationSucceed() : base(61488)
		{
		}

		// Token: 0x04002879 RID: 10361
		public ServerNoticeInfo m_information;
	}
}
