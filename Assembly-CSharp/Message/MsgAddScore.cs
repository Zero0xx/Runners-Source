using System;

namespace Message
{
	// Token: 0x020005CD RID: 1485
	public class MsgAddScore
	{
		// Token: 0x06002B40 RID: 11072 RVA: 0x00109FE0 File Offset: 0x001081E0
		public MsgAddScore(int score)
		{
			this.m_score = score;
		}

		// Token: 0x040027F1 RID: 10225
		public readonly int m_score;
	}
}
