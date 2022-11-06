using System;

namespace Boss
{
	// Token: 0x0200088C RID: 2188
	public class BossStateAppearMap1 : BossStateAppearMapBase
	{
		// Token: 0x06003AFB RID: 15099 RVA: 0x00138030 File Offset: 0x00136230
		protected override float GetTime1()
		{
			return BossStateAppearMap1.WAIT_TIME1;
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00138038 File Offset: 0x00136238
		protected override float GetTime2()
		{
			return BossStateAppearMap1.WAIT_TIME2;
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00138040 File Offset: 0x00136240
		protected override STATE_ID GetNextChangeState()
		{
			return STATE_ID.AttackMap1;
		}

		// Token: 0x04003322 RID: 13090
		private static float WAIT_TIME1 = 3f;

		// Token: 0x04003323 RID: 13091
		private static float WAIT_TIME2 = 1f;
	}
}
