using System;
using Message;
using UnityEngine;

namespace Boss
{
	// Token: 0x02000882 RID: 2178
	public class BossStateAppearEventBase : FSMState<ObjBossEventBossState>
	{
		// Token: 0x06003ADB RID: 15067 RVA: 0x00137898 File Offset: 0x00135A98
		public override void Enter(ObjBossEventBossState context)
		{
			context.DebugDrawState("BossStateAppearEvent");
			context.SetHitCheck(false);
			context.transform.rotation = Quaternion.Euler(BossStateAppearEventBase.APPEAR_ROT);
			context.transform.position = new Vector3(context.GetPlayerPosition().x + context.BossParam.DefaultPlayerDistance, context.BossParam.StartPos.y + BossStateAppearEventBase.APPEAR_OFFSET_POS_Y, 0f);
			context.SetupMoveY(1f);
			context.KeepSpeed();
			if (this.IsFirst())
			{
				context.OpenHpGauge();
			}
			else
			{
				context.BossMotion.SetMotion(EventBossMotion.ATTACK, false);
			}
			this.m_time = 0f;
			this.m_state = BossStateAppearEventBase.State.Start;
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x0013795C File Offset: 0x00135B5C
		public override void Leave(ObjBossEventBossState context)
		{
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x00137960 File Offset: 0x00135B60
		public override void Step(ObjBossEventBossState context, float delta)
		{
			switch (this.m_state)
			{
			case BossStateAppearEventBase.State.Start:
				if (this.IsFirst())
				{
					this.m_time += delta;
					if (this.m_time > BossStateAppearEventBase.START_TIME)
					{
						this.m_time = 0f;
						this.m_state = BossStateAppearEventBase.State.Move;
					}
				}
				else
				{
					this.m_time = 0f;
					this.m_state = BossStateAppearEventBase.State.Move;
				}
				break;
			case BossStateAppearEventBase.State.Move:
				context.UpdateMoveY(delta, context.BossParam.StartPos.y, BossStateAppearEventBase.MOVE_SPEED);
				if (Mathf.Abs(context.BossParam.StartPos.y - context.transform.position.y) < 0.1f)
				{
					context.transform.position = new Vector3(context.transform.position.x, context.BossParam.StartPos.y, context.transform.position.z);
					if (this.IsFirst())
					{
						context.BossMotion.SetMotion(EventBossMotion.APPEAR, true);
					}
					this.m_time = 0f;
					this.m_state = BossStateAppearEventBase.State.Wait1;
				}
				break;
			case BossStateAppearEventBase.State.Wait1:
				if (this.IsFirst())
				{
					this.m_time += delta;
					if (this.m_time > BossStateAppearEventBase.WAIT_TIME1)
					{
						MsgTutorialMapBoss value = new MsgTutorialMapBoss();
						GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialMapBoss", value, SendMessageOptions.DontRequireReceiver);
						context.BossParam.SetupBossTable();
						context.StartGauge();
						this.m_time = 0f;
						this.m_state = BossStateAppearEventBase.State.Wait2;
					}
				}
				else
				{
					this.m_time = 0f;
					this.m_state = BossStateAppearEventBase.State.Wait2;
				}
				break;
			case BossStateAppearEventBase.State.Wait2:
				if (this.IsFirst())
				{
					this.m_time += delta;
					if (this.m_time > BossStateAppearEventBase.WAIT_TIME2)
					{
						context.RequestStartChaoAbility();
						this.m_time = 0f;
						this.m_state = BossStateAppearEventBase.State.Wait3;
					}
				}
				else
				{
					this.m_time = 0f;
					this.m_state = BossStateAppearEventBase.State.Wait3;
				}
				break;
			case BossStateAppearEventBase.State.Wait3:
				this.m_time += delta;
				if (this.m_time > BossStateAppearEventBase.WAIT_TIME3)
				{
					context.ChangeState(this.GetNextChangeState());
					this.m_state = BossStateAppearEventBase.State.Idle;
				}
				break;
			}
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x00137BD0 File Offset: 0x00135DD0
		protected virtual EVENTBOSS_STATE_ID GetNextChangeState()
		{
			return EVENTBOSS_STATE_ID.AttackEvent1;
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x00137BD4 File Offset: 0x00135DD4
		protected virtual bool IsFirst()
		{
			return true;
		}

		// Token: 0x040032FB RID: 13051
		private static Vector3 APPEAR_ROT = new Vector3(0f, 90f, 0f);

		// Token: 0x040032FC RID: 13052
		private static float APPEAR_OFFSET_POS_Y = 7f;

		// Token: 0x040032FD RID: 13053
		private static float MOVE_SPEED = 2.5f;

		// Token: 0x040032FE RID: 13054
		private static float START_TIME = 3.5f;

		// Token: 0x040032FF RID: 13055
		private static float WAIT_TIME1 = 2f;

		// Token: 0x04003300 RID: 13056
		private static float WAIT_TIME2 = 1f;

		// Token: 0x04003301 RID: 13057
		private static float WAIT_TIME3 = 1f;

		// Token: 0x04003302 RID: 13058
		private BossStateAppearEventBase.State m_state;

		// Token: 0x04003303 RID: 13059
		private float m_time;

		// Token: 0x02000883 RID: 2179
		private enum State
		{
			// Token: 0x04003305 RID: 13061
			Idle,
			// Token: 0x04003306 RID: 13062
			Start,
			// Token: 0x04003307 RID: 13063
			Move,
			// Token: 0x04003308 RID: 13064
			Wait1,
			// Token: 0x04003309 RID: 13065
			Wait2,
			// Token: 0x0400330A RID: 13066
			Wait3
		}
	}
}
