using System;

namespace Message
{
	// Token: 0x02000585 RID: 1413
	public class MsgDeactivateBlock : MessageBase
	{
		// Token: 0x06002AE9 RID: 10985 RVA: 0x00109750 File Offset: 0x00107950
		public MsgDeactivateBlock(int block, int activateID, float distance) : base(12300)
		{
			this.m_blockNo = block;
			this.m_activateID = activateID;
			this.m_distance = distance;
		}

		// Token: 0x0400274C RID: 10060
		public float m_distance;

		// Token: 0x0400274D RID: 10061
		public int m_blockNo;

		// Token: 0x0400274E RID: 10062
		public int m_activateID;
	}
}
