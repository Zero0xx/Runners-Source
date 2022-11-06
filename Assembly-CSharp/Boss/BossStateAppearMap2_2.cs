using System;
using UnityEngine;

namespace Boss
{
	// Token: 0x0200088E RID: 2190
	public class BossStateAppearMap2_2 : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B08 RID: 15112 RVA: 0x0013813C File Offset: 0x0013633C
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStateAppearMap2_2");
			context.SetHitCheck(false);
			context.transform.rotation = Quaternion.Euler(BossStateAppearMap2_2.APPEAR_ROT);
			context.transform.position = new Vector3(context.GetPlayerPosition().x + context.BossParam.DefaultPlayerDistance, context.BossParam.StartPos.y + BossStateAppearMap2_2.APPEAR_OFFSET_POS_Y, 0f);
			context.SetupMoveY(1f);
			context.KeepSpeed();
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
			this.m_time = 0f;
			this.m_state = BossStateAppearMap2_2.State.Move;
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x001381E8 File Offset: 0x001363E8
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x001381EC File Offset: 0x001363EC
		public override void Step(ObjBossEggmanState context, float delta)
		{
			BossStateAppearMap2_2.State state = this.m_state;
			if (state != BossStateAppearMap2_2.State.Move)
			{
				if (state == BossStateAppearMap2_2.State.Wait)
				{
					this.m_time += delta;
					if (this.m_time > BossStateAppearMap2_2.WAIT_TIME)
					{
						context.ChangeState(STATE_ID.AttackMap2);
						this.m_state = BossStateAppearMap2_2.State.Idle;
					}
				}
			}
			else
			{
				context.UpdateMoveY(delta, context.BossParam.StartPos.y, BossStateAppearMap2_2.MOVE_SPEED);
				if (Mathf.Abs(context.BossParam.StartPos.y - context.transform.position.y) < 0.1f)
				{
					context.transform.position = new Vector3(context.transform.position.x, context.BossParam.StartPos.y, context.transform.position.z);
					context.BossMotion.SetMotion(BossMotion.MISSILE_START, true);
					this.m_state = BossStateAppearMap2_2.State.Wait;
				}
			}
		}

		// Token: 0x04003327 RID: 13095
		private static Vector3 APPEAR_ROT = new Vector3(0f, 90f, 0f);

		// Token: 0x04003328 RID: 13096
		private static float APPEAR_OFFSET_POS_Y = 7f;

		// Token: 0x04003329 RID: 13097
		private static float MOVE_SPEED = 2.5f;

		// Token: 0x0400332A RID: 13098
		private static float WAIT_TIME = 0.5f;

		// Token: 0x0400332B RID: 13099
		private BossStateAppearMap2_2.State m_state;

		// Token: 0x0400332C RID: 13100
		private float m_time;

		// Token: 0x0200088F RID: 2191
		private enum State
		{
			// Token: 0x0400332E RID: 13102
			Idle,
			// Token: 0x0400332F RID: 13103
			Move,
			// Token: 0x04003330 RID: 13104
			Wait
		}
	}
}
