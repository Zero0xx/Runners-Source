using System;

namespace Message
{
	// Token: 0x020005D9 RID: 1497
	public class MsgResetScore
	{
		// Token: 0x06002B4F RID: 11087 RVA: 0x0010A1C8 File Offset: 0x001083C8
		public MsgResetScore()
		{
			this.m_score = 0;
			this.m_animal = 0;
			this.m_ring = 0;
			this.m_red_ring = 0;
			this.m_final_score = 0;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x0010A1F4 File Offset: 0x001083F4
		public MsgResetScore(int score, int animal, int ring)
		{
			this.m_score = score;
			this.m_animal = animal;
			this.m_ring = ring;
			this.m_red_ring = 0;
			this.m_final_score = 0;
		}

		// Token: 0x04002813 RID: 10259
		public int m_score;

		// Token: 0x04002814 RID: 10260
		public int m_animal;

		// Token: 0x04002815 RID: 10261
		public int m_ring;

		// Token: 0x04002816 RID: 10262
		public int m_red_ring;

		// Token: 0x04002817 RID: 10263
		public int m_final_score;
	}
}
