using System;

namespace Message
{
	// Token: 0x02000587 RID: 1415
	public class MsgChangeCurrentBlock : MessageBase
	{
		// Token: 0x06002AEB RID: 10987 RVA: 0x00109790 File Offset: 0x00107990
		public MsgChangeCurrentBlock(int block, int layer, int activateID) : base(12302)
		{
			this.m_blockNo = block;
			this.m_activateID = activateID;
			this.m_layerNo = layer;
		}

		// Token: 0x0400274F RID: 10063
		public int m_blockNo;

		// Token: 0x04002750 RID: 10064
		public int m_activateID;

		// Token: 0x04002751 RID: 10065
		public int m_layerNo;
	}
}
