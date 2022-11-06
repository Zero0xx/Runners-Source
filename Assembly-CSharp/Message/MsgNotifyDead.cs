using System;

namespace Message
{
	// Token: 0x020005A9 RID: 1449
	public class MsgNotifyDead : MessageBase
	{
		// Token: 0x06002B17 RID: 11031 RVA: 0x00109C00 File Offset: 0x00107E00
		public MsgNotifyDead() : base(20480)
		{
		}

		// Token: 0x04002792 RID: 10130
		private int playerNo;
	}
}
