using System;

namespace Message
{
	// Token: 0x020005EE RID: 1518
	public class MsgGetCountrySucceed : MessageBase
	{
		// Token: 0x06002B68 RID: 11112 RVA: 0x0010A410 File Offset: 0x00108610
		public MsgGetCountrySucceed() : base(61443)
		{
		}

		// Token: 0x0400282F RID: 10287
		public int m_countryId;

		// Token: 0x04002830 RID: 10288
		public string m_countryCode;
	}
}
