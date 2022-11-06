using System;

namespace Boss
{
	// Token: 0x02000891 RID: 2193
	public class BossStateAttackBase : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B0E RID: 15118 RVA: 0x00138314 File Offset: 0x00136514
		public override void Enter(ObjBossEggmanState context)
		{
			context.SetHitCheck(true);
			context.SetupMoveY(0f);
			this.m_speed = 0f;
			this.m_time = 0f;
			this.m_boss_y = 0f;
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x0013834C File Offset: 0x0013654C
		public override void Leave(ObjBossEggmanState context)
		{
			context.SetHitCheck(false);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x00138358 File Offset: 0x00136558
		protected bool UpdateTime(float delta, float time_max)
		{
			this.m_time += delta;
			return this.m_time > time_max;
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x00138378 File Offset: 0x00136578
		protected void ResetTime()
		{
			this.m_time = 0f;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x00138388 File Offset: 0x00136588
		protected void UpdateMove(ObjBossEggmanState context, float delta)
		{
			context.UpdateMoveY(delta, this.m_boss_y, this.m_speed);
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x001383A0 File Offset: 0x001365A0
		protected void SetMove(ObjBossEggmanState context, float step, float speed, float boss_y)
		{
			context.SetupMoveY(step);
			this.m_speed = speed;
			this.m_boss_y = boss_y;
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x001383B8 File Offset: 0x001365B8
		protected bool IsMoveStepEquals(ObjBossEggmanState context, float val)
		{
			return context.BossParam.StepMoveY.Equals(val);
		}

		// Token: 0x04003331 RID: 13105
		private float m_speed;

		// Token: 0x04003332 RID: 13106
		private float m_time;

		// Token: 0x04003333 RID: 13107
		private float m_boss_y;
	}
}
