using System;

namespace Boss
{
	// Token: 0x020008A8 RID: 2216
	public class BossStateDamageMap2 : BossStateDamageFever
	{
		// Token: 0x06003B6A RID: 15210 RVA: 0x0013A35C File Offset: 0x0013855C
		protected override string GetStateName()
		{
			return "BossStateDamageMap2";
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x0013A364 File Offset: 0x00138564
		protected override void Setup(ObjBossEggmanState context)
		{
			base.Setup(context);
			base.SetRotAngle(-context.transform.right);
			base.SetDistance(context.BossParam.DefaultPlayerDistance);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x0013A3A0 File Offset: 0x001385A0
		protected override void ChangeStateWait(ObjBossEggmanState context)
		{
			context.KeepSpeed();
			if (context.BossParam.BossHP > 0)
			{
				context.UpdateBossStateAfterAttack();
				context.ChangeState(STATE_ID.AttackMap2);
			}
			else
			{
				context.ChangeState(STATE_ID.DeadMap);
			}
		}
	}
}
