using System;

namespace Boss
{
	// Token: 0x020008A6 RID: 2214
	public class BossStateDamageFever : BossStateDamageBase
	{
		// Token: 0x06003B60 RID: 15200 RVA: 0x0013A1C8 File Offset: 0x001383C8
		protected override string GetStateName()
		{
			return "BossStateDamageFever";
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x0013A1D0 File Offset: 0x001383D0
		protected override void Setup(ObjBossEggmanState context)
		{
			float damageSpeedParam = context.GetDamageSpeedParam();
			context.SetSpeed((60f + context.BossParam.AddSpeed) * damageSpeedParam);
			base.SetSpeedDown(120f + context.BossParam.AddSpeed);
			base.SetRotSpeed(45f);
			base.SetRotSpeedDown(0.9f);
			base.SetRotMin(10f);
			base.SetDistance(9f);
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x0013A240 File Offset: 0x00138440
		protected override void SetStateSpeedDown(ObjBossEggmanState context)
		{
			base.SetSpeedDown(0.5f);
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x0013A250 File Offset: 0x00138450
		protected override void ChangeStateWait(ObjBossEggmanState context)
		{
			if (context.BossParam.BossHP > 0)
			{
				context.UpdateBossStateAfterAttack();
				context.ChangeState(STATE_ID.AttackFever);
			}
			else
			{
				context.ChangeState(STATE_ID.DeadFever);
			}
		}

		// Token: 0x040033B2 RID: 13234
		private const float START_SPEED = 60f;

		// Token: 0x040033B3 RID: 13235
		private const float START_DOWNSPEED = 120f;

		// Token: 0x040033B4 RID: 13236
		private const float WAIT_DOWNSPEED = 0.5f;

		// Token: 0x040033B5 RID: 13237
		private const float ROT_SPEED = 45f;

		// Token: 0x040033B6 RID: 13238
		private const float ROT_DOWNSPEED = 0.9f;

		// Token: 0x040033B7 RID: 13239
		private const float ROT_MIN = 10f;

		// Token: 0x040033B8 RID: 13240
		private const float END_DISTANCE = 9f;
	}
}
