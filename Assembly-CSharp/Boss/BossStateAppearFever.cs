using System;
using Message;
using UnityEngine;

namespace Boss
{
	// Token: 0x02000888 RID: 2184
	public class BossStateAppearFever : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003AEA RID: 15082 RVA: 0x00137C2C File Offset: 0x00135E2C
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStateAppearFever");
			context.SetHitCheck(false);
			context.transform.rotation = Quaternion.Euler(BossStateAppearFever.APPEAR_ROT);
			context.OpenHpGauge();
			this.m_state = BossStateAppearFever.State.Start;
			this.m_time = 0f;
			this.m_distance = 9f + context.BossParam.AddSpeedDistance;
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x00137C90 File Offset: 0x00135E90
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x00137C94 File Offset: 0x00135E94
		public override void Step(ObjBossEggmanState context, float delta)
		{
			switch (this.m_state)
			{
			case BossStateAppearFever.State.Start:
				context.BossMotion.SetMotion(BossMotion.MOVE_R, true);
				context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
				this.m_state = BossStateAppearFever.State.Appear;
				break;
			case BossStateAppearFever.State.Appear:
			{
				float playerDistance = context.GetPlayerDistance();
				if (playerDistance < this.m_distance)
				{
					context.RequestStartChaoAbility();
					context.KeepSpeed();
					context.BossEffect.PlayFoundEffect();
					context.BossMotion.SetMotion(BossMotion.NOTICE, true);
					ObjUtil.PlaySE("boss_find", "SE");
					GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialFeverBoss", new MsgTutorialFeverBoss(), SendMessageOptions.DontRequireReceiver);
					this.m_time = 0f;
					this.m_state = BossStateAppearFever.State.Open;
				}
				break;
			}
			case BossStateAppearFever.State.Open:
				this.m_time += delta;
				if (this.m_time > 0.6f)
				{
					ObjUtil.PlaySE("boss_bomb_drop", "SE");
					this.m_time = 0f;
					this.m_state = BossStateAppearFever.State.Wait;
				}
				break;
			case BossStateAppearFever.State.Wait:
				this.m_time += delta;
				if (this.m_time > 2f)
				{
					context.BossParam.SetupBossTable();
					context.StartGauge();
					context.ChangeState(STATE_ID.AttackFever);
					this.m_state = BossStateAppearFever.State.Idle;
				}
				break;
			}
		}

		// Token: 0x0400330B RID: 13067
		private const float PLAYER_DISTANCE = 9f;

		// Token: 0x0400330C RID: 13068
		private const float WAIT_TIME = 2f;

		// Token: 0x0400330D RID: 13069
		private static Vector3 APPEAR_ROT = new Vector3(0f, 90f, 0f);

		// Token: 0x0400330E RID: 13070
		private BossStateAppearFever.State m_state;

		// Token: 0x0400330F RID: 13071
		private float m_time;

		// Token: 0x04003310 RID: 13072
		private float m_distance;

		// Token: 0x02000889 RID: 2185
		private enum State
		{
			// Token: 0x04003312 RID: 13074
			Idle,
			// Token: 0x04003313 RID: 13075
			Start,
			// Token: 0x04003314 RID: 13076
			Appear,
			// Token: 0x04003315 RID: 13077
			Open,
			// Token: 0x04003316 RID: 13078
			Wait
		}
	}
}
