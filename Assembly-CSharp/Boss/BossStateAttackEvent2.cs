using System;

namespace Boss
{
	// Token: 0x02000894 RID: 2196
	public class BossStateAttackEvent2 : BossStateAttackEventBase
	{
		// Token: 0x06003B1A RID: 15130 RVA: 0x001384B0 File Offset: 0x001366B0
		public override void Enter(ObjBossEventBossState context)
		{
			base.Enter(context);
			context.DebugDrawState("BossStateAttackEvent2");
			this.m_state = BossStateAttackEvent2.State.Idle;
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x001384CC File Offset: 0x001366CC
		public override void Leave(ObjBossEventBossState context)
		{
			base.Leave(context);
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x001384D8 File Offset: 0x001366D8
		public override void Step(ObjBossEventBossState context, float delta)
		{
			if (context.IsPlayerDead())
			{
				return;
			}
			if (context.IsBossDistanceEnd())
			{
				context.ChangeState(EVENTBOSS_STATE_ID.PassDistanceEnd);
				return;
			}
			base.UpdateTrapBall(context, delta);
			if (base.UpdateBumper(context, delta))
			{
				this.m_state = BossStateAttackEvent2.State.Speedup;
			}
			BossStateAttackEvent2.State state = this.m_state;
			if (state != BossStateAttackEvent2.State.Idle)
			{
				if (state == BossStateAttackEvent2.State.Speedup)
				{
					if (base.UpdateBumperSpeedup(context, delta))
					{
						this.m_state = BossStateAttackEvent2.State.Idle;
					}
				}
			}
			else if (base.UpdateBoost(context, delta))
			{
				context.ChangeState(EVENTBOSS_STATE_ID.AppearEvent2_2);
			}
		}

		// Token: 0x04003338 RID: 13112
		private BossStateAttackEvent2.State m_state;

		// Token: 0x02000895 RID: 2197
		private enum State
		{
			// Token: 0x0400333A RID: 13114
			Idle,
			// Token: 0x0400333B RID: 13115
			Speedup
		}
	}
}
