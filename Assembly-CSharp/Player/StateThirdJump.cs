using System;
using Message;
using Tutorial;
using UnityEngine;

namespace Player
{
	// Token: 0x020009A9 RID: 2473
	public class StateThirdJump : FSMState<CharacterState>
	{
		// Token: 0x060040BA RID: 16570 RVA: 0x0014FF88 File Offset: 0x0014E188
		public override void Enter(CharacterState context)
		{
			Vector3 horzVelocity = context.Movement.HorzVelocity;
			bool flag = false;
			JumpParameter enteringParameter = context.GetEnteringParameter<JumpParameter>();
			if (enteringParameter != null)
			{
				flag = enteringParameter.m_onAir;
			}
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, true);
			this.m_jumpForce = context.Parameter.m_jumpForce;
			this.m_addForceTmer = context.Parameter.m_jumpAddSec;
			this.m_addAcc = context.Parameter.m_jumpAddAcc;
			this.m_speed = horzVelocity.magnitude;
			context.Movement.Velocity = horzVelocity + this.m_jumpForce * Vector3.up;
			context.GetAnimator().CrossFade("ThirdJump", 0.05f);
			context.AddAirAction();
			context.OnAttack(AttackPower.PlayerSpin, DefensePower.PlayerSpin);
			StateUtil.SetAttackAttributePowerIfPowerType(context, true);
			if (this.m_effect != null)
			{
				this.m_effect.SetValid(true);
			}
			if (flag)
			{
				StateUtil.Create2ndJumpEffect(context);
			}
			else
			{
				StateUtil.CreateJumpEffect(context);
				CharaSEUtil.PlayJumpSE(context.charaType);
			}
			this.m_specialJumpMagnet = false;
			if (context.NumAirAction > 2)
			{
				StateUtil.SetSpecialtyJumpMagnet(context, CharacterAttribute.POWER, ChaoAbility.MAGNET_POWER_TYPE_JUMP, true);
				StateUtil.SetSpecialtyJumpDestroyEnemy(ChaoAbility.JUMP_DESTROY_ENEMY_AND_TRAP);
				StateUtil.SetSpecialtyJumpDestroyEnemy(ChaoAbility.JUMP_DESTROY_ENEMY);
				this.m_specialJumpMagnet = true;
			}
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x001500C4 File Offset: 0x0014E2C4
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			if (this.m_effect != null)
			{
				this.m_effect.SetValid(false);
			}
			ObjUtil.SendMessageTutorialClear(EventID.JUMP);
			if (this.m_specialJumpMagnet)
			{
				StateUtil.SetSpecialtyJumpMagnet(context, CharacterAttribute.POWER, ChaoAbility.MAGNET_POWER_TYPE_JUMP, false);
			}
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x00150110 File Offset: 0x0014E310
		public override void Step(CharacterState context, float deltaTime)
		{
			Vector3 vector = context.Movement.VertVelocity;
			Vector3 a = context.Movement.Velocity - vector;
			vector += context.Movement.GetGravity() * deltaTime;
			if (this.m_addForceTmer >= 0f)
			{
				this.m_addForceTmer -= deltaTime;
				vector += context.Movement.GetUpDir() * this.m_addAcc * deltaTime;
			}
			float magnitude = a.magnitude;
			if (magnitude < this.m_speed && magnitude < context.DefaultSpeed)
			{
				this.m_speed = context.DefaultSpeed;
			}
			float targetSpeed = Mathf.Max(context.DefaultSpeed, this.m_speed);
			a = context.Movement.GetForwardDir() * StateUtil.GetForwardSpeedAir(context, targetSpeed, deltaTime);
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

		// Token: 0x060040BD RID: 16573 RVA: 0x0015027C File Offset: 0x0014E47C
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			return StateUtil.ChangeAfterSpinattack(context, messageId, msg);
		}

		// Token: 0x04003766 RID: 14182
		private float m_jumpForce;

		// Token: 0x04003767 RID: 14183
		private float m_speed;

		// Token: 0x04003768 RID: 14184
		private float m_addForceTmer;

		// Token: 0x04003769 RID: 14185
		private float m_addAcc;

		// Token: 0x0400376A RID: 14186
		private CharacterLoopEffect m_effect;

		// Token: 0x0400376B RID: 14187
		private bool m_specialJumpMagnet;
	}
}
