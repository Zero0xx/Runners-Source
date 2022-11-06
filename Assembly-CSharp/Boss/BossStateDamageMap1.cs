using System;

namespace Boss
{
	// Token: 0x020008A7 RID: 2215
	public class BossStateDamageMap1 : BossStateDamageBase
	{
		// Token: 0x06003B65 RID: 15205 RVA: 0x0013A290 File Offset: 0x00138490
		protected override string GetStateName()
		{
			return "BossStateDamageMap1";
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x0013A298 File Offset: 0x00138498
		protected override void Setup(ObjBossEggmanState context)
		{
			context.SetSpeed(20f);
			base.SetSpeedDown(40f);
			base.SetRotSpeed(30f);
			base.SetRotSpeedDown(0.3f);
			base.SetRotMin(2f);
			base.SetRotAngle(-context.transform.right);
			base.SetDistance(context.BossParam.DefaultPlayerDistance);
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x0013A304 File Offset: 0x00138504
		protected override void SetStateSpeedDown(ObjBossEggmanState context)
		{
			base.SetSpeedDown(1f);
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x0013A314 File Offset: 0x00138514
		protected override void ChangeStateWait(ObjBossEggmanState context)
		{
			context.KeepSpeed();
			if (context.BossParam.BossHP > 0)
			{
				context.UpdateBossStateAfterAttack();
				context.ChangeState(STATE_ID.AttackMap1);
			}
			else
			{
				context.ChangeState(STATE_ID.DeadMap);
			}
		}

		// Token: 0x040033B9 RID: 13241
		private const float START_SPEED = 20f;

		// Token: 0x040033BA RID: 13242
		private const float START_DOWNSPEED = 40f;

		// Token: 0x040033BB RID: 13243
		private const float WAIT_DOWNSPEED = 1f;

		// Token: 0x040033BC RID: 13244
		private const float ROT_SPEED = 30f;

		// Token: 0x040033BD RID: 13245
		private const float ROT_DOWNSPEED = 0.3f;

		// Token: 0x040033BE RID: 13246
		private const float ROT_MIN = 2f;
	}
}
