using System;
using UnityEngine;

namespace Boss
{
	// Token: 0x0200088D RID: 2189
	public class BossStateAppearMap2 : BossStateAppearMapBase
	{
		// Token: 0x06003B00 RID: 15104 RVA: 0x0013806C File Offset: 0x0013626C
		public override void Enter(ObjBossEggmanState context)
		{
			base.Enter(context);
			context.transform.position = new Vector3(context.transform.position.x, context.BossParam.StartPos.y, context.transform.position.z);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x001380CC File Offset: 0x001362CC
		protected override float GetTime1()
		{
			return BossStateAppearMap2.WAIT_TIME1;
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x001380D4 File Offset: 0x001362D4
		protected override float GetTime2()
		{
			return BossStateAppearMap2.WAIT_TIME2;
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x001380DC File Offset: 0x001362DC
		protected override float GetTime3()
		{
			return BossStateAppearMap2.WAIT_TIME3;
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x001380E4 File Offset: 0x001362E4
		protected override void SetMotion3(ObjBossEggmanState context)
		{
			context.BossMotion.SetMotion(BossMotion.MISSILE_START, true);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x001380F4 File Offset: 0x001362F4
		protected override STATE_ID GetNextChangeState()
		{
			return STATE_ID.AttackMap2;
		}

		// Token: 0x04003324 RID: 13092
		private static float WAIT_TIME1 = 3f;

		// Token: 0x04003325 RID: 13093
		private static float WAIT_TIME2 = 2f;

		// Token: 0x04003326 RID: 13094
		private static float WAIT_TIME3 = 2f;
	}
}
