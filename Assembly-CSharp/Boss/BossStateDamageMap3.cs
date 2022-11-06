using System;

namespace Boss
{
	// Token: 0x020008A9 RID: 2217
	public class BossStateDamageMap3 : BossStateDamageMap1
	{
		// Token: 0x06003B6E RID: 15214 RVA: 0x0013A3E8 File Offset: 0x001385E8
		protected override string GetStateName()
		{
			return "BossStateDamageMap3";
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x0013A3F0 File Offset: 0x001385F0
		protected override void ChangeStateWait(ObjBossEggmanState context)
		{
			context.KeepSpeed();
			if (context.BossParam.BossHP > 0)
			{
				context.UpdateBossStateAfterAttack();
				context.ChangeState(STATE_ID.AttackMap3);
			}
			else
			{
				context.ChangeState(STATE_ID.DeadMap);
			}
		}
	}
}
