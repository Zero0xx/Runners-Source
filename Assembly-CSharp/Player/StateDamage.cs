using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x0200099E RID: 2462
	public class StateDamage : FSMState<CharacterState>
	{
		// Token: 0x06004086 RID: 16518 RVA: 0x0014EAF4 File Offset: 0x0014CCF4
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Run);
			context.Movement.OffGround();
			this.m_timer = context.Parameter.m_damageStumbleTime;
			context.GetAnimator().CrossFade("Damaged", 0.05f);
			context.Movement.HorzVelocity = context.Movement.GetForwardDir() * context.DefaultSpeed * context.Parameter.m_damageSpeedRate;
			context.StartDamageBlink();
			if (!context.m_notDropRing)
			{
				SoundManager.SePlay("act_ringspread", "SE");
			}
			context.ClearAirAction();
			if (StageTutorialManager.Instance)
			{
				MsgTutorialDamage value = new MsgTutorialDamage();
				StageTutorialManager.Instance.SendMessage("OnMsgTutorialDamage", value, SendMessageOptions.DontRequireReceiver);
			}
			GameObjectUtil.SendDelayedMessageToTagObjects("Boss", "OnPlayerDamage", new MsgBossPlayerDamage(false));
			ObjUtil.StopCombo();
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x0014EBD4 File Offset: 0x0014CDD4
		public override void Leave(CharacterState context)
		{
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x0014EBD8 File Offset: 0x0014CDD8
		public override void Step(CharacterState context, float deltaTime)
		{
			bool flag = context.Movement.IsOnGround();
			Vector3 vector = context.Movement.VertVelocity;
			if (flag)
			{
				HitInfo hitInfo;
				if (context.Movement.GetGroundInfo(out hitInfo))
				{
					Vector3 normal = hitInfo.info.normal;
					vector -= Vector3.Project(vector, normal);
					context.Movement.VertVelocity = vector;
				}
			}
			else
			{
				vector += context.Movement.GetGravity() * deltaTime;
				context.Movement.VertVelocity = vector;
			}
			context.Movement.HorzVelocity = context.Movement.GetForwardDir() * context.DefaultSpeed * context.Parameter.m_damageSpeedRate;
			this.m_timer -= deltaTime;
			if (this.m_timer <= context.Parameter.m_damageEnableJumpTime && context.m_input.IsTouched())
			{
				context.ChangeState(STATE_ID.Jump);
				return;
			}
			if (this.m_timer <= 0f)
			{
				if (flag)
				{
					context.ChangeState(STATE_ID.Run);
				}
				else
				{
					context.ChangeState(STATE_ID.Fall);
				}
				return;
			}
			STATE_ID state = STATE_ID.Non;
			if (StateUtil.CheckHitWallAndGoDeadOrStumble(context, deltaTime, ref state))
			{
				context.ChangeState(state);
				return;
			}
		}

		// Token: 0x04003748 RID: 14152
		private float m_timer;
	}
}
