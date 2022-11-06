using System;

namespace Boss
{
	// Token: 0x020008B2 RID: 2226
	public class BossStatePassEventDistanceEnd : FSMState<ObjBossEventBossState>
	{
		// Token: 0x06003B81 RID: 15233 RVA: 0x0013A934 File Offset: 0x00138B34
		public override void Enter(ObjBossEventBossState context)
		{
			context.DebugDrawState("BossStatePassEventDistanceEnd");
			context.SetHitCheck(false);
			this.m_time = 0f;
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x0013A954 File Offset: 0x00138B54
		public override void Leave(ObjBossEventBossState context)
		{
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x0013A958 File Offset: 0x00138B58
		public override void Step(ObjBossEventBossState context, float delta)
		{
			this.m_time += delta;
			if (this.m_time > 1f)
			{
				context.ChangeState(EVENTBOSS_STATE_ID.Pass);
			}
		}

		// Token: 0x040033F1 RID: 13297
		private const float WAIT_TIME = 1f;

		// Token: 0x040033F2 RID: 13298
		private float m_time;
	}
}
