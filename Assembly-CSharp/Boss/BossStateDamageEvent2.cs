using System;

namespace Boss
{
	// Token: 0x020008A5 RID: 2213
	public class BossStateDamageEvent2 : BossStateDamageEventBase
	{
		// Token: 0x06003B5E RID: 15198 RVA: 0x0013A188 File Offset: 0x00138388
		protected override void ChangeStateWait(ObjBossEventBossState context)
		{
			if (context.BossParam.BossHP > 0)
			{
				context.UpdateBossStateAfterAttack();
				context.ChangeState(EVENTBOSS_STATE_ID.AttackEvent2);
			}
			else
			{
				context.ChangeState(EVENTBOSS_STATE_ID.Dead);
			}
		}
	}
}
