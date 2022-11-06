using System;

namespace Boss
{
	// Token: 0x02000890 RID: 2192
	public class BossStateAppearMap3 : BossStateAppearMap1
	{
		// Token: 0x06003B0C RID: 15116 RVA: 0x00138308 File Offset: 0x00136508
		protected override STATE_ID GetNextChangeState()
		{
			return STATE_ID.AttackMap3;
		}
	}
}
