using System;

namespace Boss
{
	// Token: 0x020008B6 RID: 2230
	public class BossStatePassMap : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B8D RID: 15245 RVA: 0x0013ABB4 File Offset: 0x00138DB4
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStatePassMap");
			context.SetHitCheck(false);
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
			this.m_state = BossStatePassMap.State.Wait;
			this.m_time = 0f;
			this.m_pass_speed = context.BossParam.PlayerSpeed * 2f;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x0013AC08 File Offset: 0x00138E08
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x0013AC0C File Offset: 0x00138E0C
		public override void Step(ObjBossEggmanState context, float delta)
		{
			context.UpdateSpeedUp(delta, this.m_pass_speed);
			BossStatePassMap.State state = this.m_state;
			if (state != BossStatePassMap.State.Wait)
			{
				if (state == BossStatePassMap.State.End)
				{
					this.m_time += delta;
					if (this.m_time > 1f)
					{
						context.BossEnd(false);
						this.m_state = BossStatePassMap.State.Idle;
					}
				}
			}
			else
			{
				float playerBossPositionX = context.GetPlayerBossPositionX();
				if (playerBossPositionX > 8f)
				{
					context.SetFailed();
					this.m_time = 0f;
					this.m_state = BossStatePassMap.State.End;
				}
			}
		}

		// Token: 0x04003405 RID: 13317
		private const float PASS_SPEED = 2f;

		// Token: 0x04003406 RID: 13318
		private const float END_DISTANCE = 8f;

		// Token: 0x04003407 RID: 13319
		private const float END_TIME = 1f;

		// Token: 0x04003408 RID: 13320
		private BossStatePassMap.State m_state;

		// Token: 0x04003409 RID: 13321
		private float m_time;

		// Token: 0x0400340A RID: 13322
		private float m_pass_speed;

		// Token: 0x020008B7 RID: 2231
		private enum State
		{
			// Token: 0x0400340C RID: 13324
			Idle,
			// Token: 0x0400340D RID: 13325
			Wait,
			// Token: 0x0400340E RID: 13326
			End
		}
	}
}
