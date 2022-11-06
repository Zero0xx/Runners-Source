using System;

namespace Boss
{
	// Token: 0x020008B5 RID: 2229
	public class BossStatePassFeverDistanceEnd : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B89 RID: 15241 RVA: 0x0013AB3C File Offset: 0x00138D3C
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStatePassFeverDistanceEnd");
			context.SetHitCheck(false);
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
			this.m_time = 0f;
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x0013AB74 File Offset: 0x00138D74
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x0013AB78 File Offset: 0x00138D78
		public override void Step(ObjBossEggmanState context, float delta)
		{
			this.m_time += delta;
			if (this.m_time > 1f)
			{
				context.ChangeState(STATE_ID.PassFever);
			}
		}

		// Token: 0x04003403 RID: 13315
		private const float WAIT_TIME = 1f;

		// Token: 0x04003404 RID: 13316
		private float m_time;
	}
}
