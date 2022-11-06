using System;

namespace Boss
{
	// Token: 0x020008B0 RID: 2224
	public class BossStatePassEvent : FSMState<ObjBossEventBossState>
	{
		// Token: 0x06003B7D RID: 15229 RVA: 0x0013A800 File Offset: 0x00138A00
		public override void Enter(ObjBossEventBossState context)
		{
			context.DebugDrawState("BossStatePassMap");
			context.SetHitCheck(false);
			this.m_state = BossStatePassEvent.State.Wait1;
			this.m_time = 0f;
			context.BossMotion.SetMotion(EventBossMotion.PASS, true);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x0013A840 File Offset: 0x00138A40
		public override void Leave(ObjBossEventBossState context)
		{
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x0013A844 File Offset: 0x00138A44
		public override void Step(ObjBossEventBossState context, float delta)
		{
			switch (this.m_state)
			{
			case BossStatePassEvent.State.Wait1:
				this.m_time += delta;
				if (this.m_time > 1f)
				{
					this.m_pass_speed = context.BossParam.PlayerSpeed * 2f;
					this.m_state = BossStatePassEvent.State.Wait2;
				}
				break;
			case BossStatePassEvent.State.Wait2:
			{
				context.UpdateSpeedUp(delta, this.m_pass_speed);
				float playerBossPositionX = context.GetPlayerBossPositionX();
				if (playerBossPositionX > 8f)
				{
					context.SetFailed();
					this.m_time = 0f;
					this.m_state = BossStatePassEvent.State.End;
				}
				break;
			}
			case BossStatePassEvent.State.End:
				context.UpdateSpeedUp(delta, this.m_pass_speed);
				this.m_time += delta;
				if (this.m_time > 1f)
				{
					context.BossEnd(false);
					this.m_state = BossStatePassEvent.State.Idle;
				}
				break;
			}
		}

		// Token: 0x040033E5 RID: 13285
		private const float PASS_SPEED = 2f;

		// Token: 0x040033E6 RID: 13286
		private const float END_DISTANCE = 8f;

		// Token: 0x040033E7 RID: 13287
		private const float END_TIME = 1f;

		// Token: 0x040033E8 RID: 13288
		private const float WAIT_TIME = 1f;

		// Token: 0x040033E9 RID: 13289
		private BossStatePassEvent.State m_state;

		// Token: 0x040033EA RID: 13290
		private float m_time;

		// Token: 0x040033EB RID: 13291
		private float m_pass_speed;

		// Token: 0x020008B1 RID: 2225
		private enum State
		{
			// Token: 0x040033ED RID: 13293
			Idle,
			// Token: 0x040033EE RID: 13294
			Wait1,
			// Token: 0x040033EF RID: 13295
			Wait2,
			// Token: 0x040033F0 RID: 13296
			End
		}
	}
}
