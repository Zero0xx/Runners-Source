using System;

namespace Boss
{
	// Token: 0x020008B8 RID: 2232
	public class BossStatePassMapDistanceEnd : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B91 RID: 15249 RVA: 0x0013ACA8 File Offset: 0x00138EA8
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStatePassMapDistanceEnd");
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
			context.SetHitCheck(false);
			this.m_time = 0f;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x0013ACE0 File Offset: 0x00138EE0
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x0013ACE4 File Offset: 0x00138EE4
		public override void Step(ObjBossEggmanState context, float delta)
		{
			this.m_time += delta;
			if (this.m_time > 1f)
			{
				context.ChangeState(STATE_ID.PassMap);
			}
		}

		// Token: 0x0400340F RID: 13327
		private const float WAIT_TIME = 1f;

		// Token: 0x04003410 RID: 13328
		private float m_time;
	}
}
