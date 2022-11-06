using System;

namespace Boss
{
	// Token: 0x02000892 RID: 2194
	public class BossStateAttackEvent1 : BossStateAttackEventBase
	{
		// Token: 0x06003B16 RID: 15126 RVA: 0x001383EC File Offset: 0x001365EC
		public override void Enter(ObjBossEventBossState context)
		{
			base.Enter(context);
			context.DebugDrawState("BossStateAttackEvent1");
			this.m_state = BossStateAttackEvent1.State.Idle;
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x00138408 File Offset: 0x00136608
		public override void Leave(ObjBossEventBossState context)
		{
			base.Leave(context);
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x00138414 File Offset: 0x00136614
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
			base.UpdateMissile(context, delta);
			if (base.UpdateBumper(context, delta))
			{
				this.m_state = BossStateAttackEvent1.State.Speedup;
			}
			BossStateAttackEvent1.State state = this.m_state;
			if (state != BossStateAttackEvent1.State.Idle)
			{
				if (state == BossStateAttackEvent1.State.Speedup)
				{
					if (base.UpdateBumperSpeedup(context, delta))
					{
						this.m_state = BossStateAttackEvent1.State.Idle;
					}
				}
			}
			else if (base.UpdateBoost(context, delta))
			{
				context.ChangeState(EVENTBOSS_STATE_ID.AppearEvent1_2);
			}
		}

		// Token: 0x04003334 RID: 13108
		private BossStateAttackEvent1.State m_state;

		// Token: 0x02000893 RID: 2195
		private enum State
		{
			// Token: 0x04003336 RID: 13110
			Idle,
			// Token: 0x04003337 RID: 13111
			Speedup
		}
	}
}
