using System;

namespace Boss
{
	// Token: 0x020008A4 RID: 2212
	public class BossStateDamageEvent1 : BossStateDamageEventBase
	{
		// Token: 0x06003B5C RID: 15196 RVA: 0x0013A148 File Offset: 0x00138348
		protected override void ChangeStateWait(ObjBossEventBossState context)
		{
			if (context.BossParam.BossHP > 0)
			{
				context.UpdateBossStateAfterAttack();
				context.ChangeState(EVENTBOSS_STATE_ID.AttackEvent1);
			}
			else
			{
				context.ChangeState(EVENTBOSS_STATE_ID.Dead);
			}
		}
	}
}
