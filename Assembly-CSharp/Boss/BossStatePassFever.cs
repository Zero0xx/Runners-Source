using System;
using Message;
using Tutorial;
using UnityEngine;

namespace Boss
{
	// Token: 0x020008B3 RID: 2227
	public class BossStatePassFever : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B85 RID: 15237 RVA: 0x0013A994 File Offset: 0x00138B94
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStatePassFever");
			context.SetHitCheck(false);
			this.m_state = BossStatePassFever.State.Bom;
			this.m_pass_speed = context.BossParam.PlayerSpeed * 2f;
			this.m_distance_1 = 0f + context.BossParam.AddSpeedDistance;
			this.m_distance_2 = 9f + context.BossParam.AddSpeedDistance;
			this.m_distance_3 = 7f + context.BossParam.AddSpeedDistance;
			context.BossMotion.SetMotion(BossMotion.PASS, true);
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Attack);
			ObjUtil.SendMessageTutorialClear(EventID.FEVER_BOSS);
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x0013AA38 File Offset: 0x00138C38
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x0013AA3C File Offset: 0x00138C3C
		public override void Step(ObjBossEggmanState context, float delta)
		{
			context.UpdateSpeedUp(delta, this.m_pass_speed);
			switch (this.m_state)
			{
			case BossStatePassFever.State.Bom:
			{
				float playerBossPositionX = context.GetPlayerBossPositionX();
				if (playerBossPositionX > this.m_distance_1)
				{
					ObjUtil.PauseCombo(MsgPauseComboTimer.State.PAUSE_TIMER, -1f);
					this.m_bom_obj = context.CreateBom(false, 20f, false);
					this.m_state = BossStatePassFever.State.Shot;
				}
				break;
			}
			case BossStatePassFever.State.Shot:
			{
				float playerBossPositionX2 = context.GetPlayerBossPositionX();
				if (playerBossPositionX2 > this.m_distance_2)
				{
					context.ShotBom(this.m_bom_obj);
					this.m_state = BossStatePassFever.State.End;
				}
				break;
			}
			case BossStatePassFever.State.End:
			{
				float num = this.m_bom_obj.transform.position.x - context.GetPlayerPosition().x;
				if (num < this.m_distance_3)
				{
					context.BlastBom(this.m_bom_obj);
					context.BossEnd(false);
					this.m_state = BossStatePassFever.State.Idle;
				}
				break;
			}
			}
		}

		// Token: 0x040033F3 RID: 13299
		private const float PASS_SPEED = 2f;

		// Token: 0x040033F4 RID: 13300
		private const float BOM_DISTANCE1 = 0f;

		// Token: 0x040033F5 RID: 13301
		private const float BOM_DISTANCE2 = 9f;

		// Token: 0x040033F6 RID: 13302
		private const float BOM_DISTANCE3 = 7f;

		// Token: 0x040033F7 RID: 13303
		private const float BOM_SHOT_SPEED = 20f;

		// Token: 0x040033F8 RID: 13304
		private BossStatePassFever.State m_state;

		// Token: 0x040033F9 RID: 13305
		private float m_pass_speed;

		// Token: 0x040033FA RID: 13306
		private float m_distance_1;

		// Token: 0x040033FB RID: 13307
		private float m_distance_2;

		// Token: 0x040033FC RID: 13308
		private float m_distance_3;

		// Token: 0x040033FD RID: 13309
		private GameObject m_bom_obj;

		// Token: 0x020008B4 RID: 2228
		private enum State
		{
			// Token: 0x040033FF RID: 13311
			Idle,
			// Token: 0x04003400 RID: 13312
			Bom,
			// Token: 0x04003401 RID: 13313
			Shot,
			// Token: 0x04003402 RID: 13314
			End
		}
	}
}
