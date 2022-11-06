using System;

namespace Boss
{
	// Token: 0x020008AE RID: 2222
	public class BossStateDeadMap : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B79 RID: 15225 RVA: 0x0013A6F4 File Offset: 0x001388F4
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStateDeadMap");
			context.SetHitCheck(false);
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
			this.m_state = BossStateDeadMap.State.Wait1;
			this.m_pass_speed = 0f;
			this.m_time = 0f;
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x0013A73C File Offset: 0x0013893C
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x0013A740 File Offset: 0x00138940
		public override void Step(ObjBossEggmanState context, float delta)
		{
			context.UpdateSpeedUp(delta, this.m_pass_speed);
			BossStateDeadMap.State state = this.m_state;
			if (state != BossStateDeadMap.State.Wait1)
			{
				if (state == BossStateDeadMap.State.Wait2)
				{
					this.m_time += delta;
					if (this.m_time > 3f)
					{
						context.BossEnd(true);
						this.m_state = BossStateDeadMap.State.Idle;
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
					this.m_state = BossStateDeadMap.State.Wait2;
				}
			}
		}

		// Token: 0x040033DB RID: 13275
		private const float PASS_SPEED = 2f;

		// Token: 0x040033DC RID: 13276
		private const float WAIT_TIME1 = 1.5f;

		// Token: 0x040033DD RID: 13277
		private const float WAIT_TIME2 = 3f;

		// Token: 0x040033DE RID: 13278
		private BossStateDeadMap.State m_state;

		// Token: 0x040033DF RID: 13279
		private float m_pass_speed;

		// Token: 0x040033E0 RID: 13280
		private float m_time;

		// Token: 0x020008AF RID: 2223
		private enum State
		{
			// Token: 0x040033E2 RID: 13282
			Idle,
			// Token: 0x040033E3 RID: 13283
			Wait1,
			// Token: 0x040033E4 RID: 13284
			Wait2
		}
	}
}
