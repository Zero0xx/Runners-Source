using System;

namespace Message
{
	// Token: 0x020005ED RID: 1517
	public class MsgGetMigrationPasswordSucceed : MessageBase
	{
		// Token: 0x06002B67 RID: 11111 RVA: 0x0010A400 File Offset: 0x00108600
		public MsgGetMigrationPasswordSucceed() : base(61442)
		{
		}

		// Token: 0x0400282E RID: 10286
		public string m_migrationPassword;
	}
}
