using System;
using Tutorial;
using UnityEngine;

namespace Boss
{
	// Token: 0x020008AC RID: 2220
	public class BossStateDeadFever : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B75 RID: 15221 RVA: 0x0013A530 File Offset: 0x00138730
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStateDeadFever");
			context.SetHitCheck(false);
			this.m_state = BossStateDeadFever.State.Wait1;
			this.m_pass_speed = 0f;
			this.m_time = 0f;
			this.m_bom_time = 0.7f;
			this.m_distance = 7f + context.BossParam.AddSpeedDistance;
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
			ObjUtil.SendMessageTutorialClear(EventID.FEVER_BOSS);
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x0013A5A0 File Offset: 0x001387A0
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x0013A5A4 File Offset: 0x001387A4
		public override void Step(ObjBossEggmanState context, float delta)
		{
			context.UpdateSpeedUp(delta, this.m_pass_speed);
			switch (this.m_state)
			{
			case BossStateDeadFever.State.Wait1:
				this.m_time += delta;
				if (this.m_time > 1f)
				{
					context.BossMotion.SetMotion(BossMotion.ESCAPE, true);
					this.m_time = 0f;
					this.m_state = BossStateDeadFever.State.Wait2;
				}
				break;
			case BossStateDeadFever.State.Wait2:
				this.m_time += delta;
				if (this.m_time > 0.5f)
				{
					this.m_pass_speed = context.BossParam.PlayerSpeed * 2f;
					this.m_time = 0f;
					this.m_state = BossStateDeadFever.State.Bom;
				}
				break;
			case BossStateDeadFever.State.Bom:
			{
				float playerBossPositionX = context.GetPlayerBossPositionX();
				if (playerBossPositionX > this.m_distance)
				{
					this.m_bom_obj = context.CreateBom(false, 25f, true);
					this.m_time = 0f;
					this.m_state = BossStateDeadFever.State.End;
				}
				break;
			}
			case BossStateDeadFever.State.End:
				this.m_time += delta;
				if (this.m_time > this.m_bom_time)
				{
					context.BlastBom(this.m_bom_obj);
					context.BossEnd(true);
					this.m_state = BossStateDeadFever.State.Idle;
				}
				break;
			}
		}

		// Token: 0x040033C9 RID: 13257
		private const float PASS_SPEED = 2f;

		// Token: 0x040033CA RID: 13258
		private const float BOM_DISTANCE = 7f;

		// Token: 0x040033CB RID: 13259
		private const float BOM_TIME = 0.7f;

		// Token: 0x040033CC RID: 13260
		private const float WAIT_TIME1 = 1f;

		// Token: 0x040033CD RID: 13261
		private const float WAIT_TIME2 = 0.5f;

		// Token: 0x040033CE RID: 13262
		private const float BOM_SHOT_SPEED = 25f;

		// Token: 0x040033CF RID: 13263
		private BossStateDeadFever.State m_state;

		// Token: 0x040033D0 RID: 13264
		private float m_pass_speed;

		// Token: 0x040033D1 RID: 13265
		private float m_time;

		// Token: 0x040033D2 RID: 13266
		private float m_distance;

		// Token: 0x040033D3 RID: 13267
		private float m_bom_time;

		// Token: 0x040033D4 RID: 13268
		private GameObject m_bom_obj;

		// Token: 0x020008AD RID: 2221
		private enum State
		{
			// Token: 0x040033D6 RID: 13270
			Idle,
			// Token: 0x040033D7 RID: 13271
			Wait1,
			// Token: 0x040033D8 RID: 13272
			Wait2,
			// Token: 0x040033D9 RID: 13273
			Bom,
			// Token: 0x040033DA RID: 13274
			End
		}
	}
}
