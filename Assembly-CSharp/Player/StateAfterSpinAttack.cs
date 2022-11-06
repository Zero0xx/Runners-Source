using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009BB RID: 2491
	public class StateAfterSpinAttack : FSMState<CharacterState>
	{
		// Token: 0x0600411B RID: 16667 RVA: 0x00152BBC File Offset: 0x00150DBC
		public override void Enter(CharacterState context)
		{
			StateUtil.SetRotationOnGravityUp(context);
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, true);
			this.m_jumpForce = context.Parameter.m_spinAttackForce;
			this.m_speed = Mathf.Max(context.Movement.GetForwardVelocityScalar(), 0f);
			Vector3 a = -context.Movement.GetGravityDir();
			Vector3 forward = context.transform.forward;
			context.Movement.Velocity = forward * this.m_speed + this.m_jumpForce * a;
			context.GetAnimator().CrossFade("Jump", 0.05f);
			context.OnAttack(AttackPower.PlayerSpin, DefensePower.PlayerSpin);
			StateUtil.SetAttackAttributePowerIfPowerType(context, true);
			context.SetAirAction(1);
			this.m_effect = context.GetSpinAttackEffect();
			if (this.m_effect != null)
			{
				this.m_effect.SetValid(true);
			}
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x00152CA4 File Offset: 0x00150EA4
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			if (this.m_effect != null)
			{
				this.m_effect.SetValid(false);
			}
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x00152CCC File Offset: 0x00150ECC
		public override void Step(CharacterState context, float deltaTime)
		{
			Vector3 vector = context.Movement.VertVelocity;
			Vector3 a = context.Movement.Velocity - vector;
			vector += context.Movement.GetGravity() * deltaTime;
			a = context.Movement.GetForwardDir() * StateUtil.GetForwardSpeedAir(context, context.DefaultSpeed, deltaTime);
			context.Movement.Velocity = a + vector;
			STATE_ID state = STATE_ID.Non;
			if (StateUtil.CheckHitWallAndGoDeadOrStumble(context, deltaTime, ref state))
			{
				context.ChangeState(state);
				return;
			}
			if (context.m_input.IsTouched() && StateUtil.CheckAndChangeStateToAirAttack(context, false, false))
			{
				return;
			}
			if (context.Movement.GetVertVelocityScalar() > 0f || !context.Movement.IsOnGround())
			{
				return;
			}
			if (StateUtil.ChangeToJumpStateIfPrecedeInputTouch(context, 0.1f, true))
			{
				return;
			}
			StateUtil.NowLanding(context, true);
			context.ChangeState(STATE_ID.Run);
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x00152DBC File Offset: 0x00150FBC
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			return StateUtil.ChangeAfterSpinattack(context, messageId, msg);
		}

		// Token: 0x040037C7 RID: 14279
		private float m_jumpForce;

		// Token: 0x040037C8 RID: 14280
		private float m_speed;

		// Token: 0x040037C9 RID: 14281
		private CharacterLoopEffect m_effect;
	}
}
