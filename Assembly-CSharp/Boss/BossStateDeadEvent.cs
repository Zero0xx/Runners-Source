using System;

namespace Boss
{
	// Token: 0x020008AA RID: 2218
	public class BossStateDeadEvent : FSMState<ObjBossEventBossState>
	{
		// Token: 0x06003B71 RID: 15217 RVA: 0x0013A438 File Offset: 0x00138638
		public override void Enter(ObjBossEventBossState context)
		{
			context.DebugDrawState("BossStateDeadMap");
			context.SetHitCheck(false);
			this.m_state = BossStateDeadEvent.State.Wait1;
			this.m_pass_speed = 0f;
			this.m_time = 0f;
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x0013A46C File Offset: 0x0013866C
		public override void Leave(ObjBossEventBossState context)
		{
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x0013A470 File Offset: 0x00138670
		public override void Step(ObjBossEventBossState context, float delta)
		{
			context.UpdateSpeedUp(delta, this.m_pass_speed);
			BossStateDeadEvent.State state = this.m_state;
			if (state != BossStateDeadEvent.State.Wait1)
			{
				if (state == BossStateDeadEvent.State.Wait2)
				{
					this.m_time += delta;
					if (this.m_time > 3f)
					{
						context.BossEnd(true);
						this.m_state = BossStateDeadEvent.State.Idle;
					}
				}
			}
			else
			{
				this.m_time += delta;
				if (this.m_time > 1.5f)
				{
					context.SetClear();
					this.m_pass_speed = context.BossParam.PlayerSpeed * 2f;
					this.m_time = 0f;
					this.m_state = BossStateDeadEvent.State.Wait2;
				}
			}
		}

		// Token: 0x040033BF RID: 13247
		private const float PASS_SPEED = 2f;

		// Token: 0x040033C0 RID: 13248
		private const float WAIT_TIME1 = 1.5f;

		// Token: 0x040033C1 RID: 13249
		private const float WAIT_TIME2 = 3f;

		// Token: 0x040033C2 RID: 13250
		private BossStateDeadEvent.State m_state;

		// Token: 0x040033C3 RID: 13251
		private float m_pass_speed;

		// Token: 0x040033C4 RID: 13252
		private float m_time;

		// Token: 0x020008AB RID: 2219
		private enum State
		{
			// Token: 0x040033C6 RID: 13254
			Idle,
			// Token: 0x040033C7 RID: 13255
			Wait1,
			// Token: 0x040033C8 RID: 13256
			Wait2
		}
	}
}
